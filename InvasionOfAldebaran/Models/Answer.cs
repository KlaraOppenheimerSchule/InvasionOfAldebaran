using System;
using InvasionOfAldebaran.Annotations;

namespace InvasionOfAldebaran.Models
{
    public class Answer
    {
        public string Text { get; private set; }
        public bool IsCorrect { get; private set; }
        public string Alien { get; set; }

	    //[NotNull]
	    //public Uri Source
	    //{
		   // get => new Uri(@"../../Resources/Images/" + Alien + ".png", UriKind.Relative);
		   // set => this.Source = value ?? throw new ArgumentNullException(nameof(value));
	    //}

	    public Answer(string text, bool isCorrect)
        {
            this.Text = text;
            this.IsCorrect = isCorrect;
        }
    }
}