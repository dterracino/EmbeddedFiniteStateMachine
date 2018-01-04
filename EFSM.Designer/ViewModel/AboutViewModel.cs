using Cas.Common.WPF.Behaviors;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Reflection;
using System.Windows.Input;

namespace EFSM.Designer.ViewModel
{
    public class AboutViewModel : ViewModelBase, ICloseableViewModel
    {
        public AboutViewModel()
        {
            CloseCommand = new RelayCommand(OnClose);
        }

        public ICommand CloseCommand { get; }

        public Version Version => GetRunningVersion();

        private void OnClose()
        {
            Close?.Invoke(this, new CloseEventArgs(true));
        }

        public event EventHandler<CloseEventArgs> Close;

        public bool CanClose() => true;

        public void Closed()
        {
        }

        private Version GetRunningVersion()
        {
            try
            {
                return System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion;
            }
            catch (Exception)
            {
                return Assembly.GetExecutingAssembly().GetName().Version;
            }
        }
    }
}
