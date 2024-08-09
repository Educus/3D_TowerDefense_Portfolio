using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour
{
    [SerializeField] EnemySpawner enemySpawner;
    [SerializeField] ClearPanel clearPanel;
    [SerializeField] GameObject waveButton;     // 시작버튼
    [SerializeField] Image timerImage;          // 시작버튼 타이머
    [SerializeField] int waveOrder = 0;         // 몇 번째 웨이브인가
    [SerializeField] float waveTime = 0;        // 다음 웨이브까지 대기시간
    [SerializeField] float spawnRate = 1.5f;    // 몬스터 스폰 간격

    public int enemyType;                   // 생성되는 몬스터의 종류
    public int[,] enemySerialNumber;        // 몬스터 시리얼 넘버
    public int[,] enemyNumber;              // 몬스터 생성 수

    public bool waitSpawn = true;

    int startWave;
    public string[] waveStrings;

    int remainTime; // 남은 시간만큼 골드 추가

    private void Start()
    {
        int stage = ScoreManager.Instance.stage;
        int round = ScoreManager.Instance.round;
        enemySerialNumber = new int[5, enemySpawner.prefab.Length];
        enemyNumber = new int[5, enemySpawner.prefab.Length];
        startWave = (stage * 6 * 5) + (round * 5); // 스테이지 * 6라운드 * 5웨이브 + 라운드 * 5웨이브
    }

    private void Update()
    {
        if (ScoreManager.Instance.hp <= 0)   // 플레이어의 체력이 0이 될 경우
        {
            StopAllCoroutines();    // 모든 코루틴 중지

            clearPanel.gameClear = false;   // 클리어 실패
            clearPanel.ShowScore();

            return;
        }

        if (waitSpawn == true)
            return;

        waveTime -= Time.deltaTime;

        // if (waveTime <= 20)
        if (waveTime <= 30)
        {
            timerImage.fillAmount = waveTime / 20;
            waveButton.SetActive(true);
        }

            if (waveTime >= 0)
            return;

        if (waveTime <= 0)
        {
            waveButton.SetActive(false);
            WaveStart();
        }
    }

    public void StartButton()
    {
        waveButton.SetActive(false);

        waitSpawn = false;
        remainTime = (int)waveTime;
        ScoreManager.Instance.BonusGold(remainTime);
        waveTime = 0;
    }
    public void PauseButton()
    {
        switch(Time.timeScale)
        {
            case 0:
                Time.timeScale = 1;
                break;
            case 1:
                Time.timeScale = 0;
                break;
        }
    }

    public void WaveStart()
    {
        for(int wave = 0; wave < 5; wave++)
        {
            waveStrings = waveEnemy[startWave + wave].Split(',');   // 웨이브 불러오기
            enemyType = int.Parse(waveStrings[0]);                  // 웨이브마다 생성되는 몬스터의 종류

            for (int i = 0; i < enemyType; i++)
            {
                enemySerialNumber[wave, i] = int.Parse(waveStrings[(i * 2) + 1]);
                enemyNumber[wave, i] = int.Parse(waveStrings[((i + 1) * 2)]);
            }
        }
        
        StartCoroutine(Wave());
    }

    public IEnumerator Wave()
    {
        waitSpawn = true;       // update stop

        if (waveOrder < 5)          // total of 5 waves
        {
            for (int i = 0; i < enemyType; i++)
            {
                // howEnemy, howMany, howTime
                yield return StartCoroutine(enemySpawner.IESpawn(enemySerialNumber[waveOrder, i], enemyNumber[waveOrder, i], spawnRate));
            }

            waveOrder++;
            waitSpawn = false;      // update start
            waveTime = 30;          // 30s Initialization
        }
        else
        {
            while(true)
            {
                yield return 0;

                if(enemySpawner.transform.childCount == 0)  // (field Enemy == 0) = clear
                {
                    Debug.Log("클리어");

                    ScoreManager.Instance.SaveScore();

                    yield return new WaitForSeconds(3.0f);

                    clearPanel.ShowScore();

                    break;
                }
            }
        }
    }



    public string[] waveEnemy = // enemyType, enemySirialNumber1, enemyNumber1, SirialNumber2, Number2 ....
        {
        "1,0,5", // Desert 1round 1wave
        "1,1,5", // Desert 1round 2wave
        "2,0,6,1,4", // Desert 1round 3wave
        "2,0,10,1,8", // Desert 1round 4wave
        "2,0,15,1,10", // Desert 1round 5wave
        "1,2,5", // Desert 2round 1wave
        "1,3,5", // Desert 2round 2wave
        "2,2,6,3,4", // Desert 2round 3wave
        "2,2,10,3,8", // Desert 2round 4wave
        "2,2,15,3,10", // Desert 2round 5wave
        "1,4,5", // Desert 3round 1wave
        "1,5,5", // Desert 3round 2wave
        "2,4,6,5,4", // Desert 3round 3wave
        "2,4,10,5,8", // Desert 3round 4wave
        "2,6,10,7,8", // Desert 3round 5wave
        "1,4,6", // Desert 4round 1wave
        "2,4,6,5,3", // Desert 4round 2wave
        "1,6,16", // Desert 4round 3wave
        "1,8,1", // Desert 4round 4wave
        "2,7,3,8,1", // Desert 4round 5wave
        "2,0,6,1,6", // Desert 5round 1wave
        "3,2,5,3,6,4,7", // Desert 5round 2wave
        "3,4,8,5,6,4,8", // Desert 5round 3wave
        "3,6,8,7,6,6,8", // Desert 5round 4wave
        "1,8,2", // Desert 5round 5wave
        "3,1,4,0,8,3,4", // Desert 6round 1wave
        "4,0,6,2,6,4,6,6,4", // Desert 6round 2wave
        "2,4,6,5,10", // Desert 6round 3wave
        "2,6,10,8,1", // Desert 6round 4wave
        "2,7,6,8,2", // Desert 6round 5wave

        "1,0,5", // Forest 1round 1wave
        "1,1,5", // Forest 1round 2wave
        "2,0,6,1,4", // Forest 1round 3wave
        "2,0,10,1,8", // Forest 1round 4wave
        "2,0,15,1,10", // Forest 1round 5wave
        "1,2,5", // Forest 2round 1wave
        "1,3,5", // Forest 2round 2wave
        "2,2,6,3,4", // Forest 2round 3wave
        "2,2,10,3,8", // Forest 2round 4wave
        "2,2,15,3,10", // Forest 2round 5wave
        "1,4,5", // Forest 3round 1wave
        "1,5,5", // Forest 3round 2wave
        "2,4,6,5,4", // Forest 3round 3wave
        "2,4,10,5,8", // Forest 3round 4wave
        "2,6,10,7,8", // Forest 3round 5wave
        "1,4,6", // Forest 4round 1wave
        "2,4,6,5,3", // Forest 4round 2wave
        "1,6,16", // Forest 4round 3wave
        "1,8,1", // Forest 4round 4wave
        "2,7,3,8,1", // Forest 4round 5wave
        "2,0,6,1,6", // Forest 5round 1wave
        "3,2,5,3,6,4,7", // Forest 5round 2wave
        "3,4,8,5,6,4,8", // Forest 5round 3wave
        "3,6,8,7,6,6,8", // Forest 5round 4wave
        "1,8,2", // Forest 5round 5wave
        "3,1,4,0,8,3,4", // Forest 6round 1wave
        "4,0,6,2,6,4,6,6,4", // Forest 6round 2wave
        "2,4,6,5,10", // Forest 6round 3wave
        "2,6,10,8,1", // Forest 6round 4wave
        "2,7,6,8,2", // Forest 6round 5wave

        "1,0,5", // Winter 1round 1wave
        "1,1,5", // Winter 1round 2wave
        "2,0,6,1,4", // Winter 1round 3wave
        "2,0,10,1,8", // Winter 1round 4wave
        "2,0,15,1,10", // Winter 1round 5wave
        "1,2,5", // Winter 2round 1wave
        "1,3,5", // Winter 2round 2wave
        "2,2,6,3,4", // Winter 2round 3wave
        "2,2,10,3,8", // Winter 2round 4wave
        "2,2,15,3,10", // Winter 2round 5wave
        "1,4,5", // Winter 3round 1wave
        "1,5,5", // Winter 3round 2wave
        "2,4,6,5,4", // Winter 3round 3wave
        "2,4,10,5,8", // Winter 3round 4wave
        "2,6,10,7,8", // Winter 3round 5wave
        "1,4,6", // Winter 4round 1wave
        "2,4,6,5,3", // Winter 4round 2wave
        "1,6,16", // Winter 4round 3wave
        "1,8,1", // Winter 4round 4wave
        "2,7,3,8,1", // Winter 4round 5wave
        "2,0,6,1,6", // Winter 5round 1wave
        "3,2,5,3,6,4,7", // Winter 5round 2wave
        "3,4,8,5,6,4,8", // Winter 5round 3wave
        "3,6,8,7,6,6,8", // Winter 5round 4wave
        "1,8,2", // Winter 5round 5wave
        "3,1,4,0,8,3,4", // Winter 6round 1wave
        "4,0,6,2,6,4,6,6,4", // Winter 6round 2wave
        "2,4,6,5,10", // Winter 6round 3wave
        "2,6,10,8,1", // Winter 6round 4wave
        "2,7,6,8,2", // Winter 6round 5wave
    };

}

enum EnemyType
{
    WalkMushroomSmile,  // 0
    RunMushroomSmile,   // 1
    WalkMushroomAngry,  // 2
    RunMushroomAngry,   // 3
    WalkGrunt,          // 4
    RunGrunt,           // 5
    WalkCactus,         // 6
    RunCatus,           // 7
    Golem               // 8
}
