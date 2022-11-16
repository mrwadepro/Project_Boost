using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{


    [SerializeField] float mainThrust = 100;

    [SerializeField] float rotationSpeed = 100;

    [SerializeField] AudioClip mainEngine;

    Rigidbody rb;
    AudioSource audioData;

    bool isAlive;

    //Play the music
    bool m_Play;
    //Detect when you use the toggle, ensures music isn’t played multiple times
    bool m_ToggleChange;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioData = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
            if (!audioData.isPlaying)
                audioData.PlayOneShot(mainEngine);
        }
        else
        {
            audioData.Stop();
        }


    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {

            ApplyRotation(rotationSpeed);
        }

        else if (Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-rotationSpeed);

        }

    }

    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; // freezing rotation so we can manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false; // unfreezing rotation so we can manually rotate
    }

}
