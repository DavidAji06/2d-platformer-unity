using UnityEngine;


[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections), typeof(Damageable))]

public class Knight : MonoBehaviour
{
    

    public float walkAccelerate = 4f;
    public float maxSpeed = 4f;
    public float walkStopRate = 0.0005f;
    Rigidbody2D rb;
    private Vector2 walkDirVector = Vector2.right;
    TouchingDirections touchingDirections;
    public DetectionZone attackZone;
    public DetectionZone cliffDetectionZone;
    public enum walkableDir { Right, Left}
    private walkableDir _walkDir;
    Animator animator;
    Damageable damageable;
    


    public walkableDir walkDir
    {
        get { return _walkDir; }
        set { 
             if (_walkDir != value)
            {
                //Flip direction
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);

                if(value == walkableDir.Right)
                {
                    walkDirVector = Vector2.right;   
                }
                else if (value == walkableDir.Left)
                {
                    walkDirVector = Vector2.left;
                }
            }
            _walkDir = value;
        }
        
    }
    public bool _hasTarget = false;

    public bool HasTarget
    {
        get { return _hasTarget; }
        
        private set
        {
            _hasTarget = value;
            animator.SetBool(AnimationStrings.hasTarget, value);
        }

    }
    public bool CanMove
    {
        get
        {
            return animator.GetBool(AnimationStrings.canMove);
        }
    }

    public float AttackCooldown
    {
        get
        {
            return animator.GetFloat(AnimationStrings.attackCooldown);

        }
        private set
        {
            animator.SetFloat(AnimationStrings.attackCooldown, Mathf.Max(value, 0));
        }
    }

    void Update()
    {
        HasTarget = attackZone.detectedColliders.Count > 0;
        if (AttackCooldown > 0)
        {
            AttackCooldown -= Time.deltaTime;
        }
    }


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        touchingDirections = GetComponent<TouchingDirections>();
        animator = GetComponent<Animator>();
        damageable = GetComponent<Damageable>();
    }

    private void FixedUpdate()
    {
        if ((!damageable.LockVelocity))
        {
            if (CanMove && touchingDirections.IsGrounded)
            {
                rb.linearVelocity = new Vector2( Mathf.Clamp(rb.linearVelocity.x + (walkAccelerate * walkDirVector.x * Time.fixedDeltaTime), -maxSpeed, maxSpeed), rb.linearVelocity.y);

            }
            else
            {
                rb.linearVelocity = new Vector2(Mathf.Lerp(rb.linearVelocity.x, 0, walkStopRate), rb.linearVelocity.y);
            }
        }

        if (touchingDirections.IsGrounded && touchingDirections.IsOnWall)
        {
            FlipDirection();
        }


    }

    private void FlipDirection()
    {
        if(walkDir  == walkableDir.Right)
        {
            walkDir = walkableDir.Left;
        }
        else if(walkDir == walkableDir.Left)
        {
            walkDir = walkableDir.Right;
        }
        else
        {
            Debug.LogError("Current walkable direction is not set to either values right or left");
        }
    }

    public void OnHit( int damage, Vector2 knockback)
    {
        rb.linearVelocity = new Vector2(knockback.x,rb.linearVelocity.y + knockback.y);
    }

    public void OnCliffDetected()
    {
        if (touchingDirections.IsGrounded)
        {
            FlipDirection();
        }
    }
}
