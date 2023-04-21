using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyMovement : MonoBehaviour
{
    public float minDist = 5f;
    public float maxDist = 15f;
    public float speed = 0.5f;
    private GameObject mech;
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
        ChaseMech
    };
    private State state;

    private void Start()
    {
        mech = GameObject.Find("Mech");
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
            case State.ChaseMech:
                ChaseMech();
                break;
            default:
                state = State.Idle;
                break;
        }
    }

    void ChaseMech()
    {
        Vector3 localPosition = mech.transform.position - transform.position;
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
        ScanForMech();
    }

    void ScanForMech()
    {
        Vector3 localPosition = mech.transform.position - transform.position;
        if (localPosition.sqrMagnitude >= minDist * minDist)
        {
            if (localPosition.sqrMagnitude <= maxDist * maxDist)
            {
                state = State.ChaseMech;
            }
            else
            {
                state = State.Roam;
            }
        }
    }
}
