using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StageButton : MonoBehaviour
{
    [SerializeField] TMP_Text roundText;

    public int roundValue;      // 선택한 라운드

    public int stage;           // 현제 스테이지
    public int beforeStage = -1;     // 이전 스테이지
    public string stageText;    // 스테이지 텍스트

    private void Start()
    {
        roundValue = gameObject.transform.GetSiblingIndex();
    }

    private void Update()
    {
        stage = ScoreManager.Instance.stage[0];
        if (stage == beforeStage)
        {
            return;
        }
        else
        {
            beforeStage = stage;
            ChangingText();
        }
    }

    public void ChangingText()
    {
        string stageText = ScoreManager.Instance.StageText(ScoreManager.Instance.stage[0]);

        roundText.text = stageText + " " + (roundValue + 1).ToString();
    }

    public void SelectRound()
    {
        ScoreManager.Instance.NowRound(roundValue);
        ScoreManager.Instance.ResetHp();

        Execute();
    }

    public void Execute()
    {
        StartCoroutine(SceneController.Instance.AsyncLoad(2));
    }
}
