using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnLockManager : MonoBehaviour
{
    [SerializeField] GameObject stageLockSystem;
    [SerializeField] GameObject roundLockSystem;
    [SerializeField] GameObject stageViewParent;
    [SerializeField] GameObject roundParent;
    [SerializeField] GameObject roundViewParent;

    [SerializeField] Vector3[] stagePosition;

    public GameObject[] stageLocks;
    public GameObject[] roundLocks;

    public int totalStage;
    public int totalRound;

    public bool active;

    private void Start()
    {
        stagePosition = new Vector3[3];
        stagePosition[0] = new Vector3(466.6667f, 0, 0);
        stagePosition[1] = new Vector3(960, 540, 0);
        stagePosition[2] = new Vector3(1440, 540, 0);

        totalStage = ScoreManager.Instance.totalStage;
        totalRound = ScoreManager.Instance.totalRound;

        stageLocks = new GameObject[totalStage];
        roundLocks = new GameObject[totalRound];

        stageLock();
        roundLock();
    }

    void Update()
    {
        if (stageViewParent.activeSelf == true)
            active = true;
        else
            active = false;

        for (int i = 0; i < stageLocks.Length; i++)
        {
            if (stageLocks[i] != null)
                stageLocks[i].SetActive(active);
        }


        if (roundParent.activeSelf == true)
            active = true;
        else
            active = false;

        for (int i = 0; i < roundLocks.Length; i++)
        {
            if (roundLocks[i] != null)
                roundLocks[i].SetActive(active);
        }
    }

    public void stageLock()
    {
        for (int i = 0; i < totalStage; i++)
        {
            if (i != 0)
            {
                stageLocks[i] = Instantiate(stageLockSystem, transform);
                // stageLocks[i].transform.position = stageViewParent.transform.GetChild(i).transform.position;
                stageLocks[i].transform.position = stagePosition[i];
            }
        }
    }

    public void roundLock()
    {
        for (int i = 0; i < totalRound; i++)
        {
            if (i != 0)
            {
                roundLocks[i] = Instantiate(roundLockSystem, transform);
                roundLocks[i].transform.position = roundViewParent.transform.GetChild(i).transform.position;
            }
        }
    }
}
