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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InvasionOfAldebaran.Views
{
	/// <summary>
	/// Interaction logic for MainMenuView.xaml
	/// </summary>
	public partial class MainMenuView : UserControl
	{
        private MainMenuViewModel MainView;

		public MainMenuView()
		{
			InitializeComponent();
            this.MainView = new MainMenuViewModel();
		}

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            this.MainView.Change_Window();
        }

    }
}
