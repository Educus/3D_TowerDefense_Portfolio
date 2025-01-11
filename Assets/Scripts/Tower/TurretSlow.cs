using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretSlow : TurretBase
{
    protected override void Lv1()
    {
        towersDB = TowerDB.Instance.SlowTurret();

        ChangeStat();
    }

    protected override void Lv2()
    {
        towersDB = TowerDB.Instance.SlowTurret();

        ChangeStat();
    }
    protected override void Lv3()
    {
        towersDB = TowerDB.Instance.SlowTurret();

        ChangeStat();
    }
}
