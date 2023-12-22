using UnityEngine;

public class InfoIcon2 : MonoBehaviour
{
    [SerializeField] GameObject _iconMaterials;
    [SerializeField] float _speedRot = -4f;

    [Header("MESH RENDER")]
    [SerializeField] Material _matUnlocked;
    MeshRenderer _myMat;
    Construct _construct;

    [SerializeField] GameObject[] _hidenObjsBridge;
    Inventory _inventory;

    public GameObject iconWood;
    public GameObject iconRope;

    private void Awake()
    {
        _myMat = GetComponent<MeshRenderer>();
        
        _inventory = FindObjectOfType<Inventory>();
    }

    void Start()
    {
        _construct = GetComponent<Construct>();
        _iconMaterials.SetActive(false);
        _construct.enabled = false;

        foreach (var item in _hidenObjsBridge)
            item.SetActive(false);
    }

    private void FixedUpdate()
    {
        RotateIcon(0, _speedRot, 0);
    }

    public void UnlockBridge2()
    {
        _myMat.material = _matUnlocked;
        _construct.enabled = true;
        Destroy(this);
    }

    void RotateIcon(float x, float y, float z)
    {
        transform.Rotate(x, y, z);
    }

    private void OnTriggerStay(Collider other)
    {
        var player = other.GetComponent<Character2022>();
        if (player != null)
        {
            if (!Input.GetKey(KeyCode.Space)) _iconMaterials.SetActive(true);
            else if (Input.GetKey(KeyCode.Space))
            {
                if (_inventory.amountWood < 20) iconWood.GetComponent<Renderer>().material.color = Color.red;
                else iconWood.GetComponent<Renderer>().material.color = Color.white;

                iconRope.GetComponent<Renderer>().material.color = Color.red;
            }

            else if (Input.GetKeyUp(KeyCode.Space))
            {
                player.EjecuteAnim("Idle");
                iconWood.GetComponent<Renderer>().material.color = Color.white;
                iconRope.GetComponent<Renderer>().material.color = Color.white;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<Character2022>();
        if (player != null)
        {
            _iconMaterials.SetActive(false);
            iconWood.GetComponent<Renderer>().material.color = Color.white;
            iconRope.GetComponent<Renderer>().material.color = Color.white;
        }
            
            
    }
}