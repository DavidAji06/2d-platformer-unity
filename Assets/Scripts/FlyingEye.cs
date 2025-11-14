using System.Collections.Generic;
using UnityEngine;

public class FlyingEye : MonoBehaviour
{
    public List<Transform> waypoints;
    public float flightSpeed = 5f;
    int waypointNum = 0;
    public float waypointReachedDistance = 0.1f;

    public Collider2D deathCollider;

    public DetectionZone biteDetectionZone;
    Damageable damageable;
    Rigidbody2D rb;
    Animator animator;
    Transform nextWaypoint;

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
    private void Start()
    {
        nextWaypoint = waypoints[waypointNum];
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        damageable = GetComponent<Damageable>();
    }

    private void Update()
    {
        HasTarget = biteDetectionZone.detectedColliders.Count > 0;
    }

    private void FixedUpdate()
    {
        if (damageable.IsAlive)
        {
            if(CanMove)
            {
                Flight();
            }
            else
            {
                rb.linearVelocity = Vector3.zero;
            }
        }
    }

    private void Flight()
    {
        //fly to next waypoint
        Vector2 directionToWaypoint = (nextWaypoint.position - transform.position).normalized;

        //check distance to see if reached waypoint
        float distance = Vector2.Distance(nextWaypoint.position, transform.position);

        rb.linearVelocity = directionToWaypoint * flightSpeed;
        UpdateDirection();

        //see if must switch waypoints
        if(distance <= waypointReachedDistance)
        {
            waypointNum++; //next waypoint

            if(waypointNum >= waypoints.Count)
            {
                waypointNum = 0; //go back to first waypoint
            }
            nextWaypoint = waypoints[waypointNum];
        }
    }

    private void UpdateDirection()
    {
        Vector3 loScale = transform.localScale;

        if (transform.localScale.x > 0)
        {
            if(rb.linearVelocity.x < 0)
            {
                transform.localScale = new Vector3( -1 *loScale.x, loScale.y, loScale.z);
            }
        }
        else
        {
            if (rb.linearVelocity.x > 0)
            {
                transform.localScale = new Vector3( -1 * loScale.x, loScale.y, loScale.z);
            }

        }
    }

    public void OnDeath()
    {
        rb.gravityScale = 1f;
        rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        deathCollider.enabled = true;
    }

}
