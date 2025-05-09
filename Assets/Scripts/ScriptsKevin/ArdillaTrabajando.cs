using Photon.Pun;
using UnityEngine;

public class ArdillaTrabajando : MonoBehaviourPun
{
    public bool trabajando = true;

    public delegate void OnTrabajandoChange(bool trabajando);
    public static event OnTrabajandoChange TrabajandoChange;

    public void SetTrabajando(bool estado)
    {
        if (PhotonNetwork.IsConnected)
        {
            photonView.RPC("RPC_SetTrabajando", RpcTarget.AllBuffered, estado);
        }
        else
        {
            RPC_SetTrabajando(estado);
        }
    }

    [PunRPC]
    void RPC_SetTrabajando(bool estado)
    {
        if (trabajando != estado)
        {
            trabajando = estado;
            TrabajandoChange?.Invoke(trabajando);
        }
    }
}