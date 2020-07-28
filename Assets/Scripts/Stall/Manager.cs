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

    //[SerializeField]
    //private GameObject[] h1_Stalls;

    //[SerializeField]
    //private GameObject[] h2_Stalls;

    //[SerializeField]
    //private GameObject[] h3_Stalls;

    private List<string> stallName;

    //[SerializeField]
    //private GameObject[] h2_Stalls;

    //[SerializeField]
    //private GameObject[] h3_Stalls;

    [Header("Position Container")]
    [SerializeField]
    private GameObject shopPositionContainer;

    [SerializeField]
    private GameObject hall_1_Container;

    [SerializeField]
    private GameObject hall_2_Container;

    [SerializeField]
    private GameObject hall_3_Container;



    [Header("Stall Holder")]
    public GameObject h1_Holder;

    public GameObject h2_Holder;

    public GameObject h3_Holder;

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


    //public IEnumerator GenarateHall_1_Stalls()
    //{
    //    for (int i = 0; i < hall_1_Container.transform.childCount; i++)
    //    {
    //        GameObject obj = Instantiate(h1_Stalls[i], hall_1_Container.transform.GetChild(i).position, hall_1_Container.transform.GetChild(i).rotation) as GameObject;
    //        obj.transform.parent = h1_Holder.transform;

    //        h1_Holder.SetActive(false);
    //        obj.transform.GetComponent<StallManager>().currentIndex = i.ToString();

    //        yield return obj.transform.GetComponent<StallManager>().loadedStatus;
    //    }
    //}
    //public IEnumerator GenarateHall_2_Stalls()
    //{
    //    for (int i = 0; i < hall_2_Container.transform.childCount; i++)
    //    {
    //        GameObject obj = Instantiate(h2_Stalls[i], hall_2_Container.transform.GetChild(i).position, hall_2_Container.transform.GetChild(i).rotation) as GameObject;
    //        obj.transform.parent = h2_Holder.transform;

    //        h2_Holder.SetActive(false);
    //        obj.transform.GetComponent<StallManager>().currentIndex = i.ToString();

    //        yield return obj.transform.GetComponent<StallManager>().loadedStatus;
    //    }
    //}
    //public IEnumerator GenarateHall_3_Stalls()
    //{
    //    for (int i = 0; i < hall_3_Container.transform.childCount; i++)
    //    {
    //        GameObject obj = Instantiate(h3_Stalls[i], hall_3_Container.transform.GetChild(i).position, hall_3_Container.transform.GetChild(i).rotation) as GameObject;
    //        obj.transform.parent = h3_Holder.transform;

    //        h3_Holder.SetActive(false);
    //        obj.transform.GetComponent<StallManager>().currentIndex = i.ToString();

    //        yield return obj.transform.GetComponent<StallManager>().loadedStatus;
    //    }
    //}


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