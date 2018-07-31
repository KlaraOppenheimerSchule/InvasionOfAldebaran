using Caliburn.Micro;
using InvasionOfAldebaran.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Windows.Input;
using System.Windows.Media;

namespace InvasionOfAldebaran.ViewModels
{
    public class MainMenuViewModel : NotifyPropertyChangedBase
    {
		private List<Score> _highScore;

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
				// todo: Sorting funktioniert evtl nicht
				this._highScore.Sort(new ScoreCompareHelper());

				for (int i = 0; i < _highScore.Count; i++)
				{
					this._highScore[i].ListPosition = i + 1;
				}
				NotifyPropertyChanged(nameof(Highscore));
			}
		}

        private FrameWindowViewModel _frameModel;

        public MainMenuViewModel(FrameWindowViewModel frameModel)
        {
            _frameModel = frameModel;

            this.PlayButtonCommand = new RelayCommand(this.ChangeWindow);
            this.CloseButtonCommand = new RelayCommand(this.CloseWindow);

			this.Highscore = new List<Score>
			{
				new Score(50, "Ein Spieler")
			};
		}

        public void AddScore(Score score)
        {
			this.Highscore.Add(score);
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