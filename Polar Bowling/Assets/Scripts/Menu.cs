using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    //public ConnectionStateBITalino miBITalino;
    private string[] escenas = new string[] { "BITalinoAndLeapMotion", "Setting", "LeapMotion", "StartMenu" };
    private string escenaActual="";

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RecargarEscena();
        }
    }

    private void RecargarEscena()
    {
        escenaActual = SceneManager.GetActiveScene().name;
        if (escenaActual == escenas[0] || escenaActual == escenas[2])
            SceneManager.LoadScene(escenaActual, LoadSceneMode.Single);
    }

    public void CambiarEscena(string escena)
    {
        SceneManager.LoadScene(escena, LoadSceneMode.Single);
    }

    public void CerrarJuego()
    {
        Application.Quit();
    }
}
