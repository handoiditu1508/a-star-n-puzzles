using System.Collections.Generic;
using System.Linq;

namespace NPuzzle.Core
{
	public class State
	{
		public IList<char> Order { get; private set; }
		public short PuzzleWidth { get; private set; }
		public short DashIndex { get; private set; }
		public State LastState { get; private set; }

		public State(short puzzleWidth, IList<char> order)
		{
			this.PuzzleWidth = puzzleWidth;
			this.Order = order;
			DashIndex = (short)order.IndexOf('-');
		}

		public State(short puzzleWidth, State lastState) : this(puzzleWidth, lastState.Order.Select(c => c).ToList())
		{
			this.LastState = lastState;
		}

		private void moveDashToIndex(short newDashIndex)
		{
			Order[DashIndex] = Order[newDashIndex];
			Order[newDashIndex] = '-';
			DashIndex = newDashIndex;
		}

		public bool MoveUp()
		{
			short newDashIndex = (short)(DashIndex - PuzzleWidth);
			if (newDashIndex > -1)
			{
				moveDashToIndex(newDashIndex);
				return true;
			}
			return false;
		}

		public bool MoveRight()
		{
			short newDashIndex = (short)(DashIndex + 1);
			if (newDashIndex % PuzzleWidth != 0)
			{
				moveDashToIndex(newDashIndex);
				return true;
			}
			return false;
		}

		public bool MoveDown()
		{
			short newDashIndex = (short)(DashIndex + PuzzleWidth);
			if (newDashIndex < Order.Count)
			{
				moveDashToIndex(newDashIndex);
				return true;
			}
			return false;
		}

		public bool MoveLeft()
		{
			if (DashIndex % PuzzleWidth != 0)
			{
				moveDashToIndex((short)(DashIndex - 1));
				return true;
			}
			return false;
		}

		public bool Move(Movement movement)
		{
			switch (movement)
			{
				case Movement.Up:
					return MoveUp();
				case Movement.Right:
					return MoveRight();
				case Movement.Down:
					return MoveDown();
				case Movement.Left:
					return MoveLeft();
				default:
					return false;
			}
		}

		public override bool Equals(object obj)
		{
			if ((obj == null) || !this.GetType().Equals(obj.GetType()))
			{
				return false;
			}
			else
			{
				State state = (State)obj;
				if (Order.Count != state.Order.Count)
					return false;
				for (short i = 0; i < Order.Count; i++)
				{
					if (Order[i] != state.Order[i])
						return false;
				}
				return true;
			}
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public override string ToString()
		{
			return base.ToString();
		}
	}
}