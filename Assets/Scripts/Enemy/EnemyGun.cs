using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyGun : MonoBehaviour
{
 protected Transform selfTransform;
    
    private Camera mainCamera;
    [SerializeField] protected float radius = 1f;

    public Transform aborigen;

    Transform target;
    
    public GameObject bulletPrefab;
    private BulletController bulletController;
    
    private bool canFire = true;
    protected float fireRate = 5.0f;
    
    private void Start()
    {
        selfTransform = transform;
        mainCamera = Camera.main;
        target = GameObject.FindWithTag("Player").transform;
        bulletController = GameObject.FindWithTag("BulletController").GetComponent<BulletController>();
    }
    
    public void RotateWeapon()
    {
        var playerPos = target.position;
        var position = aborigen.position;
        var direction = playerPos - position;
        
        direction.Normalize();

        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        selfTransform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        
        selfTransform.position = position + direction * radius;
    }

    public abstract void CreateBullets(Vector3 position);

    protected void SetBulletDirection(Bullet bullet, Vector3 position)
    {
        bullet.direction = position - aborigen.position;
        
        bullet.direction.z = 0;
        bullet.direction.Normalize();
        bullet.direction *= bullet.bulletSpeed;
        
        bulletController.AddBullet(bullet);

        bullet.rb.velocity = bullet.direction;
    }

    private IEnumerator ShootCoroutine()
    {
        var position = selfTransform.position;

        position.z = 0;
        
        CreateBullets(position);
        StartCoroutine(FireRateHandler());
        
        yield return null;
    }
    
    private IEnumerator FireRateHandler()
    {
        float timeToWait = 1 / fireRate;
        yield return new WaitForSeconds(timeToWait);
        canFire = true;
    }
    
    public void Shoot()
    {
        if (canFire)
        {
            canFire = false;
            StartCoroutine(ShootCoroutine());
        }
    }
}

