using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] List<GameObject> enemys = null;
    [SerializeField] GameObject target;
    [SerializeField] float shortDistance;

    [SerializeField] Bullet prefabBullet;

    [SerializeField] GameObject[] bulletOut;
    [SerializeField] int countBulletOut;
    [SerializeField] float attackRange = 5;
    [SerializeField] float cooltime;
    [SerializeField] float damage = 0;
    [SerializeField] float slowTime = 0;
    [SerializeField] float slowdown = 1;
    
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

        if (enemys != null)
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
        SearchEnemy();

        if (target != null && Vector3.Distance(transform.position, target.transform.position) <= attackRange)
        {
            transform.LookAt(target.transform.position);

            Bullet bullet = Instantiate(prefabBullet, bulletOut[Random.Range(0, countBulletOut)].transform);
            bullet.Setup(target.transform.position, damage, slowTime, slowdown);

            yield return new WaitForSeconds(cooltime);
        }
    }
}
