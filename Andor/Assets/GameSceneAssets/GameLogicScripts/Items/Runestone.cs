using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
	public class Runestone : Item
	{
		private GemColor color;

		public Runestone(ItemWeight itemWeight, GemColor color) : base(itemWeight)
		{
			this.color = color;
		}

        public GemColor getColor()
		{
			return color;
		}
	}
}
