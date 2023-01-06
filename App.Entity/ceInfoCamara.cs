using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.Entity
{
    public class ceInfoCamara
    {
        public string IP         { get; set; }
        public string Puerto     { get; set; }
        public string Usuario    { get; set; }
        public string Password   { get; set; }
        public int    NumCanales { get; set; }
        public IntPtr LoginID    { get; set; }
        public string Mac        { get; set; }
    }
}
