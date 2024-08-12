using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnLockManager : MonoBehaviour
{
    [SerializeField] GameObject stageLockSystem;
    [SerializeField] GameObject roundLockSystem;
    [SerializeField] GameObject getStarSystem;
    [SerializeField] GameObject stageViewParent;
    [SerializeField] GameObject roundParent;
    [SerializeField] GameObject roundViewParent;

    [SerializeField] Vector3[] stagePosition;

    [SerializeField] RectTransform targetRectTransform;
    [SerializeField] RectTransform myRectTransform;

    public GameObject[] stageLocks;
    public GameObject[] roundLocks;
    public GameObject[] getStar;

    public int totalStage;
    public int totalRound;

    public bool active;

    private void Start()
    {

        stagePosition = new Vector3[3];
        stagePosition[0] = new Vector3(480, 0, 0);
        stagePosition[1] = new Vector3(960, 540, 0);
        stagePosition[2] = new Vector3(1440, 540, 0);

        totalStage = ScoreManager.Instance.totalStage;
        totalRound = ScoreManager.Instance.totalRound;

        stageLocks = new GameObject[totalStage];
        roundLocks = new GameObject[totalRound];
        getStar = new GameObject[totalRound];

        StartCoroutine(IECreate());
    }

    void Update()
    {
        if (stageViewParent.activeSelf == true)
            active = true;
        else
            active = false;

        transform.Find("StageLocks").gameObject.SetActive(active);


        if (roundParent.activeSelf == true)
            active = true;
        else
            active = false;
     
        transform.Find("RoundLocks").gameObject.SetActive(active);
        transform.Find("GetStars").gameObject.SetActive(active);
    }

    IEnumerator IECreate()
    {
        yield return new WaitForEndOfFrame();

        StageLock();
        RoundLock();
        GetStar();

        for (int i = 0; i < totalStage - 1; i++)
        {
            if (ScoreManager.Instance.BringScore(i, totalRound - 1) == 0)
                stageLocks[i + 1].SetActive(true);
            else
                stageLocks[i + 1].SetActive(false);


        }
        
    }

    public void StageLock()
    {
        for (int i = 0; i < totalStage; i++)
        {
            if (i != 0)
            {
                stageLocks[i] = Instantiate(stageLockSystem, transform.Find("StageLocks"));
                // stageLocks[i].transform.position = stageViewParent.transform.GetChild(i).transform.position;
                // 좌표가 이상하게 나옴
                stageLocks[i].transform.position = stagePosition[i];
            }
        }
    }

    public void RoundLock()
    {
        for (int i = 0; i < totalRound; i++)
        {
            if (i != 0)
            {
                roundLocks[i] = Instantiate(roundLockSystem, transform.Find("RoundLocks"));
                // targetRectTransform = roundViewParent.transform.GetChild(i).GetComponent<RectTransform>();
                // myRectTransform = roundLocks[i].GetComponent<RectTransform>();
                // myRectTransform.anchoredPosition = targetRectTransform.anchoredPosition;
                roundLocks[i].transform.position = roundViewParent.transform.GetChild(i).transform.position;
            }
        }
    }

    public void GetStar()
    {
        for (int i = 0; i < totalRound; i++)
        {
            getStar[i] = Instantiate(getStarSystem, transform.Find("GetStars"));
            getStar[i].transform.position = roundViewParent.transform.GetChild(i).transform.position + new Vector3(-125, 125, 0);
        }
    }

    public void buttonChange(int stage)
    {
        for (int i = 0; i < totalRound - 1; i++)
        {
            if(ScoreManager.Instance.BringScore(stage, i) == 0)
            {
                roundLocks[i + 1].SetActive(true);
            }
            else
            {
                roundLocks[i + 1].SetActive(false);
            }
        }

        for (int i = 0; i < totalRound; i++)
        {
            if (ScoreManager.Instance.BringScore(stage, i) == 3)
                getStar[i].SetActive(true);
            else
                getStar[i].SetActive(false);

        }
    }
}
