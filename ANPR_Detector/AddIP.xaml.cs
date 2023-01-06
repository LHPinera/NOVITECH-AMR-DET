using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MahApps.Metro.Controls;
using App.Entity;
using App.Deal;
using NetSDKCS;

namespace IntelligentTraffic
{
    /// <summary>
    /// Lógica de interacción para AddIP.xaml
    /// </summary>
    public partial class AddIP : MetroWindow
    {
        public AddIP()
        {
            InitializeComponent();
        }

        private static fDisConnectCallBack m_DisConnectCallBack;
        private static fHaveReConnectCallBack m_ReConnectCallBack;
        private static fAnalyzerDataCallBack m_AnalyzerDataCallBack;
        IntPtr loginID = IntPtr.Zero;

        public Hikvision.CHCNetSDK.STRU_DEVICE_INFO[] g_struDeviceInfo = new Hikvision.CHCNetSDK.STRU_DEVICE_INFO[Hikvision.CHCNetSDK.MAX_DEVICES];
        
        private void ValidarCampos()
        {
            if (!string.IsNullOrEmpty(txtIp.Text) && !string.IsNullOrEmpty(txtPuerto.Text) && !string.IsNullOrEmpty(txtUsuario.Text) && !string.IsNullOrEmpty(txtPassword.Password))
            {
                AddIp(txtIp.Text, txtPuerto.Text, txtUsuario.Text, txtPassword.Password);

                this.Close();
            }
            else
            {
                MessageBox.Show("Hay campos vacios, por favor verifiquelos.");
            }
        }

        private void BtnAgregar_Click(object sender, RoutedEventArgs e)
        {
            ValidarCampos();
        }

        private void TxtPuerto_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            try
            {
                Convert.ToUInt32(e.Text);
                e.Handled = false;
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                e.Handled = true;
            }
        }

        public bool AgrHikvison(string ip, string port, string userName, string password, int contar)
        {
            //Conectar HikVision
            Hikvision.ConnectHKV connect = new Hikvision.ConnectHKV();
            bool respuesta = connect.SDK_Login(true, ip, port, userName, password, contar);

            return respuesta;
        }

        NET_DEVICEINFO_Ex deviceInfo = new NET_DEVICEINFO_Ex();
        public IntPtr AgrDahua(string ip, string port, string userName, string password)
        {
            ushort dev_port = 0;
            dev_port = Convert.ToUInt16(port);

            //Conectar Dahua
            loginID = NETClient.Login(ip, dev_port, userName, password, EM_LOGIN_SPAC_CAP_TYPE.TCP, IntPtr.Zero, ref deviceInfo);

            return loginID;
        }

        public bool AgrListaInfoDHA(IntPtr login, string ip, string port, string userName, string password)
        {
            bool bandera = false;

            var obj = new ceInfoCamara
            {
                IP = ip,
                Puerto = port,
                Usuario = userName.Trim(),
                Password = password.Trim(),
                NumCanales = deviceInfo.nChanNum,
                LoginID   = login
            };

            if (Lista.lstInfo.Any())
            {
                var data = Lista.lstInfo.Where(a => a.IP.Equals(ip));

                if (data.Any())
                {
                    MessageBox.Show("La IP ya fue agregada.");
                }
                else
                {
                    Lista.lstInfo.Add(obj);
                    bandera = true;
                }
            }
            else
            {
                Lista.lstInfo.Add(obj);
                bandera = true;
            }

            return bandera;
        }

        public bool AgrListaInfoHKV(string ip, string port, string userName, string password)
        {
            bool bandera = false;

            var obj = new ceInfoCamara
            {
                IP = ip,
                Puerto = port,
                Usuario = userName.Trim(),
                Password = password.Trim(),
                NumCanales = deviceInfo.nChanNum
            };            

            if (Lista.lstInfo.Any())
            {
                var data = Lista.lstInfo.Where(a => a.IP.Equals(ip));

                if (data.Any())
                {
                    MessageBox.Show("La IP ya fue agregada.");
                }
                else
                {
                    Lista.lstInfo.Add(obj);
                    bandera = true;
                }
            }
            else
            {
                Lista.lstInfo.Add(obj);
                bandera = true;
            }

            return bandera;
        }

        int contamax = 0;

        private void AddIp(string ip, string port, string userName, string password)
        {
            switch(cbMarca.Text)
            {
                case "HIKVISION":
                    {
                        bool respuesta = AgrHikvison(ip, port, userName, password, contamax);

                        if(respuesta)
                        {
                           bool agr = AgrListaInfoHKV(ip, port, userName, password);
                            //Agregar
                            if (Lista.lstInfo.Any() && agr)
                            {
                                TreeViewItem nodoHKV = new TreeViewItem
                                {
                                    Header = ip,
                                    Tag = Lista.lstInfo.Count
                                };

                                for (int i = 0; i < deviceInfo.nChanNum; i++)
                                {
                                    TreeViewItem item = new TreeViewItem
                                    {
                                        Header = "Canal " + i.ToString(),
                                        Tag = i
                                    };

                                    nodoHKV.Items.Add(item);
                                }
                                MainWindow.f1.agrArb.listIp.Items.Add(nodoHKV);

                                Hikvision.HikvisionAnalisis info = new Hikvision.HikvisionAnalisis();
                                
                                info.Inicio(contamax);
                                contamax++;

                                //Insertar Configuración
                                CdeAlerta.InsertarConfigs(cbMarca.Text,ip, port, userName, password);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Error en la conexíon.");
                        }
                    }
                    break;
                case "SAMSUNG":
                    {
                        bool respuesta = Samsung.SamsungAnalisis.ConSamsung(ip, port, userName, password);

                        if(respuesta)
                        {
                            Samsung.ConsultaTagRepuve.Iniciar("");
                        }
                        else
                        {

                        }

                    }
                    break;
                case "DAHUA":
                    {
                        IntPtr login = AgrDahua(ip, port, userName, password);

                        if (login != IntPtr.Zero)
                        {
                            bool agr = AgrListaInfoDHA(login, ip, port, userName, password);

                            //Agregar
                            TreeViewItem nodo = new TreeViewItem();
                            if (Lista.lstInfo.Any() && agr)
                            {
                                nodo.Header = ip;
                                nodo.Tag = Lista.lstInfo.Count;

                                for (int i = 0; i < deviceInfo.nChanNum; i++)
                                {
                                    TreeViewItem item = new TreeViewItem
                                    {
                                        Header = "Canal " + i.ToString(),
                                        Tag = i
                                    };

                                    nodo.Items.Add(item);
                                }
                                MainWindow.f1.agrArb.listIp.Items.Add(nodo);

                                if (nodo.Items.Count > 0)
                                {
                                    Dahua.DahuaAnalisis.Inicio(loginID, m_AnalyzerDataCallBack);
                                }

                                //Insertar Configuración
                              CdeAlerta.InsertarConfigs(cbMarca.Text, ip, port, userName, password);
                            }                            
                        }
                        else
                        {
                            MessageBox.Show("Error en la conexíon.");
                        }
                    }
                    break;
                default:
                    MessageBox.Show("Error D150 - Administrador");
                    break;
            }
            
        }

        List<string> opMarca = new List<string>();
        private void AgrMarca()
        {
            cbMarca.ItemsSource = null;
            opMarca.Add("HIKVISION");
            opMarca.Add("DAHUA");
            opMarca.Add("SAMSUNG");
            cbMarca.ItemsSource = opMarca;
        }
        
        private void AddIP_Loaded(object sender, RoutedEventArgs e)
        {
            //Cargar lista de marcas
            AgrMarca();

            //#region Conectar procesos Dahua
            //Dahua.DahuaAnalisis dahua = new Dahua.DahuaAnalisis();           
            //m_AnalyzerDataCallBack = new fAnalyzerDataCallBack(dahua.AnalyzerDataCallBack);

            //try
            //{
            //    NETClient.Init(m_DisConnectCallBack, IntPtr.Zero, null);
            //    NETClient.SetAutoReconnect(m_ReConnectCallBack, IntPtr.Zero);
                
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //    Process.GetCurrentProcess().Kill();
            //}
            //#endregion

            //#region Conectar procesos Hikvision
            //string inifile = "";
            //inifile = System.Windows.Forms.Application.StartupPath + "\\Config.ini";
            //Hikvision.CHCNetSDK.ProtocolType = Hikvision.CHCNetSDK.ReadIniData("Protocol", "ProtocolType", Hikvision.CHCNetSDK.ProtocolType, inifile);

            //Hikvision.ConnectHKV connect = new Hikvision.ConnectHKV();
            //connect.SDK_Init();
            //#endregion
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CbMarca_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(cbMarca.SelectedValue.ToString()) && cbMarca.SelectedValue.ToString() != "")
            {
                txtIp.IsEnabled = true;
                txtPuerto.IsEnabled = true;
                txtUsuario.IsEnabled = true;
                txtPassword.IsEnabled = true;
                btnAgregar.IsEnabled = true;
            }
        }

        private void TxtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ValidarCampos();
            }
        }
    }
}
