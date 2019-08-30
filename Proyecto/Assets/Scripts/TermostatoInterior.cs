using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
//using AForge.Fuzzy;
using Accord.Fuzzy;
public class TermostatoInterior : MonoBehaviour {
    System.Random r = new System.Random();
    int numero;
    int temperaturaI;
    public Text texto;
	public Termostato t;
	public Text aire;
	public Text cale;
    private bool activada = true;
	public void nuevaTemperatura(int temperatura) {
        numero = r.Next(1, 100);
        if (numero > 25) {
            temperaturaI = temperatura + 2;
            if (numero > 50) {
                temperaturaI = temperatura + 3;
                if (numero > 75) {
                    temperaturaI = temperatura + 4;
                }
            }
        }
        else{
        temperaturaI = temperatura +1;
        }
        texto.text = temperaturaI + "º";
    }

	public void obtenerPotencia()
	{
        if (activada)
        {
            int pcal = (int)calefaccion(t.temperatura, temperaturaI);
            int pair = (int)aireAcondicionado(t.temperatura, temperaturaI);

            int potenciaA;
            int potenciaC;

            //calefaccion

            if (pcal < 20)
            {
                potenciaC = 0;
                //temperaturaI += 0;
            }
            else
            {
                if (pcal < 50)
                {
                    potenciaC = 1;
                    temperaturaI += 1;
                }
                else
                {
                    if (pcal < 75)
                    {

                        potenciaC = 2;
                        temperaturaI += 2;
                    }
                    else
                    {

                        potenciaC = 3;
                        temperaturaI += 3;
                    }
                }
            }

            //Aire acondicionado


            if (pair < 20)
            {
                potenciaA = 0;
                //temperaturaI += 0;
            }
            else
            {
                if (pair < 50)
                {
                    potenciaA = 1;
                    temperaturaI -= 1;
                }
                else
                {
                    if (pair < 75)
                    {

                        potenciaA = 2;
                        temperaturaI -= 2;
                    }
                    else
                    {

                        potenciaA = 3;
                        temperaturaI -= 3;
                    }
                }
            }
            if (potenciaC == 0)
            {
                cale.text = "Calefaccion desactivada";
                cale.color = new Color(0.866f, 0.854f, 0.854f);
            }
            else
            {
                if (potenciaC == 1)
                {
                    cale.text = "Calefacción Nivel: 1";
                    cale.color = new Color(0.972f, 0.905f, 0.125f);
                }
                else
                {
                    if (potenciaC == 2)
                    {
                        cale.text = "Calefacción Nivel: 2";
                        cale.color = new Color(0.976f, 0.619f, 0.043f);
                    }
                    else
                    {
                        cale.text = "Calefacción Nivel: 3";
                        cale.color = new Color(0.976f, 0.043f, 0.043f);
                    }
                }
            }

            if (potenciaA == 0)
            {
                aire.text = "Aire desactivado";
                aire.color = new Color(0.866f, 0.854f, 0.854f);
            }
            else
            {
                if (potenciaA == 1)
                {
                    aire.text = "Aire Nivel: 1";
                    aire.color = new Color(0.125f, 0.941f, 0.972f);
                }
                else
                {
                    if (potenciaA == 2)
                    {
                        aire.text = "Aire Nivel: 2";
                        aire.color = new Color(0.125f, 0.533f, 0.972f);
                    }
                    else
                    {
                        aire.text = "Aire Nivel: 3";
                        aire.color = new Color(0.152f, 0.125f, 0.972f);
                    }
                }
            }

            texto.text = temperaturaI + "º";
        }
	}

	public double calefaccion(int tempExt, int tempInt)
	{
		double potencia;


		//temperatura interior
		FuzzySet fsTIBaja = new FuzzySet("TIBaja", new TrapezoidalFunction(-15, 20, TrapezoidalFunction.EdgeType.Right));
		FuzzySet fsTIMedia = new FuzzySet("TIMedia", new TrapezoidalFunction(19, 20, 22, 25));
		FuzzySet fsTIAlta = new FuzzySet("TIAlta", new TrapezoidalFunction(22, 25, TrapezoidalFunction.EdgeType.Left));
        

		LinguisticVariable lvTI = new LinguisticVariable("TI", -15, 40);
		lvTI.AddLabel(fsTIBaja);
		lvTI.AddLabel(fsTIMedia);
		lvTI.AddLabel(fsTIAlta);



		//temperatura exterior 
		FuzzySet fsTEBaja = new FuzzySet("TEBaja", new TrapezoidalFunction(-15, 20, TrapezoidalFunction.EdgeType.Right));
		FuzzySet fsTEMedia = new FuzzySet("TEMedia", new TrapezoidalFunction(19, 20, 22, 25));
		FuzzySet fsTEAlta = new FuzzySet("TEAlta", new TrapezoidalFunction(22, 25, TrapezoidalFunction.EdgeType.Left));

		LinguisticVariable lvTE = new LinguisticVariable("TE", -15, 40);
		lvTE.AddLabel(fsTEBaja);
		lvTE.AddLabel(fsTEMedia);
		lvTE.AddLabel(fsTEAlta);


		//potencia calefaccion
		FuzzySet fsCBaja = new FuzzySet("CBaja", new TrapezoidalFunction(0, 40, TrapezoidalFunction.EdgeType.Right));
		FuzzySet fsCMedia = new FuzzySet("CMedia", new TrapezoidalFunction(35, 45, 55, 65));
		FuzzySet fsCAlta = new FuzzySet("CAlta", new TrapezoidalFunction(55, 65, TrapezoidalFunction.EdgeType.Left));

		LinguisticVariable lvCalefaccion = new LinguisticVariable("Calefaccion", 0, 100);
		lvCalefaccion.AddLabel(fsCBaja);
		lvCalefaccion.AddLabel(fsCMedia);
		lvCalefaccion.AddLabel(fsCAlta);


		// Creamos la base de datos 
		Database fuzzyDB = new Database();
		fuzzyDB.AddVariable(lvTI);
		fuzzyDB.AddVariable(lvTE);
		fuzzyDB.AddVariable(lvCalefaccion);

		// Creamos el sistema de inferencia
		InferenceSystem IS = new InferenceSystem(fuzzyDB, new CentroidDefuzzifier(1000));

		//Reglas: 

		//Regla 1: Si la temperatura interior es baja entonces la calefaccion es alta
		IS.NewRule("Rule 1", "IF TI IS TIBaja THEN Calefaccion IS CAlta");
		//Regla 2: Si la temperatura interior es media y la temperatura expterior es  baja entonces calefaccion es media 
		IS.NewRule("Rule 2", "IF TI IS TIMedia AND TE IS TEBaja THEN Calefaccion IS CMedia");
		//Regla 3: Si la temperatura interior es media y la temperatura exterior es media o alta entonces calefaccion es baja
		IS.NewRule("Rule 3", "IF TI IS TIMedia AND (TE IS TEMedia OR TE IS TEAlta) THEN Calefaccion IS CBaja");
		//Regla 4: Si la temperatura interior es alta entonces la calefaccion es baja 
		IS.NewRule("Rule 4", "IF TI IS TIAlta THEN Calefaccion IS CBaja");

		IS.SetInput("TI", tempInt);
		IS.SetInput("TE", tempExt);


		potencia = IS.Evaluate("Calefaccion");

		return potencia;
	}

	public double aireAcondicionado(int tempExt, int tempInt)
	{
		double potencia;


		//temperatura interior
		FuzzySet fsTIBaja = new FuzzySet("TIBaja", new TrapezoidalFunction(-15, 20, TrapezoidalFunction.EdgeType.Right));
		FuzzySet fsTIMedia = new FuzzySet("TIMedia", new TrapezoidalFunction(19, 20, 22, 25));
		FuzzySet fsTIAlta = new FuzzySet("TIAlta", new TrapezoidalFunction(22, 25, TrapezoidalFunction.EdgeType.Left));


		LinguisticVariable lvTI = new LinguisticVariable("TI", -15, 40);
		lvTI.AddLabel(fsTIBaja);
		lvTI.AddLabel(fsTIMedia);
		lvTI.AddLabel(fsTIAlta);



		//temperatura exterior 
		FuzzySet fsTEBaja = new FuzzySet("TEBaja", new TrapezoidalFunction(-15, 20, TrapezoidalFunction.EdgeType.Right));
		FuzzySet fsTEMedia = new FuzzySet("TEMedia", new TrapezoidalFunction(19, 20, 22, 25));
		FuzzySet fsTEAlta = new FuzzySet("TEAlta", new TrapezoidalFunction(22, 25, TrapezoidalFunction.EdgeType.Left));

		LinguisticVariable lvTE = new LinguisticVariable("TE", -15, 40);
		lvTE.AddLabel(fsTEBaja);
		lvTE.AddLabel(fsTEMedia);
		lvTE.AddLabel(fsTEAlta);


		//potencia aire acondicionado
		FuzzySet fsAireBaja = new FuzzySet("AireBaja", new TrapezoidalFunction(0, 40, TrapezoidalFunction.EdgeType.Right));
		FuzzySet fsAireMedia = new FuzzySet("AireMedia", new TrapezoidalFunction(35, 45, 55, 65));
		FuzzySet fsAireAlta = new FuzzySet("AireAlta", new TrapezoidalFunction(55, 65, TrapezoidalFunction.EdgeType.Left));

		LinguisticVariable lvAire = new LinguisticVariable("Aire", 0, 100);
		lvAire.AddLabel(fsAireBaja);
		lvAire.AddLabel(fsAireMedia);
		lvAire.AddLabel(fsAireAlta);


		// Creamos la base de datos 
		Database fuzzyDB = new Database();
		fuzzyDB.AddVariable(lvTI);
		fuzzyDB.AddVariable(lvTE);
		fuzzyDB.AddVariable(lvAire);

		// Creamos el sistema de inferencia
		InferenceSystem IS = new InferenceSystem(fuzzyDB, new CentroidDefuzzifier(1000));

		//Reglas: 

		//Regla 1 y 2: Si la temperatura interior es baja entonces activar aire acondicionado bajo
		IS.NewRule("Rule 1", "IF TI IS TIBaja THEN Aire IS AireBaja");
		//Regla 2: Si la temperatura interior media y la exterior es baja entonces activar aire acondicionado bajo
		IS.NewRule("Rule 2", "IF TI IS TIMedia AND TE IS TEBaja THEN Aire IS AireBaja");
		//Regla 3: Si la temperatura interior es media y la exterior es media o alta entonces activar aire acondicinado a medio 
		IS.NewRule("Rule 3", "IF TI IS TIMedia AND (TE IS TEMedia OR TE IS TEAlta) THEN Aire IS AireMedia");
		//Regla 5: Si la temperatura es alta entonces activar aire acondicionado alto 
		IS.NewRule("Rule 4", "IF TI IS TIAlta THEN Aire IS AireAlta");

		IS.SetInput("TI", tempInt);
		IS.SetInput("TE", tempExt);


		potencia = IS.Evaluate("Aire");

		return potencia;
	}

    public void desactivarControl() {
        if (activada == true)
        {
            activada = false;
            aire.text = "Aire desactivado";
            aire.color = new Color(0.866f, 0.854f, 0.854f);
            cale.text = "Calefaccion desactivada";
            cale.color = new Color(0.866f, 0.854f, 0.854f);
        }
        else {
            activada = true;
            obtenerPotencia();
        }
    }
}
