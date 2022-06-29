using System;

namespace Lsp.Animals.ConcreteBirds
{
	public class Crow : Bird, IFlyable
	{
		public void Fly()
		{
			Console.WriteLine("I am crow and I fly");
		}

		public override void Shout()
		{
			Console.WriteLine("karrr!!!");
		}
	}
}