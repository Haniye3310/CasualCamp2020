using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashLogo : MonoBehaviour
{
    void Start() 
    {
        StartCoroutine(LoadSceneAfterSecond(4));
    }
    IEnumerator LoadSceneAfterSecond(int second) 
    {
        yield return new WaitForSeconds(second);
        SceneManager.LoadScene(1);
    }
}
