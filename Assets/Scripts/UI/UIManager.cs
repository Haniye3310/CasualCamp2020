using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject LoadingBar;
    [SerializeField] GameObject Settingpage;
    [SerializeField] GameObject Helppage;
    [SerializeField] GameObject Socialcontactpage;
    [SerializeField] GameObject Aboutuspage;
    [SerializeField] GameObject Startpage;
    public void PlayButton_OnClick() 
    {
         SceneManager.LoadSceneAsync(2);
         LoadingBar.SetActive(true);
    }
    public void NewGame_OnClick()
    {
        SceneManager.LoadScene(2);
    }
    public void Start_OnClick()
    {
        Startpage.SetActive(true);
    }
    public void Setting_OnClick()
    {
        Settingpage.SetActive(true);
    }
    public void Help_OnClick()
    {
        Helppage.SetActive(true);
    }
    public void SocialContact_OnClick()
    {
        Socialcontactpage.SetActive(true);
    }
    public void AboutUs_OnClick()
    {
        Aboutuspage.SetActive(true);
    }
    public void  Close_OnClick() 
    {
        Aboutuspage.SetActive(false);
        Socialcontactpage.SetActive(false);
        Helppage.SetActive(false);
        Settingpage.SetActive(false);
        Startpage.SetActive(false);
    }


}
