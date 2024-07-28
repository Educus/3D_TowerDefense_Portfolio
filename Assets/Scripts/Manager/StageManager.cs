using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] GameObject[] gameMapPrefabs;

    public GameObject gameMap;

    public int stage;
    public int round;

    private void Awake()
    {
        stage = ScoreManager.Instance.stage;
        round = ScoreManager.Instance.round;

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
