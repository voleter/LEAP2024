using System.Collections;
using System.Collections.Generic;


using UnityEngine;
using UnityEngine.SceneManagement;


public class buttunplayer : MonoBehaviour
{
    public Animator anim;
    public void PlayGame(){
        
       StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex+1));
       
    }
  

  IEnumerator LoadLevel(int LevelIndex){

        anim.SetTrigger("Start");
        yield return new WaitForSeconds(2);

        SceneManager.LoadScene("MariamAhmed");

  }


   
 
  
   
}
