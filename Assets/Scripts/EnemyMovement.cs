using System.Collections;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float minDist = 5f;
    public float maxDist = 15f;
    public float speed = 0.5f;

    public string targetTag;
    public float searchRadius;

    public GameObject closestObject;

    public float moveSpeed = 3f;        // Speed at which the enemy moves
    public float turnSpeed = 180f;      // Speed at which the enemy turns
    public float moveDistance = 5f;     // Distance the enemy moves before stopping
    public float minIdleTime = 1f;      // Minimum time the enemy waits before moving again
    public float maxIdleTime = 3f;      // Maximum time the enemy waits before moving again

    private float currentMoveDistance;  // Distance the enemy has moved so far
    private float currentIdleTime;      // Time the enemy has been idle
    private bool isMoving;              // Flag to indicate if the enemy is currently moving
    private Vector3 moveDirection;      // Direction the enemy is moving in

    public Transform target;            // Target to shoot at
    public GameObject bulletPrefab;     // Prefab for the bullet
    public float bulletSpeed = 10f;     // Speed of the bullet
    public float fireRate = 1f;         // Rate of fire in shots per second

    private float fireTimer;            // Timer for tracking fire rate

    enum Direction
    {
        Left,
        Right,
        Up,
        Down,
    };

    public enum State
    {
        None,
        Idle,
        Roam,
        ChaseClosestObject,
        ShootClosestObject
    };
    public State state;

    private void Start()
    {
        state = State.Idle;
        targetTag = "Player";
    }

    void Update()
    {
        switch (state)
        {
            case State.Idle:
                Idle();
                break;
            case State.Roam:
                Roam();
                break;
            case State.ChaseClosestObject:
                ChaseClosestObject();
                break;
            case State.ShootClosestObject:
                ShootClosestObject();
                break;
            default:
                state = State.Idle;
                break;
        }
    }

    void FindClosestObject()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, searchRadius);

        float closestDistance = Mathf.Infinity;
        bool foundPlayer = false;

        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.CompareTag(targetTag))
            {
                foundPlayer = true;
                float distance = Vector2.Distance(transform.position, collider.transform.position);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestObject = collider.gameObject;
                }
            }
        }

        if (!foundPlayer) closestObject = null;
    }

    void ChaseClosestObject()
    {
        FindClosestObject();
        if (closestObject != null)
        {
            target = closestObject.transform;
            Vector3 localPosition = closestObject.transform.position - transform.position;
            if (localPosition.sqrMagnitude >= minDist * minDist)
            {
                if (localPosition.sqrMagnitude <= maxDist * maxDist)
                {
                    localPosition = localPosition.normalized; // The normalized direction in LOCAL space
                    transform.Translate(localPosition.x * Time.deltaTime * speed, localPosition.y * Time.deltaTime * speed, localPosition.z * Time.deltaTime * speed);
                }
                else
                {
                    state = State.Roam;
                }
                state = State.ShootClosestObject;
            }
            else
            {
                state = State.Roam;
            }
        } else
        {
            state = State.Roam;
        }
    }

    void ShootClosestObject()
    {
        if (target != null)
        {
            // Check if it's time to fire
            if (fireTimer <= 0f)
            {
                // Calculate the direction to the target
                Vector2 direction = target.position - transform.position;
                direction.Normalize();

                Vector3 dir = new Vector3(direction.x, direction.y, transform.position.z);

                // Create the bullet
                GameObject bullet = Instantiate(bulletPrefab, transform.position + dir, Quaternion.identity);

                // Set the velocity of the bullet
                Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
                bulletRigidbody.velocity = direction * bulletSpeed;

                // Reset the fire timer
                fireTimer = 1f / fireRate;
                state = State.ShootClosestObject;
            }

            // Decrement the fire timer
            fireTimer -= Time.deltaTime;
            state = State.ChaseClosestObject;
        }
    }

    void Idle()
    {
        StartCoroutine(waitASec());
        // Start in a random direction
        moveDirection = Quaternion.Euler(0, 0, Random.Range(0f, 360f)) * Vector3.right;
    }

    IEnumerator waitASec()
    {
        yield return new WaitForSeconds(1);
        state = State.Roam;
    }

    void Roam()
    {
        if (isMoving)
        {
            // Move in the current direction
            transform.position += moveDirection * moveSpeed * Time.deltaTime;

            // Update the distance moved
            currentMoveDistance += moveSpeed * Time.deltaTime;

            // Stop moving if the distance has been reached
            if (currentMoveDistance >= moveDistance)
            {
                isMoving = false;
                currentMoveDistance = 0f;
            }
        }
        else
        {
            // Wait for a random amount of time before moving again
            currentIdleTime += Time.deltaTime;
            if (currentIdleTime >= Random.Range(minIdleTime, maxIdleTime))
            {
                // Pick a new direction to move in
                var turnAngle = Random.Range(-turnSpeed, turnSpeed);
                Quaternion rotation = Quaternion.AngleAxis(turnAngle, Vector3.forward);
                moveDirection = rotation * moveDirection;
                isMoving = true;
                currentIdleTime = 0f;
            }
        }
        if (closestObject != null) state = State.ChaseClosestObject;
        ChaseClosestObject();
    }
}
