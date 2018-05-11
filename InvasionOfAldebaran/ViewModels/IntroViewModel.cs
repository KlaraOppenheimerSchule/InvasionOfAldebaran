﻿using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace InvasionOfAldebaran.ViewModels
{
	class IntroViewModel : Screen
	{
		private FrameWindowViewModel _frameWindow;
		private MediaElement _mediaElement;

		public double Width { get; private set; }
		public double Height { get; private set; }
		public MediaElement MediaElement {
			get
			{
				return this._mediaElement;
			}
			private set
			{
				this._mediaElement = value;
				NotifyOfPropertyChange(nameof(MediaElement));
			}
		}

		public Uri VideoSource;

		public IntroViewModel(FrameWindowViewModel frameWindow)
		{
			this._frameWindow = frameWindow;
			this.Width = frameWindow.Width;
			this.Height = frameWindow.Height;

			this.VideoSource = new Uri(@"C:\Git\InvasionOfAldebaran\InvasionOfAldebaran\Resources\Media", UriKind.Absolute);
			this.MediaElement = new MediaElement
			{
				Source = this.VideoSource,
				Height = this.Height,
				Width = this.Width,
				LoadedBehavior = MediaState.Manual
			};

			this.MediaElement.MediaEnded += IntroEndedEventHandler;
		}

		public void StartIntro()
		{
			this.MediaElement.Play();
			this.MediaElement.Volume = 0;
		}

		private void IntroEndedEventHandler(object sender, EventArgs eventArgs)
		{
			this.MediaElement.MediaEnded -= IntroEndedEventHandler;
			this._frameWindow.ChangeScreen(typeof(MainMenuViewModel), typeof(IntroViewModel));
		}
	}
}
