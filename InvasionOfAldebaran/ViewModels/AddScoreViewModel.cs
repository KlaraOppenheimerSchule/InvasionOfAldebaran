using InvasionOfAldebaran.Helper;
using System.Windows.Input;

namespace InvasionOfAldebaran.ViewModels
{
	public class AddScoreViewModel : NotifyPropertyChangedBase
	{
		private int _points;
		private string _name;
		private bool _buttonEnabled;

		private FrameWindowViewModel _frameModel;

		public int Points
		{
			get { return this._points; }
			private set
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

				if (this._name.Length > 3)
					this.ButtonEnabled = true;
				else
					this.ButtonEnabled = false;
			}
		}

		public bool ButtonEnabled
		{
			get { return this._buttonEnabled; }
			private set
			{
				this._buttonEnabled = value;
				this.NotifyPropertyChanged(nameof(this.ButtonEnabled));
			}
		}

		public ICommand SendScoreCommand { get; private set; }

		public AddScoreViewModel(FrameWindowViewModel frameModel, int points)
		{
			this._frameModel = frameModel;
			this.Points = points;
			this.ButtonEnabled = false;
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
