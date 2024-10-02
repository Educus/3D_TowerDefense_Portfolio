using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    [SerializeField] GameObject[] gameMapPrefabs;

    public GameObject gameMap;

    public int stage;
    public int round;

    private void Awake()
    {
        stage = ScoreManager.Instance.stage[0];
        round = ScoreManager.Instance.stage[1];

        CreateMap();
    }

    public void CreateMap()
    {
        gameMap = Instantiate(gameMapPrefabs[stage]);
        gameMap.transform.position = Vector3.zero;
    }

    public GameObject GetMap()
    {
        return gameMap;
    }
}
