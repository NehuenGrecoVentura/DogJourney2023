using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    private void Update()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Puzzle1()
    {
        SceneManager.LoadScene(1);
    }

    public void Puzzle2()
    {
        SceneManager.LoadScene(2);
    }

    public void Puzzle3()
    {
        SceneManager.LoadScene(3);
    }

    public void Puzzle4()
    {
        SceneManager.LoadScene(4);
    }

    public void Tutorial()
    {
        SceneManager.LoadScene(5);
    }

    public void Puzzle1Art()
    {
        SceneManager.LoadScene(6);
    }



    public void Exit()
    {
        Application.Quit();
    }
}