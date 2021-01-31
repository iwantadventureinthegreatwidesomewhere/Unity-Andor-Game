using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/* COMBAT CONTROL FLOW:
 *
 *  -  current player clicks on a monster, triggering OnMouseDown method in MonsterController
 *  -  MonsterController calls StartCombatRound in CombatManager
 * (1) CombatManager calls StartCombatRound in Combat
 * 
 * (2) main for loop:
 *      (3) CombatManager displays combat options to the current player and waits for player input
 *      (4) current player's battle value is calculated and displayed
 *      (5) CombatManager displays OK button to all players and waits for each player to press it
 *          - during this time, players can use items/abilities (you don't need to worry about it here)
 *      (6) after item/ability effects, current player's battle value is recalculated, displayed,
 *          and added to the party's total battle value
 *        
 * (7) after each hero has attacked, CombatManager displays the party's total battle value
 * (8) monster's battle value is calculated and displayed
 * (9) again, CombatManager displays OK button to all players and waits for each player to press it
 *      - during this time, players can use shield
 *
 * (10) if players win:
 *      - monster takes damage
 *      - if monster dies:
 *          (11) CombatManager displays reward choices to party leader and waits for leader's input
 *           -   combat ends
 *          
 * (12) if monster wins:
 *      - players take damage if they didn't use a shield
 *      - CombatManager displays message to players who died
 *      - dead players are removed from the party
 *
 * (13) each player selects whether they want to continue fighting or retreat
 *      - if all players retreat, combat ends
 *      - else, StartCombatRound is called again
 *      
 */

namespace Scripts
{
    public class Combat
    {
        CombatManager combatManager = CombatManager.instance;

        CombatParty party;
        Monster monster;
        bool princePresent;

        public Combat(CombatParty party, Monster monster, bool prince)
        {
            this.party = party;
            this.monster = monster;
            princePresent = prince;
        }
        // (1)
        public IEnumerator StartCombatRound()
        {
            int partyBattleValue = 0;
            foreach (Hero hero in party)
            {
                hero.AdvanceTimeTrack();
                hero.UsedHeroPower = false;
            }
            //Sum battle value for each hero in the party
            // (2)
            foreach (Hero hero in party)
            {
                // (3)
                combatManager.StartHeroAttack();
                int battleValue = 0;
                //Wait for player to roll their dice
                combatManager.DoneRolling = false;
                while(!combatManager.DoneRolling)
                {
                    bool archerDone = false;
                    //Wait for dice roll
                    while(!combatManager.DiceButtonPressed())
                    {
                        if (combatManager.DoneRollingButtonPressed())
                        {
                            archerDone = true;
                            break;
                        }
                        yield return null;
                    }
                    //If archer is done rolling, move to next hero
                    if (archerDone)
                    {
                        break;
                    }
                    // (4)
                    battleValue = hero.CalculateBattleValue();
                    combatManager.SetHeroBV("Your battle value: " + battleValue);
                    yield return null;
                }
                combatManager.DisplayDoneRollingButton(false);
                //Wait for players to use ability/item
                //Continue only once everyone presses ok
                yield return new WaitForSeconds(1);
                //TODO: Each player needs to press their own OK button to continue
                // (5)
                foreach(Hero hero1 in party)
                {
                    combatManager.DisplayOKButton();
                    while(!combatManager.OKButtonPressed())
                    {
                        yield return null;
                    }
                }
                // (6)
                battleValue = hero.CalculateBattleValue(combatManager.heroRolls);
                if(hero.StrengthBoost > 0)
                {
                    battleValue += hero.StrengthBoost;
                    hero.StrengthBoost = 0;
                }
                combatManager.SetHeroBV("Your battle value: " + battleValue);
                partyBattleValue += battleValue;
            }
            //Increase battle value if Prince is on the tile
            if (princePresent)
            {
                partyBattleValue += PrinceThorald.Strength;
            }
            // (7)
            combatManager.SetHeroBV("Your party's battle value: " + partyBattleValue);
            //Calculate battle value for monster
            // (8)
            int monsterBattleValue = monster.CalculateBattleValue();
            combatManager.SetMonsterBV("Monster's battle value: " + monsterBattleValue);

            //Display the result of the battle, then wait for players to confirm
            //They can use this time to use shield, heal, etc
            yield return new WaitForSeconds(1);
            //TODO: Each player needs to press OK to continue
            // (9)
            foreach (Hero hero1 in party)
            {
                combatManager.DisplayOKButton();
                while (!combatManager.OKButtonPressed())
                {
                    yield return null;
                }
            }
            // (10)
            if (partyBattleValue - monsterBattleValue > 0) //Players win
            {
                int result = partyBattleValue - monsterBattleValue;
                monster.TakeDamage(result);
                if (monster.IsDead())
                {
                    //TODO: Make it so only the party leader can see these buttons
                    // (11)
                    combatManager.SetCombatStatusText("You win! Choose your reward");
                    combatManager.DisplayRewardButtons();
                    while (true)
                    {
                        if (combatManager.GoldButtonPressed())
                        {
                            party.GetLeader().IncrementGold(monster.GetRewardValue());
                            break;
                        }
                        else if (combatManager.WPButtonPressed())
                        {
                            party.GetLeader().Heal(monster.GetRewardValue());
                            break;
                        }
                        yield return null;
                    }
                    GameObject.FindObjectOfType<NarratorController>().advance();
                    if(monster.onTower())
                    {
                        GameObject.FindObjectOfType<GameManager>().setSkralDefeated(true);
                        //GameObject.Find("GameManager").GetComponent<GameManager>().gameEnd = true;
                        GameObject.Find("Narrator").GetComponent<NarratorController>().jumpToN();
                    }
                    combatManager.EndCombat();
                    yield break;
                }
            }
            // (12)
            else if (partyBattleValue - monsterBattleValue < 0) //Monster wins
            {
                int result = monsterBattleValue - partyBattleValue;
                List<Hero> dead = new List<Hero>();
                foreach (Hero hero in party)
                {
                    if(hero.UsedShield)
                    {
                        hero.UsedShield = false;
                    }
                    else
                    {
                        hero.TakeDamage(result);
                        if (hero.IsDead())
                        {
                            hero.Die();
                            dead.Add(hero);
                        }
                    }
                }
                foreach (Hero hero in dead)
                {
                    //TODO: Display this message and button only to the players who died
                    combatManager.SetCombatStatusText("You died! You must retreat!");
                    combatManager.DisplayRetreatButton();
                    while (!combatManager.RetreatButtonPressed())
                    {
                        yield return null;
                    }
                    party.RemoveHero(hero);
                }

                //If all heroes died, end the battle
                if (party.IsEmpty())
                {
                    monster.Recover();
                    combatManager.EndCombat();
                    yield break;
                }
            }
            CombatManager.instance.setBVSync(monsterBattleValue, partyBattleValue);
            combatManager.EndCombatRound();
            //Ask players if they want to continue
            // (13)
            List<Hero> remove = new List<Hero>();
            foreach(Hero hero in party)
            {
                //TODO: each hero should have their own Continue and Retreat button
                while (true)
                {
                    if (!hero.HasTimeLeft())
                    {
                        remove.Add(hero);
                        break;
                    }
                    else if (combatManager.ContinueButtonPressed())
                    {
                        break;
                    }
                    else if(combatManager.RetreatButtonPressed())
                    {
                        remove.Add(hero);
                        break;
                    }
                    yield return null;
                }
            }
            foreach (Hero hero in remove)
            {
                party.RemoveHero(hero);
            }
            //If no heroes in party, end the battle
            if (party.IsEmpty())
            {
                monster.Recover();
                combatManager.EndCombat();
                yield break;
            }
            //Otherwise, continue the battle
            else
            {
                combatManager.StartCombatRound(this);
            }
        }
    }
}

