using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    public abstract class UsableItem : Item
    {
        //Some items can only be used once per turn
        public bool UsedThisTurn { get; set; }

        protected UsableItem(ItemWeight itemWeight) : base(itemWeight)
        {

        }

        public abstract void Use();
    }
}

