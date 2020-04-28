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
/* Se cambie el curso a Espera y cuando se termina regresa al cursor 
 * anterior
 */

using System;
using System.Windows.Forms;

namespace apl.Log
{
    /// <summary>
    /// 
    /// </summary>
    public class CWaitCursor : CCursor
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public CWaitCursor() : base(Cursors.WaitCursor) { }
    }
}
