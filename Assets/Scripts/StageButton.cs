using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.SceneManagement;

public class StageButton : MonoBehaviour
{
    [SerializeField] TMP_Text roundText;

    public int roundValue;      // ������ ����

    public int stage;           // ���� ��������
    public int beforeStage = -1;     // ���� ��������
    public string stageText;    // �������� �ؽ�Ʈ

    private void Start()
    {
        roundValue = gameObject.transform.GetSiblingIndex();
    }

    private void Update()
    {
        stage = ScoreManager.Instance.stage;
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
        string stageText = ScoreManager.Instance.StageText(ScoreManager.Instance.stage);

        roundText.text = stageText + " " + (roundValue + 1).ToString();
    }

    public void SelectRound()
    {
        ScoreManager.Instance.NowRound(roundValue);

        Execute();
    }

    public void Execute()
    {
        StartCoroutine(SceneController.Instance.AsyncLoad(2));
    }
}
