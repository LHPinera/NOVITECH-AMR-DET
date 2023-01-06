using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using App.Entity;
using System.Configuration;

namespace App.Data
{
    public class CdAlerta
    {        
        public void Insertar(int id, string nombre, string ip, string placa, byte[] imgplaca, byte[] imgauto, float latitud, float longitud, float velocidad, string arco)
        {
            int respuesta = 0;

            if (!placa.Contains("NoPlate"))
            {
                InsertarLocal(id, nombre, ip, placa, imgplaca, imgauto, latitud, longitud, velocidad, arco);
            }

            //respuesta = InsertarNube(id, nombre, ip, placa, imgplaca, imgauto, latitud, longitud, velocidad, arco);

            //if (respuesta == 1)
            //{
            //    UpdateLocal(placa);
            //}           
        }

        public int InsertarNube(string id, string nombre, string ip, string placa, byte[] imgplaca, byte[] imgauto, float latitud, float longitud, float velocidad, string arco)
        {
            int bandera = 0;

            //try
            //{
            //    using (var con = new SqlConnection(Conection.GetConNube()))
            //    {
            //        con.Open();

            //        var cmd = con.CreateCommand();
            //        cmd.CommandText = " INSERT INTO Hits "
            //                        + " (Hit_Disp_Id, Hit_Disp_Nombre, Hit_Disp_IP, Hit_Placa, Hit_Placa_img, Hit_Imagen, Hit_Fecha, Hit_Latitud, Hit_Longitud, Hit_Velocidad, Hit_Arco, Hit_Carril, Hit_Sentido, Hit_Procesado) "
            //                        + " VALUES(@ID, @Nombre, @IP, @Placa, @ImgPlaca, @ImgAuto, @Fecha, @Latitud, @Longitud, @Velocidad, @Arco, @Carril, @Sentido, @Procesado)";
            //        cmd.Parameters.Add("@ID",       SqlDbType.VarChar, 50).Value = id;
            //        cmd.Parameters.Add("@Nombre",   SqlDbType.VarChar, 100).Value = nombre;
            //        cmd.Parameters.Add("@IP",       SqlDbType.VarChar, 50).Value = ip;
            //        cmd.Parameters.Add("@Placa",    SqlDbType.VarChar, 250).Value = placa;
            //        cmd.Parameters.Add("@ImgPlaca", SqlDbType.VarBinary).Value = imgplaca;
            //        cmd.Parameters.Add("@ImgAuto",  SqlDbType.VarBinary).Value = imgauto;
            //        cmd.Parameters.Add("@Fecha",    SqlDbType.DateTime).Value = DateTime.Now;

            //        if (!string.IsNullOrEmpty(latitud.ToString()))
            //        {
            //            cmd.Parameters.Add("@Latitud", SqlDbType.Float).Value = latitud;
            //        }
            //        else
            //        {
            //            cmd.Parameters.Add("@Latitud", SqlDbType.Float).Value = 0.0;
            //        }

            //        if (!string.IsNullOrEmpty(latitud.ToString()))
            //        {
            //            cmd.Parameters.Add("@Longitud", SqlDbType.Float).Value = longitud;
            //        }
            //        else
            //        {
            //            cmd.Parameters.Add("@Longitud", SqlDbType.Float).Value = 0.0;
            //        }

            //        if (!string.IsNullOrEmpty(latitud.ToString()))
            //        {
            //            cmd.Parameters.Add("@Velocidad", SqlDbType.Float).Value = velocidad;
            //        }
            //        else
            //        {
            //            cmd.Parameters.Add("@Velocidad", SqlDbType.Float).Value = 0.0;
            //        }

            //        cmd.Parameters.Add("@Arco", SqlDbType.VarChar, 50).Value = "Arco1";
            //        cmd.Parameters.Add("@Carril", SqlDbType.VarChar, 50).Value = "Arco1";
            //        cmd.Parameters.Add("@Sentido", SqlDbType.VarChar, 50).Value = "Arco1";
            //        cmd.Parameters.Add("@Procesado", SqlDbType.Int).Value = 0;

            //        cmd.ExecuteNonQuery();

            //        con.Close();

            //        bandera = 1;
            //    }
            //}
            //catch (SqlException ex)
            //{
            //    bandera = 0;
            //    Console.Write("BD-Nube\n" + ex.ToString());                
            //}

            return bandera;
        }

        public void InsertarLocal(int id, string nombre, string ip, string placa, byte[] imgplaca, byte[] imgauto, float latitud, float longitud, float velocidad, string arco)
        {            
            try
            {
                using (var con = new SqlConnection(Conection.GetConLocal()))
                {
                    con.Open();

                    var cmd = con.CreateCommand();
                    cmd.CommandText = " INSERT INTO Hits "
                                    + " (Hit_Disp_Id, Hit_Disp_Nombre, Hit_Disp_IP, Hit_Placa, Hit_Placa_img, Hit_Imagen, Hit_Fecha, Hit_Latitud, Hit_Longitud, Hit_Velocidad, Hit_Arco, Hit_Carril, Hit_Sentido, Hit_Procesado, Hit_Enviado) "
                                    + " VALUES(@ID, @Nombre, @IP, @Placa, @ImgPlaca, @ImgAuto, @Fecha, @Latitud, @Longitud, @Velocidad, @Arco, @Carril, @Sentido, @Procesado, @Enviado)";
                    cmd.Parameters.Add("@ID", SqlDbType.VarChar, 50).Value = id;
                    cmd.Parameters.Add("@Nombre", SqlDbType.VarChar, 100).Value = nombre;
                    cmd.Parameters.Add("@IP", SqlDbType.VarChar, 50).Value = ip;
                    cmd.Parameters.Add("@Placa", SqlDbType.VarChar, 250).Value = placa;
                    cmd.Parameters.Add("@ImgPlaca", SqlDbType.VarBinary).Value = imgplaca;
                    cmd.Parameters.Add("@ImgAuto", SqlDbType.VarBinary).Value = imgauto;
                    cmd.Parameters.Add("@Fecha", SqlDbType.DateTime).Value = DateTime.Now;

                    if (!string.IsNullOrEmpty(latitud.ToString()))
                    {
                        cmd.Parameters.Add("@Latitud", SqlDbType.Float).Value = latitud;
                    }
                    else
                    {
                        cmd.Parameters.Add("@Latitud", SqlDbType.Float).Value = 0.0;
                    }

                    if (!string.IsNullOrEmpty(latitud.ToString()))
                    {
                        cmd.Parameters.Add("@Longitud", SqlDbType.Float).Value = longitud;
                    }
                    else
                    {
                        cmd.Parameters.Add("@Longitud", SqlDbType.Float).Value = 0.0;
                    }

                    if (!string.IsNullOrEmpty(latitud.ToString()))
                    {
                        cmd.Parameters.Add("@Velocidad", SqlDbType.Float).Value = velocidad;
                    }
                    else
                    {
                        cmd.Parameters.Add("@Velocidad", SqlDbType.Float).Value = 0.0;
                    }
                    var appSettings = ConfigurationManager.AppSettings;

                    cmd.Parameters.Add("@Arco", SqlDbType.VarChar, 50).Value = appSettings["Arco"];
                    cmd.Parameters.Add("@Carril", SqlDbType.VarChar, 50).Value = appSettings["Carril"];
                    cmd.Parameters.Add("@Sentido", SqlDbType.VarChar, 50).Value = appSettings["Sentido"];
                    cmd.Parameters.Add("@Procesado", SqlDbType.Int).Value = 0;
                    cmd.Parameters.Add("@Enviado", SqlDbType.Int).Value = 0;

                    cmd.ExecuteNonQuery();

                    con.Close();
                }
            }
            catch (SqlException ex)
            {
                Console.Write("BD-Local\n" + ex.ToString());
            }
        }

        public void InsertDb()
        {
            int respuesta = 0;
            string ID = null, Nombre = null, IP = null, Placa = null, Fecha = null, Arco = null, Carril = null, Sentido = null;
            byte[] ImgPlaca = null, ImgAuto = null;
            float Latitud = 0, Longitud = 0, Velocidad = 0;

            try
            {
                using (var con = new SqlConnection(Conection.GetConLocal()))
                {
                    con.Open();

                    var cmd = con.CreateCommand();
                    cmd.CommandText = " SELECT * FROM Hits WHERE Hit_Enviado = 0 ";
                    var ready = cmd.ExecuteReader();

                    //Lista.lstAlocal.Clear();

                    while (ready.Read())
                    {
                        ID        = Convert.ToString(ready["Hit_Disp_Id"]);
                        Nombre    = Convert.ToString(ready["Hit_Disp_Nombre"]);
                        IP        = Convert.ToString(ready["Hit_Disp_IP"]);
                        Placa     = Convert.ToString(ready["Hit_Placa"]);
                        ImgPlaca  = (byte[])(ready["Hit_Placa_img"]);
                        ImgAuto   = (byte[])(ready["Hit_Imagen"]);
                        Fecha     = Convert.ToDateTime(ready["Hit_Fecha"]).ToString("yyyy-MM-dd HH:mm:ss");
                        Latitud   = Convert.ToSingle(ready["Hit_Latitud"]);
                        Longitud  = Convert.ToSingle(ready["Hit_Longitud"]);
                        Velocidad = Convert.ToSingle(ready["Hit_Velocidad"]);
                        Arco      = Convert.ToString(ready["Hit_Arco"]);
                        Carril    = Convert.ToString(ready["Hit_Carril"]);
                        Sentido   = Convert.ToString(ready["Hit_Sentido"]);

                        respuesta = InsertarNube(ID, Nombre, IP, Placa, ImgPlaca, ImgAuto, Latitud, Longitud, Velocidad, Arco);

                        if (respuesta == 1)
                        {
                            UpdateLocal(Placa);
                        }
                    }

                    con.Close();
                }                
            }
            catch (SqlException ex)
            {
                Console.Write("BDL-Query\n" + ex.ToString());
                //Lista.lstAlocal.Clear();
            }
        }
        
        public void UpdateLocal(string placa)
        {
            try
            {
                using (var con = new SqlConnection(Conection.GetConLocal()))
                {
                    con.Open();

                    var cmd = con.CreateCommand();
                    cmd.CommandText = " UPDATE Hits SET Hit_Enviado = 1 WHERE Hit_Placa = @Placa";                    
                    cmd.Parameters.Add("@Placa", SqlDbType.VarChar).Value = placa;
                    cmd.ExecuteNonQuery();

                    con.Close();
                }
            }
            catch (SqlException ex)
            {
                Console.Write("BDUpdate-Local\n" + ex.ToString());
            }
        }
          
        public List<ceCamara> GetCamNube()
        {
            List<ceCamara> lista = new List<ceCamara>();

            try
            {
                using (var con = new SqlConnection(Conection.GetConNube()))
                {
                    con.Open();
                    var cmd = con.CreateCommand();
                    cmd.CommandText = " SELECT * FROM Camaras ";
                    var ready = cmd.ExecuteReader();

                    while (ready.Read())
                    {
                        var obj = new ceCamara
                        {
                            In_Mac      = Convert.ToString(ready["Cam_Mac"]),
                            In_Nombre   = Convert.ToString(ready["Cam_Nombre"]),
                            In_Latitud  = Convert.ToSingle(ready["Cam_Latitud"]),
                            In_Longitud = Convert.ToSingle(ready["Cam_Longitud"]),
                            In_Arco     = Convert.ToString(ready["Cam_Arco"]),
                            In_Carril   = Convert.ToString(ready["Cam_Carril"]),
                            In_Sentido  = Convert.ToString(ready["Cam_Sentido"])
                        };
                        lista.Add(obj);
                    }
                    con.Close();
                }
            }
            catch (SqlException ex)
            {
                Console.Write("BDMac-Nube\n" + ex.ToString());
                lista.Clear();
            }

            return lista;
        }

        public List<ceCamara> GetCamLocal()
        {
            List<ceCamara> lista = new List<ceCamara>();

            try
            {
                using (var con = new SqlConnection(Conection.GetConLocal()))
                {
                    con.Open();
                    var cmd = con.CreateCommand();
                    cmd.CommandText = " SELECT * FROM Camaras ";
                    var ready = cmd.ExecuteReader();

                    while (ready.Read())
                    {
                        var obj = new ceCamara
                        {
                            In_Id       = Convert.ToInt32(ready["Cam_Id"]),
                            In_Mac      = Convert.ToString(ready["Cam_Mac"]),
                            In_Nombre   = Convert.ToString(ready["Cam_Nombre"]),
                            In_Latitud  = Convert.ToSingle(ready["Cam_Latitud"]),
                            In_Longitud = Convert.ToSingle(ready["Cam_Longitud"]),
                            In_Arco     = Convert.ToString(ready["Cam_Arco"]),
                            In_Carril   = Convert.ToString(ready["Cam_Carril"]),
                            In_Sentido  = Convert.ToString(ready["Cam_Sentido"])
                        };
                        lista.Add(obj);
                    }
                    con.Close();
                }
            }
            catch (SqlException ex)
            {
                Console.Write("DBMac-Local\n" + ex.ToString());
                lista.Clear();
            }

            return lista;
        }

        public void InsertarConfiguracion(string marca, string ip, string puerto, string usuario, string clave)
        {
            try
            {
                using (var con = new SqlConnection(Conection.GetConLocal()))
                {
                    con.Open();

                    var cmd = con.CreateCommand();
                    cmd.CommandText = "IF NOT EXISTS(SELECT Config_IP FROM Config_CAM WHERE Config_IP ='" + ip +"')" 
                                    + " BEGIN" 
                                    + " INSERT INTO Config_CAM "
                                    + " (Config_Marca, Config_IP, Config_Puerto, Config_Usuario, Config_Clave) "
                                    + " VALUES(@Marca, @IP, @Puerto, @Usuario, @Clave)" 
                                    + " END";
                    cmd.Parameters.Add("@Marca",    SqlDbType.VarChar, 100).Value = marca;
                    cmd.Parameters.Add("@IP",       SqlDbType.VarChar, 100).Value = ip;
                    cmd.Parameters.Add("@Puerto",   SqlDbType.Int).Value = puerto;
                    cmd.Parameters.Add("@Usuario",  SqlDbType.VarChar, 100).Value = usuario;
                    cmd.Parameters.Add("@Clave",    SqlDbType.VarChar, 200).Value = clave;
                    cmd.ExecuteNonQuery();

                    con.Close();                   
                }
            }
            catch (SqlException ex)
            {
                Console.Write("BD-Configuracion\n" + ex.ToString());
            }
        }

        public List<ceConfig> GetConfig()
        {
            List<ceConfig> lista = new List<ceConfig>();

            try
            {
                using (var con = new SqlConnection(Conection.GetConLocal()))
                {
                    con.Open();

                    var cmd = con.CreateCommand();
                    cmd.CommandText = " SELECT * FROM Config_CAM ";
                    var ready = cmd.ExecuteReader();

                    while(ready.Read())
                    {
                        var l1 = new ceConfig
                        {
                            Config_Marca    = Convert.ToString(ready["Config_Marca"]),
                            Config_IP       = Convert.ToString(ready["Config_IP"]),
                            Config_Puerto   = Convert.ToString(ready["Config_Puerto"]),
                            Config_Usuario  = Convert.ToString(ready["Config_Usuario"]),
                            Config_Clave    = Convert.ToString(ready["Config_Clave"])
                        };

                        lista.Add(l1);
                    }
                    con.Close();
                }
            }
            catch (SqlException ex)
            {
                Console.Write("BD-GetConfiguracion\n" + ex.ToString());
            }

            return lista;
        }

    }
}
