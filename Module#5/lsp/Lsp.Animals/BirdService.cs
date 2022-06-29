using System.Collections.Generic;

namespace Lsp.Animals
{
	public class BirdService
	{
		public void MigrateToSouth(IEnumerable<IFlyable> birds)
		{
			foreach (var bird in birds)
			{
				bird.Fly();
			}
		}

		public void Shout(IEnumerable<Bird> birds)
		{
			foreach (var bird in birds)
			{
				bird.Shout();
			}
		}
	}
}