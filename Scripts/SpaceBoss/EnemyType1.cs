using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyType1 : SpaceBossEnemyModel
{

    private float moveTimer = 0.5f;
    private float moveSpeed = 0.5f;

    private Vector3 startPosition = new Vector3();
    private Vector3 endPosition = new Vector3();

    //bring the updates and awakes and starts up here

    protected override void Movement()
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

    protected override void SetUpMuzzles()
    {
        //change this to a for each loop next time
        Transform tmp = null;
        for (int index = 0; index < transform.childCount; index++)
        {
            tmp = transform.GetChild(index);
            if (tmp.tag == "Muzzle")
                muzzleChildren.Add(tmp);
        }
    }

    protected override void FireProjectile()
    {
        for (int index = 0; index < muzzleChildren.Count; index++)
            Instantiate(projectile, muzzleChildren[index].position, projectileRotation, transform.parent.parent);
    }

    protected override void SetProjectileRotation()
    {
        projectileRotation.eulerAngles = transform.eulerAngles;
    }

    protected override void FireTimerUpdate()
    {
        fireTimer -= Time.deltaTime;
        if (fireTimer <= 0)
        {
            FireProjectile();
            fireTimer = fireRate;
        }
    }
}
