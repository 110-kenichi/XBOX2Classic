using System;
using System.Runtime.InteropServices;
using Zanac.XBOX2Classic;
using SharpDX;
using SharpDX.DirectInput;
using static System.Windows.Forms.AxHost;
using System.Reflection;
using System.Diagnostics;

namespace XInputDotNetPure
{
    public struct GamePadThumbSticks
    {
        public struct StickValue
        {
            float x, y;

            internal StickValue(float x, float y)
            {
                this.x = x;
                this.y = y;
            }

            public float X
            {
                get { return x; }
            }

            public float Y
            {
                get { return y; }
            }
        }

        StickValue left, right;

        internal GamePadThumbSticks(StickValue left, StickValue right)
        {
            this.left = left;
            this.right = right;
        }

        public StickValue Left
        {
            get { return left; }
        }

        public StickValue Right
        {
            get { return right; }
        }
    }

    public struct GamePadTriggers
    {
        float left;
        float right;

        internal GamePadTriggers(float left, float right)
        {
            this.left = left;
            this.right = right;
        }

        public float Left
        {
            get { return left; }
        }

        public float Right
        {
            get { return right; }
        }
    }

    public enum ButtonStates
    {
        DPadUp = 0x00000001,
        DPadDown = 0x00000002,
        DPadLeft = 0x00000004,
        DPadRight = 0x00000008,
        Start = 0x00000010,
        Back = 0x00000020,
        LeftThumb = 0x00000040,
        RightThumb = 0x00000080,
        LeftShoulder = 0x0100,
        RightShoulder = 0x0200,
        Guide = 0x0400,
        A = 0x1000,
        B = 0x2000,
        X = 0x4000,
        Y = 0x8000
    }

    public struct GamePadState
    {
        bool isConnected;
        uint packetNumber;
        GamePadThumbSticks thumbSticks;
        GamePadTriggers triggers;

        internal GamePadState(bool isConnected, NativeMethods.XINPUT_STATE rawState, GamePadDeadZone deadZone)
        {
            this.isConnected = isConnected;

            if (!isConnected)
            {
                rawState.dwPacketNumber = 0;
                rawState.Gamepad.wButtons = 0;
                rawState.Gamepad.bLeftTrigger = 0;
                rawState.Gamepad.bRightTrigger = 0;
                rawState.Gamepad.sThumbLX = 0;
                rawState.Gamepad.sThumbLY = 0;
                rawState.Gamepad.sThumbRX = 0;
                rawState.Gamepad.sThumbRY = 0;
            }

            packetNumber = rawState.dwPacketNumber;

            ButtonState = (ButtonStates)rawState.Gamepad.wButtons;

            thumbSticks = new GamePadThumbSticks(
                Utils.ApplyLeftStickDeadZone(rawState.Gamepad.sThumbLX, rawState.Gamepad.sThumbLY, deadZone),
                Utils.ApplyRightStickDeadZone(rawState.Gamepad.sThumbRX, rawState.Gamepad.sThumbRY, deadZone)
            );
            triggers = new GamePadTriggers(
                Utils.ApplyTriggerDeadZone(rawState.Gamepad.bLeftTrigger, deadZone),
                Utils.ApplyTriggerDeadZone(rawState.Gamepad.bRightTrigger, deadZone)
            );
        }

        public uint PacketNumber
        {
            get { return packetNumber; }
        }

        public bool IsConnected
        {
            get { return isConnected; }
        }

        public ButtonStates ButtonState
        {
            get;
            private set;
        }

        public GamePadTriggers Triggers
        {
            get { return triggers; }
        }

        public GamePadThumbSticks ThumbSticks
        {
            get { return thumbSticks; }
        }
    }

    public enum PlayerIndex
    {
        One = 0,
        Two,
        Three,
        Four
    }

    public enum GamePadDeadZone
    {
        Circular,
        IndependentAxes,
        None
    }

    public class GamePad
    {
        static DirectInput dinput;

        static Joystick joystick = null;

        static Guid joystickGuid = Guid.Empty;

        static DirectInput DInput
        {
            get
            {
                if (dinput == null)
                    dinput = new DirectInput();
                return dinput;
            }
        }

        static Joystick Joystick
        {
            get
            {
                // ゲームパッドを探す
                //if (joystickGuid == Guid.Empty)
                //{
                //    foreach (DeviceInstance device in DInput.GetDevices(DeviceType.Gamepad, DeviceEnumerationFlags.AllDevices))
                //    {
                //        joystickGuid = device.InstanceGuid;
                //        // 見つかった場合
                //        if (joystickGuid != Guid.Empty)
                //        {
                //            // ゲームパッドの取得
                //            joystick?.Dispose();
                //            joystick = new Joystick(DInput, joystickGuid);
                //            // ゲームパッドが取得できた場合
                //            if (joystick != null)
                //            {
                //                // バッファサイズを指定
                //                joystick.Properties.BufferSize = 128;
                //            }
                //        }
                //        break;
                //    }
                //}
                // ジョイスティックを探す
                if (joystickGuid == Guid.Empty)
                {
                    foreach (DeviceInstance device in DInput.GetDevices(DeviceType.Joystick, DeviceEnumerationFlags.AllDevices))
                    {
                        joystickGuid = device.InstanceGuid;
                        // 見つかった場合
                        if (joystickGuid != Guid.Empty)
                        {
                            // ゲームパッドの取得
                            joystick?.Dispose();
                            joystick = new Joystick(DInput, joystickGuid);
                            // ゲームパッドが取得できた場合
                            if (joystick != null)
                            {
                                // バッファサイズを指定
                                joystick.Properties.BufferSize = 128;
                            }
                        }
                        break;
                    }
                }

                return joystick;
            }
        }

        public static GamePadState GetState(PlayerIndex playerIndex)
        {
            return GetState(playerIndex, GamePadDeadZone.IndependentAxes);
        }

        public static GamePadState GetStateX()
        {
            GamePadState state = new GamePadState();

            // ゲームパッドの入力を取得
            try
            {
                Joystick?.Acquire();
                Joystick?.Poll();
            }
            catch
            {
                // ゲームパッドが抜けた
                Joystick?.Dispose();
                joystick = null;
                return state;
            }
            // ゲームパッドのデータ取得
            var jState = joystick?.GetCurrentState();
            // 取得できない場合
            if (jState == null)
            {
                return state;
            }

            NativeMethods.XINPUT_STATE xstate = new NativeMethods.XINPUT_STATE();

            xstate.Gamepad.sThumbLX = (short)((int)jState.X - 32768);
            xstate.Gamepad.sThumbLY = (short)((int)jState.Y - 32768);
            xstate.Gamepad.sThumbRX = (short)((int)jState.RotationZ - 32768);
            xstate.Gamepad.sThumbRY = (short)((int)jState.Z - 32768);

            Debug.WriteLine(jState.X);

            // POVの場合
            if (jState.PointOfViewControllers[0] == 0)
                xstate.Gamepad.wButtons |= (ushort)ButtonStates.DPadUp;
            if (jState.PointOfViewControllers[0] == 9000)
                xstate.Gamepad.wButtons |= (ushort)ButtonStates.DPadRight;
            if (jState.PointOfViewControllers[0] == 18000)
                xstate.Gamepad.wButtons |= (ushort)ButtonStates.DPadDown;
            if (jState.PointOfViewControllers[0] == 27000)
                xstate.Gamepad.wButtons |= (ushort)ButtonStates.DPadLeft;

            if (jState.Buttons[0])
                xstate.Gamepad.wButtons |= (ushort)ButtonStates.X;
            if (jState.Buttons[1])
                xstate.Gamepad.wButtons |= (ushort)ButtonStates.Y;
            if (jState.Buttons[2])
                xstate.Gamepad.wButtons |= (ushort)ButtonStates.A;
            if (jState.Buttons[3])
                xstate.Gamepad.wButtons |= (ushort)ButtonStates.B;
            if (jState.Buttons[4])
                xstate.Gamepad.wButtons |= (ushort)ButtonStates.LeftShoulder;
            if (jState.Buttons[5])
                xstate.Gamepad.wButtons |= (ushort)ButtonStates.RightShoulder;
            if (jState.Buttons[6])
                xstate.Gamepad.bLeftTrigger = 255;
            if (jState.Buttons[7])
                xstate.Gamepad.bRightTrigger = 255;
            if (jState.Buttons[8])
                xstate.Gamepad.wButtons |= (ushort)ButtonStates.LeftThumb;
            if (jState.Buttons[9])
                xstate.Gamepad.wButtons |= (ushort)ButtonStates.RightThumb;
            if (jState.Buttons[10])
                xstate.Gamepad.wButtons |= (ushort)ButtonStates.Back;
            if (jState.Buttons[11])
                xstate.Gamepad.wButtons |= (ushort)ButtonStates.Start;
            if (jState.Buttons[12])
                xstate.Gamepad.wButtons |= (ushort)ButtonStates.Guide;

            return new GamePadState(true, xstate, GamePadDeadZone.IndependentAxes);
        }

        public static GamePadState GetState(PlayerIndex playerIndex, GamePadDeadZone deadZone)
        {
            NativeMethods.XINPUT_STATE state;
            uint result = NativeMethods.XInputGetState((uint)playerIndex, out state);
            return new GamePadState(result == Utils.Success, state, deadZone);
        }

        public static void SetVibration(PlayerIndex playerIndex, float leftMotor, float rightMotor)
        {
            NativeMethods.XINPUT_VIBRATION vib = new NativeMethods.XINPUT_VIBRATION();
            vib.LeftMotorSpeed = (ushort)(65535 * leftMotor);
            vib.RightMotorSpeed = (ushort)(65535 * rightMotor);

            NativeMethods.XInputSetState((uint)playerIndex, ref vib);
        }

    }
}
