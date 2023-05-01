using UnityEngine;

public class Bullet : MonoBehaviour
{
    public AudioClip hit;
    public float maxLifetime = 1f;
    private float lifetime = 0f;
    private float damageAmount = 20f;

    void Update()
    {
        lifetime += 1 * Time.deltaTime;
        if (lifetime > maxLifetime) Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        AudioSource.PlayClipAtPoint(hit, transform.position, 15f);
        Destroy(gameObject);
        if (collision.gameObject.tag.Contains("Player") || collision.gameObject.tag.Contains("Enemy"))
        {
            // Check if the collided object has a health component
            Health health = collision.gameObject.GetComponent<Health>();
            if (health != null) health.TakeDamage(damageAmount);
        }
    }
}
