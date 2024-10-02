using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBullet : MonoBehaviour
{
    [SerializeField] int moveSpeed = 1;
    [SerializeField] Vector3 target;

    private void OnTriggerEnter(Collider other)
    {
        IUIHitablea uiHitablea = other.GetComponent<IUIHitablea>();

        if (uiHitablea != null)
        {
            uiHitablea.Dead();
        }

        Destroy(gameObject);
    }

    public void Setup(Vector3 target)
    {
        this.target = target;
    }

    public void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
    }
}
