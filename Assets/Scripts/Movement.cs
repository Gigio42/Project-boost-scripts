using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float rocketTrust = 1f;
    [SerializeField] float rotationSpeed = 1f;

    [SerializeField] ParticleSystem mainBooster;
    [SerializeField] ParticleSystem leftBooster;
    [SerializeField] ParticleSystem rightBooster;

    Rigidbody rB;
    AudioSource aS;

    void Start()
    {
        rB = GetComponent<Rigidbody>();
        aS = GetComponent<AudioSource>();
    }

    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

    void ProcessRotation()
    {
        if(Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }
        else if(Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }
        else
        {
            RotateStop();
        }
    }

    void StartThrusting()
    {
        rB.AddRelativeForce(Vector3.up * rocketTrust * Time.deltaTime);
        if (!mainBooster.isPlaying)
        {
            mainBooster.Play();
        }
        if (!aS.isPlaying)
        {
            aS.Play();
        }
    }

    void StopThrusting()
    {
        mainBooster.Stop();
        aS.Stop();
    }

    void RotateLeft()
    {
        ApplyRotation(rotationSpeed);
        if (!rightBooster.isPlaying)
        {
            rightBooster.Play();
        }
    }

    void RotateRight()
    {
        ApplyRotation(-rotationSpeed);
        if (!leftBooster.isPlaying)
        {
            leftBooster.Play();
        }
    }

    void RotateStop()
    {
        rightBooster.Stop();
        leftBooster.Stop();
    }

    void ApplyRotation(float rotationThisFrame)
    {
        rB.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rB.freezeRotation = false;
    }
}
