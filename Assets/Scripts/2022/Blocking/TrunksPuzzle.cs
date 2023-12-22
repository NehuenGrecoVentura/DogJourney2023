using UnityEngine.UI;
using UnityEngine;

public class TrunksPuzzle : MonoBehaviour
{
    public Text text1;
    public Text text2;
    public Text textBack;
    public Text trunksAmmount;
    [HideInInspector] public int ammount;
    Tree2022[] _trees;
    DogHelpTrunk _dogHelp;

    void Start()
    {
        _trees = FindObjectsOfType<Tree2022>();
        _dogHelp = FindObjectOfType<DogHelpTrunk>();
        textBack.gameObject.SetActive(false);
    }

    void Update()
    {
        trunksAmmount.text = ammount.ToString();
        if (ammount == 30 && ammount < 60) _dogHelp.HelpTrunk30();
        else if ((ammount == 60 || ammount == 65) && ammount < 100) _dogHelp.HelpTrunk60();
        else if (ammount >= 100)
        {
            ammount = 100;
            Destroy(text1);
            Destroy(text2);
            Destroy(trunksAmmount);
            textBack.gameObject.SetActive(true);
            foreach (var tree in _trees) Destroy(tree);
        }
    }
}