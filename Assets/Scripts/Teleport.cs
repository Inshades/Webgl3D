using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Teleport : MonoBehaviour
{
    public Dropdown dropdown;
    public GameObject FP;
    public GameObject stall;

    // Start is called before the first frame update
    void Start()
    {
        dropdown.onValueChanged.AddListener(delegate
        {
            tranformlocation(dropdown);
        });
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void tranformlocation(Dropdown sender)
    {
        Debug.Log(sender.options[sender.value].text );

        stall = GameObject.FindGameObjectWithTag(sender.options[sender.value].text);
        FP.transform.position = new Vector3(stall.transform.position.x, stall.transform.position.y, stall.transform.position.z);
        
        return;
    }
}
