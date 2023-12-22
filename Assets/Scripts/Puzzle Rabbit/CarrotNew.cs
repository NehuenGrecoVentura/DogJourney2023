using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrotNew : MonoBehaviour
{
    public bool carrotInArea = false;
    public float timeToDestruction;
    public float speed;
    public GameObject areaRabbit;
    public bool canLootRabbit = false;
    [SerializeField] private RabbitHide2 _scriptHide;
    public GameObject area;
    public ParticleSystem effectEat;

    private void Awake()
    {
        gameObject.AddComponent<Grabbable>();
    }

    private void Start()
    {
        effectEat.Stop();
    }

    public void Update()
    {
        if (carrotInArea)
        {
            timeToDestruction -= speed * Time.deltaTime;
            if (timeToDestruction <= 0)
            {
                Destroy(areaRabbit);
                carrotInArea = false;
                canLootRabbit = true;
                effectEat.Play();
                gameObject.SetActive(false);
            }
        }
    }




    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "Rabbit Hide" && Input.GetKeyDown(KeyCode.F))
        {
            carrotInArea = true;
        }
    }
}
