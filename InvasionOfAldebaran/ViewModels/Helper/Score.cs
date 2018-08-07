using InvasionOfAldebaran.Models;

namespace InvasionOfAldebaran.ViewModels
{
	public class Score : NotifyPropertyChangedBase
	{
		private string _name;
		private int _points;
		private int _listPosition;

		public string Name
		{
			get { return _name; }
			set
			{
				_name = value;
				NotifyPropertyChanged(nameof(Name));
			}
		}
		public int Points
		{
			get { return _points; }
			set
			{
				_points = value;
				NotifyPropertyChanged(nameof(Points));
			}
		}
		public int ListPosition
		{
			get { return _listPosition; }
			set
			{
				_listPosition = value;
				NotifyPropertyChanged(nameof(ListPosition));
			}
		}

		public Score(int points, string name)
		{
			this.Points = points;
			this.Name = name;
		}

		public SerializableScore GetSerializableScore()
		{
			var score = new SerializableScore
			{
				Points = this.Points,
				Name = this.Name,
				ListPosition = this.ListPosition
			};

			return score;
		}
	}
}
