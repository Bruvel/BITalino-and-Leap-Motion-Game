using Leap;
using Leap.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControladorBITalinoLeap : MonoBehaviour
{
    public ConnectionStateBITalino miBitalino;
    public ControladorPelota miControl;
    public Ventana miVentana;
    public Feature miMAV;
    public xmlEdit miEditor;

    LeapProvider proveedor;
    Frame frame;
    Hand mano;

    public int numeroDesvEstandar;
    private int tamañoVentana;

    //private float promedioNeutro, desEstNeutro, limiteSupNeutro;
    private float promedioExtensión, desEstExtensión, limiteInfExtensión, limiteSupExtensión;
    //private float promedioUlnar, desEstUlnar, limiteInfUlnar, limiteSupUlnar;

    private double[] señal;
    private float ACC, MAV1, MAV2;

    void Start()
    {
        proveedor = FindObjectOfType<LeapProvider>() as LeapProvider;
        tamañoVentana = 500;

        promedioExtensión = miEditor.ObtenerDato("promedio", "id", "23");
        desEstExtensión = miEditor.ObtenerDato("de", "id", "24");

        //arrayMAV = new double[2];
        /**
        promedioNeutro = (float)miEditor.ObtenerDato("promedio", "id", "7");
        promedioUlnar = miEditor.ObtenerDato("promedio", "id", "15");
        promedioExtensión = miEditor.ObtenerDato("promedio", "id", "23");

        desEstNeutro = miEditor.ObtenerDato("de", "id", "8");
        desEstUlnar = miEditor.ObtenerDato("de", "id", "16");
        desEstExtensión = miEditor.ObtenerDato("de", "id", "24");
        */
        Limites();
    }

    private void Update()
    {
        if (miBitalino.Conectado && proveedor.isActiveAndEnabled)
        {
            //Debug.Log("Sensores conectados");
            if (miControl.BolaActiva())
            {
                Limites();
                Señal();
                Leap();
                Movimientos();
            }
        }
    }

    private void Leap()
    {
        frame = proveedor.CurrentFrame;
        if (frame.Hands.Count>0)
        {
            //Debug.Log("Se detecto una mano");
            mano = frame.Hands[0];
        }
    }

    void Limites()
    {
        /**
        limiteSupNeutro = promedioNeutro + (desEstNeutro * numeroDesvEstandar);

        limiteInfUlnar = promedioUlnar - (desEstUlnar * numeroDesvEstandar);
        limiteSupUlnar = promedioUlnar + (desEstUlnar * numeroDesvEstandar);
        */
        limiteInfExtensión = promedioExtensión - (desEstExtensión * numeroDesvEstandar);
        limiteSupExtensión = promedioExtensión + (desEstExtensión * numeroDesvEstandar);

        Debug.Log("Limite Inferior " + limiteInfExtensión);
        Debug.Log("Limite Superior " + limiteSupExtensión);
    }

    private void Señal()
    {
        señal = miBitalino.ObtenerSeñal(0).ToArray();
        ACC = (float)miBitalino.ObtenerDato(1);
        //Debug.Log(ACC);
        miVentana.CapturarVentana(señal, tamañoVentana, 1);
        MAV1 = (float)miMAV.CalcularMAV(miVentana.Segmento);
        miVentana.CapturarVentana(señal, tamañoVentana, 2);
        MAV2 = (float)miMAV.CalcularMAV(miVentana.Segmento);

        //Debug.Log("mav1: " + MAV1 + " mav2: " + MAV2);
    }

    private void Movimientos()
    {
        bool ulnar = mano.Direction.Yaw.IsBetween(2, 2.8f);

        bool extensiónInf = (MAV1 <= limiteSupExtensión || MAV2 <= limiteSupExtensión);
        bool extensiónSup = (MAV1 >= limiteInfExtensión || MAV2 >= limiteInfExtensión);
        bool extensión = extensiónInf & extensiónSup;

        bool supinación = ACC < 6.5;

        //bool neutroInf = (MAV1 <= limiteSupNeutro || MAV2 <= limiteSupNeutro);
        bool neutro = !(extensión || ulnar || supinación);

        if (extensión)
        {
            miControl.MotionControl("adelante");
        }
        if (ulnar)
        {
            miControl.MotionControl("derecha");
        }
        if (supinación)
        {
            miControl.MotionControl("izquierda");
        }
        if(neutro)
        {
            miControl.MotionControl("neutro");
        }
    }
}
