using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Launcher : MonoBehaviourPunCallbacks
{

    public PhotonView playerPrefab;

    public Transform spawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.JoinRandomOrCreateRoom();
    }


    public override void OnJoinedRoom()
    {
        GameObject player = PhotonNetwork.Instantiate(playerPrefab.name, spawnPoint.position,spawnPoint.rotation);
        player.GetComponent<PhotonView>().RPC("SetNameText", RpcTarget.AllBuffered, PlayerPrefs.GetString("PlayerName"));

        // Cambiar color localmente y luego sincronizar a través de un RPC
        PlayerColor playerColor = player.GetComponent<PlayerColor>();
        if (playerColor != null)
        {
            playerColor.ChangeColor(); // Asegúrate de que este método ajuste el color localmente y llame a un RPC si es necesario
        }


    }

}
