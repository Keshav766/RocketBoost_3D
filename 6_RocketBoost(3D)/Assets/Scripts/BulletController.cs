using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{

    [SerializeField] float bulletSpeed = 20f;
    [SerializeField] ParticleSystem bulletParticles;
    [SerializeField] Transform parent;

    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * bulletSpeed);
    }

     void OnCollisionEnter(Collision collision)
     {
        DestroyBullet();
     }
    
    void DestroyBullet()
    {
        Instantiate(bulletParticles, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
