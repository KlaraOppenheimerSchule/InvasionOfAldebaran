using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Media;
using InvasionOfAldebaran.Helper;
using InvasionOfAldebaran.ViewModels;

namespace InvasionOfAldebaran.Models
{
	public class Question : NotifyPropertyChangedBase
	{
		private Random _random = new Random();

		public string Text { get; private set; }
		public ObservableCollection<Answer> Answers { get; private set; }
		public Answer CorrectAnswer { get; private set; }
		public Difficulty Difficulty { get; private set; }

		public Question(string text, List<Answer> answers, Difficulty difficulty)
		{
			this.Answers = new ObservableCollection<Answer>();
			this.Difficulty = difficulty;
			this.Text = text;

			this.HandleAnswers(answers);
		}
		public Question(string text, Answer answer1, Answer answer2, Answer answer3, Answer answer4, Difficulty difficulty)
		{
			this.Answers = new ObservableCollection<Answer>();
			this.Difficulty = difficulty;
			this.Text = text;

			var answers = new List<Answer>() {answer1, answer2, answer3, answer4};

			this.HandleAnswers(answers);
		}

		private void HandleAnswers(List<Answer> answers)
		{
			//Todo: Fragen Farben müssen besser randomisiert^^ werden. Oft immer die gleiche
			if(answers == null)
				throw new ArgumentNullException($@"The provided answerlist {answers} was null");

            var aliens = new List<string> { "alien1", "alien2", "alien3", "alien4" };			
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

				var r = _random.Next(0, 3 - i);
				answers[i].Alien = aliens[r];
				aliens.RemoveAt(r);

				this.Answers.Add(answers[i]);
			}
		}
	}
}
