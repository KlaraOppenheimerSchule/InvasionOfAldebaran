using System.Collections;

namespace InvasionOfAldebaran.ViewModels
{
	public class Score
	{
		public string Name { get; set; }
		public int Points { get; set; }
		public string ScoreString
		{
			get { return ListPosition.ToString() + ". " + this.Name + " : " + this.Points.ToString(); }
		}
		public int ListPosition { get; set; }

		public Score(int points, string name)
		{
			this.Name = name;
			this.Points = points;
		}
	}
}