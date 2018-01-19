using System.Dynamic;
using System.Windows;
using Caliburn.Micro;
using InvasionOfAldebaran.ViewModels;


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
            
            this.DisplayRootViewFor<MainWindowViewModel>();
        }
    }
}
