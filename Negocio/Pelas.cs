#region TIT
/*
 * tlacaelel.icpac@gmail.com
 * Tlacaelel Icpac
 */
#endregion

using apl.Log;
using Datos;
using FirebirdSql.Data.FirebirdClient;
using Negocio.ICO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using Negocio.AES;

namespace Negocio
{
    public static class Pelas
    {
        public static List<AntiguosSaldos> VendedoresSaldos(string ruta, int nmEmpr)
        {
            if (string.IsNullOrEmpty(ruta))
            {
                throw new Exception("Falta la ruta de los datos !");
            }

            NmrEmprs = nmEmpr;

            FireBird datos = new FireBird();
            (datos.CadenaConn as FbConnectionStringBuilder).Database = ruta;
            //short concepto = 1;

            string sql;

            sql =
                $" select max(m.importe) - sum(d.importe) as total," +
                $" m.cve_clie, m.refer, d.refer," +
                $" fc.cve_vend," +
                $" m.importe * m.signo as impfac," +
                $" m.fecha_apli, m.fecha_venc, d.fecha_apli as dtfecha_apli" +

                $" from {CuentasNombre} as m" +
                $" left join  {FacturaNombre} as fc on fc.cve_doc = m.refer" +
                $"       and fc.CVE_CLPV = m.CVE_CLIE" +

                $" inner join {DetalleCuentasNombre} as d" +
                $"    on m.refer = d.refer" +

                $" where m.num_cpto <> 9 and " +
                $" d.num_cpto in (5, 10, 11, 17, 22, 23, 46, 47, 48)" +
                $" group by m.refer, d.refer, m.cve_clie," +
                $" impfac, m.fecha_apli, m.fecha_venc," +
                $" dtfecha_apli, fc.cve_vend";

            FbDataReader reader = datos.LeeDatosOnly(sql) as FbDataReader;
            List<AntiguosSaldos> Lista = new List<AntiguosSaldos>();

            using (new CWaitCursor())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        if (reader.GetDouble(0) != 0)
                        {
                            Lista.Add(new AntiguosSaldos
                            {
                                Cliente = reader.GetString(1),
                                Referencia = reader.GetString(2),
                                ImporteFactura = reader.GetDouble(5),
                                FechaAplicacion = reader.GetDateTime(6),
                                FechaVencimiento = reader.GetDateTime(7),
                                Vendedor = reader.GetString(4),
                                /*Pagos = reader.GetDouble(7),*/
                                Saldo = reader.GetDouble(0),
                            });
                        }
                    }
                }
                reader.Close();
            }

            sql = $" select m.cve_clie, m.refer," +
                $" m.importe* m.signo as impfac," +
                $" m.fecha_apli, m.fecha_venc," +
                $" fc.cve_vend" +
                $" from {CuentasNombre} as m" +
                $" left join {DetalleCuentasNombre} as d" +
                $" on d.refer = m.refer" +
                $" LEFT JOIN  {FacturaNombre} as fc on fc.cve_doc = m.refer" +
                $" and fc.CVE_CLPV = m.CVE_CLIE" +
                $" where d.refer is null";

            FbDataReader readerv = datos.LeeDatosOnly(sql) as FbDataReader;
            using (new CWaitCursor())
            {
                if (readerv.HasRows)
                {
                    while (readerv.Read())
                    {
                        if (readerv.GetDouble(2) != 0)
                        {
                            Lista.Add(new AntiguosSaldos
                            {
                                Cliente = readerv.GetString(0),
                                Referencia = readerv.GetString(1),
                                ImporteFactura = readerv.GetDouble(2),
                                FechaAplicacion = readerv.GetDateTime(3),
                                FechaVencimiento = readerv.GetDateTime(4),
                                Vendedor = readerv.GetString(5),
                                Saldo = readerv.GetDouble(2),
                            }) ;
                        }
                    }
                }
                readerv.Close();
            }
            Lista.Sort(new AntiguosSaldosComparer());
            return Lista;
        }

        public static List<Cliente>Clientes(string ruta, int nmEmp)
        {
            if (string.IsNullOrEmpty(ruta))
            {
                throw new Exception("Falta la ruta de los datos !");
            }
            else 
            {
                return null;
            }
        }

        public static List<Inve> Productos(string ruta, int nmEm)
        {
            if (string.IsNullOrEmpty(ruta))
            {
                throw new Exception("Falta la ruta de datos");
            }
            else
            {
                NmrEmprs = nmEm;

                FireBird datos = new FireBird();
                (datos.CadenaConn as FbConnectionStringBuilder).Database = ruta;

                string sql = string.Format("select * From {0} order by cve_art asc", ProductoNombre);
                FbDataReader reader = datos.LeeDatosOnly(sql) as FbDataReader;
                List<Inve> ListaInve = new List<Inve>();

                using (new CWaitCursor())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            ListaInve.Add(new Inve
                            {
                                Clave = reader.GetString(0),
                                Descripcion = reader.GetString(1)
                            });
                        }
                    }
                    reader.Close();
                }
                return ListaInve;

                #region
                /*
                FireBird datos = new FireBird();

                (datos.CadenaConn as FbConnectionStringBuilder).Database = ruta; // stgs.RutaSAE;

                string sql = "select * From inve01 order by cve_art asc";

                DataSet datset = datos.LeeDatos(sql);

                if (datset != null && datset.Tables != null && datset.Tables.Count > 0)
                {

                    foreach (DataRow rw in datset.Tables[0].Rows)
                    {
                        Lista.Add(new Inve
                        {
                            Cve_art = rw.Field<string>("CVE_ART"),
                            Descr = rw.Field<string>("DESCR")
                        });
                    }
                }
                return Lista;*/
                #endregion
            }
        }

        public static List<Ventas> Ventas(string ruta, int nmEm)
        {
            if (string.IsNullOrEmpty(ruta))
            {
                throw new Exception("Falta la ruta de datos");
            }
            else
            {
                NmrEmprs = nmEm;

                FireBird datos = new FireBird();
                List<Ventas> ListaVnt = new List<Ventas>();

                (datos.CadenaConn as FbConnectionStringBuilder).Database = ruta;

                string sql = string.Format("select {0}.cve_clpv" +
                    ", {1}.nombre" +
                    ", {0}.cve_vend" +
                    ", {2}.calle as DatEnvio " +
                    ", {0}.cve_doc" +
                    ", {0}.fecha_doc" +
                    ", {0}.can_tot as Importe" +
                    ", {0}.imp_tot1" +
                    ", {0}.imp_tot2" +
                    ", {0}.imp_tot3" +
                    ", {0}.imp_tot4" +
                    ", ({0}.can_tot + {0}.imp_tot1 + {0}.imp_tot2 + " +
                    "{0}.imp_tot3 + {0}.imp_tot4) as Total " +
                    " From {0}" +
                    " left join {1} on {1}.clave = {0}.cve_clpv" +
                    " left join {2} on {2}.cve_info = {0}.dat_envio ",
                    FacturaNombre, ClienteNombre, EnvioNombre);

                using (new CWaitCursor())
                {
                    DataSet datset = datos.LeeDatos(sql);

                    if (datset != null && datset.Tables != null && datset.Tables.Count > 0)
                    {
                        ListaVnt = new List<Ventas>();
                        foreach (DataRow rw in datset.Tables[0].Rows)
                        {
                            ListaVnt.Add(new Ventas
                            {
                                Cliente = rw.Field<string>("cve_clpv"),
                                ClienteNombre = rw.Field<string>("nombre"),
                                Vendedor = rw.Field<string>("cve_vend"),
                                Envio = rw.Field<string>("DatEnvio"),
                                FolioDocumento = rw.Field<string>("cve_doc"),
                                FechaDocumento = rw.Field<DateTime>("fecha_doc"),
                                SubTotal = rw.Field<double>("Importe"),
                                ImpuestoTotal1 = rw.Field<double>("imp_tot1"),
                                ImpuestoTotal2 = rw.Field<double>("imp_tot2"),
                                ImpuestoTotal3 = rw.Field<double>("imp_tot3"),
                                ImpuestoTotal4 = rw.Field<double>("imp_tot4"),
                                Total = rw.Field<double>("Total")
                            });
                        }
                    }
                }

                return ListaVnt;
            }
        }

        static List<Cuenta> ListaCtas = new List<Cuenta>();
        public static List<Cuenta> CuentasCOI(string ruta, FiltroCOI fil)
        {
            FireBird datos = new FireBird();

            if (fil == null)
                Ejercicio = "18";
            else
                Ejercicio = fil.Ejercicio.ToString().Substring(2);

            ParametrosCOI prm = ParametrosCOI(ruta);
            (datos.CadenaConn as FbConnectionStringBuilder).Database = ruta;

            ListaCtas.Clear();
            string sql = string.Format("SELECT {0}.num_cta, {0}.nombre, {0}.cta_papa" +
                ", {1}.inicial, {1}.cargo01, {1}.abono01 FROM {0}" +
                " LEFT JOIN {1} ON {0}.num_cta = {1}.num_cta", CuentaNombre, SaldoNombre);

            DataSet datset = datos.LeeDatos(sql);

            if (datset != null && datset.Tables != null && datset.Tables.Count > 0)
            {
                int id = 1; // 0 sería el papá de todos
                foreach (DataRow rw in datset.Tables[0].Rows)
                {
                    Cuenta cta = new Cuenta
                    {
                        ID = id++,
                        ParentID = 0,
                        Numero = rw.Field<string>("NUM_CTA"),
                        CuentaFrmt = CuentaFormato(rw.Field<string>("NUM_CTA"), prm),
                        Nombre = rw.Field<string>("NOMBRE"),
                        PapaNumero = rw.Field<string>("CTA_PAPA").Trim()
                    };
                    double ini = 0;
                    double cg01 = 0;
                    double ab01 = 0;
                    try
                    {
                        ini = rw.Field<double>("inicial");
                    }
                    catch (Exception)
                    { }
                    try
                    {
                        cg01 = rw.Field<double>("cargo01");
                    }
                    catch (Exception)
                    { }
                    try
                    {
                        ab01 = rw.Field<double>("abono01");
                    }
                    catch (Exception)
                    { }
                    cta.SaldoFinal = ini + cg01 - ab01;

                    ListaCtas.Add(cta);
                }

                for (int i = 0; i < ListaCtas.Count; i++)
                {
                    Cuenta ct = ListaCtas[i] as Cuenta;

                    if (ct.PapaNumero != "-1")
                    {
                        ct.ParentID = ListaCtas.Find(delegate (Cuenta c) { return c.Numero == ct.PapaNumero; }).ID;
                    }
                }
            }
            return ListaCtas;
        }

        public static string rutaEmprs(string ruta, string empresa)
        {
            string rt = string.Format("Empresa{0}\\COI80EMPRE{0}.FDB", empresa);

            rt = Path.Combine(ruta, rt);
            return rt;
        }


        public static List<Auxiliar> PruebaAux()
        {
            List<Auxiliar> lista = new List<Auxiliar>();

            lista.Clear();
            Auxiliar ax = new Auxiliar
            {
                Cuenta = "6010000000",
                CuentaFrmt = "6010-000-000",
                Descripcion = "Gastos Generales",
                IdProyecto = 1,
                Grupo = 1,
                Tipo = "A"
            };
            lista.Add(ax);

            ax = new Auxiliar
            {
                Cuenta = "6010001000",
                CuentaFrmt = "6010-001-000",
                Descripcion = "Gastos Auxiliares",
                IdProyecto = 1,
                Grupo = 1,
                Tipo = "A"
            };
            lista.Add(ax);

            ax = new Auxiliar
            {
                Cuenta = "6010001001",
                CuentaFrmt = "6010-001-001",
                Descripcion = "Gastos Detalle",
                IdProyecto = 1,
                Grupo = 1,
                Tipo = "D"
            };
            AuxiliarPoliza axPoliza = new AuxiliarPoliza
            {
                Tipo = "Dr",
                Numero = "1",
                Fecha = new DateTime(2018, 10, 1),
                Concepto = "Concepto"
            };
            ax.Partidas.Add(axPoliza);
            lista.Add(ax);


            ax = new Auxiliar
            {
                Cuenta = "6010000000",
                CuentaFrmt = "6010-000-000",
                Descripcion = "Gastos Generales",
                IdProyecto = 2,
                Grupo = 2,
                Tipo = "A"
            };
            lista.Add(ax);
            ax = new Auxiliar
            {
                Cuenta = "6010001000",
                CuentaFrmt = "6010-001-000",
                Descripcion = "Gastos Auxiliares",
                IdProyecto = 2,
                Grupo = 2,
                Tipo = "A"
            };
            lista.Add(ax);

            ax = new Auxiliar
            {
                Cuenta = "6010001001",
                CuentaFrmt = "6010-001-001",
                Descripcion = "Gastos Detalle",
                IdProyecto = 2,
                Grupo = 2,
                Tipo = "D"
            };
            axPoliza = new AuxiliarPoliza
            {
                Tipo = "Dr",
                Numero = "2",
                Fecha = new DateTime(2018, 10, 1),
                Concepto = "Concepto"
            };
            ax.Partidas.Add(axPoliza);
            lista.Add(ax);

            return lista;
        }

        // En el reporte Cargos  sumSum([Partidas].[Cargos])
        // Abonos  sumSum([Partidas].[Abonos])
        public static List<Auxiliar> AuxiliarC(string ruta, FiltroCOI fil)
        {
            if (string.IsNullOrEmpty(ruta))
            {
                throw new Exception("Falta la ruta de datos");
            }
            else
            {
                ruta = rutaEmprs(ruta, fil.Emprss);
                ParametrosCOI prm = ParametrosCOI(ruta);

                if (fil == null)
                    Ejercicio = "18";
                else
                    Ejercicio = fil.Ejercicio.ToString().Substring(2);

                FireBird datos = new FireBird();
                (datos.CadenaConn as FbConnectionStringBuilder).Database = ruta;

                string sql = string.Format("SELECT {0}.num_cta, {0}.nombre, {0}.cta_papa, {0}.Tipo" +
                    ", {1}.inicial" +
                    ", {1}.cargo01, {1}.abono01 " +
                    ", {1}.cargo02, {1}.abono02 " +
                    ", {1}.cargo03, {1}.abono03 " +
                    ", {1}.cargo04, {1}.abono04 " +
                    ", {1}.cargo05, {1}.abono05 " +
                    ", {1}.cargo06, {1}.abono06 " +
                    ", {1}.cargo07, {1}.abono07 " +
                    ", {1}.cargo08, {1}.abono08 " +
                    ", {1}.cargo09, {1}.abono09 " +
                    ", {1}.cargo10, {1}.abono10 " +
                    ", {1}.cargo11, {1}.abono11 " +
                    ", {1}.cargo12, {1}.abono12 " +
                    ", {1}.cgrupos" +
                    " FROM {0}" +
                    " LEFT JOIN {1} ON {0}.num_cta = {1}.num_cta" +
                    " AND Ejercicio = {2}",
                    CuentaNombre, SaldoCcNombre, fil.Ejercicio);

                if (fil.Desde != null)
                    sql = string.Format("{0} WHERE {1}.num_cta >= '{2}' ", sql, CuentaNombre, fil.Desde.Numero);
                if (fil.Hasta != null)
                    sql = string.Format("{0} {1} {2}.num_cta <= '{3}' ", sql, sql.Contains("WHERE") ? "AND" : "WHERE",
                        CuentaNombre, fil.Hasta.Numero);
                if (fil.CentroC != null)
                    sql = string.Format("{0} {1} CCostos = {2} ", sql, sql.Contains("WHERE") ? "AND" : "WHERE",
                        fil.CentroC.Id);
                if (fil.CProyecto != null)
                    sql = string.Format("{0} {1} CGrupos = {2}", sql, sql.Contains("WHERE") ? "AND" : "WHERE",
                        fil.CProyecto.Id);

                DataSet datset = datos.LeeDatos(sql);

                if (datset != null && datset.Tables != null && datset.Tables.Count > 0)
                {
                    List<Auxiliar> lista = new List<Auxiliar>();
                    int id = 1; // 0 sería el papá de todos

                    lista.Clear();
                    List<Proyecto> lstPry = Proyectos(ruta);

                    // Llenamos las Cuentas
                    foreach (DataRow rw in datset.Tables[0].Rows)
                    {
                        bool yasta = false;
                        Auxiliar ax = null;
                        string ct = rw.Field<string>("NUM_CTA");
                        int pry = 0;

                        if (!rw.IsNull("CGrupos"))
                            pry = rw.Field<int>("CGrupos");

                        if (fil.CProyecto == null)
                        {
                            // Buscamos si ya la agregamos a la lista 
                            // cuando no se da el PRoyecto
                            ax = lista.Find
                                (delegate (Auxiliar b)
                                { return b.Cuenta == ct && b.IdProyecto == pry; });
                        }
                        /*
                        if (fil.CentroC == null && fil.CProyecto == null)
                        {
                            // Buscamos si ya la agregamos a la lista 
                            // cuando no se da CC ni PR
                            ax = lista.Find
                                (delegate (Auxiliar b)
                                { return b.Cuenta == ct; });
                        }*/

                        if (ax == null)
                        {
                            ax = new Auxiliar
                            {
                                ID = id++,
                                ParentID = 0,
                                Cuenta = ct,
                                CuentaFrmt = CuentaFormato(ct, prm),
                                Descripcion = rw.Field<string>("NOMBRE"),
                                PapaNumero = rw.Field<string>("CTA_PAPA").Trim(),
                                Tipo = rw.Field<string>("TIPO"),
                                IdProyecto = pry
                            };
                        }
                        else
                            yasta = true;


                        // Se calcula el Saldo Inicial y Final
                        int ms = fil.MesD.GetHashCode() + 1;
                        double sldini = 0;

                        if (!rw.IsNull("inicial"))
                            sldini = rw.Field<double>("inicial");
                        double sldfin = sldini;
                        string cmpc = string.Empty;
                        string cmpa = string.Empty;

                        /*
                        if (ax.Cuenta.Contains("5010002002"))
                            sldini++;*/
                        for (int i = 1; i < ms; i++)
                        {
                            cmpc = string.Format("cargo{0}", i.ToString().PadLeft(2, '0'));
                            cmpa = string.Format("abono{0}", i.ToString().PadLeft(2, '0'));

                            double crg = 0;
                            double abn = 0;
                            if (!rw.IsNull(cmpc))
                                crg = rw.Field<double>(cmpc);
                            if (!rw.IsNull(cmpa))
                                abn = rw.Field<double>(cmpa);

                            sldini += crg - abn;
                        }
                        cmpc = string.Format("cargo{0}", ms.ToString().PadLeft(2, '0'));
                        cmpa = string.Format("abono{0}", ms.ToString().PadLeft(2, '0'));

                        ax.SaldoInicial += sldini;
                        if (!rw.IsNull(cmpc))
                            ax.Cargos += rw.Field<double>(cmpc);
                        if (!rw.IsNull(cmpa))
                            ax.Abonos += rw.Field<double>(cmpa);



                        // Este for lo agregué mrz 2019
                        int mh = fil.MesH.GetHashCode() + 1;
                        for (int i = ms+1; i <= mh; i++)
                        {
                            cmpc = string.Format("cargo{0}", i.ToString().PadLeft(2, '0'));
                            cmpa = string.Format("abono{0}", i.ToString().PadLeft(2, '0'));

                            double crg = 0;
                            double abn = 0;
                            if (!rw.IsNull(cmpc))
                                crg = rw.Field<double>(cmpc);
                            if (!rw.IsNull(cmpa))
                                abn = rw.Field<double>(cmpa);

                            ax.Cargos += crg;
                            ax.Abonos += abn;
                        }


                        ax.SaldoFinal = ax.SaldoInicial + ax.Cargos - ax.Abonos;

                        if (!yasta)
                        {
                            Proyecto bsqPry = lstPry.Find(delegate (Proyecto p)
                            { return p.Id == ax.IdProyecto; });

                            ax.NombreProyecto = bsqPry != null ? bsqPry.Descripcion : string.Empty;
                            ax.NombreEmpresa = NombreEmpresa(Convert.ToInt16(fil.Emprss));
                            lista.Add(ax);
                        }
                    }


                    // Asigna el papa
                    for (int i = 0; i < lista.Count; i++)
                    {
                        Auxiliar ct = lista[i] as Auxiliar;

                        if (ct.PapaNumero != "-1")
                        {
                            Auxiliar bsq = lista.Find(delegate (Auxiliar c)
                            { return c.Cuenta == ct.PapaNumero && c.IdProyecto == ct.IdProyecto; });
                            ct.ParentID = bsq != null ? bsq.ID : ct.ParentID;
                        }
                    }


                    int gp = 1;
                    // Llenamos los auxiliares
                    foreach (Auxiliar ct in lista.Where(x => x.Tipo == "D"))
                    {
                        string dh = string.Empty;

                        string sqlax = string.Format("SELECT *" +
                            " FROM {3}" +
                            " WHERE NUM_CTA = '{0}'" +
                            " AND PERIODO >= {1} AND PERIODO <= {2}",
                            ct.Cuenta, fil.MesD.GetHashCode() + 1,
                            fil.MesH.GetHashCode() + 1, AuxiliarNombre);

                        if (fil.CentroCId > 0)
                            sqlax = string.Format("{0} AND CCOSTOS = {1}", sqlax,
                                fil.CentroCId);
                        /*
                        if (fil.CProyectoId > 0)
                            sqlax = string.Format("{0} AND CGRUPOS = {1}", sqlax,
                                fil.CProyectoId);*/
                        sqlax = string.Format("{0} AND CGRUPOS = {1}", sqlax,
                             ct.IdProyecto);
                        /*
                        // 112000100100000000003
                        if (ct.Cuenta == "112000100100000000003")
                            ct.Cargos += 1; */

                        DataSet datsetAx = datos.LeeDatos(sqlax);
                        if (datsetAx != null && datsetAx.Tables != null && datsetAx.Tables.Count > 0)
                        {
                            foreach (DataRow rw in datsetAx.Tables[0].Rows)
                            {
                                AuxiliarPoliza ax = new AuxiliarPoliza
                                {
                                    Tipo = rw.Field<string>("TIPO_POLI"),
                                    Numero = rw.Field<string>("NUM_POLIZ"),
                                    Fecha = rw.Field<DateTime>("FECHA_POL"),
                                    Concepto = rw.Field<string>("CONCEP_PO"),
                                };
                                dh = rw.Field<string>("DEBE_HABER");
                                if (dh == "H")
                                    ax.Abonos = rw.Field<double>("MONTOMOV");
                                else
                                    ax.Cargos = rw.Field<double>("MONTOMOV");
                                ct.Partidas.Add(ax);
                            }

                            if (datsetAx.Tables[0].Rows.Count > 0)
                            {
                                ConMovimientos(ct, ref lista, gp);
                                gp++;
                            }

                            datsetAx.Dispose();
                        }
                    }
                    datset.Dispose();

                    lista.RemoveAll(item => !item.ConMovs);
                    /*
                    foreach (Auxiliar ct in lista.Where(x => x.Tipo == "D"))
                    {
                        Auxiliar bsq = lista.Find(delegate (Auxiliar c)
                        { return c.Cuenta == ct.PapaNumero && c.IdProyecto == ct.IdProyecto; });

                        / *
                        if (bsq != null)
                            bsq.Child.Add(ct);* /
                        ct.Grupo = gp;
                        if (bsq != null)
                            bsq.Grupo = gp;

                        gp++;
                    }*/
                    // lista.RemoveAll(item => item.Tipo == "D");

                    /*
                        foreach (Auxiliar ct in lista.Where(x => x.PapaNumero != "-1"))
                        {
                            Auxiliar bsq = lista.Find(delegate (Auxiliar c)
                            { return c.Cuenta == ct.PapaNumero && c.IdProyecto == ct.IdProyecto; });

                            / *
                            if (bsq != null)
                                bsq.Child.Add(ct);* /
                            if (bsq !=null)
                                bsq.Grupo = ct.Grupo;
                        }*/
                    //lista.RemoveAll(item => item.PapaNumero != "-1");
                    //lista.RemoveAll(item => !item.ConMovs);
                    return lista;
                    // return PruebaAux();
                }
            }
            return null;
        }

        private static void ConMovimientos(Auxiliar ax, ref List<Auxiliar> list, int gp)
        {
            if (ax != null)
            {
                ax.ConMovs = true;
                ax.Grupo = gp;

                if (ax.ParentID > 0)
                {
                    Auxiliar ppaux = list.Find(delegate (Auxiliar c)
                    { return c.Cuenta == ax.PapaNumero && c.IdProyecto == ax.IdProyecto; });
                    ConMovimientos(ppaux, ref list, gp);
                }
            }
        }

        public static List<BalanzaComprobacion> BalanceC(string ruta, FiltroCOI fil)
        {
            if (string.IsNullOrEmpty(ruta))
            {
                throw new Exception("Falta la ruta de datos");
            }
            else
            {
                ruta = rutaEmprs(ruta, fil.Emprss);
                ParametrosCOI prm = ParametrosCOI(ruta);

                if (fil == null)
                    Ejercicio = "18";
                else
                    Ejercicio = fil.Ejercicio.ToString().Substring(2);

                FireBird datos = new FireBird();
                (datos.CadenaConn as FbConnectionStringBuilder).Database = ruta;

                string sql = string.Format("SELECT {0}.num_cta, {0}.nombre, {0}.cta_papa, " +
                    " {0}.naturaleza, {0}.nivel " +
                    ", {1}.inicial" +
                    ", {1}.cargo01, {1}.abono01 " +
                    ", {1}.cargo02, {1}.abono02 " +
                    ", {1}.cargo03, {1}.abono03 " +
                    ", {1}.cargo04, {1}.abono04 " +
                    ", {1}.cargo05, {1}.abono05 " +
                    ", {1}.cargo06, {1}.abono06 " +
                    ", {1}.cargo07, {1}.abono07 " +
                    ", {1}.cargo08, {1}.abono08 " +
                    ", {1}.cargo09, {1}.abono09 " +
                    ", {1}.cargo10, {1}.abono10 " +
                    ", {1}.cargo11, {1}.abono11 " +
                    ", {1}.cargo12, {1}.abono12 " +
                    ", {1}.cgrupos" +
                    " FROM {0}" +
                    " LEFT JOIN {1} ON {0}.num_cta = {1}.num_cta" +
                    " WHERE Ejercicio = {2}",
                        CuentaNombre, SaldoCcNombre, fil.Ejercicio);
                if (fil.CentroC != null)
                    sql = string.Format("{0} AND CCostos = {1}", sql, fil.CentroC.Id);
                if (fil.CProyecto != null)
                    sql = string.Format("{0} AND CGrupos = {1}", sql, fil.CProyecto.Id);
                if (fil.Desde != null)
                    sql = $"{sql} AND {CuentaNombre}.NUM_CTA >= '{fil.Desde.Numero}' ";
                if (fil.Hasta != null)
                    sql = $"{sql} AND {CuentaNombre}.NUM_CTA <= '{fil.Hasta.Numero}' ";



                DataSet datset = datos.LeeDatos(sql);

                if (datset != null && datset.Tables != null && datset.Tables.Count > 0)
                {
                    List<BalanzaComprobacion> lista = new List<BalanzaComprobacion>();
                    int id = 1; // 0 sería el papá de todos
                    List<Proyecto> lstPry = Proyectos(ruta);

                    foreach (DataRow rw in datset.Tables[0].Rows)
                    {
                        int pry = 0;

                        if (!rw.IsNull("CGrupos"))
                            pry = rw.Field<int>("CGrupos");

                        BalanzaComprobacion bc = new BalanzaComprobacion
                        {
                            ID = id++,
                            ParentID = 0,
                            Numero = rw.Field<string>("NUM_CTA"),
                            CuentaFrmt = CuentaFormato(rw.Field<string>("NUM_CTA"), prm),
                            Nombre = rw.Field<string>("NOMBRE"),
                            PapaNumero = rw.Field<string>("CTA_PAPA").Trim(),
                            Naturaleza = rw.Field<short>("Naturaleza"),
                            Nivel = rw.Field<short>("Nivel"),
                            IdProyecto = pry
                        };
                        double ini = 0;
                        double cg01 = 0;
                        double ab01 = 0;
                        int pos = fil.MesD.GetHashCode();

                        try
                        {
                            ini = rw.Field<double>("inicial");
                        }
                        catch (Exception)
                        { }
                        bc.SaldoInicial = ini;


                        for (int mes = 1; mes <= pos + 1; mes++)
                        {
                            try
                            {
                                string aux = string.Format("cargo{0}", mes.ToString().PadLeft(2, '0'));
                                if (!rw.IsNull(aux))
                                    cg01 = rw.Field<double>(aux);
                                bc.Debe = cg01;
                            }
                            catch (Exception)
                            { }
                            try
                            {
                                string aux = string.Format("abono{0}", mes.ToString().PadLeft(2, '0'));
                                if (!rw.IsNull(aux))
                                    ab01 = rw.Field<double>(aux);
                                bc.Haber = ab01;
                            }
                            catch (Exception)
                            { }

                            if (mes < pos + 1)
                            {
                                if (bc.Naturaleza == 0)
                                    bc.SaldoInicial = bc.SaldoInicial + cg01 - ab01;
                                else
                                    bc.SaldoInicial = bc.SaldoInicial - cg01 + ab01;
                            }
                        }

                        if (bc.Naturaleza == 0)
                        {
                            bc.SaldoFinal = bc.SaldoInicial + cg01 - ab01;
                        }
                        else
                        {
                            bc.SaldoFinal = bc.SaldoInicial - cg01 + ab01;
                            bc.SaldoInicial = -bc.SaldoInicial;
                            bc.SaldoFinal = -bc.SaldoFinal;
                        }

                        Proyecto bsqPry = lstPry.Find(delegate (Proyecto p)
                        { return p.Id == bc.IdProyecto; });

                        bc.NombreProyecto = bsqPry != null ? bsqPry.Descripcion : string.Empty;
                        bc.NombreEmpresa = NombreEmpresa(Convert.ToInt16(fil.Emprss));
                        lista.Add(bc);
                    }

                    for (int i = 0; i < lista.Count; i++)
                    {
                        BalanzaComprobacion ct = lista[i] as BalanzaComprobacion;

                        if (ct.PapaNumero != "-1")
                        {
                            ct.ParentID = lista.Find(delegate (BalanzaComprobacion c)
                            { return c.Numero == ct.PapaNumero; }).ID;
                        }
                    }
                    datset.Dispose();

                    return lista;
                }
            }

            return null;
        }

        public static void VerificaSaldos(string ruta, FiltroCOI fil)
        {
            List<SaldoI> slds = new List<SaldoI>();
            slds.Clear();

            ruta = rutaEmprs(ruta, fil.Emprss);
            ParametrosCOI prm = ParametrosCOI(ruta);

            if (fil == null)
                Ejercicio = "18";
            else
                Ejercicio = fil.Ejercicio.ToString().Substring(2);

            FireBird datos = new FireBird();
            (datos.CadenaConn as FbConnectionStringBuilder).Database = ruta;

            string sql = string.Format("SELECT num_cta" +
                    ", inicial" +
                    ", cargo01, abono01 " +
                    ", cargo02, abono02 " +
                    ", cargo03, abono03 " +
                    ", cargo04, abono04 " +
                    ", cargo05, abono05 " +
                    ", cargo06, abono06 " +
                    ", cargo07, abono07 " +
                    ", cargo08, abono08 " +
                    ", cargo09, abono09 " +
                    ", cargo10, abono10 " +
                    ", cargo11, abono11 " +
                    ", cargo12, abono12 " +
                    " FROM {0}" +
                    " WHERE Ejercicio = {1}", SaldoNombre, fil.Ejercicio);

            DataSet datset = datos.LeeDatos(sql);
            datos.ConeccClose();
            if (datset != null && datset.Tables != null && datset.Tables.Count > 0)
            {
                foreach (DataRow rw in datset.Tables[0].Rows)
                {
                    SaldoI bc = new SaldoI
                    {
                        Num_Cta = rw.Field<string>("NUM_CTA"),
                        Inicial = rw.Field<double>("inicial"),
                        Cargo01 = rw.Field<double>("cargo01"),
                        Abono01 = rw.Field<double>("abono01"),
                        Cargo02 = rw.Field<double>("cargo02"),
                        Abono02 = rw.Field<double>("abono02"),
                        Cargo03 = rw.Field<double>("cargo03"),
                        Abono03 = rw.Field<double>("abono03"),
                        Cargo04 = rw.Field<double>("cargo04"),
                        Abono04 = rw.Field<double>("abono04"),
                        Cargo05 = rw.Field<double>("cargo05"),
                        Abono05 = rw.Field<double>("abono05"),
                        Cargo06 = rw.Field<double>("cargo06"),
                        Abono06 = rw.Field<double>("abono06"),
                        Cargo07 = rw.Field<double>("cargo07"),
                        Abono07 = rw.Field<double>("abono07"),
                        Cargo08 = rw.Field<double>("cargo08"),
                        Abono08 = rw.Field<double>("abono08"),
                        Cargo09 = rw.Field<double>("cargo09"),
                        Abono09 = rw.Field<double>("abono09"),
                        Cargo10 = rw.Field<double>("cargo10"),
                        Abono10 = rw.Field<double>("abono10"),
                        Cargo11 = rw.Field<double>("cargo11"),
                        Abono11 = rw.Field<double>("abono11"),
                        Cargo12 = rw.Field<double>("cargo12"),
                        Abono12 = rw.Field<double>("abono12")
                    };
                    slds.Add(bc);
                }

                foreach (SaldoI sini in slds)
                {
                    sql = string.Format("SELECT num_cta" +
                            ", SUM(inicial) as ini" +
                            ", SUM(cargo01) as c1, SUM(abono01) as a1" +
                            ", SUM(cargo02) as c2, SUM(abono02) as a2" +
                            ", SUM(cargo03) as c3, SUM(abono03) as a3" +
                            ", SUM(cargo04) as c4, SUM(abono04) as a4" +
                            ", SUM(cargo05) as c5, SUM(abono05) as a5" +
                            ", SUM(cargo06) as c6, SUM(abono06) as a6" +
                            ", SUM(cargo07) as c7, SUM(abono07) as a7" +
                            ", SUM(cargo08) as c8, SUM(abono08) as a8" +
                            ", SUM(cargo09) as c9, SUM(abono09) as a9" +
                            ", SUM(cargo10) as c10, SUM(abono10) as a10" +
                            ", SUM(cargo11) as c11, SUM(abono11) as a11" +
                            ", SUM(cargo12) as c12, SUM(abono12) as a12" +
                            " FROM {0}" +
                            " WHERE Ejercicio = {1} AND num_cta = '{2}'" +
                            " GROUP BY num_cta", SaldoCcNombre, fil.Ejercicio, sini.Num_Cta);
                    DataSet datsetCc = datos.LeeDatos(sql);
                    if (datsetCc != null && datsetCc.Tables != null && datsetCc.Tables.Count > 0)
                    {
                        foreach (DataRow rw in datsetCc.Tables[0].Rows)
                        {
                            string num_Cta = rw.Field<string>("NUM_CTA");
                            SaldoI saux = slds.Find(delegate (SaldoI s)
                            { return s.Num_Cta == num_Cta; });
                            if (saux != null)
                            {
                                saux.InicialEx = rw.Field<double>("ini");
                                saux.CargoEx01 = rw.Field<double>("c1");
                                saux.AbonoEx01 = rw.Field<double>("a1");
                                saux.CargoEx02 = rw.Field<double>("c2");
                                saux.AbonoEx02 = rw.Field<double>("a2");
                                saux.CargoEx03 = rw.Field<double>("c3");
                                saux.AbonoEx03 = rw.Field<double>("a3");
                                saux.CargoEx04 = rw.Field<double>("c4");
                                saux.AbonoEx04 = rw.Field<double>("a4");
                                saux.CargoEx05 = rw.Field<double>("c5");
                                saux.AbonoEx05 = rw.Field<double>("a5");
                                saux.CargoEx06 = rw.Field<double>("c6");
                                saux.AbonoEx06 = rw.Field<double>("a6");
                                saux.CargoEx07 = rw.Field<double>("c7");
                                saux.AbonoEx07 = rw.Field<double>("a7");
                                saux.CargoEx08 = rw.Field<double>("c8");
                                saux.AbonoEx08 = rw.Field<double>("a8");
                                saux.CargoEx09 = rw.Field<double>("c9");
                                saux.AbonoEx09 = rw.Field<double>("a9");
                                saux.CargoEx10 = rw.Field<double>("c10");
                                saux.AbonoEx10 = rw.Field<double>("a10");
                                saux.CargoEx11 = rw.Field<double>("c11");
                                saux.AbonoEx11 = rw.Field<double>("a11");
                                saux.CargoEx12 = rw.Field<double>("c12");
                                saux.AbonoEx12 = rw.Field<double>("a12");
                            }
                        }
                        datsetCc.Dispose();
                    }
                }

                foreach (SaldoI sini in slds)
                {
                    if (Math.Abs(sini.Inicial - sini.InicialEx) > 0.01)
                        LogDebug.Escribe(string.Format("Saldo inicial diferente: {0}, {1}, {2}", sini.Num_Cta, sini.Inicial, sini.InicialEx));
                    if (Math.Abs(sini.Abono01 - sini.AbonoEx01) > 0.01)
                        LogDebug.Escribe(string.Format("Saldo Abono01 diferente: {0}, {1}, {2}", sini.Num_Cta, sini.Abono01, sini.AbonoEx01));
                    if (Math.Abs(sini.Abono02 - sini.AbonoEx02) > 0.01)
                        LogDebug.Escribe(string.Format("Saldo Abono02 diferente: {0}, {1}, {2}", sini.Num_Cta, sini.Abono02, sini.AbonoEx02));
                    if (Math.Abs(sini.Abono03 - sini.AbonoEx03) > 0.01)
                        LogDebug.Escribe(string.Format("Saldo Abono03 diferente: {0}, {1}, {2}", sini.Num_Cta, sini.Abono03, sini.AbonoEx03));
                    if (Math.Abs(sini.Abono04 - sini.AbonoEx04) > 0.01)
                        LogDebug.Escribe(string.Format("Saldo Abono04 diferente: {0}, {1}, {2}", sini.Num_Cta, sini.Abono04, sini.AbonoEx04));
                    if (Math.Abs(sini.Abono05 - sini.AbonoEx05) > 0.01)
                        LogDebug.Escribe(string.Format("Saldo Abono05 diferente: {0}, {1}, {2}", sini.Num_Cta, sini.Abono05, sini.AbonoEx05));
                    if (Math.Abs(sini.Abono06 - sini.AbonoEx06) > 0.01)
                        LogDebug.Escribe(string.Format("Saldo Abono06 diferente: {0}, {1}, {2}", sini.Num_Cta, sini.Abono06, sini.AbonoEx06));
                    if (Math.Abs(sini.Abono07 - sini.AbonoEx07) > 0.01)
                        LogDebug.Escribe(string.Format("Saldo Abono07 diferente: {0}, {1}, {2}", sini.Num_Cta, sini.Abono07, sini.AbonoEx07));
                    if (Math.Abs(sini.Abono08 - sini.AbonoEx08) > 0.01)
                        LogDebug.Escribe(string.Format("Saldo Abono08 diferente: {0}, {1}, {2}", sini.Num_Cta, sini.Abono08, sini.AbonoEx08));
                    if (Math.Abs(sini.Abono09 - sini.AbonoEx09) > 0.01)
                        LogDebug.Escribe(string.Format("Saldo Abono09 diferente: {0}, {1}, {2}", sini.Num_Cta, sini.Abono09, sini.AbonoEx09));
                    if (Math.Abs(sini.Abono10 - sini.AbonoEx10) > 0.01)
                        LogDebug.Escribe(string.Format("Saldo Abono10 diferente: {0}, {1}, {2}", sini.Num_Cta, sini.Abono10, sini.AbonoEx10));
                    if (Math.Abs(sini.Abono11 - sini.AbonoEx11) > 0.01)
                        LogDebug.Escribe(string.Format("Saldo Abono11 diferente: {0}, {1}, {2}", sini.Num_Cta, sini.Abono11, sini.AbonoEx11));
                    if (Math.Abs(sini.Abono12 - sini.AbonoEx12) > 0.01)
                        LogDebug.Escribe(string.Format("Saldo Abono12 diferente: {0}, {1}, {2}", sini.Num_Cta, sini.Abono12, sini.AbonoEx12));
                }
                datos.ConeccClose();
                datset.Dispose();
            }
        }

        public static EstadoResultado PruebaEdoR(string ruta, FiltroCOI fil)
        {
            EstadoResultado edoR = new EstadoResultado();

            edoR.NombreEmpresa = NombreEmpresa(Convert.ToInt16(fil.Emprss));
            // ruta = rutaEmprs(ruta, fil.Emprss);


            List<Proyecto> lstPry = null;
            List<CentroCosto> Ccst = null;

            lstPry = Proyectos(ruta);
            Ccst = CentrosC(ruta);

            edoR.Ingresos.Add(
                new ConceptoResultado
                {
                    ParentID = 0,
                    Numero = "CUENTA",
                    Nombre = "Ingresos",
                    PapaNumero = "CTA_PAPA",
                    Proyecto = lstPry.Find(delegate (Proyecto pr)
                    { return pr.Id == 49; }),
                    Centro = Ccst.Find(delegate (CentroCosto cc)
                    { return cc.Id == 1; }),
                    SaldoMes = 1000,
                    Acumulado = 2000
                });

            edoR.Ingresos.Add(
                new ConceptoResultado
                {
                    ParentID = 0,
                    Numero = "CUENTA",
                    Nombre = "Descuentos",
                    PapaNumero = "CTA_PAPA",
                    Proyecto = lstPry.Find(delegate (Proyecto pr)
                    { return pr.Id == 49; }),
                    Centro = Ccst.Find(delegate (CentroCosto cc)
                    { return cc.Id == 1; }),
                    SaldoMes = -500,
                    Acumulado = -700
                });


            // Costos
            edoR.Costos.Add(
                new ConceptoResultado
                {
                    ParentID = 0,
                    Numero = "CUENTA",
                    Nombre = "Costos de Ventas",
                    PapaNumero = "CTA_PAPA",
                    Proyecto = lstPry.Find(delegate (Proyecto pr)
                    { return pr.Id == 49; }),
                    Centro = Ccst.Find(delegate (CentroCosto cc)
                    { return cc.Id == 1; }),
                    SaldoMes = 300,
                    Acumulado = 500
                });

            // Gastos
            edoR.GastosGenerales.Add(
                new ConceptoResultado
                {
                    ParentID = 0,
                    Numero = "CUENTA",
                    Nombre = "Gastos de Ventas",
                    PapaNumero = "CTA_PAPA",
                    Proyecto = lstPry.Find(delegate (Proyecto pr)
                    { return pr.Id == 49; }),
                    Centro = Ccst.Find(delegate (CentroCosto cc)
                    { return cc.Id == 1; }),
                    SaldoMes = 30,
                    Acumulado = 50
                });

            edoR.GastosGenerales.Add(
                new ConceptoResultado
                {
                    ParentID = 0,
                    Numero = "CUENTA",
                    Nombre = "Gastos de Administración",
                    PapaNumero = "CTA_PAPA",
                    Proyecto = lstPry.Find(delegate (Proyecto pr)
                    { return pr.Id == 49; }),
                    Centro = Ccst.Find(delegate (CentroCosto cc)
                    { return cc.Id == 1; }),
                    SaldoMes = 20,
                    Acumulado = 45
                });

            edoR.GastosGenerales.Add(
                new ConceptoResultado
                {
                    ParentID = 0,
                    Numero = "CUENTA",
                    Nombre = "Amortización de Gastos diferidos",
                    PapaNumero = "CTA_PAPA",
                    Proyecto = lstPry.Find(delegate (Proyecto pr)
                    { return pr.Id == 49; }),
                    Centro = Ccst.Find(delegate (CentroCosto cc)
                    { return cc.Id == 1; }),
                    SaldoMes = 10,
                    Acumulado = 25
                });

            edoR.OtrosIngresosGastos.Add(
                new ConceptoResultado
                {
                    ParentID = 0,
                    Numero = "CUENTA",
                    Nombre = "Productos Financieros",
                    PapaNumero = "CTA_PAPA",
                    Proyecto = lstPry.Find(delegate (Proyecto pr)
                    { return pr.Id == 49; }),
                    Centro = Ccst.Find(delegate (CentroCosto cc)
                    { return cc.Id == 1; }),
                    SaldoMes = 8,
                    Acumulado = 15
                });
            edoR.OtrosIngresosGastos.Add(
                new ConceptoResultado
                {
                    ParentID = 0,
                    Numero = "CUENTA",
                    Nombre = "Gastos Financieros",
                    PapaNumero = "CTA_PAPA",
                    Proyecto = lstPry.Find(delegate (Proyecto pr)
                    { return pr.Id == 49; }),
                    Centro = Ccst.Find(delegate (CentroCosto cc)
                    { return cc.Id == 1; }),
                    SaldoMes = 18,
                    Acumulado = 35
                });

            edoR.Impuestos.Add(
                new ConceptoResultado
                {
                    ParentID = 0,
                    Numero = "CUENTA",
                    Nombre = "PTU",
                    PapaNumero = "CTA_PAPA",
                    Proyecto = lstPry.Find(delegate (Proyecto pr)
                    { return pr.Id == 49; }),
                    Centro = Ccst.Find(delegate (CentroCosto cc)
                    { return cc.Id == 1; }),
                    SaldoMes = 8,
                    Acumulado = 13
                });
            return edoR;
        }

        public static EstadoResultado EstadoR(string ruta, FiltroCOI fil, ref double vtas)
        {
            FireBird datos = new FireBird();
            EstadoResultado edoR = new EstadoResultado();

            edoR.NombreEmpresa = NombreEmpresa(Convert.ToInt16(fil.Emprss));
            ruta = rutaEmprs(ruta, fil.Emprss);
            ParametrosCOI prm = ParametrosCOI(ruta);

            if (fil == null)
                Ejercicio = "18";
            else
                Ejercicio = fil.Ejercicio.ToString().Substring(2);

            (datos.CadenaConn as FbConnectionStringBuilder).Database = ruta;

            int[] rubros = { 6, 7, 8, 9, 10 };

            List<Proyecto> lstPry = null;
            List<CentroCosto> Ccst = null;

            if (fil.CProyecto == null)
                lstPry = Proyectos(ruta);
            else
            {
                lstPry = new List<Proyecto>();
                lstPry.Add(fil.CProyecto);
            }
            if (fil.CentroC == null)
                Ccst = CentrosC(ruta);
            else
            {
                Ccst = new List<CentroCosto>();
                Ccst.Add(fil.CentroC);
            }

            for (int i = 0; i < rubros.Length; i++)
            {
                string sql = string.Format("SELECT {0}.cuenta," +
                    " {0}.segpapa," +
                    " {2}.nombre, {2}.cta_papa" +
                    " FROM {0}" +
                    " LEFT JOIN {2} ON {0}.cuenta = {2}.num_cta" +
                    " WHERE {0}.rubro = {1}", RubroNombre, rubros[i], CuentaNombre);

                DataSet datset = datos.LeeDatos(sql);

                if (datset != null && datset.Tables != null && datset.Tables.Count > 0)
                {
                    foreach (DataRow rw in datset.Tables[0].Rows)
                    {
                        string numcta = rw.Field<string>("CUENTA");
                        string nmbr = rw.Field<string>("NOMBRE");

                        ConceptoResultado cta = new ConceptoResultado
                        {
                            ParentID = 0,
                            Numero = rw.Field<string>("CUENTA"),
                            Nombre = rw.Field<string>("NOMBRE"),
                            PapaNumero = rw.Field<string>("CTA_PAPA")
                        };

                        // A ver si puedo resolver lo del Descuento
                        List<ConceptoResultado> lstAct = null;
                        if (i == 0)
                            lstAct = edoR.Ingresos;
                        else if (i == 1)
                            lstAct = edoR.Costos;
                        else if (i == 2)
                            lstAct = edoR.GastosGenerales;
                        else if (i == 3)
                            lstAct = edoR.OtrosIngresosGastos;
                        else if (i == 4)
                            lstAct = edoR.Impuestos;


                        if (!string.IsNullOrEmpty(cta.Nombre))
                        {
                            foreach (CentroCosto cc in Ccst)
                            {
                                // Llenamos todos los proyectos
                                foreach (Proyecto py in lstPry)
                                {
                                    lstAct.Add(
                                        new ConceptoResultado
                                        {
                                            ParentID = 0,
                                            Numero = rw.Field<string>("CUENTA"),
                                            Nombre = rw.Field<string>("NOMBRE"),
                                            PapaNumero = rw.Field<string>("CTA_PAPA"),
                                            Proyecto = py,
                                            Centro = cc
                                        });
                                }
                            }
                        }
                    }


                    foreach (DataRow rw in datset.Tables[0].Rows)
                    {
                        string numcta = rw.Field<string>("CUENTA");

                        if (i == 0)
                            LlenaEdoR(numcta, edoR.Ingresos, ruta, fil, i);
                        else if (i == 1)
                            LlenaEdoR(numcta, edoR.Costos, ruta, fil, i);
                        else if (i == 2)
                            LlenaEdoR(numcta, edoR.GastosGenerales, ruta, fil, i);
                        else if (i == 3)
                            LlenaEdoR(numcta, edoR.OtrosIngresosGastos, ruta, fil, i);
                        else if (i == 4)
                            LlenaEdoR(numcta, edoR.Impuestos, ruta, fil, i);
                    }

                    datset.Dispose();
                }
            }

            //edoR = PruebaEdoR(ruta, fil);

            // Utilidades
            foreach (CentroCosto cc in Ccst)
            {
                // Llenamos todos los proyectos
                foreach (Proyecto py in lstPry)
                {
                    edoR.UtilidadBruta.Add(new Utilidad
                    {
                        Proyecto = py,
                        Centro = cc
                    });
                    edoR.UtilidadOperacion.Add(new Utilidad
                    {
                        Proyecto = py,
                        Centro = cc
                    }
                        );
                    edoR.UtilidadNeta.Add(new Utilidad
                    {
                        Proyecto = py,
                        Centro = cc
                    });
                }
            }


            foreach (CentroCosto cc in Ccst)
            {
                // Llenamos todos los proyectos
                foreach (Proyecto py in lstPry)
                {
                    // Calcula porcentajes
                    // Calcula total de Ventas
                    double ventas = 0;
                    double acmventas = 0;
                    double descuentos = 0;
                    double acmdescuentos = 0;

                    ventas = edoR.Ingresos.Where(item => (item.Proyecto == null || item.Proyecto.Id == py.Id)
                                   && (item.Centro == null || item.Centro.Id == cc.Id)
                                   && item.Nombre.Contains("INGRESOS")) //"Ingresos"))
                        .Sum(item => item.SaldoMes);
                    acmventas = edoR.Ingresos.Where(item => (item.Proyecto == null || item.Proyecto.Id == py.Id)
                                   && (item.Centro == null || item.Centro.Id == cc.Id)
                                   && item.Nombre.Contains("INGRESOS")) //"Ingresos"))
                        .Sum(item => item.Acumulado);
                    descuentos = -edoR.Ingresos.Where(item => (item.Proyecto == null || item.Proyecto.Id == py.Id)
                                   && (item.Centro == null || item.Centro.Id == cc.Id)
                                   && !item.Nombre.Contains("INGRESOS"))//"Ingresos"))
                        .Sum(item => item.SaldoMes);
                    acmdescuentos = -edoR.Ingresos.Where(item => (item.Proyecto == null || item.Proyecto.Id == py.Id)
                                   && (item.Centro == null || item.Centro.Id == cc.Id)
                                   && !item.Nombre.Contains("INGRESOS")) //"Ingresos"))
                        .Sum(item => item.Acumulado);

                    Utilidad utb = edoR.UtilidadBruta.Find
                                            (delegate (Utilidad fut)
                                            { return fut.Proyecto == py && fut.Centro == cc; });
                    Utilidad uto = edoR.UtilidadOperacion.Find
                                            (delegate (Utilidad fut)
                                            { return fut.Proyecto == py && fut.Centro == cc; });
                    Utilidad utn = edoR.UtilidadNeta.Find
                                            (delegate (Utilidad fut)
                                            { return fut.Proyecto == py && fut.Centro == cc; });

                    vtas = ventas;
                    foreach (ConceptoResultado cpt in edoR.Ingresos.Where(item => (item.Proyecto == null || item.Proyecto.Id == py.Id)
                               && (item.Centro == null || item.Centro.Id == cc.Id)))
                    {
                        if ((ventas + descuentos) != 0)
                            cpt.Porcentaje = Convert.ToSingle(cpt.SaldoMes / (ventas + descuentos));
                        if ((acmventas + acmdescuentos) != 0)
                            cpt.PorcentajeAcumulado = Convert.ToSingle(cpt.Acumulado / (acmventas + acmdescuentos));
                        cpt.TipoC = ETipoConceptoER.aIngreso;
                        cpt.TipoU = ETipoUtilidad.aBruta;

                        if (!cpt.Nombre.Contains("INGRESO"))
                        {
                            cpt.SaldoMes *= -1;
                            cpt.Acumulado *= -1;
                            cpt.Porcentaje *= -1;
                            cpt.PorcentajeAcumulado *= -1;
                        }

                        utb.SaldoMes += cpt.SaldoMes;
                        utb.Acumulado += cpt.Acumulado;

                        uto.SaldoMes += cpt.SaldoMes;
                        uto.Acumulado += cpt.Acumulado;

                        utn.SaldoMes += cpt.SaldoMes;
                        utn.Acumulado += cpt.Acumulado;
                    }
                    foreach (ConceptoResultado cpt in edoR.Costos.Where(item => (item.Proyecto == null || item.Proyecto.Id == py.Id)
                               && (item.Centro == null || item.Centro.Id == cc.Id)))
                    {
                        if ((ventas + descuentos) != 0)
                            cpt.Porcentaje = Convert.ToSingle(cpt.SaldoMes / (ventas + descuentos));
                        if ((acmventas + acmdescuentos) != 0)
                            cpt.PorcentajeAcumulado = Convert.ToSingle(cpt.Acumulado / (acmventas + acmdescuentos));
                        cpt.TipoC = ETipoConceptoER.bCosto;
                        cpt.TipoU = ETipoUtilidad.aBruta;

                        utb.SaldoMes -= cpt.SaldoMes;
                        utb.Acumulado -= cpt.Acumulado;

                        uto.SaldoMes -= cpt.SaldoMes;
                        uto.Acumulado -= cpt.Acumulado;

                        utn.SaldoMes -= cpt.SaldoMes;
                        utn.Acumulado -= cpt.Acumulado;
                    }

                    foreach (ConceptoResultado cpt in edoR.GastosGenerales.Where(item => (item.Proyecto == null || item.Proyecto.Id == py.Id)
                               && (item.Centro == null || item.Centro.Id == cc.Id)))
                    {
                        if ((ventas + descuentos) != 0)
                            cpt.Porcentaje = Convert.ToSingle(cpt.SaldoMes / (ventas + descuentos));
                        if ((acmventas + acmdescuentos) != 0)
                            cpt.PorcentajeAcumulado = Convert.ToSingle(cpt.Acumulado / (acmventas + acmdescuentos));
                        cpt.TipoC = ETipoConceptoER.cGastoGeneral;
                        cpt.TipoU = ETipoUtilidad.bOperacion;

                        uto.SaldoMes -= cpt.SaldoMes;
                        uto.Acumulado -= cpt.Acumulado;

                        utn.SaldoMes -= cpt.SaldoMes;
                        utn.Acumulado -= cpt.Acumulado;
                    }

                    foreach (ConceptoResultado cpt in edoR.OtrosIngresosGastos.Where(item => (item.Proyecto == null || item.Proyecto.Id == py.Id)
                               && (item.Centro == null || item.Centro.Id == cc.Id)))
                    {
                        if ((ventas + descuentos) != 0)
                            cpt.Porcentaje = Convert.ToSingle(cpt.SaldoMes / (ventas + descuentos));
                        if ((acmventas + acmdescuentos) != 0)
                            cpt.PorcentajeAcumulado = Convert.ToSingle(cpt.Acumulado / (acmventas + acmdescuentos));
                        cpt.TipoC = ETipoConceptoER.dOtrosIngresosGastos;
                        cpt.TipoU = ETipoUtilidad.cNeta;

                        utn.SaldoMes += cpt.SaldoMes;
                        utn.Acumulado += cpt.Acumulado;
                    }

                    foreach (ConceptoResultado cpt in edoR.Impuestos.Where(item => (item.Proyecto == null || item.Proyecto.Id == py.Id)
                               && (item.Centro == null || item.Centro.Id == cc.Id)))
                    {
                        if ((ventas + descuentos) != 0)
                            cpt.Porcentaje = Convert.ToSingle(cpt.SaldoMes / (ventas + descuentos));
                        if ((acmventas + acmdescuentos) != 0)
                            cpt.PorcentajeAcumulado = Convert.ToSingle(cpt.Acumulado / (acmventas + acmdescuentos));
                        cpt.TipoC = ETipoConceptoER.eImpuestos;
                        cpt.TipoU = ETipoUtilidad.cNeta;

                        utn.SaldoMes -= cpt.SaldoMes;
                        utn.Acumulado -= cpt.Acumulado;
                    }

                    if ((ventas + descuentos) != 0)
                    {
                        utb.Porcentaje = Convert.ToSingle(utb.SaldoMes / (ventas + descuentos));
                        uto.Porcentaje = Convert.ToSingle(uto.SaldoMes / (ventas + descuentos));
                        utn.Porcentaje = Convert.ToSingle(utn.SaldoMes / (ventas + descuentos));
                    }
                    if ((acmventas + acmdescuentos) != 0)
                    {
                        utb.PorcentajeAcumulado = Convert.ToSingle(utb.Acumulado / (acmventas + acmdescuentos));
                        uto.PorcentajeAcumulado = Convert.ToSingle(uto.Acumulado / (acmventas + acmdescuentos));
                        utn.PorcentajeAcumulado = Convert.ToSingle(utn.Acumulado / (acmventas + acmdescuentos));
                    }
                }
            }


            return edoR;
        }

        #region
        // Edo de Resultado por Centro de Costos
        private static void LlenaEdoRCc(string nC, List<ConceptoResultado> lista, string ruta, FiltroCOI fil)
        {
            int pos = fil.MesD.GetHashCode() + 1;
            FireBird datos = new FireBird();
            (datos.CadenaConn as FbConnectionStringBuilder).Database = ruta;

            if (fil == null)
                Ejercicio = "18";
            else
                Ejercicio = fil.Ejercicio.ToString().Substring(2);

            string sqlcta = string.Format("SELECT montomov FROM auxiliar18 WHERE " +
                " ejercicio = {0} AND periodo = {1} " +
                " AND num_cta LIKE '{2}%' AND ccostos = {3}", fil.Ejercicio, pos, nC, fil.CentroC.Id);

            DataSet datsetCta = datos.LeeDatos(sqlcta);

            if (datsetCta != null && datsetCta.Tables != null && datsetCta.Tables.Count > 0)
            {
                double mnto = 0;
                foreach (DataRow rwCta in datsetCta.Tables[0].Rows)
                {
                    mnto += rwCta.Field<double>("MONTOMOV");
                }

                ConceptoResultado cta = new ConceptoResultado
                {
                    ID = 0,
                    ParentID = 0,
                    Numero = nC,
                    Nombre = string.Empty,
                    TipoAD = 0,
                    PapaNumero = string.Empty
                };

                // double ini = 0;
                double cg01 = mnto;
                double ab01 = 0;

                cta.SaldoMes = Math.Abs(cg01 - ab01);
                cta.Acumulado += cta.SaldoMes;
                lista.Add(cta);
            }
        }
        #endregion

        private static void LlenaEdoR(string nC, List<ConceptoResultado> lista, string ruta,
            FiltroCOI fil, int cual)
        {
            int pos = fil.MesD.GetHashCode();
            FireBird datos = new FireBird();
            (datos.CadenaConn as FbConnectionStringBuilder).Database = ruta;

            string sqlcta = string.Format("SELECT {0}.num_cta, {0}.nombre, {0}.cta_papa, {0}.naturaleza" +
                ", {1}.inicial" +
                ", {1}.cargo01, {1}.abono01 " +
                ", {1}.cargo02, {1}.abono02 " +
                ", {1}.cargo03, {1}.abono03 " +
                ", {1}.cargo04, {1}.abono04 " +
                ", {1}.cargo05, {1}.abono05 " +
                ", {1}.cargo06, {1}.abono06 " +
                ", {1}.cargo07, {1}.abono07 " +
                ", {1}.cargo08, {1}.abono08 " +
                ", {1}.cargo09, {1}.abono09 " +
                ", {1}.cargo10, {1}.abono10 " +
                ", {1}.cargo11, {1}.abono11 " +
                ", {1}.cargo12, {1}.abono12 " +
                ", {1}.ccostos" +
                ", {1}.cgrupos" +
                " FROM {0}" +
                " LEFT JOIN {1} ON {0}.num_cta = {1}.num_cta" +
                " WHERE {0}.num_cta = '{2}'" +
                " AND Ejercicio = {3}",
                CuentaNombre, SaldoCcNombre, nC.Substring(0, nC.Length), fil.Ejercicio);
            if (fil.CentroC != null)
                sqlcta = string.Format("{0} AND {2}.CCostos = {1}", sqlcta, fil.CentroC.Id, SaldoCcNombre);
            if (fil.CProyecto != null)
                sqlcta = string.Format("{0} AND {2}.CGrupos = {1}", sqlcta, fil.CProyecto.Id, SaldoCcNombre);


            DataSet datsetCta = datos.LeeDatos(sqlcta);

            if (datsetCta != null && datsetCta.Tables != null && datsetCta.Tables.Count > 0)
            {
                //int id = 1; // 0 sería el papá de todos
                foreach (DataRow rwCta in datsetCta.Tables[0].Rows)
                {
                    int pry = 0;
                    int cc = 0;
                    if (!rwCta.IsNull("CGrupos"))
                        pry = rwCta.Field<int>("CGrupos");
                    if (!rwCta.IsNull("CCostos"))
                        cc = rwCta.Field<int>("CCostos");

                    var filtrado = from ConceptoResultado cR in lista
                                   where cR.Numero == nC && (cR.Proyecto == null || cR.Proyecto.Id == pry)
                                   && (cR.Centro == null || cR.Centro.Id == cc)
                                   select cR;


                    foreach (ConceptoResultado cta in filtrado)
                    {
                        if (cta.Nombre == "VENTAS" || cta.Nombre == "PRODUCTOS FINANCIEROS")
                            cta.Suma = true;

                        double ini = 0;
                        double cg01 = 0;
                        double ab01 = 0;

                        try
                        {
                            ini = rwCta.Field<double>("inicial");
                        }
                        catch (Exception)
                        { }
                        for (int mes = 1; mes <= pos + 1; mes++)
                        {
                            try
                            {
                                string aux = string.Format("cargo{0}", mes.ToString().PadLeft(2, '0'));
                                if (!rwCta.IsNull(aux))
                                    cg01 = rwCta.Field<double>(aux);
                            }
                            catch (Exception)
                            { }
                            try
                            {
                                string aux = string.Format("abono{0}", mes.ToString().PadLeft(2, '0'));
                                if (!rwCta.IsNull(aux))
                                    ab01 = rwCta.Field<double>(aux);
                            }
                            catch (Exception)
                            { }

                            cta.SaldoMes = Math.Abs(cg01 - ab01);
                            cta.Acumulado += cta.SaldoMes;
                        }
                        cta.Acumulado += ini;
                    }
                }

                datsetCta.Dispose();
            }
        }

        public static BindingList<Poliza> Polizas(string ruta)
        {
            FireBird datos = new FireBird();

            (datos.CadenaConn as FbConnectionStringBuilder).Database = ruta;

            string sql = "select * From polizas18";

            DataSet datset = datos.LeeDatos(sql);
            BindingList<Poliza> ListaPoliz = null;

            using (new CWaitCursor())
            {
                if (datset != null && datset.Tables != null && datset.Tables.Count > 0)
                {
                    Poliza pol = null;
                    ListaPoliz = new BindingList<Poliza>();
                    foreach (DataRow rw in datset.Tables[0].Rows)
                    {
                        pol = new Poliza
                        {
                            Tipo = rw.Field<string>("TIPO_POLI"),
                            Numero = rw.Field<string>("NUM_POLIZ"),
                            Periodo = rw.Field<short>("PERIODO"),
                            Ejercicio = rw.Field<int>("EJERCICIO"),
                            Fecha = rw.Field<DateTime>("FECHA_POL"),
                            Concepto = rw.Field<string>("CONCEP_PO"),
                            Cuantas = rw.Field<int>("NUM_PART")
                        };

                        sql = string.Format(
                            "select * From auxiliar18 where tipo_poli = '{0}'" +
                            " and num_poliz = '{1}'" +
                            " and PERIODO = {2}", pol.Tipo, pol.Numero, pol.Periodo);
                        DataSet datAux = datos.LeeDatos(sql);

                        if (datAux != null && datAux.Tables != null && datAux.Tables.Count > 0)
                        {
                            foreach (DataRow rwa in datAux.Tables[0].Rows)
                            {
                                pol.Partidas.Add(new PolizaItem
                                {
                                    Partida = Convert.ToInt32(rwa.Field<double>("NUM_PART")),
                                    Cuenta = rwa.Field<string>("NUM_CTA"),
                                    Concepto = rwa.Field<string>("CONCEP_PO"),
                                    DebeHaber = rwa.Field<string>("DEBE_HABER"),
                                    Monto = rwa.Field<double>("MONTOMOV"),
                                    Departamento = rwa.Field<short>("NUMDEPTO"),
                                    TipoCambio = rwa.Field<double>("TIPCAMBIO"),
                                    CentroCosto = rwa.Field<int>("CCOSTOS"),
                                    Proyecto = rwa.Field<int>("CGRUPOS")
                                });
                            }
                        }
                        ListaPoliz.Add(pol);
                    }
                }
            }
            return ListaPoliz;
        }

        public static List<BalanceGeneral> BalanceGeneral(string ruta, FiltroCOI fil)
        {
            FireBird datos = new FireBird();
            BalanceGeneral bg = new BalanceGeneral();
            List<BalanceGeneral> ListaBalanceG = new List<BalanceGeneral>();

            ListaBalanceG.Clear();
            if (fil == null)
                Ejercicio = "18";
            else
                Ejercicio = fil.Ejercicio.ToString().Substring(2);

            int[] rubros = { 1, 2, 3, 4, 5 };

            (datos.CadenaConn as FbConnectionStringBuilder).Database = ruta;

            for (int i = 0; i < rubros.Length; i++)
            {
                string sql = string.Format("SELECT {0}.cuenta, {0}.segpapa FROM {0}" +
                    " WHERE {0}.rubro = {1}", RubroNombre, rubros[i]);

                DataSet datset = datos.LeeDatos(sql);

                if (datset != null && datset.Tables != null && datset.Tables.Count > 0)
                {
                    foreach (DataRow rw in datset.Tables[0].Rows)
                    {
                        string numcta = rw.Field<string>("CUENTA");
                        string papa = rw.Field<string>("SEGPAPA");

                        if (i == 0)
                            LlenaBalG(numcta, bg.ActivoCirculante, ruta, fil, EAPC.Activo);
                        else if (i == 1)
                            LlenaBalG(numcta, bg.ActivoNoCirculante, ruta, fil, EAPC.Activo);
                        else if (i == 2)
                            LlenaBalG(numcta, bg.PasivoACortoPlazo, ruta, fil, EAPC.PasivoCapitalContable);
                        else if (i == 3)
                            LlenaBalG(numcta, bg.PasivoALargoPlazo, ruta, fil, EAPC.PasivoCapitalContable);
                        else if (i == 4)
                            LlenaBalG(numcta, bg.CapitalContable, ruta, fil, EAPC.PasivoCapitalContable);
                    }
                }
            }

            #region
            /*
            BalanceGeneral bg = new BalanceGeneral();

            bg.ActivoCirculante = new List<CuentaBalanceGeneral>
            {
                new CuentaBalanceGeneral
                (nombre:"FONDO FIJO DE CAJA", mes:12000, mesAnterior: 12000, porcentaje: 0 ),
                new CuentaBalanceGeneral
                (nombre:"BANCOS", mes:15493639.83m, mesAnterior: 15368426.02m, porcentaje: 0 ),
                new CuentaBalanceGeneral
                (nombre:"INVERSIONES EN VALORES", mes:125140.83m, mesAnterior: 125140.00m, porcentaje: 0 )
            };


            bg.ActivoNoCirculante = new List<CuentaBalanceGeneral>
            {
                new CuentaBalanceGeneral
                (nombre:"PROPIEDADES PANTA Y EQUIPO", mes:1453920.00m, mesAnterior: 1453920.00m, porcentaje: 0 ),
                new CuentaBalanceGeneral
                (nombre:"DEPRECIACION", mes:75081.00m, mesAnterior: 62567.50m, porcentaje: 0 )
            };*/

            /*
            bg.PasivoALagoPlazo = new List<CuentaBalanceGeneral>();
            bg.PasivoACortoPlazo = new List<CuentaBalanceGeneral>();*/
            /*
            ListaBalanceG.Add(bg);
            return ListaBalanceG;*/

            /*
            ListaBalanceG.AddRange(
                new List<CuentaBalanceGeneral>
                {
                new CuentaBalanceGeneral
                (nombre: "FONDO FIJO DE CAJA", mes: 12000, mesAnterior: 12000, porcentaje: 0),
                new CuentaBalanceGeneral
                (nombre: "BANCOS", mes: 15493639.83m, mesAnterior: 15368426.02m, porcentaje: 0),
                new CuentaBalanceGeneral
                (nombre: "INVERSIONES EN VALORES", mes: 125140.83m, mesAnterior: 125140.00m, porcentaje: 0)
                }
            );
            return ListaBalanceG;*/
            #endregion

            /*
            ListaBalanceG.AddRange(bg.ActivoCirculante);
            ListaBalanceG.AddRange(bg.ActivoNoCirculante);
            ListaBalanceG.AddRange(bg.PasivoACortoPlazo);
            ListaBalanceG.AddRange(bg.PasivoALargoPlazo);
            ListaBalanceG.AddRange(bg.CapitalContable);*/
            ListaBalanceG.Add(bg);
            return ListaBalanceG;
        }

        private static void LlenaBalG(string nC, List<CuentaBalanceGeneral> lista,
            string ruta, FiltroCOI fil, EAPC tp)
        {
            int pos = fil.MesD.GetHashCode();
            FireBird datos = new FireBird();
            (datos.CadenaConn as FbConnectionStringBuilder).Database = ruta;

            string sqlcta = string.Format("SELECT {0}.num_cta, {0}.nombre, {0}.cta_papa, {0}.naturaleza" +
                ", {1}.inicial" +
                ", {1}.cargo01, {1}.abono01 " +
                ", {1}.cargo02, {1}.abono02 " +
                " FROM {0}" +
                " LEFT JOIN {1} ON {0}.num_cta = {1}.num_cta" +
                " WHERE {0}.num_cta ='{2}'", CuentaNombre, SaldoNombre, nC.Substring(0, nC.Length));

            DataSet datsetCta = datos.LeeDatos(sqlcta);

            if (datsetCta != null && datsetCta.Tables != null && datsetCta.Tables.Count > 0)
            {
                int id = 1; // 0 sería el papá de todos
                foreach (DataRow rwCta in datsetCta.Tables[0].Rows)
                {
                    CuentaBalanceGeneral cta = new CuentaBalanceGeneral
                    {
                        SaldoMes = 0,
                        MesAnterior = 0,
                        Porcentaje = 0,

                        ID = id++,
                        ParentID = 0,
                        Numero = rwCta.Field<string>("NUM_CTA"),
                        Nombre = rwCta.Field<string>("NOMBRE"),
                        PapaNumero = rwCta.Field<string>("CTA_PAPA").Trim(),
                        TpAPC = tp
                    };

                    double ini = 0;
                    double cg01 = 0;
                    double ab01 = 0;

                    try
                    {
                        ini = rwCta.Field<double>("inicial");
                    }
                    catch (Exception)
                    { }
                    for (int mes = 1; mes <= pos + 1; mes++)
                    {
                        try
                        {
                            string aux = string.Format("cargo{0}", mes.ToString().PadLeft(2, '0'));
                            cg01 = rwCta.Field<double>(aux);
                        }
                        catch (Exception)
                        { }
                        try
                        {
                            string aux = string.Format("abono{0}", mes.ToString().PadLeft(2, '0'));
                            ab01 = rwCta.Field<double>(aux);
                        }
                        catch (Exception)
                        { }

                        cta.SaldoMes = Math.Abs(cg01 - ab01);
                    }
                    lista.Add(cta);
                }

                for (int i = 0; i < lista.Count; i++)
                {
                    CuentaBalanceGeneral ct = lista[i] as CuentaBalanceGeneral;

                    if (ct.PapaNumero != "-1")
                    {
                        ct.ParentID = lista.Find(delegate (CuentaBalanceGeneral c) { return c.Numero == ct.PapaNumero; }).ID;
                    }
                }
            }
        }


        public static List<CentroCosto> CentrosC(string ruta)
        {
            if (string.IsNullOrEmpty(ruta))
            {
                throw new Exception("Falta la ruta de datos");
            }
            else
            {
                FireBird datos = new FireBird();
                (datos.CadenaConn as FbConnectionStringBuilder).Database = ruta;

                string sql = string.Format("select * From ccostos");
                FbDataReader reader = datos.LeeDatosOnly(sql) as FbDataReader;
                List<CentroCosto> ccostos = new List<CentroCosto>();

                using (new CWaitCursor())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            ccostos.Add(new CentroCosto
                            {
                                Id = reader.GetInt16(0),
                                Descripcion = reader.GetString(1)
                            });
                        }
                    }
                    reader.Close();
                }
                return ccostos;
            }
        }

        public static List<Proyecto> Proyectos(string ruta)
        {
            if (string.IsNullOrEmpty(ruta))
            {
                throw new Exception("Falta la ruta de datos");
            }
            else
            {
                FireBird datos = new FireBird();
                (datos.CadenaConn as FbConnectionStringBuilder).Database = ruta;

                string sql = string.Format("select * FROM cgrupos");
                FbDataReader reader = datos.LeeDatosOnly(sql) as FbDataReader;
                List<Proyecto> proyectos = new List<Proyecto>();

                using (new CWaitCursor())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            proyectos.Add(new Proyecto
                            {
                                Id = reader.GetInt16(0),
                                Descripcion = reader.GetString(1)
                            });
                        }
                    }
                    reader.Close();
                }
                return proyectos;
            }
        }

        public static List<SaldoI> SaldosICc(string ruta, int prd)
        {
            if (string.IsNullOrEmpty(ruta))
            {
                throw new Exception("Falta la ruta de datos");
            }
            else
            {
                Ejercicio = prd.ToString().Substring(2);

                FireBird datos = new FireBird();
                (datos.CadenaConn as FbConnectionStringBuilder).Database = ruta;

                string sql = string.Format("SELECT * FROM {0}", SaldoCcNombre);
                FbDataReader reader = datos.LeeDatosOnly(sql) as FbDataReader;
                List<SaldoI> sldsICc = new List<SaldoI>();

                using (new CWaitCursor())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            sldsICc.Add(new SaldoI
                            {
                                Num_Cta = reader.GetString(0),
                                Ejercicio = reader.GetInt16(1),
                                CentroCosto = reader.GetInt16(2),
                                Inicial = reader.GetDouble(3)
                            });
                        }
                    }
                    reader.Close();
                }
                return sldsICc;
            }
        }

        private static ParametrosCOI mParametrosCOI;
        private static ParametrosCOI ParametrosCOI(string ruta)
        {
            if (mParametrosCOI == null)
            {
                mParametrosCOI = new ParametrosCOI();

                FireBird datos = new FireBird();
                (datos.CadenaConn as FbConnectionStringBuilder).Database = ruta;

                string sql = string.Format("select " +
                    "DigCta1, DigCta2, DigCta3, DigCta4, DigCta5, " +
                    "DigCta6, DigCta7, DigCta8, DigCta9, NivelActu, Guion_sino " +
                    "From paramemp");
                FbDataReader reader = datos.LeeDatosOnly(sql) as FbDataReader;

                if (reader.HasRows)
                {
                    //while (reader.Read())
                    //{
                    reader.Read();
                    for (int i = 0; i < 9; i++)
                    {
                        mParametrosCOI.DigCta[i] = reader.GetInt16(i);
                    }
                    mParametrosCOI.NivelActu = reader.GetInt16(9);
                    mParametrosCOI.Guio_Sino = reader.GetString(10) == "S";
                    //}
                }
                reader.Close();
            }
            return mParametrosCOI;
        }

        private static string CuentaFormato(string value, ParametrosCOI prm)
        {
            string aux = string.Empty;
            int ini = 0;


            for (int i = 0; i < prm.NivelActu; i++)
            {
                if (i > 0)
                    aux += "-";
                aux += value.Substring(ini, prm.DigCta[i]);
                ini += prm.DigCta[i];
            }

            return aux;
        }

        public static void CreaTablaCC(int prd, string ruta, int emprs)
        {
            FireBird datos = new FireBird();

            ruta = rutaEmprs(ruta, emprs.ToString());
            (datos.CadenaConn as FbConnectionStringBuilder).Database = ruta;

            Ejercicio = prd.ToString().Substring(2);

            if (!datos.ExisteTabla(SaldoCcNombre))
            {
                List<Campo> cmps = new List<Campo>
                {
                    new Campo { Nombre = "Num_Cta", Tipo = "char", Tamano = 21, Requerido = true },
                    new Campo { Nombre = "Ejercicio", Tipo = "smallint", Requerido = true },
                    new Campo { Nombre = "Ccostos", Tipo = "int", Requerido = true },
                    new Campo { Nombre = "CGrupos", Tipo = "int", Requerido = true },
                    new Campo { Nombre = "Inicial", Tipo = "numeric", Value = 0 },
                    new Campo { Nombre = "InicialEx", Tipo = "numeric", Value = 0 },
                    new Campo { Nombre = "Cargo01", Tipo = "numeric", Value = 0 },
                    new Campo { Nombre = "Abono01", Tipo = "numeric", Value = 0 },
                    new Campo { Nombre = "CargoEx01", Tipo = "numeric", Value = 0 },
                    new Campo { Nombre = "AbonoEx01", Tipo = "numeric", Value = 0 },
                    new Campo { Nombre = "Cargo02", Tipo = "numeric", Value = 0 },
                    new Campo { Nombre = "Abono02", Tipo = "numeric", Value = 0 },
                    new Campo { Nombre = "CargoEx02", Tipo = "numeric", Value = 0 },
                    new Campo { Nombre = "AbonoEx02", Tipo = "numeric", Value = 0 },
                    new Campo { Nombre = "Cargo03", Tipo = "numeric", Value = 0 },
                    new Campo { Nombre = "Abono03", Tipo = "numeric", Value = 0 },
                    new Campo { Nombre = "CargoEx03", Tipo = "numeric", Value = 0 },
                    new Campo { Nombre = "AbonoEx03", Tipo = "numeric", Value = 0 },
                    new Campo { Nombre = "Cargo04", Tipo = "numeric", Value = 0 },
                    new Campo { Nombre = "Abono04", Tipo = "numeric", Value = 0 },
                    new Campo { Nombre = "CargoEx04", Tipo = "numeric", Value = 0 },
                    new Campo { Nombre = "AbonoEx04", Tipo = "numeric", Value = 0 },
                    new Campo { Nombre = "Cargo05", Tipo = "numeric", Value = 0 },
                    new Campo { Nombre = "Abono05", Tipo = "numeric", Value = 0 },
                    new Campo { Nombre = "CargoEx05", Tipo = "numeric", Value = 0 },
                    new Campo { Nombre = "AbonoEx05", Tipo = "numeric", Value = 0 },
                    new Campo { Nombre = "Cargo06", Tipo = "numeric", Value = 0 },
                    new Campo { Nombre = "Abono06", Tipo = "numeric", Value = 0 },
                    new Campo { Nombre = "CargoEx06", Tipo = "numeric", Value = 0 },
                    new Campo { Nombre = "AbonoEx06", Tipo = "numeric", Value = 0 },
                    new Campo { Nombre = "Cargo07", Tipo = "numeric", Value = 0 },
                    new Campo { Nombre = "Abono07", Tipo = "numeric", Value = 0 },
                    new Campo { Nombre = "CargoEx07", Tipo = "numeric", Value = 0 },
                    new Campo { Nombre = "AbonoEx07", Tipo = "numeric", Value = 0 },
                    new Campo { Nombre = "Cargo08", Tipo = "numeric", Value = 0 },
                    new Campo { Nombre = "Abono08", Tipo = "numeric", Value = 0 },
                    new Campo { Nombre = "CargoEx08", Tipo = "numeric", Value = 0 },
                    new Campo { Nombre = "AbonoEx08", Tipo = "numeric", Value = 0 },
                    new Campo { Nombre = "Cargo09", Tipo = "numeric", Value = 0 },
                    new Campo { Nombre = "Abono09", Tipo = "numeric", Value = 0 },
                    new Campo { Nombre = "CargoEx09", Tipo = "numeric", Value = 0 },
                    new Campo { Nombre = "AbonoEx09", Tipo = "numeric", Value = 0 },
                    new Campo { Nombre = "Cargo10", Tipo = "numeric", Value = 0 },
                    new Campo { Nombre = "Abono10", Tipo = "numeric", Value = 0 },
                    new Campo { Nombre = "CargoEx10", Tipo = "numeric", Value = 0 },
                    new Campo { Nombre = "AbonoEx10", Tipo = "numeric", Value = 0 },
                    new Campo { Nombre = "Cargo11", Tipo = "numeric", Value = 0 },
                    new Campo { Nombre = "Abono11", Tipo = "numeric", Value = 0 },
                    new Campo { Nombre = "CargoEx11", Tipo = "numeric", Value = 0 },
                    new Campo { Nombre = "AbonoEx11", Tipo = "numeric", Value = 0 },
                    new Campo { Nombre = "Cargo12", Tipo = "numeric", Value = 0 },
                    new Campo { Nombre = "Abono12", Tipo = "numeric", Value = 0 },
                    new Campo { Nombre = "CargoEx12", Tipo = "numeric", Value = 0 },
                    new Campo { Nombre = "AbonoEx12", Tipo = "numeric", Value = 0 },
                    new Campo { Nombre = "Cargo13", Tipo = "numeric", Value = 0 },
                    new Campo { Nombre = "Abono13", Tipo = "numeric", Value = 0 },
                    new Campo { Nombre = "CargoEx13", Tipo = "numeric", Value = 0 },
                    new Campo { Nombre = "AbonoEx13", Tipo = "numeric", Value = 0 },
                    new Campo { Nombre = "Cargo14", Tipo = "numeric", Value = 0 },
                    new Campo { Nombre = "Abono14", Tipo = "numeric", Value = 0 },
                    new Campo { Nombre = "CargoEx14", Tipo = "numeric", Value = 0 },
                    new Campo { Nombre = "AbonoEx14", Tipo = "numeric", Value = 0 },
                    new Campo { Nombre = "Mov01", Tipo = "smallint", Value = 0 },
                    new Campo { Nombre = "Mov02", Tipo = "smallint", Value = 0 },
                    new Campo { Nombre = "Mov03", Tipo = "smallint", Value = 0 },
                    new Campo { Nombre = "Mov04", Tipo = "smallint", Value = 0 },
                    new Campo { Nombre = "Mov05", Tipo = "smallint", Value = 0 },
                    new Campo { Nombre = "Mov06", Tipo = "smallint", Value = 0 },
                    new Campo { Nombre = "Mov07", Tipo = "smallint", Value = 0 },
                    new Campo { Nombre = "Mov08", Tipo = "smallint", Value = 0 },
                    new Campo { Nombre = "Mov09", Tipo = "smallint", Value = 0 },
                    new Campo { Nombre = "Mov10", Tipo = "smallint", Value = 0 },
                    new Campo { Nombre = "Mov11", Tipo = "smallint", Value = 0 },
                    new Campo { Nombre = "Mov12", Tipo = "smallint", Value = 0 },
                    new Campo { Nombre = "Mov13", Tipo = "smallint", Value = 0 },
                    new Campo { Nombre = "Mov14", Tipo = "smallint", Value = 0 }
                };

                datos.CreaTabla(SaldoCcNombre, cmps);

                IDataParameter[] parames = null;
                string sql = string.Empty;

                sql = string.Format("ALTER TABLE {0} ADD CONSTRAINT PK_SALDOSCC18 PRIMARY KEY(NUM_CTA, EJERCICIO, CCOSTOS, CGRUPOS)", SaldoCcNombre);
                datos.EjecutaSentencia(sql, parames);
            }
        }

        public static void AddSaldos(string ruta, int prd)
        {
            if (string.IsNullOrEmpty(ruta))
            {
                throw new Exception("Falta la ruta de datos");
            }
            else
            {
                FireBird datos = new FireBird();
                (datos.CadenaConn as FbConnectionStringBuilder).Database = ruta;

                string sql = string.Format("select Num_cta" +
                    " From {0}", SaldoNombre);

                using (new CWaitCursor())
                {
                    FbDataReader reader = datos.LeeDatosOnly(sql) as FbDataReader;

                    if (reader.HasRows)
                    {
                        List<CentroCosto> Ccst = CentrosC(ruta);
                        List<Proyecto> Prycts = Proyectos(ruta);

                        IDataParameter[] parames = null;
                        while (reader.Read())
                        {
                            string cta = reader.GetString(0);

                            foreach (CentroCosto cc in Ccst)
                            {
                                foreach (Proyecto pr in Prycts)
                                {
                                    string sqlin = string.Format("INSERT INTO {0} " +
                                        "(NUM_CTA, EJERCICIO, CCOSTOS, CGRUPOS) VALUES ('{1}', {2}, {3}, {4});",
                                        SaldoCcNombre, cta, prd, cc.Id, pr.Id);
                                    datos.EjecutaSentencia(sqlin, parames);
                                }
                            }
                        }
                    }
                    reader.Close();
                }
            }
        }

        private static List<SaldoI> SldosI;
        public static void UpdtSaldos(string ruta, int ejrc, int prd, int emprs)
        {
            if (string.IsNullOrEmpty(ruta))
            {
                throw new Exception("Falta la ruta de datos");
            }
            else
            {
                if (prd == 1)
                    UpdSldIni(ruta, ejrc, emprs);


                Ejercicio = ejrc.ToString().Substring(2);
                FireBird datos = new FireBird();

                ruta = rutaEmprs(ruta, emprs.ToString());
                (datos.CadenaConn as FbConnectionStringBuilder).Database = ruta;

                string sql = string.Format("select Num_Cta, MontoMov, CCostos, Debe_Haber, CGrupos" +
                    " From {0} Where Periodo = {1}", AuxiliarNombre, prd);

                using (new CWaitCursor())
                {
                    FbDataReader reader = datos.LeeDatosOnly(sql) as FbDataReader;
                    SldosI = new List<SaldoI>();

                    if (reader.HasRows)
                    {
                        FiltroCOI fil = new FiltroCOI();
                        fil.Ejercicio = ejrc;

                        List<Cuenta> Ctas = CuentasCOI(ruta, fil);


                        while (reader.Read())
                        {
                            string cta = reader.GetString(0);
                            double mnt = reader.GetDouble(1);
                            int cc = reader.GetInt16(2);
                            string dh = reader.GetString(3);
                            int cg = reader.GetInt16(4);

                            if (cta.Contains(/*"104000201100000000003"*/
                                "102000100100000000003") && cc == 0)
                            {
                                mnt = mnt + 1;
                                mnt = mnt - 1;
                            }

                            UpSldsCicl(cta, cc, dh, mnt, cg);
                        }
                    }
                    reader.Close();
                    // datos.ConeccClose();

                    string sqlup = string.Empty;
                    IDataParameter[] parames = null;

                    foreach (SaldoI sd in SldosI)
                    {
                        sqlup = string.Format("SELECT NUM_CTA FROM {0} WHERE" +
                            " Num_Cta = '{1}' AND Ejercicio = {2} AND CCostos = {3}" +
                            " AND CGrupos = {4}", SaldoCcNombre, sd.Num_Cta, ejrc, sd.CentroCosto, sd.CentroGrupo);

                        bool existe = false;
                        DataSet dts = datos.LeeDatos(sqlup);
                        if (dts != null && dts.Tables != null && dts.Tables.Count > 0
                            && dts.Tables[0].Rows.Count > 0)
                        {
                            existe = true;
                        }

                        if (sd.Num_Cta.Contains("104000201100000000003"))
                            LogDebug.Escribe("Existe");

                        if (!existe)
                        {
                            sqlup = string.Format("INSERT INTO {0} " +
                                "(NUM_CTA, EJERCICIO, CCOSTOS, CGRUPOS) VALUES ('{1}', {2}, {3}, {4});",
                                SaldoCcNombre, sd.Num_Cta, ejrc, sd.CentroCosto, sd.CentroGrupo);
                            datos.EjecutaSentencia(sqlup, parames);
                        }

                        sqlup = string.Format("UPDATE {0} SET CARGO{7} = {1}, ABONO{7} = {2}" +
                            "WHERE Num_Cta = '{3}' AND Ejercicio = {4} AND CCostos = {5} AND CGrupos = {6}",
                            SaldoCcNombre, sd.Cargo01, sd.Abono01, sd.Num_Cta, ejrc, sd.CentroCosto,
                            sd.CentroGrupo, prd.ToString().PadLeft(2, '0'));

                        if (sd.Cargo01 < 0 || sd.Abono01 < 0)
                            existe = false;

                        datos.EjecutaSentencia(sqlup, parames);
                    }
                }
            }
        }

        private static void UpdSldIni(string ruta, int ejrc, int emprs)
        {
            if (string.IsNullOrEmpty(ruta))
            {
                throw new Exception("Falta la ruta de datos");
            }
            else
            {
                Ejercicio = (ejrc - 1).ToString().Substring(2);
                FireBird datos = new FireBird();
                FireBird datupd = new FireBird();
                IDataParameter[] parames = null;

                ruta = rutaEmprs(ruta, emprs.ToString());
                (datos.CadenaConn as FbConnectionStringBuilder).Database = ruta;
                (datupd.CadenaConn as FbConnectionStringBuilder).Database = ruta;

                string sql = string.Format("SELECT num_cta, ejercicio" +
                    ", ccostos, cgrupos" +
                    ", inicial" +
                    ", cargo01, abono01 " +
                    ", cargo02, abono02 " +
                    ", cargo03, abono03 " +
                    ", cargo04, abono04 " +
                    ", cargo05, abono05 " +
                    ", cargo06, abono06 " +
                    ", cargo07, abono07 " +
                    ", cargo08, abono08 " +
                    ", cargo09, abono09 " +
                    ", cargo10, abono10 " +
                    ", cargo11, abono11 " +
                    ", cargo12, abono12 " +
                    " FROM {0}" +
                    " WHERE Ejercicio = {1}",
                    SaldoCcNombre, ejrc-1);

                using (new CWaitCursor())
                {
                    FbDataReader reader = datos.LeeDatosOnly(sql) as FbDataReader;
                    string sqlup;

                    Ejercicio = ejrc.ToString().Substring(2);
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            double ini = reader.GetDouble(4);

                            string cta = reader.GetString(0);
                            int cc = reader.GetInt16(2);
                            int cg = reader.GetInt16(3);

                            double crg = 0;
                            double abn = 0;

                            /*
                            if (cta == "101000000000000000001")
                                crg = 1; */
                            for (int i = 5; i <= 27; i+=2)
                            {
                                crg = reader.GetDouble(i);
                                abn = reader.GetDouble(i+1);

                                ini = ini + crg - abn;
                            }


                            sqlup = string.Format("SELECT NUM_CTA FROM {0} WHERE" +
                                " Num_Cta = '{1}' AND Ejercicio = {2} AND CCostos = {3}" +
                                " AND CGrupos = {4}", SaldoCcNombre, cta, ejrc, cc, cg);

                            bool existe = false;
                            DataSet dts = datupd.LeeDatos(sqlup);

                            existe = dts != null && dts.Tables != null && dts.Tables.Count > 0
                                && dts.Tables[0].Rows.Count > 0;


                            if (!existe)
                            {
                                sqlup = string.Format("INSERT INTO {0}" +
                                    " (NUM_CTA, EJERCICIO, CCOSTOS, CGRUPOS)" +
                                    " VALUES ('{1}', {2}, {3}, {4});",
                                    SaldoCcNombre, cta, ejrc, cc, cg);

                                datupd.EjecutaSentencia(sqlup, parames);
                            }

                            sqlup = string.Format("UPDATE {0} SET INICIAL = {1}" +
                                " WHERE Num_Cta = '{2}' AND Ejercicio = {3}" +
                                " AND CCostos = {4} AND CGrupos = {5}",
                                SaldoCcNombre, ini, cta, ejrc, cc, cg);

                            datupd.EjecutaSentencia(sqlup, parames);
                        }
                    }
                    reader.Close();
                }
            }
        }

        private static void UpSldsCicl(string cta, int cc, string dh, double mnt, int cg)
        {
            SaldoI busq = SldosI.Find(delegate (SaldoI c)
            { return c.Num_Cta == cta && c.CentroCosto == cc && c.CentroGrupo == cg; });

            if (busq != null)
            {
                if (dh == "D")
                    busq.Cargo01 += mnt;
                else
                    busq.Abono01 += mnt;
            }
            else
            {
                SaldoI nva = new SaldoI();
                nva.Num_Cta = cta;
                nva.CentroCosto = cc;
                nva.CentroGrupo = cg;

                if (dh == "D")
                    nva.Cargo01 = mnt;
                else
                    nva.Abono01 = mnt;

                SldosI.Add(nva);
            }


            Cuenta ctta = ListaCtas.Find(delegate (Cuenta c) { return c.Numero == cta; });
            if (ctta.PapaNumero != "-1")
                UpSldsCicl(ctta.PapaNumero, cc, dh, mnt, cg);
        }

        private static string Ejercicio;
        private static int NmrEmprs;
        static private string ProductoNombre
        {
            get { return string.Format("inve{0}", NmrEmprs.ToString().PadLeft(2, '0')); }
        }

        static private string FacturaNombre
        {
            get { return string.Format("factf{0}", NmrEmprs.ToString().PadLeft(2, '0')); }
        }

        static private string ClienteNombre
        {
            get { return string.Format("clie{0}", NmrEmprs.ToString().PadLeft(2, '0')); }
        }

        static private string EnvioNombre
        {
            get { return string.Format("infenvio{0}", NmrEmprs.ToString().PadLeft(2, '0')); }
        }

        static private string CuentaNombre
        {
            get { return string.Format("cuentas{0}", Ejercicio); }
        }

        static private string SaldoNombre
        {
            get { return string.Format("saldos{0}", Ejercicio); }
        }

        static private string SaldoCcNombre
        {
            get { return string.Format("saldosCc{0}", Ejercicio); }
        }

        static private string RubroNombre
        {
            get { return "ctarub"; }
        }

        static private string AuxiliarNombre
        {
            get { return string.Format("auxiliar{0}", Ejercicio); }
        }

        static private string CuentasNombre
        {
            get { return $"cuen_m{NmrEmprs.ToString().PadLeft(2, '0')}"; }
        }

        static private string DetalleCuentasNombre
        {
            get { return $"cuen_det{NmrEmprs.ToString().PadLeft(2, '0')}"; }
        }


        private static string NombreEmpresa(int nE)
        {
            List<string> le = new List<string>();

            le.Add("COCOLAB INTERNATIONAL, S.A.DE C.V.");
            le.Add("PIPELINE STUDIO, S.A.DE C.V.");
            le.Add("MAY CONTENT FOR PEOPLE, S.A.DEC.V.");
            le.Add("MAPA E IDEAS, S.A.DE C.V.");
            le.Add("SOFT POPPLER, S.A.DE C.V.");
            le.Add("HOT PIXEL INTERACTIVE, S.A.DE C.V.");
            le.Add("SYNK POINK, S.A.DE C.V.");
            le.Add("POINK TECHNOLOGIES, S.A.DE C.V.");
            le.Add("MODERN MEDIA TECHNOLOGIES S.A.DE C.V.");
            le.Add("TOUR YOURSELF S.A.DE C.V.");
            le.Add("BOLOMCHON, S.A.DE C.V.");

            if (nE >= 19 && nE <= 29)
                return le[nE - 19];
            else
                return "EMPRESA INVALIDA SA DE CV";
        }
    }


    public enum EQuerySAE
    {
        Productos,
        Movimientos,
        Ventas,
        Vendedores,
        VendedoresSaldos
    }

    public enum EQueryCOI
    {
        Cuentas,
        Polizas,
        EstadoR,
        BalanzaC,
        Auxiliar,
        BalanzaG,
        SaldoInicial
    }
}
