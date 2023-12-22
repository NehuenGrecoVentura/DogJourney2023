using UnityEngine;

public class Nidus : MonoBehaviour
{
    [HideInInspector] public bool eggInNidus = false;
    public GameObject iconEgg;

    private void Start()
    {
        iconEgg.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Egg") iconEgg.gameObject.SetActive(true);
        if (other.gameObject.name=="Egg" && Input.GetKeyDown(KeyCode.F))
        {
            print("HUEVO PLANTADO");
            eggInNidus = true;
            Destroy(iconEgg);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "Egg") iconEgg.gameObject.SetActive(true);
        if (other.gameObject.name == "Egg" && Input.GetKeyDown(KeyCode.F))
        {
            print("HUEVO PLANTADO");
            Destroy(iconEgg);
            eggInNidus = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Egg" && !eggInNidus) iconEgg.gameObject.SetActive(false);
    }
}
