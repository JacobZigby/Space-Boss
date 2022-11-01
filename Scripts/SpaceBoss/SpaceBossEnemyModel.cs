using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpaceBossEnemyModel : MonoBehaviour
{
    /*
     * Every enemy needs:
     * 
     * Fields:
     * 
     * projectile
     * muzzles
     * lives
     * (maybe a firerate and firetimer)
     * projectileRotation
     * maybe a move timer and move speed
     * 
     * Methods:
     * An awake that sets everything up, but that can also be done per script
     * an update to lower the potential timer
     * and a late update for movement
     * 
     * neccesary:
     * movement
     * setupmuzzle
     * fireprojectile
     * onTirgger
     * setBorder
     */
    
    [SerializeField] protected GameObject projectile;
    protected List<Transform> muzzleChildren = new List<Transform>();
    protected int lives = 3;

    protected float fireRate = 1f;
    protected float fireTimer = 1f;
    protected Quaternion projectileRotation;

    protected abstract void Movement();
    protected abstract void SetUpMuzzles();
    protected abstract void FireProjectile();
    protected abstract void SetProjectileRotation();
    protected abstract void FireTimerUpdate();


}
