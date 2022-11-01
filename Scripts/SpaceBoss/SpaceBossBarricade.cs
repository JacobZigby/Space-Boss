using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceBossBarricade : MonoBehaviour
{
    private int timesHit = 0;
    private int lives = 3;
    private Color[] colors = { new Color(1f, 0f, 0f, 0.8f), new Color(0.8f, 0f, 0f, 0.6f), new Color(0.7f, 0f, 0f, 0.4f) };
    private SpriteRenderer renderer;

    private void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();

        //Error message
        if (renderer == null)
            Debug.Log("Renderer was not loaded");
        //error message
        if(lives > colors.Length)
        {
            Debug.Log("The lifes count and color array do not match in quantity");
        }
        renderer.color = colors[timesHit];
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger has been Called");
        if(collision.CompareTag("EnemyMissle"))
        {
            Destroy(collision.gameObject);
            if (++timesHit < lives)
            {
                Debug.Log("Times hit: " + timesHit);
                renderer.color = colors[timesHit];
            }
            else
                Destroy(this.gameObject);
        }
    }
}
