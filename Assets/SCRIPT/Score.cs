using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public int _score;
    public Text ScoreDisplay;
    PhotonView view;

    void Start()
    {
        view = GetComponent<PhotonView>();
    }

    
    void Update()
    {
        
    }

    public void AddScore()
    {
        view.RPC("AddScoreRPC", RpcTarget.All);
    }

    [PunRPC]
    void AddScoreRPC()
    {
        _score++;
        ScoreDisplay.text = _score.ToString();
    }
}
