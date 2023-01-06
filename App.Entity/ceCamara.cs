using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.Entity
{
    public class ceCamara
    {
        public int In_Id            { get; set; }
        public string In_Mac        { get; set; }
        public string In_Nombre     { get; set; }
        public float In_Longitud    { get; set; }
        public float In_Latitud     { get; set; }
        public string In_Arco       { get; set; }
        public string In_Carril     { get; set; }
        public string In_Sentido    { get; set; }
    }

    public class ceConfig
    {
        public string Config_Marca   { get; set; }
        public string Config_IP      { get; set; }
        public string Config_Puerto  { get; set; }
        public string Config_Usuario { get; set; }
        public string Config_Clave   { get; set; }
    }
}
