using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public float Sense; 
    float x,y,Deltax,Deltay;
    public Transform Body;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;    
    }

    // Update is called once per frame
    void Update()
    {
        x = Input.GetAxis("Mouse X");
        y = Input.GetAxis("Mouse Y");
        Deltax += x*Sense;
        Deltay -= y*Sense;
        Deltay = Mathf.Clamp(Deltay, -40,40);

        transform.localRotation = Quaternion.Euler(Deltay, 0, 0);
        Body.localRotation = Quaternion.Euler(0, Deltax, 0);
    }
}
