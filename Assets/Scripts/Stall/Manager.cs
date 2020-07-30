/// <summary>
/// This script is for stall instantiation and management
/// </summary>
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    [Header("Stalls to Placed")]
    [SerializeField]
    private GameObject[] stalls;

  
    private List<string> stallName;


    [Header("Position Container")]
    [SerializeField]
    private GameObject shopPositionContainer;

 



    [Header("Stall Holder")]
    public GameObject h1_Holder;

  

    public static Manager instance = null;
    public static Manager Instance
    {
        get { return instance; }
    }

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {

    }
    ///// <summary>
    /// Generate stalls in the position of shopPositionContainer
    /// </summary>
    public IEnumerator GenarateStalls(metaDataUrlData _metaDataUrl)
    {

        for (int i = 0; i < shopPositionContainer.transform.childCount; i++)
        {
            foreach (Transform item in shopPositionContainer.transform)
            {
                //Debug.Log("Item Name " + item.name + " Booth Id  " + ApiHandler.instance._metaDataUrlContent._collegeDataClassList[i].exhibhitorsBoothId[0]);
                if (item.name == _metaDataUrl._collegeDataClassList[i].exhibhitorsBoothId[0])
                {
                    stallType _stlType = (stallType)Enum.Parse(typeof(stallType), _metaDataUrl._collegeDataClassList[i].exhibhitorsBoothModel[0]);
                    GameObject obj = Instantiate(stalls[(int)_stlType], shopPositionContainer.transform.GetChild(item.GetSiblingIndex()).position, shopPositionContainer.transform.GetChild(item.GetSiblingIndex()).rotation) as GameObject;
                    obj.transform.parent = h1_Holder.transform;
                    obj.transform.GetComponent<StallManager>().currentIndex = item.GetSiblingIndex().ToString();
                    obj.name = item.name;
                    yield return obj.transform.GetComponent<StallManager>().currentIndex;


                }

            }
        }
    }



}
public enum stallType
{
    BASIC1,
    BASIC2,
    GOLD1,
    GOLD2,
    ASSOCIATE,
    POWEREDBY,
    PRESENTING
}