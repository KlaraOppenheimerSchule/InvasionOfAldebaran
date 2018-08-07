using Caliburn.Micro;
using InvasionOfAldebaran.Helper;
using InvasionOfAldebaran.Models;
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

			List<Score> scores = new List<Score>();
			scores = LoadScores();

			Highscore = scores;
		}

        public void AddScore(Score score)
        {
			var newList = new List<Score>();
			newList.AddRange(_highScore);
			newList.Add(score);
			Highscore = newList;
			// ja ich weiß...

			var serializableScores = new List<SerializableScore>();
			_highScore.ForEach(s => serializableScores.Add(s.GetSerializableScore()));

			Serializer.SerializeObject<List<SerializableScore>>(serializableScores, @"../../saves.xml");
        }
		private List<Score> LoadScores()
		{
			var savedScores = (List<SerializableScore>)Serializer.DeserializeXml<List<SerializableScore>>(@"../../saves.xml");

			if (savedScores != null)
			{
				List<Score> scores = new List<Score>();
				savedScores.ForEach(s => scores.Add(new Score(s.Points, s.Name)));

				return scores as List<Score>;
			}
			else
				throw new System.Exception("Couldnt load saved Scores");
		}

		#region Interface Members

        private void CloseWindow()
        {
			var serializableScores = new List<SerializableScore>();
			_highScore.ForEach(s => serializableScores.Add(s.GetSerializableScore()));

			Serializer.SerializeObject<List<SerializableScore>>(serializableScores, @"../../saves.xml");

			_frameModel.CloseItem(_frameModel);
        }

        private void ChangeWindow()
        {
			_frameModel.ChangeScreen(typeof(PlayViewModel));
        }

        #endregion Interface Members
    }
}