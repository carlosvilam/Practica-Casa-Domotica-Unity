using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pausa : MonoBehaviour {
    public string estado;
    public Codigo c;
    public GameObject panel;
    public GameObject canvas;
    void Start() {
        estado = "despausa";
        panel.gameObject.SetActive(false);
    }

	void Update () {
        if (Input.GetKeyDown("escape")) {
            if (estado == "despausa")
            {
                pausa();  
            }
            else {
                despausa();       
            }
        }
	}
    public void pausa() {
        estado = "pausa";
        panel.gameObject.SetActive(true);
        c.tiempoPausado();
        Debug.Log("pausa!");
        canvas.gameObject.SetActive(false);
    }
    public void despausa() {
        estado = "despausa";
        panel.gameObject.SetActive(false);
        c.tiempoDespausado();
        canvas.gameObject.SetActive(true);
    }
    public void salir() {
        Application.Quit();
    }
}
