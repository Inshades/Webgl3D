using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class myconn_controller : MonoBehaviour
{
    public GameObject Listtemplate;

    public void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            GameObject listitem = Instantiate(Listtemplate) as GameObject;
            listitem.SetActive(true);
           // listitem.GetComponent<myconnection_data>().setData("key","name","email",//"phoneno");         
            listitem.transform.SetParent(Listtemplate.transform.parent, false);
        }
    }
}
