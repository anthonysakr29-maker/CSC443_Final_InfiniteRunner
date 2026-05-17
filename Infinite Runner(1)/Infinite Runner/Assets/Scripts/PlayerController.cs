using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [Header("Lanes")]
    [SerializeField] private float laneOffset = 2f;
    [SerializeField, Min(1)] private int laneCount = 3;
    [SerializeField] private float laneSwitchSpeed = 14f;

    [Header("Jump")]
    [SerializeField] private float jumpVelocity = 8f;
    [SerializeField] private float gravity = -25f;

    [Header("Slide")]
    [SerializeField] private float slideDuration = 0.7f;
    [SerializeField] private float slideColliderHeight = 1f;
    [SerializeField] private Vector3 slideColliderCenter = new Vector3(0f, 0.5f, 0f);
    [SerializeField] private ParticleSystem slideDustEffect;

    private bool _isSliding;
    private CapsuleCollider _capsuleCollider;
    private float _normalColliderHeight;
    private Vector3 _normalColliderCenter;
    private Animator _animator;

    private int _laneIndex;
    private float _y;
    private float _yVel;
    private float _groundHeight;
    private Vector2 _prevMove;

    void Awake()
    {
        if (TryGetComponent(out Rigidbody rb))
        {
            rb.isKinematic = true;
            rb.useGravity = false;
        }

        _y = transform.position.y;

        _capsuleCollider = GetComponent<CapsuleCollider>();

        if (_capsuleCollider != null)
        {
            _normalColliderHeight = _capsuleCollider.height;
            _normalColliderCenter = _capsuleCollider.center;
        }

        _animator = GetComponentInChildren<Animator>();
    }

    public void Move(InputAction.CallbackContext ctx)
    {
        if (GameManager.Instance != null && GameManager.Instance.IsGameOver) return;

        Vector2 v = ctx.ReadValue<Vector2>();

        if (v.x > 0.5f && _prevMove.x <= 0.5f)
            ChangeLane(+1);
        else if (v.x < -0.5f && _prevMove.x >= -0.5f)
            ChangeLane(-1);

        if (v.y > 0.5f && _prevMove.y <= 0.5f && IsGrounded())
            _yVel = jumpVelocity;

        if (v.y < -0.5f && _prevMove.y >= -0.5f && !_isSliding && IsGrounded())
            StartCoroutine(SlideRoutine());

        _prevMove = v;
    }

    private void ChangeLane(int delta)
    {
        int half = laneCount / 2;
        _laneIndex = Mathf.Clamp(_laneIndex + delta, -half, half);
    }

    void Update()
    {
        if (GameManager.Instance != null && GameManager.Instance.IsGameOver) return;

        _yVel += gravity * Time.deltaTime;
        _y += _yVel * Time.deltaTime;

        if (_y < _groundHeight)
        {
            _y = _groundHeight;
            _yVel = 0f;
        }

        Vector3 pos = transform.position;
        pos.x = Mathf.MoveTowards(pos.x, _laneIndex * laneOffset, laneSwitchSpeed * Time.deltaTime);
        pos.y = _y;
        pos.z = 0f;
        transform.position = pos;

        
    }

    private bool IsGrounded()
    {
        return Mathf.Abs(_y - _groundHeight) < 0.05f && _yVel <= 0f;
    }

    public void SetGroundHeight(float height)
    {
        if (height > _groundHeight)
            _groundHeight = height;
    }

    public void ClearGroundHeight(float height)
    {
        if (Mathf.Approximately(_groundHeight, height))
            _groundHeight = 0f;
    }

    private IEnumerator SlideRoutine()
    {
        _isSliding = true;

        if (_animator != null)
            _animator.SetTrigger("Slide");

        if (_capsuleCollider != null)
        {
            _capsuleCollider.height = slideColliderHeight;
            _capsuleCollider.center = slideColliderCenter;
        }

        if (slideDustEffect != null)
            slideDustEffect.Play();

        yield return new WaitForSeconds(slideDuration);

        if (_capsuleCollider != null)
        {
            _capsuleCollider.height = _normalColliderHeight;
            _capsuleCollider.center = _normalColliderCenter;
        }

        if (slideDustEffect != null)
            slideDustEffect.Stop();

        _isSliding = false;
    }
}