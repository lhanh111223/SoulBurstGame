using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class MovementController : MonoBehaviour
{
    [Header("Movement Settings")]
    public KeyCode inputUp = KeyCode.W;
    public KeyCode inputDown = KeyCode.S;
    public KeyCode inputLeft = KeyCode.A;
    public KeyCode inputRight = KeyCode.D;
    public Transform BodyPlayer;
    public float Speed = 5f;
    public GameObject DashEffect;
    public float DashDelaySeconds;


    // Dash
    public float DashBoost;
    public float DashTime;
    float _dashTime;
    bool _isDashing;

    // Internal Variables
    Animator animator;
    Rigidbody2D rb;
    float _localScaleX;
    // Dash
    Coroutine dashEffectCoroutine;

    private void Start()
    {
        this.animator = GetComponentInChildren<Animator>();
        this.rb = GetComponent<Rigidbody2D>();
        this._localScaleX = BodyPlayer.localScale.x;
    }

    private void Update()
    {
        // Move
        Vector2 direction = new Vector2(0, 0);

        if (Input.GetKey(KeyCode.W))
        {
            direction += Vector2.up;
        }

        if (Input.GetKey(KeyCode.A))
        {
            direction += Vector2.left;
        }

        if (Input.GetKey(KeyCode.S))
        {
            direction += Vector2.down;
        }

        if (Input.GetKey(KeyCode.D))
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

        this.rb.velocity = direction.normalized * this.Speed;
        this.animator.SetBool("PlayerNinjaRun", direction.magnitude > 0f);

        // Dash
        if (Input.GetKeyDown(KeyCode.Space) && !_isDashing && _dashTime <= 0)
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
    }


    void StopDashEffect()
    {
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

}
