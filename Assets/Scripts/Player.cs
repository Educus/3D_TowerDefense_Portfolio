using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int life = 5;

    private void OnEnable()
    {
        InputManager.Instance.keyAction += OnKeyUpdate;
    }
    void Start()
    {
        
    }
    void OnKeyUpdate()
    {
        if (life <= 0) return;

        if (Input.GetMouseButtonDown(0))        // ��ž ����
        {
        }
        else if (Input.GetMouseButtonDown(1))   // ��ž ���� �˾�
        {
        }
    }
    void Update()
    {
        
    }
    private void OnDisable()
    {
        InputManager.Instance.keyAction -= OnKeyUpdate;
    }
}
