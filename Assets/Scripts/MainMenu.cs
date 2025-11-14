using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioSource musicTrack;

    private void Start()
    {
        musicTrack.Play();
    }

    public void PlayGame()
    {
        SceneManager.LoadSceneAsync("Level 1");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadSceneAsync("Main Menu");
    }

    public void PlayerSelect()
    {
        SceneManager.LoadSceneAsync("Player Select");
    }
    

}
