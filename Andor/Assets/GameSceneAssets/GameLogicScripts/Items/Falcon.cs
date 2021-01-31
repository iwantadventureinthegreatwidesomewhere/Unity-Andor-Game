using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
	public class Falcon : Item
	{
		private bool usedToday;

        public Falcon(ItemWeight itemWeight) : base(itemWeight)
		{
			usedToday = false;
		}

		public bool getUsedToday()
		{
			return usedToday;
		}

		public void setUsedToday(bool usedToday)
		{
			this.usedToday = usedToday;
		}
	}
}
