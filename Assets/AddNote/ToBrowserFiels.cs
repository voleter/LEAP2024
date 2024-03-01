using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class ToBrowserFiels : MonoBehaviour
{
    // Start is called before the first frame update
   string path;
   public RawImage image;

   public void OpenExplorerr()
   {
    path = EditorUtility.OpenFilePanel("Overwrite with png", "","png");
    GetImage();
   }

   void GetImage(){
        if (path != null)
        {
        UdateImage();
        }
    }
    void UdateImage()
    {
        WWW www = new WWW("file:///"+ path);
        image.texture = www.texture;
    }
   
}
