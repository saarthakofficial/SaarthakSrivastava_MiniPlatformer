using System.Collections;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class PlayerController : MonoBehaviour
{
    Rigidbody2D _rb;
    CapsuleCollider2D _col;
    bool _grounded;
    bool _jumping;
    int _jumpsRemaining;
    bool _dashing;
    float _dashCooldownTimer;
    float _direction;

    [SerializeField] ScriptableStats _stats;

    void Awake(){
        _rb = GetComponent<Rigidbody2D>();
        _col = GetComponent<CapsuleCollider2D>();
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

        if (moveInput > 0){
            _direction = 1;
        }
        else if (moveInput < 0){

            _direction = -1;
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
        if (Input.GetAxis("Jump") > 0){
            if (_grounded){
                _jumping = true;
                _rb.AddForce(Vector2.up * _stats.JumpPower, ForceMode2D.Impulse);
            }
            else if (_jumpsRemaining > 0){
                _jumping = true;
                _jumpsRemaining--;
                _rb.velocity = new Vector2(_rb.velocity.x, 0);
                _rb.AddForce(Vector2.up * _stats.JumpPower, ForceMode2D.Impulse);
            }
        }
    }

    void HandleDash(){
        _dashCooldownTimer -= Time.deltaTime;
        if (Input.GetAxis("Fire3") > 0 && !_dashing && _dashCooldownTimer<0){
            _dashing = true;
            _dashCooldownTimer = _stats.DashCooldown;
            StartCoroutine("Dash");
        }
    }

    IEnumerator Dash(){
        Debug.Log("Dashing!");
        Vector2 targetVelocity = _direction * Vector2.right *_stats.DashSpeed;
        float elapsedTime = 0;
        while (elapsedTime < _stats.DashDuration){
            float t = elapsedTime / _stats.DashDuration;
            _rb.velocity = Vector2.Lerp(_rb.velocity, targetVelocity, t);
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