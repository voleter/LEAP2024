using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class timer1 : MonoBehaviour
{
    private int remainingDuration;
    public int Duration;
    [SerializeField] private TMP_Text uiText;

    [SerializeField] private Image uiFill2;





    public void Start()
    {
        Being(Duration);
        GameObject othergame = GetComponent<GameObject>();
        
        
    }

    

    public void Being(int Second)
    {
        remainingDuration = Second;
        StartCoroutine(UpdateTimer());
    }
    
    private IEnumerator UpdateTimer()
    {
        while(remainingDuration >= 0)
        {
            
            {
                uiText.text = $"{remainingDuration / 60:00}:{remainingDuration % 60:00}";
                uiFill2.fillAmount = Mathf.InverseLerp(0, Duration, remainingDuration);
                remainingDuration--;
                yield return new WaitForSeconds(1f);
            }
            yield return null;
        }
    }

}


