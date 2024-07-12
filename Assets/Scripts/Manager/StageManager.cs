using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] GameObject[] gameMapPrefabs;

    public GameObject gameMap;
    private void Awake()
    {
        CreateMap();
    }

    public void CreateMap()
    {
        gameMap = Instantiate(gameMapPrefabs[0]);
        gameMap.transform.position = Vector3.zero;
    }
    void Update()
    {
        
    }
}
