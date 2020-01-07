using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Setting : MonoBehaviour
{
    public ConnectionStateBITalino miBitalino;
    public Ventana miVentana;
    public Feature miMAV;
    public xmlEdit miEditor;

    public Dropdown listaMovimientos;
    public Image imagenMovimiento;
    private Sprite imagenNeutro;
    private Sprite imagenUlnar;
    private Sprite imagenExtensión;
    private List<float> listaParametros;
    private GameObject txtDato;

    private int tamañoVentana;

    private double[] señal;
    private float[] arrayMAV;


    void Start()
    {

        tamañoVentana = 500;

        listaParametros = new List<float>();
        arrayMAV = new float[2];
        txtDato = GameObject.FindGameObjectWithTag("Dato");

        imagenNeutro = Resources.Load<Sprite>("Sprites/Neutro");
        imagenUlnar = Resources.Load<Sprite>("Sprites/Ulnar");
        imagenExtensión = Resources.Load<Sprite>("Sprites/Extensión");

        /*
        Debug.Log(miEditor.ObtenerDato("promedio", "id", "pn"));
        miEditor.GuardarDato("promedio", "id", "pn", 111111111111);
        */
    }

    void FixedUpdate()
    {
        if (miBitalino.Conectado)
        {
            ObtenerSeñal();
        }
    }

    private void ObtenerSeñal()
    {
        señal = miBitalino.ObtenerSeñal(0).ToArray();
        for (int i = 0; i <arrayMAV.Length; i++)
        {
            miVentana.CapturarVentana(señal, tamañoVentana, i + 1);
            arrayMAV[i] = (float)miMAV.CalcularMAV(miVentana.Segmento);
        }
        for(int j=0; j < señal.Length; j++)
        {
            señal[j] = 0;
        }
    }

    private void MostrarParametro()
    {
        txtDato.GetComponent<InputField>().text = listaParametros[6].ToString();
    }

    public void AlmacenarMAV()
    {
        if (listaParametros.Count <= 6)
        {
            listaParametros.Add(arrayMAV[0]);
            listaParametros.Add(arrayMAV[1]);

            arrayMAV[0] = 0;
            arrayMAV[1] = 0;
            if (listaParametros.Count == 6)
            {
                DefinirUmbral();
                MostrarParametro();
            }
        }
    }

    public void GuardarParametros()
    {
        string[] elementos = new string[] { "mav", "promedio", "de" };
        string atributo = "id";
        int index = 0;
        if (listaMovimientos.value == 1)
        {
            for (int i = 1; i <=6; i++)
            {
                miEditor.GuardarDato(elementos[0], atributo, i.ToString(), listaParametros[index++]);
            }
            miEditor.GuardarDato(elementos[1], atributo, 7.ToString(), listaParametros[6]);
            miEditor.GuardarDato(elementos[2], atributo, 8.ToString(), listaParametros[7]);
            /**
            miEditor.GuardarDato(elementos[0], atributo, 1.ToString(), listaParametros[0]);
            miEditor.GuardarDato(elementos[0], atributo, 2.ToString(), listaParametros[1]);
            miEditor.GuardarDato(elementos[0], atributo, 3.ToString(), listaParametros[2]);
            miEditor.GuardarDato(elementos[0], atributo, 4.ToString(), listaParametros[3]);
            miEditor.GuardarDato(elementos[0], atributo, 5.ToString(), listaParametros[4]);
            miEditor.GuardarDato(elementos[0], atributo, 6.ToString(), listaParametros[5]);
            */
        }
        else if (listaMovimientos.value == 2)
        {
            for (int i = 9; i <= 14; i++)
            {
                miEditor.GuardarDato(elementos[0], atributo, i.ToString(), listaParametros[index++]);
            }
            miEditor.GuardarDato(elementos[1], atributo, 15.ToString(), listaParametros[6]);
            miEditor.GuardarDato(elementos[2], atributo, 16.ToString(), listaParametros[7]);
            /**
            miEditor.GuardarDato(elementos[0], atributo, 9.ToString(), listaParametros[0]);
            miEditor.GuardarDato(elementos[0], atributo, 10.ToString(), listaParametros[1]);
            miEditor.GuardarDato(elementos[0], atributo, 11.ToString(), listaParametros[2]);
            miEditor.GuardarDato(elementos[0], atributo, 12.ToString(), listaParametros[3]);
            miEditor.GuardarDato(elementos[0], atributo, 13.ToString(), listaParametros[4]);
            miEditor.GuardarDato(elementos[0], atributo, 14.ToString(), listaParametros[5]);
            */
        }
        else if (listaMovimientos.value == 3)
        {
            for (int i = 17; i <= 22; i++)
            {
                miEditor.GuardarDato(elementos[0], atributo, i.ToString(), listaParametros[index++]);
            }
            miEditor.GuardarDato(elementos[1], atributo, 23.ToString(), listaParametros[6]);
            miEditor.GuardarDato(elementos[2], atributo, 24.ToString(), listaParametros[7]);
            /**
            miEditor.GuardarDato(elementos[0], atributo, 17.ToString(), listaParametros[0]);
            miEditor.GuardarDato(elementos[0], atributo, 18.ToString(), listaParametros[1]);
            miEditor.GuardarDato(elementos[0], atributo, 19.ToString(), listaParametros[2]);
            miEditor.GuardarDato(elementos[0], atributo, 20.ToString(), listaParametros[3]);
            miEditor.GuardarDato(elementos[0], atributo, 21.ToString(), listaParametros[4]);
            miEditor.GuardarDato(elementos[0], atributo, 22.ToString(), listaParametros[5]);
            */
        }
        listaParametros.Clear();
        txtDato.GetComponent<InputField>().text = "";
    }

    private void DefinirUmbral()
    {
        float promedio = listaParametros.Average();
        float desvEst = (float)Math.Sqrt(listaParametros.Average(d => Math.Pow(d - promedio, 2)));
        listaParametros.Add(promedio);
        listaParametros.Add(desvEst);
    }

    public void ElegirMovimiento(int op)
    {
        switch (op)
        {
            case 1:
                imagenMovimiento.sprite = imagenNeutro;
                break;
            case 2:
                imagenMovimiento.sprite = imagenUlnar;
                break;
            case 3:
                imagenMovimiento.sprite = imagenExtensión;
                break;
            default:
                imagenMovimiento.sprite = null;
                break;
        }
    }
}
