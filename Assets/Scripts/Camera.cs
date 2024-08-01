using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] Camera camera;
    [SerializeField] float height;  // 카메라 높이
    [SerializeField] float speed;   // 카메라 스피드

    public float cameraSizeX;   // 카메라의 가로 길이
    public float cameraSizeY;   // 카메라의 세로 길이
    public float maxX;          // 맵의 가로 길이
    public float maxY;          // 맵의 세로 길이

    public Vector2 mouse;       // 마우스의 현제 좌표
    public float zoomAmount;    // 마우스 휠
    public float zoomSpeed;    // 마우스 휠 스피드


    void Start()
    {
        camera = GetComponent<Camera>();
    }

    void Update()
    {
        zoomAmount = transform.position.y + (Input.GetAxis("Mouse ScrollWheel") * zoomSpeed);

        transform.position = new Vector3(0, Mathf.Clamp(zoomAmount, 5, 20),0);
    }
}
