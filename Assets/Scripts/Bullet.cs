using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float holdingTime = 2.0f;     // 총알 유지시간
    [SerializeField] int moveSpeed = 1;
    [SerializeField] Vector3 target;

    [SerializeField] public float damage;
    [SerializeField] public float slowTime;
    [SerializeField] public float slowdown;

    private void OnTriggerEnter(Collider other)
    {
        IHitablea Hitablea = other.GetComponent<IHitablea>();

        if (Hitablea != null)
        {
            Hitablea.Damage(damage);
            Hitablea.Slow(slowTime, slowdown);

            Destroy(gameObject);
        }
    }

    public void Setup(Vector3 target, float damage, float slowTime, float slowdown)
    {
        this.target = target;
        this.damage = damage;
        this.slowTime = slowTime;
        this.slowdown = slowdown;
    }

    public void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);

        holdingTime -= Time.deltaTime;

        if(holdingTime <= 0)
        {
            Destroy(gameObject);
        }
    }
}
