using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : PlayerGun
{
    Rifle()
    {
        fireRate = 5.0f;
    }

    public override void CreateBullets(Vector3 position)
    {
        GameObject bulletInstance = Instantiate(bulletPrefab, position, selfTransform.rotation);

        Bullet bullet = bulletInstance.GetComponent<Bullet>();
        
        bullet.damage = 5.0f * this.multiplier;
        bullet.bulletSpeed = 10;
        
        SetBulletDirection(bullet, position);
    }
}