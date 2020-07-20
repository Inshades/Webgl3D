using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveJoystick : MonoBehaviour
{
    protected Joystick joystick;
    public CharacterController controller;
    float speed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        joystick = FindObjectOfType<Joystick>();
    }

    // Update is called once per frame
    void Update()
    {
       
        //rigidbody.velocity = new Vector3(joystick.Horizontal * 100f, 0, joystick.Vertical * 100f);
        Vector3 mov = new Vector3(-joystick.Horizontal * speed, 0, -joystick.Vertical * speed);
        controller.Move(mov * Time.deltaTime);
    }
}
