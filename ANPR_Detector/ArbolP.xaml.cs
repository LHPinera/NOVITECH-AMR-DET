using App.Data;
using App.Entity;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace IntelligentTraffic
{
    public partial class ArbolP : UserControl
    {
        public static ArbolP control2;

        public Hikvision.CHCNetSDK.STRU_DEVICE_INFO[] g_struDeviceInfo = new Hikvision.CHCNetSDK.STRU_DEVICE_INFO[Hikvision.CHCNetSDK.MAX_DEVICES];
        public bool g_bTreenodeChange = false;
        private int m_iCurDeviceIndex = -1;
        private int m_iCurChanIndex = -1;        
        private static ArbolP g_DeviceTree = new ArbolP();

        public class OSDTypeInfo
        {
            public string typeName;
            public bool enabled;
            public int index;
        }
        public List<OSDTypeInfo> struOSDTypeInfo = new List<OSDTypeInfo>();

        public ArbolP()
        {
            InitializeComponent();
        }                

        public static ArbolP Instance()
        {
            CdAlerta dato = new CdAlerta();
            int cantidad  = dato.GetConfig().Count;

            for (int i = 0; i < g_DeviceTree.g_struDeviceInfo.Length && i < cantidad; i++)
            {
                if (g_DeviceTree.g_struDeviceInfo[i].bInit)
                {
                    break;
                }
                g_DeviceTree.g_struDeviceInfo[i].Init();
                g_DeviceTree.g_struDeviceInfo[i].pStruChanInfo = new Hikvision.CHCNetSDK.STRU_CHANNEL_INFO[Hikvision.CHCNetSDK.MAX_CHANNUM_V40];
                g_DeviceTree.g_struDeviceInfo[i].struZeroChan = new Hikvision.CHCNetSDK.STRU_CHANNEL_INFO[16];
                g_DeviceTree.g_struDeviceInfo[i].struMirrorChan = new Hikvision.CHCNetSDK.STRU_CHANNEL_INFO[16];
                for (int j = 0; j < Hikvision.CHCNetSDK.MAX_CHANNUM_V40; j++)
                {
                    g_DeviceTree.g_struDeviceInfo[i].pStruChanInfo[j].init();
                }
                for (int j = 0; j < 16; j++)
                {
                    g_DeviceTree.g_struDeviceInfo[i].struZeroChan[j].init();
                    g_DeviceTree.g_struDeviceInfo[i].struMirrorChan[j].init();
                }
                g_DeviceTree.g_struDeviceInfo[i].struPassiveDecode = new Hikvision.CHCNetSDK.PASSIVEDECODE_CHANINFO[256];
                for (int j = 0; j < 256; j++)
                {
                    g_DeviceTree.g_struDeviceInfo[i].struPassiveDecode[j].init();
                }                
            }

            return g_DeviceTree;
        }

        public int GetCurlChannel()
        {
            if (m_iCurChanIndex >= 0 && m_iCurChanIndex < Hikvision.CHCNetSDK.MAX_CHANNUM_V40)
            {
                if (m_iCurDeviceIndex >= 0 || m_iCurDeviceIndex < Hikvision.CHCNetSDK.MAX_DEVICES)
                {
                    return g_struDeviceInfo[m_iCurDeviceIndex].pStruChanInfo[m_iCurChanIndex].iChannelNO;
                }

            }
            return -1;
        }

        public String GetCurDeviceIp()
        {
            if (m_iCurDeviceIndex < 0 || m_iCurDeviceIndex > Hikvision.CHCNetSDK.MAX_DEVICES)
            {
                return null;
            }
            return g_struDeviceInfo[m_iCurDeviceIndex].chDeviceIP;
        }

        public long GetCurLoginID()
        {
            if (m_iCurDeviceIndex < 0 || m_iCurDeviceIndex > Hikvision.CHCNetSDK.MAX_DEVICES)
            {
                return -1;
            }
            return g_struDeviceInfo[m_iCurDeviceIndex].lLoginID;
        }

        public String GetCurLocalNodeName()
        {
            if (m_iCurDeviceIndex < 0 || m_iCurDeviceIndex > Hikvision.CHCNetSDK.MAX_DEVICES)
            {
                return null;
            }
            return g_struDeviceInfo[m_iCurDeviceIndex].chLocalNodeName;
        }

        public int GetCurDeviceIndex()
        {
            if (m_iCurDeviceIndex < 0 || m_iCurDeviceIndex > Hikvision.CHCNetSDK.MAX_DEVICES)
            {
                return 0;
            }

            return m_iCurDeviceIndex;
        }

        public int GetCurChanIndex()
        {
            if (m_iCurDeviceIndex <= 0 || m_iCurDeviceIndex > Hikvision.CHCNetSDK.MAX_DEVICES)
            {
                return 0;
            }
            return m_iCurChanIndex;
        }

        public int GetCurChanNo()
        {
            if (m_iCurChanIndex < 0 || m_iCurChanIndex > Hikvision.CHCNetSDK.MAX_CHANNUM_V40)
            {
                if (m_iCurDeviceIndex < 0 || m_iCurDeviceIndex > Hikvision.CHCNetSDK.MAX_DEVICES)
                {
                    return -1;
                }
                if (g_struDeviceInfo[m_iCurDeviceIndex].lLoginID >= 0)
                {
                    return 1;
                }
                return -1;
            }
            return g_struDeviceInfo[m_iCurDeviceIndex].pStruChanInfo[m_iCurChanIndex].iChannelNO;
        }

        public int GetNoNode()
        {
            int cantidad = 0;

            cantidad = listIp.Items.Count;

            return cantidad;
        }

        public String GetCurChanName()
        {
            if (m_iCurChanIndex >= 0 && m_iCurChanIndex < Hikvision.CHCNetSDK.MAX_CHANNUM_V40)
            {
                if (m_iCurDeviceIndex >= 0 || m_iCurDeviceIndex < Hikvision.CHCNetSDK.MAX_DEVICES)
                {
                    return g_struDeviceInfo[m_iCurDeviceIndex].pStruChanInfo[m_iCurChanIndex].chChanName;
                }
            }
            return null;
        }

        public int GetCurPreChanNo(int index)
        {
            if (m_iCurChanIndex < 0 || m_iCurChanIndex > Hikvision.CHCNetSDK.MAX_CHANNUM_V40)
            {
                if (m_iCurDeviceIndex < 0 || m_iCurDeviceIndex > Hikvision.CHCNetSDK.MAX_DEVICES)
                {
                    return -1;
                }
                return -1;
            }//m_iCurDeviceIndex m_iCurChanIndex
            return g_struDeviceInfo[index].pStruChanInfo[index].iChannelNO;
        }

        public int SetCurRealHandle(Int32 lRealHandle)
        {
            if (m_iCurChanIndex >= 0 && m_iCurChanIndex < Hikvision.CHCNetSDK.MAX_CHANNUM_V40)
            {
                if (m_iCurDeviceIndex >= 0 || m_iCurDeviceIndex < Hikvision.CHCNetSDK.MAX_DEVICES)
                {
                    g_struDeviceInfo[m_iCurDeviceIndex].pStruChanInfo[m_iCurChanIndex].lRealHandle = lRealHandle;
                    return lRealHandle;
                }
            }
            return -1;
        }

        public long GetCurRealHandle()
        {
            if (m_iCurChanIndex >= 0 && m_iCurChanIndex < Hikvision.CHCNetSDK.MAX_CHANNUM_V40)
            {
                if (m_iCurDeviceIndex >= 0 || m_iCurDeviceIndex < Hikvision.CHCNetSDK.MAX_DEVICES)
                {
                    return g_struDeviceInfo[m_iCurDeviceIndex].pStruChanInfo[m_iCurChanIndex].lRealHandle;
                }
            }
            return -1;
        }

        public Hikvision.CHCNetSDK.STRU_DEVICE_INFO GetCurDeviceInfo()
        {
            if (m_iCurDeviceIndex < 0 || m_iCurDeviceIndex > Hikvision.CHCNetSDK.MAX_DEVICES)
            {
                return g_struDeviceInfo[0];
            }
            return g_struDeviceInfo[m_iCurDeviceIndex];
        }

        public Hikvision.CHCNetSDK.STRU_DEVICE_INFO GetCurDeviceInfobyIndex(int iDeviceIndex)
        {
            if (iDeviceIndex < 0 || iDeviceIndex > Hikvision.CHCNetSDK.MAX_DEVICES)
            {
                return g_struDeviceInfo[0];
            }
            return g_struDeviceInfo[iDeviceIndex];
        }

        public Hikvision.CHCNetSDK.STRU_CHANNEL_INFO GetCurChanInfo()
        {
            if (m_iCurChanIndex < 0 || m_iCurChanIndex > Hikvision.CHCNetSDK.MAX_CHANNUM_V40)
            {
                if (m_iCurDeviceIndex < 0 || m_iCurDeviceIndex > Hikvision.CHCNetSDK.MAX_DEVICES)
                {
                    return g_struDeviceInfo[0].pStruChanInfo[0];
                }
                return g_struDeviceInfo[m_iCurDeviceIndex].pStruChanInfo[0];
            }
            return g_struDeviceInfo[m_iCurDeviceIndex].pStruChanInfo[m_iCurChanIndex];
        }

        public Hikvision.CHCNetSDK.STRU_CHANNEL_INFO GetCurChanInfoByIndex(int iChanIndex)
        {
            if (iChanIndex < 0 || iChanIndex > Hikvision.CHCNetSDK.MAX_CHANNUM_V40)
            {
                if (m_iCurDeviceIndex < 0 || m_iCurDeviceIndex > Hikvision.CHCNetSDK.MAX_DEVICES)
                {
                    return g_struDeviceInfo[0].pStruChanInfo[0];
                }
                return g_struDeviceInfo[m_iCurDeviceIndex].pStruChanInfo[0];
            }
            return g_struDeviceInfo[m_iCurDeviceIndex].pStruChanInfo[iChanIndex];
        }

        private void treeIps_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                TreeViewItem item = (TreeViewItem)listIp.SelectedItem;

                if (item.Parent.GetType() == typeof(TreeView))
                {
                    //Obtener el padre
                    TreeView parent = (TreeView)item.Parent;
                    int index = parent.Items.IndexOf(item);

                    System.Windows.Forms.DialogResult respuesta = System.Windows.Forms.MessageBox.Show("Desea remover la IP?", "Advertencia", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Warning);

                    if (respuesta == System.Windows.Forms.DialogResult.Yes)
                    {
                        //Quitar el item
                        parent.Items.RemoveAt(index);                        
                    }
                }
            }            
        }
    }
}
