using System.Collections.Generic;

namespace NPuzzle.Core
{
	public class Result
	{
		public IList<Movement> Moves { get; set; }
		public IList<short> DashIndexes { get; set; }

		public Result()
		{
			Moves = new List<Movement>();
			DashIndexes = new List<short>();
		}

		public Result(IList<Movement> moves, IList<short> dashIndexes)
		{
			Moves = moves;
			DashIndexes = dashIndexes;
		}
	}
}