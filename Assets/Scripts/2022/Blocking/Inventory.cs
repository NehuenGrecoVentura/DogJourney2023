using UnityEngine;
using TMPro;

public class Inventory : MonoBehaviour
{
    [Header("ICONS INVENTORY")]
    public GameObject iconBag;
    public GameObject inventory;
    private bool _openInventory = false;

    [Header("AMOUNTS IN INVENTORY")]
    public int amountWood = 0;
    public int amountnails = 0;
    public int amountRopes = 0;
    public int amountPurple = 0;
    public int amountOil = 0;
    public int money = 0;

    [Header("UPGRADE")]
    public bool upgrade = false;
    public GameObject iconUpgrade;

    [Header("TEXTS")]
    public TMP_Text textMoney;
    public TMP_Text textNails;
    public TMP_Text textWood;
    public TMP_Text textRope;
    public TMP_Text textWoodPurple;
    public TMP_Text textOil;

    private void Start()
    {
        _openInventory = false;
        iconUpgrade.SetActive(false);
        iconBag.SetActive(true);
        inventory.SetActive(false);
    }

    void Update()
    {
        UIInventory();
    }

    void UIInventory()
    {
        textMoney.text = money.ToString();
        textNails.text = amountnails.ToString();
        textWood.text = amountWood.ToString();
        textRope.text = amountRopes.ToString();
        textWoodPurple.text = amountPurple.ToString();
        textOil.text = amountOil.ToString();

        if (upgrade) iconUpgrade.SetActive(true);
        else iconUpgrade.SetActive(false);

        if (Input.GetKeyDown(KeyCode.I))
        {
            if (!_openInventory)
            {
                iconBag.SetActive(false);
                inventory.SetActive(true);
                _openInventory = true;
            }

            else
            {
                iconBag.SetActive(true);
                inventory.SetActive(false);
                _openInventory = false;
            }
        }
    }
}