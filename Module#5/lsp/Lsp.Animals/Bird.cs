using Lsp.Animals.Interfaces;
using System;

namespace Lsp.Animals
{
	public abstract class Bird: IShout
	{
		public abstract void Shout();
	}
}