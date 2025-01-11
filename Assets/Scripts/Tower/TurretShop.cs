using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class TurretShop : MonoBehaviour
{
    // [SerializeField] Turret[] turretPrefab;
    [SerializeField] List <TurretBase> turretPrefab;
    // [SerializeField] Turret createTurret;
    [SerializeField] TurretBase createTurret;
    [SerializeField] Vector3[] height;

    Ray ray;
    Plane xy;
    float distance;

    Renderer renderer;
    LayerMask mask;
    LayerMask notMask;
    bool hitLayer;
    bool notHitLayer;

    int nowGold;
    int buyGold;
    int turretValue;

    Vector3 nowMousePosition;
    Vector3 mousePosition;

    GameObject unableInstallBox;

    private void Start()
    {
        turretPrefab.Clear();
        turretPrefab.Add(Resources.Load<TurretBase>("BasicsTurret_LV1"));
        turretPrefab.Add(Resources.Load<TurretBase>("CannonTurret"));
        turretPrefab.Add(Resources.Load<TurretBase>("SlowTurret_Lv1"));
        unableInstallBox = Instantiate(Resources.Load<GameObject>("UnableInstallBox"));
        unableInstallBox.SetActive(false);


        // height = new Vector3[turretPrefab.Length];
        height = new Vector3[turretPrefab.Count];
        mask = 1 << LayerMask.NameToLayer("TowerMap") ;
        notMask = 1 << LayerMask.NameToLayer("Turret");

        height[0] = new Vector3(0, 0.75f, 0);
        height[1] = new Vector3(0, 1.25f, 0);
        height[2] = new Vector3(0, 0.9f, 0);
    }

    void Update()
    {
        if (createTurret != null)
        {
            mousePosition = Input.mousePosition;
            ray = Camera.main.ScreenPointToRay(mousePosition);
            xy = new Plane(Vector3.up, new Vector3(0, 0, 0));
            xy.Raycast(ray, out distance);
            nowMousePosition = ray.GetPoint(distance);

            nowMousePosition.x = Mathf.Floor(nowMousePosition.x) + 0.5f;
            nowMousePosition.z = Mathf.Floor(nowMousePosition.z) + 0.5f;

            createTurret.transform.position = nowMousePosition + height[turretValue];

            hitLayer = Physics.Raycast(nowMousePosition + new Vector3(0, 5, 0), Vector3.down, 10f, mask);
            notHitLayer = Physics.Raycast(nowMousePosition + new Vector3(0, 5, 0), Vector3.down, 10f, notMask);
            renderer = createTurret.GetComponent<Renderer>();

            // unableInstallBox = createTurret.transform.Find("UnableInstallBox");

            if (hitLayer && !notHitLayer)
            {
                unableInstallBox.gameObject.SetActive(false);

                if (Input.GetMouseButtonDown(0))
                {
                    createTurret.ActiveTurret();
                    createTurret.gameObject.layer = 8;      // layer : Turret으로 변경
                    createTurret = null;
                }
            }
            else
            {
                unableInstallBox.gameObject.SetActive(true);
                unableInstallBox.transform.position = createTurret.transform.position;
            }
        }
    }

    [Tooltip("0 : basic, 1:cannon, 2:slow")]
    public void CreateTurret(int value)
    {
        nowGold = StateManager.Instance.gold;

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

        StateManager.Instance.gold -= buyGold;
        // mousePosition = Camera.main.ScreenToWorldPoint(mousePosition + new Vector3(0,-40,0));

        createTurret = Instantiate(turretPrefab[value]);
        turretValue = value;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(nowMousePosition + new Vector3(0, 5, 0), Vector3.down * 10f);
    }
}
