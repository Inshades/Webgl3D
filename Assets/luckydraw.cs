using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.UI;



public class luckydraw : MonoBehaviour
{
    [SerializeField]
    private GameObject buttonSend;

    [SerializeField]
    private GameObject[] images = new GameObject[6];
   


    private List<string> RandomList = new List<string>();

    private int count = 0;


    // Start is called before the first frame update
    void Start()
    {
      GenerateRandomList();
    }

    // Update is called once per frame
   public void GenerateRandomList()
    {
       
        RandomList.Add("Columbia University");
        RandomList.Add("Duke University");
        RandomList.Add("McGill University");
        RandomList.Add("Princeton University");
        RandomList.Add("Stanford University");
        RandomList.Add("UC Berkeley");
        Debug.Log(RandomList);
    }

   public  void checker(string Exhib_name)
    {


        Debug.Log("triggg" +Exhib_name);
        if (RandomList.Contains(Exhib_name))
        {           
            RandomList.Remove(Exhib_name);
            images[count].SetActive(false);
            count++;
            Debug.Log(Exhib_name);
        }

        if (RandomList.Count == 0)
        {
            buttonSend.SetActive(true);
        }

    }

   public void buttonLuckyDraw()
    {
        Debug.Log("You are eligible for lucky draw" );
    }
        
}
