using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class Conection : MonoBehaviourPunCallbacks
{
    public Button button;
    bool loading = false;
    //string roomNumber;
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.AutomaticallySyncScene = true;
    }
    void Update()
    {
        if (!loading && PhotonNetwork.IsMasterClient && PhotonNetwork.InRoom && PhotonNetwork.CurrentRoom.PlayerCount > 1)
        {
            loading = true;
            PhotonNetwork.LoadLevel(2);
        }
    }

    override
        public void OnConnectedToMaster()
    {
        button.interactable = true;
    }
    public void PushButton() {
        PhotonNetwork.JoinOrCreateRoom("sala1", new RoomOptions() { MaxPlayers = 3 }, TypedLobby.Default);
    }
}
