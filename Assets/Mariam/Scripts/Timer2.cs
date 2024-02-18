using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class Timer2 : MonoBehaviour , IPointerClickHandler
{

    public void OnPointerClick(PointerEventData eventData)
    {
        Pause = !Pause;
    }

    [SerializeField] private Image uiFill;
    [SerializeField] private Image uiFill2;

    [SerializeField] private TMP_Text uiText;
    [SerializeField] private TMP_Text uiIText;
    public GameObject Uitonumber;
    public GameObject Uitobeging;
    

    //[SerializeField] private TMP_Text uiTextt;
   
    
    

    public int Duration;
    public float remaindertime;
    private int remainingDuration;

    private bool Pause;

    public AudioSource ss;
    public AudioSource waketimer;
    public void Start()
    {
        Being(Duration);
        GameObject othergame = GetComponent<GameObject>();
        Uitonumber.SetActive(false);
        
    }

    

    private void Being(int Second)
    {
        remainingDuration = Second;
        StartCoroutine(UpdateTimer());
    }
    
    private IEnumerator UpdateTimer()
    {
        while(remainingDuration >= 0)
        {
            if (!Pause)
            {
                uiText.text = $"{remainingDuration / 60:00}:{remainingDuration % 60:00}";
                uiFill.fillAmount = Mathf.InverseLerp(0, Duration, remainingDuration);
                remainingDuration--;
                yield return new WaitForSeconds(1f);
            }
            yield return null;
        }
        OnEnd();
        //resttimer();
        Uitonumber.SetActive(true);
        Uitobeging.SetActive(false);
        ss.Play();


    }

    private void OnEnd()
    {
        //End Time , if want Do something
        print("End");
        ss.Play();
        //resttimer();
        print("here2");

    }

    private void Update(){
      

        if (remaindertime>0){

            remaindertime -= Time.deltaTime;
            
        }

        else if (remaindertime < 0 ){

            remaindertime=0;
            Being(Duration);
            Uitonumber.SetActive(false);
            Uitobeging.SetActive(true);
            waketimer.Play();
            remaindertime=1900;
            
            
        }
    
    int minutes = Mathf.FloorToInt(remaindertime/60);
    int seconds = Mathf.FloorToInt(remaindertime%60);    
    uiIText.text = string.Format("{0:00}: {1:00}",minutes, seconds);
    uiFill2.fillAmount = Mathf.InverseLerp(0, remaindertime, remaindertime);

    
    }
    
    
 
        
}




