using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
	public class Castle : TileUnit
	{
		private int numGoldenShields;

        public Castle(Region region, int numHeroes) : base(region)
		{

			if(numHeroes == 1){
				numGoldenShields = 5;
			}
			else if(numHeroes == 2)
			{
				numGoldenShields = 3;
			}
            else if(numHeroes == 3)
			{
				numGoldenShields = 2;
			}
            else if(numHeroes == 4)
			{
				numGoldenShields = 1;
			}
		}

        public int getNumGoldenShields()
		{
			return numGoldenShields;
		}

        public bool removeGoldenShield()
		{
            if(numGoldenShields > 0)
			{
				numGoldenShields--;
				return true;
			}

			return false;
		}

		public void addGoldenShield(){
			numGoldenShields++;
		}
	}
}
