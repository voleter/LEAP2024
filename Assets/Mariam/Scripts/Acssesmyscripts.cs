using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acssesmyscripts : MonoBehaviour
{
    timer1 timer;
    public int Duration;

    public GameObject Canvas;
    

    // Start is called before the first frame update
    void Awake()
    {
        timer=Canvas.GetComponent<timer1>();
        
    }

    // Update is called once per frame
    void Update()
    {
       // timer.Being(Duration);
    }

    public void Duration2(int Duration){
        timer.Being(Duration);

    }

}
