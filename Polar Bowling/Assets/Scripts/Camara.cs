using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camara : MonoBehaviour
{
    public Transform player;

    private float posicionCamara;
    private float distancia;

    void Start()
    {
        distancia = transform.position.z - player.position.z;
    }

    void LateUpdate()
    {
        if (player != null && player.position.z < 85)
        {
            posicionCamara = player.position.z + distancia;
            transform.position = new Vector3(transform.position.x,transform.position.y,posicionCamara);
        }
    }
}
