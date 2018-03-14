namespace InvasionOfAldebaran.Models
{
	public class Answer
	{
		public string Text { get; private set; }
		public bool IsCorrect { get; private set; }

		public Answer(string text, bool isCorrect)
		{
			this.Text = text;
			this.IsCorrect = isCorrect;
		}
	}
}