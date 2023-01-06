using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;
using App.Data;
using App.Entity;

namespace App.Deal
{
    public class CdeAlerta
    {
        private static CdAlerta alerta = new CdAlerta();

        public static void Actualizar()
        {
            alerta.InsertDb();
        }

        public static void Insertar(int id, string nombre, string ip, string placa, byte[] imgplaca, byte[] imgauto, float latitud, float longitud, float velocidad, string arco)
        {
            if (ValidarPlaca(placa))
            {
                alerta.Insertar(id, nombre, ip, placa, imgplaca, imgauto, latitud, longitud, velocidad, arco);
            }
        }

        public static bool ValidarPlaca(string placa)
        {
            bool respuesta = false;
                                   
            if(placa.Length > 5  && placa.Length < 11)
            {
                if (RevisarNumero(placa) && RevisarLetra(placa))
                {
                    respuesta = true;
                }
            }
            else
            {
                respuesta = false;
            }

            return respuesta;
        }

        public  static bool RevisarNumero(string placa)
        {
            int cNumero = 0;
            bool respuesta = false;
            
            Regex reg = new Regex("[A-Z]");

            foreach (char c in placa)
            {
                bool okay = reg.IsMatch(c.ToString());

                if (okay)
                {
                    cNumero++;
                }
            }

            if (cNumero > 1)
            {
                respuesta = true;
            }

            return respuesta;
        }

        public static bool RevisarLetra(string placa)
        {
            int cLetra = 0;
            bool respuesta = false;

            Regex reg = new Regex("[0-9]");

            foreach (char c in placa)
            {
                bool okay = reg.IsMatch(c.ToString());

                if(okay)
                {
                    cLetra++;
                }
            }

            if(cLetra > 1)
            {
                respuesta = true;
            }

            return respuesta;
        }

        public static List<ceCamara> GetCamara()
        {
            List<ceCamara> lista = new List<ceCamara>();
            
            lista = alerta.GetCamLocal();

            /*if (lista.Count <= 0)
            {          
                lista = alerta.GetCamNube();
            }*/

            return lista;
        }

        public static string GetMacAddress(string ipAddress)
        {
            string macAddress = string.Empty;
            Process pProcess = new Process();
            pProcess.StartInfo.FileName = "arp";
            pProcess.StartInfo.Arguments = "-a " + ipAddress;
            pProcess.StartInfo.UseShellExecute = false;
            pProcess.StartInfo.RedirectStandardOutput = true;
            pProcess.StartInfo.CreateNoWindow = true;
            pProcess.Start();
            string strOutput = pProcess.StandardOutput.ReadToEnd();
            string pattern = @"(?<ip>([0-9]{1,3}\.?){4})\s*(?<mac>([a-f0-9]{2}-?){6})";

            foreach (Match m in Regex.Matches(strOutput, pattern, RegexOptions.IgnoreCase))
            {
                macAddress = m.Groups["mac"].Value;
            }

            return macAddress.ToUpper();
        }
        //4C:11:BF:AA:CA:D2

        public static void InsertarConfigs(string marca, string ip, string puerto, string usuario, string clave)
        {
            alerta.InsertarConfiguracion(marca, ip, puerto, usuario, clave);
        }

        public static List<ceConfig> GetConfigs()
        {
            return alerta.GetConfig();
        }

        public static void InsertarISS()
        {

        }

    }
}
