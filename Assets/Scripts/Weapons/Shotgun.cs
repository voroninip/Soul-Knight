using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : PlayerGun
{
    [SerializeField] private float angle = 3f;
    private int bulletCount = 5;

    Shotgun()
    {
        fireRate = 5.0f;
    }

    private Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Vector3 angles)
    {
        var dir = point - pivot;
        dir = Quaternion.Euler(angles) * dir;
        point = dir + pivot;
        return point;
    }

    public override void CreateBullets(Vector3 position)
    {
        Console.WriteLine("Shotgun");

        var playerPosition = player.position;
        for (var i = 0; i < bulletCount; ++i)
        {
            var bulletPosition =
                RotatePointAroundPivot(position, playerPosition,
                    new Vector3(0, 0, (i - (bulletCount - 1) / 2.0f) * angle));

            var direction = bulletPosition - playerPosition;
            direction.Normalize();

            var tempAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            var bulletRotation = Quaternion.Euler(new Vector3(0, 0, tempAngle));

            GameObject bulletInstance = Instantiate(bulletPrefab, bulletPosition, bulletRotation);

            var bullet = bulletInstance.GetComponent<Bullet>();

            bullet.damage = 5.0f * this.multiplier;
            bullet.bulletSpeed = 7;
            
            SetBulletDirection(bullet, bulletInstance.transform.position);
        }
    }
}