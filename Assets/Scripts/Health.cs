using UnityEngine;

public class Health : MonoBehaviour
{
    public float health;
    public AudioClip deathAudio;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            GameObject targetObject = GameObject.Find("Sound Controller");
            object[] args = new object[] { deathAudio, transform.position };
            targetObject.SendMessage("PlayAudioClip", args);
            Destroy(gameObject);
        }
    }

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
    }
}
