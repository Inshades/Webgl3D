using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doc_controller : MonoBehaviour
{
    public GameObject Doctemplate;

    public void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            GameObject listitem = Instantiate(Doctemplate) as GameObject;
            listitem.SetActive(true);
            listitem.GetComponent<doc_data>().setData("key", "doc_name", "exhibator_name");
            listitem.transform.SetParent(Doctemplate.transform.parent, false);
        }
    }
}
