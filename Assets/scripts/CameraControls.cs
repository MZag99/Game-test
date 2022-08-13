using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
    public float panSpeed = 20f;
    public float rotSpeed = 5f;
    void Update()
    {
        Vector3 pos = transform.position;
        Vector3 rot = transform.eulerAngles;

        if (Input.GetKey("left shift"))
        {
            panSpeed = 40f;
        }
        else
        {
            panSpeed = 20f;
        }

        if (Input.GetKey("w"))
        {

            transform.Translate(Vector3.forward * panSpeed * Time.deltaTime);
        }

        if (Input.GetKey("s"))
        {
            transform.Translate(-Vector3.forward * panSpeed * Time.deltaTime);
        }

        if (Input.GetKey("a"))
        {
            transform.Translate(-Vector3.right * panSpeed * Time.deltaTime);
        }

        if (Input.GetKey("d"))
        {
            transform.Translate(Vector3.right * panSpeed * Time.deltaTime);
        }

        if (Input.GetMouseButton(0))
        {
            rot += rotSpeed * new Vector3(x: -Input.GetAxis("Mouse Y"), y: Input.GetAxis("Mouse X"), z: 0);
        }

        transform.eulerAngles = rot;
    }
}
