using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject LoadingBar;
   public void PlayButton_OnClick() 
    {
         SceneManager.LoadSceneAsync(2);
         LoadingBar.SetActive(true);
    }
 
}
