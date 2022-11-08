using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayer : MonoBehaviour
{
    public GameObject Player;
    //public float minX, minY, maxX, maxY;
    Vector2 _border;

    private void Start()
    {
        _border = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        Vector2 pos = new Vector2(Random.Range(-_border.x, _border.x), Random.Range(-_border.y,_border.y));
        PhotonNetwork.Instantiate(Player.name, pos, Quaternion.identity);
    }
}
