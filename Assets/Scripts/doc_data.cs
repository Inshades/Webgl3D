using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class doc_data : MonoBehaviour
{
    [SerializeField]
    private Text Doc_name;
    [SerializeField]
    private Text Exhibitor_name;
   

    private string myKeystring;

    public void setData(string key, string Doc_name, string Exhibitor_name)
    {
        myKeystring = key;
        this.Doc_name.text = Doc_name;
        this.Exhibitor_name.text = Exhibitor_name;
        
    }

}
