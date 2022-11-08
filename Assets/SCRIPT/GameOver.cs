using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class GameOver : MonoBehaviour
{
    public Text scoreDisplay;
    public GameObject resetButton;
    public GameObject waitingText;

    PhotonView view;
    void Start()
    {
        view = GetComponent<PhotonView>();
        scoreDisplay.text = FindObjectOfType<Score>()._score.ToString();

        if(PhotonNetwork.IsMasterClient == false)
        {
            resetButton.SetActive(false);
            waitingText.SetActive(true);
        }
    }

    
    void Update()
    {
        
    }

    public void OnClickRestart()
    {
        view.RPC("Restart", RpcTarget.All);
    }

    [PunRPC]
    void Restart()
    {
        PhotonNetwork.LoadLevel("Game");
        
    }
}
