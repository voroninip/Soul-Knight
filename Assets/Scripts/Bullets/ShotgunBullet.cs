using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunBullet : Bullet
{
    private float expirationDistance = 4f;
    
    public override void Action()
    {
        var distance = bulletSpeed * Time.deltaTime;
        
        expirationDistance -= distance;
        
        if (expirationDistance <= 0)
        {
            Destroy(gameObject);
        }
    }
}