/// <summary>
/// This script is for stall instantiation and management
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    [Header("Stalls to Placed")]
    [SerializeField]
    private GameObject[] stalls;
    [Header("Position Holder")]
    [SerializeField]
    private GameObject shopPositionContainer;

    public GameObject StallsContainer;
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
        GenarateStalls();
    }
    /// <summary>
    /// Generate stalls in the position of shopPositionContainer
    /// </summary>
    public void GenarateStalls()
    {

        for (int i = 0; i < shopPositionContainer.transform.childCount; i++)
        {
            GameObject obj = Instantiate(stalls[i], shopPositionContainer.transform.GetChild(i).position, shopPositionContainer.transform.GetChild(i).rotation) as GameObject;
            obj.transform.parent = StallsContainer.transform;
            
        }

       
    }
}
public enum stallType
{
    BASIC1,
    BASIC2,
    GOLD1,
    GOLD2,
    PLATINUM1,
    PLATINUM2
}