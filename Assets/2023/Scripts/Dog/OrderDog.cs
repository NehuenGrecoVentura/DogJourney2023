using System.Collections.Generic;
using UnityEngine;

public class OrderDog : MonoBehaviour
{
    [SerializeField] List<Dog> _dogs;
    private int _dogCount, _dogNumber;
    [HideInInspector] public bool activeOrders = false;

    private void Awake()
    {
        _dogCount = _dogs.Count;
        _dogNumber = 0;
    }

    private void Update()
    {

        //if (activeOrders)
        //{
        //    if (transform.position.y >= 18f)
        //    {
        //        foreach (var dog in _dogs)
        //            dog.enabled = false;
        //    }

        //    else
        //    {
        //        foreach (var dog in _dogs)
        //            dog.enabled = true;
        //    }
        //}
    }

    public void CallDog()
    {
        if (activeOrders) _dogs[_dogNumber].OrderGo();
    }

    public void StopDog()
    {
        if (activeOrders) _dogs[_dogNumber].Stop();
    }

    public void CallAllDogs()
    {
        if (_dogCount > 1 && activeOrders)
        {
            for (int i = 0; i < _dogCount; i++)
                _dogs[i].OrderGo();
        }
    }

    public void SelectDog(KeyCode inputInitialDog, KeyCode inputNextDog)
    {
        if (Input.GetKeyDown(inputInitialDog)) _dogNumber = 0;
        if (_dogCount >= 2 && Input.GetKeyDown(inputNextDog)) _dogNumber = 1;
    }
}