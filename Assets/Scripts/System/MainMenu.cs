using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void Execute()
    {
        StartCoroutine(SceneController.Instance.AsyncLoad(1));
    }

    public void HardReset() // 추가 할 것
    {

    }
}
