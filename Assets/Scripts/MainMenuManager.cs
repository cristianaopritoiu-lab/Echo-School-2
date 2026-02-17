using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour

{
    public void Play()

    {
        SceneManager.LoadScene("idk 1");
    }

    public void Story()

    {
        SceneManager.LoadScene("Story");
    }


    public void Quit()

    {

        Application.Quit();

    }

}