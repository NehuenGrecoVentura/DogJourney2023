using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TradeFish : MonoBehaviour
{
    [SerializeField] bool[] _trades;

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