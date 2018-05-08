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
    }
}