using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : Mech
{
    public Transform firePoint;

    public GameObject bulletPrefab;

    public float bulletForce = 20f;

    public AudioSource shootingAudioSource;

    public AudioClip shot;

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<Mech>().isSelected) {
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }
        }
    }

    void Shoot()
    {
        shootingAudioSource.PlayOneShot(shot);
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
    }
}
