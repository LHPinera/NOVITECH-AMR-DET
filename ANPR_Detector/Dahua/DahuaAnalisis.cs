using System;
using System.Drawing;
using System.Linq;
using NetSDKCS;
using System.Runtime.InteropServices;
using System.IO;
using App.Entity;
using App.Deal;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Drawing.Text;

namespace IntelligentTraffic.Dahua
{
    public class DahuaAnalisis
    {
        private static Int64 m_ID = 1;
        private static IntPtr m_EventID = IntPtr.Zero;

        public static void Inicio(IntPtr m_LoginID, fAnalyzerDataCallBack m_AnalyzerDataCallBack)
        {
            if (Lista.lstInfo.Any())
            {
                for (int i = 0; i < Lista.lstInfo.Count; i++)
                {
                    if (IntPtr.Zero == m_EventID)
                    {
                        m_ID = 1;
                        m_EventID = NETClient.RealLoadPicture(m_LoginID, i, (uint)EM_EVENT_IVS_TYPE.ALL, true, m_AnalyzerDataCallBack, m_LoginID, IntPtr.Zero);
                        if (IntPtr.Zero == m_EventID)
                        {
                            // MessageBox.Show(this, NETClient.GetLastError());
                            return;
                        }
                        else
                            m_EventID = IntPtr.Zero;
                    }
                }
            }
            else
            {

            }
        }

        public delegate string IAsyncResult(Delegate method, params object[] args);

        public int AnalyzerDataCallBack(IntPtr lAnalyzerHandle, uint dwEventType, IntPtr pEventInfo, IntPtr pBuffer, uint dwBufSize, IntPtr dwUser, int nSequence, IntPtr reserved)
        {
            string id = null, ip = null, mac = null, _mac = null, placa = null;
            byte[] imgPlaca = null, imgAuto = null, buffer = null;
            float velocidad = 0;
            uint archivo = 0, info2 = 0;

            EM_EVENT_IVS_TYPE type = (EM_EVENT_IVS_TYPE)dwEventType;

            switch (type)
            {
                case EM_EVENT_IVS_TYPE.TRAFFIC_RUNREDLIGHT:
                    {
                        NET_DEV_EVENT_TRAFFIC_RUNREDLIGHT_INFO info = (NET_DEV_EVENT_TRAFFIC_RUNREDLIGHT_INFO)Marshal.PtrToStructure(pEventInfo, typeof(NET_DEV_EVENT_TRAFFIC_RUNREDLIGHT_INFO));

                        foreach (var nlista in Lista.lstInfo)
                        {
                            if (nlista.LoginID == dwUser)
                            {
                                ip = nlista.IP;
                                _mac = CdeAlerta.GetMacAddress(nlista.IP);
                            }
                        }

                        id = m_ID.ToString();
                        mac = _mac;
                        placa = info.stTrafficCar.szPlateNumber;
                        velocidad = info.stTrafficCar.nSpeed;
                        archivo = info.stuObject.stPicInfo.dwFileLenth;
                        info2 = info.stuObject.stPicInfo.dwOffSet;

                        if (IntPtr.Zero != pBuffer && dwBufSize > 0)
                        {
                            buffer = new byte[dwBufSize];
                            Marshal.Copy(pBuffer, buffer, 0, (int)dwBufSize);
                        }

                        MostrarImg(buffer, archivo, info2, id);

                        imgAuto = _imgAuto;
                        imgPlaca = _imgPlaca;

                        foreach (var item in CdeAlerta.GetCamara())
                        {
                            if (_mac.Equals(item.In_Mac))
                            {
                                CdeAlerta.Insertar(item.In_Id, item.In_Nombre, ip, placa, imgPlaca, imgAuto, item.In_Latitud, item.In_Longitud, velocidad, item.In_Arco);
                            }
                        }

                        m_ID++;
                        break;
                    }
                case EM_EVENT_IVS_TYPE.TRAFFICJUNCTION:
                    {
                        NET_DEV_EVENT_TRAFFICJUNCTION_INFO info = new NET_DEV_EVENT_TRAFFICJUNCTION_INFO();
                        info = (NET_DEV_EVENT_TRAFFICJUNCTION_INFO)Marshal.PtrToStructure(pEventInfo, typeof(NET_DEV_EVENT_TRAFFICJUNCTION_INFO));

                        foreach (var nlista in Lista.lstInfo)
                        {
                            if (nlista.LoginID == dwUser)
                            {
                                ip = nlista.IP;
                                _mac = CdeAlerta.GetMacAddress(nlista.IP);
                            }
                        }

                        id = m_ID.ToString();
                        mac = _mac;
                        placa = info.stTrafficCar.szPlateNumber;
                        velocidad = info.stTrafficCar.nSpeed;
                        archivo = info.stuObject.stPicInfo.dwFileLenth;
                        info2 = info.stuObject.stPicInfo.dwOffSet;

                        if (IntPtr.Zero != pBuffer && dwBufSize > 0)
                        {
                            buffer = new byte[dwBufSize];
                            Marshal.Copy(pBuffer, buffer, 0, (int)dwBufSize);
                        }

                        MostrarImg(buffer, archivo, info2, id);

                        imgAuto = _imgAuto;
                        imgPlaca = _imgPlaca;

                        foreach (var item in CdeAlerta.GetCamara())
                        {
                            if (_mac.Equals(item.In_Mac))
                            {
                                CdeAlerta.Insertar(item.In_Id, item.In_Nombre, ip, placa, imgPlaca, imgAuto, item.In_Latitud, item.In_Longitud, velocidad, item.In_Arco);
                            }
                        }

                        m_ID++;
                        break;
                    }
                case EM_EVENT_IVS_TYPE.TRAFFIC_OVERSPEED:
                    {
                        NET_DEV_EVENT_TRAFFIC_OVERSPEED_INFO info = (NET_DEV_EVENT_TRAFFIC_OVERSPEED_INFO)Marshal.PtrToStructure(pEventInfo, typeof(NET_DEV_EVENT_TRAFFIC_OVERSPEED_INFO));

                        foreach (var nlista in Lista.lstInfo)
                        {
                            if (nlista.LoginID == dwUser)
                            {
                                ip = nlista.IP;
                                _mac = CdeAlerta.GetMacAddress(nlista.IP);
                            }
                        }

                        id = m_ID.ToString();
                        mac = _mac;
                        placa = info.stTrafficCar.szPlateNumber;
                        velocidad = info.stTrafficCar.nSpeed;
                        archivo = info.stuObject.stPicInfo.dwFileLenth;
                        info2 = info.stuObject.stPicInfo.dwOffSet;

                        if (IntPtr.Zero != pBuffer && dwBufSize > 0)
                        {
                            buffer = new byte[dwBufSize];
                            Marshal.Copy(pBuffer, buffer, 0, (int)dwBufSize);
                        }

                        MostrarImg(buffer, archivo, info2, id);

                        imgAuto = _imgAuto;
                        imgPlaca = _imgPlaca;

                        foreach (var item in CdeAlerta.GetCamara())
                        {
                            if (_mac.Equals(item.In_Mac))
                            {
                                CdeAlerta.Insertar(item.In_Id, item.In_Nombre, ip, placa, imgPlaca, imgAuto, item.In_Latitud, item.In_Longitud, velocidad, item.In_Arco);
                            }
                        }

                        m_ID++;
                        break;
                    }
                case EM_EVENT_IVS_TYPE.TRAFFIC_UNDERSPEED:
                    {
                        NET_DEV_EVENT_TRAFFIC_UNDERSPEED_INFO info = (NET_DEV_EVENT_TRAFFIC_UNDERSPEED_INFO)Marshal.PtrToStructure(pEventInfo, typeof(NET_DEV_EVENT_TRAFFIC_UNDERSPEED_INFO));

                        foreach (var nlista in Lista.lstInfo)
                        {
                            if (nlista.LoginID == dwUser)
                            {
                                ip = nlista.IP;
                                _mac = CdeAlerta.GetMacAddress(nlista.IP);
                            }
                        }

                        id = m_ID.ToString();
                        mac = _mac;
                        placa = info.stTrafficCar.szPlateNumber;
                        velocidad = info.stTrafficCar.nSpeed;
                        archivo = info.stuObject.stPicInfo.dwFileLenth;
                        info2 = info.stuObject.stPicInfo.dwOffSet;

                        if (IntPtr.Zero != pBuffer && dwBufSize > 0)
                        {
                            buffer = new byte[dwBufSize];
                            Marshal.Copy(pBuffer, buffer, 0, (int)dwBufSize);
                        }

                        MostrarImg(buffer, archivo, info2, id);

                        imgAuto = _imgAuto;
                        imgPlaca = _imgPlaca;

                        foreach (var item in CdeAlerta.GetCamara())
                        {
                            if (_mac.Equals(item.In_Mac))
                            {
                                CdeAlerta.Insertar(item.In_Id, item.In_Nombre, ip, placa, imgPlaca, imgAuto, item.In_Latitud, item.In_Longitud, velocidad, item.In_Arco);
                            }
                        }

                        m_ID++;
                        break;
                    }
                case EM_EVENT_IVS_TYPE.TRAFFIC_MANUALSNAP:
                    {
                        NET_DEV_EVENT_TRAFFIC_MANUALSNAP_INFO info = (NET_DEV_EVENT_TRAFFIC_MANUALSNAP_INFO)Marshal.PtrToStructure(pEventInfo, typeof(NET_DEV_EVENT_TRAFFIC_MANUALSNAP_INFO));

                        foreach (var nlista in Lista.lstInfo)
                        {
                            if (nlista.LoginID == dwUser)
                            {
                                ip = nlista.IP;
                                _mac = CdeAlerta.GetMacAddress(nlista.IP);
                            }
                        }

                        id = m_ID.ToString();
                        mac = _mac;
                        placa = info.stTrafficCar.szPlateNumber;
                        velocidad = info.stTrafficCar.nSpeed;
                        archivo = info.stuObject.stPicInfo.dwFileLenth;
                        info2 = info.stuObject.stPicInfo.dwOffSet;

                        if (IntPtr.Zero != pBuffer && dwBufSize > 0)
                        {
                            buffer = new byte[dwBufSize];
                            Marshal.Copy(pBuffer, buffer, 0, (int)dwBufSize);
                        }

                        MostrarImg(buffer, archivo, info2, id);

                        imgAuto = _imgAuto;
                        imgPlaca = _imgPlaca;

                        foreach (var item in CdeAlerta.GetCamara())
                        {
                            if (_mac.Equals(item.In_Mac))
                            {
                                CdeAlerta.Insertar(item.In_Id, item.In_Nombre, ip, placa, imgPlaca, imgAuto, item.In_Latitud, item.In_Longitud, velocidad, item.In_Arco);
                            }
                        }

                        m_ID++;
                        break;
                    }
                case EM_EVENT_IVS_TYPE.TRAFFIC_PARKINGSPACEPARKING:
                    {
                        NET_DEV_EVENT_TRAFFIC_PARKINGSPACEPARKING_INFO info = (NET_DEV_EVENT_TRAFFIC_PARKINGSPACEPARKING_INFO)Marshal.PtrToStructure(pEventInfo, typeof(NET_DEV_EVENT_TRAFFIC_PARKINGSPACEPARKING_INFO));

                        foreach (var nlista in Lista.lstInfo)
                        {
                            if (nlista.LoginID == dwUser)
                            {
                                ip = nlista.IP;
                                _mac = CdeAlerta.GetMacAddress(nlista.IP);
                            }
                        }

                        id = m_ID.ToString();
                        mac = _mac;
                        placa = info.stTrafficCar.szPlateNumber;
                        velocidad = info.stTrafficCar.nSpeed;
                        archivo = info.stuObject.stPicInfo.dwFileLenth;
                        info2 = info.stuObject.stPicInfo.dwOffSet;

                        if (IntPtr.Zero != pBuffer && dwBufSize > 0)
                        {
                            buffer = new byte[dwBufSize];
                            Marshal.Copy(pBuffer, buffer, 0, (int)dwBufSize);
                        }

                        MostrarImg(buffer, archivo, info2, id);

                        imgAuto = _imgAuto;
                        imgPlaca = _imgPlaca;

                        foreach (var item in CdeAlerta.GetCamara())
                        {
                            if (_mac.Equals(item.In_Mac))
                            {
                                CdeAlerta.Insertar(item.In_Id, item.In_Nombre, ip, placa, imgPlaca, imgAuto, item.In_Latitud, item.In_Longitud, velocidad, item.In_Arco);
                            }
                        }

                        m_ID++;
                        break;
                    }
                case EM_EVENT_IVS_TYPE.TRAFFIC_PARKINGSPACENOPARKING:
                    {
                        NET_DEV_EVENT_TRAFFIC_PARKINGSPACENOPARKING_INFO info = (NET_DEV_EVENT_TRAFFIC_PARKINGSPACENOPARKING_INFO)Marshal.PtrToStructure(pEventInfo, typeof(NET_DEV_EVENT_TRAFFIC_PARKINGSPACENOPARKING_INFO));

                        foreach (var nlista in Lista.lstInfo)
                        {
                            if (nlista.LoginID == dwUser)
                            {
                                ip = nlista.IP;
                                _mac = CdeAlerta.GetMacAddress(nlista.IP);
                            }
                        }

                        id = m_ID.ToString();
                        mac = _mac;
                        placa = info.stTrafficCar.szPlateNumber;
                        velocidad = info.stTrafficCar.nSpeed;
                        archivo = info.stuObject.stPicInfo.dwFileLenth;
                        info2 = info.stuObject.stPicInfo.dwOffSet;

                        if (IntPtr.Zero != pBuffer && dwBufSize > 0)
                        {
                            buffer = new byte[dwBufSize];
                            Marshal.Copy(pBuffer, buffer, 0, (int)dwBufSize);
                        }

                        MostrarImg(buffer, archivo, info2, id);

                        imgAuto = _imgAuto;
                        imgPlaca = _imgPlaca;

                        foreach (var item in CdeAlerta.GetCamara())
                        {
                            if (_mac.Equals(item.In_Mac))
                            {
                                CdeAlerta.Insertar(item.In_Id, item.In_Nombre, ip, placa, imgPlaca, imgAuto, item.In_Latitud, item.In_Longitud, velocidad, item.In_Arco);
                            }
                        }

                        m_ID++;
                        break;
                    }
                case EM_EVENT_IVS_TYPE.TRAFFIC_OVERLINE:
                    {
                        NET_DEV_EVENT_TRAFFIC_OVERLINE_INFO info = (NET_DEV_EVENT_TRAFFIC_OVERLINE_INFO)Marshal.PtrToStructure(pEventInfo, typeof(NET_DEV_EVENT_TRAFFIC_OVERLINE_INFO));

                        foreach (var nlista in Lista.lstInfo)
                        {
                            if (nlista.LoginID == dwUser)
                            {
                                ip = nlista.IP;
                                _mac = CdeAlerta.GetMacAddress(nlista.IP);
                            }
                        }

                        id = m_ID.ToString();
                        mac = _mac;
                        placa = info.stTrafficCar.szPlateNumber;
                        velocidad = info.stTrafficCar.nSpeed;
                        archivo = info.stuObject.stPicInfo.dwFileLenth;
                        info2 = info.stuObject.stPicInfo.dwOffSet;

                        if (IntPtr.Zero != pBuffer && dwBufSize > 0)
                        {
                            buffer = new byte[dwBufSize];
                            Marshal.Copy(pBuffer, buffer, 0, (int)dwBufSize);
                        }

                        MostrarImg(buffer, archivo, info2, id);

                        imgAuto = _imgAuto;
                        imgPlaca = _imgPlaca;

                        foreach (var item in CdeAlerta.GetCamara())
                        {
                            if (_mac.Equals(item.In_Mac))
                            {
                                CdeAlerta.Insertar(item.In_Id, item.In_Nombre, ip, placa, imgPlaca, imgAuto, item.In_Latitud, item.In_Longitud, velocidad, item.In_Arco);
                            }
                        }

                        m_ID++;
                        break;
                    }
                case EM_EVENT_IVS_TYPE.TRAFFIC_RETROGRADE:
                    {
                        NET_DEV_EVENT_TRAFFIC_RETROGRADE_INFO info = (NET_DEV_EVENT_TRAFFIC_RETROGRADE_INFO)Marshal.PtrToStructure(pEventInfo, typeof(NET_DEV_EVENT_TRAFFIC_RETROGRADE_INFO));

                        foreach (var nlista in Lista.lstInfo)
                        {
                            if (nlista.LoginID == dwUser)
                            {
                                ip = nlista.IP;
                                _mac = CdeAlerta.GetMacAddress(nlista.IP);
                            }
                        }

                        id = m_ID.ToString();
                        mac = _mac;
                        placa = info.stTrafficCar.szPlateNumber;
                        velocidad = info.stTrafficCar.nSpeed;
                        archivo = info.stuObject.stPicInfo.dwFileLenth;
                        info2 = info.stuObject.stPicInfo.dwOffSet;

                        if (IntPtr.Zero != pBuffer && dwBufSize > 0)
                        {
                            buffer = new byte[dwBufSize];
                            Marshal.Copy(pBuffer, buffer, 0, (int)dwBufSize);
                        }

                        MostrarImg(buffer, archivo, info2, id);

                        imgAuto = _imgAuto;
                        imgPlaca = _imgPlaca;

                        foreach (var item in CdeAlerta.GetCamara())
                        {
                            if (_mac.Equals(item.In_Mac))
                            {
                                CdeAlerta.Insertar(item.In_Id, item.In_Nombre, ip, placa, imgPlaca, imgAuto, item.In_Latitud, item.In_Longitud, velocidad, item.In_Arco);
                            }
                        }

                        m_ID++;
                        break;
                    }
                //case EM_EVENT_IVS_TYPE.TRAFFIC_TURNLEFT:
                //    {
                //        NET_DEV_EVENT_TRAFFIC_TURNLEFT_INFO info = (NET_DEV_EVENT_TRAFFIC_TURNLEFT_INFO)Marshal.PtrToStructure(pEventInfo, typeof(NET_DEV_EVENT_TRAFFIC_TURNLEFT_INFO));
                //        
                //        break;
                //    }
                //case EM_EVENT_IVS_TYPE.TRAFFIC_TURNRIGHT:
                //    {
                //        NET_DEV_EVENT_TRAFFIC_TURNRIGHT_INFO info = (NET_DEV_EVENT_TRAFFIC_TURNRIGHT_INFO)Marshal.PtrToStructure(pEventInfo, typeof(NET_DEV_EVENT_TRAFFIC_TURNRIGHT_INFO));
                //        
                //        break;
                //    }
                //case EM_EVENT_IVS_TYPE.TRAFFIC_UTURN:
                //    {
                //        NET_DEV_EVENT_TRAFFIC_UTURN_INFO info = (NET_DEV_EVENT_TRAFFIC_UTURN_INFO)Marshal.PtrToStructure(pEventInfo, typeof(NET_DEV_EVENT_TRAFFIC_UTURN_INFO));
                //        
                //        break;
                //    }
                //case EM_EVENT_IVS_TYPE.TRAFFIC_PARKING:
                //    {
                //        NET_DEV_EVENT_TRAFFIC_PARKING_INFO info = (NET_DEV_EVENT_TRAFFIC_PARKING_INFO)Marshal.PtrToStructure(pEventInfo, typeof(NET_DEV_EVENT_TRAFFIC_PARKING_INFO));
                //     
                //        break;
                //    }
                case EM_EVENT_IVS_TYPE.TRAFFIC_WRONGROUTE:
                    {
                        NET_DEV_EVENT_TRAFFIC_WRONGROUTE_INFO info = (NET_DEV_EVENT_TRAFFIC_WRONGROUTE_INFO)Marshal.PtrToStructure(pEventInfo, typeof(NET_DEV_EVENT_TRAFFIC_WRONGROUTE_INFO));

                        foreach (var nlista in Lista.lstInfo)
                        {
                            if (nlista.LoginID == dwUser)
                            {
                                ip = nlista.IP;
                                _mac = CdeAlerta.GetMacAddress(nlista.IP);
                            }
                        }

                        id = m_ID.ToString();
                        mac = _mac;
                        placa = info.stTrafficCar.szPlateNumber;
                        velocidad = info.stTrafficCar.nSpeed;
                        archivo = info.stuObject.stPicInfo.dwFileLenth;
                        info2 = info.stuObject.stPicInfo.dwOffSet;

                        if (IntPtr.Zero != pBuffer && dwBufSize > 0)
                        {
                            buffer = new byte[dwBufSize];
                            Marshal.Copy(pBuffer, buffer, 0, (int)dwBufSize);
                        }

                        MostrarImg(buffer, archivo, info2, id);

                        imgAuto = _imgAuto;
                        imgPlaca = _imgPlaca;

                        foreach (var item in CdeAlerta.GetCamara())
                        {
                            if (_mac.Equals(item.In_Mac))
                            {
                                CdeAlerta.Insertar(item.In_Id, item.In_Nombre, ip, placa, imgPlaca, imgAuto, item.In_Latitud, item.In_Longitud, velocidad, item.In_Arco);
                            }
                        }

                        m_ID++;
                        break;
                    }
                //case EM_EVENT_IVS_TYPE.TRAFFIC_CROSSLANE:
                //    {
                //        NET_DEV_EVENT_TRAFFIC_CROSSLANE_INFO info = (NET_DEV_EVENT_TRAFFIC_CROSSLANE_INFO)Marshal.PtrToStructure(pEventInfo, typeof(NET_DEV_EVENT_TRAFFIC_CROSSLANE_INFO));

                //        break;
                //    }
                case EM_EVENT_IVS_TYPE.TRAFFIC_OVERYELLOWLINE:
                    {
                        NET_DEV_EVENT_TRAFFIC_OVERYELLOWLINE_INFO info = (NET_DEV_EVENT_TRAFFIC_OVERYELLOWLINE_INFO)Marshal.PtrToStructure(pEventInfo, typeof(NET_DEV_EVENT_TRAFFIC_OVERYELLOWLINE_INFO));

                        foreach (var nlista in Lista.lstInfo)
                        {
                            if (nlista.LoginID == dwUser)
                            {
                                ip = nlista.IP;
                                _mac = CdeAlerta.GetMacAddress(nlista.IP);
                            }
                        }

                        id = m_ID.ToString();
                        mac = _mac;
                        placa = info.stTrafficCar.szPlateNumber;
                        velocidad = info.stTrafficCar.nSpeed;
                        archivo = info.stuObject.stPicInfo.dwFileLenth;
                        info2 = info.stuObject.stPicInfo.dwOffSet;

                        if (IntPtr.Zero != pBuffer && dwBufSize > 0)
                        {
                            buffer = new byte[dwBufSize];
                            Marshal.Copy(pBuffer, buffer, 0, (int)dwBufSize);
                        }

                        MostrarImg(buffer, archivo, info2, id);

                        imgAuto = _imgAuto;
                        imgPlaca = _imgPlaca;

                        foreach (var item in CdeAlerta.GetCamara())
                        {
                            if (_mac.Equals(item.In_Mac))
                            {
                                CdeAlerta.Insertar(item.In_Id, item.In_Nombre, ip, placa, imgPlaca, imgAuto, item.In_Latitud, item.In_Longitud, velocidad, item.In_Arco);
                            }
                        }

                        m_ID++;
                        break;
                    }
                //case EM_EVENT_IVS_TYPE.TRAFFIC_YELLOWPLATEINLANE:
                //    {
                //        NET_DEV_EVENT_TRAFFIC_YELLOWPLATEINLANE_INFO info = (NET_DEV_EVENT_TRAFFIC_YELLOWPLATEINLANE_INFO)Marshal.PtrToStructure(pEventInfo, typeof(NET_DEV_EVENT_TRAFFIC_YELLOWPLATEINLANE_INFO));

                //        break;
                //    }
                case EM_EVENT_IVS_TYPE.TRAFFIC_PEDESTRAINPRIORITY:
                    {
                        NET_DEV_EVENT_TRAFFIC_PEDESTRAINPRIORITY_INFO info = (NET_DEV_EVENT_TRAFFIC_PEDESTRAINPRIORITY_INFO)Marshal.PtrToStructure(pEventInfo, typeof(NET_DEV_EVENT_TRAFFIC_PEDESTRAINPRIORITY_INFO));

                        foreach (var nlista in Lista.lstInfo)
                        {
                            if (nlista.LoginID == dwUser)
                            {
                                ip = nlista.IP;
                                _mac = CdeAlerta.GetMacAddress(nlista.IP);
                            }
                        }

                        id = m_ID.ToString();
                        mac = _mac;
                        placa = info.stTrafficCar.szPlateNumber;
                        velocidad = info.stTrafficCar.nSpeed;
                        archivo = info.stuObject.stPicInfo.dwFileLenth;
                        info2 = info.stuObject.stPicInfo.dwOffSet;

                        if (IntPtr.Zero != pBuffer && dwBufSize > 0)
                        {
                            buffer = new byte[dwBufSize];
                            Marshal.Copy(pBuffer, buffer, 0, (int)dwBufSize);
                        }

                        MostrarImg(buffer, archivo, info2, id);

                        imgAuto = _imgAuto;
                        imgPlaca = _imgPlaca;

                        foreach (var item in CdeAlerta.GetCamara())
                        {
                            if (_mac.Equals(item.In_Mac))
                            {
                                CdeAlerta.Insertar(item.In_Id, item.In_Nombre, ip, placa, imgPlaca, imgAuto, item.In_Latitud, item.In_Longitud, velocidad, item.In_Arco);
                            }
                        }

                        m_ID++;
                        break;
                    }
                case EM_EVENT_IVS_TYPE.TRAFFIC_VEHICLEINROUTE:
                    {
                        NET_DEV_EVENT_TRAFFIC_VEHICLEINROUTE_INFO info = (NET_DEV_EVENT_TRAFFIC_VEHICLEINROUTE_INFO)Marshal.PtrToStructure(pEventInfo, typeof(NET_DEV_EVENT_TRAFFIC_VEHICLEINROUTE_INFO));

                        foreach (var nlista in Lista.lstInfo)
                        {
                            if (nlista.LoginID == dwUser)
                            {
                                ip = nlista.IP;
                                _mac = CdeAlerta.GetMacAddress(nlista.IP);
                            }
                        }

                        id = m_ID.ToString();
                        mac = _mac;
                        placa = info.stTrafficCar.szPlateNumber;
                        velocidad = info.stTrafficCar.nSpeed;
                        archivo = info.stuObject.stPicInfo.dwFileLenth;
                        info2 = info.stuObject.stPicInfo.dwOffSet;

                        if (IntPtr.Zero != pBuffer && dwBufSize > 0)
                        {
                            buffer = new byte[dwBufSize];
                            Marshal.Copy(pBuffer, buffer, 0, (int)dwBufSize);
                        }

                        MostrarImg(buffer, archivo, info2, id);

                        imgAuto = _imgAuto;
                        imgPlaca = _imgPlaca;

                        foreach (var item in CdeAlerta.GetCamara())
                        {
                            if (_mac.Equals(item.In_Mac))
                            {
                                CdeAlerta.Insertar(item.In_Id, item.In_Nombre, ip, placa, imgPlaca, imgAuto, item.In_Latitud, item.In_Longitud, velocidad, item.In_Arco);
                            }
                        }

                        m_ID++;
                        break;
                    }
                case EM_EVENT_IVS_TYPE.TRAFFIC_VEHICLEINBUSROUTE:
                    {
                        NET_DEV_EVENT_TRAFFIC_VEHICLEINBUSROUTE_INFO info = (NET_DEV_EVENT_TRAFFIC_VEHICLEINBUSROUTE_INFO)Marshal.PtrToStructure(pEventInfo, typeof(NET_DEV_EVENT_TRAFFIC_VEHICLEINBUSROUTE_INFO));

                        foreach (var nlista in Lista.lstInfo)
                        {
                            if (nlista.LoginID == dwUser)
                            {
                                ip = nlista.IP;
                                _mac = CdeAlerta.GetMacAddress(nlista.IP);
                            }
                        }

                        id = m_ID.ToString();
                        mac = _mac;
                        placa = info.stTrafficCar.szPlateNumber;
                        velocidad = info.stTrafficCar.nSpeed;
                        archivo = info.stuObject.stPicInfo.dwFileLenth;
                        info2 = info.stuObject.stPicInfo.dwOffSet;

                        if (IntPtr.Zero != pBuffer && dwBufSize > 0)
                        {
                            buffer = new byte[dwBufSize];
                            Marshal.Copy(pBuffer, buffer, 0, (int)dwBufSize);
                        }

                        MostrarImg(buffer, archivo, info2, id);

                        imgAuto = _imgAuto;
                        imgPlaca = _imgPlaca;

                        foreach (var item in CdeAlerta.GetCamara())
                        {
                            if (_mac.Equals(item.In_Mac))
                            {
                                CdeAlerta.Insertar(item.In_Id, item.In_Nombre, ip, placa, imgPlaca, imgAuto, item.In_Latitud, item.In_Longitud, velocidad, item.In_Arco);
                            }
                        }

                        m_ID++;
                        break;
                    }
                case EM_EVENT_IVS_TYPE.TRAFFIC_BACKING:
                    {
                        NET_DEV_EVENT_IVS_TRAFFIC_BACKING_INFO info = (NET_DEV_EVENT_IVS_TRAFFIC_BACKING_INFO)Marshal.PtrToStructure(pEventInfo, typeof(NET_DEV_EVENT_IVS_TRAFFIC_BACKING_INFO));

                        foreach (var nlista in Lista.lstInfo)
                        {
                            if (nlista.LoginID == dwUser)
                            {
                                ip = nlista.IP;
                                _mac = CdeAlerta.GetMacAddress(nlista.IP);
                            }
                        }

                        id = m_ID.ToString();
                        mac = _mac;
                        placa = info.stTrafficCar.szPlateNumber;
                        velocidad = info.stTrafficCar.nSpeed;
                        archivo = info.stuObject.stPicInfo.dwFileLenth;
                        info2 = info.stuObject.stPicInfo.dwOffSet;

                        if (IntPtr.Zero != pBuffer && dwBufSize > 0)
                        {
                            buffer = new byte[dwBufSize];
                            Marshal.Copy(pBuffer, buffer, 0, (int)dwBufSize);
                        }

                        MostrarImg(buffer, archivo, info2, id);

                        imgAuto = _imgAuto;
                        imgPlaca = _imgPlaca;

                        foreach (var item in CdeAlerta.GetCamara())
                        {
                            if (_mac.Equals(item.In_Mac))
                            {
                                CdeAlerta.Insertar(item.In_Id, item.In_Nombre, ip, placa, imgPlaca, imgAuto, item.In_Latitud, item.In_Longitud, velocidad, item.In_Arco);
                            }
                        }

                        m_ID++;
                        break;
                    }
                //case EM_EVENT_IVS_TYPE.TRAFFIC_WITHOUT_SAFEBELT:
                //    {
                //        NET_DEV_EVENT_TRAFFIC_WITHOUT_SAFEBELT info = (NET_DEV_EVENT_TRAFFIC_WITHOUT_SAFEBELT)Marshal.PtrToStructure(pEventInfo, typeof(NET_DEV_EVENT_TRAFFIC_WITHOUT_SAFEBELT));

                //        break;
                //    }
                default:
                    Console.WriteLine(dwEventType.ToString("X"));
                    break;
            }

            return 0;
        }

        private static MemoryStream BytearrayToStream(byte[] arr)
        {
            return new MemoryStream(arr, 0, arr.Length);
        }

        private static byte[] autoimg = null, placaimg = null, _imgPlaca = null, _imgAuto = null;

        public static void MostrarImg(byte[] buffer, uint fileLenth, uint offSet, string id)
        {
            Image completa = null, placa = null;

            if (null == buffer)
            {
                return;
            }

            if (fileLenth > 0 && offSet > 0 && buffer.Length >= offSet + fileLenth)
            {
                autoimg = new byte[offSet];
                Array.Copy(buffer, 0, autoimg, 0, offSet - 1);

                placaimg = new byte[fileLenth];
                Array.Copy(buffer, offSet, placaimg, 0, fileLenth);

                try
                {
                    _imgPlaca = RezizeImage(Image.FromStream(BytearrayToStream(placaimg)), 64, 96);

                    MemoryStream img1 = new MemoryStream(_imgPlaca);
                    placa = Image.FromStream(img1);

                    MainWindow.f1.pboxPlaca.Image = placa;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }

            }
            else
            {
                MainWindow.f1.pboxPlaca.Image = null;
                autoimg = buffer;
            }
            try
            {
                _imgAuto = RezizeImage(Image.FromStream(BytearrayToStream(autoimg)), 280, 440);

                MemoryStream img2 = new MemoryStream(_imgAuto);
                completa = Image.FromStream(img2);

                MainWindow.f1.pboxCompleta.Image = completa;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            //try
            //{
            //    if (placaimg != null)
            //    {
            //        _imgPlaca = ResizeImage(MainWindow.f1.pboxPlaca.Image, MainWindow.f1.pboxPlaca.Height, MainWindow.f1.pboxPlaca.Width);                    
            //    }
            //    //if (autoimg != null)
            //    //{
            //        _imgAuto = ResizeImage(MainWindow.f1.pboxCompleta.Image, MainWindow.f1.pboxCompleta.Image.Height / 2, MainWindow.f1.pboxCompleta.Image.Width / 2);
            //    //}
            //}
            //catch (InvalidOperationException ex)
            //{
            //    Console.WriteLine(ex);
            //}
        }

        public static byte[] RezizeImage(Image pImagen, int pAlto, int pAncho)
        {
            //creamos un bitmap con el nuevo tamaño
            Bitmap vBitmap = new Bitmap(pAncho, pAlto);
            //creamos un graphics tomando como base el nuevo Bitmap
            using (Graphics vGraphics = Graphics.FromImage((Image)vBitmap))
            {
                //especificamos el tipo de transformación, se escoge esta para no perder calidad.
                vGraphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                //Se dibuja la nueva imagen
                vGraphics.DrawImage(pImagen, 0, 0, pAncho, pAlto);
            }

            MemoryStream imgnew = new MemoryStream();
            vBitmap.Save(imgnew, ImageFormat.Jpeg);

            //retornamos la nueva imagen
            return imgnew.ToArray();
        }

    }
}
