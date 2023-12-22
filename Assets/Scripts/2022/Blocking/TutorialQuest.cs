using UnityEngine;
using UnityEngine.UI;

public class TutorialQuest : MonoBehaviour
{
    Inventory _inventory;
    CutTree[] _trees;
    TutorialQuest[] _treesQuestScript;
    [SerializeField] Image _iconLocation; 

    void Start()
    {
        _inventory = FindObjectOfType<Inventory>();
        _trees = FindObjectsOfType<CutTree>();
        _treesQuestScript = FindObjectsOfType<TutorialQuest>();
    }

    private void Update()
    {
        JobCompleted();
    }

    void JobCompleted()
    {
        if (_inventory.amountWood >= 100)
        {
            foreach (var scriptCut in _trees) Destroy(scriptCut);
            foreach (var scriptTutorial in _treesQuestScript) Destroy(scriptTutorial);
            _iconLocation.gameObject.SetActive(true);
        }
    }
}