using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public enum CharacterType { Warrior, Barbarian, Wizard }
    public CharacterType selectedCharacter;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keep between scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SelectCharacter(CharacterType character)
    {
        selectedCharacter = (CharacterType)character;
    }
}
