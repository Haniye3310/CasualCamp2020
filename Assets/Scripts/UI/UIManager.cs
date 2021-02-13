using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject LoadingBar;
    [SerializeField] GameObject Settingpage;
    [SerializeField] GameObject cupspage;
    [SerializeField] GameObject Aboutuspage;
    [SerializeField] GameObject Startpage;
    [SerializeField] GameObject goldcup1;
    [SerializeField] GameObject choosingpage;
    [SerializeField] GameObject bronzecup1;
    [SerializeField] Sprite[] soldiers;
    [SerializeField] Image soldierimage;
    [SerializeField] GameObject backbtn;
    [SerializeField] SoldierIcon Soldier;
    [SerializeField] GameObject silvercup1;
    [SerializeField] SoldierType[] soldierTypes;
    [SerializeField] Sprite[] selectedIcon;
    int x = 0;
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
        choosingpage.SetActive(true);
    }

    public void Setting_OnClick()
    {
        Settingpage.SetActive(true);
    }
    public void cupsp_OnClick()
    {
        cupspage.SetActive(true);
    }
    public void AboutUs_OnClick()
    {
        Aboutuspage.SetActive(true);
    }
    public void Close_OnClick()
    {
        Aboutuspage.SetActive(false);
        cupspage.SetActive(false);
        Settingpage.SetActive(false);
    }
    public void GoldCup_button()
    {
        goldcup1.SetActive(true);
    }
    public void closecuppage_onclick()
    {
        goldcup1.SetActive(false);
    }
    public void onevsone_onclick()
    {
        choosingpage.SetActive(true);
    }
   /////// public void front_onclick()
//////{
////////soldierimage.sprite = soldiers[x];
        /////Soldier.SoldierType = soldierTypes[x];
       ////// Soldier._selectedIcon = selectedIcon[x];
     //////   x++;
///////if (x == soldiers.Length)
      /////      x = soldiers.Length - 1;
///////}
   /////// public void back_onclick()
/////{
        ////soldierimage.sprite = soldiers[x];
        ///Soldier.SoldierType = soldierTypes[x];
        ///Soldier._selectedIcon = selectedIcon[x];
/////////x--;
     //////   if (x == -1)
          //////  x = 0;
   ////// }

    public void backchoose_onclick()
    {
        Startpage.SetActive(true);
        choosingpage.SetActive(false);
    }
    public void bronzecup_button()
    {
        bronzecup1.SetActive(true);
    }
    public void closebronze_onclick()
    {
        bronzecup1.SetActive(false);
        cupspage.SetActive(true);
    }
    public void backbtn_onclick()
    {
        cupspage.SetActive(true);
        silvercup1.SetActive(false);
    } 
    public void silverpage_onclick()
    {
        silvercup1.SetActive(true);
        cupspage.SetActive(false);
    } 
    public void silvercup_button()
    {
        silvercup1.SetActive(true);
    } 
   
    
    

}
