using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    public enum EventKind
    {
        Event1,
        Event2,
        Event3,
        Event4,
        Event5,
        Event6,
        Event7,
        Event8,
        Event9,
        Event10,
        Event11,
        Event12,
        Event13,
        Event14,
        Event15,
        Event16,
        Event17,
        Event18,
        Event19,
        Event20,
        Event21,
        Event22,
        Event23,
        Event24,
        Event25,
        Event26,
        Event27,
        Event28,
        Event29,
        Event30,
        Event31,
        Event32,
        Event33,
        Event34,
        Herb,
        EventNull,
    }

    public static class Extensions
    {


        public static string getDescription(this EventKind eventKind)
        {
            string description=null;
            //9
            if (eventKind == EventKind.Event9)
            {
                description= "Dark clouds cover the sun, filling all the good people of Andor with a strange foreboding";
            }
            //24
            if (eventKind == EventKind.Event24)
            {
                description= "A storm moves across the countryside and weighs upon the mood of the heroes.";
            }
            //19
            if (eventKind == EventKind.Event19)
            {
                description = "An exhausting day.";
            }
            //30
            if (eventKind == EventKind.Event30)
            {
                description = "A drink in the tavern.";
            }
            //33
            if (eventKind == EventKind.Event33)
            {
                description = "Their advanture is wearing down the heroes.";
            }
            //29
            if (eventKind == EventKind.Event29)
            {
                description = "The keepers of the Tree of Songs offer a gift.";
            }
            //26
            if (eventKind == EventKind.Event26)
            {
                description = "The minstrels sing a ballad about the deeds of the heroes, strengthening their determination.";
            }
            //21
            if (eventKind == EventKind.Event21)
            {
                description = "A mysterious terror lurks in the southern woods.";
            }
            //1
            if (eventKind == EventKind.Event1)
            {
                description = "The dwarf merchant Garz makes an offer.";
            }
            //32
            if (eventKind == EventKind.Event32)
            {
                description = "A sleepless night awaits the heroes.";
            }
            //20
            if (eventKind == EventKind.Event20)
            {
                description = "A farmer falls ill.";
            }
            //7
            if (eventKind == EventKind.Event7)
            {
                description = "Sulfurous mists surround the heroes.";
            }
            //25
            if (eventKind == EventKind.Event25)
            {
                description = "Keeper Melkart's generosity.";
            }
            //34
            if (eventKind == EventKind.Event34)
            {
                description = "The dwarf merchant Garz meets one of the heroes and offer him a trade.";
            }
            //18
            if (eventKind == EventKind.Event18)
            {
                description = "A wild gor storms forth.";
            }
            //2
            if (eventKind == EventKind.Event2)
            {
                description = "A bitting wind blows across the coast from the sea.";
            }
            //16
            if (eventKind == EventKind.Event16)
            {
                description = "Royal falcons fly high above the land, keeping watch.";
            }
            //22
            if (eventKind == EventKind.Event22)
            {
                description = "Rampaging creatures despoil the well at the foot of the mountains.";
            }
            //11
            if (eventKind == EventKind.Event11)
            {
                description = "The creatures gather their strength.";
            }
            //28
            if (eventKind == EventKind.Event28)
            {
                description = "A beautifully clear, starry night gives the heroes confidence.";
            }
            //3
            if (eventKind == EventKind.Event3)
            {
                description = "Wisdom from the Tree of Song.";
            }
            //8
            if (eventKind == EventKind.Event8)
            {
                description = "Trading ships reach the coast of Andor.";
            }
            //17
            if (eventKind == EventKind.Event17)
            {
                description = "Heavy weather moves across the land.";
            }
            //6
            if (eventKind == EventKind.Event6)
            {
                description = "The dwarf merchant Garz invites one of the heroes to have a drink.";
            }
            //27
            if (eventKind == EventKind.Event27)
            {
                description = "The creatures are possessed with blind fury.";
            }
            //15
            if (eventKind == EventKind.Event15)
            {
                description = "Rampaging creatures despoil the well in the south of Andor.";
            }
            //14
            if (eventKind == EventKind.Event14)
            {
                description = "A fragment of a very old sculpture has been found. Not all of the heroes are able to appreciate that kind of handiwork.";
            }
            //13
            if (eventKind == EventKind.Event13)
            {
                description = "A lovely sound of a horn echoes across the land.";
            }
            //10
            if (eventKind == EventKind.Event10)
            {
                description = "Jugglers from the north display their art.";
            }
            //23
            if (eventKind == EventKind.Event23)
            {
                description = "The king's blacksmiths are laboring tirelessly.";
            }
            //31
            if (eventKind == EventKind.Event31)
            {
                description = "Hot rain from the south lashes the land.";
            }
            //4
            if (eventKind == EventKind.Event4)
            {
                description = "The heroes replenish their water supplies at the river.";
            }
            //5
            if (eventKind == EventKind.Event5)
            {
                description = "Poisonous vapors from the mountains are tormenting the heroes.";
            }
            //12
            if (eventKind == EventKind.Event12)
            {
                description = "A farmer gild sings a beautiful song that wafts across the northern woods. But it fails to stir the hearts of all the heroes.";
            }
            // TO-DO: add all events
            return description;
        }

        public static string getEffect(this EventKind eventKind)
        {
            string effect=null;
            //9
            if (eventKind == EventKind.Event9)
            {
                effect="On this day, no hero is allowed to use a 10th hour. Place this card above the overtime area of the time track. At the end of the day, it is removed from the game.";
            }
            //24
            if (eventKind == EventKind.Event24)
            {
                effect="Any hero who is not on a forest space, in the mine (space 71), in the tavern (space 72), or in the castle (space 0) loses 2 willpower points.";
            }
            //19
            if (eventKind == EventKind.Event19)
            {
                effect = "On this day, the 9th and 10th hours will each cost 3 willpower points instead of 2.\nPlace this card above the overtime area of the time track. At the end of the day, it is removed from the game.";
            }
            //30
            if (eventKind == EventKind.Event30)
            {
                effect = "Place a wineskin on the tavern space (72). A hero who enters space 72 or is already standing there can collect the wineskin and place it on the small storage space on his hero board. If more than one hero is standing there, the hero with the lowest rank gets the wineskin.";
            }
            //33
            if (eventKind == EventKind.Event33)
            {
                effect = "One of the heroes immediately loses 1 strength point. You can decide as a group which hero that will be. If no hero has more than 1 point, nothing happens.";
            }
            //29
            if (eventKind == EventKind.Event29)
            {
                effect = "Now place a shield on space 57. A hero who enters space 57 or is already standing there can collect the shield and place it on the large storage space on his hero board. If more than one hero is standing there, the hero with the lowest rank gets the shield.";
            }
            //26
            if (eventKind == EventKind.Event26)
            {
                effect = "On this day, the 8th hour costs no willpower points.\nPlace this card above the overtime area of the time track. At the end of the day, it is removed from the game.";
            }
            //21
            if (eventKind == EventKind.Event21)
            {
                effect="A hero who enters space 22, 23, 24 25 or is already standing there will immediately lose 4 willpower points. If more than one hero is standing there,  the one with the highest rank loses the points.\nPlace this card next to space 24 until it is triggered. Then it is removed from the game.";
            }
            //1
            if (eventKind == EventKind.Event1)
            {
                effect = "Each hero may now purchase any article from the equipment board (except the witch's brew) in exchange for 3 willpower points."; 
            }
            //32
            if (eventKind == EventKind.Event32)
            {
                effect = "Every hero whose time marker is presently in the sunrise box loses 2 willpower points.";
            }
            //20
            if (eventKind == EventKind.Event20)
            {
                effect = "One farmer token on the game board that has not yet been taken to the castle must be removed from the game. The group can prevent that by paying gold and/or willpower points:\nfor 2 heroes, 2 gold/WP\nfor 3 heroes, 3 gold/WP\nfor 4 heroes, 4 gold/WP.";
            }
            //7
            if (eventKind == EventKind.Event7)
            {
                effect = "The hero with the lowest rank rolls one of his hero dice. The group loses the rolled number of willpower points.";
            }
            //25
            if (eventKind == EventKind.Event25)
            {
                effect = "Any hero with fewer than 6 willpower points rolls a hero die and gets the rolled number of willpower points.";
            }
            //34
            if (eventKind == EventKind.Event34)
            {
                effect = "One of the heroes can now purchase 10 willpower points in exchange for 2 strength points. You can decide as group which hero will be.";
            }
            //18
            if (eventKind == EventKind.Event18)
            {
                effect = "The gor on the space with the lowest number now moves one space in the direction of the arrow. The group can prevent that by paying gold and/or willpower points:\nfor 2 heroes, 2 gold/WP\nfor 3 heroes, 4 gold/WP\nfor 4 heroes, 6 gold/WP.";
            }
            //2
            if (eventKind == EventKind.Event2)
            {
                effect = "Each hero standing on a space with a number between 0 and 20 now loses 3 willpower points.";
            }
            //16
            if (eventKind == EventKind.Event16)
            {
                effect = "The hero with the highest rank is allowed to take a look at the top card on the event card deck. Then he gets to decide whether to remove the card from the game or to place it back on the deck.";
            }
            //22
            if (eventKind == EventKind.Event22)
            {
                effect = "The well token on space 45 is removed from the game.";
            }
            //11
            if (eventKind == EventKind.Event11)
            {
                effect = "On this day, each creature has one extra strength point.\nPlace this card next to the creature display. At the end of the day, it is removed from the game.";
            }
            //28
            if (eventKind == EventKind.Event28)
            {
                effect = "Every hero whose time marker is presently in the sunrise box gets 2 willpower points.";
            }
            //3
            if (eventKind == EventKind.Event3)
            {
                effect = "A hero who enters the Tree of Songs space or is already standing there gets 1 strength point. If more than one hero is standing there, the hero with the highest rank gets the strength point\nNow place this card on space 57 until a hero has gotten the strength point. Then remove it from the game.";
            }
            //8
            if (eventKind == EventKind.Event8)
            {
                effect = "A hero who enters the Tree of Songs space or is already standing there can buy 2 strength points there for just 2 gold.\nPlace this card on space 9 until a hero has made the purchase. Then remove it from the game.";
            }
            //17
            if (eventKind == EventKind.Event17)
            {
                effect = "Each hero with more than 12 willpower points immediately reduce his point total to 12.";
            }
            //6
            if (eventKind == EventKind.Event6)
            {
                effect = "The hero with the lowest rank gets to decide whether he wants to roll one of his hero dice. If he rolls 1, 2,3 or 4, he loses his rolled number of willpower points. If he rolls 5 or 6, he wins that number of willpower points.";
            }
            //27
            if (eventKind == EventKind.Event27)
            {
                effect = "The creature standing on the space with the highest number will now move one space along the arrow . The group can prevent that by paying gold and/or willpower points:\nfor 2 heroes, 2 gold/WP\nfor 3 heroes, 3 gold/WP\nfor 4 heroes, 4 gold/WP.";
            }
            //15
            if (eventKind == EventKind.Event15)
            {
                effect = "The well token on space 35 is removed from the game.";
            }
            //14
            if (eventKind == EventKind.Event14)
            {
                effect = "The dwarf and the warrior immediately get 3 willpower points each.";
            }
            //13
            if (eventKind == EventKind.Event13)
            {
                effect = "Each hero who has fewer than 10 willpower points can immediately raise his total to 10.";
            }
            //10
            if (eventKind == EventKind.Event10)
            {
                effect = "Each hero can now purchase 3 willpower points in exchange of 1 gold.";
            }
            //23
            if (eventKind == EventKind.Event23)
            {
                effect = "Up to two heroes with 6 or fewer strength points can each add 1 strength point to what they already have. You can decide as a group which heroes those will be.";
            }
            //31
            if (eventKind == EventKind.Event31)
            {
                effect = "Any hero who is not on a foreset space, in the mine (space 71), in the tavern (space 72), or in the castle (space 0) loses2 willpower points.";
            }
            //4
            if (eventKind == EventKind.Event4)
            {
                effect = "Each hero who is now standing on a space bordering the river gets a wineskin.";
            }
            //5
            if (eventKind == EventKind.Event5)
            {
                effect = "Each hero standing on a space with a number between 37 and 70 now loses 3 willpower points.";
            }
            //12
            if (eventKind == EventKind.Event12)
            {
                effect = "The wizard and the archer each immediately get 3 willpower points.";
            }
            // TO-DO: add all events, make the effect happens
            return effect;
        }
    }
}
