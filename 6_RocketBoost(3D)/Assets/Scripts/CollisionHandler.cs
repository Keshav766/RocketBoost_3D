using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CollisionHandler : MonoBehaviour
{

    [SerializeField] float levelTimeDelay = 1f;
    [SerializeField] AudioClip crashClip;
    [SerializeField] AudioClip finishClip;

    [SerializeField] ParticleSystem crashParticle;
    [SerializeField] ParticleSystem finishParticle;



    AudioSource audioSource;
    ParticleSystem rocketParticles;

    bool collisionHappned = false;
    bool collisionDisable = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        RespondToDebugKeys();   //  CHEATS

    }
    void RespondToDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            collisionDisable = !collisionDisable;
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (collisionHappned || collisionDisable) { return; }

        switch (other.gameObject.tag)
        {
            case "Finish":
                StartSuccessSequence();

                break;
            case "Start":
                Debug.Log("Wassup");
                // CrashSequence();
                break;
            default:
                Debug.Log("You blew up!!");
                CrashSequence();
                break;
        }

    }

    void CrashSequence()
    {
        collisionHappned = true;
        audioSource.Stop();
        audioSource.PlayOneShot(crashClip);
        crashParticle.Play();
        GetComponent<Movements>().enabled = false;
        Invoke("ReloadLevel", levelTimeDelay);
    }

    void StartSuccessSequence()
    {
        collisionHappned = true;
        audioSource.Stop();
        audioSource.PlayOneShot(finishClip);
        finishParticle.Play();
        GetComponent<Movements>().enabled = false;
        Invoke("LoadNextLevel", levelTimeDelay);
    }

    void ReloadLevel()
    {
        collisionHappned = false;
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }
    void LoadNextLevel()
    {
        collisionHappned = false;
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        int nextScene = currentScene + 1;
        if (nextScene == SceneManager.sceneCountInBuildSettings)
        {
            nextScene = 0;
        }
        SceneManager.LoadScene(nextScene);
    }
}
