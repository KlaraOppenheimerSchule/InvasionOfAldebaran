using Caliburn.Micro;
using InvasionOfAldebaran.Helper;
using System.Collections.Generic;
using System.Windows.Input;

namespace InvasionOfAldebaran.ViewModels
{
	public class MainMenuViewModel : NotifyPropertyChangedBase
    {
		private List<Score> _highScore;
		private ScoreCompareHelper _scoreHelper;

		public string Name { get; set; }
        public ICommand PlayButtonCommand { get; set; }
        public ICommand CloseButtonCommand { get; set; }
        public int Points { get; set; }
        public int NewPoints { get; set; }
	    public List<Score> Highscore
		{
			get { return this._highScore; }
			set
			{
				this._highScore = value;
				this._highScore.Sort(_scoreHelper);

				for (int i = 0; i < _highScore.Count; i++)
				{
					this._highScore[i].ListPosition = i + 1;
				}
				if (_highScore.Count > 10)
					_highScore.RemoveAt(_highScore.Count - 1);

				NotifyPropertyChanged(nameof(Highscore));
			}
		}

        private FrameWindowViewModel _frameModel;

        public MainMenuViewModel(FrameWindowViewModel frameModel)
        {
            _frameModel = frameModel;
			_scoreHelper = new ScoreCompareHelper();

            this.PlayButtonCommand = new RelayCommand(this.ChangeWindow);
            this.CloseButtonCommand = new RelayCommand(this.CloseWindow);

			_highScore = LoadScore();
		}

        public void AddScore(Score score)
        {
			var newList = _highScore;
			newList.Add(score);
			this.Highscore = newList;
			// ja ich weiß...

			Serializer.SerializeObject<List<Score>>(Highscore, @"../../saves.xml");
        }
		private List<Score> LoadScore()
		{
			var savedScores = Serializer.DeserializeXml<List<Score>>(@"../../saves.xml");

			if (savedScores != null)
				return savedScores as List<Score>;
			else
				throw new System.Exception("Couldnt load saved Scores");
		}

		#region Interface Members

        private void CloseWindow()
        {
            _frameModel.CloseItem(_frameModel);
        }

        private void ChangeWindow()
        {
			_frameModel.ChangeScreen(typeof(PlayViewModel));
        }

        #endregion Interface Members
    }
}