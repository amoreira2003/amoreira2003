using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFpsCamera : MonoBehaviour
{
    public Transform playerBody;

    [Range(0.1f,1f)]
    public float sensitivity = 0.5f;
    float sensitivityMultiplier = 100f;

    [Range(0f,90f)]
    public float ClampCamera = 90f;
    float Rotation = 0f;


    bool ableToWork = true;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if(!ableToWork) return;

        float x = Input.GetAxis("Mouse X") * sensitivity * sensitivityMultiplier * Time.deltaTime;
        float y = Input.GetAxis("Mouse Y") * sensitivity * sensitivityMultiplier *Time.deltaTime;
        
        Rotation -= y;
        Rotation = Mathf.Clamp(Rotation,-ClampCamera,ClampCamera);

        transform.localRotation = Quaternion.Euler(Rotation,0,0);
        playerBody.Rotate(Vector3.up * x);

    }

    public void setFpsCameraActive(bool status) {
        if(status) {
          Cursor.lockState = CursorLockMode.Locked;
        } else {
          Cursor.lockState = CursorLockMode.Confined;
        }
        ableToWork = status;

    }
}
