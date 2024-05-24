using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaFactory : MonoBehaviour
{
    //Singleton para tener solo una referencia global de este factory
    public static BalaFactory Instance
    {
        get
        {
            
            return _instance;
        }
    }

    static BalaFactory _instance;

    public Bala BalaPrefab; //Prefab del objeto que va a contener el pool
    public int BalaStock = 5;  //Cantidad de objetos que se van a crear al inicio


    public BalaPool<Bala> pool; //El pool de mis balas

    // Start is called before the first frame update
    // Start is called before the first frame update
    void Start()
    {
        _instance = this;

        //Creo un nuevo pool pasandole:
        //1.- La funcion que contiene la logica de instanciar el objeto (factoryMethod)
        //2.- La funcion que contiene la logica de que hacer al pedir el objeto (turnOnCallback)
        //3.- La funcion que contiene la logica de que hacer al devolver el objeto al pool (turnOffCallback)
        //4.- La cantidad de objetos que se crearan al inicio (initialStock)
        //5.- Si es dinamico o no, generalmente va a ser true (isDynamic)
        pool = new BalaPool<Bala>(BalaSpawner, Bala.TurnOn, Bala.TurnOff, BalaStock, true);
    }
    
    //Funcion que contiene la logica de la instanciacion de la bala
    public Bala BalaSpawner()
    {
        return Instantiate(BalaPrefab);
    }

    //Funcion que en este caso se llama desde la bala para ejecutar la devolucion del objeto al pool
    public void ReturnBala(Bala b)
    {
        pool.ReturnObject(b);
    }
}
