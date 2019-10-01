using System;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class Feature : MonoBehaviour
{
    private double[] ventana;
    private double mean;

    //Método que calcula la feature MAV
    public double CalcularMAV(double[] ventana)
    {
        for (int i = 0; i < ventana.Length; i++)
        {
            ventana[i] = Math.Abs(ventana[i]);
        }
        mean = ventana.Average();
        return mean;
    }

    public double Mean { get; private set; }
}
