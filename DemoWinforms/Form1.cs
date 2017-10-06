using System;
using System.Windows.Forms;
using SatsuiInput;

namespace DemoWinforms
{
    public partial class Form1 : Form
    {
        delegate void UpdateInputCallback(InputData data);
        private InputRecorder inputRecorder = null;

        public Form1()
        {
            // Initializing UI
            InitializeComponent();

            // Initializing the recorder
            this.inputRecorder = new InputRecorder();
            this.inputRecorder.Pressed += InputRecorder_Pressed;
            this.inputRecorder.Start();
        }

        /// <summary>
        /// Callback receiving input events
        /// </summary>
        /// <param name="data">Informations about the input</param>
        private void InputRecorder_Pressed(InputData data)
        {
            // Thread safe access
            if (this.InvokeRequired)
            {
                UpdateInputCallback d = new UpdateInputCallback(InputRecorder_Pressed);
                this.Invoke(d, new object[] { data });
                return;
            }

            // Showing input informations
            this.lblInput.Text = data.ToString();
        }

        #region Controls events

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.inputRecorder != null) this.inputRecorder.Stop();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.lblInput.Text = "";
        }

        #endregion

    }
}
