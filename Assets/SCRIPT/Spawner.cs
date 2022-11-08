using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject[] enemyPrefabs;
    public GameObject enemyPrefab;
    public float startTimeBTWSpawn;
    float timeBTWSpawn;
    void Start()
    {
        timeBTWSpawn = startTimeBTWSpawn;
    }

    // Update is called once per frame
    void Update()
    {
        if (PhotonNetwork.IsMasterClient == false|| PhotonNetwork.CurrentRoom.PlayerCount != 2)
        {
            return;
        }
        if(timeBTWSpawn <= 0)
        {
            Vector3 spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)].position;
            //PhotonNetwork.Instantiate(enemyPrefab.name, spawnPoint, Quaternion.identity);
            PhotonNetwork.Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)].name, spawnPoint, Quaternion.identity);

            timeBTWSpawn = startTimeBTWSpawn;
        }
        else
        {
            timeBTWSpawn -= Time.deltaTime;
        }
    }
}
