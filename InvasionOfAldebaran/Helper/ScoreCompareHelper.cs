using InvasionOfAldebaran.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvasionOfAldebaran.Helper
{
	class ScoreCompareHelper : IComparer<Score>
	{
		public int Compare(Score x, Score y)
		{
			Score xScore = (Score)x;
			Score yScore = (Score)y;

			if (xScore.Points > yScore.Points)
				return 1;
			if (xScore.Points < yScore.Points)
				return -1;
			else
				return 0;
		}
	}
}
