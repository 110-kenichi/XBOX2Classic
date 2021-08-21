using FTD2XX_NET;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XBOX2Classic.Properties;
using XInputDotNetPure;

namespace Zanac.XBOX2Classic
{
    public partial class FormMain : Form
    {
        private GamePadState lastGamePadState;

        public const int FTDI_BAUDRATE = 115200 / 16;
        public const int FTDI_BAUDRATE_MUL = 100;

        private FTD2XX_NET.FTDI ftdi = new FTD2XX_NET.FTDI();

        public FormMain()
        {
            InitializeComponent();

            restoreCheckStatus(1, Settings.Default.DPadUp);
            restoreCheckStatus(2, Settings.Default.DPadLeft);
            restoreCheckStatus(3, Settings.Default.DPadRight);
            restoreCheckStatus(4, Settings.Default.DPadDown);
            restoreCheckStatus(5, Settings.Default.LStickUp);
            restoreCheckStatus(6, Settings.Default.LStickLeft);
            restoreCheckStatus(7, Settings.Default.LStickRight);
            restoreCheckStatus(8, Settings.Default.LStickDown);
            restoreCheckStatus(9, Settings.Default.RStickUp);
            restoreCheckStatus(10, Settings.Default.RStickLeft);
            restoreCheckStatus(11, Settings.Default.RStickRight);
            restoreCheckStatus(12, Settings.Default.RStickDown);
            restoreCheckStatus(13, Settings.Default.Y);
            restoreCheckStatus(14, Settings.Default.X);
            restoreCheckStatus(15, Settings.Default.B);
            restoreCheckStatus(16, Settings.Default.A);
            restoreCheckStatus(17, Settings.Default.LTrigger);
            restoreCheckStatus(18, Settings.Default.LBumper);
            restoreCheckStatus(19, Settings.Default.RTrigger);
            restoreCheckStatus(20, Settings.Default.RBumper);
            restoreCheckStatus(21, Settings.Default.Back);
            restoreCheckStatus(22, Settings.Default.Start);

            timerRapid.Interval = (int)numericUpDownFireRate.Value;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            Settings.Default.DPadUp = storeCheckStatus(1);
            Settings.Default.DPadLeft = storeCheckStatus(2);
            Settings.Default.DPadRight = storeCheckStatus(3);
            Settings.Default.DPadDown = storeCheckStatus(4);
            Settings.Default.LStickUp = storeCheckStatus(5);
            Settings.Default.LStickLeft = storeCheckStatus(6);
            Settings.Default.LStickRight = storeCheckStatus(7);
            Settings.Default.LStickDown = storeCheckStatus(8);
            Settings.Default.RStickUp = storeCheckStatus(9);
            Settings.Default.RStickLeft = storeCheckStatus(10);
            Settings.Default.RStickRight = storeCheckStatus(11);
            Settings.Default.RStickDown = storeCheckStatus(12);
            Settings.Default.Y = storeCheckStatus(13);
            Settings.Default.X = storeCheckStatus(14);
            Settings.Default.B = storeCheckStatus(15);
            Settings.Default.A = storeCheckStatus(16);
            Settings.Default.LTrigger = storeCheckStatus(17);
            Settings.Default.LBumper = storeCheckStatus(18);
            Settings.Default.RTrigger = storeCheckStatus(19);
            Settings.Default.RBumper = storeCheckStatus(20);
            Settings.Default.Back = storeCheckStatus(21);
            Settings.Default.Start = storeCheckStatus(22);
        }

        private void restoreCheckStatus(int row, int val)
        {
            for (int i = 0; i < 8; i++)
            {
                int stat = val & 0x3;
                CheckBox cb = (CheckBox)tableLayoutPanelCheck.GetControlFromPosition(1 + i, row);
                switch (stat)
                {
                    case 0:
                        cb.CheckState = CheckState.Unchecked;
                        break;
                    case 1:
                        cb.CheckState = CheckState.Indeterminate;
                        break;
                    default:
                        cb.CheckState = CheckState.Checked;
                        break;
                }
                val = val >> 2;
            }
        }

        private int storeCheckStatus(int row)
        {
            int stat = 0;
            for (int i = 7; i >= 0; i--)
            {
                stat = stat << 2;
                CheckBox cb = (CheckBox)tableLayoutPanelCheck.GetControlFromPosition(1 + i, row);
                switch (cb.CheckState)
                {
                    case CheckState.Unchecked:
                        stat |= 0;
                        break;
                    case CheckState.Indeterminate:
                        stat |= 1;
                        break;
                    case CheckState.Checked:
                        stat |= 2;
                        break;
                }
            }
            return stat;
        }

        private uint processButton(int row)
        {
            uint stat = 0;
            for (int i = 7; i >= 0; i--)
            {
                stat = stat << 1;
                CheckBox cb = (CheckBox)tableLayoutPanelCheck.GetControlFromPosition(1 + i, row);
                switch (cb.CheckState)
                {
                    case CheckState.Unchecked:
                        stat |= 0;
                        break;
                    case CheckState.Indeterminate:
                        stat |= (uint)rapidFire;
                        break;
                    case CheckState.Checked:
                        stat |= 1;
                        break;
                }
            }
            return stat;
        }

        //int cnt = 0;

        private int rapidFire;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerRapid_Tick(object sender, EventArgs e)
        {
            rapidFire = rapidFire ^ 1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="val"></param>
        /// <param name="row"></param>
        private uint processPseudoAnalog(float val, int row)
        {
            var cb = (CheckBox)tableLayoutPanelCheck.GetControlFromPosition(9, 5);
            if (cb.Tag == null)
                cb.Tag = 0f;

            float lval = (float)cb.Tag;

            lval += Math.Abs(val);

            uint bstat = 0;

            if (lval > 1.0f)
            {
                bstat = processButton(row);
                lval -= 1.0f;
            }

            cb.Tag = lval;

            return bstat;
        }

        private void timerController_Tick(object sender, EventArgs e)
        {
            var stat = GamePad.GetState((PlayerIndex)((int)numericUpDownCtrlId.Value));
            //if (stat.PacketNumber != lastGamePadState.PacketNumber)
            //{
            //cnt = 0;

            //processGuideButton(stat);
            uint bstat = 0;

            var bs = stat.ButtonState;
            //var lbs = lastGamePadState.ButtonState;

            if ((bs & ButtonStates.DPadUp) == ButtonStates.DPadUp)
                bstat |= processButton(1);

            if ((bs & ButtonStates.DPadLeft) == ButtonStates.DPadLeft)
                bstat |= processButton(2);
            if ((bs & ButtonStates.DPadRight) == ButtonStates.DPadRight)
                bstat |= processButton(3);
            if ((bs & ButtonStates.DPadDown) == ButtonStates.DPadDown)
                bstat |= processButton(4);
            if (((CheckBox)tableLayoutPanelCheck.GetControlFromPosition(9, 5)).Checked && stat.ThumbSticks.Left.Y > 0)
            {
                bstat |= processPseudoAnalog(stat.ThumbSticks.Left.Y, 5);
            }
            else if (stat.ThumbSticks.Left.Y > 0.5)
                bstat |= processButton(5);
            if (((CheckBox)tableLayoutPanelCheck.GetControlFromPosition(9, 6)).Checked && stat.ThumbSticks.Left.X < 0)
            {
                bstat |= processPseudoAnalog(stat.ThumbSticks.Left.X, 6);
            }
            else if (stat.ThumbSticks.Left.X < -0.5)
                bstat |= processButton(6);
            if (((CheckBox)tableLayoutPanelCheck.GetControlFromPosition(9, 7)).Checked && stat.ThumbSticks.Left.X > 0)
            {
                bstat |= processPseudoAnalog(stat.ThumbSticks.Left.X, 7);
            }
            else if (stat.ThumbSticks.Left.X > 0.5)
                bstat |= processButton(7);
            if (((CheckBox)tableLayoutPanelCheck.GetControlFromPosition(9, 8)).Checked && stat.ThumbSticks.Left.Y < 0)
            {
                bstat |= processPseudoAnalog(stat.ThumbSticks.Left.Y, 8);
            }
            else if (stat.ThumbSticks.Left.Y < -0.5)
                bstat |= processButton(8);
            if (((CheckBox)tableLayoutPanelCheck.GetControlFromPosition(9, 9)).Checked && stat.ThumbSticks.Right.Y > 0)
            {
                bstat |= processPseudoAnalog(stat.ThumbSticks.Right.Y, 9);
            }
            else if (stat.ThumbSticks.Right.Y > 0.5)
                bstat |= processButton(9);
            if (((CheckBox)tableLayoutPanelCheck.GetControlFromPosition(9, 10)).Checked && stat.ThumbSticks.Right.X < 0)
            {
                bstat |= processPseudoAnalog(stat.ThumbSticks.Right.X, 10);
            }
            else if (stat.ThumbSticks.Right.X < -0.5)
                bstat |= processButton(10);
            if (((CheckBox)tableLayoutPanelCheck.GetControlFromPosition(9, 5)).Checked && stat.ThumbSticks.Right.X > 0)
            {
                bstat |= processPseudoAnalog(stat.ThumbSticks.Right.X, 11);
            }
            else if (stat.ThumbSticks.Right.X > 0.5)
                bstat |= processButton(11);
            if (((CheckBox)tableLayoutPanelCheck.GetControlFromPosition(9, 5)).Checked && stat.ThumbSticks.Right.Y < 0)
            {
                bstat |= processPseudoAnalog(stat.ThumbSticks.Right.Y, 12);
            }
            else if (stat.ThumbSticks.Right.Y < -0.5)
                bstat |= processButton(12);
            if ((bs & ButtonStates.Y) == ButtonStates.Y)
                bstat |= processButton(13);
            if ((bs & ButtonStates.X) == ButtonStates.X)
                bstat |= processButton(14);
            if ((bs & ButtonStates.B) == ButtonStates.B)
                bstat |= processButton(15);
            if ((bs & ButtonStates.A) == ButtonStates.A)
                bstat |= processButton(16);
            if (((CheckBox)tableLayoutPanelCheck.GetControlFromPosition(9, 5)).Checked)
            {
                bstat |= processPseudoAnalog(stat.Triggers.Left, 17);
            }
            else if (stat.Triggers.Left > 0.5)
                bstat |= processButton(17);
            if ((bs & ButtonStates.LeftShoulder) == ButtonStates.LeftShoulder)
                bstat |= processButton(18);
            if (((CheckBox)tableLayoutPanelCheck.GetControlFromPosition(9, 5)).Checked)
            {
                bstat |= processPseudoAnalog(stat.Triggers.Right, 19);
            }
            else if (stat.Triggers.Right > 0.5)
                bstat |= processButton(19);
            if ((bs & ButtonStates.RightShoulder) == ButtonStates.RightShoulder)
                bstat |= processButton(20);
            if ((bs & ButtonStates.Back) == ButtonStates.Back)
                bstat |= processButton(21);
            if ((bs & ButtonStates.Start) == ButtonStates.Start)
                bstat |= processButton(22);

            if (checkBoxInvHiLo.Checked)
                bstat = ~bstat;
            ftdi.Write(new byte[] { (byte)bstat }, 1, ref bstat);
            //}
            //else
            //{
            //    cnt++;
            //}

            lastGamePadState = stat;
        }

        private void checkBoxConn_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxConn.Checked)
            {
                var stat = ftdi.OpenByIndex((uint)numericUpDownPort.Value);
                if (stat == FTDI.FT_STATUS.FT_OK)
                {
                    ftdi.SetBitMode(0x00, FTDI.FT_BIT_MODES.FT_BIT_MODE_RESET);
                    ftdi.SetBitMode(0xff, FTDI.FT_BIT_MODES.FT_BIT_MODE_ASYNC_BITBANG);
                    ftdi.SetBaudRate(FTDI_BAUDRATE * FTDI_BAUDRATE_MUL);
                    ftdi.SetTimeouts(500, 500);
                    ftdi.SetLatency(0);
                }
                else
                {
                    checkBoxConn.Checked = false;
                }
            }
            else
            {
                ftdi.Close();
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            timerRapid.Interval = (int)numericUpDownFireRate.Value;
        }

    }
}
