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

            canvas.PreviewKeyDown += this.OnKeyDownHandler;
            canvas.PreviewKeyUp += this.OnKeyUpDownHandler;
        }

        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Left)
			{
				LeftPressed = true;
				RightPressed = false;
				e.Handled = true;
			}		
			if(e.Key == Key.Right)
			{
				RightPressed = true;
				LeftPressed = false;
				e.Handled = true;
			}
			if(e.Key == Key.Space)
			{
				SpacePressed = true;
				e.Handled = true;
			}
			if (e.Key == Key.Escape)
			{
				EscapePressed = true;
				e.Handled = true;
			}
		}

        private void OnKeyUpDownHandler(object sender, KeyEventArgs e)
        {
			if (e.Key == Key.Left)
			{
				LeftPressed = false;
				e.Handled = true;
			}
			if (e.Key == Key.Right)
			{
				RightPressed = false;
				e.Handled = true;
			}
			if (e.Key == Key.Space)
			{
				SpacePressed = false;
				e.Handled = true;
			}
			if (e.Key == Key.Escape)
			{
				EscapePressed = false;
				e.Handled = true;
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