using System;
using System.Windows;
using SatsuiInput;
using DemoMVVM.ViewModels;
using DemoMVVM.Views;

namespace DemoMVVM
{
    public partial class Bootstrap
    {
        private InputRecorder inputRecorder = null;

        protected override void OnStartup(StartupEventArgs e)
        {
            try
            {
                // Initializing the recorder
                // We are catching keyboard and pad events
                this.inputRecorder = new InputRecorder(InputType.Keyboard | InputType.Pad);
                this.inputRecorder.Start();

                // Showing the main view
                MainView view = new MainView() { DataContext = new MainViewModel(this.inputRecorder) };
                view.ShowDialog();
            }
            catch(Exception ex)
            {
                // Oopss, an error occured...
                MessageBox.Show(ex.Message, "DemoMVVM", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                Application.Current.Shutdown();
            }
            
        }

        protected override void OnExit(ExitEventArgs e)
        {
            // Stopping the recorder
            if(this.inputRecorder != null) this.inputRecorder.Stop();
        }
    }
}
