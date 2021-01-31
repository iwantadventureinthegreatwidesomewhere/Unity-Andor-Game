using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

namespace Scripts
{
	public class Fog : TileUnit
	{
		// fog seems to need no hidden attribute since only the objects behind it can be hidden or not
		// change it to revealed
		//private bool hidden;
		private bool revealed;

		//one fog can hide one thing so list is not necessary
		///private List<Hideable> hiddenObjects;
		private Hideable hiddenObject;
		private FogKind fogKind;

        public Fog(Region region, FogKind fogKind) : base(region)
		{
			revealed = false;
			this.fogKind = fogKind;
			hiddenObject=null;
		}

		/* change to set Hiddenobject
        public bool addHiddenObject(Hideable hideable)
		{
			if (!hiddenObjects.Contains(hideable))
			{
				hiddenObjects.Add(hideable);
				return true;
			}

			return false;
		}
		*/

		public void setHiddenObject(Hideable hideable)
		{
			this.hiddenObject = hideable;
		}

		public Hideable getHiddenObject()
        {
			return this.hiddenObject;
        }

		// I don't think fog needs a attribute hidden since only the object behind it can be hidden or not
		// comment it for now
		/*
		public override bool isHidden()
		{
			return hidden;
		}
		*/
		public bool isRevealed()
        {
			return revealed;
        }

		/* change to only reveal one
        public void reveal()
		{
			hidden = false;

            foreach(Hideable hideable in hiddenObjects)
			{
				hideable.reveal();
			}
		}
		*/

		public bool reveal()
		{
			if (revealed == false)
            {
				Debug.Log(getFogKind());
				if (this.hiddenObject != null)
					this.hiddenObject.reveal();
				// if not using telescope, then only reveal
				// if using telescope, reveal all adjacent regions, not activate them
				if (this.fogKind == FogKind.Monster)
				{
					Monster gor = (Monster)this.hiddenObject;
					region.addTileUnit(gor);
					gor.setRegion(region);
				}
				if (this.fogKind == FogKind.Witch)
				{
					Witch witch = (Witch)this.hiddenObject;
					region.addTileUnit(witch);
					witch.setRegion(region);
				}
                if (this.fogKind == FogKind.Event)
                {
					Event e = (Event)getHiddenObject();
					e.setEventKind((GameObject.Find("GameManager").GetComponent<GameManager>().getEventCards())[0].getEventKind());
					//Debug.Log((GameObject.Find("GameManager").GetComponent<GameManager>().getEventCards())[0].getEventKind());
					GameObject.Find("GameManager").GetComponent<GameManager>().getEventCards().RemoveAt(0);
					//Debug.Log((GameObject.Find("GameManager").GetComponent<GameManager>().getEventCards())[0].getEventKind());
				}
				revealed = true;
				GameObject.Find("GameManager").GetComponent<GameManager>().updateFog();
				return true;
			}
			return false;		// the fog has already been revealed
		}

		public bool activate(Hero hero)
        {
			//need to reveal first;
			if (this.revealed != true)
				reveal();
			string description = "";
            if (this.fogKind != FogKind.None)
            {
				if(this.fogKind == FogKind.TwoWP)
                {
					hero.setWillpowerPoints(hero.getWillpowerPoints() + 2);
				}
				if (this.fogKind == FogKind.Witch)
				{
					int roll = Random.Range(1, 7);
					Debug.Log(roll);
					int rank = -1;
					Region r = null; 
                    if (roll == 1 || roll == 2)
                    {
						r = GameObject.Find("R (37)").GetComponent<Node>().GetRegion();
						rank = 37;
						Vector3 position = new Vector3(-3.72f, 0.0f, -4.35f);
						GameObject.Find("GameManager").GetComponent<GameManager>().updateHerb(true,position);
					}
					if (roll == 3 || roll == 4)
					{
						r = GameObject.Find("R (67)").GetComponent<Node>().GetRegion();
						rank = 67;
						Vector3 position = new Vector3(5.17f, 0.0f, -2.02f);
						GameObject.Find("GameManager").GetComponent<GameManager>().updateHerb(true, position);
					}
					if (roll == 5 || roll == 6)
					{
						r = GameObject.Find("R (61)").GetComponent<Node>().GetRegion();
						rank = 61;
						Vector3 position = new Vector3(6.24f, 0.0f, 1.18f);
						GameObject.Find("GameManager").GetComponent<GameManager>().updateHerb(true, position);
					}
					r.setHerb(true);
					string show = revealedFogDescription();
					string show2 = show + "\n\nHerb is on space " + rank;
					GameObject FogInfo = GameObject.Find("FogInfo").transform.GetChild(0).gameObject;
					GameObject DUI = null;
					for (int i = 0; i < FogInfo.transform.childCount - 1; i++)
					{
						if (FogInfo.transform.GetChild(i).transform.name == "Description")
						{
							DUI = FogInfo.transform.GetChild(i).gameObject;
						}
					}
					DUI.GetComponent<Text>().text = show2;
					FogInfo.SetActive(true);
					GameObject.Find("GameManager").GetComponent<GameManager>().witchFound = true;
				}
				if (this.fogKind == FogKind.ThreeWP)
                {
					hero.setWillpowerPoints(hero.getWillpowerPoints() + 3);
				}
				if (this.fogKind == FogKind.SP)
                {
					hero.setStrengthPoints(hero.getStrengthPoints() + 1);
				}
				if (this.fogKind == FogKind.Gold)
                {
					hero.setGold(hero.getGold() + 1);
				}
				if (this.fogKind == FogKind.Wineskin)
                {
					Wineskin wineskin = (Wineskin)this.hiddenObject;
					hero.addItem(wineskin);
					Debug.Log("wineskin added");
				}
				if (this.fogKind == FogKind.Event)
				{
					((Event)getHiddenObject()).applyEventEffect();
				}
				if (this.fogKind != FogKind.Event&&this.fogKind!=FogKind.Witch)
				{
					// method below return  a string of the description
					// event has its own description, so first check whether what hidden is event or not
					description = revealedFogDescription();
					GameObject FogInfo = GameObject.Find("FogInfo").transform.GetChild(0).gameObject;
					GameObject DUI = null;
					for (int i = 0; i < FogInfo.transform.childCount; i++)
					{
						if (FogInfo.transform.GetChild(i).transform.name == "Description")
						{
							DUI = FogInfo.transform.GetChild(i).gameObject;
						}
					}
					DUI.GetComponent<Text>().text = description;
					FogInfo.SetActive(true);
				}
				// after the fog effect, set fogkind to None
				this.fogKind = FogKind.None;
				GameObject.Find("GameManager").GetComponent<GameManager>().updateFog();
				return true;
			}
			return false;		// the fog has already been activated
        }

		public string revealedFogDescription()
        {
			string description=null;
			if (this.fogKind == FogKind.Monster)
			{
				description="A gor is behind the fog";
			}
			if (this.fogKind == FogKind.TwoWP)
			{
				description="Two wellpower points are gained!";
			}
			if (this.fogKind == FogKind.ThreeWP)
			{
				description="Three wellpower points are gained!";
			}
			if (this.fogKind == FogKind.SP)
			{
				description="One strengthpower point is gained!";
			}
			if (this.fogKind == FogKind.Gold)
			{
				description="One gold is gained!";
			}
			if (this.fogKind == FogKind.Witch)
			{
				description="Finally! There in the fog, one of the heroes discovers the witch named Reka.\n\nTask: When the Narrator reaches the letter 'N' on the legend track, the medicinal herb must be in space 0.";
			}
			if (this.fogKind == FogKind.Wineskin)
			{
				description="You got one wineskin！";
			}
			if (this.fogKind == FogKind.Event)
			{
				// The event has it own description
			}
			return description;
		}

		public FogKind getFogKind()
        {
			return this.fogKind;
        }

		public void setFogKind(FogKind fk)
		{
			this.fogKind = fk;
		}
	}
}
