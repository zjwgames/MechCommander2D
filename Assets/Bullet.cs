using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public AudioClip hit;
    public float maxLifetime = 1f;
    private float lifetime = 0f;

    void Update()
    {
        lifetime += 1 * Time.deltaTime;
        if (lifetime > maxLifetime) Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        AudioSource.PlayClipAtPoint(hit, transform.position, 1.5f);
        Destroy(gameObject);
    }
}
