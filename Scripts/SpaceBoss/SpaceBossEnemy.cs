using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceBossEnemy : MonoBehaviour
{
    [SerializeField] private GameObject projectile;
    private List<Transform> muzzleChildren = new List<Transform>();

    //A temp number just for testing purposes
    private int lives = 3;

    private readonly float fireRate = 0.85f;
    private float fireTimer = 1f;
    private Quaternion projectileRotation;

    private float moveTimer = 0.5f;
    //a multiplier used to slow down how fast the enemy moves
    //will have to be made to adjust to the width of the screen
    private float moveSpeed = 0.5f;

    //The predefined values are for testing
    private Vector3 startPosition = new Vector3();
    private Vector3 endPosition = new Vector3();



    private void Awake()
    {
        SetUpMuzzles();
        //might change it so the manager script assigns the value below based on player location
        projectileRotation.eulerAngles = transform.eulerAngles;

        startPosition.z = 0;
        endPosition.z = 0;
        startPosition.y = transform.position.y;
        endPosition.y = transform.position.y;
    }

    private void Update()
    {
        fireTimer -= Time.deltaTime;
        if (fireTimer <= 0)
        {
            FireProjectiles();
            fireTimer = fireRate;
        }
    }

    private void LateUpdate()
    {
        Movement();
    }

    private void SetUpMuzzles()
    {
        //change this to a for each loop next time
        Transform tmp = null;
        for(int index = 0; index < transform.childCount; index++)
        {
            tmp = transform.GetChild(index);
            if (tmp.tag == "Muzzle")
                muzzleChildren.Add(tmp);
        }
    }

    private void FireProjectiles()
    {
        for(int index = 0; index < muzzleChildren.Count; index++)
            Instantiate(projectile, muzzleChildren[index].position, projectileRotation, transform.parent.parent);
    }

    //might change movement to follow the player more
    private void Movement()
    {
        //The movement will lerp between 2 points based on time
        if (moveTimer >= 1)
        {
            moveTimer = 0;

            //Switching values places
            Vector3 tmp = startPosition;
            startPosition = endPosition;
            endPosition = tmp;
        }
        else
            moveTimer += Time.deltaTime * moveSpeed;
        transform.localPosition = Vector3.Lerp(startPosition, endPosition, moveTimer);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //checks if the projectile is the players and deals damage
        if (collision.CompareTag("PlayerMissle"))
        {
            --lives;
            Destroy(collision.gameObject);
            //checks if it can take the hit
            if (lives <= 0)
            {
                Destroy(gameObject);
            }
        }

    }

    public void SetBorder(float width)
    {
        /*
        //will need to be changed when sprites get sent
        //use to get size of collider
        float length = GetComponentInChildren<Collider2D>().bounds.size.x;
        //will be used for scaling from global to local (ratio)
        float ratio = length / transform. 
        */

        float border = width * 1f;
        border = border / 2;
        //this will prevent the enemy going through the barrier
        border = border - transform.localScale.x;


        startPosition.x = border;
        endPosition.x = border * -1;
       
    }
}
