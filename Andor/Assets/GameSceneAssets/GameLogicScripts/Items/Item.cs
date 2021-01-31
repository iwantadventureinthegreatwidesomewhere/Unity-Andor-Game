using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
	public class Item
	{
		private ItemWeight itemWeight;
		protected Hero owner;

        public Item(ItemWeight itemWeight)
		{
			this.itemWeight = itemWeight;
		}

        public ItemWeight getItemWeight()
		{
			return itemWeight;
		}

        public void SetOwner(Hero hero)
        {
			owner = hero;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
			return obj.GetType() == this.GetType();
        }

        //TODO: override hashcode
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
