using System;

namespace InvasionOfAldebaran.Models
{
	[Serializable]
	public class SerializableScore
	{
		private string _name;
		private int _points;
		private int _listPosition;

		public string Name
		{
			get { return _name; }
			set
			{
				_name = value;
			}
		}
		public int Points
		{
			get { return _points; }
			set
			{
				_points = value;
			}
		}
		public int ListPosition
		{
			get { return _listPosition; }
			set
			{
				_listPosition = value;
			}
		}
		
		public SerializableScore() { }
	}
}