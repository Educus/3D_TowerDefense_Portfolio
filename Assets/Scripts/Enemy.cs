using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float moveSpeed;

    Queue<Transform> cornerQueue;

    public void Setup(Transform[] corrners)
    {
        cornerQueue = new Queue<Transform>(corrners);
        transform.position = cornerQueue.Peek().position;
    }

    private void Update()
    {
        if (cornerQueue.Count <= 0)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 destination = cornerQueue.Peek().position;
        if (Vector3.Distance(transform.position, destination) <= 0)
            cornerQueue.Dequeue();
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, destination, moveSpeed * Time.deltaTime);
            // transform.LookAt(destination);
            transform.rotation = Quaternion.Lerp
                (
                    transform.rotation,
                    Quaternion.LookRotation(destination - transform.position),
                    0.1f
                );
        }
    }
}
