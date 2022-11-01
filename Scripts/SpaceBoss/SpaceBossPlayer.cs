using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceBossPlayer : MonoBehaviour
{
    [SerializeField] private int speed = 10;
    //Border will determine how far the player can move as to stay in the field
    private float border;

    private int lives = 3;
    //The awake method will activated upon creation
    private void Awake()
    {

    }

    // Update is called once per frame
    void LateUpdate()
    {
        Movement();
    }

    //Will be used to set borders for the player once the player get's access to the width of screen
    private void Movement()
    {
        //debating wether to make a local translation variable so we don't have to contiune making a new one each time
        Vector3 translation = new Vector2((Input.GetAxis("Horizontal") * speed * Time.deltaTime), 0);


        //This if statment will keep the player inside the game object
        if(transform.localPosition.x + translation.x < border && transform.localPosition.x + translation.x > border * -1)
            transform.localPosition += translation;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyMissle"))
        {
            Destroy(collision.gameObject);

            if (--lives <= 0)
                Destroy(this.gameObject);

        }
    }


    public int GetSpeed()
    {
        return speed;
    }

    public void SetSpeed(int speed = 10)
    {
        this.speed = speed;
    }

    public void SetBorder(float width)
    {
        border = width / 2;
    }
}
