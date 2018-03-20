﻿using System;
using System.Linq;
using Caliburn.Micro;

namespace InvasionOfAldebaran.ViewModels
{
    public class FrameWindowViewModel : Conductor<Screen>.Collection.OneActive
    {
        public string Name { get; set; }
        public int Points { get; set; }

        public FrameWindowViewModel()
        {
            string[] arguments = Environment.GetCommandLineArgs();

			if (arguments.Length> 2)
            {
	            this.Name = arguments[1];

	            int.TryParse(arguments[2], out var points);
	            this.Points = points;	
            }

	        this.Items.Add(new MainMenuViewModel(this));
	        this.Items.Add(new PlayViewModel(this));
	        this.ActiveItem = this.Items.FirstOrDefault();
        }
		
        public void WindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Environment.ExitCode = this.Points;
        }

        public void PointsAchieved( int points )
        {
	        this.Points = points;
        }
    }
}
