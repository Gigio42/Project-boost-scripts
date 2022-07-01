using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float rocketTrust = 1f;
    [SerializeField] float rotationSpeed = 1f;
    [SerializeField] AudioClip mainEngine;

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
            rB.AddRelativeForce(Vector3.up * rocketTrust * Time.deltaTime);
            if (!aS.isPlaying)
            {
                aS.Play();
            } 
            
        }
        else
        {
            aS.Stop();
        }
    }

    void ProcessRotation()
    {
        if(Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotationSpeed);
        }
        else if(Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-rotationSpeed);
        }
    }

    void ApplyRotation(float rotationThisFrame)
    {
        rB.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rB.freezeRotation = false;
    }
}
