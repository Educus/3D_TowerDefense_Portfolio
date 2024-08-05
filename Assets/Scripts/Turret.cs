using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] List<GameObject> enemys = null;
    [SerializeField] GameObject target = null;
    [SerializeField] float shortDistance;

    [SerializeField] Bullet prefabBullet;

    [SerializeField] GameObject[] bulletOut;
    [SerializeField] int countBulletOut;
    [SerializeField] float attackRange = 5;
    [SerializeField] float cooltime;
    [SerializeField] float damage = 0;
    [SerializeField] float slowTime = 0;
    [SerializeField] float slowdown = 0;

    [SerializeField] bool activeTurret = false;

    // 가장 가까운 적 찾기
    // 그 대상의 좌표 가져오기
    // 머리를 그 대상 방향으로 돌리기
    // 그 대상에게 총알을 발사하고 쿨타임만큼 대기

    void Start()
    {
        countBulletOut = bulletOut.Length;  // 랜덤 총구

        StartCoroutine(ShotBullet());
    }

    public void SearchEnemy()
    {
        enemys = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));

        if (enemys.Count != 0)
        {
            shortDistance = Vector3.Distance(gameObject.transform.position, enemys[0].transform.position);

            target = enemys[0];

            foreach (GameObject found in enemys)
            {
                float distance = Vector3.Distance(gameObject.transform.position, found.transform.position);

                if (distance < shortDistance)
                {
                    target = found;
                    shortDistance = distance;
                }
            }
        }
    }

    IEnumerator ShotBullet()
    {
        while (true)
        {
            if (activeTurret)
            {
                SearchEnemy();

                if (target != null)
                {
                    if (Vector3.Distance(transform.position, target.transform.position) <= attackRange)
                    {
                        transform.LookAt(target.transform.position);

                        Bullet bullet = Instantiate(prefabBullet, bulletOut[Random.Range(0, countBulletOut - 1)].transform);
                        bullet.Setup(target.transform.position + new Vector3(0,0.15f,0), damage, slowTime, slowdown);
                        bullet = null;

                        yield return new WaitForSeconds(cooltime);
                    }
                }
            }

            yield return null;
        } 
    }

    public void ActiveTurret()
    {
        activeTurret = true;
    }
}
