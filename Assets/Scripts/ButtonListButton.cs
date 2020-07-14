using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
public class ButtonListButton : MonoBehaviour
{
    [SerializeField]
    private Text mytext;
    [SerializeField]

    private Image mysprite;
    [SerializeField]
    private Text description;
    [SerializeField]
    private UiManager buttonControl;
    private int myKey;
    [SerializeField]
    private string ImgUrl;


    public void setKey(int key)
    {
        myKey = key;
    }
    public void setText(string textString)
    {
        mytext.text = textString;
      
    }

    public void setDescription(string textDescription)
    {
        description.text = textDescription;
    }


    public void setImage(string url)
    {
        // mysprite.sprite = image
    }

    public IEnumerator downloadImage(string url)
    {
        ImgUrl = url;
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
                mysprite.sprite = sprite;
            }
        }
    }
    public void onClick()
    {
        buttonControl.ButtonClicked(myKey);
    }

}
