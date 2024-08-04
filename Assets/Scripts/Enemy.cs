using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IHitablea
{
    [SerializeField] float enemyHp;
    [SerializeField] float moveSpeed;
    [SerializeField] float slowdown = 1;
    Queue<Transform> cornerQueue;
    bool startingIESlow = false;

    public void Setup(Transform[] corrners)
    {
        cornerQueue = new Queue<Transform>(corrners);
        transform.position = cornerQueue.Peek().position;
    }

    private void Update()
    {
        if (ScoreManager.Instance.hp <= 0)
        {
            return;
        }

        if (cornerQueue.Count <= 0)
        {
            ScoreManager.Instance.hp -= 1;
            Destroy(gameObject);
            return;
        }

        Vector3 destination = cornerQueue.Peek().position;
        if (Vector3.Distance(transform.position, destination) <= 0)
            cornerQueue.Dequeue();
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, destination, moveSpeed * slowdown * Time.deltaTime);
            // transform.LookAt(destination);
            transform.rotation = Quaternion.Lerp
                (
                    transform.rotation,
                    Quaternion.LookRotation(destination - transform.position),
                    0.1f
                );
        }
    }
    public void Damage(float damage)
    {
        enemyHp -= damage;

        if(enemyHp <= 0)
        {
            Destroy(gameObject);
        }
    }
    public void Slow(float time, float slow)
    {
        if(startingIESlow)
        {
            StopCoroutine(IESlow(time, slow));
        }
        StartCoroutine(IESlow(time, slow));
    }

    IEnumerator IESlow(float time, float slow)
    {
        startingIESlow = true;

        slowdown -= slowdown * slow;

        while (time > 0)
        {
            time -= Time.deltaTime;

            yield return null;
        }

        slowdown = 1;

        startingIESlow = false;

        yield return null;
    }
}
