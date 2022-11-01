using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceBossManager : MonoBehaviour
{
    /*
     * Spawn player prefab as a percentage size of the width and height (about five percent?) Done
     * Spawn enemy prefab as a percentage size of the width and height: Done
     * Make enemy Abstract class script and first enemy child : In progress
     * fix enemy shooting patern (Like alternate shooting from arms)
     * Animate the enemy shooting
     * Change player and enemy shooting increments 
    */
    
    [SerializeField]private GameObject pf_Player;

    [SerializeField]private GameObject pf_Barricade;

    //will be an array of potential bosses
    [SerializeField] private GameObject[] pf_Enemies;

    //Holds active player object;
    private GameObject player;
    //hold active enemy object
    private GameObject enemy;


    //These values will hold the games resolution for future calculations
    private float resolution_H, resolution_W;

    private void Awake()
    {
        SetResolution();

        SpawnPlayer();

        SpawnEnemy();

        //sets the speed of the projectiles to traverse the screen in about X secounds
        SpaceBossProjectile.setSpeed(Mathf.Floor(resolution_H / 4));
    }

    private void Start()
    {
        //Is called again as the width doesn't get properly calculated before hand but is only needed after
        SetResolution();

        SetUpPlayer();

        SetUpEnemy();

        SpawnBarricade();
        
    }

    // Update is called once per frame
    void Update()
    {
        //Jacob, Agian Delete this after the Testing is done
        if (Input.GetKeyDown(KeyCode.Q))
            Application.Quit();

    }

    //This will set the get the user's resolution and store it
    private void SetResolution()
    {
        RectTransform rt = gameObject.GetComponent<RectTransform>();
        resolution_H = rt.rect.height;
        resolution_W = rt.rect.width;
    }

    //This will spawn the player and assign any modifications that need to be made
    private void SpawnPlayer()
    {
        //this will determine how far from the bottom the player will be spawned according to user res
        float playerOffset = Mathf.Floor(resolution_H / 5);
        Vector3 spawnPoint = new Vector3(0, (resolution_H / -2) + playerOffset, 0);
        player = Instantiate(pf_Player, transform);
        player.transform.localPosition = spawnPoint;
    }

    private void SetUpPlayer()
    {
        //Accesses the player script from the player gameobject
        SpaceBossPlayer playerScript = player.GetComponent<SpaceBossPlayer>();

        //This line should give the player an approximate speed that will allow them to traverse the field in 4 seconds
        playerScript.SetSpeed((int)(resolution_W / 4));
        playerScript.SetBorder(resolution_W);

        //This segment will resize the player according to the resolution
        //Put this segment into the Spawn player to test wether it works on a final build
        float width = Mathf.Floor(resolution_W / 20);
        float height = Mathf.Floor(resolution_H / 10);
        player.transform.localScale = new Vector3(width, height, 0);
    }

    private void SpawnBarricade()
    {
        //this will be used to determine the position that is 10% above the player's y location (might change to 5%)
        float heightOffset = player.transform.localPosition.y + Mathf.Floor(resolution_H / 10);

        //Will determin how far the West and East barricades will spawn
        float widthOffset = Mathf.Floor(resolution_W / 4);

        //Barricade's height and width
        float height, width;

        width = Mathf.Floor(resolution_W / 10);
        height = Mathf.Floor(width / 4);

        GameObject tmpBarricade;

        //Will spawn three barricades
        for (int row = -1; row <= 1; row++)
        {
            tmpBarricade = Instantiate(pf_Barricade, transform);
            tmpBarricade.transform.localPosition = new Vector3(widthOffset * row, heightOffset, 0);
            tmpBarricade.transform.localScale = new Vector3(width, height, 0);
        }
        /*
         * Might use this later, spawns layerd barricades
        for (int row = -1; row <= 1; row++)
        {
            for (int column = 0; column < 3; column++)
            {
                tmpBarricade = Instantiate(pf_Barricade, transform);
                tmpBarricade.transform.localPosition = new Vector3(widthOffset * row, heightOffset + column * height, 0);
                tmpBarricade.transform.localScale = new Vector3(width, height, 0);
            }
        }*/
    }

    //spawn enemy 20% from the top 
    private void SpawnEnemy()
    {
        int enemyIndex = Random.Range(0, pf_Enemies.Length);
        float enemyOffset = Mathf.Floor(resolution_H / 5);
        Vector3 spawnPoint = new Vector3(0, (resolution_H / 2) - enemyOffset);

        Quaternion enemyRotation = new Quaternion();
        enemyRotation.eulerAngles = pf_Enemies[enemyIndex].transform.eulerAngles;
        
        enemy = Instantiate(pf_Enemies[enemyIndex], spawnPoint, enemyRotation, transform);
        
    }

    private void SetUpEnemy()
    {
        SpaceBossEnemy enemyScript = enemy.GetComponent<SpaceBossEnemy>();

        //the SetUp methods will have to resize the player, like in the barricade setUp Method
        float width = Mathf.Floor(resolution_W / 20);
        float height = Mathf.Floor(resolution_H / 20);
        enemy.transform.localScale = new Vector3(width, height, 0);
        
        //Importnant to set borders after the size of the unit is established as the borders depend on the size
        enemyScript.SetBorder(resolution_W);
    }
}
