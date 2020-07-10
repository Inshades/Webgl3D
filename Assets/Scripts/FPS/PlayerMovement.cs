using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This script is to move FPS Character
/// </summary>
public class PlayerMovement : MonoBehaviour
{   [Header("FPS Controller")]
    public CharacterController controller;
    public float speed = 12f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    /// <summary>
    /// To move FPS Charcter using WSAD and arrow keys
    /// </summary>
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);
    }
}
