using UnityEngine;
using Photon.Pun;
using System.Collections;

public class PlayerColor : MonoBehaviourPunCallbacks
{
    private Renderer myRenderer;
    private MaterialPropertyBlock propBlock;

    void Awake()
    {
        myRenderer = GetComponent<Renderer>();
        if (myRenderer != null)
        {
            // Clonar el material para asegurarse de que cada instancia tiene su propio material
            myRenderer.material = new Material(myRenderer.material);
        }
        propBlock = new MaterialPropertyBlock();
    }

    void Start()
    {
        if (photonView.IsMine)
        {
            ChangeColor();
        }
    }

    public void ChangeColor()
    {
        StartCoroutine(ChangeColorAfterDelay());
    }

    IEnumerator ChangeColorAfterDelay()
    {
        yield return new WaitForSeconds(0.1f); // Espera 100 milisegundos antes de aplicar el color
        Color newColor = new Color(Random.value, Random.value, Random.value);
        photonView.RPC("UpdateColor", RpcTarget.AllBuffered, newColor.r, newColor.g, newColor.b);
    }

    [PunRPC]
    void UpdateColor(float r, float g, float b)
    {
        Debug.Log("RPC Received - Updating Color");
        ApplyColor(new Color(r, g, b));
    }

    private void ApplyColor(Color color)
    {
        Debug.Log("Applying Color: " + color);
        if (myRenderer != null)
        {
            myRenderer.GetPropertyBlock(propBlock);
            propBlock.SetColor("_Color", color);
            myRenderer.SetPropertyBlock(propBlock);
        }
        else
        {
            Debug.LogError("Renderer not found on " + gameObject.name);
        }
    }
}
