using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
	public class Telescope : UsableItem
	{
		public Telescope(ItemWeight itemWeight) : base(itemWeight)
		{

		}

        public override void Use()
        {
            //Reveal fog around hero
            owner.RevealAdjacentFog();
        }
    }
}
