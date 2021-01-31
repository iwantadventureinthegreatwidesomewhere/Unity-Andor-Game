using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
	public class Game
	{
		private Legend legend;
		private GameDifficulty difficulty;
		private List<Player> players;
		private Dictionary<Player, Hero> heroes;
		private List<Region> regions;
		private bool joinable;

		public Game(Legend legend)
		{
			this.legend = legend;
			players = new List<Player>();
			heroes = new Dictionary<Player, Hero>();
			regions = new List<Region>();
			joinable = true;
		}

        public Legend getLegend()
		{
			return legend;
		}

		public void selectDifficulty(GameDifficulty difficulty)
        {
			this.difficulty = difficulty;
        }

        public bool chooseHero(Player player, Hero hero)
		{
			if (heroes.ContainsKey(player))
			{
				heroes.Remove(player);
			}

			foreach (KeyValuePair<Player, Hero> entry in heroes)
			{
				if (entry.Value.getHeroKind() == hero.getHeroKind())
				{
					return false;
				}
			}

			return true;
		}

        public Region getRegionByRank(int rank)
		{
            foreach(Region region in regions)
			{
                if(region.getRank() == rank)
				{
					return region;
				}
			}

			return null;
		}

		public bool isJoinable()
		{
			return joinable;
		}

		public bool join(Player player)
		{
			if (player != null && !players.Contains(player) && players.Count <= 4)
			{
				players.Add(player);
				return true;
			}

			return false;
		}


		public void initializeLegend()
		{
			//create all 84 regions and add to regions
			for (int i = 0; i <= 84; i++)
			{
				Region newRegion = new Region(i);
				newRegion.addTileUnit(new TileUnit(newRegion));
				regions.Add(newRegion);
			}

			//manually set neighborRegions for all 84 regions
			//0 castle
			regions[0].addTileUnit(new Castle(regions[0], players.Count));

			// place well on 5 35 45 55
			regions[5].addTileUnit(new Well(regions[5]));
			regions[35].addTileUnit(new Well(regions[35]));
			regions[45].addTileUnit(new Well(regions[45]));
			regions[55].addTileUnit(new Well(regions[55]));

			// place merchant on 18 57 71
			regions[18].addTileUnit(new Merchant(regions[18]));
			regions[57].addTileUnit(new Merchant(regions[57]));
			regions[71].addTileUnit(new Merchant(regions[71]));

			//17 have two gold
			regions[17].setGold(2); // two gold

			//20 one gold
			regions[20].setGold(1); // one gold

			// place gor on 8 20 21 26 48
			regions[8].addTileUnit(new Monster(regions[8], MonsterKind.Gor, false));
			regions[20].addTileUnit(new Monster(regions[20], MonsterKind.Gor, false));
			regions[21].addTileUnit(new Monster(regions[21], MonsterKind.Gor, false));
			regions[26].addTileUnit(new Monster(regions[26], MonsterKind.Gor, false));
			regions[48].addTileUnit(new Monster(regions[48], MonsterKind.Gor, false));

			// place skral on 19
			regions[19].addTileUnit(new Monster(regions[19], MonsterKind.Skral, false));

			// place farmer on 24 and 36 if Easy mode, else, only on 24
			regions[24].addTileUnit(new Farmer(regions[24]));
			if (difficulty == GameDifficulty.Easy)
				regions[36].addTileUnit(new Farmer(regions[36]));

			// place fog on 8 11 12 13 16 32 42 44 46 47 48 49 56 63 64
			List<Fog> fogTokens = new List<Fog>();		// this are all 15 fogTokens
			// 5 event fog tokens
			for (int i = 0; i < 5; i++)
			{
				Fog fog = new Fog(regions[0], FogKind.Event); //region[0] as placehoder for initialization, has changed below
				fog.setHiddenObject(new Event(EventKind.EventNull));
				fog.getHiddenObject().hide();
				fogTokens.Add(fog);
			}
			// one SP fog token
			Fog SPfog = new Fog(regions[0], FogKind.SP);
			fogTokens.Add(SPfog);
			// one WP+2 fog token
			Fog WPfog2 = new Fog(regions[0], FogKind.TwoWP);
			fogTokens.Add(WPfog2);
			// one WP+3 fog token
			Fog WPfog3 = new Fog(regions[0], FogKind.ThreeWP);
			fogTokens.Add(WPfog3);
			// 3 one gold fog token
			for (int i = 0; i < 3; i++)
			{
				Fog goldFog = new Fog(regions[0], FogKind.Gold);
				fogTokens.Add(goldFog);
			}
			// one wineskin
			Fog wineSkinFog = new Fog(regions[0], FogKind.Wineskin);
			wineSkinFog.setHiddenObject(new Wineskin(ItemWeight.Light));
			wineSkinFog.getHiddenObject().hide();
			fogTokens.Add(wineSkinFog);

			// one witch
			Fog witchFog = new Fog(regions[0], FogKind.Witch);
			witchFog.setHiddenObject(new Witch(regions[0]));//need to reset the region for Witch
			fogTokens.Add(witchFog);       

			// two gor
			for (int i = 0; i < 2; i++)
			{
				Fog gorFog = new Fog(regions[0], FogKind.Monster);
				gorFog.setHiddenObject(new Monster(regions[0], MonsterKind.Gor, false)); // need to reset region for Gor
				fogTokens.Add(gorFog);
			}
			// shuffle all fog tokens
			// can not sync so make the "random" list manually
			/*
			for (int i = 0; i < fogTokens.Count; i++)
			{
				Fog temp = fogTokens[i];
				int randomIndex = Random.Range(i, fogTokens.Count);
				fogTokens[i] = fogTokens[randomIndex];
				fogTokens[randomIndex] = temp;
			}*/
			// shuffle manually
			fogTokens.Add(fogTokens[3]);
			fogTokens.RemoveAt(3);
			fogTokens.Add(fogTokens[6]);
			fogTokens.RemoveAt(6);
			fogTokens.Add(fogTokens[2]);
			fogTokens.RemoveAt(2);
			fogTokens.Add(fogTokens[14]);
			fogTokens.RemoveAt(14);
			fogTokens.Add(fogTokens[8]);
			fogTokens.RemoveAt(8);
			fogTokens.Add(fogTokens[10]);
			fogTokens.RemoveAt(10);


			// assign
			List<Region> fogRegions = new List<Region>();		//below are all fogged regions
			fogRegions.Add(regions[8]);
			fogRegions.Add(regions[11]);
			fogRegions.Add(regions[12]);
			fogRegions.Add(regions[13]);
			fogRegions.Add(regions[16]);
			fogRegions.Add(regions[32]);
			fogRegions.Add(regions[42]);
			fogRegions.Add(regions[44]);
			fogRegions.Add(regions[46]);
			fogRegions.Add(regions[47]);
			fogRegions.Add(regions[48]);
			fogRegions.Add(regions[49]);
			fogRegions.Add(regions[56]);
			fogRegions.Add(regions[63]);
			fogRegions.Add(regions[64]);

			for(int i = 0; i < 15; i++)
            {
				fogRegions[i].addTileUnit(fogTokens[i]);
				fogTokens[i].setRegion(fogRegions[i]);
                if (fogTokens[i].getFogKind() == FogKind.Monster)
                {
					((Combatant)fogTokens[i].getHiddenObject()).setRegion(fogRegions[i]);
					fogTokens[i].getHiddenObject().hide();
				}else if(fogTokens[i].getFogKind() == FogKind.Witch)
                {
					((TileUnit)fogTokens[i].getHiddenObject()).setRegion(fogRegions[i]);
					fogTokens[i].getHiddenObject().hide();
				}

			}

			// finish creating all regions
			// setup hero
			foreach (KeyValuePair<Player, Hero> entry in heroes)
            {
				entry.Value.setupHero();
            }

		}

		public Dictionary<Player, Hero> getHeroes(){
			return heroes;
		}

		public GameDifficulty getDifficulty()
        {
			return difficulty;
        }
	}
}
