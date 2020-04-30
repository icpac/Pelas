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

namespace PlsProGui
{
    /// <summary>
    /// Para evitar que se muestre en las Vistas View
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class VisibleViewAttribute : Attribute
    {
        private bool mVisible;

        public VisibleViewAttribute(bool visible)
        { mVisible = visible; }

        public bool Visible { get { return mVisible; } }
    }
}
