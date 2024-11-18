using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class MainMenuLauncher : MonoBehaviourPunCallbacks
{
    public TMP_InputField usernameInput;
    public TMP_Text buttonText;
    public void OnClickConnect()
    {
        if (usernameInput.text.Length > 1)
        {
            PhotonNetwork.NickName = usernameInput.text;    
            PlayerPrefs.SetString("PlayerName", usernameInput.text);
            buttonText.text = "Conectando ...";
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Nos hemos conectado al Master");
        SceneManager.LoadScene("SceneMultiplayer");
    }
}
