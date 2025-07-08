using UnityEngine;

public class ChangeAxeTransform : MonoBehaviour
{
    public Camera playerCamera;

    private void Update()
    {

        transform.rotation = playerCamera.transform.rotation;

        float xRotation = transform.rotation.eulerAngles.x;

        xRotation = Mathf.Clamp(xRotation, -30f, 30f);
        transform.rotation = Quaternion.Euler(xRotation, 0, 0);
    }   
}
