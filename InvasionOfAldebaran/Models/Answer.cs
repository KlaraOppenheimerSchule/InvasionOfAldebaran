using System;
using System.Windows.Media;
using System.Xml.Serialization;

namespace InvasionOfAldebaran.Models
{
	public class Answer
	{
		public string Text { get; private set; }
		public bool IsCorrect { get; private set; }
		public Brush Color { get; set; }

		public Answer(string text, bool isCorrect)
		{
			this.Text = text;
			this.IsCorrect = isCorrect;
		}
	}
}