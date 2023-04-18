using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movements : MonoBehaviour
{

    Rigidbody rocketBody;
    AudioSource rocketAudio;

    [SerializeField] float thrustAmount = 1f;
    [SerializeField] float rotationAmount = 1f;
    [SerializeField] AudioClip mainEngine;
    
    [SerializeField] ParticleSystem leftThrustParticle;
    [SerializeField] ParticleSystem rightThrustParticle;
    [SerializeField] ParticleSystem thrustParticle;

    void Start()
    {
        rocketBody = GetComponent<Rigidbody>();
        rocketAudio = GetComponent<AudioSource>();
    }

    void Update()
    {
        ProcessThrust(); 
        ProcessRotation();
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

    void Thrusting()
    {
        // Debug.Log("Thrusting");
        // rocketBody.AddRelativeForce(0 ,thrustAmount * Time.deltaTime , 0);
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
    }


    void ApplyRotation(float rotationThisFrame)
    {
        rocketBody.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rocketBody.freezeRotation = false;
    }
}
