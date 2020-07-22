using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hall1 : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Manager.instance.h1_Holder.SetActive(true);
    }
    private void OnTriggerExit(Collider other)
    {
        Manager.instance.h1_Holder.SetActive(false);
    }
}
