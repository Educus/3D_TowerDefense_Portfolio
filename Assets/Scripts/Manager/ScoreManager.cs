using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
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
    public int totalStage = 3;
    public int totalRound = 6;

    public int stage = -1;
    public int round = -1;
    public int maxHp = 10;
    public int hp = 0;
    public int firstGold = 30;
    public int gold = 0;

    [SerializeField] string[] clearStage;
    public string saveScores = "";                          // 저장용 string
    public string[] score;                                  // 인게임 기록용 string[]
    public int nowScore;                                    // 마지막 게임의 점수

    private void Start()
    {
        clearStage = new string[totalStage];
        score = new string[totalRound];

        if (saveScores == "")
        {
            for (int i = 0; i < totalStage; i++)
            {
                for (int o = 0; o < totalRound; o++)
                {
                    if (o == 0)
                    {
                        clearStage[i] = "0";
                    }
                    else
                    {
                        clearStage[i] += "0";
                    }

                    if (o + 1 != totalRound)
                    {
                        clearStage[i] += ",";
                    }
                }
            }
        }

        ResetHp();
        LoadClearStage();
    }

    public void ResetHp()
    {
        hp = maxHp;
        gold = firstGold;
    }
    public void BonusGold(int time)
    {
        gold += time * 1;
    }

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
        this.stage = stage;
    }

    public void NowRound(int round)
    {
        this.round = round;
    }

    public int BringScore(int needStage, int needRound) // needStage와 needRound로 해당 위치의 점수 가져오기
    {
        score = clearStage[needStage].Split(',');

        return int.Parse(score[needRound]);
    }

    public void SaveScore()
    {
        // 플레이어의 체력 받아오기
        // 플레이어 체력 : max = 3, 70%이상 = 2, 이외 = 1
        // 이전 점수와 비교
        if (hp == maxHp)
            nowScore = 3;
        else if (hp >= maxHp * (0.7))
            nowScore = 2;
        else
            nowScore = 1;

        if (stage == -1 || round == -1) return;

        if (BringScore(stage, round) < nowScore)   // 이번에 얻은 점수가 기록된 점수보다 크다면 저장
        {
            score[round] = nowScore.ToString();

            UpLoadScore();
        }
    }
    public void UpLoadScore()
    {
        for (int o = 0; o < score.Length; o++)
        {
            if (o == 0)
            {
                clearStage[stage] = score[o].ToString();
            }
            else
            {
                clearStage[stage] += score[o].ToString();
            }

            if (o + 1 != score.Length)
            {
                clearStage[stage] += ",";
            }
        }

        SaveClearStage();
    }

    public void SaveClearStage()    // 파일로 저장
    {
        for (int i = 0; i < totalStage; i++)
        {
            if (i == 0)
            {
                saveScores = clearStage[i];
            }
            else
            {
                saveScores += clearStage[i];
            }

            if (i + 1 != totalRound)
            {
                saveScores += ":";
            }
        }

        PlayerPrefs.SetString("Score", saveScores);
    }
    public void LoadClearStage()    // 파일 불러오기
    {
        if (PlayerPrefs.HasKey("Score"))
        {
            saveScores = PlayerPrefs.GetString("Score");

            clearStage = saveScores.Split(':');
        }
    }

    public void HardResetButton()
    {
        for (int i = 0; i < totalStage; i++)
        {
            for (int o = 0; o < totalRound; o++)
            {
                if (o == 0)
                {
                    clearStage[i] = "0";
                }
                else
                {
                    clearStage[i] += "0";
                }

                if (o + 1 != totalRound)
                {
                    clearStage[i] += ",";
                }
            }
        }

        SaveClearStage();
        LoadClearStage();
    }
}
