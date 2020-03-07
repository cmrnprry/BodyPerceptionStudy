using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SIManager : MonoBehaviour
{

    public float playerSpeed = 10.0f;
    public float bulletSpeed = 4.0f;
    public float leftBound, rightBound, leftSpawn, rightSpawn, spawnTime;
    public int startDiff, endDiff = 0;
   

    // Private Fields 
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject player;
    private PlayerManager pm;
    [SerializeField] private List<GameObject> enemyTypes;
    [SerializeField] private Transform bulletLoc;
    [SerializeField] private GameObject UI;
    [SerializeField] private GameManager gm;

    public void StartGame()
    {
        pm = player.GetComponent<PlayerManager>();
        UI.SetActive(true);
        
        //Game logic
        StartCoroutine(PlayerMovement());
        StartCoroutine(PlayerShooting());
        StartCoroutine(SpawnEnemy());
        StartCoroutine(GameOver());
    }

    void StopGame()
    {
       
        StopCoroutine(PlayerMovement());
        StopCoroutine(PlayerShooting());
        StopCoroutine(SpawnEnemy());
        StopCoroutine(GameOver());
        UI.SetActive(false); 

    }

    IEnumerator GameOver()
    {
        if (pm.health <= 0)
        {
            gm.TeleportPlayer(0);
            UI.SetActive(false);

            StopGame();
            yield break;
        }

        yield return new WaitForEndOfFrame();
        StartCoroutine(GameOver());
    }

    IEnumerator PlayerMovement()
    {
        // if the player is within bounds
        if (player.gameObject.transform.position.x >= leftBound)
        {
            //Move Left
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                Vector3 temp = new Vector3((-1 * playerSpeed * Time.deltaTime), 0, 0);
                player.transform.position += temp;
                Debug.Log("move left");

            }
        }

        if (player.gameObject.transform.position.x <= rightBound)
        { 
            //Move Right
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                Vector3 temp = new Vector3((playerSpeed * Time.deltaTime), 0, 0);
                player.transform.position += temp;
                Debug.Log("move right");

            }
        }

        yield return new WaitForEndOfFrame();
        StartCoroutine(PlayerMovement());
    }

    IEnumerator PlayerShooting()
    {
        //Shooting
        if (Input.GetKey(KeyCode.Space))
        { 
            var b = Instantiate(bullet, bulletLoc.position, bullet.transform.rotation);
            StartCoroutine(BulletMovement(b));
        }

        //wait 0.25 seconds until you can shoot the next bullet
        yield return new WaitForSeconds(0.25f);
        StartCoroutine(PlayerShooting());
    }

    IEnumerator BulletMovement(GameObject b)
    {
        while (b.transform.position.y <= 15.0)
        {
            Vector3 temp = new Vector3(0, (bulletSpeed * Time.deltaTime), 0);
            b.transform.position += temp;
            yield return null;
        }
        
        Destroy(b);
               
    }

    IEnumerator SpawnEnemy()
    {
        DetermineDificulty();
        var spawnPoint = new Vector3(Random.Range(leftSpawn, rightSpawn), 15.0f, 0);
        var e = Instantiate(ChooseEnemy(), spawnPoint, Quaternion.identity);
        StartCoroutine(EnemyMovement(e));

        yield return new WaitForSeconds(spawnTime);
        StartCoroutine(SpawnEnemy());
    }

    void DetermineDificulty()
    {
        switch (pm.scoreVal)
        {
            //easiest, only one enemy that doesn't shoot
            case int val when (val <= 50):
                endDiff = 1;
                spawnTime = 3f;
                break;
            case int val when (val <= 100):
                endDiff = 1;
                spawnTime = 2.75f;
                break;

            //second level, two enemies where one shoots
            case int val when (val <= 250):
                endDiff = 2;
                spawnTime = 2.5f;
                break;
            case int val when (val <= 350):
                endDiff = 2;
                spawnTime = 2f;
                break;

            //third level, three enemies where two shoot, spawn is bit faster
            case int val when (val <= 500):
                endDiff = 3;
                spawnTime = 1.75f;
                break;
            case int val when (val <= 750):
                endDiff = 3;
                spawnTime = 1.5f;
                break;

            //fourth level, all enemies where three shoot, spawn is bit faster
            case int val when (val <= 1000):
                startDiff = 1;
                endDiff = 4;
                spawnTime = 1.25f;
                break;

            //very hard, all enemies where three shoot, very fast
            default:
                startDiff = 2;
                spawnTime = 1f;
                break;

        }
    }

    GameObject ChooseEnemy()
    {
        var val = Random.Range(startDiff, endDiff);

        return enemyTypes[val];
    }
    
    IEnumerator EnemyMovement(GameObject e)
    {
        var speed = e.GetComponent<EnemyManager>().enemySpeed;

        while (e.transform.position.y >= -3.0)
        {
            Vector3 temp = new Vector3(0, (-speed * Time.deltaTime), 0);
            e.transform.position += temp;
            yield return null;
        }

        Destroy(e);
    }
}
