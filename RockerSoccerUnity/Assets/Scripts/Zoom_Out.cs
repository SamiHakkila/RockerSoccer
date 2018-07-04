﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zoom_Out : MonoBehaviour
{

    public new Transform camera;

    public float distance;
    public Vector2 endPosition;
    public Camera m_OrthographicCamera;
    public Transform ballT;
    public float inputAngle;
    public Transform cameraStart;
    public float size_Offset;
    public bool slow_Mo_Camera = false;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       // print(inputAngle);
        inputAngle = GameObject.Find("Input_Manager").GetComponent<TouchControl>().camera_Angle;

        if (GameObject.Find("Game_Manager").GetComponent<Game_Manager>().releaseBall == true)
        {

            if (camera.transform.position.y > 0 && slow_Mo_Camera == false)
            {
                // m_OrthographicCamera.orthographicSize = 4 + camera.transform.position.y + Mathf.Lerp(size_Offset,0.0f , 1.7f * Time.deltaTime);
                m_OrthographicCamera.orthographicSize = 4 + camera.transform.position.y + size_Offset;
            }
            if (slow_Mo_Camera == true)
            {
                m_OrthographicCamera.orthographicSize = 4 + camera.transform.position.y + size_Offset;

            }


        }
        else if (slow_Mo_Camera == false)
        {
            m_OrthographicCamera.orthographicSize = 4 + 1.83f + size_Offset;
        }

        if (GameObject.Find("Game_Manager").GetComponent<Game_Manager>().SlowMo == true && TouchControl.inputState == TouchControl.InputState.InputStart)
        {
            slow_Mo_Camera = true;
            if (inputAngle >= 0.0f && inputAngle <= 90)
            {
                Camera.main.transform.position = Vector3.Lerp(camera.transform.position, camera.transform.position + new Vector3(15.0f, 0.0f, 0.0f), 0.7f * Time.deltaTime);
                size_Offset += 0.1f ;
                
            }
            if (inputAngle > 90.0f && inputAngle <= 180)
            {
                Camera.main.transform.position = Vector3.Lerp(camera.transform.position, camera.transform.position - new Vector3(15.0f, 0.0f, 0.0f), 0.7f * Time.deltaTime);
                size_Offset += 0.1f;
                
            }
        }
        else
        {
            Camera.main.transform.position = Vector3.Lerp(camera.transform.position, cameraStart.transform.position, 0.7f * Time.deltaTime);
            size_Offset -= 0.2f;
            slow_Mo_Camera = false;
            if (size_Offset <= 0.0f)
            {
                size_Offset = 0.0f;
            }
        }
    }
}
