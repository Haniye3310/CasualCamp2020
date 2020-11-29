using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] GameObject _menu;
    [SerializeField] GameObject _setting;
    int _counter=0;
   public void Menu_OnClick() 
    {
        if(_counter %2 ==0) _menu.SetActive(true);
        if(_counter %2 !=0) _menu.SetActive(false);
        _counter++;
    }
    public void Exit_OnClick() 
    {
        SceneManager.LoadScene(1);
    }
    //public void Setting_OnClick() 
    //{
    //    _setting.SetActive(true);
    //}
    //public void SettingExit() 
    //{
    //    _setting.SetActive(false);
    //}
}
