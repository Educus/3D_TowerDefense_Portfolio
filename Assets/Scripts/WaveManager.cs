using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] EnemySpawner enemySpawner;
    [SerializeField] GameObject waveButton;     // ���̺� ���۽� ��ư�� ��Ȱ��ȭ, ���̺� ����� Ȱ��ȭ
    [SerializeField] int waveOrder = 0;         // �� ��° ���̺�         // ������ 
    [SerializeField] float waveTime = 0;        // ���̺� �ð�           // ������
    [SerializeField] float spawnRate = 1.5f;    // ���� ����             // ������
    [SerializeField] int enemyType;             // �����Ǵ� ���� ����                   // ����
    [SerializeField] int[,] enemySerialNumber;     // ������ ������ ��ȣ[���̺�,����]    // ����
    [SerializeField] int[,] enemyNumber;           // ���� ��[���̺�,����]              // ����

    public bool waitSpawn = true;

    public int stage;
    public int round;
    public string[] waveStrings;

    private void Start()
    {
        stage = ScoreManager.Instance.stage;
        round = ScoreManager.Instance.round;
    }

    private void Update()
    {
        if (waitSpawn == true)
            return;

        waveTime -= Time.deltaTime;

        if(waveTime < 15)
            waveButton.SetActive(true);

        if(waveTime > 0)
            return;

        if (waveTime < 0)
        {
            waveButton.SetActive(false);
            WaveStart();
        }
    }

    public void StartButton()
    {
        waveButton.SetActive(false);

        waitSpawn = false;
        waveTime = 0;
    }
    public void WaveStart() // �Ұ�
    {
        // ���̺� ���� �������� ���͸� ��ȯ�� ���̸�, � ���͸� ��� ��ȯ�� ���ΰ��� for������
        enemyType = 1;
        enemySerialNumber[0,0] = 1;
        enemyNumber[0,0] = 10;

        for(int wave = 0; wave < 5; wave++)
        {
            for (int i = 0; i < enemyType; i++)
            {
                waveStrings = waveEnemy[i].Split(','); ;

                enemySerialNumber[wave, i] = int.Parse(waveStrings[i]); // �̾��ϱ�
            }

        }
        
        StartCoroutine(Wave());
    }

    public IEnumerator Wave()
    {
        waitSpawn = true;       // ������Ʈ ����

        for (int i = 0; i < enemyType; i++)
        {
            // ����̺꿡 � ���͸�, �󸶳�, ���ʸ��� ��ȯ�� ������
            yield return StartCoroutine(enemySpawner.IESpawn(enemySerialNumber[waveOrder, i], enemyNumber[waveOrder, i], spawnRate));
        }

        if (waveOrder < 5)          // �ִ� 5 ���̺�
        {
            waveOrder++;
            waitSpawn = false;      // ������Ʈ Ȱ��ȭ
            waveTime = 30;          // 30�� ���Ŀ� ���̺� ����
        }
        else
        {
            // ���� ���̻� ���� ���� ���� ���
            // Ŭ����
            Debug.Log("Clear");
        }
    }



    public string[] waveEnemy = // 
        {
        "1,2,3,4,5", // Desert 1round 1wave
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
