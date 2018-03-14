using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace InvasionOfAldebaran.Models
{
	public class InputHandler
	{
		private readonly Canvas _canvas;

		public bool LeftPressed { get; private set; }
		public bool RightPressed { get; private set; }
		public bool SpacePressed { get; private set; }

		public InputHandler(Canvas canvas)
		{
			_canvas = canvas;
			_canvas.PreviewKeyDown += this.OnKeyDownHandler;
			_canvas.PreviewKeyUp += this.OnKeyUpDownHandler;
		}

		private void OnKeyDownHandler(object sender, KeyEventArgs e)
		{
			switch (e.Key)
			{
				case Key.Left:
					this.LeftPressed = true;
					break;

				case Key.Right:
					this.RightPressed = true;
					break;

				case Key.Space:
					this.SpacePressed = true;
					break;
			}
		}

		private void OnKeyUpDownHandler(object sender, KeyEventArgs e)
		{
			switch (e.Key)
			{
				case Key.Left:
					this.LeftPressed = false;
					break;

				case Key.Right:
					this.RightPressed = false;
					break;

				case Key.Space:
					this.SpacePressed = false;
					break;
			}
		}
	}
}
