using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zanac.XBOX2Classic
{
    public class NativeMethods
    {
        static NativeMethods()
        {
            // Try XInput 1.3 first as it has all the features we need.
            IntPtr mHandle = LoadLibrary("xinput1_3.dll");
            // Look for XInput 1.4 as a backup (newer machines may not have 1.3 at all).
            if (mHandle == IntPtr.Zero)
                mHandle = LoadLibrary("xinput1_4.dll");
            // Look for XInput 9.1.0 as a last resort! One of the others should exist but we may as well try to load it.
            if (mHandle == IntPtr.Zero)
                mHandle = LoadLibrary("xinput9_1_0.dll");
            if (mHandle != IntPtr.Zero)
            {
                var ptr = GetProcAddress(mHandle, (IntPtr)100); // Ordinal 100 is the same as XInputGetState but supports the Guide button.
                XInputGetState = (XInputGetStateDelegate)Marshal.GetDelegateForFunctionPointer(ptr, typeof(XInputGetStateDelegate));

                ptr = GetProcAddress(mHandle, "XInputSetState");
                XInputSetState = (XInputSetStateDelegate)Marshal.GetDelegateForFunctionPointer(ptr, typeof(XInputSetStateDelegate));
            }
        }

        [DllImport("kernel32", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern IntPtr LoadLibrary(string lpFileName);

        [DllImport("kernel32", SetLastError = true)]
        public static extern bool FreeLibrary(IntPtr hModule);

        [DllImport("kernel32", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = false)]
        public static extern IntPtr GetProcAddress(IntPtr hModule, string lpProcName);

        [DllImport("kernel32", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = false)]
        public static extern IntPtr GetProcAddress(IntPtr hModule, IntPtr lpProcName);

        public delegate uint XInputGetStateDelegate(uint playerIndex, out XINPUT_STATE state);

        public static XInputGetStateDelegate XInputGetState;

        public delegate void XInputSetStateDelegate(uint playerIndex, ref XINPUT_VIBRATION pVibration);

        public static XInputSetStateDelegate XInputSetState;

        /// <summary>
        /// Xinput対応コントローラーの振動機能の状態を指定します。
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct XINPUT_VIBRATION
        {
            /// <summary>
            /// 左モーターの速度。0は全く振動せず、65535では
            /// 完全に振動する。大きく動く方。
            /// </summary>
            public ushort LeftMotorSpeed;

            /// <summary>
            /// 右モーターの速度。0は全く振動せず、65535では
            /// 完全に振動する。小さく動く方。
            /// </summary>
            public ushort RightMotorSpeed;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct XINPUT_STATE
        {
            public uint dwPacketNumber;
            public XINPUT_GAMEPAD Gamepad;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct XINPUT_GAMEPAD
        {
            public ushort wButtons;
            public byte bLeftTrigger;
            public byte bRightTrigger;
            public short sThumbLX;
            public short sThumbLY;
            public short sThumbRX;
            public short sThumbRY;
        }

        // System.Windows.Forms.UnsafeNativeMethods
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern int GetKeyboardState(byte[] keystate);

        // System.Windows.Forms.UnsafeNativeMethods
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool BlockInput([MarshalAs(UnmanagedType.Bool)] [In] bool fBlockIt);

        [DllImport("user32.dll")]
        public static extern IntPtr GetMessageExtraInfo();


        struct INPUT
        {
            public INPUTType type;
            public INPUTUnion Event;
        }

        [StructLayout(LayoutKind.Explicit)]
        struct INPUTUnion
        {
            [FieldOffset(0)]
            internal MOUSEINPUT mi;
            [FieldOffset(0)]
            internal KEYBDINPUT ki;
            [FieldOffset(0)]
            internal HARDWAREINPUT hi;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct MOUSEINPUT
        {
            public int dx;
            public int dy;
            public int mouseData;
            public MOUSEEVENTF dwFlags;
            public uint time;
            public IntPtr dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct KEYBDINPUT
        {
            public ushort wVk;
            public ushort wScan;
            public KEYEVENTF dwFlags;
            public int time;
            public IntPtr dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct HARDWAREINPUT
        {
            public int uMsg;
            public short wParamL;
            public short wParamH;
        }

        enum INPUTType : uint
        {
            INPUT_MOUSE = 0,
            INPUT_KEYBOARD = 1
        }

        [Flags]
        enum KEYEVENTF : uint
        {
            EXTENDEDKEY = 0x0001,
            KEYUP = 0x0002,
            SCANCODE = 0x0008,
            UNICODE = 0x0004
        }

        public const int SCREEN_LENGTH = 0x10000;

        [Flags]
        enum MOUSEEVENTF : uint
        {
            MOVED = 0x0001,
            LEFTDOWN = 0x0002,
            LEFTUP = 0x0004,
            RIGHTDOWN = 0x0008,
            RIGHTUP = 0x0010,
            MIDDLEDOWN = 0x0020,
            MIDDLEUP = 0x0040,
            WHEEL = 0x0800,
            XDOWN = 0x0100,
            XUP = 0x0200,
            ABSOLUTE = 0x8000,

            XBUTTON1 = 0x00000001,
            XBUTTON2 = 0x00000002
        }

        [DllImport("user32.dll", SetLastError = true)]
        static extern UInt32 SendInput(int numberOfInputs, INPUT[] inputs, int sizeOfInputStructure);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern uint MapVirtualKey(uint uCode, uint uMapType);

        public static bool IsMouseButton(VkKeys key)
        {
            return key == VkKeys.LButton ||
                key == VkKeys.RButton ||
                key == VkKeys.MButton ||
                key == VkKeys.XButton1 ||
                key == VkKeys.XButton2;
        }

        private static bool isExtendedKey(VkKeys key)
        {
            return key == VkKeys.LWin ||
                key == VkKeys.RControlKey ||
                key == VkKeys.RWin ||
                key == VkKeys.RMenu ||
                key == VkKeys.Apps ||
                key == VkKeys.Insert ||
                key == VkKeys.Home ||
                key == VkKeys.PageUp ||
                key == VkKeys.PageDown ||
                key == VkKeys.Delete ||
                key == VkKeys.End ||
                key == VkKeys.Up ||
                key == VkKeys.Down ||
                key == VkKeys.Left ||
                key == VkKeys.Right ||
                key == VkKeys.Divide ||
                key == VkKeys.Return ||
                key == VkKeys.MediaNextTrack ||
                key == VkKeys.MediaPreviousTrack ||
                key == VkKeys.MediaStop ||
                key == VkKeys.MediaPlayPause ||
                key == VkKeys.VolumeMute ||
                key == VkKeys.VolumeUp ||
                key == VkKeys.VolumeDown ||
                key == VkKeys.SelectMedia ||
                key == VkKeys.LaunchMail ||
                key == VkKeys.LaunchApplication1 ||
                key == VkKeys.LaunchApplication2 ||
                key == VkKeys.BrowserSearch ||
                key == VkKeys.BrowserHome ||
                key == VkKeys.BrowserBack ||
                key == VkKeys.BrowserForward ||
                key == VkKeys.BrowserStop ||
                key == VkKeys.BrowserRefresh ||
                key == VkKeys.BrowserFavorites;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="pressing"></param>
        public static void ProcessMouseMove(int dx, int dy)
        {
            INPUT[] inputs = new INPUT[1];

            inputs[0].type = INPUTType.INPUT_MOUSE;
            inputs[0].Event.mi.dwFlags = MOUSEEVENTF.MOVED;
            inputs[0].Event.mi.dx = dx;
            inputs[0].Event.mi.dy = dy;
            inputs[0].Event.mi.mouseData = 0;
            inputs[0].Event.mi.dwExtraInfo = IntPtr.Zero;
            inputs[0].Event.mi.time = 0;

            uint cSuccess = SendInput(1, inputs, Marshal.SizeOf(typeof(INPUT)));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="pressing"></param>
        public static void ProcessMouseWheel(float delta)
        {
            INPUT[] inputs = new INPUT[1];

            inputs[0].type = INPUTType.INPUT_MOUSE;
            inputs[0].Event.mi.dwFlags = MOUSEEVENTF.WHEEL;
            inputs[0].Event.mi.dx = 0;
            inputs[0].Event.mi.dy = 0;
            inputs[0].Event.mi.mouseData = (int)(delta * SystemInformation.MouseWheelScrollDelta);
            inputs[0].Event.mi.dwExtraInfo = IntPtr.Zero;
            inputs[0].Event.mi.time = 0;

            uint cSuccess = SendInput(1, inputs, Marshal.SizeOf(typeof(INPUT)));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="pressing"></param>
        public static void ProcessKey(VkKeys key, bool pressing)
        {
            INPUT[] inputs = new INPUT[1];
            if (IsMouseButton(key))
            {
                inputs[0].type = INPUTType.INPUT_MOUSE;
                inputs[0].Event.mi.dx = 0;
                inputs[0].Event.mi.dy = 0;
                inputs[0].Event.mi.mouseData = 0;
                switch (key)
                {
                    case VkKeys.LButton:
                        inputs[0].Event.mi.dwFlags = pressing ? MOUSEEVENTF.LEFTDOWN : MOUSEEVENTF.LEFTUP;
                        break;
                    case VkKeys.RButton:
                        inputs[0].Event.mi.dwFlags = pressing ? MOUSEEVENTF.RIGHTDOWN : MOUSEEVENTF.RIGHTUP;
                        break;
                    case VkKeys.MButton:
                        inputs[0].Event.mi.dwFlags = pressing ? MOUSEEVENTF.MIDDLEDOWN : MOUSEEVENTF.MIDDLEUP;
                        break;
                    case VkKeys.XButton1:
                        inputs[0].Event.mi.dwFlags = pressing ? MOUSEEVENTF.XDOWN : MOUSEEVENTF.XUP;
                        inputs[0].Event.mi.mouseData = (int)MOUSEEVENTF.XBUTTON1;
                        break;
                    case VkKeys.XButton2:
                        inputs[0].Event.mi.dwFlags = pressing ? MOUSEEVENTF.XDOWN : MOUSEEVENTF.XUP;
                        inputs[0].Event.mi.mouseData = (int)MOUSEEVENTF.XBUTTON2;
                        break;
                }
            }
            else
            {
                inputs[0].type = INPUTType.INPUT_KEYBOARD;
                inputs[0].Event.ki.dwFlags = KEYEVENTF.SCANCODE |
                    (pressing ? 0 : KEYEVENTF.KEYUP) |
                    (isExtendedKey(key) ? KEYEVENTF.EXTENDEDKEY : 0);
                MapVirtualKeyEx(key, inputs);
            }
            uint cSuccess = SendInput(1, inputs, Marshal.SizeOf(typeof(INPUT)));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="inputs"></param>
        private static void MapVirtualKeyEx(VkKeys key, INPUT[] inputs)
        {
            switch ((int)key)
            {
                case 0x1c://変換キー
                    inputs[0].Event.ki.wScan = 0x0029;
                    break;
                case 0x1d://無変換キー
                    inputs[0].Event.ki.wScan = 0x007b;
                    break;
                case 0xf2://ひらがな/カタカナキー
                    inputs[0].Event.ki.wScan = 0x0070;
                    break;
                case 0xf3://全角/半角
                case 0xf4:
                    inputs[0].Event.ki.wScan = 0x0029;
                    break;
                default:
                    inputs[0].Event.ki.wScan = (ushort)MapVirtualKey((uint)key, (uint)0x0);
                    break;
            }
        }


    }
}
