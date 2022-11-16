using System;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CollisionHandler : MonoBehaviour
{
    Movement movement;
    [SerializeField] float resetDelay = 2f;
    [SerializeField] AudioClip successClip;
    [SerializeField] AudioClip crashClip;
    AudioSource audioData;

    bool isTransitioning = false;
    void Start()
    {
        movement = GetComponent<Movement>();
        audioData = GetComponent<AudioSource>();


    }
    void OnCollisionEnter(Collision other)
    {
        if (isTransitioning)
        {
            return;
        }
        switch (other.gameObject.tag)
        {
            case "Friendly":
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    void StartSuccessSequence()
    {
        isTransitioning = true;
        audioData.Stop();
        audioData.PlayOneShot(successClip);


        movement.enabled = false;
        Invoke("nextLevel", resetDelay);
    }

    void StartCrashSequence()
    {
        isTransitioning = true;

        audioData.Stop();
        audioData.PlayOneShot(crashClip);

        // todo add particle effect
        movement.enabled = false;
        Invoke("ReloadLevel", resetDelay);

    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);


    }
    void nextLevel()
    {

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }

        SceneManager.LoadScene(nextSceneIndex);


    }
}
