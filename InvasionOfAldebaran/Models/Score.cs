namespace InvasionOfAldebaran.ViewModels
{
	public class Score
	{
		public string Name { get; set; }
		public string Points { get; set; }

		public Score(int points, string name)
		{
			this.Name = name;
			this.Points = points.ToString();
		}

		public override string ToString()
		{
			return "" + this.Name + " : " + this.Points;
		}
	}
}