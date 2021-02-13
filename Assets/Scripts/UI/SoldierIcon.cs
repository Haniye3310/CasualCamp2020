using UnityEngine;
using UnityEngine.UI;

public class SoldierIcon : MonoBehaviour
{
    public SoldierType SoldierType;

    public Sprite _selectedIcon;
    Image _image;
    Sprite _defaultIcon;
    StartGamePanel _startGamePanel;

    void Start() 
    {
        _image = gameObject.GetComponent<Image>();
        _defaultIcon = _image.sprite;
        _startGamePanel = gameObject.GetComponentInParent<StartGamePanel>();
        _startGamePanel.RegisterIcon(SoldierType, this);
    }

    public void SetSelected(bool isSelected) 
    {
        if(isSelected) 
        {
            _image.sprite = _selectedIcon;
        }
        else 
        {
            _image.sprite = _defaultIcon;
        }
    }

    public void SetSelected_OnClick() {
        Debug.Log("Selecting");
        _startGamePanel.SetSelectedSoldier(SoldierType);
    }

}