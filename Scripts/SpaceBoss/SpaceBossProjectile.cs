using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceBossProjectile : MonoBehaviour
{
    //might change this to be an abstract class for all projectiles

    private static float speed;

    // Update is called once per frame
    void LateUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        Vector3 translation = transform.up * speed * Time.deltaTime;
        transform.localPosition += translation;
    }

    public static void setSpeed(float speed = 10)
    {
        SpaceBossProjectile.speed = speed;
    }
}
