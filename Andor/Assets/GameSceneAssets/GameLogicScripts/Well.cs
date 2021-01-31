using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
	public class Well : TileUnit
	{
		private bool usedToday;

        public Well(Region region) : base(region)
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
