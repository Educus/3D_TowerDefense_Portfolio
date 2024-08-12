using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class ClearPanel : MonoBehaviour
{
    [SerializeField] TMP_Text stageboard;
    [SerializeField] TMP_Text roundboard;

    [SerializeField] GameObject nextButton1;  // 다음맵이 있을 때
    [SerializeField] GameObject nextButton2;  // 다음맵이 없을 때
    [SerializeField] GameObject star1;
    [SerializeField] GameObject star2;
    [SerializeField] GameObject star3;

    public bool gameClear = true;
    public int score = 0;

    void Start()
    {
        stageboard.text = ScoreManager.Instance.StageText(ScoreManager.Instance.stage);
        roundboard.text = "Stage " + (ScoreManager.Instance.round + 1);
    }

    public void ShowScore()
    {
        gameObject.SetActive(true);

        if (ScoreManager.Instance.round < 5 && gameClear)
        {
            nextButton1.SetActive(true);
            nextButton2.SetActive(false);
        }
        else
        {
            nextButton1.SetActive(false);
            nextButton2.SetActive(true);
        }


        star1.SetActive(false);
        star2.SetActive(false);
        star3.SetActive(false);

        if (gameClear)
            score = ScoreManager.Instance.nowScore;
        else
            score = 0;

        if(score >= 1)
        {
            star1.SetActive(true);
            if (score >= 2)
            {
                star2.SetActive(true);
                if (score >= 3)
                {
                    star3.SetActive(true);
                }
            }
        }
    }

    public void NextButton()
    {
        ScoreManager.Instance.NowRound(++ScoreManager.Instance.round);
        ScoreManager.Instance.ResetHp();
        NewMap(2);
    }
    public void RetryButton()
    {
        ScoreManager.Instance.ResetHp();
        NewMap(2);
    }
    public void HomeButton()
    {
        NewMap(1);
    }

    public void NewMap(int value)
    {
        StartCoroutine(SceneController.Instance.AsyncLoad(value));
    }
}
