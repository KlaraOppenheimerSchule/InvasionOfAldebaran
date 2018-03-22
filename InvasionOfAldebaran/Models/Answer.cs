using System;

namespace InvasionOfAldebaran.Models
{
    public class Answer
    {
        public string Text { get; private set; }
        public bool IsCorrect { get; private set; }
        public string Alien { get; set; }

        public Uri Source
        {
            get { return new Uri(@"../../Resources/Images/" + Alien + ".png", UriKind.Relative); }
            set { this.Source = value; }
        }

        public Answer(string text, bool isCorrect)
        {
            this.Text = text;
            this.IsCorrect = isCorrect;
        }
    }
}