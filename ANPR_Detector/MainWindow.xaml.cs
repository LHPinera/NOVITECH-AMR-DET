using System;
using MahApps.Metro.Controls;
using App.Deal;
using System.Windows.Threading;
using System.Threading;
using NetSDKCS;
using System.Windows.Controls;
using System.Windows;
using App.Entity;
using System.Linq;
using System.Diagnostics;
using System.Configuration;

namespace IntelligentTraffic
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    /// 
      
    public partial class MainWindow : MetroWindow
    {
        public static MainWindow f1;
   
        public MainWindow()
        {
            InitializeComponent();
            MainWindow.f1 = this;
        }

        int cantidad = 0;
        private void MetroWindow_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            #region Conectar procesos Dahua
            Dahua.DahuaAnalisis dahua = new Dahua.DahuaAnalisis();
            m_AnalyzerDataCallBack = new fAnalyzerDataCallBack(dahua.AnalyzerDataCallBack);

            try
            {
                NETClient.Init(m_DisConnectCallBack, IntPtr.Zero, null);
                NETClient.SetAutoReconnect(m_ReConnectCallBack, IntPtr.Zero);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Process.GetCurrentProcess().Kill();
            }
            #endregion

            #region Conectar procesos Hikvision
            string inifile = "";
            inifile = System.Windows.Forms.Application.StartupPath + "\\Config.ini";
            Hikvision.CHCNetSDK.ProtocolType = Hikvision.CHCNetSDK.ReadIniData("Protocol", "ProtocolType", Hikvision.CHCNetSDK.ProtocolType, inifile);

            Hikvision.ConnectHKV connect = new Hikvision.ConnectHKV();
            connect.SDK_Init();

            #endregion

            #region Iniciar IP´s
            GetIp();
            #endregion

            #region Iniciar Hilo
            DispatcherTimer dispatcher = new DispatcherTimer
            {
                Interval = new TimeSpan(0, 0, 30)
            };

            dispatcher.Tick += (s, a) =>
            {
                Thread hilo = new Thread(IniciarHilo);
                hilo.Start();
            };

            dispatcher.Start();
            #endregion
        }

        private void IniciarHilo()
        {
            CdeAlerta.Actualizar();
        }

        private void CtrIp_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            AddIP agrIp = new AddIP();
            agrIp.ShowDialog();
        }

        #region Iniciar IP registradas

        private static fDisConnectCallBack m_DisConnectCallBack;
        private static fHaveReConnectCallBack m_ReConnectCallBack;
        private static fAnalyzerDataCallBack m_AnalyzerDataCallBack;
        IntPtr loginID = IntPtr.Zero;

        public Hikvision.CHCNetSDK.STRU_DEVICE_INFO[] g_struDeviceInfo = new Hikvision.CHCNetSDK.STRU_DEVICE_INFO[Hikvision.CHCNetSDK.MAX_DEVICES];

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
                LoginID = login
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

        private void GetIp()
        {
            foreach (var lst in CdeAlerta.GetConfigs())
            {
                switch (lst.Config_Marca)
                {
                    case "HIKVISION":
                        {
                            bool respuesta = AgrHikvison(lst.Config_IP, lst.Config_Puerto, lst.Config_Usuario, lst.Config_Clave, contamax);

                            if (respuesta)
                            {
                                bool agr = AgrListaInfoHKV(lst.Config_IP, lst.Config_Puerto, lst.Config_Usuario, lst.Config_Clave);
                                //Agregar
                                if (Lista.lstInfo.Any() && agr)
                                {
                                    TreeViewItem nodoHKV = new TreeViewItem
                                    {
                                        Header = lst.Config_IP,
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

                        }
                        break;
                    case "DAHUA":
                        {
                            IntPtr login = AgrDahua(lst.Config_IP, lst.Config_Puerto, lst.Config_Usuario, lst.Config_Clave);

                            if (login != IntPtr.Zero)
                            {
                                bool agr = AgrListaInfoDHA(login, lst.Config_IP, lst.Config_Puerto, lst.Config_Usuario, lst.Config_Clave);

                                //Agregar
                                TreeViewItem nodo = new TreeViewItem();
                                if (Lista.lstInfo.Any() && agr)
                                {
                                    nodo.Header = lst.Config_IP;
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
        }

        private void ctrIniciar_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }
        #endregion
    }
}