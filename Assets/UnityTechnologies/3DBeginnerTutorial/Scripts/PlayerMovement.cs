using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float turnSpeed = 20f;
    public Transform cameraTransform; // Camera's transform
    public float movementSpeedMultiplier = 1f; // Movement speed multiplier
    public float audioSpeedMultiplier = 2f; // Audio speed multiplier when spacebar and movement keys are pressed

    Animator m_Animator;
    Rigidbody m_Rigidbody;
    AudioSource m_AudioSource;
    Vector3 m_Movement;
    Quaternion m_Rotation = Quaternion.identity;
    bool spaceBarPressed = false; // Track if spacebar is held down
    bool movementKeyPressed = false; // Track if a movement key is pressed

    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();
        m_AudioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Check if spacebar is held down
        spaceBarPressed = Input.GetKey(KeyCode.Space);

        // Check if any movement key is pressed
        movementKeyPressed = Input.GetAxis("Horizontal") != 0f || Input.GetAxis("Vertical") != 0f;

        // Set the "AnimationSpeed" parameter in the Animator
        m_Animator.SetBool("AnimationSpeed", spaceBarPressed);
    }

    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 cameraForward = cameraTransform.forward;
        Vector3 cameraRight = cameraTransform.right;
        cameraForward.y = 0; // ensure that the movement is only on the x-z plane
        cameraRight.y = 0;
        cameraForward.Normalize();
        cameraRight.Normalize();

        // Apply movement speed multiplier only when spacebar is held down
        float speedMultiplier = spaceBarPressed ? movementSpeedMultiplier : 1f;
        m_Movement = (cameraForward * vertical + cameraRight * horizontal) * speedMultiplier;

        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);
        bool isWalking = hasHorizontalInput || hasVerticalInput;
        m_Animator.SetBool("IsWalking", isWalking);

        if (isWalking)
        {
            if (!m_AudioSource.isPlaying)
            {
                m_AudioSource.Play();
            }
        }
        else
        {
            m_AudioSource.Stop();
        }

        // Adjust audio pitch based on conditions
        if (spaceBarPressed && movementKeyPressed)
        {
            m_AudioSource.pitch = audioSpeedMultiplier;
        }
        else
        {
            m_AudioSource.pitch = 1f;
        }

        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f);
        m_Rotation = Quaternion.LookRotation(desiredForward);
    }

    void OnAnimatorMove()
    {
        m_Rigidbody.MovePosition(m_Rigidbody.position + m_Movement * m_Animator.deltaPosition.magnitude);
        m_Rigidbody.MoveRotation(m_Rotation);
    }
}
