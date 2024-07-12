using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] Enemy[] prefab;
    [SerializeField] int enemyValue;
    [SerializeField] float spawnRate;
    [SerializeField] int spawnCount;
    [SerializeField] Transform[] corners;

    private void Start()
    {
        
    }
    public void WaveStart()
    {
        StartCoroutine(IESpawn());
    }

    public IEnumerator IESpawn()
    {
        int remaining = spawnCount;
        while (remaining > 0)
        {
            yield return new WaitForSeconds(spawnRate);
            Enemy newEnemy = Instantiate(prefab[enemyValue], transform);
            newEnemy.Setup(corners);
            remaining -= 1;
        }
    }

}
