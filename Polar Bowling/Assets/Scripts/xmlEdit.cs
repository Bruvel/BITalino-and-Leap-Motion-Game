using System;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Linq;
using System.Linq;
using System.IO;

public class xmlEdit : MonoBehaviour
{
    public string nombreArchivo;

    private string dataPath;
    //private XmlDocument xml = new XmlDocument();
    private XElement root;
    private IEnumerable<XElement> elementos;

    private void Start()
    {
        dataPath = "Assets/Resources/Files/"+nombreArchivo +".xml";
        CargarXML();
    }
    
    //Método que carga el archivo xml
    private void CargarXML()
    {
        try
        {
            root = XElement.Load(dataPath);
        }
        catch (FileNotFoundException e)
        {
            Debug.LogException(e);
        }
        catch   (IOException e)
        {
            Debug.LogException(e);
        }
        catch (UnauthorizedAccessException e)
        {
            Debug.LogException(e);
        }
    }

    //Método que recupera todos los elementos del elemento padre pasado como parametro 
    private void RecuperarElementos()
    {
        elementos = root.Descendants();
    }

    public float ObtenerDato(string elemento, string atributo, string valorAtributo)
    {
        CargarXML();
        float dato= float.Parse(root.Descendants(elemento).SingleOrDefault(e => (string)e.Attribute(atributo) == valorAtributo).Value);
        return dato;
    }

    public void GuardarDato(string elemento, string atributo, string valorAtributo, float nuevoDato)
    {   
        root.Descendants(elemento).SingleOrDefault(e => (string)e.Attribute(atributo) == valorAtributo).SetValue(nuevoDato);
        GuardarXML();
    }
    //Método que guarda el archivo xml
    private void GuardarXML()
    {
        root.Save(dataPath);
    }
}