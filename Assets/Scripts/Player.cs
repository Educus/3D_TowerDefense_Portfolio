using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    // 바꿀 예정
    /*
    public static int maxHp = 10;
    public static int firstGold = 30;

    public int hp = 0;
    public int gold = 0;


    public void ResetHp()
    {
        hp = maxHp;
        gold = firstGold;
    }
    public void BonusGold(int time)
    {
        gold += time * 1;
    }
    */


    private void OnEnable()
    {
        InputManager.Instance.keyAction += OnKeyUpdate;
    }
    void OnKeyUpdate()
    {
        if (ScoreManager.Instance.hp <= 0) return;

        if (Input.GetMouseButtonDown(0))        // 포탑 선택
        {
        }
        else if (Input.GetMouseButtonDown(1))   // 포탑 설명 팝업
        {
        }
    }
    private void OnDisable()
    {
        InputManager.Instance.keyAction -= OnKeyUpdate;
    }
}
