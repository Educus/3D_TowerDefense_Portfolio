using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCamera : MonoBehaviour
{
    [SerializeField] Camera camera;
    [SerializeField] float planeScale;
    [SerializeField] float height;  // 카메라 높이
    [SerializeField] float speed;   // 카메라 스피드

    private float cameraAngle;   // 카메라 시야각          // 65도 고정
    private float cameraLeftSize;      // 카메라의 왼쪽 길이
    private float cameraUpSize;        // 카메라의 위쪽 길이
    private float cameraDownSize;      // 카메라의 아래쪽 길이

    private float maxX;          // 카메라의 최대 가로 길이
    private float minX;          // 카메라의 최소 가로 길이
    private float maxZ;          // 카메라의 최대 세로 길이
    private float minZ;          // 카메라의 최소 세로 길이

    private Vector2 mousePosition;       // 마우스의 현재 좌표(읽기 전용)
    private float zoomAmount;    // 마우스 휠
    private float zoomSpeed;    // 마우스 휠 스피드

    private float screenX;       // 화면 해상도 가로
    private float screenY;       // 화면 해상도 세로(기준)
    private float screenRatio;         // 가로 세로 비율

    void Start()
    {
        camera = Camera.main;
        planeScale = 41;
        speed = 20;
        height = transform.position.y - 2;      // 건물높이 적용

        zoomSpeed = 10;

        screenX = Screen.width;
        screenY = Screen.height;

        screenRatio = screenX / screenY;
    }

    void Update()
    {
        if(SettingManager.Instance.settingMenu != null)
        {
            return;
        }

        // 마우스 위치에 따른 이동 구현(완료)
        // 0.004f = Time.deltatime -> pause 상태에서도 움직이기 위함
        mousePosition = Input.mousePosition;

        if (mousePosition.x >= (screenX - (screenX * 0.02)))
        {
            transform.Translate(Vector3.right * speed * 0.004f);
        }
        else if(mousePosition.x <= screenX * 0.02)
        {
            transform.Translate(Vector3.left * speed * 0.004f);
        }

        if (mousePosition.y >= (screenY - (screenY * 0.02)))
        {
            transform.position += Vector3.forward * speed * 0.004f;
        }
        else if(mousePosition.y <= screenY * 0.02)
        {
            transform.position += Vector3.back * speed * 0.004f;
        }

        // 카메라 보이는 각 최대치 제한
        cameraAngle = camera.fieldOfView / 2;

        cameraLeftSize = height / (Mathf.Tan((90 - (cameraAngle * screenRatio)) * Mathf.Deg2Rad));

        cameraUpSize = height / Mathf.Tan((90 - (25 + cameraAngle)) * Mathf.Deg2Rad);
        cameraDownSize = height / Mathf.Tan((90 - (25 - cameraAngle)) * Mathf.Deg2Rad);

        maxX = (planeScale / 2) - cameraLeftSize + 5;   // 여유공간

        maxZ = (planeScale / 2) - cameraUpSize;
        minZ = -(planeScale / 2) - cameraDownSize;

        // 마우스 휠에 의한 시야각 조정(완료)
        zoomAmount = camera.fieldOfView + (-Input.GetAxis("Mouse ScrollWheel") * zoomSpeed);

        camera.fieldOfView = Mathf.Clamp(zoomAmount, 10, 60);

        transform.position =
            new Vector3
            (
                Mathf.Clamp(transform.position.x, -maxX, maxX),
                transform.position.y,
                Mathf.Clamp(transform.position.z, minZ, maxZ)
            );
    }
}
