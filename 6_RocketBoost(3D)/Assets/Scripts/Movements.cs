using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;


public class Movements : MonoBehaviour
{

    Rigidbody rocketBody;
    public bool shortGunOn = false;

    [SerializeField] float thrustAmount = 1f;
    [SerializeField] float rotationAmount = 1f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip sideEngine;
    [SerializeField] AudioClip plasmaBulletSound;
    [SerializeField] public int bulletCount = 20;

    //[SerializeField] AudioSource BGM;
    [SerializeField] AudioSource mainEngineAudioSource;
    [SerializeField] AudioSource sideEngineAudioSource;
    [SerializeField] AudioSource plasmaBulletAudioSource;
    [SerializeField] TMP_Text plasmaBulletText;
    [SerializeField] ParticleSystem leftThrustParticle;
    [SerializeField] ParticleSystem rightThrustParticle;
    [SerializeField] ParticleSystem thrustParticle;
    [SerializeField] GameObject playerBulletRef;
    [SerializeField] GameObject plasmaBullet;
    [SerializeField] GameObject[] shipBulletRef;

    void Start()
    {
        rocketBody = GetComponent<Rigidbody>();
        //mainEngineAudioSource = GetComponent<AudioSource>();
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
        if(bulletCount <= 0)
        {
            plasmaBulletText.text = "Out of Plasma";
            return;
        }
            plasmaBulletText.text = "Plasma : " + bulletCount.ToString();
            if (Input.GetKeyDown(KeyCode.RightControl) || Input.GetKeyDown(KeyCode.Keypad0))
                {
                    if(bulletCount > 0)
                    {
                        plasmaBulletAudioSource.PlayOneShot(plasmaBulletSound);
                        if(shortGunOn)
                        {
                            bulletCount -= 5;
                            plasmaBulletText.text = "Plasma : " + bulletCount.ToString();
                            foreach (GameObject X in shipBulletRef)
                            { 
                                Instantiate(plasmaBullet, X.transform.position, X.transform.rotation);
                            }
                        }
                            bulletCount--;
                            plasmaBulletText.text = "Plasma : " + bulletCount.ToString();
                            Instantiate(plasmaBullet, playerBulletRef.transform.position, playerBulletRef.transform.rotation);
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

        if (!mainEngineAudioSource.isPlaying)
        {
            mainEngineAudioSource.PlayOneShot(mainEngine);
        }
    }
    void StopThrusting()
    {
        mainEngineAudioSource.Stop();
        thrustParticle.Stop();
    }

    void RotateLeft()
    {
        ApplyRotation(rotationAmount);
        if (!leftThrustParticle.isPlaying)
        {
            leftThrustParticle.Play();
        }
        if(!sideEngineAudioSource.isPlaying)
        {
            sideEngineAudioSource.PlayOneShot(sideEngine);
        }
    }
    void RotateRight()
    {
        ApplyRotation(-rotationAmount);
        if (!rightThrustParticle.isPlaying)
        {
            rightThrustParticle.Play();
        }
        if (!sideEngineAudioSource.isPlaying)
        {
            sideEngineAudioSource.PlayOneShot(sideEngine);
        }
    }
    void StopRotation()
    {
        leftThrustParticle.Stop();
        rightThrustParticle.Stop();
        //EulerAnglesToQuaternion(transform.rotation, 0, 0, transform.rotation.z);
        if (sideEngineAudioSource.isPlaying)
        {
            sideEngineAudioSource.Stop();
        }
    }


    void ApplyRotation(float rotationThisFrame)
    {
        rocketBody.freezeRotation = true;  //  this freezes the rotations of physics system so that we can apply our rotation
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rocketBody.freezeRotation = false;  // unfreezes the pyysics rotation system
    }


   
}
