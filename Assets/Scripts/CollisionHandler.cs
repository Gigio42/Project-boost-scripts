using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 2f;
    [SerializeField] AudioClip finish;
    [SerializeField] AudioClip crash;

    [SerializeField] ParticleSystem finishFX;
    [SerializeField] ParticleSystem crashFX;

    AudioSource aS;

    bool isTransitioning = false;
    bool collisionDisabled = false;

    void Start()
    {
        aS = GetComponent<AudioSource>();
    }

    void Update()
    {
        RespondToDebugKeys();
    }

    void RespondToDebugKeys()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            ReloadNextLevel();
        }

        if(Input.GetKeyDown(KeyCode.C))
        {
            collisionDisabled = !collisionDisabled;
        }
    }

    void OnCollisionEnter(Collision other) 
    {
        if (isTransitioning || collisionDisabled) { return; }

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
        crashFX.Play();
        
    }

    void StartFinishSequence()
    {
        isTransitioning = true;
        aS.Stop();
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadNextLevel", levelLoadDelay);
        aS.PlayOneShot(finish);
        finishFX.Play();
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