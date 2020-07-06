using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// /// This script for the camera movement
/// </summary>
public class CameraMovement : MonoBehaviour
{
    public float MoveSpeed;    
    // Update is called once per frame
    void Update()
    {
        transform.Translate(MoveSpeed * Input.GetAxis("Horizontal") * Time.deltaTime, 0f, MoveSpeed * Input.GetAxis("Vertical") * Time.deltaTime);
    }
}
