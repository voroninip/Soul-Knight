using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperRifle : PlayerGun
{
    SniperRifle()
    {
        fireRate = 5.0f;
    }
    
    public override void CreateBullets(Vector3 position)
    {
        GameObject bulletInstance = Instantiate(bulletPrefab, position, selfTransform.rotation);

        Bullet bullet = bulletInstance.GetComponent<Bullet>();
        
        bullet.damage = 10.0f * this.multiplier;
        bullet.bulletSpeed = 40;
        
        SetBulletDirection(bullet, position);
    }
}