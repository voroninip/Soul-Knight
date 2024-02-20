using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 2.0f;

    float health = 60.0f;

    public Rigidbody2D rb;
    public Animator animator;

    Vector2 spawnPoint;
    Vector2 randomPoint;
    public float radius = 15.0f;

    MapGenerator mapGenerator;

    Transform player;

    TMP_Text energyText;

    float last_point_pick = 0.0f;

    //states enum
    public enum State
    {
        attack,
        stagger
    }

    State state = State.stagger;


    [SerializeField] private EnemyGun gun;
    // private bool isShooting = false;

    void Start()
    {
        spawnPoint = transform.position;
        randomPoint = spawnPoint;
        mapGenerator = GameObject.FindWithTag("MapGenerator").GetComponent<MapGenerator>();
        player = GameObject.FindWithTag("Player").transform;
        energyText = GameObject.Find("energy").GetComponent<TMP_Text>();
    }

    //get random point in a circle
    private Vector2 GetRandomPointInCircle(float radius)
    {
        float angle = UnityEngine.Random.Range(0f, Mathf.PI * 2f);
        float distance = Mathf.Sqrt(UnityEngine.Random.Range(0.5f, 1f)) * radius;

        float x = spawnPoint.x + Mathf.Cos(angle) * distance;
        float y = spawnPoint.y + Mathf.Sin(angle) * distance;

        return new Vector2(x, y);
    }

    //function to move randomly around the spawnpoint
    private void WalkAround()
    {
        //check if close enough to randomPoint
        if (Vector2.Distance(transform.position, randomPoint) < 0.1f || (Time.time - last_point_pick) > 3.0f)
        {
            do
            {
                randomPoint = GetRandomPointInCircle(radius);
            } while (!mapGenerator.CheckPointIsClear(randomPoint));
            last_point_pick = Time.time;
        }

        //move to random point
        var movement = Vector2.MoveTowards(transform.position, randomPoint, moveSpeed * Time.deltaTime);
        
        var dir = randomPoint - (Vector2)transform.position;
        
        animator.SetFloat("Horizontal", dir.x);
        animator.SetFloat("Vertical", dir.y);
        animator.SetFloat("Speed", dir.sqrMagnitude);
        
        transform.position = movement;
    }

    private void Update()
    {

        gun.RotateWeapon();
        if (Vector2.Distance(transform.position, player.position) < 6f) {
            state = State.attack;
        }
        if (Vector2.Distance(transform.position, player.position) > 8f) {
            state = State.stagger;
        }

        switch (state) 
        {
            case State.attack:
            {
                Attack();
                break;
            }
            case State.stagger: 
            {
                WalkAround();
                break;
            }
        }
    }

    public void TakeDamage(float damage) {
        health -= damage;
        Debug.Log("Enemy health: " + health + " damage: " + damage);


        if (health <= 0)
        {
            Debug.Log("Enemy died");
            if (PlayerPrefs.HasKey("energy") == false)
            {
                PlayerPrefs.SetInt("energy", 0); // пример значения
            }
            int energy = PlayerPrefs.GetInt("energy");
            PlayerPrefs.DeleteKey("energy");
            PlayerPrefs.SetInt("energy", energy + 1);
            energyText.text = "Energy: " + (energy + 1).ToString() + "/15";
            Debug.Log("energy text : " + energy);
            if (energy >= 15) {

                SceneManager.LoadScene("WinScreen");
            }

            Destroy(gun);
            Destroy(gameObject);
        }
    }

    private void Attack()
    {
        animator.SetFloat("Horizontal", 0);
        animator.SetFloat("Vertical", 0);
        animator.SetFloat("Speed", 0);
        
        gun.Shoot();
    }
}