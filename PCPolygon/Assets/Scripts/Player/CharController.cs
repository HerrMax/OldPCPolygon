using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]

public class CharController : MonoBehaviour {

    [SerializeField] private bool isWalking;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] [Range(0f, 1f)] private float runstepLenghten;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private float stickToGroundForce;
    [SerializeField] private float gravityMultiplier;
    [SerializeField] private AudioClip[] footstepSounds;    
    [SerializeField] private AudioClip jumpSound;           
    [SerializeField] private AudioClip landSound;

    private CharacterController characterController;
    private bool jump;
    private AudioSource audioSource;
    private bool previouslyGrounded;
    private bool jumping;
    private Vector2 input;
    private Vector3 moveDir = Vector3.zero;
    private float stepCycle;
    private float nextStep;
    private CollisionFlags collisionFlags;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        audioSource = GetComponent<AudioSource>();
        stepCycle = 0f;
        nextStep = stepCycle / 2f;
        jumping = false;
    }

    private void Update()
    {
        if (!jump)
        {
            jump = Input.GetButtonDown("Jump");
        }

        if (!previouslyGrounded && characterController.isGrounded)
        {
            PlayLandingSound();
            moveDir.y = 0f;
            jumping = false;
        }
        if (!characterController.isGrounded && !jumping && previouslyGrounded)
        {
            moveDir.y = 0f;
        }

        previouslyGrounded = characterController.isGrounded;
    }

    private void FixedUpdate()
    {
        float speed;
        GetInput(out speed);
    }
    
    private void GetInput(out float speed)
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        isWalking = !Input.GetKey(KeyCode.LeftShift);

        speed = isWalking ? walkSpeed : runSpeed;
        input = new Vector2(horizontal, vertical);

        if (input.sqrMagnitude > 1)
        {
            input.Normalize();
        }
    }

    private void PlayJumpSound()
    {
        audioSource.clip = jumpSound;
        audioSource.Play();
    }

    private void PlayLandingSound()
    {
        audioSource.clip = landSound;
        audioSource.Play();
        nextStep = stepCycle + .5f;
    }

    private void PlayFootStepAudio()
    {
        if (!characterController.isGrounded)
        {
            return;
        }
        int n = Random.Range(1, footstepSounds.Length);
        audioSource.clip = footstepSounds[n];
        audioSource.PlayOneShot(audioSource.clip);
        footstepSounds[n] = footstepSounds[0];
        footstepSounds[0] = audioSource.clip;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;
        if (collisionFlags == CollisionFlags.Below)
        {
            return;
        }

        if (body == null || body.isKinematic)
        {
            return;
        }
        body.AddForceAtPosition(characterController.velocity * 0.1f, hit.point, ForceMode.Impulse);
    }
}