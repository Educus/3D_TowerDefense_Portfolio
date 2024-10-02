using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using Unity.VisualScripting;
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
    WaveDB waveDB;

    public int enemyType;                   // 생성되는 몬스터의 종류
    public int[,] enemySerialNumber;        // 몬스터 시리얼 넘버
    public int[,] enemyNumber;              // 몬스터 생성 수

    public bool waitSpawn = true;

    int startWave;
    public string[] waveStrings;

    int remainTime; // 남은 시간만큼 골드 추가

    private void Start()
    {
        waveDB = gameObject.GetComponent<WaveDB>();

        int stage = ScoreManager.Instance.stage[0];
        int round = ScoreManager.Instance.stage[1];
        enemySerialNumber = new int[5, enemySpawner.prefab.Length];
        enemyNumber = new int[5, enemySpawner.prefab.Length];
        startWave = (stage * 6 * 5) + (round * 5); // 스테이지 * 6라운드 * 5웨이브 + 라운드 * 5웨이브
    }

    private void Update()
    {
        if (StateManager.Instance.hp <= 0)   // 플레이어의 체력이 0이 될 경우
        {
            StopAllCoroutines();    // 모든 코루틴 중지

            clearPanel.gameClear = false;   // 클리어 실패
            clearPanel.ShowScore();

            return;
        }

        if (waitSpawn == true)
            return;

        if(waveOrder >= 5)
        {
            StartCoroutine(Wave());

            return;
        }

        waveTime -= Time.deltaTime;

        if (waveTime <= 20)
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
        StateManager.Instance.BonusGold(remainTime);
        waveTime = 0;
    }
    

    public void WaveStart()
    {
        if (waveOrder != 0)
            StateManager.Instance.gold += 20;

        for (int wave = 0; wave < 5; wave++)
        {
            waveStrings = waveDB.waveEnemy[startWave + wave].Split(',');   // 웨이브 불러오기
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
            waveTime = 25;          // 30s Initialization
        }
        else
        {
            while(true)
            {
                yield return 0;

                if(enemySpawner.transform.childCount == 0)  // (field Enemy == 0) = clear
                {
                    ScoreManager.Instance.SaveScore();

                    yield return new WaitForSeconds(2.0f);

                    clearPanel.gameClear = true;
                    clearPanel.ShowScore();

                    break;
                }
            }
        }
    }
}