using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ZTest : MonoBehaviour
{
    string[] score;

    private void Start()
    {
        score = new string[1];
        score[0] = "0";
    }
    private void Update()
    {

        Debug.Log("score : " + int.Parse(this.score[0]));       // 버그

    }
}
