using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ventana : MonoBehaviour
{
    //atributos
    private int inicioSegmento;
    private int finSegmento;
    
    //Método que captura un segmento de la señal
    public void CapturarVentana(double[] buffer, int tamaño, int index)
    {
        this.Segmento = new double[tamaño];
        DeterminarSegmento(index , tamaño);
        for (int i = inicioSegmento; i < finSegmento; i++)
        {
            Segmento[i-inicioSegmento] = buffer[i];
        }
    }

    //Método para determinar el segmento a tomar del buffer
    private void DeterminarSegmento(int indexVentana, int tamaño)
    {
        finSegmento = indexVentana * tamaño;
        inicioSegmento = finSegmento - tamaño;
        
    }

    //Getters y Setters
    public double[] Segmento { get; private set; }

}
