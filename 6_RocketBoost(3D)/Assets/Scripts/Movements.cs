using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;


public class Movements : MonoBehaviour
{

    Rigidbody rocketBody;
    AudioSource rocketAudio;

    [SerializeField] float thrustAmount = 1f;
    [SerializeField] float rotationAmount = 1f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] public int bulletCount = 20;

    [SerializeField] TMP_Text plasmaBulletText;
    [SerializeField] ParticleSystem leftThrustParticle;
    [SerializeField] ParticleSystem rightThrustParticle;
    [SerializeField] ParticleSystem thrustParticle;
    [SerializeField] GameObject playerBulletRef;
    [SerializeField] GameObject plasmaBullet;

    void Start()
    {
        rocketBody = GetComponent<Rigidbody>();
        rocketAudio = GetComponent<AudioSource>();
        
    }

    void Update()
    {
        ProcessThrust();
        ProcessRotation();
        ProcessFiringPlasmaBullets();
    }

    void ProcessThrust()
    {

        if (Input.GetKey(KeyCode.Space))
        {
            Thrusting();
        }
        else
        {
            StopThrusting();
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            RotateLeft();

        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            RotateRight();

        }
        else
        {
            StopRotation();
        }
    }

    void ProcessFiringPlasmaBullets()
    {
        plasmaBulletText.text = "Plasma : " + bulletCount.ToString();
        if (Input.GetKeyDown(KeyCode.RightControl) || Input.GetKeyDown(KeyCode.Keypad0))
            {
                if(bulletCount > 0)
                {
                    bulletCount--;
                    plasmaBulletText.text = "Plasma : " + bulletCount.ToString();
                    Instantiate(plasmaBullet, playerBulletRef.transform.position, playerBulletRef.transform.rotation);
                }
            else
            {
                plasmaBulletText.text = "Out of Plasma";
            }

        }
    }

    void Thrusting()
    {
        rocketBody.AddRelativeForce(Vector3.up * thrustAmount * Time.deltaTime);
        if (!thrustParticle.isPlaying)
        {
            thrustParticle.Play();
        }

        if (!rocketAudio.isPlaying)
        {
            rocketAudio.PlayOneShot(mainEngine);
        }
    }
    void StopThrusting()
    {
        rocketAudio.Stop();
        thrustParticle.Stop();
    }

    void RotateLeft()
    {
        ApplyRotation(rotationAmount);
        if (!leftThrustParticle.isPlaying)
        {
            leftThrustParticle.Play();
        }
    }
    void RotateRight()
    {
        ApplyRotation(-rotationAmount);
        if (!rightThrustParticle.isPlaying)
        {
            rightThrustParticle.Play();
        }
    }
    void StopRotation()
    {
        leftThrustParticle.Stop();
        rightThrustParticle.Stop();
        //EulerAnglesToQuaternion(transform.rotation, 0, 0, transform.rotation.z);
    }


    void ApplyRotation(float rotationThisFrame)
    {
        rocketBody.freezeRotation = true;  //  this freezes the rotations of physics system so that we can apply our rotation
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rocketBody.freezeRotation = false;  // unfreezes the pyysics rotation system
    }


    /*void EulerAnglesToQuaternion(Quaternion q, double x, double y, double z)
    {
        float cx = (float)Math.Cos(x * 0.5);
        float cy = (float)Math.Cos(y * 0.5);
        float cz = (float)Math.Cos(z * 0.5);
        float sx = (float)Math.Sin(x * 0.5);
        float sy = (float)Math.Sin(y * 0.5);
        float sz = (float)Math.Sin(z * 0.5);

        q.w = (cz * cx * cy) + (sz * sx * sy);
        q.x = (cz * sx * cy) - (sz * cx * sy);
        q.y = (cz * cx * sy) + (sz * sx * cy);
        q.z = (sz * cx * cy) - (cz * sx * sy);
    }
    */
}
