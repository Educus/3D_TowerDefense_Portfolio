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
        StartCoroutine(IEStart());
    }

    
    public void SelectStage(int stage)
    {
        if (stage != -1)
        {
            ScoreManager.Instance.NowStage(stage);

            roundText.text = ScoreManager.Instance.StageText(stage);
        }

        StartCoroutine(IESelectStage());
    }

    IEnumerator IEStart()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();

        stageView.SetActive(true);
        roundView.SetActive(false);
    }

    IEnumerator IESelectStage( )    // Stage 선택 시 어두워지고 Active 비활성화 및 Round 활성화 후 밝아짐
    {
        yield return StartCoroutine(SceneController.Instance.ReFade());
        // 다른 코루틴을 실행시키고 해당 코루틴이 끝날 때까지 대기
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
