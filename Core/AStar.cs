using System.Collections.Generic;
using System.Linq;

namespace NPuzzle.Core
{
	public class AStar
	{
		public short N { get; private set; }
		public State Begin { get; private set; }
		public State Goal { get; private set; }
		private IList<short> g;
		private IList<State> open;
		public IList<Movement> Moves { get; private set; }

		public AStar(short n, State begin, State goal)
		{
			N = n;
			Begin = begin;
			Goal = goal;
			g = new List<short>(Enumerable.Repeat((short)-1, N));
			open = new List<State>();
			Moves = new List<Movement>();
		}

		public IList<Movement> Solve()
		{
			addToOpen(Begin);
			while (open.Any())
			{
				
			}
			return Moves;
		}

		private short addToOpen(State state)
		{
			short resultIndex = 0;
			for (; resultIndex < open.Count; resultIndex++)
			{
				if (f(state) < f(open[resultIndex]))
				{
					open.Insert(resultIndex, state);
					break;
				}
			}
			return resultIndex;
		}

		private short f(State state)
		{
			return 0;
		}
	}
}