using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This script is for stall instantiation and management
/// </summary>
public class StallManager : MonoBehaviour
{
    [Header("Stalls to Placed")]
    [SerializeField]
    private GameObject[] stalls;
    [Header("Position Holder")]
    [SerializeField]
    private GameObject shopPositionContainer;

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

            Instantiate(stalls[i], shopPositionContainer.transform.GetChild(i).position, shopPositionContainer.transform.GetChild(i).rotation);
        }
    }
    
}
