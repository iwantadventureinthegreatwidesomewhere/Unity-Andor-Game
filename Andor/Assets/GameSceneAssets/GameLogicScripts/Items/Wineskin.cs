using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
	public class Wineskin : UsableItem, Hideable
	{
		private ItemFullness itemFullness;
		private bool hidden;

        public Wineskin(ItemWeight itemWeight) : base(itemWeight)
		{
			itemFullness = ItemFullness.Full;
			hidden = false;
		}

        public override void Use()
        {
			owner.FreeMoveSpaces = 1;
			if(itemFullness == ItemFullness.Full)
            {
				itemFullness = ItemFullness.Half;
				owner.UpdateItemState(this);
            }
            else
            {
				owner.DiscardItem(this);
            }
        }

        public override bool Equals(object obj)
        {
			if (obj.GetType() == null)
				return false;
			if (obj.GetType() != this.GetType())
				return false;
			return ((Wineskin)obj).itemFullness == itemFullness;
        }

        //TODO: override hashcode
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public ItemFullness getItemFullness()
		{
			return itemFullness;
		}

        public void setItemFullness(ItemFullness itemFullness)
		{
			this.itemFullness = itemFullness;
		}

        public void hide()
		{
			hidden = true;
		}

        public bool isHidden()
		{
			return hidden;
		}

        public void reveal()
		{
			hidden = false;
		}
	}
}
