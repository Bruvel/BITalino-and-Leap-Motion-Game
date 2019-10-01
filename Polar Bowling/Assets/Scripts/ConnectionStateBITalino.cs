using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConnectionStateBITalino : MonoBehaviour
{
    public BitalinoManager manager;
    public BitalinoReader reader;
    public BitalinoSerialPort serial;
    public Text estado;

    //private int bufferSize;
    private double dato;

    private List<double> datosSeñal = new List<double>();

    void Start()
    {
        //bufferSize = reader.BufferSize;
        StartCoroutine(Comenzar());
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Desconectar();
        }
    }
    /// <summary>
    /// Verifica si se ha establecido la conexión
    /// </summary>
    private IEnumerator Comenzar()
    {
        estado.text = "Estableciendo conexión";
        while (!reader.asStart)
            yield return new WaitForSeconds(0.5f);
        estado.text = "Conexión establecida";
        estado.color = Color.green;
        yield return new WaitForSeconds(0.5f);
        estado.enabled = false;
    }

    /// <summary>
    /// Obtiene un conjunto de datos de la señal.
    /// </summary>
    /// <param name="canal">Señal a leer</param>
    /// <returns>Devuelve una lista con los valores de la señal</returns>
    public List<double> ObtenerSeñal(int canal)
    {
        datosSeñal.Clear();
        if (reader.asStart)
        {
            foreach (BITalinoFrame frame in reader.getBuffer())
            {
                dato = frame.GetAnalogValue(canal);
                datosSeñal.Add(dato);
            }
        }
        return datosSeñal;
    }

    /// <summary>
    /// Obtiene un dato de la señal.
    /// </summary>
    /// <param name="canal">Señal a leer</param>
    /// <returns>Devuelve el dato de la señal</returns>
    public double ObtenerDato(int canal)
    {
        if (reader.asStart)
        {
            foreach (BITalinoFrame frame in reader.getBuffer())
            {
                dato = frame.GetAnalogValue(canal);
            }
        }
        return dato;
    }

    /// <summary>
    /// Consulta el estado de la adquisición de datos
    /// </summary>
    /// <returns>Devuelve true si la adquisición esta activa, en caso contrario devuelve false</returns>
    
    public bool Conectado
    {
        get
        {
            return reader.asStart;
        }
    }

    /// <summary>
    /// Conecta el dispositivo e inicializa la adquisición de datos
    /// </summary>
    public void Conectar()
    {
        manager.Connection();
        manager.StartAcquisition();
    }

    /// <summary>
    /// Detiene la adquisición de datos y desconecta el dispositivo.
    /// </summary>
    public void Desconectar()
    {
        manager.StopAcquisition();
        manager.Deconnection();
    }
}
