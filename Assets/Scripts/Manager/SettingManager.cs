using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingManager : Singleton<SettingManager>
{
    [SerializeField] GameObject prefab; // 설정 프리팹

    public GameObject settingMenu;

    private void Start()
    {
        CursorConfined();
    }

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            SettingMenu();
        }
    }

    public void SettingMenu()
    {
        if(settingMenu != null)
        {
            CursorConfined();

            Destroy(settingMenu);

            return;
        }

        settingMenu = Instantiate(prefab);
        Cursor.lockState = CursorLockMode.None;
    }

    public void CursorConfined()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }

}
