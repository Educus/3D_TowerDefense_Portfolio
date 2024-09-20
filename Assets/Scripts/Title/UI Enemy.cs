using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIEnemy : MonoBehaviour, IUIHitablea
{
    [SerializeField] float moveSpeed;
    
    private Vector3 destination;
    private Animator anim;
    private Collider collider;

    public bool playing = true;

    public void Setup(Transform target)
    {
        destination = target.position;
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
        collider = GetComponent<Collider>();
    }
    private void Update()
    {
        if (!playing)
        {
            return;
        }
        transform.position = Vector3.MoveTowards(transform.position, destination, moveSpeed * Time.deltaTime);
        // transform.LookAt(destination);
        transform.rotation = Quaternion.Lerp
        (
            transform.rotation,
            Quaternion.LookRotation(destination - transform.position),
            0.1f
        );
    }

    public void Dead()
    {
        playing = false;
        anim.SetTrigger("Dead");
        collider.enabled = false;
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
