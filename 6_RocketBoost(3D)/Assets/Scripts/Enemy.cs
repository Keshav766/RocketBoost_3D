using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] float enemySpeed = 30f;
    [SerializeField] ParticleSystem explosionParticle;

    GameObject playerRef;
    Rigidbody enemyRB;
    public float enemyHealth = 2000f;
    public float enemyMaxHealth = 2000f;
    bool playerDetected = false;

    public Slider enemyHealthSlider;
    public Image fill;

    // Start is called before the first frame update
    void Start()
    {
        enemyRB = GetComponent<Rigidbody>();
        playerRef = GameObject.Find("Player");
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
            enemyHealth -= 500f;
            if(enemyHealth <= 0)
            {
                KillEnemy();
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        playerDetected = true;    
    }

    void ProcessHealthBar()
    {
       
        enemyHealthSlider.value = enemyHealth;
        
    }

    void KillEnemy()
        {
            Instantiate(explosionParticle, gameObject.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    
}
