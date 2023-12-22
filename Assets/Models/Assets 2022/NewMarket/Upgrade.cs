using UnityEngine;
using UnityEngine.UI;

public class Upgrade : MonoBehaviour
{
    [SerializeField] private Color[] _colorsStatus;
    [SerializeField] private GameObject _description;
    [SerializeField] private GameObject _iconBox;

    private Image _myImage;
    private Inventory _inventory;
    private TreeRegen[] _trees;
    private Character2022 _players;
    private Dog2022[] _dogs;
    private FeedbackMarket _animMessage;

    public bool activeTutorialDogs = false;
    public bool dogsAdded = false;
    private NewMarket[] _markets;

    private void Awake()
    {
        _myImage = GetComponent<Image>();
        _inventory = FindObjectOfType<Inventory>();
        _trees = FindObjectsOfType<TreeRegen>();
        _players = FindObjectOfType<Character2022>();
        _dogs = FindObjectsOfType<Dog2022>();
        _animMessage = FindObjectOfType<FeedbackMarket>();
        _markets = FindObjectsOfType<NewMarket>();
    }

    private void Start()
    {
        _description.SetActive(false);
    }

    void Update()
    {
        if (_inventory.upgrade)
            _myImage.color = _colorsStatus[1];

        else _myImage.color = _colorsStatus[0];
    }

    #region PLAYER UPGRADES

    public void BuyAxe()
    {
        // Si tengo una mejora en el inventario y la compro...
        if (_inventory.upgrade)
        {
            _description.SetActive(false); // Desactivo la descripción del upgrade.
            _myImage.color = _colorsStatus[2]; // El boton de compra pasa a un color verde.
            foreach (var tree in _trees) tree.hitDown = 2; // Ahora los árboles se talan más rápido.

            // Activo la animación del mensaje de la notificación del upgrade.
            _animMessage.Message("YOUR HAS BEEN UPGRADED.", "+ 5%", false, false, false, true, false, false, false, false, false);

            Destroy(this); // Destruyo el script para no vuelva a funcionar en el mismo botón.
        }

        // Una vez hecha la compra me quedo sin la mejora en el inventario
        _inventory.upgrade = false;
    }

    public void BuySpeedPlayer()
    {
        // Si tengo una mejora en el inventario y la compro...
        if (_inventory.upgrade)
        {
            _description.SetActive(false); // Desactivo la descripción del upgrade.
            _myImage.color = _colorsStatus[2]; // El boton de compra pasa a un color verde.
            _players._speedAux = 13; // Ahora la velocidad del player es más rápida al caminar.
            _players.speedRun = 20; // Ahora la velocidad del player es más rápida al correr.

            // Activo la animación del mensaje de la notificación del upgrade.
            _animMessage.Message("NOW YOU MOVE FASTER.", " + 3%", false, false, false, false, true, false, false, false, false);

            Destroy(this); // Destruyo el script para no vuelva a funcionar en el mismo botón.
        }

        // Una vez hecha la compra me quedo sin la mejora en el inventario
        _inventory.upgrade = false;
    }

    // REVISAR EN LOS OTROS SCRIPTS PARA PONERLO EN PUBLICO LAS VARIABLES A CAMBIAR.
    public void BuyTreeReg()
    {
        // Si tengo una mejora en el inventario y la compro...
        if (_inventory.upgrade)
        {
            _description.SetActive(false); // Desactivo la descripción del upgrade.
            _myImage.color = _colorsStatus[2]; // El boton de compra pasa a un color verde.

            //Activo el bool para que ahora en los mercados sepan que compré el upgrade de juntar mas madera.
            foreach (var market in _markets)
                market.activeUpgradeTreeReg = true;

            _animMessage.Message("NOW THE TREES GROW QUICKLY.", " + 15%", false, false, true, false, false, false, false, false, false);

            Destroy(this); // Destruyo el script para no vuelva a funcionar en el mismo botón.
        }

        // Una vez hecha la compra me quedo sin la mejora en el inventario
        _inventory.upgrade = false;
    }

    #endregion

    public void BuyTrolley()
    {
        // Si tengo una mejora en el inventario y la compro...
        if (_inventory.upgrade)
        {
            _description.SetActive(false); // Desactivo la descripción del upgrade.
            _myImage.color = _colorsStatus[2]; // El boton de compra pasa a un color verde.


            
            foreach (var market in _markets) 
                market.activeUpgradeTrolley = true;

            _animMessage.Message("THE CART NOW SUPPORTS MORE WOOD.", " + 15%", false, true, false, false, false, false, false, false, false);

            Destroy(this); // Destruyo el script para no vuelva a funcionar en el mismo botón.
        }

        // Una vez hecha la compra me quedo sin la mejora en el inventario
        _inventory.upgrade = false;
    }

    public void BuySpeedDog()
    {
        // Si tengo una mejora en el inventario y la compro...
        if (_inventory.upgrade)
        {
            _description.SetActive(false); // Desactivo la descripción del upgrade.
            _myImage.color = _colorsStatus[2]; // El boton de compra pasa a un color verde.

            foreach (var dog in _dogs) // Ahora los perros se mueven más rápido.
            {
                dog.normalSpeed = 15f;
                dog.runSpeed = 20f;
            }

            _animMessage.Message("YOUR DOG MOVES FASTER.", " + 3%", false, false, false, false, false, true, false, false, false);

            Destroy(this); // Destruyo el script para no vuelva a funcionar en el mismo botón.
        }

        

        // Una vez hecha la compra me quedo sin la mejora en el inventario
        _inventory.upgrade = false;
    }

    public void BuyDog()
    {
        // Si tengo una mejora en el inventario y la compro...
        if (_inventory.upgrade)
        {
            _description.SetActive(false); // Desactivo la descripción del upgrade.
            _myImage.color = _colorsStatus[2]; // El boton de compra pasa a un color verde.

            //////////////


            _animMessage.Message("ADDED A NEW DOG TO CONTROL.", " + 1", true, false, false, false, false, false, true, false, true); // DEJO EL ANTEULTIMO EN FALSE, YA QUE LO ACTIVO DIRECTAMENTE EN EL SCRIPT DE FEEDBACK MARKET

            Destroy(this); // Destruyo el script para no vuelva a funcionar en el mismo botón.
        }

        // Una vez hecha la compra me quedo sin la mejora en el inventario

        dogsAdded = true;
        activeTutorialDogs = true;
        _inventory.upgrade = false;
    }

    #region EVENTS TRIGGERS

    //Events para mostrar la descripción de los upgrades
    public void ShowDescription()
    {
        _description.SetActive(true);
    }

    public void HideDescription()
    {
        _description.SetActive(false);
    }

    #endregion
}