using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvasionOfAldebaran.ViewModels
{
	public class AddScoreViewModel : NotifyPropertyChangedBase
	{
		private string _points;
		private string _name;

		public string Points
		{
			get { return this._points; }
			set
			{
				this._points = value;
				this.NotifyPropertyChanged(nameof(this.Points));
			}
		}
		public string Name
		{
			get { return this._name; }
			set
			{
				if(!string.IsNullOrEmpty(value))
					this._name = value;
			}
		}

		public AddScoreViewModel(int points)
		{
			this.Points = points.ToString();
		}
	}
}
