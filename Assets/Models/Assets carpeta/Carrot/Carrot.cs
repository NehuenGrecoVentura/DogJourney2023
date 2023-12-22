using UnityEngine;
using UnityEngine.AI;

public class Carrot : MonoBehaviour
{
    public bool carrotInArea = false;
    public float timeToDestruction;
    public float speed;
    public GameObject areaRabbit;
    public bool canLootRabbit = false;
    [SerializeField] private RabbitHide _scriptHide;
    public GameObject area;
    public ParticleSystem effectEat;
    public AudioSource audioRabbit;
    public GameObject iconArea;
    private BoxCollider _col;

    private void Awake()
    {
        gameObject.AddComponent<Grabbable>();
        iconArea.gameObject.SetActive(false);
        _col = GetComponent<BoxCollider>();
    }

    private void Start()
    {
        effectEat.Stop();
    }

    public void Update()
    {
        if (carrotInArea)
        {
            _col.enabled = false;
            timeToDestruction -= speed * Time.deltaTime;
            if (timeToDestruction <= 0)
            {
                timeToDestruction = 0;
                carrotInArea = false;
                canLootRabbit = true;
                effectEat.Play();
                audioRabbit.Play();
                Destroy(gameObject);
            }
        }
    }


    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "Rabbit Hide") iconArea.gameObject.SetActive(true);


        if (other.gameObject.name == "Rabbit Hide" && Input.GetKeyDown(KeyCode.F))
        {
            carrotInArea = true;
            Destroy(iconArea);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Rabbit Hide") iconArea.gameObject.SetActive(false);
    }


}
