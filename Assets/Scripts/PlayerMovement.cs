using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    Vector2 moveInput;
    Rigidbody2D myRigidbody;

    [SerializeField] AudioSource audioSource;

    [SerializeField] float moveSpeed = 10f;
    Animator myAnimator;

    void Start()
    {
      myRigidbody = GetComponent<Rigidbody2D>();  
      audioSource = GetComponent<AudioSource>();
      myAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        Run();
        FlipSprite();
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * moveSpeed, myRigidbody.linearVelocity.y);
        myRigidbody.linearVelocity = playerVelocity;
        bool isRunning = Mathf.Abs(myRigidbody.linearVelocity.x) > 0.1f;
        if (isRunning && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
        else if (!isRunning && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
        bool hasHorizontalSpeed = Mathf.Abs(myRigidbody.linearVelocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("isRunning", hasHorizontalSpeed);
    }

    void FlipSprite()
    {
        bool hasHorizontalSpeed = Mathf.Abs(myRigidbody.linearVelocity.x) > Mathf.Epsilon;
        if(hasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidbody.linearVelocity.x), 1f);
        }
    }
}
