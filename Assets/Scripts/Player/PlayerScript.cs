using Assets.Scripts;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerScript : Singleton<PlayerScript>
{
    public float forwardSpeed = 5f;
    public float velocity = 1f;
    public float lerpSpeed = 10f;
    public Vector2 pastPosition;
    public GameObject gameOverPanel;
    public Rigidbody rb;

    private float flyTargetHeight;

    public float flyHeight = 3f;
    public float flyForce = 5f;
    public Animator animator;
    public GameObject shield;
    public GameObject timerUI;
    public TextMeshProUGUI countdownText;
    public GameObject gatherMagnet;
    public GameObject player;
    public ParticleSystem deathParticle;

    [Header("Ground Detection")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundCheckDistance = 1.5f;
    [SerializeField] private float groundStickForce = 2f;

    private float horizontalInput;
    private bool _canRun;
    private float _currentSpeed;
    private bool _isInvincible;
    private bool isFlying = false;

    public enum PowerUpType
    {
        Speed,
        Fly,
        Invincible,
        Gather
    }

    [System.Serializable]
    public class ActivePowerUp
    {
        public PowerUpType type;
        public float timer;
    }

    private List<ActivePowerUp> activePowerUps =
        new List<ActivePowerUp>();

    void Start()
    {
        // Animate the player into the scene.
        transform.localScale = Vector3.zero;
        transform.DOScale(Vector3.one, 1f);

        _canRun = false;
        ResetSpeed();

        // Start the countdown before gameplay begins.
        StartCoroutine(StartCountdown());
    }

    void Update()
    {
        // Ignore input and power-up updates until the countdown ends.
        if (!_canRun)
            return;

        // Update the movement animation using the player's current speed.
        animator.SetFloat("Speed", rb.linearVelocity.magnitude);

        // Update active power-up timers and remove expired effects.
        for (int i = activePowerUps.Count - 1; i >= 0; i--)
        {
            activePowerUps[i].timer -= Time.deltaTime;

            if (activePowerUps[i].timer <= 0)
            {
                EndPowerUp(activePowerUps[i].type);
                activePowerUps.RemoveAt(i);
            }
        }

        // Convert horizontal mouse movement into player input.
        if (Input.GetMouseButton(0))
        {
            float deltaX =
                Input.mousePosition.x - pastPosition.x;

            horizontalInput =
                deltaX * velocity * Time.deltaTime * 100f;
        }
        else
        {
            horizontalInput = 0f;
        }

        pastPosition = Input.mousePosition;
    }

    void FixedUpdate()
    {
        // Stop applying movement while the game has not started.
        if (!_canRun)
            return;

        Vector3 velocityVector = rb.linearVelocity;

        // Apply horizontal input and constant forward movement.
        velocityVector.x = horizontalInput;
        velocityVector.z = _currentSpeed;

        if (isFlying)
        {
            // Move upward until the target flight height is reached.
            if (transform.position.y < flyTargetHeight)
            {
                velocityVector.y = flyForce;
            }
            else
            {
                velocityVector.y = 0f;
            }
        }
        else
        {
            // Keep the player attached to the ground.
            StickToGround(ref velocityVector);
        }

        rb.linearVelocity = velocityVector;
    }

    private void StickToGround(ref Vector3 velocityVector)
    {
        Vector3 rayOrigin =
            transform.position + Vector3.up * 0.2f;

        if (Physics.Raycast(
            rayOrigin,
            Vector3.down,
            out RaycastHit hit,
            groundCheckDistance,
            groundLayer))
        {
            Vector3 normal = hit.normal;

            // Check whether the player is standing on a slope.
            float slopeAngle =
                Vector3.Angle(normal, Vector3.up);

            if (slopeAngle > 0.1f)
            {
                // Project forward movement onto the slope surface.
                Vector3 forwardDirection =
                    Vector3.ProjectOnPlane(
                        Vector3.forward,
                        normal
                    ).normalized;

                float verticalVelocity =
                    forwardDirection.y * _currentSpeed;

                velocityVector.y = verticalVelocity;

                // Apply extra force to keep the player grounded.
                if (velocityVector.y > -groundStickForce)
                {
                    velocityVector.y -= groundStickForce;
                }
            }
            else
            {
                // Apply a small downward force on flat ground.
                velocityVector.y = -groundStickForce;
            }
        }
        else
        {
            // Apply gravity when no ground is detected.
            velocityVector.y +=
                Physics.gravity.y * Time.fixedDeltaTime;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Ignore obstacle damage while the player is invincible.
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            if (_isInvincible)
                return;

            // Trigger the game over sequence.
            gameOverPanel.SetActive(true);
            animator.SetTrigger("IsDead");
            deathParticle.Play();

            StopPlayer();
        }
    }

    public void ResetSpeed()
    {
        // Restore the player's default movement speed.
        _currentSpeed = forwardSpeed;
    }

    public void ApplyPowerUp(
        PowerUpType type,
        float duration,
        float value = 0f)
    {
        // Check whether this power-up is already active.
        var existing =
            activePowerUps.Find(p => p.type == type);

        if (existing != null)
        {
            // Extend the remaining duration instead of creating a duplicate.
            existing.timer += duration;
        }
        else
        {
            // Register the new active power-up.
            activePowerUps.Add(new ActivePowerUp
            {
                type = type,
                timer = duration
            });

            StartPowerUp(type, value);
        }
    }

    private void StartPowerUp(
        PowerUpType type,
        float value)
    {
        // Apply the effect associated with the selected power-up.
        switch (type)
        {
            case PowerUpType.Speed:
                _currentSpeed = forwardSpeed + value;
                break;

            case PowerUpType.Fly:
                EnableFly();
                break;

            case PowerUpType.Invincible:
                EnableInvincible();
                break;

            case PowerUpType.Gather:
                EnableGather();
                break;
        }
    }

    private void EndPowerUp(PowerUpType type)
    {
        // Remove the effect associated with the expired power-up.
        switch (type)
        {
            case PowerUpType.Speed:
                ResetSpeed();
                break;

            case PowerUpType.Fly:
                DisableFly();
                break;

            case PowerUpType.Invincible:
                DisableInvincible();
                break;

            case PowerUpType.Gather:
                DisableGather();
                break;
        }
    }

    void EnableFly()
    {
        // Enable flying and set the target height relative to the player.
        isFlying = true;
        flyTargetHeight =
            transform.position.y + flyHeight;
    }

    void DisableFly()
    {
        // Return the player to normal ground movement.
        isFlying = false;
    }

    void EnableInvincible()
    {
        // Enable invincibility and show the shield.
        _isInvincible = true;
        shield.SetActive(true);
    }

    void DisableInvincible()
    {
        // Disable invincibility and hide the shield.
        _isInvincible = false;
        shield.SetActive(false);
    }

    void EnableGather()
    {
        // Enable the collectible attraction effect.
        gatherMagnet.SetActive(true);
    }

    void DisableGather()
    {
        // Disable the collectible attraction effect.
        gatherMagnet.SetActive(false);
    }

    IEnumerator StartCountdown()
    {
        timerUI.SetActive(true);

        int count = 3;

        // Display the countdown before allowing the player to move.
        while (count > 0)
        {
            countdownText.text = count.ToString();

            yield return new WaitForSeconds(1f);

            count--;
        }

        countdownText.text = "GO!";

        yield return new WaitForSeconds(0.5f);

        countdownText.gameObject.SetActive(false);
        timerUI.SetActive(false);

        _canRun = true;
    }

    void StopPlayer()
    {
        // Disable movement and reset the player's velocity.
        _canRun = false;

        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        horizontalInput = 0f;

        // Stop the running animation.
        animator.SetFloat("Speed", 0f);
    }
}