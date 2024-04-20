using UnityEngine;

public class ArrowQuest : MonoBehaviour
{
    [SerializeField] int _rotX = -4;
    //Inventory _inventory;

    //private void Start()
    //{
    //    _inventory = FindObjectOfType<Inventory>();    
    //}

    void Update()
    {
        RotateArrow(_rotX, 0, 0);
        DisabledArrow();
    }

    void RotateArrow(int x, int y, int z)
    {
        transform.Rotate(x, y, z);
    }

    void DisabledArrow()
    {
        //if (_inventory.amountWood >= 100 && gameObject.name == "Arrow Tree") gameObject.SetActive(false);
        //else gameObject.SetActive(true);
    }
}
