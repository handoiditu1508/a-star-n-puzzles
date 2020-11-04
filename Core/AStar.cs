using System.Collections.Generic;
using System.Linq;

namespace NPuzzle.Core
{
	public class AStar
	{
		public short N { get; private set; }
		public State Begin { get; private set; }
		public State Goal { get; private set; }
		private IDictionary<State, short> g;
		private IDictionary<State, short> h;
		private IDictionary<State, short> f;
		private IDictionary<State, State> open;
		private IDictionary<State, State> prev;
		private IDictionary<State, Movement> move;

		public AStar(short n, State begin, State goal)
		{
			N = n;
			Begin = begin;
			Goal = goal;
			g = new Dictionary<State, short>();
			g[Begin] = 0;
			h = new Dictionary<State, short>();
			h[Begin] = calH(Begin);
			f = new Dictionary<State, short>();
			f[Begin] = (short)(g[Begin] + h[Begin]);
			open = new Dictionary<State, State>();
			prev = new Dictionary<State, State>();
			move = new Dictionary<State, Movement>();
		}

		public Result Solve()
		{
			Result result = new Result();

			open.Add(Begin, Begin);
			while (open.Any())
			{
				State bestState = getBestState();
				open.Remove(bestState);

				if (bestState.Equals(Goal))
				{
					while (prev.TryGetValue(bestState, out State prevState))
					{
						result.Moves.Insert(0, move[bestState]);
						result.DashIndexes.Insert(0, bestState.DashIndex);
						bestState = prevState;
					}
					result.DashIndexes.Insert(0, bestState.DashIndex);
					break;
				}

				State newState;
				short newGValue = (short)(g[bestState] + 1);
				short oldGValue;

				#region move up
				newState = bestState.MoveUp();
				if (newState != null)
				{
					if (g.TryGetValue(newState, out oldGValue))
					{
						if (newGValue < oldGValue)
						{
							g[newState] = newGValue;
							prev[newState] = bestState;
							open[newState] = newState;
							move[newState] = Movement.Up;
							f[newState] = (short)(newGValue + h[newState]);
						}
					}
					else
					{
						g[newState] = newGValue;
						prev[newState] = bestState;
						open[newState] = newState;
						move[newState] = Movement.Up;
						h[newState] = calH(newState);
						f[newState] = (short)(newGValue + h[newState]);
					}
				}
				#endregion

				#region move right
				newState = bestState.MoveRight();
				if (newState != null)
				{
					if (g.TryGetValue(newState, out oldGValue))
					{
						if (newGValue < oldGValue)
						{
							g[newState] = newGValue;
							prev[newState] = bestState;
							open[newState] = newState;
							move[newState] = Movement.Right;
							f[newState] = (short)(newGValue + h[newState]);
						}
					}
					else
					{
						g[newState] = newGValue;
						prev[newState] = bestState;
						open[newState] = newState;
						move[newState] = Movement.Right;
						h[newState] = calH(newState);
						f[newState] = (short)(newGValue + h[newState]);
					}
				}
				#endregion

				#region move down
				newState = bestState.MoveDown();
				if (newState != null)
				{
					if (g.TryGetValue(newState, out oldGValue))
					{
						if (newGValue < oldGValue)
						{
							g[newState] = newGValue;
							prev[newState] = bestState;
							open[newState] = newState;
							move[newState] = Movement.Down;
							f[newState] = (short)(newGValue + h[newState]);
						}
					}
					else
					{
						g[newState] = newGValue;
						prev[newState] = bestState;
						open[newState] = newState;
						move[newState] = Movement.Down;
						h[newState] = calH(newState);
						f[newState] = (short)(newGValue + h[newState]);
					}
				}
				#endregion

				#region move left
				newState = bestState.MoveLeft();
				if (newState != null)
				{
					if (g.TryGetValue(newState, out oldGValue))
					{
						if (newGValue < oldGValue)
						{
							g[newState] = newGValue;
							prev[newState] = bestState;
							open[newState] = newState;
							move[newState] = Movement.Left;
							f[newState] = (short)(newGValue + h[newState]);
						}
					}
					else
					{
						g[newState] = newGValue;
						prev[newState] = bestState;
						open[newState] = newState;
						move[newState] = Movement.Left;
						h[newState] = calH(newState);
						f[newState] = (short)(newGValue + h[newState]);
					}
				}
				#endregion
			}
			return result;
		}

		private State getBestState()
		{
			if (open.Any())
			{
				State best = open.First().Key;
				short bestF = f[best];
				foreach (KeyValuePair<State, State> pair in open)
				{
					short newBestF = f[pair.Value];
					if (newBestF < bestF)
					{
						bestF = newBestF;
						best = pair.Key;
					}
				}
				return best;
			}
			return null;
		}

		private short calH(State state)
		{
			short result = 0;
			foreach (KeyValuePair<char, short> pair in state.OrderIndex)
			{
				result += state.getManhattanDistance(pair.Key, Goal.OrderIndex[pair.Key]);
			}
			return result;
		}
	}
}