using UnityEngine;
using UnityEngine.InputSystem;

//[RequireComponent(typeof(Rigidbody), typeof(TouchingDirections), typeof(Damageable))]

public class PlayerController : MonoBehaviour
{
    Vector2 moveInput;
    public float walkSpeed = 5.5f;
    public float runSpeed = 7.5f;
    public float airWalkSpeed = 5f;
    [SerializeField]  private bool _isMoving = false;
    [SerializeField]  private bool _isRunning = false;
    public bool _isFacingRight = true;
    TouchingDirections touchingDirections;
    public float jumpImpulse = 10f;
    Damageable damageable;


    public float currentMoveSpeed
    {
        get
        {
            if (CanMove)
            {
                if (IsMoving && !touchingDirections.IsOnWall)
                {
                    if (touchingDirections.IsGrounded)
                    {
                        if (IsRunning)
                        {
                            return walkSpeed;
                        }
                        else
                        {
                            return runSpeed;
                        }
                    }
                    else
                    {
                        return airWalkSpeed; //speed in air
                    }
                }
                else
                {
                    return 0; //idle speed
                }
            }
            else
            {
                return 0; //movement locked when CanMove = false
            }

        }
    }

    public bool IsMoving
    { get
        { return _isMoving;}

        private set
        {
            _isMoving = value;
            animator.SetBool(AnimationStrings.isMoving, value);
        }
    }
   
    public bool IsRunning
    {
        get
        {
            return !_isRunning;
        }
        set
        {
            _isRunning = value;
            animator.SetBool(AnimationStrings.isRunning, value );
        }
    }


    Rigidbody2D rb;
    Animator animator;
    public bool isFacingRight
    {
        get
        {
            return _isFacingRight;
        }
        private set
        {
            if (_isFacingRight != value)
            {
                //flip local scale to make player face the opposite direction
                transform.localScale *= new Vector2(-1, 1);
            }
            _isFacingRight = value;
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingDirections = GetComponent<TouchingDirections>();
        damageable = GetComponent<Damageable>();
        
    }

    private void FixedUpdate()
    {
        if(!damageable.LockVelocity)
        {
            rb.linearVelocity = new Vector2(moveInput.x * currentMoveSpeed, rb.linearVelocity.y); // speed in x, only want y velocity to be grav so uses rb
        }

        

        animator.SetFloat(AnimationStrings.yVelocity, rb.linearVelocity.y);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        if (isAlive)
        {
            IsMoving = moveInput != Vector2.zero;

            SetFacingDirection(moveInput);
        }
        else
        {
            IsMoving = false;
        }
    }

    private void SetFacingDirection(Vector2 moveInput)
    {
        if(moveInput.x > 0 && !isFacingRight)
        {
            //face right
            isFacingRight = true;

        }
        else if (moveInput.x < 0 && isFacingRight)
        {
            //face left
            isFacingRight = false;
        }
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            IsRunning = true;
        }
        else if(context.canceled)
        {
            IsRunning = false;
        }
    }

    public void OnJump( InputAction.CallbackContext context)
    {
        if (context.started && touchingDirections.IsGrounded && CanMove)
        {
            animator.SetTrigger(AnimationStrings.jumpTrigger);
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpImpulse);

        }
    }

    public void OnAttack( InputAction.CallbackContext context)
    {
        if(context.started)
        {
            animator.SetTrigger(AnimationStrings.attackTrigger);
        }
    }

    public void OnRangedAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            animator.SetTrigger(AnimationStrings.rangedAttackTrigger);
        }
    }

    public bool CanMove
    {
        get
        {
            return animator.GetBool(AnimationStrings.canMove);
        }
    }

    public bool isAlive
    {
        get
        {
            return animator.GetBool(AnimationStrings.isAlive);
        }
    }

    public void OnHit(int damage, Vector2 knockback)
    {
        rb.linearVelocity = new Vector2(knockback.x, rb.linearVelocity.y + knockback.y);
    }

}
