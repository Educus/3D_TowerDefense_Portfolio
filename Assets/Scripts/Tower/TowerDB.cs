using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerDB : Singleton<TowerDB>
{
    private float range;
    private float damage;
    private float bulletSize;
    private float slowDown;
    private float slowTime;
    private float coolTime;

    public float[] BasicsTurret()
    {
        range = 5f;
        damage = 1f;
        bulletSize = 0.5f;
        slowDown = 0f;
        slowTime = 0f;
        coolTime = 1f;

        return new float[]{range, damage, bulletSize, slowDown, slowTime, coolTime};
    }

    public float[] CannonTurret()
    {
        range = 15f;
        damage = 5f;
        bulletSize = 0.7f;
        slowDown = 0f;
        slowTime = 0f;
        coolTime = 5f;

        return new float[] { range, damage, bulletSize, slowDown, slowTime, coolTime };
    }

    public float[] SlowTurret()
    {
        range = 3f;
        damage = 0.5f;
        bulletSize = 0.2f;
        slowDown = 0.3f;
        slowTime = 3f;
        coolTime = 1.5f;

        return new float[] { range, damage, bulletSize, slowDown, slowTime, coolTime };
    }

    public float[] FireTurret()
    {
        range = 0f;
        damage = 0f;
        bulletSize = 1f;
        slowDown = 0f;
        slowTime = 0f;
        coolTime = 0f;
        
        return new float[] { range, damage, bulletSize, slowDown, slowTime, coolTime };
    }
}
