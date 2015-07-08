using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronosEngine;
using Tutorials.FirstTutorial;
using Tutorials.SecondTutorial;

namespace Tutorials {
	class Program {
		static List<ChronoGame> examples = new List<ChronoGame>();
		static void Main(string[] args) {
			examples.Add(new Tutorial1());
			examples.Add(new Tutorial2());
			Console.WriteLine("Please enter a number from 1 to " + examples.Count + " to run that example.");
			while (true) {
				Console.WriteLine("Please enter a tutorial number.");
				Console.Write(">");
				string choice = Console.ReadLine();
				int example = 0;
				if (!int.TryParse(choice, out example))
					continue;
				if (example > examples.Count || example <= 0)
					continue;

				examples[example - 1].Run();
			}
		}
	}
}
