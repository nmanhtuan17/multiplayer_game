using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Health : MonoBehaviour
{
    public static Health instance;


    public int health;
    public Text healthDisplay;
    PhotonView view;

    public GameObject GameOver;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        view = GetComponent<PhotonView>();
    }

    void Update()
    {
        
    }
    public void TakeDamage()
    {
        view.RPC("TakeDamageRPC", RpcTarget.All);
    }

    [PunRPC]
    public void TakeDamageRPC()
    {   
        health--;
        if(health <= 0)
        {
            health = 0;
            GameOver.SetActive(true);

        }
        healthDisplay.text = health.ToString();
    }
}
