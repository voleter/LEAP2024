using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotesCreater : MonoBehaviour
{
   public GameObject note;
    // Update is called once per frame
    void Awake()
    {
        //note = GetComponent<GameObject>();  
    }

    public void CreateNote(){
        Instantiate(note, transform.position, Quaternion.identity);
    }
}
