using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    public float bulletSpeed = 1f;
    public Vector3 direction;
    public float damage = 1;
    public Rigidbody2D rb;
    public bool isEnemyBullet = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public abstract void Action();

    private void OnCollisionEnter2D(Collision2D other)
    {

        bool isPlayer = other.gameObject.CompareTag("Player");
        bool isEnemy = other.gameObject.CompareTag("Enemy");
        bool isBullet = other.gameObject.CompareTag("Bullet");

        bool isOwnBullet = (!isEnemyBullet && isPlayer) || (isEnemyBullet && isEnemy) || isBullet;
        bool isDamageBullet = (isEnemyBullet && isPlayer) || (!isEnemyBullet && isEnemy);

        if (isDamageBullet) {
            Destroy(gameObject);
            if (isEnemy) {
                other.gameObject.GetComponent<EnemyMovement>().TakeDamage(damage);
            } else {
                other.gameObject.GetComponent<PlayerMovement>().TakeDamage(damage);
            }
        }

        if (isOwnBullet)
        {
            return;
        }

        Destroy(gameObject);
    }
}