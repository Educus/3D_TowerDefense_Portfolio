using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEnemySpawner : MonoBehaviour
{
    [SerializeField] UIEnemy prefab;
    [SerializeField] Transform target;

    private void Start()
    {
        StartCoroutine(IESpawn());
    }
    public IEnumerator IESpawn()
    {
        while (true)
        {
            UIEnemy uiEnemy = Instantiate(prefab, transform);
            uiEnemy.Setup(target);

            yield return new WaitForSeconds(5);
        }
    }

}
