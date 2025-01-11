using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] int moveSpeed = 5;
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

        if(Vector3.Distance(transform.position, target) <= 0.05f)
        {
            Destroy(gameObject);
        }
    }
}
