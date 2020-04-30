/*
 * javier1604@gmail.com
 * Carlos Javier Lopez Cruz
 *
 * User: Tlacaelel
 * Date: 28/ Nov /2018
 * Time: 01:58 p. m.
 * 
 */

 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.COI
{
    public class Proyecto
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }

        public override string ToString()
        {
            return string.Format("{0}-{1}", Id, Descripcion);
        }
    }
}
