using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TradeFish : MonoBehaviour
{
    [SerializeField] bool[] _trades;

    [SerializeField] Sprite _spriteCommonFish;
    [SerializeField] Sprite _spriteSpecialFish;
    [SerializeField] Sprite _spriteFlowers;
    [SerializeField] Sprite _spriteNail;
    [SerializeField] Sprite _spriteApple;

    [Header("TEXT VALUES")]
    [SerializeField] TMP_Text _txtValueBait;
    [SerializeField] TMP_Text _txtValueApple;
    [SerializeField] TMP_Text _txtValueCommonFish;
    [SerializeField] TMP_Text _txtValueSpecialFish;
    [SerializeField] TMP_Text _txtValueNormalFlower;
    [SerializeField] TMP_Text _txtValueSpecialFlower;
    [SerializeField] TMP_Text _txtValueNail;

    [Header("TEXT AMOUNT")]
    [SerializeField] TMP_Text _txtAmountBait;
    [SerializeField] TMP_Text _txtAmountApple;
    [SerializeField] TMP_Text _txtAmountCommonFish;
    [SerializeField] TMP_Text _txtAmountSpecialFish;
    [SerializeField] TMP_Text _txtAmountNormalFlower;
    [SerializeField] TMP_Text _txtAmountSpecialFlower;
    [SerializeField] TMP_Text _txtAmountNail;

    #region NO BORRAR - EVENTS TRIGGERS PARA BOTONES

    //public void TradeCommonFish()
    //{
    //    if (_trades.Length > 0)
    //    {
    //        _trades[0] = true;

    //        for (int i = 1; i < _trades.Length; i++)
    //        {
    //            _trades[i] = false;
    //        }
    //    }
    //    else return;
    //}

    //public void TradeSpecialFish()
    //{
    //    if (_trades.Length >= 2)
    //    {
    //        for (int i = 0; i < _trades.Length; i++)
    //        {
    //            _trades[i] = false;
    //        }

    //        _trades[1] = true;
    //    }
    //    else return;
    //}


    //public void TradeNormalFlowers()
    //{
    //    if (_trades.Length >= 3)
    //    {
    //        for (int i = 0; i < _trades.Length; i++)
    //        {
    //            _trades[i] = false;
    //        }

    //        _trades[2] = true;
    //    }
    //    else return;
    //}

    //public void TradeSpecialFlowers()
    //{
    //    if (_trades.Length >= 4)
    //    {
    //        for (int i = 0; i < _trades.Length; i++)
    //        {
    //            _trades[i] = false;
    //        }

    //        _trades[3] = true;
    //    }
    //    else return;
    //}

    //public void TradeNails()
    //{
    //    if (_trades.Length >= 5)
    //    {
    //        for (int i = 0; i < _trades.Length; i++)
    //        {
    //            _trades[i] = false;
    //        }

    //        _trades[4] = true;
    //    }
    //    else return;
    //}

    //public void TradeApples()
    //{
    //    if (_trades.Length >= 6)
    //    {
    //        for (int i = 0; i < _trades.Length; i++)
    //        {
    //            _trades[i] = false;
    //        }

    //        _trades[5] = true;
    //    }
    //    else return;
    //}

    #endregion

    private void TradeItem(int index)
    {
        if (index >= 0 && index < _trades.Length)
        {
            for (int i = 0; i < _trades.Length; i++)
            {
                _trades[i] = (i == index);
            }
        }
        else return;
    }

    public void TradeCommonFish()
    {
        TradeItem(0);

    }

    public void TradeSpecialFish()
    {
        TradeItem(1);
    }

    public void TradeNormalFlowers()
    {
        TradeItem(2);
    }

    public void TradeSpecialFlowers()
    {
        TradeItem(3);
    }

    public void TradeNails()
    {
        TradeItem(4);
    }

    public void TradeApples()
    {
        TradeItem(5);
    }
}