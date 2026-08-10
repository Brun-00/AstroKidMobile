using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class PlayerScript : Singleton<PlayerScript>
{
    public float forwardSpeed = 5f;
    public float velocity = 1f;
    public float lerpSpeed = 10f;
    public Vector2 pastPosition;
    public GameObject gameOverPanel;
    public GameObject winPanel;
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

    private List<ActivePowerUp> activePowerUps = new List<ActivePowerUp>();

    void Start()
    {
        
        transform.localScale = Vector3.zero; 
        transform.DOScale(Vector3.one, 1f);


        _canRun = false;
        ResetSpeed();
        StartCoroutine(StartCountdown());
    }

    void Update()
    {
        if (!_canRun) return;

        animator.SetFloat("Speed", rb.linearVelocity.magnitude);

        for (int i = activePowerUps.Count - 1; i >= 0; i--)
        {
            activePowerUps[i].timer -= Time.deltaTime;

            if (activePowerUps[i].timer <= 0)
            {
                EndPowerUp(activePowerUps[i].type);
                activePowerUps.RemoveAt(i);

      
            }
        }

        if (Input.GetMouseButton(0))
        {
            float deltaX = Input.mousePosition.x - pastPosition.x;
            horizontalInput = deltaX * velocity * Time.deltaTime * 100f;
        }
        else
        {
            horizontalInput = 0;
        }

        pastPosition = Input.mousePosition;


    }

    void FixedUpdate()
    {

        if (!_canRun) return;

        Vector3 velocityVector = new Vector3(
            horizontalInput,
            rb.linearVelocity.y,
            _currentSpeed
        );

        if (isFlying)
        {
            if (transform.position.y < flyTargetHeight)
            {
                velocityVector.y = flyForce;
            }
            else
            {
                velocityVector.y = 0f; 
            }
        }

        rb.linearVelocity = velocityVector;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("FinishLine"))
        {
            winPanel.SetActive(true);
            animator.SetTrigger("Win");
            StopPlayer();
        }
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            if (_isInvincible)
            {
                return;
            }
            gameOverPanel.SetActive(true);
            animator.SetTrigger("IsDead");
            deathParticle.Play();
            StopPlayer();

        }
    }
    public void ResetSpeed()
    {
        _currentSpeed = forwardSpeed;
    }

    public void ApplyPowerUp(PowerUpType type, float duration, float value = 0f)
    {

        var existing = activePowerUps.Find(p => p.type == type);

        if (existing != null)
        {

            existing.timer += duration;
        }
        else
        {
            activePowerUps.Add(new ActivePowerUp { type = type, timer = duration });
            StartPowerUp(type, value);
        }

    }

    #region PowerUp Logic
    private void StartPowerUp(PowerUpType type, float value)
    {
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
    #endregion



    #region PowerUp Methods
    void EnableFly()
    {
        isFlying = true;
        flyTargetHeight = transform.position.y + flyHeight;
    }

    void DisableFly()
    {
        isFlying = false;
    }

    void EnableInvincible()
    {
        _isInvincible = true;
        shield.SetActive(true);
    }

    void DisableInvincible()
    {
        _isInvincible = false;
        shield.SetActive(false);
    }

    void EnableGather()
    {
        gatherMagnet.SetActive(true);
    }

    void DisableGather()
    {
        gatherMagnet.SetActive(false);
    }

    #endregion  

    IEnumerator StartCountdown()
    {
        timerUI.SetActive(true);
        int count = 3;

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
        _canRun = false;


        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;


        horizontalInput = 0f;


        animator.SetFloat("Speed", 0f);


    }



}

