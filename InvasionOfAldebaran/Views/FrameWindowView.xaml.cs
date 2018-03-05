using InvasionOfAldebaran.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace InvasionOfAldebaran.Views
{
	/// <summary>
	/// Interaction logic for FrameWindowView.xaml
	/// </summary>
	public partial class FrameWindowView : Window
	{
        private FrameWindowViewModel ViewModel;

        public FrameWindowView()
        {
            InitializeComponent();
            this.ViewModel = new FrameWindowViewModel();

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.ViewModel.TryClose();
        }
    }
}
