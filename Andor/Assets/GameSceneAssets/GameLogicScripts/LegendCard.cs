using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Scripts
{
    public class LegendCard {
        LegendCardKind ck;
        GameDifficulty difficulty;


        public LegendCard(LegendCardKind pKind,GameDifficulty pDifficulty)
        {
            ck = pKind;
            difficulty = pDifficulty;
        }

        public string getDescription()
        {
            string description = null;
            if (ck==LegendCardKind.A)
            {
                description = "    \tA gloomy mood has fallen upon the people. Rumors are making the rounds that skrals have set up a strounghold in some undisclosed location. The heroes have scattered themselves across the entire land in search of this location. The defense of the castle is in their hands alone.\n    \tMany farmers have asked for help and are seeking shelter behind the high walls of Rietburg Castle.\n    \tAt first sunlight, the heroes receive a message: Old king Brandur's willpower seems to have weakened with the passage of time. But there is said to be an herb growing in the mountain passes that can revive a person's life.";
            }
            if (ck == LegendCardKind.C)
            {
                description = "    \tThe king's scouts have discovered the skral stronghold.\n    \tAnd there's more unsettlinig news: Rumors are circulating about cruel wardraks from the south. They have not yet been sighted, but more and more farmers are losing their courage, leaving their farmsteads, and seeking safety in the castle.\n    \tBut there's good news from the south too: Prince Thorald, just back from a battle on the edge of the southern forest, is preparing himself to help the heroes.";
            }
            if (ck == LegendCardKind.G)
            {
                description = "    \tPrince Thorald joins up with a scouting patrol with the intention of leaving for just a few days. But he is not to be see again for quite a long time.\n    \tShadows are moving in the moonlight. The rumors were right - the wardraks are coming.";
            }
            if(ck == LegendCardKind.R&& difficulty==GameDifficulty.Easy)
            {
                description = "    \tThe heroes learn about an ancient magic that still hold power: rune stones!";
            }
            if (ck == LegendCardKind.R&& difficulty == GameDifficulty.Hard)
            {
                description = "    \tThe witch Reka tells the heroes about an ancient magic that still holds power: rune stones!";
            }
            return description; 
        }

        public string getEffect()
        {
            string effect = null;
            if (ck == LegendCardKind.A&& difficulty == GameDifficulty.Easy)
            {
                effect = "Gors on spaces 8, 20, 21, 26, 48. Skral on 19.\nFarmer on 24, 36 (which can defend gors entering the castle for one time)";
            }
            if (ck == LegendCardKind.A&& difficulty == GameDifficulty.Hard)
            {
                effect = "Gors on spaces 8, 20, 21, 26, 48. Skral on 19.\nFarmer on 24 (which can defend monsters entering the castle for one time)";
            }
            if (ck == LegendCardKind.C&& difficulty == GameDifficulty.Easy)
            {
                effect = "Skral on tower is blue, has SP: 10 for 2 heroes, 20 for 3 heroes, 30 for 4 heroes\nNew Gors on 27, 31. New normal Skral on 29\nNew farmer on 28\nPrince Thorald on 72.";
            }
            if (ck == LegendCardKind.C&& difficulty == GameDifficulty.Hard)
            {
                effect = "Skral on tower is blue, has SP: 20 for 2 heroes, 30 for 3 heroes, 40 for 4 heroes\nNew Gors on 27, 31. New normal Skral on 29\nNew farmer on 28\nPrince Thorald on 72";
            }
            if (ck == LegendCardKind.G)
            {
                effect = "Prince Thorald removed\nWardraks on spaces 26 and 27.";
            }
            if (ck == LegendCardKind.R&& difficulty == GameDifficulty.Easy)
            {
                effect = "New gor on 43 and new skral on 39.\nRune stones now scatter around the map.";
            }
            if (ck == LegendCardKind.R&& difficulty == GameDifficulty.Hard)
            {
                effect = "New gors on 32, 43 and new skral on 39.\nRune stones now scatter around the map.";
            }
            return effect;
        }

        public string getTask()
        {
            string task = "";
            if (ck == LegendCardKind.A)
            {
                task = "Task: The heroes must heal the king with the medicinal herb. To do that, they must find the witch. Only she knows the locations where this herb grows. The witch is hiding behind one of the fog tokens.";
            }
            if (ck == LegendCardKind.C)
            {
                task = "Task: The skral on the tower must be defeated. As soon as he is defeated, the Narrator is advanced to the letter 'N' on the Legend track.";
            }
            return task;
        }


    }
}
