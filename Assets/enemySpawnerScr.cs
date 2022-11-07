using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawnerScr : MonoBehaviour
{
    /*
    class Enemy
    {
        GameObject enemyType; //prefab for enemy
    }
    */
    [Header("Spawner settings")]
    [SerializeField] float radius=10f;
    [SerializeField] float minSpawnRate;
    [SerializeField] float maxSpawnRate;
    float nextSpawn;
    float spawnRad;
    [SerializeField]GameObject enemy;
    Vector2 spawnPos;
    void Start()
    {
        nextSpawn=Random.Range(minSpawnRate,maxSpawnRate);
        StartCoroutine(spawn());
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(spawn());
        }
    }

    IEnumerator spawn()
    {
        spawnRad=Mathf.Deg2Rad*Random.Range(0,361);
        spawnPos=new Vector2(Mathf.Cos(spawnRad)*radius,Mathf.Sin(spawnRad)*radius);
        //Debug.Log(spawnPos);
        Instantiate(enemy,spawnPos,Quaternion.Euler(0,0,0));
        yield return new WaitForSeconds(nextSpawn);
        nextSpawn=Random.Range(minSpawnRate,maxSpawnRate);
        StartCoroutine(spawn());
        yield break;
    }
}
