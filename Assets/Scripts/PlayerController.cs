using System.Collections;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class PlayerController : MonoBehaviour
{
    PlayerAnimator playerAnimator;
    Rigidbody2D _rb;
    CapsuleCollider2D _col;
    bool _grounded;
    bool _jumping;
    public int _jumpsRemaining;
    float _jumpCooldownTimer;
    bool _dashing;
    float _dashCooldownTimer;

    [SerializeField] ScriptableStats _stats;

    void Awake(){
        _rb = GetComponent<Rigidbody2D>();
        _col = GetComponent<CapsuleCollider2D>();
        playerAnimator = GetComponentInChildren<PlayerAnimator>();
    }

    void FixedUpdate(){
        CheckGrounded();
        HandleMovement();
        HandleJump();
        HandleDash();
    }

    void CheckGrounded(){
        _grounded = Physics2D.Raycast(_col.bounds.center, Vector2.down, _stats.GrounderDistance, ~_stats.PlayerLayer);
        if (_grounded){
            if (_jumping){
                playerAnimator.Land();
            }
            _jumpsRemaining = _stats.MaxJumps;
            _jumping = false;
            _dashing = false;
            
        }
    }

    void HandleMovement(){
        float moveInput = Input.GetAxis("Horizontal");
        Vector2 velocity = _rb.velocity;

        float targetVelocityX = moveInput * _stats.MaxSpeed;
        float deceleration = _stats.AirDeceleration;

        if (_grounded){
            velocity.y = -_stats.GroundingForce;
        }

        if (moveInput != 0){
            velocity.x = Mathf.MoveTowards(velocity.x, targetVelocityX, _stats.Acceleration * Time.fixedDeltaTime);
        }
        
        else{
            velocity.x = Mathf.MoveTowards(velocity.x, 0, deceleration * Time.fixedDeltaTime);
        }

        if (velocity.y < -_stats.MaxFallSpeed){
            velocity.y = -_stats.MaxFallSpeed;
        }

        if (_grounded && Input.GetAxis("Jump") > 0){
            velocity.y = _stats.JumpPower;
        }

        _rb.velocity = velocity;
    }
    void HandleJump(){
        _jumpCooldownTimer -= Time.deltaTime;
        if (Input.GetAxis("Jump") > 0){
            if (_grounded){
                _jumping = true;
                _jumpCooldownTimer = _stats.JumpCooldown;
                _rb.AddForce(Vector2.up * _stats.JumpPower, ForceMode2D.Impulse);
                playerAnimator.Jump();
            }
            else if (_jumpsRemaining > 0 && _jumpCooldownTimer < 0){
                _jumping = true;
                _jumpsRemaining--;
                _jumpCooldownTimer = _stats.JumpCooldown;
                _rb.velocity = new Vector2(_rb.velocity.x, 0);
                _rb.AddForce(Vector2.up * _stats.JumpPower * 2, ForceMode2D.Impulse);
                playerAnimator.Jump();
            }
        }
    }

    void HandleDash(){
        _dashCooldownTimer -= Time.deltaTime;
        if (Input.GetAxis("Fire3") > 0 && !_dashing && _dashCooldownTimer<0 && (Input.GetAxis("Horizontal") > 0 || Input.GetAxis("Vertical") > 0)){
            _dashing = true;
            _dashCooldownTimer = _stats.DashCooldown;
            StartCoroutine("Dash");
        }
    }

    IEnumerator Dash(){
        Debug.Log("Dashing!");
        Vector2 dashDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
        Vector2 targetVelocity = dashDirection *_stats.DashSpeed;
        float elapsedTime = 0;
        while (elapsedTime < _stats.DashDuration){
            _rb.velocity = Vector2.Lerp(_rb.velocity, targetVelocity, elapsedTime);
            elapsedTime += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }

        _dashing = false;
    }
    void OnValidate(){
        if (_stats == null){
            Debug.LogWarning("Please assign a ScriptableStats asset to the Player Controller's Stats slot", this);
        }
    }
}