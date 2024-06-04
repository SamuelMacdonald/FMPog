using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPerson : MonoBehaviour
{
    // Start is called before the first frame update
    public float mouseSesnitivityY = 850f;
    public float mouseSesnitivityX = 650f;

    public Transform playerBody;

    float xRotation = 0f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!pauseMenu.GameIsPaused)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None; 
        }
        float mouseX = Input.GetAxis("Mouse X") * mouseSesnitivityX * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSesnitivityY * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -89f, 89f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);  
        playerBody.Rotate(Vector3.up * mouseX);

    }
}
