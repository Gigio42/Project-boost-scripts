using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 2f;
    [SerializeField] AudioClip finish;
    [SerializeField] AudioClip crash;

    AudioSource aS;

    void Start()
    {
        aS = GetComponent<AudioSource>();
    }

        void OnCollisionEnter(Collision other) 
    {
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("you're bumping in a friendly object");
                break;
            case "Fuel":
                Debug.Log("you're bumping in a fuel");
                break;
            case "Obstacle":
                StartCrashSequence();
                break;
            case "Ground":
                StartCrashSequence();
                break;
            case "Finish":
                StartFinishSequence();
                break;
            default:
                break;
        }
    }

    void StartCrashSequence()
    {
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", 1f);
        aS.PlayOneShot(crash);
    }

    void StartFinishSequence()
    {
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadNextLevel", levelLoadDelay);
        aS.PlayOneShot(finish);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void ReloadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex+1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
}