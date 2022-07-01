using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 2f;
    [SerializeField] AudioClip finish;
    [SerializeField] AudioClip crash;

    AudioSource aS;

    bool isTransitioning = false;

    void Start()
    {
        aS = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision other) 
    {
        if (isTransitioning) { return; }

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
        isTransitioning = true;
        aS.Stop();
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", 1f);
        aS.PlayOneShot(crash);
    }

    void StartFinishSequence()
    {
        isTransitioning = true;
        aS.Stop();
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