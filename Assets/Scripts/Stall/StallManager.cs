using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class StallManager : MonoBehaviour
{
    public GameObject playerPosition;
    public List<Image> slideShowimages;
    public List<string> url;
    public string boothId;
    public stallType boothType;

    public void slideShow()
    {
        for (int i = 0; i < url.Count; i++)
        {
            StartCoroutine(downloadSlideShowImage(url[i], slideShowimages[i]));
        }
    }
    public IEnumerator downloadSlideShowImage(string url, Image img)
    {

        UnityWebRequest www = UnityWebRequest.Get(url);

        yield return www.SendWebRequest();
        DownloadHandler handle = www.downloadHandler;

        if (www.isNetworkError)
        {
            UnityEngine.Debug.Log("Error while Receiving: " + www.error);
        }
        else
        {
            UnityEngine.Debug.Log("Success");
            //Load Image
            Texture2D texture2d = new Texture2D(8, 8);
            Sprite sprite = null;
            if (texture2d.LoadImage(handle.data))
            {
                sprite = Sprite.Create(texture2d, new Rect(0, 0, texture2d.width, texture2d.height), Vector2.zero);
            }
            if (sprite != null)
            {
                img.sprite = sprite;
            }
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   
}
