using System;
using System.Collections.Generic;
using System.IO;
using NPuzzle.Core;

namespace NPuzzle
{
	class Program
	{
		static void Main(string[] args)
		{
			//string filePath = @"..\..\..\input\";//visual studio
			string filePath = @".\input\";//visual studio code
			string fileName = "test1.txt";

			short n;
			State begin, goal;

			#region readfile and process data
			using (StreamReader file = new StreamReader(Path.Combine(filePath, fileName)))
			{
				n = short.Parse(file.ReadLine());
				IList<char> beginOrder = new List<char>();
				IList<char> goalOrder = new List<char>();
				for (short i = 0; i < n; i++)
				{
					string row = file.ReadLine();
					foreach (string s in row.Split(' '))
					{
						beginOrder.Add(char.Parse(s));
					}
				}
				begin = new State(n, beginOrder);

				for (short i = 0; i < n; i++)
				{
					string row = file.ReadLine();
					foreach (string s in row.Split(' '))
					{
						goalOrder.Add(char.Parse(s));
					}
				}
				goal = new State(n, goalOrder);
			}
			#endregion

			AStar aStar = new AStar(n, begin, goal);
			Result result = aStar.Solve();

			string temp = "";
			foreach (short dashIndex in result.DashIndexes)
			{
				temp += $"({dashIndex / n},{dashIndex % n}) -> ";
			}
			temp = temp.Substring(0, temp.Length - 4);
			Console.WriteLine(temp);

			temp = "";
			foreach (Movement move in result.Moves)
			{
				Console.Write(move.ToString() + " ");
				temp += $"{move.ToString()} ";
			}
			temp = temp.Substring(0, temp.Length - 1);
			Console.WriteLine(temp);
		}
	}
}
