using UnityEngine;

public class Enemy : MonoBehaviour
{
    public AudioClip deathAudio;
    public float health;

    void Start()
    {
        health = 100f;
    }

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
}
