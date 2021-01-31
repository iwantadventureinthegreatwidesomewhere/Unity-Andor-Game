using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    public enum GamePhase
    {
        NewGameSetup,
        ReadyToJoin,
        NewDay,
        ArcherTurn,
        DwarfTurn,
        WarriorTurn,
        WizardTurn,
        Completed
    }
}
