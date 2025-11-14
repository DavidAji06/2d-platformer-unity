using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelectButton : MonoBehaviour
{
    public GameManager.CharacterType characterToSelect;

    public void OnSelectCharacter()
    {
        GameManager.Instance.SelectCharacter(characterToSelect);
        SceneManager.LoadScene("Level 1"); // Replace with your Level 1 scene name
    }
}
