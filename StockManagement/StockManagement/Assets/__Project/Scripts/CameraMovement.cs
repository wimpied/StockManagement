﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float speed = 1;
    private void Update()
    {
        if(Input.GetKey(KeyCode.DownArrow))
        {
            transform.position += new Vector3(0, 0 , 0.1f) * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.position += new Vector3(0 , -0 , -0.1f) * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += new Vector3(0.1f , 0, 0) * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += new Vector3(-0.1f, 0, 0) * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.PageUp))
        {
            transform.position += new Vector3(0, -0.1f,0) * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.PageDown))
        {
            transform.position += new Vector3(0, 0.1f,0) * speed * Time.deltaTime;
        }

    }


}
