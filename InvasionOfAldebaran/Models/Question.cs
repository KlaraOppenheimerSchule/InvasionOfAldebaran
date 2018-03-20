using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Windows.Media;
using InvasionOfAldebaran.Helper;

namespace InvasionOfAldebaran.Models
{
	public class Question
	{
		public string Text { get; private set; }
		public List<Answer> Answers { get; private set; }
		public Answer CorrectAnswer { get; private set; }
		public Difficulty Difficulty { get; private set; }

		public Question(string text, List<Answer> answers, Difficulty difficulty)
		{
			this.Answers = new List<Answer>();
			this.Difficulty = difficulty;
			this.Text = text;

			this.HandleAnswers(answers);
		}
		public Question(string text, Answer answer1, Answer answer2, Answer answer3, Answer answer4, Difficulty difficulty)
		{
			this.Answers = new List<Answer>();
			this.Difficulty = difficulty;
			this.Text = text;

			var answers = new List<Answer>() {answer1, answer2, answer3, answer4};

			this.HandleAnswers(answers);
		}

		private void HandleAnswers(List<Answer> answers)
		{
			if(answers == null)
				throw new ArgumentNullException($@"The provided answerlist {answers} was null");

			var colors = new List<Brush> { Brushes.Green, Brushes.DarkRed, Brushes.Beige, Brushes.DeepPink };
			var random = new Random();
			var correctAnswerHandled = false;

			for (int i = 0; i < answers.Count; i++)
			{
				if (answers[i].IsCorrect && !correctAnswerHandled)
				{
					this.CorrectAnswer = answers[i];
					correctAnswerHandled = true;
				}
				else if (answers[i].IsCorrect && correctAnswerHandled)
				{
					throw new Exception($"The question \"{this.Text}\" contains multiple correct answers!");
				}

				var r = random.Next(0, 3 - i);
				answers[i].Color = colors[r];
				colors.RemoveAt(r);

				this.Answers.Add(answers[i]);
			}
		}
	}
}
