using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    /*/ public GameObject warriorPrefab;
     public GameObject barbarianPrefab;
     public GameObject wizardPrefab;

     public Transform spawnPoint;
     public ParallaxEffect[] parallaxLayers;
     public CameraFollow cameraFollow;

     void Awake()
     {
         GameObject playerInstance = null;

         switch (GameManager.Instance.selectedCharacter)
         {
             case GameManager.CharacterType.Warrior:
                 playerInstance = Instantiate(warriorPrefab, spawnPoint.position, spawnPoint.rotation);
                 break;
             case GameManager.CharacterType.Barbarian:
                 playerInstance = Instantiate(barbarianPrefab, spawnPoint.position, spawnPoint.rotation);
                 break;
             case GameManager.CharacterType.Wizard:
                 playerInstance = Instantiate(wizardPrefab, spawnPoint.position, spawnPoint.rotation);
                 break;
         }

         if (playerInstance != null)
         {
             // Camera follows the player
             if (cameraFollow != null)
                 cameraFollow.SetTarget(playerInstance.transform);

             // Backgrounds follow the player
             foreach (ParallaxEffect layer in parallaxLayers)
             {
                 layer.SetFollowTarget(playerInstance.transform);
             }
         }

     }/*/
}
