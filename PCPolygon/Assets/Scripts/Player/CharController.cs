using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    [SerializeField] private float stepInterval;

    [SerializeField] private CharacterController characterController;
    [SerializeField] private bool jump;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private bool previouslyGrounded;
    [SerializeField] private bool jumping;
    [SerializeField] private Vector2 input;
    [SerializeField] private Vector3 moveDir = Vector3.zero;
    [SerializeField] private float stepCycle;
    [SerializeField] private float nextStep;
    [SerializeField] private CollisionFlags collisionFlags;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        stepCycle = 0f;
        nextStep = stepCycle / 2f;
        jumping = false;
        audioSource = GetComponent<AudioSource>();
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
        Vector3 desiredMove = transform.forward * input.y + transform.right * input.x;

        RaycastHit hitInfo;
        Physics.SphereCast(transform.position, characterController.radius, Vector3.down, out hitInfo,
        characterController.height / 2f, Physics.AllLayers, QueryTriggerInteraction.Ignore);
        desiredMove = Vector3.ProjectOnPlane(desiredMove, hitInfo.normal).normalized;

        moveDir.x = desiredMove.x * speed;
        moveDir.z = desiredMove.z * speed;

        if (characterController.isGrounded)
        {
            moveDir.y = -stickToGroundForce;

            if (jump)
            {
                moveDir.y = jumpSpeed;
                PlayJumpSound();
                jump = false;
                jumping = true;
            }
        }
        else
        {
            moveDir += Physics.gravity * gravityMultiplier * Time.fixedDeltaTime;
        }
        collisionFlags = characterController.Move(moveDir * Time.fixedDeltaTime);

        ProgressStepCycle(speed);
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

    private void ProgressStepCycle(float speed)
    {
        if (characterController.velocity.sqrMagnitude > 0 && (input.x != 0 || input.y != 0))
        {
            stepCycle += (characterController.velocity.magnitude + (speed * (isWalking ? 1f : runstepLenghten))) *
                         Time.fixedDeltaTime;
        }

        if (!(stepCycle > nextStep))
        {
            return;
        }

        nextStep = stepCycle + stepInterval;

        PlayFootStepAudio();
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
