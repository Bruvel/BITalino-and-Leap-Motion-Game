using UnityEngine;
using Leap;
using Leap.Unity;
using UnityEngine.UI;

public class ControladorLeapMotion: MonoBehaviour
{
    public  ControladorPelota control;
    LeapProvider proveedor;
    Frame frame;
    Hand mano;

    void Start()
    {
        proveedor = FindObjectOfType<LeapProvider>() as LeapProvider;
    }

    void Update()
    {
        if (proveedor.isActiveAndEnabled)
        {
            frame = proveedor.CurrentFrame;
            if (frame.Hands.Count > 0)
            {
                mano = frame.Hands[0];
                if (control.BolaActiva())
                    Movimiento();
            }
        }
    }

    void Movimiento()
    {
        //Debug.Log("Ulnar izquierda: "+ mano.Direction.Yaw);
        //Debug.Log("Supinación izquierda: "+ mano.Direction.Roll);
        
        //Mano derecha e izquierda
        /**
        if (mano.PinchStrength >= 0.9f)
        {
            //Debug.Log("Puño: " + mano.PinchStrength);
            control.MostrarMovimiento("puño");
            control.MotionControl("adelante");
        }
        */
        if (mano.Direction.Pitch.IsBetween(1, 2))
        {
            control.MotionControl("adelante");
        }
        else if (mano.Direction.Yaw.IsBetween(2, 2.8f))
        {
            control.MotionControl("derecha");
        }
        else if (mano.Direction.Roll.IsBetween(0, 1))
        {
            control.MotionControl("izquierda");
        }
        else
        {
            control.MotionControl("neutro");
        }
    }
}
