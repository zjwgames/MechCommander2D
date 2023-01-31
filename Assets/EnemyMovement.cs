using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyMovement : MonoBehaviour
{
    public float minDist = 5f;
    public float maxDist = 15f;
    public float speed = 0.5f;
    public GameObject player;
    enum Direction
    {
        Left,
        Right,
        Up,
        Down,
    };
    private enum State
    {
        None,
        Idle,
        Roam,
        ChasePlayer
    };
    private State state;

    private void Start()
    {
        state = State.Idle;
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
            case State.ChasePlayer:
                ChasePlayer();
                break;
            default:
                state = State.Idle;
                break;
        }
    }

    void ChasePlayer()
    {
        Vector3 localPosition = player.transform.position - transform.position;
        if (localPosition.sqrMagnitude >= minDist * minDist)
        {
            if (localPosition.sqrMagnitude <= maxDist * maxDist)
            {
                localPosition = localPosition.normalized; // The normalized direction in LOCAL space
                transform.Translate(localPosition.x * Time.deltaTime * speed, localPosition.y * Time.deltaTime * speed, localPosition.z * Time.deltaTime * speed);
            }
            else
            {
                state = State.Idle;
            }
        }
    }

    void Idle()
    {
        StartCoroutine(waitASec());
    }

    IEnumerator waitASec()
    {
        yield return new WaitForSeconds(1);
        state = State.Roam;
    }

    void Roam()
    {
        
        var direction = Random.Range(0, 3);
        switch(direction)
        {
            case (int)Direction.Left:
                transform.Translate(-1 * Time.deltaTime * speed, 0, 0);
                break;
            case (int)Direction.Right:
                transform.Translate(1 * Time.deltaTime * speed, 0, 0);
                break;
            case (int)Direction.Up:
                transform.Translate(0, -1 * Time.deltaTime * speed, 0);
                break;
            case (int)Direction.Down:
                transform.Translate(0, 1 * Time.deltaTime * speed, 0);
                break;
        }
        ScanForPlayer();
    }

    void ScanForPlayer()
    {
        Vector3 localPosition = player.transform.position - transform.position;
        if (localPosition.sqrMagnitude >= minDist * minDist)
        {
            if (localPosition.sqrMagnitude <= maxDist * maxDist)
            {
                state = State.ChasePlayer;
            }
            else
            {
                state = State.Roam;
            }
        }
    }
}
