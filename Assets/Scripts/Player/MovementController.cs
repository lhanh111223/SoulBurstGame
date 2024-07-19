using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using Assets.Scripts.Parameter;
using Unity.VisualScripting;

public class MovementController : MonoBehaviour
{
    static GameParameterMovementController _param = new();

    [Header("Movement Settings")]
    public KeyCode inputUp = _param.INPUT_UP;
    public KeyCode inputDown = _param.INPUT_DOWN;
    public KeyCode inputLeft = _param.INPUT_LEFT;
    public KeyCode inputRight = _param.INPUT_RIGHT;
    public MouseButton inputAttack = _param.INPUT_ATTACK;
    public Transform BodyPlayer;
    public float Speed = _param.SPEED;
    public GameObject DashEffect;


    // Dash
    public KeyCode inputDash = _param.INPUT_DASH;
    public float DashDelaySeconds = _param.DELAY_DASH_SECOND;
    public float DashBoost = _param.DASH_BOOST;
    public float DashTime = _param.DASH_TIME;
    public float TimeBetweenDash = _param.TIME_BETWEEN_DASH;

    // Internal Variables
    float _dashTime;
    bool _isDashing;
    float _timeBetweenDash;
    Coroutine dashEffectCoroutine;

    // Player Invincible
    bool _isInvincible;

    Animator animator;
    Rigidbody2D rb;
    float _localScaleX;

    // Normal Attack

    private void Start()
    {
        this._isInvincible = false;
        this.animator = GetComponentInChildren<Animator>();
        this.rb = GetComponent<Rigidbody2D>();
        this._localScaleX = BodyPlayer.localScale.x;

    }

    private void Update()
    {
        // Move
        Vector2 direction = new Vector2(0, 0);

        if (Input.GetKey(inputUp))
        {
            direction += Vector2.up;
        }

        if (Input.GetKey(inputLeft))
        {
            direction += Vector2.left;
        }

        if (Input.GetKey(inputDown))
        {
            direction += Vector2.down;
        }

        if (Input.GetKey(inputRight))
        {
            direction += Vector2.right;
        }

        if (direction.x > 0)
        {
            BodyPlayer.localScale = new Vector3(this._localScaleX, BodyPlayer.localScale.y, BodyPlayer.localScale.z);
        }
        else if (direction.x < 0)
        {
            BodyPlayer.localScale = new Vector3(-this._localScaleX, BodyPlayer.localScale.y, BodyPlayer.localScale.z);
        }

        // Rotate player following mouse
        RotatePlayerWithMouse();
        this.rb.velocity = direction.normalized * this.Speed;
        this.animator.SetBool("PlayerRun", direction.magnitude > 0f);

        // Dash
        _timeBetweenDash -= Time.deltaTime;
        if (Input.GetKeyDown(inputDash) && !_isDashing && _dashTime <= 0 && _timeBetweenDash <= 0)
        {
            this.Speed += DashBoost;
            _dashTime = DashTime;
            _isDashing = true;
            StartDashEffect();
        }
        if (_dashTime <= 0 && _isDashing)
        {
            this.Speed -= DashBoost;
            _isDashing = false;
            StopDashEffect();
        }
        else
        {
            _dashTime -= Time.deltaTime;
        }
        // Normal Attack 
        if (Input.GetMouseButtonDown(1))
        {
            animator.SetBool("PlayerAttack", true);
        }
        else
        {
            animator.SetBool("PlayerAttack", false);
        }

        // Invincible Effect
        //if (_isInvincible)
        //{
        //    animator.SetBool("PlayerInvincible", true);
        //}
        //else
        //{
        //    animator.SetBool("PlayerInvincible", false);
        //}

    }

    // Dashing effect
    void StopDashEffect()
    {
        _timeBetweenDash = TimeBetweenDash;
        if (dashEffectCoroutine != null)
        {
            StopCoroutine(dashEffectCoroutine);
        }
    }

    void StartDashEffect()
    {
        if (dashEffectCoroutine != null)
        {
            StopCoroutine(dashEffectCoroutine);
        }
        dashEffectCoroutine = StartCoroutine(DashEffectCoroutine());
    }

    IEnumerator DashEffectCoroutine()
    {
        while (true)
        {
            if (BodyPlayer.localScale.x > 0)
                DashEffect.gameObject.transform.localScale = new Vector3(1, 1, 1);
            else
                DashEffect.gameObject.transform.localScale = new Vector3(-1, 1, 1);

            GameObject ghost = Instantiate(DashEffect, transform.position, transform.rotation);
            Sprite currentSprite = BodyPlayer.GetComponent<SpriteRenderer>().sprite;
            ghost.GetComponentInChildren<SpriteRenderer>().sprite = currentSprite;
            Destroy(ghost, 0.5f);
            yield return new WaitForSeconds(DashDelaySeconds);
        }
    }

    void RotatePlayerWithMouse()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookDir = mousePos - transform.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        if ((angle > 90 && angle < 180) || (angle > -180 && angle < -90))
        {
            BodyPlayer.localScale = new Vector3(-this._localScaleX, BodyPlayer.localScale.y, BodyPlayer.localScale.z);
        }
        else
        {
            BodyPlayer.localScale = new Vector3(this._localScaleX, BodyPlayer.localScale.y, BodyPlayer.localScale.z);
        }
    }

    // Invincible
    public void SetInvincible(bool isInvincible)
    {
        _isInvincible = isInvincible;
    }
    public bool GetInvincible()
    {
        return _isInvincible;
    }
    public void PlayerDie()
    {
        this.animator.SetBool("PlayerDie", true);
        Destroy(gameObject, 1f);
    }
}
