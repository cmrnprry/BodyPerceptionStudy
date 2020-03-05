using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private PlayerManager pm;
    private float bulletSpeed;
    private List<GameObject> list = new List<GameObject>();

    public float enemySpeed;
    public int points;

    [SerializeField] private float type;
    [SerializeField] private GameObject enemyBullet;


    // Start is called before the first frame update
    void Start()
    {
        pm = FindObjectOfType<PlayerManager>();
        bulletSpeed = enemySpeed + type;

        if (enemyBullet != null)
            StartCoroutine(EnemyShooting());
    }

    IEnumerator EnemyShooting()
    {
        var pos = this.transform.position - new Vector3(0, 0.15f, 0);
        var b = Instantiate(enemyBullet, pos, enemyBullet.transform.rotation);
        list.Add(b);
        StartCoroutine(EnemyBulletMovement(b));

        yield return new WaitForSeconds(2f);
        StartCoroutine(EnemyShooting());
    }

    IEnumerator EnemyBulletMovement(GameObject b)
    {
        while (b.transform.position.y >= -3.0)
        {
            Vector3 temp = new Vector3(0, (-bulletSpeed * Time.deltaTime), 0);
            b.transform.position += temp;
            yield return null;
        }

        Destroy(b);
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        var tag = other.gameObject.tag;

        if (tag == "Bullet")
        {
            other.gameObject.SetActive(false);
            gameObject.SetActive(false);
            DeleteBullets();
            pm.KeepScore(points);
        }

        if (tag == "Player")
        {
            gameObject.SetActive(false);
            DeleteBullets();
            pm.DecreaseHealth();
        }
    }

    void DeleteBullets()
    {
        foreach(var b in list)
        {
            Destroy(b);
        }
    }

}

