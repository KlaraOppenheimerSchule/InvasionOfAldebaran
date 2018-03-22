namespace InvasionOfAldebaran.ViewModels
{
    public interface IScreenViewModel
    {
        /// <summary>
        /// Closes the current active ViewModel in the FrameViewModel
        /// </summary>
        void CloseWindow();

        /// <summary>
        /// Changes the current active ViewModel in the FrameViewModel to another ViewModel specified within this method
        /// </summary>
        void ChangeWindow();
    }
}