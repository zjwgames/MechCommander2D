using UnityEngine;

public class Player : MonoBehaviour
{

    public float moveSpeed = 5f;

    public GameObject mech;

    Vector2 movement;

    public enum State
    {
        None,
        Free,
        UnitControl
    };
    protected State state;

    public State PlayerState
    {
        get { return state; }
    }

    void Start()
    {
        state = State.Free;
    }

    void Update()
    {
        if (mech != null)
        {
            switch (state)
            {
                case State.Free:
                    Free();
                    break;
                case State.UnitControl:
                    UnitControl();
                    break;
                default:
                    state = State.Free;
                    break;
            }
        } else
        {
            state = State.Free;
            Free();
        }
    }

    void Free()
    {
        moveSpeed = 5f * (GetComponentInChildren<Zoom>().zoomLevel * 0.1f);
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        if (Input.GetButtonDown("Switch") && mech != null)
        {
            mech.GetComponent<Mech>().isSelected = true;
            state = State.UnitControl;
        }
    }

    void UnitControl()
    {

        Vector3 newPosition = new Vector3(
            mech.transform.position.x,
            mech.transform.position.y,
            transform.position.z);
        transform.position = newPosition;
        if (Input.GetButtonDown("Switch"))
        {
            mech.GetComponent<Mech>().isSelected = false;
            mech.GetComponentInChildren<Movement>().movement = new Vector2();
            state = State.Free;
        }
    }
    void FixedUpdate()
    {
        if (state == State.Free)
        {
            var currentPosition = new Vector2(transform.position.x, transform.position.y);
            var newPosition = currentPosition + movement * moveSpeed * Time.fixedDeltaTime;
            var newPositionIn3D = new Vector3(newPosition.x, newPosition.y, transform.position.z);
            transform.position = newPositionIn3D;
        }
    }
}
