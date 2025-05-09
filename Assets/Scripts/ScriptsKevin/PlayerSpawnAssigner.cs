using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Photon.Pun;

public class PlayerSpawnAssigner : MonoBehaviour
{
    public Transform spawnPoint1, spawnPoint2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
     if (PhotonNetwork.IsMasterClient)
            PhotonNetwork.Instantiate("Player1", spawnPoint1.position, spawnPoint1.rotation);
     else
            PhotonNetwork.Instantiate("Player2", spawnPoint2.position, spawnPoint2.rotation);
    }
}
