using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvasionOfAldebaran.Models
{
	public class Question
	{
		public string Text { get; private set; }
		public List<Answer> Answers { get; private set; }
		public Difficulty Difficulty { get; private set; }

		public Question(string text, List<Answer> answers, Difficulty difficulty)
		{
			this.Difficulty = difficulty;
			this.Answers = answers;
			this.Text = text;
		}
	}
}
