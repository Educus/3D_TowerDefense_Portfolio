using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TurretBase : MonoBehaviour
{
    private GameObject prefabBulletObject;
    private Bullet prefabBullet;

    private List<Transform> bulletOut = new List<Transform>();

    private TowerSensor towerSensor;
    private GameObject target = null;

    // stat
    protected float[] towersDB;
    private float range;
    private float damage;
    private float bulletSize;
    private float slowDown;
    private float slowTime;
    private float coolTime;

    public bool activeTurret = false;
    private bool isCoolTime = false;

    private void Start()
    {
        prefabBullet = Resources.Load<Bullet>("Bullet");

        // TowerDB에서 Lv1에 해당하는 스탯 가져오기
        Lv1();

        // 총구 찾기
        FindBulletOut();

        // 센서
        Sensor();
    }

    protected void ChangeStat()
    {
        range = towersDB[0];
        damage = towersDB[1];
        bulletSize = towersDB[2];
        slowDown = towersDB[3];
        slowTime = towersDB[4];
        coolTime = towersDB[5];
    }

    private void FindBulletOut()
    {
        foreach(Transform child in transform.Find("BulletOut"))
        {
            bulletOut.Add(child);
        }
    }

    private void Sensor()
    {
        GameObject sensor = transform.Find("TowerSensor").gameObject;

        if(sensor.GetComponent<TowerSensor>() == null)
            transform.Find("TowerSensor").gameObject.AddComponent<TowerSensor>();

        towerSensor = transform.Find("TowerSensor").GetComponent<TowerSensor>();
        towerSensor.gameObject.GetComponent<CapsuleCollider>().radius = range;
    }

    private void Update()
    {
        if (activeTurret == false) return;
        if (towerSensor.active == false) return;

        // 타겟 찾기
        Target();
        // 타겟 바라보기
        LookTarget();

        if (isCoolTime) return;

        // 타겟 공격
        Attack();
    }

    private void Target()
    {
        if (target == null)
        {
            SearchEnemy();
        }

        if (target != null && Distance(target) > range + 1)
        {
            target = null;
            return;
        }
    }

    private void SearchEnemy()
    {
        if (towerSensor.enemyList.Count == 0) return;
        else
        {
            target = towerSensor.enemyList[0];

            foreach (GameObject enemy in towerSensor.enemyList)
            {
                if (enemy != null && target != null)
                {
                    float distance = Distance(enemy);

                    if (distance < Distance(target))
                    {
                        target = enemy;
                    }
                }
            }
        }
    }

    private float Distance(GameObject entity)
    {
        return Vector3.Distance(gameObject.transform.position, entity.transform.position);
    }

    private void LookTarget()
    {
        if (target == null) return;

        transform.LookAt(target.transform.position);
        transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);
    }

    private void Attack()
    {
        if (target == null) return;

        isCoolTime = true;

        Bullet bullet = Instantiate(prefabBullet, bulletOut[Random.Range(0, bulletOut.Count - 1)].transform);

        bullet.transform.parent = null;
        bullet.transform.localScale = new Vector3(bulletSize, bulletSize, bulletSize);
        bullet.transform.parent = gameObject.transform.Find("TowerDB");

        bullet.Setup(target.transform.position + new Vector3(0, 0.15f, 0), damage, slowTime, slowDown);
        bullet = null;

        StartCoroutine(IECoolTime());
    }

    IEnumerator IECoolTime()
    {
        float coolTimes = coolTime;

        while (coolTimes > 0)
        {
            coolTimes -= Time.deltaTime;

            yield return null;
        }

        isCoolTime = false;

        yield return null;
    }
    
    public void ActiveTurret()
    {
        activeTurret = true;
    }

    protected abstract void Lv1();
    protected abstract void Lv2();
    protected abstract void Lv3();
}
