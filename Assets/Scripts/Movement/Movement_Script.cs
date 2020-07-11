using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;



public class Movement_Script : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    public float MoveSpeed;
    public CharacterController controller;
    public float speed = 12f;
    [SerializeField] private AudioClip[] m_FootstepSounds;
    private AudioSource m_AudioSource;

    public static int Rot_Lft, Rot_Right, Move_Up, Move_Down, Move_Left, Move_Right, count;
    // public static bool StopForAWhile = true;
    bool movement;

    void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        movement = false;
    }
    public void OnPointerDown(PointerEventData eventData)
    {

        movement = true;
    }
    // Update is called once per frame
    void Update()
    {


        //keyboard control
        //   transform.Translate(MoveSpeed*Input.GetAxis("Horizontal")Time.deltaTime,0f,MoveSpeed Input.GetAxis("Vertical") * Time.deltaTime);
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        if (Input.GetKey("up") || Input.GetKey("down") || Input.GetKey("left") || Input.GetKey("right") || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            ProgressStepCycle(count);
            count += 1;
            Vector3 move = transform.right * x + transform.forward * z;

            controller.Move(move * speed * Time.deltaTime);
        }

            //UI Control
            if (Rot_Lft != 0)
            {
                rotateLeft();
            }
            if (Rot_Right != 0)
            {
                rotateRight();
            }
            if (Move_Up != 0)
            {
                MoveForward();
            }
            if (Move_Down != 0)
            {
                MoveBackward();
            }
            if (Move_Left != 0)
            {
                MoveLeftSide();
            }
            if (Move_Right != 0)
            {
                MoveRightSide();
            }
        



    }

    private void ProgressStepCycle(int count1)
    {
        if (count1 % 40 == 0)
        {
            count = 0;
            PlayFootStepAudio();
        }


    }

    private void PlayFootStepAudio()
    {
        if (controller.isGrounded)
        {
            return;
        }
        // pick & play a random footstep sound from the array,
        // excluding sound at index 0
        int n = UnityEngine.Random.Range(1, m_FootstepSounds.Length);
        m_AudioSource.clip = m_FootstepSounds[n];
        m_AudioSource.PlayOneShot(m_AudioSource.clip);
        // move picked sound to index 0 so it's not picked next time
        m_FootstepSounds[n] = m_FootstepSounds[0];
        m_FootstepSounds[0] = m_AudioSource.clip;
    }






    // UI Controls
    public void rotateLeft()
    {
        // transform.RotateAround(new Vector3(), new Vector3(0, 1, 0), transform.position.y * 180);
        transform.Rotate(Vector3.down * 9);
    }

    public void rotateRight()
    {

        // transform.RotateAround(new Vector3(), new Vector3(0, 1, 0), -transform.position.y * 180);

        transform.Rotate(Vector3.up * 9);
    }

    public void MoveForward()
    {

        transform.Translate(Vector3.forward * MoveSpeed * Time.deltaTime);


    }
    public void MoveBackward()
    {

        transform.Translate(Vector3.back * MoveSpeed * Time.deltaTime);
    }

    public void MoveLeftSide()
    {

        transform.Translate(Vector3.left * MoveSpeed * Time.deltaTime);
    }

    public void MoveRightSide()
    {

        transform.Translate(Vector3.right * MoveSpeed * Time.deltaTime);
    }
}