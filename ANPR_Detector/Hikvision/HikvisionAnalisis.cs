using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using App.Deal;

namespace IntelligentTraffic.Hikvision
{
    public class HikvisionAnalisis
    {
        public Int32 m_lChannel = -1;
        //private Int32 m_lPlayHandle = -1;
        public Int32 m_lUserID = -1;
        public Int32 m_iCurDeviceIndex = -1;
        private ArbolP g_deviceTree = ArbolP.Instance();
        private static CHCNetSDK.MSGCallBack m_falarmData = null;
        //private string m_strinifile = "";
        public string m_strLPRFilePath = "";
        private CHCNetSDK.LOCAL_LOG_INFO m_struLogInfo = new CHCNetSDK.LOCAL_LOG_INFO();

        public void Inicio(int contamax)
        {
            int iCurDeviceIndex = -1;
            CHCNetSDK.NET_DVR_SETUPALARM_PARAM struSetupAlarmParam = new CHCNetSDK.NET_DVR_SETUPALARM_PARAM();
            struSetupAlarmParam.dwSize = (uint)Marshal.SizeOf(struSetupAlarmParam);
            struSetupAlarmParam.byLevel = 1;
            struSetupAlarmParam.byAlarmInfoType = 1;

            iCurDeviceIndex = contamax;//Indice x 
            if (-1 == g_deviceTree.g_struDeviceInfo[iCurDeviceIndex].lFortifyHandle)
            {
                g_deviceTree.g_struDeviceInfo[iCurDeviceIndex].lFortifyHandle = (int)CHCNetSDK.NET_DVR_SetupAlarmChan_V41(
                    (int)g_deviceTree.g_struDeviceInfo[iCurDeviceIndex].lLoginID, ref struSetupAlarmParam);
                if (-1 == g_deviceTree.g_struDeviceInfo[iCurDeviceIndex].lFortifyHandle)
                {
                    CHCNetSDK.AddLog(m_lUserID, CHCNetSDK.OPERATION_FAIL_T, "NET_DVR_SetupAlarmChan_V41");
                    return;
                }
                else
                {
                    m_falarmData = new CHCNetSDK.MSGCallBack(MsgCallback);
                    if (CHCNetSDK.NET_DVR_SetDVRMessageCallBack_V30(m_falarmData, IntPtr.Zero))
                    {
                        //mensaje = "Guard success!";
                    }
                    else
                    {
                        //mensaje = "Guard failed!";
                    }
                }

            }
        }   

        private void MsgCallback(int lCommand, ref CHCNetSDK.NET_DVR_ALARMER pAlarmer, IntPtr pAlarmInfo, uint dwBufLen, IntPtr pUser)
        {
            switch (lCommand)
            {
                case CHCNetSDK.COMM_ITS_PLATE_RESULT:
                    ProcessCommAlarm_ITSPlate(ref pAlarmer, pAlarmInfo, dwBufLen, pUser);
                    break;
                default:
                    break;
            }
        }

        byte[] bycompleto, byplaca;

        private static MemoryStream BytearrayToStream(byte[] arr)
        {
            return new MemoryStream(arr, 0, arr.Length);
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
        
        private void ProcessCommAlarm_ITSPlate(ref CHCNetSDK.NET_DVR_ALARMER pAlarmer, IntPtr pAlarmInfo, uint dwBufLen, IntPtr pUser)
        {
            try
            {
                string plateFullpath = "";
                string backgroundfullpath = "";
                string LPRTodayPath = m_strLPRFilePath + "\\" + DateTime.Now.ToString("yyyy-MM-dd");
                if (!Directory.Exists(LPRTodayPath))
                    Directory.CreateDirectory(LPRTodayPath);

                string datatimenow = DateTime.Now.ToString("yyyyMMddhhmmssfff");

                CHCNetSDK.NET_ITS_PLATE_RESULT struLPRSnapeResult = new CHCNetSDK.NET_ITS_PLATE_RESULT();
                uint dwSize = (uint)Marshal.SizeOf(struLPRSnapeResult);

                struLPRSnapeResult = (CHCNetSDK.NET_ITS_PLATE_RESULT)Marshal.PtrToStructure(pAlarmInfo, typeof(CHCNetSDK.NET_ITS_PLATE_RESULT));

                string licencia; //colorp, tipop, tipoc, colorc;
                
                ////info show
                //if (struLPRSnapeResult.struPlateInfo.byPlateType == 0)
                //{
                //    tipop = "STANDARD92";
                //}
                //else if (struLPRSnapeResult.struPlateInfo.byPlateType == 1)
                //{
                //    tipop = "STANDARD02";
                //}
                //else if (struLPRSnapeResult.struPlateInfo.byPlateType == 2)
                //{
                //    tipop = "armed police";
                //}
                //else if (struLPRSnapeResult.struPlateInfo.byPlateType == 3)
                //{
                //    tipop = "police";
                //}
                //else if (struLPRSnapeResult.struPlateInfo.byPlateType == 4)
                //{
                //    tipop = "STANDARD92_BACK";
                //}
                //else if (struLPRSnapeResult.struPlateInfo.byPlateType == 5)
                //{
                //    tipop = "embassy";
                //}
                //else if (struLPRSnapeResult.struPlateInfo.byPlateType == 6)
                //{
                //    tipop = "farm";
                //}
                //else if (struLPRSnapeResult.struPlateInfo.byPlateType == 7)
                //{
                //    tipop = "moto";
                //}
                //else if (struLPRSnapeResult.struPlateInfo.byPlateType == 8)
                //{
                //    tipop = "new energy";
                //}
                //else
                //{
                //    tipop = "unknown";
                //}

                //if (struLPRSnapeResult.struPlateInfo.byColor == 0)
                //{
                //    colorp = "blue";
                //}
                //else if (struLPRSnapeResult.struPlateInfo.byColor == 0)
                //{
                //    colorp = "yellow";
                //}
                //else if (struLPRSnapeResult.struPlateInfo.byColor == 0)
                //{
                //    colorp = "white";
                //}
                //else if (struLPRSnapeResult.struPlateInfo.byColor == 0)
                //{
                //    colorp = "black";
                //}
                //else if (struLPRSnapeResult.struPlateInfo.byColor == 0)
                //{
                //    colorp = "green";
                //}
                //else if (struLPRSnapeResult.struPlateInfo.byColor == 0)
                //{
                //    colorp = "air black";
                //}
                //else
                //{
                //    colorp = "unknown";
                //}                               

                licencia = struLPRSnapeResult.struPlateInfo.sLicense.Replace("O","0").Replace("Q","0").Replace("I","1");
                
                //if (struLPRSnapeResult.byVehicleType == 0)
                //{
                //    if (struLPRSnapeResult.struVehicleInfo.byVehicleType == 0)
                //    {
                //         tipoc = "other";
                //    }
                //    else if (struLPRSnapeResult.struVehicleInfo.byVehicleType == 1)
                //    {
                //         tipoc = "small";
                //    }
                //    else if (struLPRSnapeResult.struVehicleInfo.byVehicleType == 2)
                //    {
                //         tipoc = "big";
                //    }
                //    else if (struLPRSnapeResult.struVehicleInfo.byVehicleType == 3)
                //    {
                //         tipoc = "human";
                //    }
                //    else if (struLPRSnapeResult.struVehicleInfo.byVehicleType == 4)
                //    {
                //         tipoc = "tumbrel";
                //    }
                //    else if (struLPRSnapeResult.struVehicleInfo.byVehicleType == 5)
                //    {
                //         tipoc = "trike";
                //    }
                //    else if (struLPRSnapeResult.struVehicleInfo.byVehicleType == 6)
                //    {
                //         tipoc = "motor";
                //    }
                //    else
                //    {
                //         tipoc = "unknown";
                //    }
                //}
                //else if (struLPRSnapeResult.byVehicleType == 1)
                //{
                //     tipoc = "bus";
                //}
                //else if (struLPRSnapeResult.byVehicleType == 2)
                //{
                //     tipoc = "truck";
                //}
                //else if (struLPRSnapeResult.byVehicleType == 3)
                //{
                //     tipoc = "car";
                //}
                //else if (struLPRSnapeResult.byVehicleType == 4)
                //{
                //     tipoc = "mini bus";
                //}
                //else if (struLPRSnapeResult.byVehicleType == 5)
                //{
                //     tipoc = "small truck";
                //}
                //else if (struLPRSnapeResult.byVehicleType == 6)
                //{
                //     tipoc = "human";
                //}
                //else if (struLPRSnapeResult.byVehicleType == 7)
                //{
                //     tipoc = "tumbrel";
                //}
                //else if (struLPRSnapeResult.byVehicleType == 8)
                //{
                //     tipoc = "trike";
                //}
                //else if (struLPRSnapeResult.byVehicleType == 9)
                //{
                //     tipoc = "SUV/MPV";
                //}
                //else if (struLPRSnapeResult.byVehicleType == 10)
                //{
                //     tipoc = "medium bus";
                //}
                //else if (struLPRSnapeResult.byVehicleType == 11)
                //{
                //     tipoc = "motor";
                //}
                //else if (struLPRSnapeResult.byVehicleType == 12)
                //{
                //     tipoc = "non motor";
                //}
                //else if (struLPRSnapeResult.byVehicleType == 13)
                //{
                //     tipoc = "small car";
                //}
                //else if (struLPRSnapeResult.byVehicleType == 14)
                //{
                //     tipoc = "micro car";
                //}
                //else if (struLPRSnapeResult.byVehicleType == 15)
                //{
                //     tipoc = "pickup";
                //}
                //else
                //{
                //     tipoc = "unknown";
                //}

                //if (struLPRSnapeResult.struVehicleInfo.byColor == 0)
                //{
                //    colorc = "unsupport";
                //}
                //else if (struLPRSnapeResult.struVehicleInfo.byColor == 1)
                //{
                //    colorc = "white";
                //}
                //else if (struLPRSnapeResult.struVehicleInfo.byColor == 2)
                //{
                //    colorc = "silver";
                //}
                //else if (struLPRSnapeResult.struVehicleInfo.byColor == 3)
                //{
                //    colorc = "gray";
                //}
                //else if (struLPRSnapeResult.struVehicleInfo.byColor == 4)
                //{
                //    colorc = "black";
                //}
                //else if (struLPRSnapeResult.struVehicleInfo.byColor == 5)
                //{
                //    colorc = "red";
                //}
                //else if (struLPRSnapeResult.struVehicleInfo.byColor == 6)
                //{
                //    colorc = "dark blue";
                //}
                //else if (struLPRSnapeResult.struVehicleInfo.byColor == 7)
                //{
                //    colorc = "blue";
                //}
                //else if (struLPRSnapeResult.struVehicleInfo.byColor == 8)
                //{
                //    colorc = "yellow";
                //}
                //else if (struLPRSnapeResult.struVehicleInfo.byColor == 9)
                //{
                //    colorc = "green";
                //}
                //else if (struLPRSnapeResult.struVehicleInfo.byColor == 10)
                //{
                //    colorc = "brown";
                //}
                //else if (struLPRSnapeResult.struVehicleInfo.byColor == 11)
                //{
                //    colorc = "pink";
                //}
                //else if (struLPRSnapeResult.struVehicleInfo.byColor == 12)
                //{
                //    colorc = "purple";
                //}
                //else if (struLPRSnapeResult.struVehicleInfo.byColor == 13)
                //{
                //    colorc = "dark gray";
                //}
                //else if (struLPRSnapeResult.struVehicleInfo.byColor == 14)
                //{
                //    colorc = "cyan";
                //}
                //else
                //{
                //    colorc = "unknown";
                //}

                string szInfoBuf = "ITS Plate Alarm Analysis[" + struLPRSnapeResult.byDataAnalysis.ToString() 
                                + "]YellowLabel[" + struLPRSnapeResult.byYellowLabelCar.ToString() 
                                + "]DangerousVeh[" + struLPRSnapeResult.byDangerousVehicles.ToString() 
                                + "]Region[" + struLPRSnapeResult.struPlateInfo.byRegion.ToString() 
                                + "]Country[" + struLPRSnapeResult.struPlateInfo.byCountry.ToString()
                                + "]MatchNo[" + struLPRSnapeResult.dwMatchNo.ToString() 
                                + "]DriveChan[" + struLPRSnapeResult.byDriveChan.ToString() 
                                + "]IllegalType[" + struLPRSnapeResult.wIllegalType.ToString()
                                + "]IllegalSubType[" + struLPRSnapeResult.byIllegalSubType.ToString() 
                                + "]MonitoringSiteID[" + struLPRSnapeResult.byMonitoringSiteID.ToString() 
                                + "]DeviceID[" + struLPRSnapeResult.byDeviceID.ToString() 
                                + "]Dir[" + struLPRSnapeResult.byDir.ToString()
                                + "]PicNum[" + struLPRSnapeResult.dwPicNum.ToString() 
                                + "]License[" + struLPRSnapeResult.struPlateInfo.sLicense.ToString() 
                                + "]RadarState[" + struLPRSnapeResult.struVehicleInfo.byRadarState.ToString() 
                                + "]Speed[" + struLPRSnapeResult.struVehicleInfo.wSpeed.ToString() 
                                + "]VehicleLogoRecog[" + struLPRSnapeResult.struVehicleInfo.byVehicleLogoRecog.ToString() 
                                + "]VehicleSubLogoRecog[" + struLPRSnapeResult.struVehicleInfo.byVehicleSubLogoRecog.ToString() 
                                + "]DetSceneID[" + struLPRSnapeResult.byDetSceneID.ToString()
                                + "]VehicleType[" + struLPRSnapeResult.byVehicleType.ToString() 
                                + "]DetectType[" + struLPRSnapeResult.byDetectType.ToString() 
                                + "]Type[" + struLPRSnapeResult.struPicInfo[0].byType.ToString() 
                                + "]DataType[" + struLPRSnapeResult.struPicInfo[0].byDataType.ToString()
                                + "]CloseUpType[" + struLPRSnapeResult.struPicInfo[0].byCloseUpType.ToString() 
                                + "]EntireBelieve[" + struLPRSnapeResult.struPlateInfo.byEntireBelieve.ToString()
                                + "]Channel NO[" + ((int)(struLPRSnapeResult.byChanIndexEx * 256 + struLPRSnapeResult.byChanIndex)).ToString() 
                                + "]LicenseLen[" + struLPRSnapeResult.struPlateInfo.byLicenseLen.ToString() 
                                + "]PlateRect[x-" + struLPRSnapeResult.struPlateInfo.struPlateRect.fX.ToString()
                                + " y-" + struLPRSnapeResult.struPlateInfo.struPlateRect.fY.ToString() + " w-" + struLPRSnapeResult.struPlateInfo.struPlateRect.fWidth.ToString()
                                + " h-" + struLPRSnapeResult.struPlateInfo.struPlateRect.fHeight.ToString() + "]";

                if (struLPRSnapeResult.dwPicNum > 0)
                {
                    for (int i = 0; i < struLPRSnapeResult.dwPicNum; i++)
                    {
                        //data type
                        if (struLPRSnapeResult.struPicInfo[i].byDataType == 0)
                        {
                            //Scene picture
                            if (struLPRSnapeResult.struPicInfo[i].byType == 1)
                            {
                                if (struLPRSnapeResult.struPicInfo[i].dwDataLen > 0)
                                {
                                    //backgroundfullpath = LPRTodayPath + "\\" + datatimenow + "_ScenePic_" + i + ".jpg";

                                    int iLen = (int)struLPRSnapeResult.struPicInfo[i].dwDataLen;
                                    byte[] _bycompleto = new byte[iLen];
                                    Marshal.Copy(struLPRSnapeResult.struPicInfo[i].pBuffer, _bycompleto, 0, iLen);
                                    //Image img = Image.FromStream(new MemoryStream(bycompleto));
                                    //Bitmap bmpImage = new Bitmap(img);

                                    //if (struLPRSnapeResult.struPicInfo[i].struPlateRect.fHeight != 0)
                                    //{
                                    //    Graphics g = Graphics.FromImage(bmpImage);
                                    //    int x = (int)(struLPRSnapeResult.struPicInfo[i].struPlateRect.fX * img.Width);
                                    //    int y = (int)(struLPRSnapeResult.struPicInfo[i].struPlateRect.fY * img.Height);
                                    //    int width = (int)(struLPRSnapeResult.struPicInfo[i].struPlateRect.fWidth * img.Width);
                                    //    int height = (int)(struLPRSnapeResult.struPicInfo[i].struPlateRect.fHeight * img.Height);

                                    //    //save
                                    //    Rectangle cropArea = new Rectangle(x, y, width, height);
                                    //    Bitmap bmpCrop = bmpImage.Clone(cropArea, bmpImage.PixelFormat);
                                    //    plateFullpath = LPRTodayPath + "\\" + datatimenow + "_PlateRectPic_" + i + ".jpg";
                                    //    bmpCrop.Save(plateFullpath);
                                    //    bmpCrop.Dispose();
                                    //    MainWindow.f1.pboxPlaca.Image = Image.FromFile(plateFullpath);

                                    //    //draw rect                             
                                    //    Brush brush = new SolidBrush(Color.Red);
                                    //    Pen pen = new Pen(brush, 3);
                                    //    g.DrawRectangle(pen, new Rectangle(x, y, width, height));

                                    //    g.Dispose();
                                    //}
                                    //bmpImage.Save(backgroundfullpath);
                                    //MainWindow.f1.pboxCompleta.Image = Image.FromFile(backgroundfullpath);

                                    //img.Dispose();
                                    //bmpImage.Dispose();

                                    bycompleto = RezizeImage(Image.FromStream(BytearrayToStream(_bycompleto)), 280, 440);

                                    MemoryStream img2 = new MemoryStream(bycompleto);
                                    Image completa = Image.FromStream(img2);

                                    MainWindow.f1.pboxCompleta.Image = completa;

                                }
                            }

                            //license picture
                            if (struLPRSnapeResult.struPicInfo[i].byType == 0)
                            {
                                if (struLPRSnapeResult.struPicInfo[i].dwDataLen > 0)
                                {
                                    //plateFullpath = LPRTodayPath + "\\" + datatimenow + "_PlatePic_" + i + ".jpg";
                                    //FileStream fs = new FileStream(plateFullpath, FileMode.Create);
                                    int iLen = (int)struLPRSnapeResult.struPicInfo[i].dwDataLen;
                                    byte[] _byplaca = new byte[iLen];
                                    Marshal.Copy(struLPRSnapeResult.struPicInfo[i].pBuffer, _byplaca, 0, iLen);
                                    //fs.Write(byplaca, 0, iLen);
                                    //fs.Close();
                                    //MainWindow.f1.pboxPlaca.Image = Image.FromFile(plateFullpath);

                                    byplaca = RezizeImage(Image.FromStream(BytearrayToStream(_byplaca)), 64, 96);

                                    MemoryStream img2 = new MemoryStream(byplaca);
                                    Image placa = Image.FromStream(img2);

                                    MainWindow.f1.pboxPlaca.Image = placa;
                                }
                            }

                            string pathtemp = "";
                            //CompositeMap
                            if (struLPRSnapeResult.struPicInfo[i].byType == 2)
                            {
                                if (struLPRSnapeResult.struPicInfo[i].dwDataLen > 0)
                                {
                                    pathtemp = LPRTodayPath + "\\" + datatimenow + "_CompositeMap_" + i + ".jpg";
                                    FileStream fs = new FileStream(pathtemp, FileMode.Create);
                                    int iLen = (int)struLPRSnapeResult.struPicInfo[i].dwDataLen;
                                    byte[] by = new byte[iLen];
                                    Marshal.Copy(struLPRSnapeResult.struPicInfo[i].pBuffer, by, 0, iLen);
                                    fs.Write(by, 0, iLen);
                                    fs.Close();
                                }
                            }

                            //feature pic
                            if (struLPRSnapeResult.struPicInfo[i].byType == 3)
                            {
                                if (struLPRSnapeResult.struPicInfo[i].dwDataLen > 0)
                                {
                                    pathtemp = LPRTodayPath + "\\" + datatimenow + "_FeaturePic_" + i + ".jpg";
                                    FileStream fs = new FileStream(pathtemp, FileMode.Create);
                                    int iLen = (int)struLPRSnapeResult.struPicInfo[i].dwDataLen;
                                    byte[] by = new byte[iLen];
                                    Marshal.Copy(struLPRSnapeResult.struPicInfo[i].pBuffer, by, 0, iLen);
                                    fs.Write(by, 0, iLen);
                                    fs.Close();
                                }
                            }

                            //Two straight map
                            if (struLPRSnapeResult.struPicInfo[i].byType == 4)
                            {
                                if (struLPRSnapeResult.struPicInfo[i].dwDataLen > 0)
                                {
                                    pathtemp = LPRTodayPath + "\\" + datatimenow + "_TwoStraightMap_" + i + ".jpg";
                                    FileStream fs = new FileStream(pathtemp, FileMode.Create);
                                    int iLen = (int)struLPRSnapeResult.struPicInfo[i].dwDataLen;
                                    byte[] by = new byte[iLen];
                                    Marshal.Copy(struLPRSnapeResult.struPicInfo[i].pBuffer, by, 0, iLen);
                                    fs.Write(by, 0, iLen);
                                    fs.Close();
                                }
                            }

                            //stream
                            if (struLPRSnapeResult.struPicInfo[i].byType == 5)
                            {
                                if (struLPRSnapeResult.struPicInfo[i].dwDataLen > 0)
                                {
                                    pathtemp = LPRTodayPath + "\\" + datatimenow + "_stream_" + i + ".data";
                                    FileStream fs = new FileStream(pathtemp, FileMode.Create);
                                    int iLen = (int)struLPRSnapeResult.struPicInfo[i].dwDataLen;
                                    byte[] by = new byte[iLen];
                                    Marshal.Copy(struLPRSnapeResult.struPicInfo[i].pBuffer, by, 0, iLen);
                                    fs.Write(by, 0, iLen);
                                    fs.Close();
                                }
                            }

                            //pilotface
                            if (struLPRSnapeResult.struPicInfo[i].byType == 6)
                            {
                                if (struLPRSnapeResult.struPicInfo[i].dwDataLen > 0)
                                {
                                    pathtemp = LPRTodayPath + "\\" + datatimenow + "_Pilot_FacePic_" + i + ".jpg";
                                    FileStream fs = new FileStream(pathtemp, FileMode.Create);
                                    int iLen = (int)struLPRSnapeResult.struPicInfo[i].dwDataLen;
                                    byte[] by = new byte[iLen];
                                    Marshal.Copy(struLPRSnapeResult.struPicInfo[i].pBuffer, by, 0, iLen);
                                    fs.Write(by, 0, iLen);
                                    fs.Close();
                                }
                            }

                            //copilotface
                            if (struLPRSnapeResult.struPicInfo[i].byType == 7)
                            {
                                if (struLPRSnapeResult.struPicInfo[i].dwDataLen > 0)
                                {
                                    pathtemp = LPRTodayPath + "\\" + datatimenow + "_Copilot_FacePic_" + i + ".jpg";
                                    FileStream fs = new FileStream(pathtemp, FileMode.Create);
                                    int iLen = (int)struLPRSnapeResult.struPicInfo[i].dwDataLen;
                                    byte[] by = new byte[iLen];
                                    Marshal.Copy(struLPRSnapeResult.struPicInfo[i].pBuffer, by, 0, iLen);
                                    fs.Write(by, 0, iLen);
                                    fs.Close();
                                }
                            }

                            //Non-motor vehicles
                            if (struLPRSnapeResult.struPicInfo[i].byType == 8)
                            {
                                if (struLPRSnapeResult.struPicInfo[i].dwDataLen > 0)
                                {
                                    pathtemp = LPRTodayPath + "\\" + datatimenow + "_NonMotor_VehiclePic_" + i + ".jpg";
                                    FileStream fs = new FileStream(pathtemp, FileMode.Create);
                                    int iLen = (int)struLPRSnapeResult.struPicInfo[i].dwDataLen;
                                    byte[] by = new byte[iLen];
                                    Marshal.Copy(struLPRSnapeResult.struPicInfo[i].pBuffer, by, 0, iLen);
                                    fs.Write(by, 0, iLen);
                                    fs.Close();
                                }
                            }

                            //pedestrian
                            if (struLPRSnapeResult.struPicInfo[i].byType == 9)
                            {
                                if (struLPRSnapeResult.struPicInfo[i].dwDataLen > 0)
                                {
                                    pathtemp = LPRTodayPath + "\\" + datatimenow + "_PedestrianPic_" + i + ".jpg";
                                    FileStream fs = new FileStream(pathtemp, FileMode.Create);
                                    int iLen = (int)struLPRSnapeResult.struPicInfo[i].dwDataLen;
                                    byte[] by = new byte[iLen];
                                    Marshal.Copy(struLPRSnapeResult.struPicInfo[i].pBuffer, by, 0, iLen);
                                    fs.Write(by, 0, iLen);
                                    fs.Close();
                                }
                            }

                            //weigh original data
                            if (struLPRSnapeResult.struPicInfo[i].byType == 10)
                            {
                                if (struLPRSnapeResult.struPicInfo[i].dwDataLen > 0)
                                {
                                    pathtemp = LPRTodayPath + "\\" + datatimenow + "_Weigh_OriginalData_" + i + ".data";
                                    FileStream fs = new FileStream(pathtemp, FileMode.Create);
                                    int iLen = (int)struLPRSnapeResult.struPicInfo[i].dwDataLen;
                                    byte[] by = new byte[iLen];
                                    Marshal.Copy(struLPRSnapeResult.struPicInfo[i].pBuffer, by, 0, iLen);
                                    fs.Write(by, 0, iLen);
                                    fs.Close();
                                }
                            }

                            //target pic
                            if (struLPRSnapeResult.struPicInfo[i].byType == 11)
                            {
                                if (struLPRSnapeResult.struPicInfo[i].dwDataLen > 0)
                                {
                                    pathtemp = LPRTodayPath + "\\" + datatimenow + "_TargetPic_" + i + ".jpg";
                                    FileStream fs = new FileStream(pathtemp, FileMode.Create);
                                    int iLen = (int)struLPRSnapeResult.struPicInfo[i].dwDataLen;
                                    byte[] by = new byte[iLen];
                                    Marshal.Copy(struLPRSnapeResult.struPicInfo[i].pBuffer, by, 0, iLen);
                                    fs.Write(by, 0, iLen);
                                    fs.Close();
                                }
                            }

                            //pilotroom pic
                            if (struLPRSnapeResult.struPicInfo[i].byType == 12)
                            {
                                if (struLPRSnapeResult.struPicInfo[i].dwDataLen > 0)
                                {
                                    pathtemp = LPRTodayPath + "\\" + datatimenow + "_PilotRoomPic_" + i + ".jpg";
                                    FileStream fs = new FileStream(pathtemp, FileMode.Create);
                                    int iLen = (int)struLPRSnapeResult.struPicInfo[i].dwDataLen;
                                    byte[] by = new byte[iLen];
                                    Marshal.Copy(struLPRSnapeResult.struPicInfo[i].pBuffer, by, 0, iLen);
                                    fs.Write(by, 0, iLen);
                                    fs.Close();
                                }
                            }

                            //copilot room pic
                            if (struLPRSnapeResult.struPicInfo[i].byType == 13)
                            {
                                if (struLPRSnapeResult.struPicInfo[i].dwDataLen > 0)
                                {
                                    pathtemp = LPRTodayPath + "\\" + datatimenow + "_CopilotRoomPic_" + i + ".jpg";
                                    FileStream fs = new FileStream(pathtemp, FileMode.Create);
                                    int iLen = (int)struLPRSnapeResult.struPicInfo[i].dwDataLen;
                                    byte[] by = new byte[iLen];
                                    Marshal.Copy(struLPRSnapeResult.struPicInfo[i].pBuffer, by, 0, iLen);
                                    fs.Write(by, 0, iLen);
                                    fs.Close();
                                }
                            }
                        }
                    }
                }

                if (!licencia.ToUpper().Contains("NOPLATE") &&  byplaca != null)
                {                    
                    foreach (var item in CdeAlerta.GetCamara())
                    {
                        if (pAlarmer.sDeviceIP.Equals(item.In_Mac))
                        {
                            CdeAlerta.Insertar(item.In_Id, item.In_Nombre, pAlarmer.sDeviceIP, licencia, byplaca, bycompleto, item.In_Latitud, item.In_Longitud, struLPRSnapeResult.struVehicleInfo.wSpeed, item.In_Arco);
                        }
                    }                    
                }
                
                m_struLogInfo.strPlatePicfullpath = plateFullpath;
                m_struLogInfo.strScenePicfullpath = backgroundfullpath;
                m_struLogInfo.strLicense = licencia.ToString();

                AddLog(m_iCurDeviceIndex, CHCNetSDK.ALARM_INFO_T, szInfoBuf);

            }
            catch (Exception ex)
            {
                string err = ex.Message.ToString();
            }
        }
        
        public void AddLog(int iDeviceIndex, int iLogType, string strFormat)
        {
            ArbolP g_deviceTree = ArbolP.Instance();
            DateTime curTime = DateTime.Now;
            string strTime = null;
            string strLogInfo = null;
            string strDevInfo = null;
            string strErrInfo = null;
            string strLog = null;
            strTime = curTime.ToString();
            strLogInfo = strFormat;

            if (iDeviceIndex != -1 && iDeviceIndex < 512)
            {
                if (null == g_deviceTree.GetCurDeviceIp())
                {
                    strDevInfo = null;
                }
                else
                {
                    strDevInfo = string.Format("{0}-{1}", g_deviceTree.GetCurDeviceIp(), g_deviceTree.GetCurLocalNodeName());
                }
            }

            if (iLogType == CHCNetSDK.ALARM_INFO_T)
            {
                strLog = string.Format("{0}{1}\n", strTime, strLogInfo);
                //log file path undetermined, temporarily not implementation 
            }

            m_struLogInfo.iLogType = iLogType;
            m_struLogInfo.strTime = strTime;
            m_struLogInfo.strLogInfo = strLogInfo;
            m_struLogInfo.strDevInfo = strDevInfo;
            m_struLogInfo.strErrInfo = strErrInfo;
            AddList(m_struLogInfo);
        }        

        private void AddList(CHCNetSDK.LOCAL_LOG_INFO struLogInfo)
        {            
            if (struLogInfo.iLogType == CHCNetSDK.ALARM_INFO_T)
            {
                ListViewItem listItem = new ListViewItem
                {
                    Text = struLogInfo.strTime
                };
                listItem.SubItems.Add(struLogInfo.strDevInfo);
                listItem.SubItems.Add(struLogInfo.strLogInfo);
                listItem.SubItems.Add(struLogInfo.strPlatePicfullpath);
                listItem.SubItems.Add(struLogInfo.strScenePicfullpath);
                listItem.SubItems.Add(struLogInfo.strPlateType);
                listItem.SubItems.Add(struLogInfo.strPlateColor);
                listItem.SubItems.Add(struLogInfo.strLicense);
                listItem.SubItems.Add(struLogInfo.strVehicleType);
                listItem.SubItems.Add(struLogInfo.strVehicleColor);
            }
        }

    }
}
