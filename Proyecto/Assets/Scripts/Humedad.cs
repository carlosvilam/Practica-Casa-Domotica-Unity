using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;



public class Humedad : MonoBehaviour
{
    public Text riego;
    int humedad;
    System.Random r = new System.Random();
    int numero1, numero2;
    public Text texto;
    List<int> humedades = new List<int>();
    public void Start()
    {
        humedad = 50;
        texto = GetComponent<Text>();
        texto.text = humedad + "%";
        riego.color = new Color(0.866f, 0.854f, 0.854f); 
    }

    public void nuevaHumedad()
    {

        numero1 = r.Next(1, 100);
        numero2 = r.Next(1, 25);
        Debug.Log("Humedad: " + numero1 + "" + numero2);
        //mañana

        if (numero1<50){
            //suma numero 2
            if (humedad + numero2 >= 100)
            {
                humedad = 100;
            }
            else
            {
                humedad += numero2;
            }
        }
        else {
            //resta numero 2
            if (humedad - numero2 > 0)
            {
                if (humedad - numero2 <= 0)
                {
                    humedad = 0;
                }
                else
                {
                    humedad -= numero2;
                }
            }
        }
        Debug.Log(humedad);
        humedades.Add(humedad);
        texto.text = humedad + "%";

    }

    public void subirHumedad() {
        humedad++;
        Debug.Log("Humedad++");
        texto.text = humedad + "%";
    }

    public void bajarHumedad()
    {
        humedad--;
        texto.text = humedad + "%";
    }

    public void cambiarHum(float nuevaHum)
    {
        humedad = (int)(nuevaHum);
        texto.text = humedad + "%";
    }
    public void activarRiego()
    {
        int humedadGeneral = 0;
        foreach (int i in humedades)
        {
            humedadGeneral += (int)i;
        }
        humedades.Clear();
        humedadGeneral = humedadGeneral / 24;
        if (humedadGeneral < 50)
        {
            humedad = 99;
            texto.text = humedad + "%";
            riego.text = "Riego: Activado";
            riego.color = new Color(0.152f, 0.125f, 0.972f);
        }
    }
        public void desactivarRiego()
        {
            riego.text = "Riego: Desactivado";
            riego.color = new Color(0.866f, 0.854f, 0.854f);
        }
}
