using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class TriggerPoint : MonoBehaviour
{
   public GameObject MainVideoCanvas;
    //private void OnTriggerEnter(Collider other)
    //{
    //  //  APICanvas.SetActive(true);


    //}
    private void OnTriggerExit(Collider other)
    {
        MainVideoCanvas.SetActive(false);
    }
}
