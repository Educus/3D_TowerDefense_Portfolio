using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TurretShop : MonoBehaviour
{
    [SerializeField] Turret[] turretPrefab;
    [SerializeField] Turret createTurret;
    [SerializeField] Vector3[] height;

    int nowGold;
    int buyGold;

    Vector3 nowMousePosition;
    Vector3 mousePosition;

    private void Start()
    {
        height = new Vector3[turretPrefab.Length];

        height[0] = new Vector3(0, 0.7f, 0);
        height[1] = new Vector3(0, 1f, 0);
        height[2] = new Vector3(0, 0.7f, 0);
    }

    void Update()
    {
        if (createTurret != null)
        {
            mousePosition = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(mousePosition);
            Plane xy = new Plane(Vector3.up, new Vector3(0, 0, 0));
            float distance;
            xy.Raycast(ray, out distance);
            nowMousePosition = ray.GetPoint(distance);

            createTurret.transform.position = nowMousePosition + new Vector3(0, 0.7f, 0);

            if (Input.GetMouseButtonDown(0))
            {
                createTurret.ActiveTurret();
                createTurret = null;
            }
        }
    }

    [Tooltip("0 : basic, 1:cannon, 2:slow")]
    public void CreateTurret(int value)
    {
        nowGold = ScoreManager.Instance.gold;

        switch(value)
        {
            case 0:
                buyGold = 10;
                break;
            case 1:
                buyGold = 30;
                break;
            case 2:
                buyGold = 5;
                break;
        }

        if (nowGold < buyGold) return;

        ScoreManager.Instance.gold -= buyGold;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition + new Vector3(0,-40,0));

        createTurret = Instantiate(turretPrefab[value]);
    }
}
