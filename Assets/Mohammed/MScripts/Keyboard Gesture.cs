using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.EventSystems;
using TMPro;

public class GestureKeyboardOpener : MonoBehaviour
{
    public GameObject virtualKeyboard;
    public TMP_InputField inputField; 

    private void Start()
    {
       
        if (inputField != null)
        {
            EventTrigger trigger = inputField.gameObject.GetComponent<EventTrigger>() ?? inputField.gameObject.AddComponent<EventTrigger>();
            EventTrigger.Entry entry = new EventTrigger.Entry
            {
                eventID = EventTriggerType.PointerClick
            };
            entry.callback.AddListener((eventData) => { OpenVirtualKeyboard(); });
            trigger.triggers.Add(entry);
        }
    }

    private void OpenVirtualKeyboard()
    {
        
        virtualKeyboard.SetActive(true);
    }
}
