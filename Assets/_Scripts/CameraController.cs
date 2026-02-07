using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float transitionSpeed = 6f;
    public float zoomSpeed = 6f;

    Vector3 targetPosition;
    float targetOrthoSize;

    bool isMoving = false;
    public bool IsTransitioning => isMoving;

    CinemachineVirtualCamera cam;

    void Start()
    {
        cam = GetComponent<CinemachineVirtualCamera>();
        targetPosition = transform.position;
        targetOrthoSize = cam.m_Lens.OrthographicSize;
    }

    void LateUpdate()
    {
        if (!isMoving) return;

        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            transitionSpeed * Time.deltaTime
        );

        cam.m_Lens.OrthographicSize = Mathf.Lerp(
            cam.m_Lens.OrthographicSize,
            targetOrthoSize,
            zoomSpeed * Time.deltaTime
        );

        if (Vector3.Distance(transform.position, targetPosition) < 0.05f &&
            Mathf.Abs(cam.m_Lens.OrthographicSize - targetOrthoSize) < 0.05f)
        {
            transform.position = targetPosition;
            cam.m_Lens.OrthographicSize = targetOrthoSize;
            isMoving = false;
        }
    }

    public void MoveToRoom(Room room)
    {
        targetPosition = new Vector3(
            room.cameraPoint.position.x,
            room.cameraPoint.position.y,
            transform.position.z
        );

        targetOrthoSize = room.cameraOrthoSize;
        isMoving = true;
    }
}
