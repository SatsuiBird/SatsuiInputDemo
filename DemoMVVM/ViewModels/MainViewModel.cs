using System;
using System.ComponentModel;
using System.Collections.ObjectModel;
using SatsuiInput;
using DemoMVVM.Models;

namespace DemoMVVM.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        #region MyActions

        private ObservableCollection<MyAction> myActions;
        public ObservableCollection<MyAction> MyActions
        {
            get { return this.myActions; }
            set
            {
                if (value != this.myActions)
                {
                    this.myActions = value;
                    this.NotifyPropertyChanged("MyActions");
                }
            }
        }

        #endregion

        #region SelectedAction

        private MyAction selectedAction = null;
        public MyAction SelectedAction
        {
            get { return this.selectedAction; }
            set
            {
                if(value != this.selectedAction)
                {
                    this.selectedAction = value;
                    this.NotifyPropertyChanged("SelectedAction");
                }
            }
        }

        #endregion

        #region DetectedAction

        private MyAction detectedAction = null;
        public MyAction DetectedAction
        {
            get { return this.detectedAction; }
            set
            {
                if(value != this.detectedAction)
                {
                    this.detectedAction = value;
                    this.NotifyPropertyChanged("DetectedAction");
                }
            }
        }

        #endregion  

        private InputRecorder inputRecorder;

        public MainViewModel(InputRecorder recorder)
        {
            // Catching input events
            this.inputRecorder = recorder;
            this.inputRecorder.Pressed += InputRecorder_Pressed;

            // Populating actions
            this.MyActions = new ObservableCollection<MyAction>();
            this.MyActions.Add(new MyAction("First action"));
            this.MyActions.Add(new MyAction("Second action"));
            this.MyActions.Add(new MyAction("Last action"));
        }

        /// <summary>
        /// Callback receiving input events
        /// </summary>
        /// <param name="data">Informations about the input</param>
        private void InputRecorder_Pressed(InputData data)
        {
            if(this.SelectedAction != null)
            {
                if(!data.Pressed)
                {
                    // Update input for the selected action
                    this.SelectedAction.SetInput(data);
                    this.SelectedAction = null;
                }
            }
            else
            {
                // Detecting the action associated with the input
                if (data.Pressed)
                {
                    foreach (MyAction action in this.MyActions)
                    {
                        if (action.InputId == data.Id)
                        {
                            this.DetectedAction = action;
                            return;
                        }
                    }
                }
                else this.DetectedAction = null;
            }
        }

        #region PropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(info));
        }

        #endregion
    }
}
