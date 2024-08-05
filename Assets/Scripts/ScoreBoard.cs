using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreBoard : MonoBehaviour
{
    [SerializeField] TMP_Text hpText;
    [SerializeField] TMP_Text goldText;

    void Update()
    {
        hpText.text = ScoreManager.Instance.hp.ToString();
        goldText.text = ScoreManager.Instance.gold.ToString();
    }
}
