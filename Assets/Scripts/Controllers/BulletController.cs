using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private LinkedList<Bullet> bullets = new LinkedList<Bullet>();  

    void FixedUpdate()
    {
        for (var node = bullets.First; node != null; node = node.Next)
        {
            var bullet = node.Value;
            
            if (bullet == null)
            {
                bullets.Remove(node);
                continue;
            }
            
            bullet.Action();
        }
    }

    public void AddBullet(Bullet bullet)
    {
        bullets.AddLast(bullet);
    }
}
