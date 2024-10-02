using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButton : MonoBehaviour
{
    [SerializeField] GameObject pauseText;

    private void Update()
    {
        if(pauseText.activeSelf == false)
            return;
        
        if(Time.timeScale == 1)
            pauseText.SetActive(false);
    }

    public void Pause()
    {
        switch (Time.timeScale)
        {
            case 0:
                Time.timeScale = 1;
                break;
            case 1:
                Time.timeScale = 0;
                pauseText.SetActive(true);
                break;
        }
    }
}
