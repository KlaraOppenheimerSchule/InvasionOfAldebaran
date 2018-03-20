using InvasionOfAldebaran.ViewModels;
using System.Windows;

namespace InvasionOfAldebaran.Views
{
	/// <summary>
	/// Interaction logic for FrameWindowView.xaml
	/// </summary>
	public partial class FrameWindowView : Window
	{
        private FrameWindowViewModel _ViewModel;

        public FrameWindowView()
        {
	        this.InitializeComponent();
            _ViewModel = new FrameWindowViewModel();
            
        }

		//Todo: Was hats denn damit auf sich?
        private void WindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _ViewModel.WindowClosing(sender, e);

        }
    }
}
