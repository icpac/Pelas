#region TIT
/*
{+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++)
{                                                                   }
{     Tlacaelel Icpac                                               }
{     tlacaelel.icpac@gmail.com                                     }
{                                                                   }
{*******************************************************************}
 */
#endregion

 /* Para leer una tabla relacional
  */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class Campo
    {
        public string Nombre;
        public string Tipo;
        public int Tamano;
        public bool Indice;
        public bool Unico;
        public bool Requerido;
        public double? Value;
    }
}
