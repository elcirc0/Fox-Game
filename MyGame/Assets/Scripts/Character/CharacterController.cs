using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jump;

    private Vector3 _size;
    private Rigidbody2D _rigidBody;
    private float _axis;

    [SerializeField] private Vector2 _boxSize;
    [SerializeField] private float _castDistance;
    [SerializeField] private LayerMask _layerMask;

    [SerializeField] private TrailRenderer _trailRenderer;
    private bool _canDash = true;
    private bool _isDashing;
    [SerializeField] private float _dashingPower = 24f;
    [SerializeField] private float _dashingTime = 0.2f;
    [SerializeField] private float _dashingCooldown = 1f;

    [SerializeField] private Animator _animator;

    private CoinController CoinController = new();

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _size = gameObject.transform.localScale;
    }

    private void Update()
    {
        if (_isDashing)
            return;

        _axis = Input.GetAxisRaw("Horizontal");
        _animator.SetFloat("speed", math.abs(_axis));

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            AudioManager.Instance.PlaySFX("Jump");
            _rigidBody.AddForce(new Vector2(0, _jump), ForceMode2D.Impulse);
            _animator.SetBool("jump", true);
        }

        if (_axis > 0)
        {
            //AudioManager.Instance.PlaySFX("Walk");
            gameObject.transform.localScale = _size;
        }
           
        if (_axis < 0)
        {
            //AudioManager.Instance.PlaySFX("Walk");
            gameObject.transform.localScale = new Vector3(-_size.x, _size.y, _size.y);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && _canDash)
        {
            AudioManager.Instance.PlaySFX("Dash");
            StartCoroutine(Dash());
        }

    }

    private void FixedUpdate()
    {
        if (_isDashing)
            return;

        IsGrounded();

        _rigidBody.velocity = new Vector2(_axis * _speed, _rigidBody.velocity.y);
    }
    private bool IsGrounded()
    {
        if (Physics2D.BoxCast(transform.position, _boxSize, 0, -transform.up, _castDistance, _layerMask))
        {
            _animator.SetBool("jump", false);
            return true;
        }


        else
        {
            _animator.SetBool("jump", true);
            return false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position - transform.up * _castDistance, _boxSize);
    }

    private IEnumerator Dash()
    {
        _canDash = false;
        _animator.SetBool("jump", true);
        _isDashing = true;
        float originalGravity = _rigidBody.gravityScale;
        _rigidBody.gravityScale = 0f;
        _rigidBody.velocity = new Vector2(transform.localScale.x * _dashingPower, 0f);
        _trailRenderer.emitting = true;
        yield return new WaitForSeconds(_dashingTime);
        _trailRenderer.emitting = false;
        _rigidBody.gravityScale = originalGravity;
        _animator.SetBool("jump", false);
        _isDashing = false;
        yield return new WaitForSeconds(_dashingCooldown);
        _canDash = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("enemy"))
        {
            UiController.instance.OnLosePanel();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("coin collect");
        if (collision.transform.CompareTag("coin"))
            Destroy(collision.gameObject);

        AudioManager.Instance.PlaySFX("CoinCollect");
        CoinController.CollectCoin();
    }
}
