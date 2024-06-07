using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Pool : MonoBehaviour
{
    [Header("Bullet Pool")]
    public GameObject Enermy_Bullet_Prefab;
    public GameObject Al_Bullet_Prefab;

    private List<GameObject> enemyBullets;
    private List<GameObject> aiBullets;

    public int bulletPoolSize = 10; // Adjust this to set the number of bullets in the pool

    public static Bullet_Pool Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    void Start()
    {
        enemyBullets = new List<GameObject>();
        aiBullets = new List<GameObject>();

        // Initialize the bullet pools
        for (int i = 0; i < bulletPoolSize; i++)
        {
            GameObject enemyBullet = Instantiate(Enermy_Bullet_Prefab, transform);
            enemyBullet.SetActive(false);
            enemyBullets.Add(enemyBullet);

            GameObject aiBullet = Instantiate(Al_Bullet_Prefab, transform);
            aiBullet.SetActive(false);
            aiBullets.Add(aiBullet);
        }
    }

    public GameObject GetEnemyBullet()
    {
        foreach (GameObject bullet in enemyBullets)
        {
            if (!bullet.activeInHierarchy)
            {
                bullet.transform.SetParent(transform);
                bullet.SetActive(true);
                return bullet;
            }
        }

        // If no bullets are available, create a new one and add it to the pool
        GameObject newBullet = Instantiate(Enermy_Bullet_Prefab);
        newBullet.transform.SetParent(transform);
        enemyBullets.Add(newBullet);
        return newBullet;
    }

    public GameObject GetAIBullet()
    {
        foreach (GameObject bullet in aiBullets)
        {
            if (!bullet.activeInHierarchy)
            {
                bullet.transform.SetParent(transform);
                bullet.SetActive(true);
                return bullet;
            }
        }

        // If no bullets are available, create a new one and add it to the pool
        GameObject newBullet = Instantiate(Al_Bullet_Prefab);
        newBullet.transform.SetParent(transform);
        aiBullets.Add(newBullet);
        return newBullet;
    }
}

