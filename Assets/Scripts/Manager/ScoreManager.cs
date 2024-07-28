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

    [SerializeField] string[] clearStage = new string[totalStage];

    public int stage = -1;
    public int round = -1;
    public string[] score;

    private void Start()
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

    public void SaveScore(int score)
    {
        if (stage == -1 || round == -1) return;

        this.score = clearStage[stage].Split(',');
        this.score[round] = score.ToString();

        UpLoadScore();
    }
    public void UpLoadScore()
    {
        if (stage == -1 || round == -1) return;

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
    }

    public void ScoreBoard()        // ?
    {

    }

    public void SaveClearStage()    // 파일로 저장
    {

    }
    public void LoadClearStage()    // 파일 불러오기
    {

    }
}
