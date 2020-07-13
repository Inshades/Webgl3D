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

    //public int gold1Count;
    //public int gold2Count;
    

    public GameObject FPS;

    public stallType _stallType;
    public void TeleportTo(GameObject playerPos)
    {
        FPS.transform.position = playerPos.transform.position;
        FPS.transform.rotation = playerPos.transform.rotation;
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
            // int randomNumber = Random.Range(0, placeHolders.Length);
            Debug.Log(i);
            GameObject obj = Instantiate(stalls[(int)stallType.BASIC1], shopPositionContainer.transform.GetChild(i).position, shopPositionContainer.transform.GetChild(i).rotation) as GameObject;
            obj.transform.parent = StallsContainer.transform;
        }

        TeleportTo(StallsContainer.transform.GetChild(3).GetComponent<StallManager>().playerPosition);
         //generateGold1(gold1Count);
         //generateGold2(gold2Count);
    }

    //void generateGold1(int goldStallCount)
    //{
    //    for (int i = 0; i < goldStallCount; i++)
    //    {
    //      Instantiate(stalls[(int)stallType.BASIC1], shopPositionContainer.transform.GetChild(i).position, shopPositionContainer.transform.GetChild(i).rotation);
    //    }
    //}
    
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