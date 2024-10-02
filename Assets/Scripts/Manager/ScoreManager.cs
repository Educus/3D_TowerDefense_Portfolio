using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum MapStage
{
    Desert,
    Forest,
    Winter
}

public class ScoreManager : Singleton<ScoreManager>
{
    // static?
    public int totalStage = 3;  // 총 스테이지
    public int totalRound = 6;  // 총 라운드

    public int[] stage = new int[]{ -1, -1 };

    public List<List<int>> clearStages = new List<List<int>>(); // 점수 저장용
    // [SerializeField] string[] clearStage;
    // public string saveScores = "";                          // 저장용 string
    // public string[] score;                                  // 인게임 기록용 string[]
    public int nowScore;                                    // 마지막 게임의 점수

    private void Start()
    {
        stage = new int[] { -1, -1 };
        // list 초기화
        for (int i = 0; i < totalStage; i++)
        {
            clearStages.Add(new List<int>());

            for(int j = 0; j < totalRound; j++)
            {
                clearStages[i].Add(0);
            }
        }

        LoadClearStage();
    }

    private void Update()
    {
        // 치트키
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.Q)) Cheat();
    }

    // stage에 따른 맵 이름
    public string StageText(int stage)
    {
        MapStage mapStage = IntToMapStage(stage);

        return mapStage.ToString();
    }
    private MapStage IntToMapStage(int stage)
    {
        return (MapStage)stage;
    }

    public void NowStage(int stage)
    {
        this.stage[0] = stage;
    }

    public void NowRound(int round)
    {
        this.stage[1] = round;
    }

    public int BringScore(int needStage, int needRound) // needStage와 needRound로 해당 위치의 점수 가져오기
    {
        // score = clearStage[needStage].Split(',');

        return clearStages[needStage][needRound];
    }

    // 점수 받아와서 비교하기
    public void SaveScore()
    {
        // 플레이어의 체력 받아오기
        // 플레이어 체력 : max = 3, 70%이상 = 2, 이외 = 1
        // 저장된 이전 점수와 비교
        if (StateManager.Instance.hp == StateManager.maxHp)
            nowScore = 3;
        else if (StateManager.Instance.hp >= StateManager.maxHp * (0.7))
            nowScore = 2;
        else
            nowScore = 1;

        if (stage[0] == -1 || stage[1] == -1) return;

        if (clearStages[stage[0]][stage[1]] < nowScore)   // 이번에 얻은 점수가 기록된 점수보다 크다면 저장
        {
            clearStages[stage[0]][stage[1]] = nowScore;

            SaveClearStage();
        }
    }

    // 파일로 저장
    public void SaveClearStage()
    {
        string score;

        for (int i = 0; i < totalStage; i++)
        {
            string saveName = "Score" + i;

            score = string.Join(',', clearStages[i]);

            PlayerPrefs.SetString(saveName, score);
        }
    }

    // 파일 불러오기
    public void LoadClearStage()
    {
        string[] score = new string[] {};

        for (int i = 0; i < totalStage; i++)
        {
            string saveName = "Score" + i;

            if (PlayerPrefs.HasKey(saveName))
            {
                score = PlayerPrefs.GetString(saveName).Split(',');

                clearStages[i].Clear();

                foreach (var value in score)
                {
                    clearStages[i].Add(int.Parse(value));
                }
            }
        }
    }

    // 완전 초기화
    public void HardReset()
    {
        clearStages.Clear();

        for (int i = 0; i < totalStage; i++)
        {
            clearStages.Add(new List<int>());

            for (int j = 0; j < totalRound; j++)
            {
                clearStages[i].Add(0);
            }
        }

        SaveClearStage();
        LoadClearStage();
    }

    // 실험용 치트키 - 나중에 삭제
    public void Cheat()
    {
        for (int i = 0; i < totalStage; i++)
        {
            for (int o = 0; o < totalRound; o++)
            {
                clearStages[i][o] = 2;
            }
        }

        SaveClearStage();
        LoadClearStage();
    }
}
