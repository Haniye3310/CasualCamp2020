using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] GameObject _menu;
    [SerializeField] GameObject _setting;
   public void Menu_OnClick() 
    {
        _menu.SetActive(true);
    }
    public void Exit_OnClick() 
    {
        SceneManager.LoadScene(1);
    }
    public void Setting_OnClick() 
    {
        _setting.SetActive(true);
    }
    public void SettingExit() 
    {
        _setting.SetActive(false);
    }
}
