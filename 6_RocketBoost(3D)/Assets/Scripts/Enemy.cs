using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] float enemySpeed = 30f;
    [SerializeField] ParticleSystem explosionParticle;
    [SerializeField] bool amIInvisible = false;
    [SerializeField] GameObject[] enemyChilds;
    [SerializeField] GameObject landingPadRef;
    [SerializeField] GameObject explosionSound;

    GameObject playerRef;
    Renderer enemyBodyRenderer;
    Collider enemyBodyCollider;
    Rigidbody enemyRB;
    public float enemyHealth = 2000f;
    public float enemyMaxHealth = 2000f;
    public bool playerDetected = false;

    public Slider enemyHealthSlider;
    public Image fill;

    // Start is called before the first frame update
    void Start()
    {
        enemyRB = GetComponent<Rigidbody>();
        playerRef = GameObject.Find("Player");
        enemyBodyRenderer = GetComponent<Renderer>();
        enemyBodyCollider = GetComponent<Collider>();
        if(amIInvisible)
        {
            MakeEnemyInvisible(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        FollowPlayer();
        ProcessHealthBar();
    }

    void FollowPlayer()
    {
        if(playerDetected)
        {
            Vector3 playerDirection = (playerRef.transform.position - transform.position);
            enemyRB.AddForce(playerDirection * enemySpeed * Time.deltaTime);
        }
    }



    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullets")
        {
            playerDetected = true;  
            enemyHealth -= 500f;
            if(enemyHealth <= 0)
            {
                KillEnemy();
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            MakeEnemyInvisible(true);
            playerDetected = true;    
        }
    }

    void ProcessHealthBar()
    {
        enemyHealthSlider.value = enemyHealth;   
    }

    void KillEnemy()
        {
            Instantiate(explosionParticle, gameObject.transform.position, Quaternion.identity);
            Instantiate(explosionSound, gameObject.transform.position, Quaternion.identity);
        if (gameObject.tag == "BigPoppa")
            {
                Instantiate(landingPadRef, gameObject.transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
        }

    void MakeEnemyInvisible(bool passedValue)
    {
        enemyBodyRenderer.enabled = passedValue;
        enemyBodyCollider.enabled = passedValue;
        foreach (GameObject x in enemyChilds)
        {
            x.SetActive(passedValue);
            Debug.Log("called " + passedValue);
        }
    }
    
}
