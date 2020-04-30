#region TIT
/*
 * tlacaelel.icpac@gmail.com
 * Tlacaelel Icpac
 */
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class Inve : PObject
    {
        public string Clave { get; set; }

        private string mDescr;
        public string Descripcion
        {
            get { return mDescr; }
            set
            {
                mDescr = value;
                OnChanged("Descripcion");
            }
        }
    }
}
