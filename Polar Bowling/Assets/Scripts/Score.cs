using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public xmlEdit miEditor;
    public ControladorPelota miPelota;

    private GameObject[] pines;
    private GameObject numeroLanzamiento, scoreLanzamiento, totalPartida, panelFinJuego, scoreFinal;
    private bool[] derrivados;
    public int score, totalScore, lanzamiento;

    void Start()
    {
        pines = GameObject.FindGameObjectsWithTag("Pin");
        numeroLanzamiento = GameObject.FindGameObjectWithTag("Lanzamiento");
        scoreLanzamiento = GameObject.FindGameObjectWithTag("Score");
        totalPartida = GameObject.FindGameObjectWithTag("Total");
        panelFinJuego = GameObject.FindGameObjectWithTag("FinJuego");
        scoreFinal = GameObject.FindGameObjectWithTag("ScoreFinal");
        panelFinJuego.SetActive(false);

        derrivados = new bool[10] { false, false, false, false, false, false, false, false, false, false };
        score = 0;
        totalScore = 0;
        lanzamiento = 0;
        //MostrarDatosPartida();
    }

    void Update()
    {
        if (!miPelota.BolaActiva())
        {
            CalcularScore();
        }
        NumeroLanzamientos();
        CalcularTotalPartida();
        MostrarDatosPartida();
        if (lanzamiento > 5)
        {
            MostrarPanelFinal();
        }
        if (Input.GetKey(KeyCode.Space) && lanzamiento<=5)
        {
            GuardarLanzamiento();
        }    
    }

    void MostrarDatosPartida()
    {
        numeroLanzamiento.GetComponent<Text>().text = lanzamiento.ToString();
        scoreLanzamiento.GetComponent<Text>().text = score.ToString();
        totalPartida.GetComponent<Text>().text = totalScore.ToString();
    }

    private void NumeroLanzamientos()
    {
        lanzamiento = (int)miEditor.ObtenerDato("lanzamientos", "id", "realizados")+1;
    }

    private void CalcularScore()
    {
        for (int i = 0; i < pines.Length; i++)
        {
            if (pines[i].GetComponent<Transform>().position.y < 1.4 && derrivados[i] == false)
            {
                score += 10;
                derrivados[i] = true;
            }
        }
    }

    private void CalcularTotalPartida()
    {
        totalScore = 0;
        for (int i = 1; i <= 5; i++)
        {
            totalScore += (int)miEditor.ObtenerDato("score", "id", i.ToString());
        }
    }
    private void GuardarLanzamiento()
    {
        miEditor.GuardarDato("lanzamientos", "id", "realizados", lanzamiento);
        miEditor.GuardarDato("total", "id", "total", totalScore);
        miEditor.GuardarDato("score", "id", lanzamiento.ToString(), score);
    }

    private void LimpiarLog()
    {
        miEditor.GuardarDato("lanzamientos", "id", "realizados", 0);
        miEditor.GuardarDato("total", "id", "total", 0);
        for (int i = 1; i <=5; i++)
        {
            miEditor.GuardarDato("score", "id", i.ToString(), 0);
        }
    }

    private void MostrarPanelFinal()
    {
        panelFinJuego.SetActive(true);
        scoreFinal.GetComponent<Text>().text = totalScore.ToString();
        LimpiarLog();
    }
}
