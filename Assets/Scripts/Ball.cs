using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public Vector3 launchVelocity;
   [HideInInspector]
    public bool inPlay = false;

    private Rigidbody rigidBody;
    private AudioSource audioSourse;
    private Vector3 startPosition;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.useGravity = false;
        startPosition = transform.position;
    }

    public void Launch(Vector3 velocity)
    {
        inPlay = true;
        rigidBody.useGravity = true;
        rigidBody.velocity = velocity;
        audioSourse = GetComponent<AudioSource>();
        audioSourse.Play();
    }

    public void ResetBall()
    {
        inPlay = false;
        rigidBody.velocity = Vector3.zero;
        rigidBody.angularVelocity = Vector3.zero;
        rigidBody.useGravity = false;
        transform.position = startPosition;
        transform.rotation = Quaternion.identity;
    }

    private void Reset()
    {
    /*    inPlay = false;
        rigidBody.velocity = Vector3.zero;
        rigidBody.angularVelocity = Vector3.zero;
        rigidBody.useGravity = false;
        transform.position = startPosition;
        transform.rotation = Quaternion.identity;*/
    }
}
