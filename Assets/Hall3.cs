using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hall3 : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Manager.instance.h3_Holder.SetActive(true);
       
    }
    private void OnTriggerExit(Collider other)
    {
        Manager.instance.h3_Holder.SetActive(false);
    }
}
