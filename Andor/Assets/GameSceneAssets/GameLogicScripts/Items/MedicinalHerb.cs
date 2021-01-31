using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
	public class MedicinalHerb : UsableItem
	{
		HerbButton move;
		HerbButton heal;
		HerbButton str;
		private int strength;

        public MedicinalHerb(ItemWeight itemWeight, int strength) : base(itemWeight)
		{
			this.strength = strength;
			Transform herbButtons = GameObject.Find("HerbButtons").transform;
			move = herbButtons.Find("HerbMoveBtn").GetComponent<HerbButton>();
			heal = herbButtons.Find("HerbHealBtn").GetComponent<HerbButton>();
			str = herbButtons.Find("HerbStrBtn").GetComponent<HerbButton>();
			move.SetActive(false);
			heal.SetActive(false);
			str.SetActive(false);
		}
        //When icon pressed, reveal/hide herb buttons
		public override void Use()
		{
			move.ToggleActive();
			heal.ToggleActive();
			str.ToggleActive();
			move.Herb = this;
			heal.Herb = this;
			str.Herb = this;
		}

        public void NotifyPressed()
        {
			if (move.Pressed)
			{
				move.Pressed = false;
				owner.FreeMoveSpaces = strength;
			}
			else if (heal.Pressed)
			{
				heal.Pressed = false;
				owner.Heal(strength);
			}
			else if (str.Pressed)
			{
				str.Pressed = false;
				owner.StrengthBoost += strength;
			}
            move.SetActive(false);
			heal.SetActive(false);
			str.SetActive(false);
			owner.DiscardItem(this);
		}

		public override bool Equals(object obj)
		{
			if (obj.GetType() == null)
				return false;
			if (obj.GetType() != this.GetType())
				return false;
			return ((MedicinalHerb)obj).strength == strength;
		}

		//TODO: override hashcode
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public int getStrength()
		{
			return strength;
		}
	}
}
