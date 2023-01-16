using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float maxLifetime = 1f;
    private float lifetime = 0f;

    void Update()
    {
        lifetime += 1 * Time.deltaTime;
        if (lifetime > maxLifetime) Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
