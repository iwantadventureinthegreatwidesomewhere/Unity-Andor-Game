using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
	public class Witch : TileUnit, Hideable
	{
		private WitchsBrew witchsBrew;
		private bool hidden;

        public Witch(Region region) : base(region)
		{
			hidden = false;
		}

        public WitchsBrew getWitchsBrew()
		{
			return witchsBrew;
		}

        public void setWitchsBrew(WitchsBrew witchsBrew)
		{
			this.witchsBrew = witchsBrew;
		}

        public bool takeWitchsBrew(Hero hero)
		{
            if(witchsBrew != null)
			{
				hero.addItem(witchsBrew);
				witchsBrew = null;
				return true;
			}

			return false;
		}

		public void hide()
		{
			hidden = true;
		}

		public override bool isHidden()
		{
			return hidden;
		}

		public void reveal()
		{
			hidden = false;
		}
	}
}
