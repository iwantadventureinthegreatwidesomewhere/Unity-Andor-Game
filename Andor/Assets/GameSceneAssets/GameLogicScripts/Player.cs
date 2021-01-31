using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    public class Player
    {
		int playerID;
		private Game game;
		private Hero hero;
		private PlayerStatus playerStatus;

        public Player()
		{
			playerStatus = PlayerStatus.Available;
		}

        public void setPlayerStatus(PlayerStatus playerStatus)
		{
			this.playerStatus = playerStatus;
		}

        public bool joinGame(Game game)
		{
            if(game != null && game.isJoinable())
			{
				bool status = game.join(this);

				if (status)
				{
					this.game = game;
					playerStatus = PlayerStatus.GameLobby;
					return true;
				}
			}

			return false;
		}

        public void chooseHero(HeroKind heroKind)
		{
            if(game != null)
			{
				Hero hero = new Hero(game, heroKind, null);
				bool status = game.chooseHero(this, hero);

				if (status)
				{
					this.hero = hero;
				}
			}
		}
    }
}
