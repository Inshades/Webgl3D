using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hall2 : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Manager.instance.h2_Holder.SetActive(true);
        Manager.instance.h1_Holder.SetActive(false);
    }
    private void OnTriggerExit(Collider other)
    {
        Manager.instance.h2_Holder.SetActive(false);
    }
}
