using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Entity
{
    public class ceAntena
    {
        public DateTime Fecha  { get; set; }
        public string  Numero  { get; set; }

        private string _folio;

        public string Folio
        {
            get
            {
                return _folio;
            }
            set
            {
                if(_folio != null && _folio != "")
                {
                    for (int i = 0; i < _folio.Length; i += 2)
                    {
                        String hs = string.Empty;

                        hs = _folio.Substring(i, 2);
                        uint decval    = Convert.ToUInt32(hs, 16);
                        char character = Convert.ToChar(decval);
                        _folio += character;

                    }

                    _folio = value;
                }
            }
        }
    }
}
