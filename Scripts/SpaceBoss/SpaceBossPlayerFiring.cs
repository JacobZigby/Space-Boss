using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceBossPlayerFiring : MonoBehaviour
{
    //This script will focus on letting the player fire a projectile 

    //Will hold the prefab of the projectile to fire
    [SerializeField] private GameObject projectile;

    //A timer variable to reset the timer
    private readonly float fireRate = 0.25f;
    private float timer;

    //A temp variable to test the fire rate
    private int spawnCount = 0;

    //Verify that an object is attatched to the script
    void Start()
    {
        if (projectile == null)
            Debug.Log("No designated projectile prefab for use on start up");
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (Input.GetButton("Fire1") && timer <= 0)
        {
            FireProjectile();
            timer = fireRate;
        }
    }

    //Spawns the projectile into the game to be used
    private void FireProjectile()
    {
        Instantiate(projectile, transform.position, Quaternion.identity, transform.parent.parent);
    }
}
