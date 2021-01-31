using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO:  Farmers can also be picked up by Prince Thorald

namespace Scripts
{
	public class Farmer : TileUnit
	{
		private Hero guider;

		private bool isInCastle;
		private bool isGuided;
		private bool isDead;

		public Farmer(Region region) : base(region)
		{
			this.guider = null;
			this.isGuided = false;
			this.isInCastle = false;	//TODO: Can farmer be generated in Castle?
			this.isDead = false;
		}

		public Hero getGuider() 
		{
			return guider;
		}

		public void beDropped() {
			guider = null;
			isGuided = false;
            //TODO: remove the guidership management, leave the farmer on current region
            
        }

        	public void setGuider(Hero hero)
		{
			if (isInCastle) {
                //Debug.LoginError("Cannot pick up farmer in Castle");
			}
			if (isGuided)
			{
				//Debug.LogError("This farmer has been guided.", guider);
			}else if(hero == null) {
				//Debug.LogError("Guider cannot be null.", guider);
			}else {
				this.guider = hero;
				this.isGuided = true;
			}
			//TODO: Complete guidership management--set the farmers to follow the hero (in terms of position)
		}

		public void Die()
		{
			if (isDead)
			{
				//Debug.LogError("This farmer is already dead.", farmer);
			}
			else
			{
				this.isDead = true;
				//TODO: disable buttons for getGuider(), setGuider() and die()


				//TODO: remove this farmer from his guider's (if any) list
				if (isGuided && guider != null)
				{
					//TODO: Automatically drop this farmer. => will need a removeFarmer in Hero class
					//guider.dropFarmer()
				}
			}

		}

		public bool beGuided()
        {
			return isGuided;
        }
	}
}

