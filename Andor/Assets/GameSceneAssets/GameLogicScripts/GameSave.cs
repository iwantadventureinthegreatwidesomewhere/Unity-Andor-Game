namespace Scripts {

    struct GameSave {
        int[] turnOrder;
        HeroKind[] heros;
        int turnTick;
        bool witchFound;
        GameManager.gameState state;
        EventKind activeEvent;
        RegionSave[] regions;
        EventSave[] events;
    }

    struct RegionSave {
        int rank;
        int gold;
        bool hasHerb;
        EventKind[] events;
        ItemSave[] itemsOnGround;

        TileUnitSave[] tileUnits;
    }

    struct TileUnitSave {
        int region;
        bool hidden;
        TileUnitType tileUnitType;

        CombatantSave combatant;
        FarmerSave farmer;
        PrinceSave prince;
        WitchSave witch;
        WellSave well;
        FogSave fog;
        CastleSave castle;
        MerchantSave merchant;
    }

    struct CombatantSave {
        int maxStrength;
        int maxWillpower;
        int numRegularDice;
        int numBlackDice;

        int strengthPoints;
        int willpowerPoints;

        int diceRemaining;
        int blackDiceRemaining;

        MonsterSave monster;
        HeroSave hero;
    }

    struct MonsterSave {
        int rewardValue;
        MonsterKind monsterKind;
        bool isOnTower;
    }

    struct HeroSave {
        HeroKind heroKind;
        int gold;
        bool dayEnded;
        bool hasHerb;
        bool usedHeroPower;
        int freeMoveSpaces;
        bool usedShield;
        bool usedBrew;
        int strengthBoost;

        ItemSave[] lightItems;
        ItemSave heavyItem;
        ItemSave helm;
    }

    struct FarmerSave {
        HeroKind guider;
        bool isInCastle;
        bool isGuided;
        bool isDead;
    }

    struct WitchSave {
        ItemSave witchsBrew;
    }

    struct PrinceSave {
        int strength;
        int movement;
    }

    struct WellSave {
        bool usedToday;
    }

    struct FogSave {
        TileUnitType hiddenUnit;
        FogKind fogKind;
        bool revealed;
    }

    struct CastleSave {
        int numGoldenShields;
    }

    struct MerchantSave {
        ItemSave[] itemsForSale;
        bool isMine;
        bool canUseWP;
        bool canBuyWP;
    }

    struct ItemSave {
        ItemType itemType;
        bool usedThisTurn;
        ItemDurability itemDurability;
        ItemFullness itemFullness;
    }

    struct EventSave {
        EventKind eventKind;
        bool hidden;
        bool onHold;
    }
}