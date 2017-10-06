using System;
using System.ComponentModel;
using SatsuiInput;
using SatsuiInput.Pad;

namespace DemoMVVM.Models
{
    public class MyAction : INotifyPropertyChanged
    {
        #region Description

        private string description = "";
        public string Description
        {
            get { return this.description; }
        }

        #endregion

        #region Device

        public string Device
        {
            get
            {
                if (this.inputData == null) return "";
                else if (this.inputData.Type == InputType.Pad) return ((PadInputData)this.inputData).Device;
                else return this.inputData.Type.ToString();
            }
        }


        #endregion  

        #region InputId

        public string InputId
        {
            get
            {
                if (this.inputData != null) return this.inputData.Id;
                else return "";
            }
        }

        #endregion  

        private InputData inputData = null;

        public MyAction(string description)
        {
            this.description = description;
        }

        #region SetInput

        public void SetInput(InputData data)
        {
            this.inputData = data;
            this.NotifyPropertyChanged("Device");
            this.NotifyPropertyChanged("InputId");
        }

        #endregion

        #region PropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(info));
        }

        #endregion
    }
}
