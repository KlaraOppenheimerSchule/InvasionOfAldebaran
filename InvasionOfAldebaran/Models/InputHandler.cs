using InvasionOfAldebaran.Helper;
using System.Windows.Controls;
using System.Windows.Input;

namespace InvasionOfAldebaran.Models
{
	public class InputHandler
    {
		private Player _playerInstance;
		private SpawnHandler _spawnerInstance;

        public bool LeftPressed;
        public bool RightPressed;
        public bool SpacePressed;
	    public bool EscapePressed;

		public delegate void EscapeKeyEventHandler();
		public event EscapeKeyEventHandler EscapeKeyPressed;

        public InputHandler(Canvas canvas, Player player, SpawnHandler spawner )
        {
			this._playerInstance = player;
			this._spawnerInstance = spawner;

            canvas.KeyDown += this.OnKeyDownHandler;
            canvas.KeyUp += this.OnKeyUpDownHandler;
        }

        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Left:
					//if(e.KeyStates == KeyStates.Down)
					//{
						LeftPressed = true;
						RightPressed = false;
					//}	
					//else
					//	LeftPressed = false;
                    break;

                case Key.Right:
					//if (e.KeyStates == KeyStates.Down)
					//{
						RightPressed = true;
						LeftPressed = false;
					//}
					//else
					//	RightPressed = true;
                    break;

                case Key.Space:
                    SpacePressed = true;
                    break;

				case Key.Escape:
					EscapePressed = true;
					break;
				
				case (default):
					break;
            }
        }

        private void OnKeyUpDownHandler(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
				case Key.Left:
					LeftPressed = false;
					break;

				case Key.Right:
					RightPressed = false;
					break;

				case Key.Space:
                    SpacePressed = false;
                    break;

				case Key.Escape:
					EscapePressed = false;
					break;

				case (default):
					break;
			}
        }
		public void ApplyInput()
		{
			if (this.LeftPressed)
				this._playerInstance.Move(Direction.Left);

			if (this.RightPressed)
				this._playerInstance.Move(Direction.Right);

			if (this.SpacePressed)
				this._spawnerInstance.SpawnMissile(this._playerInstance);

			if (this.EscapePressed)
				this.EscapeKeyPressed?.Invoke();
			
		}
    }
}