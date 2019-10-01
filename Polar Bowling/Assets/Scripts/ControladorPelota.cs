using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControladorPelota : MonoBehaviour
{
    public Text txtMovimiento;
    public float velocidad = 5;
    float izquierdaDerecha, adelanteAtras;

    void Update()
    {
        if (transform.position.z > 95)
            gameObject.SetActive(false);
        
        if (transform.position.z > -67)
        {
            velocidad = 50;
            MotionControl("adelante");
        }      
        KeyboardControl();
    }

    //Control de videojuego con teclado
    public void KeyboardControl()
    {
        izquierdaDerecha = Input.GetAxis("Horizontal");
        adelanteAtras = Input.GetAxis("Vertical");
        transform.Translate(new Vector3(izquierdaDerecha, 0, adelanteAtras) * velocidad * Time.deltaTime);
    }

    //Control de videojuego con otro dispositivo
    public void MotionControl(string direccion)
    {
        switch (direccion)
        {
            case "adelante":
                
                transform.Translate(new Vector3(0, 0, velocidad * Time.deltaTime));
                txtMovimiento.text = "EXTENSIÓN";
                break;
            case "derecha":
                
                transform.Translate(new Vector3(1 * velocidad * Time.deltaTime, 0, 0));
                txtMovimiento.text = "ULNAR";
                break;
            case "izquierda":
                txtMovimiento.text = "SUPINACIÓN";
                transform.Translate(new Vector3(-1 * velocidad * Time.deltaTime, 0, 0));
                break;
            case "neutro":
                txtMovimiento.text = "NEUTRO";
                transform.Translate(new Vector3(0, 0, 0));
                break;
            default:
                Debug.Log("Movimiento No Reconocido");
                break;
        }
    }

    public bool BolaActiva()
    {
            return gameObject.activeInHierarchy;
    }
}
