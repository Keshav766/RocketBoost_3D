using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;


public class CollisionHandler : MonoBehaviour
{

    [SerializeField] float levelTimeDelay = 1f;
    [SerializeField] AudioClip crashClip;
    [SerializeField] AudioClip finishClip;

    [SerializeField] ParticleSystem crashParticle;
    [SerializeField] ParticleSystem finishParticle;

    public Slider healthSlider;
    public Gradient healthGradient;
    public Image fill;
    public float playerHealth = 5000f;
    float playerMaxHealth = 5000f;
    AudioSource audioSource;
    ParticleSystem rocketParticles;
    Movements movRef;

    bool collisionHappned = false;
    bool collisionDisable = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        movRef = FindObjectOfType<Movements>();
    }

    void Update()
    {
        RespondToDebugKeys();   //  CHEATS
        ProcessHealthBar();
    }
    void RespondToDebugKeys()
    {
        //if (Input.GetKeyDown(KeyCode.C))
        //{
         //   collisionDisable = !collisionDisable;
       // }
        if (Input.GetKey(KeyCode.L) && Input.GetKey(KeyCode.O))
        {
            LoadNextLevel();
        }
        if (Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.Keypad5))
        {
            RecenterShip();
        }
        else if(Input.GetKeyDown(KeyCode.Home))
        {
            CrashSequence();
        }
    }

    void RecenterShip()
    {
        transform.rotation = Quaternion.identity;
    }

    void ProcessHealthBar()
    {
        float currHealth = playerHealth / playerMaxHealth;
        healthSlider.value = playerHealth;
        fill.color = healthGradient.Evaluate(currHealth);
    }

    void OnCollisionEnter(Collision other)
    {
        if (collisionHappned || collisionDisable) { return; }

        switch (other.gameObject.tag)
        {
            case "Finish":
                StartSuccessSequence();
                break;
            case "Obstacle":
                ProcessHealth(300f);
                if (playerHealth <= 0)
                {
                    CrashSequence();
                }
                break;
            case "Enemy":
                ProcessHealth(900f);
                if (playerHealth <= 0)
                {
                    CrashSequence();
                }
                break;
            case "Ground":
                ProcessHealth(100f);
                if (playerHealth <= 0)
                {
                    CrashSequence();
                }
                break;
        }

    }

    void ProcessHealth(float amountToChange)
    {
        playerHealth -= amountToChange;
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
    public void LoadNextLevel()
    {
        collisionHappned = false;
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        int nextScene = currentScene + 1;
        if (nextScene == SceneManager.sceneCountInBuildSettings)
        {
            Debug.Log("You have beaten the game");
            return;
        }
        SceneManager.LoadScene(nextScene);
    }

    void OnTriggerEnter(Collider other)
    {
        switch(other.tag)
        {
            case "HeartPickup":
                ProcessHealth(-1500);
                break;
            case "PlasmaPickup":
                movRef.bulletCount += 20;
                break;
        }
    }
}
