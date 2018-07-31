using Caliburn.Micro;
using InvasionOfAldebaran.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace InvasionOfAldebaran.ViewModels
{
	public class AddScoreViewModel : NotifyPropertyChangedBase
	{
		private int _points;
		private string _name;

		private FrameWindowViewModel _frameModel;

		public int Points
		{
			get { return this._points; }
			set
			{
				this._points = value;
				this.NotifyPropertyChanged(nameof(this.Points));
			}
		}
		public string Name
		{
			get { return this._name; }
			set
			{
				if(!string.IsNullOrEmpty(value))
					this._name = value;
			}
		}

		public ICommand SendScoreCommand { get; private set; }

		public AddScoreViewModel(FrameWindowViewModel frameModel, int points)
		{
			this._frameModel = frameModel;
			this.Points = points;
			this.SendScoreCommand = new RelayCommand(this.SendScoreAndChangeMainMenu);
		}

		private void SendScoreAndChangeMainMenu()
		{
			var score = new Score(this.Points, this.Name);
			this._frameModel.SetNewHighScore(score);

			this._frameModel.ChangeScreen(typeof(MainMenuViewModel));
		}
	}
}
