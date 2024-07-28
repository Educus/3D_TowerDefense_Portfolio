using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] Enemy[] prefab;
    [SerializeField] StageManager stageManager;
    [SerializeField] int enemyValue;
    [SerializeField] float spawnRate;
    [SerializeField] int spawnCount;
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
    public void WaveStart()
    {

        // this.score = clearStage[stage].Split(',');
        // this.score[round] = score.ToString();

    }

    public IEnumerator IESpawn(int[] howEnemy, int[] howMany, int spawnRate)
    {
        int remaining;

        for (int i = 0; i < howEnemy.Length; i++)
        {
            remaining = howMany[i];

            while (remaining > 0)
            {
                yield return new WaitForSeconds(spawnRate);
                Enemy newEnemy = Instantiate(prefab[howEnemy[i]], transform);
                newEnemy.Setup(corners);
                remaining--;
            }
        }
    }


    public string[] waveEnemy = // 
    {
        "", // Desert 1round 1wave
        "", // Desert 1round 2wave
        "", // Desert 1round 3wave
        "", // Desert 1round 4wave
        "", // Desert 1round 5wave
        "", // Desert 2round 1wave
        "", // Desert 2round 2wave
        "", // Desert 2round 3wave
        "", // Desert 2round 4wave
        "", // Desert 2round 5wave
        "", // Desert 3round 1wave
        "", // Desert 3round 2wave
        "", // Desert 3round 3wave
        "", // Desert 3round 4wave
        "", // Desert 3round 5wave
        "", // Desert 4round 1wave
        "", // Desert 4round 2wave
        "", // Desert 4round 3wave
        "", // Desert 4round 4wave
        "", // Desert 4round 5wave
        "", // Desert 5round 1wave
        "", // Desert 5round 2wave
        "", // Desert 5round 3wave
        "", // Desert 5round 4wave
        "", // Desert 5round 5wave
        "", // Desert 6round 1wave
        "", // Desert 6round 2wave
        "", // Desert 6round 3wave
        "", // Desert 6round 4wave
        "", // Desert 6round 5wave

        "", // Forest 1round 1wave
        "", // Forest 1round 2wave
        "", // Forest 1round 3wave
        "", // Forest 1round 4wave
        "", // Forest 1round 5wave
        "", // Forest 2round 1wave
        "", // Forest 2round 2wave
        "", // Forest 2round 3wave
        "", // Forest 2round 4wave
        "", // Forest 2round 5wave
        "", // Forest 3round 1wave
        "", // Forest 3round 2wave
        "", // Forest 3round 3wave
        "", // Forest 3round 4wave
        "", // Forest 3round 5wave
        "", // Forest 4round 1wave
        "", // Forest 4round 2wave
        "", // Forest 4round 3wave
        "", // Forest 4round 4wave
        "", // Forest 4round 5wave
        "", // Forest 5round 1wave
        "", // Forest 5round 2wave
        "", // Forest 5round 3wave
        "", // Forest 5round 4wave
        "", // Forest 5round 5wave
        "", // Forest 6round 1wave
        "", // Forest 6round 2wave
        "", // Forest 6round 3wave
        "", // Forest 6round 4wave
        "", // Forest 6round 5wave

        "", // Winter 1round 1wave
        "", // Winter 1round 2wave
        "", // Winter 1round 3wave
        "", // Winter 1round 4wave
        "", // Winter 1round 5wave
        "", // Winter 2round 1wave
        "", // Winter 2round 2wave
        "", // Winter 2round 3wave
        "", // Winter 2round 4wave
        "", // Winter 2round 5wave
        "", // Winter 3round 1wave
        "", // Winter 3round 2wave
        "", // Winter 3round 3wave
        "", // Winter 3round 4wave
        "", // Winter 3round 5wave
        "", // Winter 4round 1wave
        "", // Winter 4round 2wave
        "", // Winter 4round 3wave
        "", // Winter 4round 4wave
        "", // Winter 4round 5wave
        "", // Winter 5round 1wave
        "", // Winter 5round 2wave
        "", // Winter 5round 3wave
        "", // Winter 5round 4wave
        "", // Winter 5round 5wave
        "", // Winter 6round 1wave
        "", // Winter 6round 2wave
        "", // Winter 6round 3wave
        "", // Winter 6round 4wave
        "", // Winter 6round 5wave
    };

}

enum EnemyType
{
    WalkMushroomSmile,
    RunMushroomSmile,
    WalkMushroomAngry,
    RunMushroomAngry,
    WalkGrunt,
    RunGrunt,
    WalkCactus,
    RunCatus,
    Golem
}

