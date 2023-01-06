using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using App.Deal;
using Microsoft.JScript;

namespace IntelligentTraffic.Samsung
{
    public class SamsungAnalisis
    {
        public void createComponents()
        {
            
        }

        public static bool ConSamsung(string ip, string port, string userName, string password)
        {
            bool respuesta = false;



            return respuesta;
        }

        public static bool ConISS()
        {
            bool respuesta = false;


            return respuesta;
        }

        public static void GetISS()
        {            
            if(ConISS())
            {
                //Datos
            }
            else
            {
                Console.Write("Error ConISS (Analisis).");
            }
        }

        public static string ConsultarPlate()
        {
            string placa = null;

            placa = "LPR";

            return placa;
        }

        public void InsertISS()
        {
            CdeAlerta.InsertarISS();
        }

    }
}
