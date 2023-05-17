using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float enemySpeed = 30f;
    
    GameObject playerRef;
    Rigidbody enemyRB;

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
    }

    void FollowPlayer()
    {
        Vector3 playerDirection = (playerRef.transform.position - transform.position);
        enemyRB.AddForce(playerDirection * enemySpeed * Time.deltaTime);
    }
}
