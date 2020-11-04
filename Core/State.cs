using System;
using System.Collections.Generic;
using System.Linq;

namespace NPuzzle.Core
{
	public class State
	{
		public IList<char> Order { get; private set; }
		private int hashCode;
		public IDictionary<char, short> OrderIndex { get; private set; }
		public short PuzzleWidth { get; private set; }
		public short DashIndex { get; private set; }

		public State(short puzzleWidth, IList<char> order)
		{
			PuzzleWidth = puzzleWidth;
			Order = order;
			OrderIndex = new Dictionary<char, short>();
			for (short i = 0; i < Order.Count; i++)
			{
				OrderIndex[Order[i]] = i;
			}
			DashIndex = (short)order.IndexOf('-');
			hashCode = new string(Order.ToArray()).GetHashCode();
		}

		/*private void moveDashToIndex(short newDashIndex)
		{
			OrderIndex['-'] = newDashIndex;
			OrderIndex[Order[newDashIndex]] = DashIndex;

			Order[DashIndex] = Order[newDashIndex];
			Order[newDashIndex] = '-';
			DashIndex = newDashIndex;
			hashCode = new string(Order.ToArray()).GetHashCode();
		}*/

		public State MoveUp()
		{
			short newDashIndex = (short)(DashIndex - PuzzleWidth);
			if (newDashIndex > -1)
			{
				IList<char> newOrder = Order.Select(c => c).ToList();
				newOrder[DashIndex] = newOrder[newDashIndex];
				newOrder[newDashIndex] = '-';
				return new State(PuzzleWidth, newOrder);
			}
			return null;
		}

		public State MoveRight()
		{
			short newDashIndex = (short)(DashIndex + 1);
			if (newDashIndex % PuzzleWidth != 0)
			{
				IList<char> newOrder = Order.Select(c => c).ToList();
				newOrder[DashIndex] = newOrder[newDashIndex];
				newOrder[newDashIndex] = '-';
				return new State(PuzzleWidth, newOrder);
			}
			return null;
		}

		public State MoveDown()
		{
			short newDashIndex = (short)(DashIndex + PuzzleWidth);
			if (newDashIndex < Order.Count)
			{
				IList<char> newOrder = Order.Select(c => c).ToList();
				newOrder[DashIndex] = newOrder[newDashIndex];
				newOrder[newDashIndex] = '-';
				return new State(PuzzleWidth, newOrder);
			}
			return null;
		}

		public State MoveLeft()
		{
			if (DashIndex % PuzzleWidth != 0)
			{
				short newDashIndex = (short)(DashIndex - 1);
				IList<char> newOrder = Order.Select(c => c).ToList();
				newOrder[DashIndex] = newOrder[newDashIndex];
				newOrder[newDashIndex] = '-';
				return new State(PuzzleWidth, newOrder);
			}
			return null;
		}

		public State Move(Movement movement)
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
					return null;
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
				/*State state = (State)obj;
				if (Order.Count != state.Order.Count)
					return false;
				for (short i = 0; i < Order.Count; i++)
				{
					if (Order[i] != state.Order[i])
						return false;
				}
				return true;*/
				return GetHashCode() == obj.GetHashCode();
			}
		}

		public override int GetHashCode() => hashCode;

		public override string ToString() => base.ToString();

		public short getManhattanDistance(char c, short destinationIndex)
		{
			short row1 = (short)(OrderIndex[c] / PuzzleWidth);
			short col1 = (short)(OrderIndex[c] % PuzzleWidth);
			short row2 = (short)(destinationIndex / PuzzleWidth);
			short col2 = (short)(destinationIndex % PuzzleWidth);
			return (short)(Math.Abs(row1 - row2) + Math.Abs(col1 - col2));
		}
	}
}