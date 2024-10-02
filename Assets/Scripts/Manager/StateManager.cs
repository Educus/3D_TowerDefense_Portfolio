using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : Singleton<StateManager>
{
    public static int maxHp = 10;
    public static int firstGold = 30;

    public int hp = 0;
    public int gold = 0;

    public void ResetState()
    {
        hp = maxHp;
        gold = firstGold;
    }
    public void BonusGold(int time)
    {
        gold += time * 1;
    }
}
