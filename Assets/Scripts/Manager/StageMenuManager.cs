using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class StageMenuManager : MonoBehaviour
{
    [SerializeField] GameObject stageView;
    [SerializeField] GameObject roundView;
    [SerializeField] TMP_Text roundText;

    void Start()
    {
        stageView.SetActive(true);
        roundView.SetActive(false);
    }

    
    public void SelectStage(int stage)
    {
        ScoreManager.Instance.NowStage(stage);

        roundText.text = ScoreManager.Instance.StageText(stage);

        StartCoroutine(IESelectStage());
    }
    IEnumerator IESelectStage( )    // Stage ���� �� ��ο����� Active ��Ȱ��ȭ �� Round Ȱ��ȭ �� �����
    {
        yield return StartCoroutine(SceneController.Instance.ReFade());
        // �ٸ� �ڷ�ƾ�� �����Ű�� �ش� �ڷ�ƾ�� ���� ������ ���
        bool active = false;
        if (stageView.activeSelf == false)
        {
            active = !active;
        }
            stageView.SetActive(active);
            roundView.SetActive(!active);

        StartCoroutine(SceneController.Instance.FadeIn());
    }
}
