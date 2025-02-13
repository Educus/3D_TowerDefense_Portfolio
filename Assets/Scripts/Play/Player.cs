using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    private void OnEnable()
    {
        InputManager.Instance.keyAction += OnKeyUpdate;
    }
    void OnKeyUpdate()
    {
        if (StateManager.Instance.hp <= 0) return;

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
