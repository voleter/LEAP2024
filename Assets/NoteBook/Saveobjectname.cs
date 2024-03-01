using UnityEngine;
using TMPro;

public class Asveobjectname : MonoBehaviour
{
    public TMP_InputField nameInputField;
    public TextMeshProUGUI subjectnameText;
    public TextMeshProUGUI subjectname1Text;

    private string subjectname;

    public void Savesubjectname()
    {
        subjectname = nameInputField.text;
       

        // Save the player's name using PlayerPrefs
        PlayerPrefs.SetString("subjectname", subjectname);
        PlayerPrefs.Save();
        PlayerPrefs.SetString("subjectname", subjectname);
        PlayerPrefs.Save();


        // Display the player's name in the game
        subjectnameText.text = subjectname;
        subjectname1Text.text= subjectname;
    }

    private void Start()
    {
        // Load the player's name from PlayerPrefs when the game starts
        subjectname = PlayerPrefs.GetString("subjectname","subjectname");
        subjectnameText.text =  subjectname ;
         subjectname = PlayerPrefs.GetString("subjectname","subjectname");
        subjectname1Text.text =  subjectname ;
    }
}