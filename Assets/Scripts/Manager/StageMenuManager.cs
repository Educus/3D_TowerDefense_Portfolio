using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

enum MapStage
{
    Desert,
    Forest,
    Winter
}

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

    void Update()
    {
        
    }

    MapStage IntToMapStage(int stage)
    {
        return (MapStage)stage;
    }
    public void SelectStage(int stage)
    {
        MapStage mapStage = IntToMapStage(stage);
        roundText.text = mapStage.ToString();

        StartCoroutine(IESelectStage());
    }
    IEnumerator IESelectStage()
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

    public void SelectRound()
    {
        // 만들 예정
        // 버튼으로 child 몇번째인지 받아서 다음 라운드 이행 = ?
        // 안될 경우 번호 받기
        // selectStage도 바꾸기
        // 타이틀에서 점수 저장해주는 시스템 만들기
    }

}
