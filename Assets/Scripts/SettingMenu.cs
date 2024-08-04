using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingMenu : MonoBehaviour
{
    [SerializeField] TMP_Text exitText;

    private int nowScenes;

    void Start()
    {
        nowScenes = SceneManager.GetActiveScene().buildIndex;

        if (nowScenes == 0)
        {
            exitText.text = "\nExit";
        }
        else
        {
            exitText.text = "\nHome";
        }

        Time.timeScale = 0;
    }

    public void Continue()
    {
        Time.timeScale = 1;
        Destroy(gameObject);
    }

    public void Setting()
    {

    }

    public void Exit()
    {
        Time.timeScale = 1;

        if (nowScenes == 0)
        {

            // Application.Quit();
            UnityEditor.EditorApplication.isPlaying = false;
        }
        else
        {
            StartCoroutine(SceneController.Instance.AsyncLoad(nowScenes - 1));
        }
    }
}
