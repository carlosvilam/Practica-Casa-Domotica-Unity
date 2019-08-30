using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;



public class Termostato : MonoBehaviour {

    public int temperatura;
    System.Random r = new System.Random();
    int numero;
    public Text texto;
    public TermostatoInterior t;

    public void Start() { 
        temperatura = 10;
        texto = GetComponent<Text>();
        texto.text = temperatura + "º";
        t.nuevaTemperatura(temperatura);
    }

    public void nuevaTemperatura(int hora) {

        numero = r.Next(1, 100);
        Debug.Log(numero + " " + hora);
        //mañana

        if (hora>=7&&hora<12) {
            if (numero > 50)
            {
                if (numero > 75)
                {
                    if (temperatura + 2 >= 40)
                    {
                        temperatura = 40;
                    }
                    else {
                        temperatura += 2;
                    }
                }
                //else temperatura+0
            }
            else {
                if (temperatura + 1 >= 40)
                {
                    temperatura = 40;
                }
                else
                {
                    temperatura += 1;
                }
            }
        }
        
        //mediodia

        if (hora >= 12 && hora <16){
            if (numero > 25)
            {
                if (numero > 50)
                {
                    if (numero > 75)
                    {
                        if (temperatura + 2 >= 40)
                        {
                            temperatura = 40;
                        }
                        else
                        {
                            temperatura += 2;
                        }
                    }
                    //else temperatura+=0
                }
                else
                {
                    if (temperatura + 3 >= 40)
                    {
                        temperatura = 40;
                    }
                    else
                    {
                        temperatura += 3;
                    }
                }
            }
            else
            {
                if (temperatura + 1 >= 40)
                {
                    temperatura = 40;
                }
                else
                {
                    temperatura += 1;
                }
            }
        }

        //tarde

        if (hora >= 16 && hora < 21)
        {
            if (numero > 25)
            {
                if (numero > 50)
                {
                    if (numero > 75)
                    {
                        if (temperatura - 2 <= -15)
                        {
                            temperatura = -15;
                        }
                        else
                        {
                            temperatura -= 2;
                        }
                    }
                    //else temperatura+=0
                }
                else
                {
                    if (temperatura - 1 <= -15)
                    {
                        temperatura = -15;
                    }
                    else
                    {
                        temperatura -= 1;
                    }
                }
            }
            else
            {
                    if (temperatura + 1 >= 40)
                    {
                        temperatura = 40;
                    }
                    else
                    {
                        temperatura += 1;
                    }
            }
        }

        //noche

        if (hora >=21 || (hora>=0&&hora<7))
        {
            if (numero > 50)
            {
                if (numero > 75)
                {
                    if (numero > 87)
                    {
                            if (temperatura - 1 <= -15)
                            {
                                temperatura = -15;
                            }
                            else
                            {
                                temperatura -= 1;
                            }
                    }
                    //else temperatura+=0
                }
                else
                {
                        if (temperatura - 2 <= -15)
                        {
                            temperatura = -15;
                        }
                        else
                        {
                            temperatura -= 2;
                        }
                }
            }
            else
            {
                    if (temperatura - 3 <= -15)
                    {
                        temperatura = -15;
                    }
                    else
                    {
                        temperatura -= 3;
                    }
            }
        }
        Debug.Log(temperatura);
        texto.text = temperatura + "º";
        t.nuevaTemperatura(temperatura);
    }

    public void subirTemperatura()
    {
        temperatura++;
        texto.text = temperatura + "º";
    }

    public void bajarTemperatura()
    {
        temperatura--;
        texto.text = temperatura + "º";
    }

    public void cambiarTemp(float nuevaTemp) {
        temperatura = (int)(nuevaTemp);
        texto.text = temperatura + "º";
        t.nuevaTemperatura(temperatura);
    }
  
}
