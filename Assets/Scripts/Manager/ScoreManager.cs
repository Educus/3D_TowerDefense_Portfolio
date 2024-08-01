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
    public static int totalStage = 3;
    public static int totalRound = 6;

    public int stage = -1;
    public int round = -1;
    public int hp = 0;

    [SerializeField] string[] clearStage = new string[totalStage];
    private string saveScores = null;                       // 저장용 string
    public string[] score;                                  // 인게임 기록용 string[]
    public int nowScore;                                    // 마지막 게임의 점수

    private void Start()
    {
        ResetHp();

        LoadClearStage();

        if (saveScores == null)
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
    }

    public void ResetHp()
    {
        hp = 5;
    }

    public string StageText(int stage)
    {
        MapStage mapStage = IntToMapStage(stage);

        return mapStage.ToString();
    }

    MapStage IntToMapStage(int stage)
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

    public void SaveScore()
    {
        // 플레이어의 체력 받아오기
        // 플레이어 체력 : max = 3, 70%이상 = 2, 이외 = 1
        // 이전 점수와 비교
        int hp = 50;

        if(hp >= 60)
            nowScore = 3;
        else if(hp >= 60*(0.7))
            nowScore = 2;
        else
            nowScore = 1;

        if (stage == -1 || round == -1) return;

        this.score = clearStage[stage].Split(',');

        if (int.Parse(this.score[round]) < nowScore)   // 이번에 얻은 점수가 기록된 점수보다 크다면 저장
        {
            this.score[round] = nowScore.ToString();

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
        if(PlayerPrefs.HasKey("Score"));
        {
            saveScores = PlayerPrefs.GetString("Score");

            clearStage = saveScores.Split(':');
        }
    }
}
