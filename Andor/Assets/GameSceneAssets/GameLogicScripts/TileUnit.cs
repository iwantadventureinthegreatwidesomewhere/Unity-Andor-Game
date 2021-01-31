using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
	public class TileUnit
	{
		protected Region region;

        public TileUnit()
        {

        }

        public TileUnit(Region region)
		{
			this.region = region;
		}

        public Region getRegion()
		{
			return region;
		}

        public void setRegion(Region region)
		{
			this.region = region;
		}


		public virtual MonsterKind GetMonsterKind(){
            return MonsterKind.NotMonster;
        }


		public virtual bool isHidden(){
            return false;
        }
	}
}
