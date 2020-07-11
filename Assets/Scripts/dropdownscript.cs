using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class dropdownscript : MonoBehaviour
{

    public Dropdown dropdown;


    //if offline logo 
    public Sprite[] sprites;
    
   
    // Start is called before the first frame update
    void Start()
    {
       // dropdown.ClearOptions();
        List<Dropdown.OptionData> optionDatalist = new List<Dropdown.OptionData>();
       // optionDatalist.Add(new Dropdown.OptionData( "Select"));

        foreach(var sprite in sprites)
        {
            

            //if online images
           // var options = new Dropdown.OptionData("stall  name" , "stall logo ");


            optionDatalist.Add(new Dropdown.OptionData(sprite.name, sprite));
        }

        dropdown.AddOptions(optionDatalist);
    }

    // Update is called once per frame
   
}
