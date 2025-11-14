using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController instance;
    [SerializeField] Animator transitionAnim;
    [SerializeField] string levelName;

     private void Awake()
     {
         if(instance == null)
         {
             instance = this;
             DontDestroyOnLoad(gameObject);
         }
        /*/ else
         {
             Destroy(gameObject);
         }/*/
    }

    public void NextLevel()
    {
        StartCoroutine(LoadLevel());
    }

    IEnumerator LoadLevel()
    {
        transitionAnim.SetTrigger("End");
        yield return new WaitForSeconds(1);
        SceneManager.LoadSceneAsync(levelName);
        transitionAnim.SetTrigger("Start");
    }
}
