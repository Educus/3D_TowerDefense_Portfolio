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

    public void SelectRound()
    {
        // ���� ����
        // ��ư���� child ���°���� �޾Ƽ� ���� ���� ���� = ?
        // �ȵ� ��� ��ȣ �ޱ�
        // selectStage�� �ٲٱ�
        // Ÿ��Ʋ���� ���� �������ִ� �ý��� �����
    }

}
