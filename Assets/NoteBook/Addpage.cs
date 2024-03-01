using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Addpage : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject page;
    public Transform contentui;
    void Start()
    {
        
    }

    // Update is called once per frame
   public void CreateNewpage(){
    GameObject go =Instantiate(page,contentui);
    go.SetActive(true);

   }
}
