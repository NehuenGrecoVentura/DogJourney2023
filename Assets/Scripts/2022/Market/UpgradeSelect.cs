using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UpgradeSelect : MonoBehaviour
{
    [SerializeField] GameObject _description;
    [SerializeField] GameObject[] _buyButton;
    [SerializeField] Market _market;
    [SerializeField] Market _market2;
    Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    void Start()
    {
        foreach (var button in _buyButton)
            button.SetActive(false);
        HideDescription();
    }

    public void ShowDescription()
    {
        _description.SetActive(true);
    }

    public void HideDescription()
    {
        _description.SetActive(false);
    }

    public void ShowBuyButton()
    {
        if (_market.selected || _market2.selected)
        {
            foreach (var button in _buyButton)
                button.SetActive(true);
            _image.color = Color.white;
            gameObject.GetComponent<EventTrigger>().enabled = true;
        }

        else
        {
            foreach (var button in _buyButton)
            {
                button.SetActive(false);
                _market.selected = false;
                _market2.selected = false;
            }
        }
    }


    #region PLAYER UPGRADES

    public void Axe()
    {
        if (_market.selected || _market2.selected)
        {
            _market.upgradeAxe = true;
            _market2.upgradeAxe = true;
        }

        else
        {
            _market.upgradeAxe = false;
            _market2.upgradeAxe = false;
        }
    }

    public void SpeedPlayer()
    {
        if (_market.selected || _market2.selected)
        {
            _market.upgradeSpeedPlayer = true;
            _market2.upgradeSpeedPlayer = true;
        }

        else
        {
            _market.upgradeSpeedPlayer = false;
            _market2.upgradeSpeedPlayer = false;
        }
    }

    public void AddDog()
    {
        if (_market.selected || _market2.selected)
        {
            _market.upgradeAddDog = true;
            _market2.upgradeAddDog = true;
        }

        else
        {
            _market.upgradeAddDog = false;
            _market2.upgradeAddDog = false;
        }
    }

    #endregion PLAYER UPGRADES

    public void Trolley()
    {
        if (_market.selected || _market2.selected)
        {
            _market.upgradeTrolley = true;
            _market2.upgradeTrolley = true;
        }

        else
        {
            _market.upgradeTrolley = false;
            _market2.upgradeTrolley = false;
        }
    }

    public void SpeedDog()
    {
        if (_market.selected || _market2.selected)
        {
            _market.upgradeSpeedDog = true;
            _market2.upgradeSpeedDog = true;
        }

        else
        {
            _market.upgradeSpeedDog = false;
            _market2.upgradeSpeedDog = false;
        }
    }

    public void TreeGeneration()
    {
        if (_market.selected || _market2.selected)
        {
            _market.upgradeTreeGen = true;
            _market2.upgradeTreeGen = true;
        }

        else
        {
            _market.upgradeTreeGen = false;
            _market2.upgradeTreeGen = false;
        }
    }
}