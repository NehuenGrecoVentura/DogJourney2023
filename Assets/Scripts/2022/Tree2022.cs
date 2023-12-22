using UnityEngine;
using UnityEngine.UI;

public class Tree2022 : MonoBehaviour
{
    Collider _col;
    Tree2022 _myScript;
    BallDog _ballDog;
    public Image iconAxe;
    public KeyCode buttonCutTree = KeyCode.Mouse0;
    public GameObject trunkTree;
    public Text textBall;
    public ParticleSystem particle;
    public AudioClip soundCutTree;
    TrunksPuzzle trunkPuzzle;
    DogOrder2022 _dogOrder;
    Character2022 _player;
    AudioSource _myAudio;

    private void Start()
    {
        _myScript = GetComponent<Tree2022>();
        _col = GetComponent<Collider>();
        iconAxe.gameObject.SetActive(false);
        trunkTree.gameObject.SetActive(false);
        trunkPuzzle = FindObjectOfType<TrunksPuzzle>();
        _ballDog = FindObjectOfType<BallDog>();
        textBall.gameObject.SetActive(false);
        _dogOrder = FindObjectOfType<DogOrder2022>();
        _dogOrder.enabled = false;
        _player = FindObjectOfType<Character2022>();
        _myAudio = GetComponent<AudioSource>();
    }

    public void CutTree()
    {
        _myAudio.PlayOneShot(soundCutTree);
        trunkPuzzle.ammount += 10;
        trunkTree.gameObject.SetActive(true);
        iconAxe.gameObject.SetActive(false);
        Destroy(_col);
        _player.EjecuteAnim("Cut");
        Destroy(_myScript);
        Destroy(gameObject);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<Axe2022>() && !Input.GetKeyDown(buttonCutTree)) iconAxe.gameObject.SetActive(true);
        else if (other.gameObject.GetComponent<Axe2022>() && Input.GetKeyDown(buttonCutTree)) CutTree();

        if (other.gameObject.GetComponent<Axe2022>() && Input.GetKeyDown(buttonCutTree) && gameObject.name == "Tree Ball") // Si corto el árbol con la pelota
        {
            Destroy(textBall);
            Destroy(_ballDog.GetComponent<Rigidbody>());
            _ballDog.transform.position = _ballDog.ground.position;
            if (_ballDog.transform.position.y < 7) _ballDog.GetComponent<Collider>().isTrigger = true;
            _dogOrder.enabled = true;
            particle.Play();
            CutTree();
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<Axe2022>()) iconAxe.gameObject.SetActive(false);
    }
}
