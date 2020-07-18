using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class SlideShowManager : MonoBehaviour
{
    public List<Sprite> slideShowSourceImages = new List<Sprite>();
    public Image[] slideShowDisplayImages;
    [SerializeField]
    private int currentImageIndex = 0;
    public GameObject leftButton, RightButton;

    public void Start()
    {
        leftButton.SetActive(false);
        RightButton.SetActive(false);
        
        Invoke("loadImages", 0.5f);
    }

    void loadImages()
    {
        slideShowDisplayImages[currentImageIndex].sprite = slideShowSourceImages[currentImageIndex];
        if (slideShowSourceImages.Count > 0)
        {
            RightButton.SetActive(true);
        }
    }

    public void LeftButtonClick()
    {
        if (currentImageIndex > 0)
        {
            currentImageIndex -= 1;
            slideShowDisplayImages[0].sprite = slideShowSourceImages[currentImageIndex];

            RightButton.SetActive(true);
        }
        if (currentImageIndex == 0)
        {
            leftButton.SetActive(false);
        }
    }

    public void RightButtonClick()
    {
        if (currentImageIndex >= 0 && currentImageIndex < slideShowSourceImages.Count - 1)
        {
            currentImageIndex += 1;
            slideShowDisplayImages[0].sprite = slideShowSourceImages[currentImageIndex];

            leftButton.SetActive(true);
        }
        if (currentImageIndex == slideShowSourceImages.Count - 1)
        {
            RightButton.SetActive(false);
        }

    }
}
