using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Enemy : MonoBehaviour
{
    Score scoreScript;
    PlayerCtrl[] Players;
    PlayerCtrl nearestPlayer;
    SpriteRenderer _Sp;
    public float _speed;

    public GameObject enemyDeath;
    PhotonView view;
    void Start()
    {
        view = GetComponent<PhotonView>();
        Players = FindObjectsOfType<PlayerCtrl>();
        scoreScript = FindObjectOfType<Score>();
        _Sp = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        float distanceOne = Vector2.Distance(transform.position, Players[0].transform.position);
        float distanceTwo = Vector2.Distance(transform.position, Players[1].transform.position);

        if(distanceOne < distanceTwo)
        {
            nearestPlayer = Players[0];
        }
        else
        {
            nearestPlayer = Players[1];
        }

        if(nearestPlayer != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, nearestPlayer.transform.position, _speed * Time.deltaTime);
        }


        if ((nearestPlayer.transform.position.x - this.transform.position.x) < 0)
        {
            _Sp.flipX = true;
        } else
        {
            _Sp.flipX = false;
        }

    }

    private void OnTriggerEnter2D(Collider2D orther)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if (orther.tag == "GoldenRay")
            {
                view.RPC("SpawnFX", RpcTarget.All);
                PhotonNetwork.Destroy(this.gameObject);
                scoreScript.AddScore();
            }
        }
        
    }

    [PunRPC]
    public void SpawnFX()
    {
        Instantiate(enemyDeath, transform.position, Quaternion.identity);
    }
}
