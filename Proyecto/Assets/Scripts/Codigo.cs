using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
public class Codigo : MonoBehaviour {

    public Termostato t;
    private int i;
    public float tiempo;
	public TermostatoInterior ti;
    public int minuto;
    public int segundo;
    public int hora;
    public bool tiemponormal;
    public Text texto;

    public Humedad h;
    void Start () {
        tiempo = 0.01f;
        segundo = 0;
        texto = GetComponent<Text>();
        tiemponormal = true;
    }

    void Update () {

        Time.timeScale = tiempo;

        //Gestor de velocidad de tiempo
        /*
        if (Input.GetKey(KeyCode.A)) {
            tiempo = 0.01f;
        }

        if (Input.GetKey(KeyCode.S))
        {
            tiempo = 1f;
        }

        if (Input.GetKey(KeyCode.D))
        {
            tiempo = 10f;
        }
        */
    }

    void FixedUpdate() {

        if (tiemponormal==true){
            texto.text = tiempoNormal();
        }
    }

    public String tiempoNormal(){
         //Segundero

        if (segundo == 59)
        {
            segundo = 00;
            if (minuto == 59)
            {
                minuto = 00;
                if (hora == 23)
                {
                    hora = 00;
                    h.activarRiego();
                }
                else
                {
                    hora++;
                    h.desactivarRiego();
                    t.nuevaTemperatura(hora);
                    h.nuevaHumedad();
                }
            }
            else
            {
                minuto++;
            }
        }
        else{
			if (segundo % 10 == 0) {
				ti.obtenerPotencia();
			}
            segundo++;
        }

    return hora + ":" + minuto.ToString("00")  + ":" + segundo.ToString("00");
    }

    public void tiempoPausado() {
        tiemponormal = false;
    }
    public void tiempoDespausado() {
        tiemponormal = true;
        tiempo = 0.01f;
    }
    public void tiempoRapido()
    {
        tiempo = 10f;
    }
}
