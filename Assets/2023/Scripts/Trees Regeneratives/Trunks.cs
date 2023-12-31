using UnityEngine;

public class Trunks : MonoBehaviour
{
    [SerializeField] GameObject _iconInteractive;
    [SerializeField] KeyCode _buttonInteractive;
    [HideInInspector] public bool isUpgraded = false;
    private CharacterInventory _inventory;

    private void Awake()
    {
        _inventory = FindObjectOfType<CharacterInventory>();
    }

    void Start()
    {
        _iconInteractive.SetActive(false);
    }

    public void UpgradeTrolley()
    {
        // Si pertenece al arbol verde...
        if (gameObject.layer == 3)
        { 
            // Si no compr� el upgrade, entonces junta de a uno.
            if (!isUpgraded) _inventory.greenTrees++;

            // Sino junta de a 10 (SI EST� EN LA QUEST 6 SUMA DE A UNO PARA EL CONTADOR)
            else _inventory.greenTrees += 10;
        }

        // Si es del arbol p�rpura, usa la misma l�gica que la del verde.
        else
        {
            if (!isUpgraded) _inventory.purpleTrees++;
            else _inventory.purpleTrees += 10; 
        } 
    }

    private void OnTriggerEnter(Collider other)
    {
        var dog = other.GetComponent<Dog>();

        if (dog != null)
        {
            UpgradeTrolley();
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        var player = other.GetComponent<Character>();

        if (player != null)
        {
            if (!Input.GetKeyDown(_buttonInteractive))
                _iconInteractive.SetActive(true);

            else _iconInteractive.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null) _iconInteractive.SetActive(false);
    }
}