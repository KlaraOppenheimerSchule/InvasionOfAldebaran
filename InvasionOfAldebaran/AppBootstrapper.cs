using Caliburn.Micro;
using InvasionOfAldebaran.ViewModels;
using System.Windows;

namespace InvasionOfAldebaran
{
    public class AppBootstrapper : BootstrapperBase
    {
        public AppBootstrapper()
        {
            this.Initialize();
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            this.DisplayRootViewFor<FrameWindowViewModel>();
        }
    }
}