using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Movement : MonoBehaviour
{
    public AudioSource audioSource;

    public AudioClip tankMoving;

    public float moveSpeed = 5f;

    public float acceleration = 1f;

    public float rotationSpeed = 1.0f;

    public Rigidbody2D rb;

    Vector2 movement;

    Vector3 mousePos;

    public Camera cam;

    public Transform legs;

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    void FixedUpdate()
    {
        // Lerp position
        var newPosition = rb.position + movement * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(Vector2.Lerp(rb.position, newPosition, acceleration * Time.fixedDeltaTime));

        legs.position = rb.position;

        // Lerp rotation of legs
        var legsAngle = Mathf.Atan2(newPosition.y, newPosition.x) * Mathf.Rad2Deg - 90f;
        var start = legs.rotation;
        var end = Quaternion.AngleAxis(legsAngle, Vector3.forward);
        legs.rotation = Quaternion.Lerp(start, end, 1f);

        // Lerp rotation of gun
        var relativePos = mousePos - rb.transform.position;
        var angle = Mathf.Atan2(relativePos.y, relativePos.x) * Mathf.Rad2Deg - 90f;
        var startRotation = rb.transform.rotation;
        var endRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        rb.transform.rotation = Quaternion.Slerp(startRotation, endRotation, rotationSpeed * Time.fixedDeltaTime);

        if (movement.magnitude > 0) {
            Debug.Log("Playing tank moving");
            if (!audioSource.isPlaying) audioSource.Play();
        } else
        {
            Debug.Log("Tank stopped");
            audioSource.Stop();
        }
    }
}
