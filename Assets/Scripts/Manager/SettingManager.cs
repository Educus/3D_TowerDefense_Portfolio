using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingManager : Singleton<SettingManager>
{
    [SerializeField] GameObject prefab; // 설정 프리팹

    public GameObject settingMenu;
    public SettingMenu menu;

    private void Start()
    {
        CursorConfined();
    }

    private void Update()
    {
        if (settingMenu == null && Cursor.lockState == CursorLockMode.None)
        {
            CursorConfined();
        }

        if(Input.GetKeyUp(KeyCode.Escape))
        {
            SettingMenu();
        }
    }

    public void SettingMenu()
    {
        if (settingMenu != null)
        {
            CursorConfined();

            if (menu != null)
            {
                menu.Continue();
            }

            return;
        }

        settingMenu = Instantiate(prefab);
        menu = settingMenu.GetComponent<SettingMenu>();

        Cursor.lockState = CursorLockMode.None;
    }

    public void CursorConfined()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }

}
