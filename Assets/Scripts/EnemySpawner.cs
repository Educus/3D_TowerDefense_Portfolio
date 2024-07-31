using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] public Enemy[] prefab;
    [SerializeField] StageManager stageManager;
    [SerializeField] Transform[] corners;

    public GameObject map;
    public GameObject corner;
    public int cornerChild;

    private void Start()
    {
        map = stageManager.GetMap();
        corner = map.transform.Find("Grid").gameObject.transform.Find("CornerMap").gameObject;
        cornerChild = corner.transform.childCount;

        corners = new Transform[cornerChild];

        for (int i = 0; i < cornerChild; i++)
        {
            corners[i] = corner.transform.GetChild(i);
        }
    }

    public IEnumerator IESpawn(int howEnemy, int howMany, float spawnRate)
    {
        int remaining = howMany;

        while (remaining > 0)
        {
            yield return new WaitForSeconds(spawnRate);
            Enemy newEnemy = Instantiate(prefab[howEnemy], transform);
            newEnemy.Setup(corners);
            remaining--;
        }
    }
}

