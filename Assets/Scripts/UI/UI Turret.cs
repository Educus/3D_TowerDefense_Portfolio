using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITurret : MonoBehaviour
{
    [SerializeField] Transform bulletOut;
    [SerializeField] UIBullet prefab;

    private RaycastHit hit;
    bool shooting = true;

    void Update()
    {
        if(Physics.Raycast(bulletOut.position, bulletOut.right, out hit))
        {
            if(shooting)
            {
                UIBullet uiBullet = Instantiate(prefab, bulletOut);
                uiBullet.Setup(hit.transform.position);
                shooting = false;
            }
        }
        else
        {
            shooting = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(bulletOut.position, bulletOut.right * 10);
    }
}
