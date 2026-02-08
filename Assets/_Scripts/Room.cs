using UnityEngine;

public class Room : MonoBehaviour
{
    public Transform cameraPoint;
    public float cameraOrthoSize = 5f;

    public void ActivateRoom()
    {
        gameObject.SetActive(true);
    }

    public void DeactivateRoom()
    {
        gameObject.SetActive(false);
    }
}
