using InvasionOfAldebaran.Helper;
using System.Windows.Input;
using System;
using System.Collections.Generic;

namespace InvasionOfAldebaran.ViewModels
{
	public class AddScoreViewModel : NotifyPropertyChangedBase
	{
		private int _points;
		private string _name;
		private bool _buttonEnabled;
		private List<string> _forbiddenStrings;

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
				if (string.IsNullOrEmpty(value))
					return;

				_name = value;

				if (_name.Length > 3)
					this.ButtonEnabled = true;
				else
					this.ButtonEnabled = false;

				NotifyPropertyChanged(nameof(Name));
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

		public delegate void ScoreAddedEventHandler(Score score);
		public event ScoreAddedEventHandler ScoreAdded;

		public AddScoreViewModel(FrameWindowViewModel frameModel)
		{
			this._frameModel = frameModel;
			this.ButtonEnabled = false;
			this.SendScoreCommand = new RelayCommand(this.SendScoreAndChangeMainMenu);
			this._forbiddenStrings = LoadWordFilter();
		}

		public void SetPoints(int points)
		{
			this.Points = points;
		}

		private void SendScoreAndChangeMainMenu()
		{
			var score = new Score(Points, HandleName(_name));
			_points = 0;
			_name = "";
			NotifyPropertyChanged(nameof(Name));

			this.ScoreAdded.Invoke(score);
		}

		private List<string> LoadWordFilter()
		{
			var filter = Serializer.DeserializeXml<List<string>>(@"../../wordfilter.xml");

			if (filter != null)
				return filter as List<string>;
			else
				throw new System.Exception("Couldnt load saved Scores");
		}

		private string HandleName(string value)
		{
			var censoredString = value;

			foreach (var word in _forbiddenStrings)
			{
				if(censoredString.ToLower().Contains(word))
				{
					string censor = "";

					for (int i = 0; i < word.Length; i++)
					{
						censor += "#";
					}
					censoredString = censoredString.ToUpper().Replace(word.ToUpper(), censor);
				}
			}
			var shortenedString = "";

			if (censoredString.Length > 20)
				shortenedString = censoredString.Substring(0, 20);
			else
				shortenedString = censoredString;

			return shortenedString;
		}

	}
}
