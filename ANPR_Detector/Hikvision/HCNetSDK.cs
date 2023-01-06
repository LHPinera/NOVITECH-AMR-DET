using System;
using System.Runtime.InteropServices;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace IntelligentTraffic.Hikvision
{
    /// <summary>
    /// CHCNetSDK 
    /// </summary>
    public class CHCNetSDK
    {

        public CHCNetSDK()
        {
            //
            // TODO: 
            //
        }
        #region 
        public static string ProtocolType;

        #endregion

        public const int MAX_SUBSYSTEM_NUM = 80;
        public const int MAX_SERIALLEN = 36;

        public const int MAX_LOOPPLANNUM = 16; 
        public const int DECODE_TIMESEGMENT = 4; 

        public const int MAX_DOMAIN_NAME = 64; 
        public const int MAX_DISKNUM_V30 = 33; 
        public const int MAX_DAYS = 7;

        public const uint DEVICE_ABILITY_INFO = 0x011;

        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_SUBSYSTEMINFO
        {
            public byte bySubSystemType;
            public byte byChan;
            public byte byLoginType;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
            public byte[] byRes1;
            public NET_DVR_IPADDR struSubSystemIP;
            public ushort wSubSystemPort;
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 6, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
            public byte[] byRes2;

            public NET_DVR_IPADDR struSubSystemIPMask;
            public NET_DVR_IPADDR struGatewayIpAddr;

            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
            public byte[] sUserName;
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = PASSWD_LEN, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
            public byte[] sPassword;

            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst = MAX_DOMAIN_NAME)]
            public string sDomainName;
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst = MAX_DOMAIN_NAME)]
            public string sDnsAddress;
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 6, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
            public byte[] sSerialNumber;
        }

        [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
        public struct NET_DVR_ALLSUBSYSTEMINFO
        {
            public uint dwSize;
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = MAX_SUBSYSTEM_NUM, ArraySubType = System.Runtime.InteropServices.UnmanagedType.Struct)]
            public NET_DVR_SUBSYSTEMINFO[] struSubSystemInfo;
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
            public byte[] byRes;
        }

        [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
        public struct NET_DVR_LOOPPLAN_SUBCFG
        {
            public uint dwSize;
            public uint dwPoolTime;
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = MAX_CYCLE_CHAN_V30, ArraySubType = System.Runtime.InteropServices.UnmanagedType.Struct)]
            public NET_DVR_MATRIX_CHAN_INFO_V30[] struChanConInfo;
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
            public byte[] byRes;
        }

        [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
        public struct NET_DVR_LOOPPLAN_ARRAYCFG
        {
            public uint dwSize;
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = MAX_LOOPPLANNUM, ArraySubType = System.Runtime.InteropServices.UnmanagedType.Struct)]
            public NET_DVR_LOOPPLAN_SUBCFG[] struLoopPlanSubCfg;
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
            public byte[] byRes;
        }

        [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
        public struct NET_DVR_ALARMMODECFG
        {
            public uint dwSize;
            public byte byAlarmMode;
            public ushort wLoopTime;
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 9, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
            public byte[] byRes;
        }

        [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
        public struct NET_DVR_CODESYSTEMINFO
        {
            public byte bySerialNum;
            public byte byChan;
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 6, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
            public byte[] byRes1;
        }

        [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
        public struct NET_DVR_CODESPLITTERINFO
        {
            public uint dwSize;
            public NET_DVR_IPADDR struIP;
            public ushort wPort;
            [MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 6, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
            public byte[] byRes1;
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
            public byte[] sUserName;
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = PASSWD_LEN, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
            public byte[] sPassword;
            public byte byChan;
            public byte by485Port;
            public ushort wBaudRate;
            public byte byDataBit;
            public byte byStopBit;
            public byte byParity;
            public byte byFlowControl;
            public ushort wDecoderType;
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 6, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
            public byte[] byRes2;
        }

        [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
        public struct NET_DVR_CODESPLITTERCFG
        {
            public uint dwSize;
            public NET_DVR_CODESYSTEMINFO struCodeSubsystemInfo;
            public NET_DVR_CODESPLITTERINFO struCodeSplitterInfo;
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
            public byte[] byRes;
        }

        [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
        public struct NET_DVR_ASSOCIATECFG
        {
            public byte byAssociateType;
            public ushort wAlarmDelay;
            public byte byAlarmNum;
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
            public byte[] byRes;
        }

        [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
        public struct NET_DVR_DYNAMICDECODE
        {
            public uint dwSize;
            public NET_DVR_ASSOCIATECFG struAssociateCfg;
            public NET_DVR_PU_STREAM_CFG struPuStreamCfg;
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
            public byte[] byRes;
        }

        [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
        public struct NET_DVR_DECODESCHED
        {
            public NET_DVR_SCHEDTIME struSchedTime;
            public byte byDecodeType;
            public byte byLoopGroup;
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 6, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
            public byte[] byRes;
            public NET_DVR_PU_STREAM_CFG struDynamicDec;
        }

        [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
        public struct NET_DVR_PLANDECODE
        {
            public uint dwSize;
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = MAX_DAYS * DECODE_TIMESEGMENT, ArraySubType = System.Runtime.InteropServices.UnmanagedType.Struct)]
            public NET_DVR_DECODESCHED[] struDecodeSched;
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
            public byte[] byres;
        }

        [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
        public struct NET_DVR_VIDEOPLATFORM_ABILITY
        {
            public uint dwSize;
            public byte byCodeSubSystemNums;
            public byte byDecodeSubSystemNums;
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 12, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
            public byte[] byWindowMode;
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
            public byte[] byRes;
        }

        public const int VIDEOPLATFORM_ABILITY = 0x210; 
        
        public const int SDK_PLAYMPEG4 = 1;
        public const int SDK_HCNETSDK = 2;

        public const int INSERTTYPE = 0;
        public const int MODIFYTYPE = 1;
        public const int DELETETYPE = 2;
       
        public const int DEF_OPE_PREVIEW = 1;
        public const int DEF_OPE_TALK = 2;
        public const int DEF_OPE_SETALARM = 3;
        public const int DEF_OPE_PTZCTRL = 4;
        public const int DEF_OPE_VIDEOPARAM = 5;
        public const int DEF_OPE_PLAYBACK = 6;
        public const int DEF_OPE_REMOTECFG = 7;
        public const int DEF_OPE_GETSERVSTATE = 8;
        public const int DEF_OPE_CHECKTIME = 9;

        public const int DEF_OPE_PRE_STARTPREVIEW = 1;
        public const int DEF_OPE_PRE_STOPPREVIEW = 2;
        public const int DEF_OPE_PRE_STRATCYCPLAY = 3;
        public const int DEF_OPE_PRE_STOPCYCPLAY = 4;
        public const int DEF_OPE_PRE_STARTRECORD = 5;
        public const int DEF_OPE_PRE_STOPRECORD = 6;
        public const int DEF_OPE_PRE_CAPTURE = 7;
        public const int DEF_OPE_PRE_OPENSOUND = 8;
        public const int DEF_OPE_PRE_CLOSESOUND = 9;

        public const int DEF_OPE_TALK_STARTTALK = 1;
        public const int DEF_OPE_TALK_STOPTALK = 2;

        public const int DEF_OPE_ALARM_SETALARM = 1;
        public const int DEF_OPE_ALARM_WITHDRAWALARM = 2;

        public const int DEF_OPE_PTZ_PTZCTRL = 1;

        public const int DEF_OPE_VIDEOPARAM_SET = 1;
       
        public const int DEF_OPE_PLAYBACK_LOCALSEARCH = 1;
        public const int DEF_OPE_PLAYBACK_LOCALPLAY = 2; 
        public const int DEF_OPE_PLAYBACK_LOCALDOWNLOAD = 3;
        public const int DEF_OPE_PLAYBACK_REMOTESEARCH = 4;
        public const int DEF_OPE_PLAYBACK_REMOTEPLAYFILE = 5;
        public const int DEF_OPE_PLAYBACK_REMOTEPLAYTIME = 6;
        public const int DEF_OPE_PLAYBACK_REMOTEDOWNLOAD = 7;

        public const int DEF_OPE_REMOTE_REMOTECFG = 1;

        public const int DEF_OPE_STATE_GETSERVSTATE = 1;

        public const int DEF_OPE_CHECKT_CHECKTIME = 1;

        public const int DEF_ALARM_IO = 1;
        public const int DEF_ALARM_HARDFULL = 2;
        public const int DEF_ALARM_VL = 3;
        public const int DEF_ALARM_MV = 4;
        public const int DEF_ALARM_HARDFORMAT = 5;
        public const int DEF_ALARM_HARDERROR = 6;
        public const int DEF_ALARM_VH = 7;
        public const int DEF_ALARM_NOPATCH = 8;
        public const int DEF_ALARM_ERRORVISIT = 9;
        public const int DEF_ALARM_EXCEPTION = 10;
        public const int DEF_ALARM_RECERROR = 11;

        public const int DEF_SYS_LOGIN = 1;
        public const int DEF_SYS_LOGOUT = 2;
        public const int DEF_SYS_LOCALCFG = 3;

        public const int NAME_LEN = 32;
        public const int PASSWD_LEN = 16;
        public const int MAX_NAMELEN = 16;
        public const int MAX_RIGHT = 32;
        public const int SERIALNO_LEN = 48;
        public const int MACADDR_LEN = 6;
        public const int MAX_ETHERNET = 2;
        public const int PATHNAME_LEN = 128;

        public const int MAX_TIMESEGMENT_V30 = 8;
        public const int MAX_TIMESEGMENT = 4;

        public const int MAX_SHELTERNUM = 4;
        public const int PHONENUMBER_LEN = 32;

        public const int MAX_DISKNUM = 16;
        public const int MAX_DISKNUM_V10 = 8;

        public const int MAX_WINDOW_V30 = 32;
        public const int MAX_WINDOW = 16;
        public const int MAX_VGA_V30 = 4;
        public const int MAX_VGA = 1;

        public const int MAX_USERNUM_V30 = 32;
        public const int MAX_USERNUM = 16;
        public const int MAX_EXCEPTIONNUM_V30 = 32;
        public const int MAX_EXCEPTIONNUM = 16;
        public const int MAX_LINK = 6;

        public const int MAX_DECPOOLNUM = 4;
        public const int MAX_DECNUM = 4;
        public const int MAX_TRANSPARENTNUM = 2;
        public const int MAX_CYCLE_CHAN = 16; 
        public const int MAX_CYCLE_CHAN_V30 = 64;
        public const int MAX_DIRNAME_LENGTH = 80;
        public const int MAX_WINDOWS = 16;

        public const int MAX_STRINGNUM_V30 = 8;
        public const int MAX_STRINGNUM = 4;
        public const int MAX_STRINGNUM_EX = 8;
        public const int MAX_AUXOUT_V30 = 16;
        public const int MAX_AUXOUT = 4;
        public const int MAX_HD_GROUP = 16;
        public const int MAX_NFS_DISK = 8;

        public const int IW_ESSID_MAX_SIZE = 32;
        public const int IW_ENCODING_TOKEN_MAX = 32;
        public const int MAX_SERIAL_NUM = 64;
        public const int MAX_DDNS_NUMS = 10;
        public const int MAX_EMAIL_ADDR_LEN = 48;
        public const int MAX_EMAIL_PWD_LEN = 32;

        public const int MAXPROGRESS = 100;
        public const int MAX_SERIALNUM = 2;
        public const int CARDNUM_LEN = 20;
        public const int MAX_VIDEOOUT_V30 = 4;
        public const int MAX_VIDEOOUT = 2;

        public const int MAX_PRESET_V30 = 256;
        public const int MAX_TRACK_V30 = 256;
        public const int MAX_CRUISE_V30 = 256;
        public const int MAX_PRESET = 128;
        public const int MAX_TRACK = 128;
        public const int MAX_CRUISE = 128;

        public const int CRUISE_MAX_PRESET_NUMS = 32;

        public const int MAX_SERIAL_PORT = 8;
        public const int MAX_PREVIEW_MODE = 8;
        public const int MAX_MATRIXOUT = 16;
        public const int LOG_INFO_LEN = 11840;
        public const int DESC_LEN = 16;
        public const int PTZ_PROTOCOL_NUM = 200;

        public const int MAX_AUDIO = 1;
        public const int MAX_AUDIO_V30 = 2;
        public const int MAX_CHANNUM = 16;
        public const int MAX_ALARMIN = 16;
        public const int MAX_ALARMOUT = 4;
        
        public const int MAX_ANALOG_CHANNUM = 32;
        public const int MAX_ANALOG_ALARMOUT = 32; 
        public const int MAX_ANALOG_ALARMIN = 32;

        public const int MAX_IP_DEVICE = 32;
        public const int MAX_IP_CHANNEL = 32;
        public const int MAX_IP_ALARMIN = 128;
        public const int MAX_IP_ALARMOUT = 64;

        //SDK_V31 ATM
        public const int MAX_ATM_NUM = 1;
        public const int MAX_ACTION_TYPE = 12;
        public const int ATM_FRAMETYPE_NUM = 4;
        public const int MAX_ATM_PROTOCOL_NUM = 1025;
        public const int ATM_PROTOCOL_SORT = 4;
        public const int ATM_DESC_LEN = 32;
       
        public const int MAX_CHANNUM_V30 = MAX_ANALOG_CHANNUM + MAX_IP_CHANNEL;//64
        public const int MAX_ALARMOUT_V30 = MAX_ANALOG_ALARMOUT + MAX_IP_ALARMOUT;//96
        public const int MAX_ALARMIN_V30 = MAX_ANALOG_ALARMIN + MAX_IP_ALARMIN;//160

        public const int MAX_INTERVAL_NUM = 4;

        public const int NORMALCONNECT = 1;
        public const int MEDIACONNECT = 2;

        public const int HCDVR = 1;
        public const int MEDVR = 2;
        public const int PCDVR = 3;
        public const int HC_9000 = 4;
        public const int HF_I = 5;
        public const int PCNVR = 6;
        public const int HC_76NVR = 8;
        
        public const int DS8000HC_NVR = 0;
        public const int DS9000HC_NVR = 1;
        public const int DS8000ME_NVR = 2;
        
        public const int NET_DVR_NOERROR = 0;
        public const int NET_DVR_PASSWORD_ERROR = 1;
        public const int NET_DVR_NOENOUGHPRI = 2;
        public const int NET_DVR_NOINIT = 3;
        public const int NET_DVR_CHANNEL_ERROR = 4;
        public const int NET_DVR_OVER_MAXLINK = 5;
        public const int NET_DVR_VERSIONNOMATCH = 6;
        public const int NET_DVR_NETWORK_FAIL_CONNECT = 7;
        public const int NET_DVR_NETWORK_SEND_ERROR = 8;
        public const int NET_DVR_NETWORK_RECV_ERROR = 9;
        public const int NET_DVR_NETWORK_RECV_TIMEOUT = 10;
        public const int NET_DVR_NETWORK_ERRORDATA = 11;
        public const int NET_DVR_ORDER_ERROR = 12;
        public const int NET_DVR_OPERNOPERMIT = 13;
        public const int NET_DVR_COMMANDTIMEOUT = 14;
        public const int NET_DVR_ERRORSERIALPORT = 15;
        public const int NET_DVR_ERRORALARMPORT = 16;
        public const int NET_DVR_PARAMETER_ERROR = 17;
        public const int NET_DVR_CHAN_EXCEPTION = 18;
        public const int NET_DVR_NODISK = 19;
        public const int NET_DVR_ERRORDISKNUM = 20;
        public const int NET_DVR_DISK_FULL = 21;
        public const int NET_DVR_DISK_ERROR = 22;
        public const int NET_DVR_NOSUPPORT = 23;
        public const int NET_DVR_BUSY = 24;
        public const int NET_DVR_MODIFY_FAIL = 25;
        public const int NET_DVR_PASSWORD_FORMAT_ERROR = 26;
        public const int NET_DVR_DISK_FORMATING = 27;
        public const int NET_DVR_DVRNORESOURCE = 28;
        public const int NET_DVR_DVROPRATEFAILED = 29;
        public const int NET_DVR_OPENHOSTSOUND_FAIL = 30;
        public const int NET_DVR_DVRVOICEOPENED = 31;
        public const int NET_DVR_TIMEINPUTERROR = 32;
        public const int NET_DVR_NOSPECFILE = 33;
        public const int NET_DVR_CREATEFILE_ERROR = 34;
        public const int NET_DVR_FILEOPENFAIL = 35;
        public const int NET_DVR_OPERNOTFINISH = 36;
        public const int NET_DVR_GETPLAYTIMEFAIL = 37;
        public const int NET_DVR_PLAYFAIL = 38;
        public const int NET_DVR_FILEFORMAT_ERROR = 39;
        public const int NET_DVR_DIR_ERROR = 40;
        public const int NET_DVR_ALLOC_RESOURCE_ERROR = 41;
        public const int NET_DVR_AUDIO_MODE_ERROR = 42;
        public const int NET_DVR_NOENOUGH_BUF = 43;
        public const int NET_DVR_CREATESOCKET_ERROR = 44;
        public const int NET_DVR_SETSOCKET_ERROR = 45;
        public const int NET_DVR_MAX_NUM = 46;
        public const int NET_DVR_USERNOTEXIST = 47;
        public const int NET_DVR_WRITEFLASHERROR = 48;
        public const int NET_DVR_UPGRADEFAIL = 49;
        public const int NET_DVR_CARDHAVEINIT = 50;
        public const int NET_DVR_PLAYERFAILED = 51;
        public const int NET_DVR_MAX_USERNUM = 52;
        public const int NET_DVR_GETLOCALIPANDMACFAIL = 53;
        public const int NET_DVR_NOENCODEING = 54;
        public const int NET_DVR_IPMISMATCH = 55;
        public const int NET_DVR_MACMISMATCH = 56;
        public const int NET_DVR_UPGRADELANGMISMATCH = 57;
        public const int NET_DVR_MAX_PLAYERPORT = 58;
        public const int NET_DVR_NOSPACEBACKUP = 59;
        public const int NET_DVR_NODEVICEBACKUP = 60;
        public const int NET_DVR_PICTURE_BITS_ERROR = 61;
        public const int NET_DVR_PICTURE_DIMENSION_ERROR = 62;
        public const int NET_DVR_PICTURE_SIZ_ERROR = 63;
        public const int NET_DVR_LOADPLAYERSDKFAILED = 64;
        public const int NET_DVR_LOADPLAYERSDKPROC_ERROR = 65;
        public const int NET_DVR_LOADDSSDKFAILED = 66;
        public const int NET_DVR_LOADDSSDKPROC_ERROR = 67;
        public const int NET_DVR_DSSDK_ERROR = 68;
        public const int NET_DVR_VOICEMONOPOLIZE = 69;
        public const int NET_DVR_JOINMULTICASTFAILED = 70;
        public const int NET_DVR_CREATEDIR_ERROR = 71;
        public const int NET_DVR_BINDSOCKET_ERROR = 72;
        public const int NET_DVR_SOCKETCLOSE_ERROR = 73;
        public const int NET_DVR_USERID_ISUSING = 74;
        public const int NET_DVR_SOCKETLISTEN_ERROR = 75;
        public const int NET_DVR_PROGRAM_EXCEPTION = 76;
        public const int NET_DVR_WRITEFILE_FAILED = 77;
        public const int NET_DVR_FORMAT_READONLY = 78;
        public const int NET_DVR_WITHSAMEUSERNAME = 79;
        public const int NET_DVR_DEVICETYPE_ERROR = 80;
        public const int NET_DVR_LANGUAGE_ERROR = 81;
        public const int NET_DVR_PARAVERSION_ERROR = 82;
        public const int NET_DVR_IPCHAN_NOTALIVE = 83; 
        public const int NET_DVR_RTSP_SDK_ERROR = 84;
        public const int NET_DVR_CONVERT_SDK_ERROR = 85;
        public const int NET_DVR_IPC_COUNT_OVERFLOW = 86;

        public const int NET_PLAYM4_NOERROR = 500;//no error
        public const int NET_PLAYM4_PARA_OVER = 501;//input parameter is invalid
        public const int NET_PLAYM4_ORDER_ERROR = 502;//The order of the function to be called is error
        public const int NET_PLAYM4_TIMER_ERROR = 503;//Create multimedia clock failed
        public const int NET_PLAYM4_DEC_VIDEO_ERROR = 504;//Decode video data failed
        public const int NET_PLAYM4_DEC_AUDIO_ERROR = 505;//Decode audio data failed
        public const int NET_PLAYM4_ALLOC_MEMORY_ERROR = 506;//Allocate memory failed
        public const int NET_PLAYM4_OPEN_FILE_ERROR = 507;//Open the file failed
        public const int NET_PLAYM4_CREATE_OBJ_ERROR = 508;//Create thread or event failed
        public const int NET_PLAYM4_CREATE_DDRAW_ERROR = 509;//Create DirectDraw object failed
        public const int NET_PLAYM4_CREATE_OFFSCREEN_ERROR = 510;//failed when creating off-screen surface
        public const int NET_PLAYM4_BUF_OVER = 511;//buffer is overflow
        public const int NET_PLAYM4_CREATE_SOUND_ERROR = 512;//failed when creating audio device
        public const int NET_PLAYM4_SET_VOLUME_ERROR = 513;//Set volume failed
        public const int NET_PLAYM4_SUPPORT_FILE_ONLY = 514;//The function only support play file
        public const int NET_PLAYM4_SUPPORT_STREAM_ONLY = 515;//The function only support play stream
        public const int NET_PLAYM4_SYS_NOT_SUPPORT = 516;//System not support
        public const int NET_PLAYM4_FILEHEADER_UNKNOWN = 517;//No file header
        public const int NET_PLAYM4_VERSION_INCORRECT = 518;//The version of decoder and encoder is not adapted
        public const int NET_PALYM4_INIT_DECODER_ERROR = 519;//Initialize decoder failed
        public const int NET_PLAYM4_CHECK_FILE_ERROR = 520;//The file data is unknown
        public const int NET_PLAYM4_INIT_TIMER_ERROR = 521;//Initialize multimedia clock failed
        public const int NET_PLAYM4_BLT_ERROR = 522;//Blt failed
        public const int NET_PLAYM4_UPDATE_ERROR = 523;//Update failed
        public const int NET_PLAYM4_OPEN_FILE_ERROR_MULTI = 524;//openfile error, streamtype is multi
        public const int NET_PLAYM4_OPEN_FILE_ERROR_VIDEO = 525;//openfile error, streamtype is video
        public const int NET_PLAYM4_JPEG_COMPRESS_ERROR = 526;//JPEG compress error
        public const int NET_PLAYM4_EXTRACT_NOT_SUPPORT = 527;//Don't support the version of this file
        public const int NET_PLAYM4_EXTRACT_DATA_ERROR = 528;//extract video data failed
      

        public const int NET_DVR_SUPPORT_DDRAW = 1;
        public const int NET_DVR_SUPPORT_BLT = 2;
        public const int NET_DVR_SUPPORT_BLTFOURCC = 4;
        public const int NET_DVR_SUPPORT_BLTSHRINKX = 8;
        public const int NET_DVR_SUPPORT_BLTSHRINKY = 16;
        public const int NET_DVR_SUPPORT_BLTSTRETCHX = 32;
        public const int NET_DVR_SUPPORT_BLTSTRETCHY = 64;
        public const int NET_DVR_SUPPORT_SSE = 128;
        public const int NET_DVR_SUPPORT_MMX = 256;
       
        public const int LIGHT_PWRON = 2;
        public const int WIPER_PWRON = 3;
        public const int FAN_PWRON = 4;
        public const int HEATER_PWRON = 5;
        public const int AUX_PWRON1 = 6;
        public const int AUX_PWRON2 = 7; 
        public const int SET_PRESET = 8;
        public const int CLE_PRESET = 9; 

        public const int ZOOM_IN = 11;
        public const int ZOOM_OUT = 12;
        public const int FOCUS_NEAR = 13; 
        public const int FOCUS_FAR = 14;
        public const int IRIS_OPEN = 15;
        public const int IRIS_CLOSE = 16;

        public const int TILT_UP = 21;
        public const int TILT_DOWN = 22;
        public const int PAN_LEFT = 23;
        public const int PAN_RIGHT = 24;
        public const int UP_LEFT = 25;
        public const int UP_RIGHT = 26;
        public const int DOWN_LEFT = 27;
        public const int DOWN_RIGHT = 28;
        public const int PAN_AUTO = 29;

        public const int FILL_PRE_SEQ = 30;
        public const int SET_SEQ_DWELL = 31;
        public const int SET_SEQ_SPEED = 32;
        public const int CLE_PRE_SEQ = 33;
        public const int STA_MEM_CRUISE = 34;
        public const int STO_MEM_CRUISE = 35;
        public const int RUN_CRUISE = 36;
        public const int RUN_SEQ = 37;
        public const int STOP_SEQ = 38;
        public const int GOTO_PRESET = 39;
        public const int DEL_SEQ = 43; 

        /*************************************************      
        NET_DVR_PlayBackControl
        NET_DVR_PlayControlLocDisplay
        NET_DVR_DecPlayBackCtrl
        **************************************************/
        public const int NET_DVR_PLAYSTART = 1;
        public const int NET_DVR_PLAYSTOP = 2;
        public const int NET_DVR_PLAYPAUSE = 3;
        public const int NET_DVR_PLAYRESTART = 4;
        public const int NET_DVR_PLAYFAST = 5;
        public const int NET_DVR_PLAYSLOW = 6;
        public const int NET_DVR_PLAYNORMAL = 7;
        public const int NET_DVR_PLAYFRAME = 8;
        public const int NET_DVR_PLAYSTARTAUDIO = 9;
        public const int NET_DVR_PLAYSTOPAUDIO = 10;
        public const int NET_DVR_PLAYAUDIOVOLUME = 11;
        public const int NET_DVR_PLAYSETPOS = 12;
        public const int NET_DVR_PLAYGETPOS = 13;
        public const int NET_DVR_PLAYGETTIME = 14;
        public const int NET_DVR_PLAYGETFRAME = 15;
        public const int NET_DVR_GETTOTALFRAMES = 16;
        public const int NET_DVR_GETTOTALTIME = 17;
        public const int NET_DVR_THROWBFRAME = 20;
        public const int NET_DVR_SETSPEED = 24;
        public const int NET_DVR_KEEPALIVE = 25;
        public const int NET_DVR_PLAYSETTIME = 26;
        public const int NET_DVR_PLAYGETTOTALLEN = 27;
        public const int NET_DVR_PLAY_FORWARD = 29; 
        public const int NET_DVR_PLAY_REVERSE = 30; 
        public const int NET_DVR_SET_DECODEFFRAMETYPE = 31;
        public const int NET_DVR_SET_TRANS_TYPE = 32; 
        public const int NET_DVR_PLAY_CONVERT = 33; 
        public const int NET_DVR_START_DRAWFRAME = 34; 
        public const int NET_DVR_STOP_DRAWFRAME = 35;

        /* key value send to CONFIG program */
        public const int KEY_CODE_1 = 1;
        public const int KEY_CODE_2 = 2;
        public const int KEY_CODE_3 = 3;
        public const int KEY_CODE_4 = 4;
        public const int KEY_CODE_5 = 5;
        public const int KEY_CODE_6 = 6;
        public const int KEY_CODE_7 = 7;
        public const int KEY_CODE_8 = 8;
        public const int KEY_CODE_9 = 9;
        public const int KEY_CODE_0 = 10;
        public const int KEY_CODE_POWER = 11;
        public const int KEY_CODE_MENU = 12;
        public const int KEY_CODE_ENTER = 13;
        public const int KEY_CODE_CANCEL = 14;
        public const int KEY_CODE_UP = 15;
        public const int KEY_CODE_DOWN = 16;
        public const int KEY_CODE_LEFT = 17;
        public const int KEY_CODE_RIGHT = 18;
        public const int KEY_CODE_EDIT = 19;
        public const int KEY_CODE_ADD = 20;
        public const int KEY_CODE_MINUS = 21;
        public const int KEY_CODE_PLAY = 22;
        public const int KEY_CODE_REC = 23;
        public const int KEY_CODE_PAN = 24;
        public const int KEY_CODE_M = 25;
        public const int KEY_CODE_A = 26;
        public const int KEY_CODE_F1 = 27;
        public const int KEY_CODE_F2 = 28;

        /* for PTZ control */
        public const int KEY_PTZ_UP_START = KEY_CODE_UP;
        public const int KEY_PTZ_UP_STOP = 32;

        public const int KEY_PTZ_DOWN_START = KEY_CODE_DOWN;
        public const int KEY_PTZ_DOWN_STOP = 33;


        public const int KEY_PTZ_LEFT_START = KEY_CODE_LEFT;
        public const int KEY_PTZ_LEFT_STOP = 34;

        public const int KEY_PTZ_RIGHT_START = KEY_CODE_RIGHT;
        public const int KEY_PTZ_RIGHT_STOP = 35;

        public const int KEY_PTZ_AP1_START = KEY_CODE_EDIT;
        public const int KEY_PTZ_AP1_STOP = 36;

        public const int KEY_PTZ_AP2_START = KEY_CODE_PAN;
        public const int KEY_PTZ_AP2_STOP = 37;

        public const int KEY_PTZ_FOCUS1_START = KEY_CODE_A;
        public const int KEY_PTZ_FOCUS1_STOP = 38;

        public const int KEY_PTZ_FOCUS2_START = KEY_CODE_M;
        public const int KEY_PTZ_FOCUS2_STOP = 39;

        public const int KEY_PTZ_B1_START = 40;
        public const int KEY_PTZ_B1_STOP = 41;

        public const int KEY_PTZ_B2_START = 42;
        public const int KEY_PTZ_B2_STOP = 43;

        public const int KEY_CODE_11 = 44;
        public const int KEY_CODE_12 = 45;
        public const int KEY_CODE_13 = 46;
        public const int KEY_CODE_14 = 47;
        public const int KEY_CODE_15 = 48;
        public const int KEY_CODE_16 = 49;
     
        //NET_DVR_SetDVRConfig NET_DVR_GetDVRConfig
        public const int NET_DVR_GET_DEVICECFG = 100;
        public const int NET_DVR_SET_DEVICECFG = 101;
        public const int NET_DVR_GET_NETCFG = 102;
        public const int NET_DVR_SET_NETCFG = 103;
        public const int NET_DVR_GET_PICCFG = 104;
        public const int NET_DVR_SET_PICCFG = 105;
        public const int NET_DVR_GET_COMPRESSCFG = 106;
        public const int NET_DVR_SET_COMPRESSCFG = 107;
        public const int NET_DVR_GET_RECORDCFG = 108;
        public const int NET_DVR_SET_RECORDCFG = 109;
        public const int NET_DVR_GET_DECODERCFG = 110;
        public const int NET_DVR_SET_DECODERCFG = 111;
        public const int NET_DVR_GET_RS232CFG = 112;
        public const int NET_DVR_SET_RS232CFG = 113;
        public const int NET_DVR_GET_ALARMINCFG = 114;
        public const int NET_DVR_SET_ALARMINCFG = 115;
        public const int NET_DVR_GET_ALARMOUTCFG = 116;
        public const int NET_DVR_SET_ALARMOUTCFG = 117;
        public const int NET_DVR_GET_TIMECFG = 118;
        public const int NET_DVR_SET_TIMECFG = 119;
        public const int NET_DVR_GET_PREVIEWCFG = 120;
        public const int NET_DVR_SET_PREVIEWCFG = 121;
        public const int NET_DVR_GET_VIDEOOUTCFG = 122;
        public const int NET_DVR_SET_VIDEOOUTCFG = 123;
        public const int NET_DVR_GET_USERCFG = 124;
        public const int NET_DVR_SET_USERCFG = 125;
        public const int NET_DVR_GET_EXCEPTIONCFG = 126;
        public const int NET_DVR_SET_EXCEPTIONCFG = 127;
        public const int NET_DVR_GET_ZONEANDDST = 128;
        public const int NET_DVR_SET_ZONEANDDST = 129;
        public const int NET_DVR_GET_SHOWSTRING = 130;
        public const int NET_DVR_SET_SHOWSTRING = 131;
        public const int NET_DVR_GET_EVENTCOMPCFG = 132;
        public const int NET_DVR_SET_EVENTCOMPCFG = 133;

        public const int NET_DVR_GET_AUXOUTCFG = 140;
        public const int NET_DVR_SET_AUXOUTCFG = 141;
        public const int NET_DVR_GET_PREVIEWCFG_AUX = 142;
        public const int NET_DVR_SET_PREVIEWCFG_AUX = 143;

        public const int NET_DVR_GET_PICCFG_EX = 200;
        public const int NET_DVR_SET_PICCFG_EX = 201;
        public const int NET_DVR_GET_USERCFG_EX = 202;
        public const int NET_DVR_SET_USERCFG_EX = 203;
        public const int NET_DVR_GET_COMPRESSCFG_EX = 204;
        public const int NET_DVR_SET_COMPRESSCFG_EX = 205;

        public const int NET_DVR_GET_NETAPPCFG = 222;//NTP/DDNS/EMAIL
        public const int NET_DVR_SET_NETAPPCFG = 223;//NTP/DDNS/EMAIL
        public const int NET_DVR_GET_NTPCFG = 224;//NTP
        public const int NET_DVR_SET_NTPCFG = 225;//NTP
        public const int NET_DVR_GET_DDNSCFG = 226;//DDNS
        public const int NET_DVR_SET_DDNSCFG = 227;//DDNS

        //NET_DVR_EMAILPARA
        public const int NET_DVR_GET_EMAILCFG = 228;//EMAIL
        public const int NET_DVR_SET_EMAILCFG = 229;//EMAIL

        public const int NET_DVR_GET_NFSCFG = 230;/* NFS disk config */
        public const int NET_DVR_SET_NFSCFG = 231;/* NFS disk config */

        public const int NET_DVR_GET_SHOWSTRING_EX = 238;
        public const int NET_DVR_SET_SHOWSTRING_EX = 239;
        public const int NET_DVR_GET_NETCFG_OTHER = 244;
        public const int NET_DVR_SET_NETCFG_OTHER = 245;

        //NET_DVR_EMAILCFG
        public const int NET_DVR_GET_EMAILPARACFG = 250;//Get EMAIL parameters
        public const int NET_DVR_SET_EMAILPARACFG = 251;//Setup EMAIL parameters

        public const int NET_DVR_GET_DDNSCFG_EX = 274;
        public const int NET_DVR_SET_DDNSCFG_EX = 275;

        public const int NET_DVR_SET_PTZPOS = 292;
        public const int NET_DVR_GET_PTZPOS = 293;
        public const int NET_DVR_GET_PTZSCOPE = 294;

        //NET_DVR_NETCFG_V30
        public const int NET_DVR_GET_NETCFG_V30 = 1000;
        public const int NET_DVR_SET_NETCFG_V30 = 1001;

        //NET_DVR_PICCFG_V30
        public const int NET_DVR_GET_PICCFG_V30 = 1002;
        public const int NET_DVR_SET_PICCFG_V30 = 1003;

        //NET_DVR_RECORD_V30
        public const int NET_DVR_GET_RECORDCFG_V30 = 1004;
        public const int NET_DVR_SET_RECORDCFG_V30 = 1005;

        //NET_DVR_USER_V30
        public const int NET_DVR_GET_USERCFG_V30 = 1006;
        public const int NET_DVR_SET_USERCFG_V30 = 1007;

        //NET_DVR_DDNSPARA_V30
        public const int NET_DVR_GET_DDNSCFG_V30 = 1010;
        public const int NET_DVR_SET_DDNSCFG_V30 = 1011;

        //EMAIL NET_DVR_EMAILCFG_V30
        public const int NET_DVR_GET_EMAILCFG_V30 = 1012;
        public const int NET_DVR_SET_EMAILCFG_V30 = 1013;

        //NET_DVR_CRUISE_PARA
        public const int NET_DVR_GET_CRUISE = 1020;
        public const int NET_DVR_SET_CRUISE = 1021;

        //NET_DVR_ALARMINCFG_V30
        public const int NET_DVR_GET_ALARMINCFG_V30 = 1024;
        public const int NET_DVR_SET_ALARMINCFG_V30 = 1025;

        //NET_DVR_ALARMOUTCFG_V30
        public const int NET_DVR_GET_ALARMOUTCFG_V30 = 1026;
        public const int NET_DVR_SET_ALARMOUTCFG_V30 = 1027;

        //NET_DVR_VIDEOOUT_V30
        public const int NET_DVR_GET_VIDEOOUTCFG_V30 = 1028;
        public const int NET_DVR_SET_VIDEOOUTCFG_V30 = 1029;

        //NET_DVR_SHOWSTRING_V30
        public const int NET_DVR_GET_SHOWSTRING_V30 = 1030;
        public const int NET_DVR_SET_SHOWSTRING_V30 = 1031;

        //NET_DVR_EXCEPTION_V30
        public const int NET_DVR_GET_EXCEPTIONCFG_V30 = 1034;
        public const int NET_DVR_SET_EXCEPTIONCFG_V30 = 1035;

        //NET_DVR_RS232CFG_V30
        public const int NET_DVR_GET_RS232CFG_V30 = 1036;
        public const int NET_DVR_SET_RS232CFG_V30 = 1037;

        //NET_DVR_NET_DISKCFG
        public const int NET_DVR_GET_NET_DISKCFG = 1038;
        public const int NET_DVR_SET_NET_DISKCFG = 1039;

        //NET_DVR_COMPRESSIONCFG_V30
        public const int NET_DVR_GET_COMPRESSCFG_V30 = 1040;
        public const int NET_DVR_SET_COMPRESSCFG_V30 = 1041;

        //NET_DVR_DECODERCFG_V30
        public const int NET_DVR_GET_DECODERCFG_V30 = 1042;
        public const int NET_DVR_SET_DECODERCFG_V30 = 1043;

        //NET_DVR_PREVIEWCFG_V30
        public const int NET_DVR_GET_PREVIEWCFG_V30 = 1044;
        public const int NET_DVR_SET_PREVIEWCFG_V30 = 1045;

        //NET_DVR_PREVIEWCFG_AUX_V30
        public const int NET_DVR_GET_PREVIEWCFG_AUX_V30 = 1046;
        public const int NET_DVR_SET_PREVIEWCFG_AUX_V30 = 1047;

        //IP NET_DVR_IPPARACFG
        public const int NET_DVR_GET_IPPARACFG = 1048;  
        public const int NET_DVR_SET_IPPARACFG = 1049;

        //IP NET_DVR_IPALARMINCFG
        public const int NET_DVR_GET_IPALARMINCFG = 1050;
        public const int NET_DVR_SET_IPALARMINCFG = 1051;

        //IP NET_DVR_IPALARMOUTCFG
        public const int NET_DVR_GET_IPALARMOUTCFG = 1052; 
        public const int NET_DVR_SET_IPALARMOUTCFG = 1053;

        //NET_DVR_HDCFG
        public const int NET_DVR_GET_HDCFG = 1054;
        public const int NET_DVR_SET_HDCFG = 1055;

        //NET_DVR_HDGROUP_CFG
        public const int NET_DVR_GET_HDGROUP_CFG = 1056;
        public const int NET_DVR_SET_HDGROUP_CFG = 1057;
    
        //NET_DVR_COMPRESSION_AUDIO
        public const int NET_DVR_GET_COMPRESSCFG_AUD = 1058;
        public const int NET_DVR_SET_COMPRESSCFG_AUD = 1059;

        //IP NET_DVR_IPPARACFG_V31
        public const int NET_DVR_GET_IPPARACFG_V31 = 1060;
        public const int NET_DVR_SET_IPPARACFG_V31 = 1061; 

        public const int NET_DVR_BARRIERGATE_CTRL = 3128;

        public const int NET_DVR_FILE_SUCCESS = 1000;
        public const int NET_DVR_FILE_NOFIND = 1001;
        public const int NET_DVR_ISFINDING = 1002;
        public const int NET_DVR_NOMOREFILE = 1003;
        public const int NET_DVR_FILE_EXCEPTION = 1004;        

        public const int COMM_ALARM = 4352;
        public const int COMM_TRADEINFO = 5376;
        public const int COMM_ALARM_V30 = 16384;
        public const int COMM_ALARM_V40 = 0x4007;
        public const int COMM_UPLOAD_PLATE_RESULT = 0x2800;
        public const int COMM_UPLOAD_FACESNAP_RESULT = 0x1112;
        public const int COMM_ITS_PLATE_RESULT = 0x3050;
        public const int COMM_IPCCFG = 16385;
        public const int COMM_IPCCFG_V31 = 16386;
        public const int COMM_ALARM_RULE_CALC = 0x1110;
        public const int COMM_ALARM_PDC = 0x1103;        

        public const int EXCEPTION_EXCHANGE = 32768;
        public const int EXCEPTION_AUDIOEXCHANGE = 32769;
        public const int EXCEPTION_ALARM = 32770;
        public const int EXCEPTION_PREVIEW = 32771;
        public const int EXCEPTION_SERIAL = 32772;
        public const int EXCEPTION_RECONNECT = 32773;
        public const int EXCEPTION_ALARMRECONNECT = 32774;
        public const int EXCEPTION_SERIALRECONNECT = 32775;
        public const int EXCEPTION_PLAYBACK = 32784;
        public const int EXCEPTION_DISKFMT = 32785;

        public const int NET_DVR_SYSHEAD = 1;
        public const int NET_DVR_STREAMDATA = 2;
        public const int NET_DVR_AUDIOSTREAMDATA = 3;
        public const int NET_DVR_STD_VIDEODATA = 4;
        public const int NET_DVR_STD_AUDIODATA = 5;

        public const int NET_DVR_REALPLAYEXCEPTION = 111;
        public const int NET_DVR_REALPLAYNETCLOSE = 112;
        public const int NET_DVR_REALPLAY5SNODATA = 113;
        public const int NET_DVR_REALPLAYRECONNECT = 114;

        public const int NET_DVR_PLAYBACKOVER = 101;
        public const int NET_DVR_PLAYBACKEXCEPTION = 102;
        public const int NET_DVR_PLAYBACKNETCLOSE = 103;
        public const int NET_DVR_PLAYBACK5SNODATA = 104;

        public const int DVR = 1;
        public const int ATMDVR = 2;
        public const int DVS = 3;
        public const int DEC = 4;
        public const int ENC_DEC = 5;
        public const int DVR_HC = 6;
        public const int DVR_HT = 7;
        public const int DVR_HF = 8;
        public const int DVR_HS = 9;
        public const int DVR_HTS = 10;
        public const int DVR_HB = 11;
        public const int DVR_HCS = 12;
        public const int DVS_A = 13; 
        public const int DVR_HC_S = 14; 
        public const int DVR_HT_S = 15;
        public const int DVR_HF_S = 16;
        public const int DVR_HS_S = 17;
        public const int ATMDVR_S = 18;
        public const int LOWCOST_DVR = 19;
        public const int DEC_MAT = 20;
        public const int DVR_MOBILE = 21;
        public const int DVR_HD_S = 22;
        public const int DVR_HD_SL = 23;
        public const int DVR_HC_SL = 24;
        public const int DVR_HS_ST = 25;
        public const int DVS_HW = 26;
        public const int DS630X_D = 27;
        public const int IPCAM = 30;
        public const int MEGA_IPCAM = 31;
        public const int IPCAM_X62MF = 32;
        public const int IPDOME = 40; 
        public const int IPDOME_MEGA200 = 41;
        public const int IPDOME_MEGA130 = 42;
        public const int IPMOD = 50;
        public const int DS71XX_H = 71;
        public const int DS72XX_H_S = 72;
        public const int DS73XX_H_S = 73;
        public const int DS76XX_H_S = 76;
        public const int DS81XX_HS_S = 81;
        public const int DS81XX_HL_S = 82;
        public const int DS81XX_HC_S = 83;
        public const int DS81XX_HD_S = 84;
        public const int DS81XX_HE_S = 85;
        public const int DS81XX_HF_S = 86;
        public const int DS81XX_AH_S = 87;
        public const int DS81XX_AHF_S = 88;
        public const int DS90XX_HF_S = 90;  
        public const int DS91XX_HF_S = 91; 
        public const int DS91XX_HD_S = 92;

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_TIME
        {
            public int dwYear;
            public int dwMonth;
            public int dwDay;
            public int dwHour;
            public int dwMinute;
            public int dwSecond;
        }
        
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_SCHEDTIME
        {
            public byte byStartHour;
            public byte byStartMin;
            public byte byStopHour;
            public byte byStopMin;
        }

        public const int NOACTION = 0;
        public const int WARNONMONITOR = 1;
        public const int WARNONAUDIOOUT = 2;
        public const int UPTOCENTER = 4;
        public const int TRIGGERALARMOUT = 8;

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_HANDLEEXCEPTION_V30
        {
            public uint dwHandleType;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_ALARMOUT_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byRelAlarmOut;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_HANDLEEXCEPTION
        {
            public uint dwHandleType;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_ALARMOUT, ArraySubType = UnmanagedType.I1)]
            public byte[] byRelAlarmOut;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_DEVICECFG
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sDVRName;
            public uint dwDVRID;
            public uint dwRecycleRecord;
            
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = SERIALNO_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sSerialNumber;
            public uint dwSoftwareVersion;
            public uint dwSoftwareBuildDate;
            public uint dwDSPSoftwareVersion;
            public uint dwDSPSoftwareBuildDate;
            public uint dwPanelVersion;
            public uint dwHardwareVersion;
            public byte byAlarmInPortNum;
            public byte byAlarmOutPortNum;
            public byte byRS232Num;
            public byte byRS485Num;
            public byte byNetworkPortNum;
            public byte byDiskCtrlNum;
            public byte byDiskNum;
            public byte byDVRType;
            public byte byChanNum;
            public byte byStartChan;
            public byte byDecordChans;
            public byte byVGANum;
            public byte byUSBNum;
            public byte byAuxoutNum;
            public byte byAudioNum;
            public byte byIPChanNum;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_IPADDR
        {
            /// char[16]
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.I1)]
            public byte[] sIpV4;

            /// BYTE[128]
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 128, ArraySubType = UnmanagedType.I1)]
            public byte[] byIPv6;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_ETHERNET_V30
        {
            public NET_DVR_IPADDR struDVRIP;
            public NET_DVR_IPADDR struDVRIPMask;
            public uint dwNetInterface;
            public ushort wDVRPort;
            public ushort wMTU;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MACADDR_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byMACAddr;
        }
        
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_ETHERNET
        {
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sDVRIP;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sDVRIPMask;
            public uint dwNetInterface;
            public ushort wDVRPort;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MACADDR_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byMACAddr;
        }

        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_PPPOECFG
        {
            public uint dwPPPOE;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sPPPoEUser;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = PASSWD_LEN)]
            public string sPPPoEPassword;
            public NET_DVR_IPADDR struPPPoEIP;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_NETCFG_V30
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_ETHERNET, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_ETHERNET_V30[] struEtherNet;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_IPADDR[] struRes1;
            public NET_DVR_IPADDR struAlarmHostIpAddr;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.U2)]
            public ushort[] wRes2;
            public ushort wAlarmHostIpPort;
            public byte byUseDhcp;
            public byte byIPv6Mode;
            public NET_DVR_IPADDR struDnsServer1IpAddr;
            public NET_DVR_IPADDR struDnsServer2IpAddr;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DOMAIN_NAME, ArraySubType = UnmanagedType.I1)]
            public byte[] byIpResolver;
            public ushort wIpResolverPort;
            public ushort wHttpPortNo;
            public NET_DVR_IPADDR struMulticastIpAddr;
            public NET_DVR_IPADDR struGatewayIpAddr;
            public NET_DVR_PPPOECFG struPPPoE;
            public byte byEnablePrivateMulticastDiscovery;
            public byte byEnableOnvifMulticastDiscovery;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 62, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;

            public void init()
            {
                struEtherNet = new NET_DVR_ETHERNET_V30[MAX_ETHERNET];
                struAlarmHostIpAddr = new NET_DVR_IPADDR();
                struDnsServer1IpAddr = new NET_DVR_IPADDR();
                struDnsServer2IpAddr = new NET_DVR_IPADDR();
                byIpResolver = new byte[MAX_DOMAIN_NAME];
                struMulticastIpAddr = new NET_DVR_IPADDR();
                struGatewayIpAddr = new NET_DVR_IPADDR();
                struPPPoE = new NET_DVR_PPPOECFG();
            }
        }
       
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_NETCFG
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_ETHERNET, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_ETHERNET[] struEtherNet;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sManageHostIP;
            public ushort wManageHostPort;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sIPServerIP;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sMultiCastIP;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sGatewayIP;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sNFSIP;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = PATHNAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sNFSDirectory;
            public uint dwPPPOE;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sPPPoEUser;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = PASSWD_LEN)]
            public string sPPPoEPassword;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sPPPoEIP;
            public ushort wHttpPort;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_MOTION_V30
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 96 * 64, ArraySubType = UnmanagedType.I1)]
            public byte[] byMotionScope;
            public byte byMotionSensitive;
            public byte byEnableHandleMotion;
            public byte byPrecision;
            public byte reservedData;
            public NET_DVR_HANDLEEXCEPTION_V30 struMotionHandleType;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DAYS * MAX_TIMESEGMENT_V30, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_SCHEDTIME[] struAlarmTime;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byRelRecordChan;
        }

        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_MOTION
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 396, ArraySubType = UnmanagedType.I1)]
            public byte[] byMotionScope;
            public byte byMotionSensitive;
            public byte byEnableHandleMotion;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 2)]
            public string reservedData;
            public NET_DVR_HANDLEEXCEPTION strMotionHandleType;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DAYS * MAX_TIMESEGMENT, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_SCHEDTIME[] struAlarmTime;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM, ArraySubType = UnmanagedType.I1)]
            public byte[] byRelRecordChan;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_HIDEALARM_V30
        {
            public uint dwEnableHideAlarm;
            public ushort wHideAlarmAreaTopLeftX;
            public ushort wHideAlarmAreaTopLeftY;
            public ushort wHideAlarmAreaWidth;
            public ushort wHideAlarmAreaHeight;
            public NET_DVR_HANDLEEXCEPTION_V30 strHideAlarmHandleType;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DAYS * MAX_TIMESEGMENT_V30, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_SCHEDTIME[] struAlarmTime;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_HIDEALARM
        {
            public uint dwEnableHideAlarm;
            public ushort wHideAlarmAreaTopLeftX;
            public ushort wHideAlarmAreaTopLeftY;
            public ushort wHideAlarmAreaWidth;
            public ushort wHideAlarmAreaHeight;
            public NET_DVR_HANDLEEXCEPTION strHideAlarmHandleType;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DAYS * MAX_TIMESEGMENT, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_SCHEDTIME[] struAlarmTime;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_VILOST_V30
        {
            public byte byEnableHandleVILost;
            public NET_DVR_HANDLEEXCEPTION_V30 strVILostHandleType;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 56, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_SCHEDTIME[] struAlarmTime;
        }
        
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_VILOST
        {
            public byte byEnableHandleVILost;
            public NET_DVR_HANDLEEXCEPTION strVILostHandleType;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DAYS * MAX_TIMESEGMENT_V30, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_SCHEDTIME[] struAlarmTime;
        }
        
        [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
        public struct NET_DVR_SHELTER
        {
            public ushort wHideAreaTopLeftX;
            public ushort wHideAreaTopLeftY;
            public ushort wHideAreaWidth;
            public ushort wHideAreaHeight;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_COLOR
        {
            public byte byBrightness;
            public byte byContrast;
            public byte bySaturation;
            public byte byHue;
        }

        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_PICCFG_V30
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = NAME_LEN)]
            //  [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public string sChanName;
            public uint dwVideoFormat;
            public NET_DVR_COLOR struColor;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 60)]
            public string reservedData;
           
            public uint dwShowChanName;
            public ushort wShowNameTopLeftX;
            public ushort wShowNameTopLeftY;
            
            public NET_DVR_VILOST_V30 struVILost;
            public NET_DVR_VILOST_V30 struRes;
            
            public NET_DVR_MOTION_V30 struMotion;
            
            public NET_DVR_HIDEALARM_V30 struHideAlarm;
            
            public uint dwEnableHide;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_SHELTERNUM, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_SHELTER[] struShelter;
            //OSD
            public uint dwShowOsd;
            public ushort wOSDTopLeftX;
            public ushort wOSDTopLeftY;
            public byte byOSDType;

            public byte byDispWeek;
            public byte byOSDAttrib;           

            public byte byHourOSDType;
            public byte byFontSize;
            public byte byOSDColorType;	
            public byte byAlignment;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 61, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;

        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_PICCFG_EX
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sChanName;
            public uint dwVideoFormat;
            public byte byBrightness;
            public byte byContrast;
            public byte bySaturation;
            public byte byHue;
         
            public uint dwShowChanName;
            public ushort wShowNameTopLeftX;
            public ushort wShowNameTopLeftY;
         
            public NET_DVR_VILOST struVILost;
            
            public NET_DVR_MOTION struMotion;
         
            public NET_DVR_HIDEALARM struHideAlarm;
         
            public uint dwEnableHide;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_SHELTERNUM, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_SHELTER[] struShelter;
            //OSD
            public uint dwShowOsd;
            public ushort wOSDTopLeftX;
            public ushort wOSDTopLeftY;
            public byte byOSDType;
        

            public byte byDispWeek;
            public byte byOSDAttrib;
   

            public byte byHourOsdType;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_PICCFG
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sChanName;
            public uint dwVideoFormat;
            public byte byBrightness;
            public byte byContrast;
            public byte bySaturation;
            public byte byHue;
   
            public uint dwShowChanName;
            public ushort wShowNameTopLeftX;
            public ushort wShowNameTopLeftY;

            public NET_DVR_VILOST struVILost;          

            public NET_DVR_MOTION struMotion;            

            public NET_DVR_HIDEALARM struHideAlarm;          

            public uint dwEnableHide;
            public ushort wHideAreaTopLeftX;
            public ushort wHideAreaTopLeftY;
            public ushort wHideAreaWidth;
            public ushort wHideAreaHeight;
            //OSD
            public uint dwShowOsd;
            public ushort wOSDTopLeftX;
            public ushort wOSDTopLeftY;
            public byte byOSDType;   

            public byte byDispWeek;
            public byte byOSDAttrib;       

            public byte reservedData2;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_COMPRESSION_INFO_V30
        {
            public byte byStreamType;
            public byte byResolution;
            public byte byBitrateType;
            public byte byPicQuality;
            public uint dwVideoBitrate;
            
            public uint dwVideoFrameRate;
            public ushort wIntervalFrameI;
          
            public byte byIntervalBPFrame;
            public byte byres1; 
            public byte byVideoEncType;
            public byte byAudioEncType;
            public byte byVideoEncComplexity;
            public byte byEnableSvc; 
            public byte byFormatType;
            public byte byAudioBitRate; 
            public byte byStreamSmooth;
            public byte byAudioSamplingRate;
            public byte bySmartCodec;
            public byte byres;
          

            public ushort wAverageVideoBitrate;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_COMPRESSIONCFG_V30
        {
            public uint dwSize;
            public NET_DVR_COMPRESSION_INFO_V30 struNormHighRecordPara;
            public NET_DVR_COMPRESSION_INFO_V30 struRes;
            public NET_DVR_COMPRESSION_INFO_V30 struEventRecordPara;
            public NET_DVR_COMPRESSION_INFO_V30 struNetPara;
        }
        
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_COMPRESSION_INFO
        {
            public byte byStreamType;
            public byte byResolution;
            public byte byBitrateType;
            public byte byPicQuality;
            public uint dwVideoBitrate;
          

            public uint dwVideoFrameRate;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_COMPRESSIONCFG
        {
            public uint dwSize;
            public NET_DVR_COMPRESSION_INFO struRecordPara;
            public NET_DVR_COMPRESSION_INFO struNetPara;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_COMPRESSION_INFO_EX
        {
            public byte byStreamType;
            public byte byResolution;
            public byte byBitrateType;
            public byte byPicQuality;
            public uint dwVideoBitrate;


            public uint dwVideoFrameRate;
            public ushort wIntervalFrameI;


            public byte byIntervalBPFrame;
            public byte byRes;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_COMPRESSIONCFG_EX
        {
            public uint dwSize;
            public NET_DVR_COMPRESSION_INFO_EX struRecordPara;
            public NET_DVR_COMPRESSION_INFO_EX struNetPara;
        }

        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_RECORDSCHED
        {
            public NET_DVR_SCHEDTIME struRecordTime;
            public byte byRecordType;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 3)]
            public string reservedData;
        }
        
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_RECORDDAY
        {
            public ushort wAllDayRecord;
            public byte byRecordType;
            public byte reservedData;
        }
      
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_RECORD_V30
        {
            public uint dwSize;
            public uint dwRecord;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DAYS, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_RECORDDAY[] struRecAllDay;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DAYS * MAX_TIMESEGMENT_V30, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_RECORDSCHED[] struRecordSched;
            public uint dwRecordTime;
            public uint dwPreRecordTime;
            public uint dwRecorderDuration;
            public byte byRedundancyRec;
            public byte byAudioRec;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 10, ArraySubType = UnmanagedType.I1)]
            public byte[] byReserve;
        }
        
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_RECORD
        {
            public uint dwSize;
            public uint dwRecord;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DAYS, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_RECORDDAY[] struRecAllDay;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DAYS * MAX_TIMESEGMENT, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_RECORDSCHED[] struRecordSched;
            public uint dwRecordTime;
            public uint dwPreRecordTime;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_PTZ_PROTOCOL
        {
            public uint dwType;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = DESC_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byDescribe;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_PTZCFG
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = PTZ_PROTOCOL_NUM, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_PTZ_PROTOCOL[] struPtz;
            public uint dwPtzNum;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_DECODERCFG_V30
        {
            public uint dwSize;
            public uint dwBaudRate;
            public byte byDataBit;
            public byte byStopBit;
            public byte byParity;
            public byte byFlowcontrol;
            public ushort wDecoderType;
            public ushort wDecoderAddress;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_PRESET_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] bySetPreset;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CRUISE_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] bySetCruise;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_TRACK_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] bySetTrack;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_DECODERCFG
        {
            public uint dwSize;
            public uint dwBaudRate;
            public byte byDataBit;
            public byte byStopBit;
            public byte byParity; 
            public byte byFlowcontrol;
            public ushort wDecoderType;
            public ushort wDecoderAddress;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_PRESET, ArraySubType = UnmanagedType.I1)]
            public byte[] bySetPreset;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CRUISE, ArraySubType = UnmanagedType.I1)]
            public byte[] bySetCruise;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_TRACK, ArraySubType = UnmanagedType.I1)]
            public byte[] bySetTrack;
        }

        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_PPPCFG_V30
        {
            public NET_DVR_IPADDR struRemoteIP;
            public NET_DVR_IPADDR struLocalIP;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sLocalIPMask;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sUsername;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = PASSWD_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sPassword;
            public byte byPPPMode;
            public byte byRedial;
            public byte byRedialMode;
            public byte byDataEncrypt;
            public uint dwMTU;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = PHONENUMBER_LEN)]
            public string sTelephoneNumber;
        }
        
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_PPPCFG
        {
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sRemoteIP;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sLocalIP;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sLocalIPMask;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sUsername;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = PASSWD_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sPassword;
            public byte byPPPMode;
            public byte byRedial;
            public byte byRedialMode;
            public byte byDataEncrypt;
            public uint dwMTU;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = PHONENUMBER_LEN)]
            public string sTelephoneNumber;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_SINGLE_RS232
        {
            public uint dwBaudRate;
            public byte byDataBit;
            public byte byStopBit;
            public byte byParity;
            public byte byFlowcontrol;
            public uint dwWorkMode;
        }
        
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_RS232CFG_V30
        {
            public uint dwSize;
            public NET_DVR_SINGLE_RS232 struRs232;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 84, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
            public NET_DVR_PPPCFG_V30 struPPPConfig;
        }
        
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_RS232CFG
        {
            public uint dwSize;
            public uint dwBaudRate;
            public byte byDataBit;
            public byte byStopBit;
            public byte byParity;
            public byte byFlowcontrol;
            public uint dwWorkMode;
            public NET_DVR_PPPCFG struPPPConfig;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_ALARMINCFG_V30
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sAlarmInName;
            public byte byAlarmType; 
            public byte byAlarmInHandle;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            public NET_DVR_HANDLEEXCEPTION_V30 struAlarmHandleType;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DAYS * MAX_TIMESEGMENT_V30, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_SCHEDTIME[] struAlarmTime;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byRelRecordChan;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byEnablePreset;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byPresetNo;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 192, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byEnableCruise;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byCruiseNo;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byEnablePtzTrack;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byPTZTrack;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes3;
        }
        
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_ALARMINCFG
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sAlarmInName;
            public byte byAlarmType;
            public byte byAlarmInHandle;
            public NET_DVR_HANDLEEXCEPTION struAlarmHandleType;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DAYS * MAX_TIMESEGMENT, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_SCHEDTIME[] struAlarmTime;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM, ArraySubType = UnmanagedType.I1)]
            public byte[] byRelRecordChan;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM, ArraySubType = UnmanagedType.I1)]
            public byte[] byEnablePreset;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM, ArraySubType = UnmanagedType.I1)]
            public byte[] byPresetNo;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM, ArraySubType = UnmanagedType.I1)]
            public byte[] byEnableCruise;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM, ArraySubType = UnmanagedType.I1)]
            public byte[] byCruiseNo;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM, ArraySubType = UnmanagedType.I1)]
            public byte[] byEnablePtzTrack;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM, ArraySubType = UnmanagedType.I1)]
            public byte[] byPTZTrack;
        }
        
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_ALARMINFO_V30
        {
            public int dwAlarmType;
            public int dwAlarmInputNumber;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_ALARMOUT_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byAlarmOutputNumber;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byAlarmRelateChannel;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byChannel;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DISKNUM_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byDiskNumber;

            public void Init()
            {
                dwAlarmType = 0;
                dwAlarmInputNumber = 0;
                byAlarmRelateChannel = new byte[MAX_CHANNUM_V30];
                byChannel = new byte[MAX_CHANNUM_V30];
                byAlarmOutputNumber = new byte[MAX_ALARMOUT_V30];
                byDiskNumber = new byte[MAX_DISKNUM_V30];
                for (int i = 0; i < MAX_CHANNUM_V30; i++)
                {
                    byAlarmRelateChannel[i] = Convert.ToByte(0);
                    byChannel[i] = Convert.ToByte(0);
                }
                for (int i = 0; i < MAX_ALARMOUT_V30; i++)
                {
                    byAlarmOutputNumber[i] = Convert.ToByte(0);
                }
                for (int i = 0; i < MAX_DISKNUM_V30; i++)
                {
                    byDiskNumber[i] = Convert.ToByte(0);
                }
            }

            public void Reset()
            {
                dwAlarmType = 0;
                dwAlarmInputNumber = 0;

                for (int i = 0; i < MAX_CHANNUM_V30; i++)
                {
                    byAlarmRelateChannel[i] = Convert.ToByte(0);
                    byChannel[i] = Convert.ToByte(0);
                }
                for (int i = 0; i < MAX_ALARMOUT_V30; i++)
                {
                    byAlarmOutputNumber[i] = Convert.ToByte(0);
                }
                for (int i = 0; i < MAX_DISKNUM_V30; i++)
                {
                    byDiskNumber[i] = Convert.ToByte(0);
                }
            }
        }
       
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_ALARMINFO_V40_TRIGER_ALARM
        {            
            public uint dwAlarmType;
            public NET_DVR_TIME_EX struAlarmTime;	
            public uint dwAlarmInputNo;		
            public uint dwTrigerAlarmOutNum;	
            public uint dwTrigerRecordChanNum;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 116, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
            public IntPtr pAlarmData;
        }
               
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_ALARMINFO_V40_CHAN_ALARM
        {
            public uint dwAlarmType;
            public NET_DVR_TIME_EX struAlarmTime;	
            public uint dwAlarmChanNum;	
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 124, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
            public IntPtr pAlarmData;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_ALARMINFO_V40_HARDDISK_ALARM
        {            
            public uint dwAlarmType;
            public NET_DVR_TIME_EX struAlarmTime;
            public uint dwAlarmHardDiskNum;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 124, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
            public IntPtr pAlarmData;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_ALARMINFO_V40_RECORDHOST_ALARM
        {

            public uint dwAlarmType;
            public NET_DVR_TIME_EX struAlarmTime;
            public byte bySubAlarmType;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            public NET_DVR_TIME_EX struRecordEndTime;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 116, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
            public IntPtr pAlarmData;
        }
      
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_ALARMINFO_V40_OTHER_ALARM
        {
            public uint dwAlarmType;
            public NET_DVR_TIME_EX struAlarmTime;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 128, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
            public IntPtr pAlarmData;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_ALARMINFO
        {
            public int dwAlarmType;
            public int dwAlarmInputNumber;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_ALARMOUT, ArraySubType = UnmanagedType.U4)]
            public int[] dwAlarmOutputNumber;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM, ArraySubType = UnmanagedType.U4)]
            public int[] dwAlarmRelateChannel;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM, ArraySubType = UnmanagedType.U4)]
            public int[] dwChannel;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DISKNUM, ArraySubType = UnmanagedType.U4)]
            public int[] dwDiskNumber;

            public void Init()
            {
                dwAlarmType = 0;
                dwAlarmInputNumber = 0;
                dwAlarmOutputNumber = new int[MAX_ALARMOUT];
                dwAlarmRelateChannel = new int[MAX_CHANNUM];
                dwChannel = new int[MAX_CHANNUM];
                dwDiskNumber = new int[MAX_DISKNUM];
                for (int i = 0; i < MAX_ALARMOUT; i++)
                {
                    dwAlarmOutputNumber[i] = 0;
                }
                for (int i = 0; i < MAX_CHANNUM; i++)
                {
                    dwAlarmRelateChannel[i] = 0;
                    dwChannel[i] = 0;
                }
                for (int i = 0; i < MAX_DISKNUM; i++)
                {
                    dwDiskNumber[i] = 0;
                }
            }

            public void Reset()
            {
                dwAlarmType = 0;
                dwAlarmInputNumber = 0;

                for (int i = 0; i < MAX_ALARMOUT; i++)
                {
                    dwAlarmOutputNumber[i] = 0;
                }
                for (int i = 0; i < MAX_CHANNUM; i++)
                {
                    dwAlarmRelateChannel[i] = 0;
                    dwChannel[i] = 0;
                }
                for (int i = 0; i < MAX_DISKNUM; i++)
                {
                    dwDiskNumber[i] = 0;
                }
            }

        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_IPDEVINFO
        {
            public uint dwEnable;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sUserName;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = PASSWD_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sPassword;
            public NET_DVR_IPADDR struIP;
            public ushort wDVRPort;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 34, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;

            public void Init()
            {
                sUserName = new byte[NAME_LEN];
                sPassword = new byte[PASSWD_LEN];
                byRes = new byte[34];
            }
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_IPDEVINFO_V31
        {
            public byte byEnable;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sUserName;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = PASSWD_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sPassword;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DOMAIN_NAME, ArraySubType = UnmanagedType.I1)]
            public byte[] byDomain;
            public NET_DVR_IPADDR struIP;
            public ushort wDVRPort;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 34, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_IPCHANINFO
        {
            public byte byEnable;
            public byte byIPID;
            public byte byChannel;
            public byte byIPIDHigh;
            public byte byProType;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 31, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_IPPARACFG
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_IP_DEVICE, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_IPDEVINFO[] struIPDevInfo;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_ANALOG_CHANNUM, ArraySubType = UnmanagedType.I1)]
            public byte[] byAnalogChanEnable;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_IP_CHANNEL, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_IPCHANINFO[] struIPChanInfo;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_IPPARACFG_V31
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_IP_DEVICE, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_IPDEVINFO_V31[] struIPDevInfo; 
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_ANALOG_CHANNUM, ArraySubType = UnmanagedType.I1)]
            public byte[] byAnalogChanEnable; 
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_IP_CHANNEL, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_IPCHANINFO[] struIPChanInfo;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_IPALARMOUTINFO
        {
            public byte byIPID;
            public byte byAlarmOut;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 18, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;

            public void Init()
            {
                byRes = new byte[18];
            }
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_IPALARMOUTCFG
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_IP_ALARMOUT, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_IPALARMOUTINFO[] struIPAlarmOutInfo;

            public void Init()
            {
                struIPAlarmOutInfo = new NET_DVR_IPALARMOUTINFO[MAX_IP_ALARMOUT];
                for (int i = 0; i < MAX_IP_ALARMOUT; i++)
                {
                    struIPAlarmOutInfo[i].Init();
                }
            }
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_IPALARMININFO
        {
            public byte byIPID;
            public byte byAlarmIn;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 18, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_IPALARMINCFG
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_IP_ALARMIN, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_IPALARMININFO[] struIPAlarmInInfo;
        }

        //ipc alarm info
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_DVR_IPALARMINFO
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_IP_DEVICE, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_IPDEVINFO[] struIPDevInfo;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_ANALOG_CHANNUM, ArraySubType = UnmanagedType.I1)]
            public byte[] byAnalogChanEnable;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_IP_CHANNEL, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_IPCHANINFO[] struIPChanInfo;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_IP_ALARMIN, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_IPALARMININFO[] struIPAlarmInInfo;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_IP_ALARMOUT, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_IPALARMOUTINFO[] struIPAlarmOutInfo;
        }
       
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_DVR_IPALARMINFO_V31
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_IP_DEVICE, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_IPDEVINFO_V31[] struIPDevInfo;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_ANALOG_CHANNUM, ArraySubType = UnmanagedType.I1)]
            public byte[] byAnalogChanEnable;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_IP_CHANNEL, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_IPCHANINFO[] struIPChanInfo;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_IP_ALARMIN, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_IPALARMININFO[] struIPAlarmInInfo;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_IP_ALARMOUT, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_IPALARMOUTINFO[] struIPAlarmOutInfo;
        }

        public enum HD_STAT
        {
            HD_STAT_OK = 0,
            HD_STAT_UNFORMATTED = 1,
            HD_STAT_ERROR = 2,
            HD_STAT_SMART_FAILED = 3,
            HD_STAT_MISMATCH = 4,
            HD_STAT_IDLE = 5,
            NET_HD_STAT_OFFLINE = 6,
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_SINGLE_HD
        {
            public uint dwHDNo;
            public uint dwCapacity;
            public uint dwFreeSpace;
            public uint dwHdStatus;
            public byte byHDAttr;
            public byte byHDType;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            public uint dwHdGroup;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 120, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_HDCFG
        {
            public uint dwSize;
            public uint dwHDCount;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DISKNUM_V30, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_SINGLE_HD[] struHDInfo;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_SINGLE_HDGROUP
        {
            public uint dwHDGroupNo;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 64, ArraySubType = UnmanagedType.I1)]
            public byte[] byHDGroupChans;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_HDGROUP_CFG
        {
            public uint dwSize;
            public uint dwHDGroupCount;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_HD_GROUP, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_SINGLE_HDGROUP[] struHDGroupAttr;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_SCALECFG
        {
            public uint dwSize;
            public uint dwMajorScale;
            public uint dwMinorScale;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.U4)]
            public uint[] dwRes;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_ALARMOUTCFG_V30
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sAlarmOutName;
            public uint dwAlarmOutDelay;

            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DAYS * MAX_TIMESEGMENT_V30, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_SCHEDTIME[] struAlarmOutTime;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_ALARMOUTCFG
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sAlarmOutName;
            public uint dwAlarmOutDelay;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DAYS * MAX_TIMESEGMENT, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_SCHEDTIME[] struAlarmOutTime;
        }
                
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_PREVIEWCFG_V30
        {
            public uint dwSize;
            public byte byPreviewNumber;
            public byte byEnableAudio;
            public ushort wSwitchTime;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_PREVIEW_MODE * MAX_WINDOW_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] bySwitchSeq;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 24, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }
        
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_PREVIEWCFG
        {
            public uint dwSize;
            public byte byPreviewNumber;
            public byte byEnableAudio;
            public ushort wSwitchTime;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_WINDOW, ArraySubType = UnmanagedType.I1)]
            public byte[] bySwitchSeq;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_VGAPARA
        {
            public ushort wResolution;
            public ushort wFreq;
            public uint dwBrightness;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_MATRIXPARA_V30
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_ANALOG_CHANNUM, ArraySubType = UnmanagedType.U2)]
            public ushort[] wOrder;
            public ushort wSwitchTime;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 14, ArraySubType = UnmanagedType.I1)]
            public byte[] res;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_MATRIXPARA
        {
            public ushort wDisplayLogo;
            public ushort wDisplayOsd;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_VOOUT
        {
            public byte byVideoFormat;
            public byte byMenuAlphaValue;
            public ushort wScreenSaveTime;
            public ushort wVOffset;
            public ushort wBrightness;
            public byte byStartMode;
            public byte byEnableScaler;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_VIDEOOUT_V30
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_VIDEOOUT_V30, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_VOOUT[] struVOOut;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_VGA_V30, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_VGAPARA[] struVGAPara;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_MATRIXOUT, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_MATRIXPARA_V30[] struMatrixPara;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_VIDEOOUT
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_VIDEOOUT, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_VOOUT[] struVOOut;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_VGA, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_VGAPARA[] struVGAPara;
            public NET_DVR_MATRIXPARA struMatrixPara;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_USER_INFO_V30
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sUserName;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = PASSWD_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sPassword;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_RIGHT, ArraySubType = UnmanagedType.I1)]
            public byte[] byLocalRight;

            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_RIGHT, ArraySubType = UnmanagedType.I1)]
            public byte[] byRemoteRight;

            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byNetPreviewRight;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byLocalPlaybackRight;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byNetPlaybackRight;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byLocalRecordRight;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byNetRecordRight;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byLocalPTZRight;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byNetPTZRight;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byLocalBackupRight;
            public NET_DVR_IPADDR struUserIP;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MACADDR_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byMACAddr;
            public byte byPriority;
     
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 17, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_USER_INFO_EX
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sUserName;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = PASSWD_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sPassword;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_RIGHT, ArraySubType = UnmanagedType.U4)]
            public uint[] dwLocalRight;

            public uint dwLocalPlaybackRight;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_RIGHT, ArraySubType = UnmanagedType.U4)]
            public uint[] dwRemoteRight;         

            public uint dwNetPreviewRight;
            public uint dwNetPlaybackRight;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sUserIP;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MACADDR_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byMACAddr;
        }

        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_USER_INFO
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sUserName;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = PASSWD_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sPassword;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_RIGHT, ArraySubType = UnmanagedType.U4)]
            public uint[] dwLocalRight;

            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_RIGHT, ArraySubType = UnmanagedType.U4)]
            public uint[] dwRemoteRight;

            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sUserIP;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MACADDR_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byMACAddr;
        }
               
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_USER_V30
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_USERNUM_V30, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_USER_INFO_V30[] struUser;
        }
        
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_USER_EX
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_USERNUM, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_USER_INFO_EX[] struUser;
        }
        
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_USER
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_USERNUM, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_USER_INFO[] struUser;
        }
       
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_EXCEPTION_V30
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_EXCEPTIONNUM_V30, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_HANDLEEXCEPTION_V30[] struExceptionHandleType;            
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_EXCEPTION
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_EXCEPTIONNUM, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_HANDLEEXCEPTION[] struExceptionHandleType;
            
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_CHANNELSTATE_V30
        {
            public byte byRecordStatic;
            public byte bySignalStatic;
            public byte byHardwareStatic;
            public byte byRes1;
            public uint dwBitRate;
            public uint dwLinkNum;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_LINK, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_IPADDR[] struClientIP;
            public uint dwIPLinkNum;
            public byte byExceedMaxLink;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
            public uint dwAllBitRate;
            public uint dwChannelNo;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_CHANNELSTATE
        {
            public byte byRecordStatic;
            public byte bySignalStatic;
            public byte byHardwareStatic;
            public byte reservedData;
            public uint dwBitRate;
            public uint dwLinkNum;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_LINK, ArraySubType = UnmanagedType.U4)]
            public uint[] dwClientIP;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_DISKSTATE
        {
            public uint dwVolume;
            public uint dwFreeSpace;
            public uint dwHardDiskStatic;
        }
        
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_WORKSTATE_V30
        {
            public uint dwDeviceStatic;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DISKNUM_V30, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_DISKSTATE[] struHardDiskStatic;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM_V30, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_CHANNELSTATE_V30[] struChanStatic;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_ALARMIN_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byAlarmInStatic;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_ALARMOUT_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byAlarmOutStatic;
            public uint dwLocalDisplay;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_AUDIO_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byAudioChanStatus;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 10, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_WORKSTATE
        {
            public uint dwDeviceStatic;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DISKNUM, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_DISKSTATE[] struHardDiskStatic;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_CHANNELSTATE[] struChanStatic;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_ALARMIN, ArraySubType = UnmanagedType.I1)]
            public byte[] byAlarmInStatic;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_ALARMOUT, ArraySubType = UnmanagedType.I1)]
            public byte[] byAlarmOutStatic;
            public uint dwLocalDisplay;

            public void Init()
            {
                struHardDiskStatic = new NET_DVR_DISKSTATE[MAX_DISKNUM];
                struChanStatic = new NET_DVR_CHANNELSTATE[MAX_CHANNUM];
                byAlarmInStatic = new byte[MAX_ALARMIN];
                byAlarmOutStatic = new byte[MAX_ALARMOUT];
            }
        }

        public const int MAJOR_ALARM = 1;

        public const int MINOR_ALARM_IN = 1;
        public const int MINOR_ALARM_OUT = 2;
        public const int MINOR_MOTDET_START = 3; 
        public const int MINOR_MOTDET_STOP = 4;
        public const int MINOR_HIDE_ALARM_START = 5;
        public const int MINOR_HIDE_ALARM_STOP = 6;
        public const int MINOR_VCA_ALARM_START = 7;
        public const int MINOR_VCA_ALARM_STOP = 8;
        public const int MINOR_ITS_ALARM_START = 0x09;  
        public const int MINOR_ITS_ALARM_STOP = 0x0a; 
        public const int MINOR_NETALARM_START = 0x0b; 
        public const int MINOR_NETALARM_STOP = 0x0c; 
        public const int MINOR_NETALARM_RESUME = 0x0d; 
        public const int MINOR_WIRELESS_ALARM_START = 0x0e;
        public const int MINOR_WIRELESS_ALARM_STOP = 0x0f; 
        public const int MINOR_PIR_ALARM_START = 0x10;  
        public const int MINOR_PIR_ALARM_STOP = 0x11;
        public const int MINOR_CALLHELP_ALARM_START = 0x12; 
        public const int MINOR_CALLHELP_ALARM_STOP = 0x13; 
        public const int MINOR_DETECTFACE_ALARM_START = 0x16;
        public const int MINOR_DETECTFACE_ALARM_STOP = 0x17;
        public const int MINOR_VQD_ALARM_START = 0x18; 
        public const int MINOR_VQD_ALARM_STOP = 0x19;
        public const int MINOR_VCA_SECNECHANGE_DETECTION = 0x1a;
        public const int MINOR_SMART_REGION_EXITING_BEGIN = 0x1b; 
        public const int MINOR_SMART_REGION_EXITING_END = 0x1c;  
        public const int MINOR_SMART_LOITERING_BEGIN = 0x1d;
        public const int MINOR_SMART_LOITERING_END = 0x1e;
        public const int MINOR_VCA_ALARM_LINE_DETECTION_BEGIN = 0x20;
        public const int MINOR_VCA_ALARM_LINE_DETECTION_END = 0x21;
        public const int MINOR_VCA_ALARM_INTRUDE_BEGIN = 0x22;  
        public const int MINOR_VCA_ALARM_INTRUDE_END = 0x23; 
        public const int MINOR_VCA_ALARM_AUDIOINPUT = 0x24; 
        public const int MINOR_VCA_ALARM_AUDIOABNORMAL = 0x25;
        public const int MINOR_VCA_DEFOCUS_DETECTION_BEGIN = 0x26;
        public const int MINOR_VCA_DEFOCUS_DETECTION_END = 0x27;
        public const int MINOR_EXT_ALARM = 0x28;
        public const int MINOR_VCA_FACE_ALARM_BEGIN = 0x29;
        public const int MINOR_SMART_REGION_ENTRANCE_BEGIN = 0x2a; 
        public const int MINOR_SMART_REGION_ENTRANCE_END = 0x2b; 
        public const int MINOR_SMART_PEOPLE_GATHERING_BEGIN = 0x2c;
        public const int MINOR_SMART_PEOPLE_GATHERING_END = 0x2d;
        public const int MINOR_SMART_FAST_MOVING_BEGIN = 0x2e; 
        public const int MINOR_SMART_FAST_MOVING_END = 0x2f;
        public const int MINOR_VCA_FACE_ALARM_END = 0x30;  
        public const int MINOR_VCA_SCENE_CHANGE_ALARM_BEGIN = 0x31;
        public const int MINOR_VCA_SCENE_CHANGE_ALARM_END = 0x32; 
        public const int MINOR_VCA_ALARM_AUDIOINPUT_BEGIN = 0x33; 
        public const int MINOR_VCA_ALARM_AUDIOINPUT_END = 0x34; 
        public const int MINOR_VCA_ALARM_AUDIOABNORMAL_BEGIN = 0x35;
        public const int MINOR_VCA_ALARM_AUDIOABNORMAL_END = 0x36; 

        public const int MINOR_VCA_LECTURE_DETECTION_BEGIN = 0x37;
        public const int MINOR_VCA_LECTURE_DETECTION_END = 0x38;
        public const int MINOR_VCA_ALARM_AUDIOSTEEPDROP = 0x39;
        public const int MINOR_VCA_ANSWER_DETECTION_BEGIN = 0x3a;  
        public const int MINOR_VCA_ANSWER_DETECTION_END = 0x3b;

        public const int MINOR_SMART_PARKING_BEGIN = 0x3c; 
        public const int MINOR_SMART_PARKING_END = 0x3d;
        public const int MINOR_SMART_UNATTENDED_BAGGAGE_BEGIN = 0x3e;
        public const int MINOR_SMART_UNATTENDED_BAGGAGE_END = 0x3f;
        public const int MINOR_SMART_OBJECT_REMOVAL_BEGIN = 0x40;  
        public const int MINOR_SMART_OBJECT_REMOVAL_END = 0x41; 
        public const int MINOR_SMART_VEHICLE_ALARM_START = 0x46; 
        public const int MINOR_SMART_VEHICLE_ALARM_STOP = 0x47;   
        public const int MINOR_THERMAL_FIREDETECTION = 0x48;  
        public const int MINOR_THERMAL_FIREDETECTION_END = 0x49;   
        public const int MINOR_SMART_VANDALPROOF_BEGIN = 0x50;   
        public const int MINOR_SMART_VANDALPROOF_END = 0x51; 

        public const int MINOR_ALARMIN_SHORT_CIRCUIT = 0x400;
        public const int MINOR_ALARMIN_BROKEN_CIRCUIT = 0x401; 
        public const int MINOR_ALARMIN_EXCEPTION = 0x402; 
        public const int MINOR_ALARMIN_RESUME = 0x403; 
        public const int MINOR_HOST_DESMANTLE_ALARM = 0x404; 
        public const int MINOR_HOST_DESMANTLE_RESUME = 0x405;  
        public const int MINOR_CARD_READER_DESMANTLE_ALARM = 0x406; 
        public const int MINOR_CARD_READER_DESMANTLE_RESUME = 0x407; 
        public const int MINOR_CASE_SENSOR_ALARM = 0x408;
        public const int MINOR_CASE_SENSOR_RESUME = 0x409; 
        public const int MINOR_STRESS_ALARM = 0x40a;
        public const int MINOR_OFFLINE_ECENT_NEARLY_FULL = 0x40b; 
        public const int MINOR_CARD_MAX_AUTHENTICATE_FAIL = 0x40c;
        public const int MINOR_SD_CARD_FULL = 0x40d; 
        public const int MINOR_LINKAGE_CAPTURE_PIC = 0x40e;  

       
        public const int MAJOR_EXCEPTION = 2;
      
        public const int MINOR_VI_LOST = 33;
        public const int MINOR_ILLEGAL_ACCESS = 34;
        public const int MINOR_HD_FULL = 35;
        public const int MINOR_HD_ERROR = 36;
        public const int MINOR_DCD_LOST = 37;
        public const int MINOR_IP_CONFLICT = 38;
        public const int MINOR_NET_BROKEN = 39;
        public const int MINOR_REC_ERROR = 40;
        public const int MINOR_IPC_NO_LINK = 41;
        public const int MINOR_VI_EXCEPTION = 42;
        public const int MINOR_IPC_IP_CONFLICT = 43;
        public const int MINOR_RAID_ERROR = 0x20; 
        public const int MINOR_SENCE_EXCEPTION = 0x2c;
        public const int MINOR_PIC_REC_ERROR = 0x2d;
        public const int MINOR_VI_MISMATCH = 0x2e;
        public const int MINOR_RESOLUTION_MISMATCH = 0x2f;
       
        
        public const int MINOR_NET_ABNORMAL = 0x35;
        public const int MINOR_MEM_ABNORMAL = 0x36;
        public const int MINOR_FILE_ABNORMAL = 0x37;
        public const int MINOR_PANEL_ABNORMAL = 0x38;
        public const int MINOR_PANEL_RESUME = 0x39;
        public const int MINOR_RS485_DEVICE_ABNORMAL = 0x3a;
        public const int MINOR_RS485_DEVICE_REVERT = 0x3b;
        public const int MINOR_SCREEN_SUBSYSTEM_ABNORMALREBOOT = 0x3c; 
        public const int MINOR_SCREEN_SUBSYSTEM_ABNORMALINSERT = 0x3d;  
        public const int MINOR_SCREEN_SUBSYSTEM_ABNORMALPULLOUT = 0x3e;
        public const int MINOR_SCREEN_ABNARMALTEMPERATURE = 0x3f;
        public const int MINOR_RECORD_OVERFLOW = 0x41;
        public const int MINOR_DSP_ABNORMAL = 0x42; 
        public const int MINOR_ANR_RECORD_FAIED = 0x43; 
        public const int MINOR_SPARE_WORK_DEVICE_EXCEPT = 0x44;
        public const int MINOR_START_IPC_MAS_FAILED = 0x45; 
        public const int MINOR_IPCM_CRASH = 0x46; 
        public const int MINOR_POE_POWER_EXCEPTION = 0x47;
        public const int MINOR_UPLOAD_DATA_CS_EXCEPTION = 0x48; 
        public const int MINOR_DIAL_EXCEPTION = 0x49;        
        public const int MINOR_DEV_EXCEPTION_OFFLINE = 0x50; 
        public const int MINOR_UPGRADEFAIL = 0x51; 
        public const int MINOR_AI_LOST = 0x52; 

        public const int MINOR_DEV_POWER_ON = 0x400;  
        public const int MINOR_DEV_POWER_OFF = 0x401; 
        public const int MINOR_WATCH_DOG_RESET = 0x402; 
        public const int MINOR_LOW_BATTERY = 0x403;
        public const int MINOR_BATTERY_RESUME = 0x404;
        public const int MINOR_AC_OFF = 0x405; 
        public const int MINOR_AC_RESUME = 0x406;
        public const int MINOR_NET_RESUME = 0x407;  
        public const int MINOR_FLASH_ABNORMAL = 0x408;
        public const int MINOR_CARD_READER_OFFLINE = 0x409; 
        public const int MINOR_CARD_READER_RESUME = 0x40a; 
        public const int MINOR_SUBSYSTEM_IP_CONFLICT = 0x4000; 
        public const int MINOR_SUBSYSTEM_NET_BROKEN = 0x4001;
        public const int MINOR_FAN_ABNORMAL = 0x4002; 
        public const int MINOR_BACKPANEL_TEMPERATURE_ABNORMAL = 0x4003;


        public const int MINOR_FANABNORMAL = 49;
        public const int MINOR_FANRESUME = 50;
        public const int MINOR_SUBSYSTEM_ABNORMALREBOOT = 51;
        public const int MINOR_MATRIX_STARTBUZZER = 52;

        public const int MAJOR_OPERATION = 3;

        public const int MINOR_VCA_MOTIONEXCEPTION = 0x29;
        public const int MINOR_START_DVR = 0x41; 
        public const int MINOR_STOP_DVR = 0x42; 
        public const int MINOR_STOP_ABNORMAL = 0x43;  
        public const int MINOR_REBOOT_DVR = 0x44;  

        public const int MINOR_LOCAL_LOGIN = 0x50;
        public const int MINOR_LOCAL_LOGOUT = 0x51; 
        public const int MINOR_LOCAL_CFG_PARM = 0x52;
        public const int MINOR_LOCAL_PLAYBYFILE = 0x53; 
        public const int MINOR_LOCAL_PLAYBYTIME = 0x54;  
        public const int MINOR_LOCAL_START_REC = 0x55;  
        public const int MINOR_LOCAL_STOP_REC = 0x56;
        public const int MINOR_LOCAL_PTZCTRL = 0x57;
        public const int MINOR_LOCAL_PREVIEW = 0x58;
        public const int MINOR_LOCAL_MODIFY_TIME = 0x59;
        public const int MINOR_LOCAL_UPGRADE = 0x5a;
        public const int MINOR_LOCAL_RECFILE_OUTPUT = 0x5b;
        public const int MINOR_LOCAL_FORMAT_HDD = 0x5c;
        public const int MINOR_LOCAL_CFGFILE_OUTPUT = 0x5d;
        public const int MINOR_LOCAL_CFGFILE_INPUT = 0x5e;
        public const int MINOR_LOCAL_COPYFILE = 0x5f; 
        public const int MINOR_LOCAL_LOCKFILE = 0x60;
        public const int MINOR_LOCAL_UNLOCKFILE = 0x61;
        public const int MINOR_LOCAL_DVR_ALARM = 0x62;
        public const int MINOR_IPC_ADD = 0x63;
        public const int MINOR_IPC_DEL = 0x64; 
        public const int MINOR_IPC_SET = 0x65;
        public const int MINOR_LOCAL_START_BACKUP = 0x66; 
        public const int MINOR_LOCAL_STOP_BACKUP = 0x67;
        public const int MINOR_LOCAL_COPYFILE_START_TIME = 0x68; 
        public const int MINOR_LOCAL_COPYFILE_END_TIME = 0x69;
        public const int MINOR_LOCAL_ADD_NAS = 0x6a;
        public const int MINOR_LOCAL_DEL_NAS = 0x6b;
        public const int MINOR_LOCAL_SET_NAS = 0x6c;
        public const int MINOR_LOCAL_RESET_PASSWD = 0x6d;

        public const int MINOR_REMOTE_LOGIN = 0x70;
        public const int MINOR_REMOTE_LOGOUT = 0x71; 
        public const int MINOR_REMOTE_START_REC = 0x72; 
        public const int MINOR_REMOTE_STOP_REC = 0x73;
        public const int MINOR_START_TRANS_CHAN = 0x74; 
        public const int MINOR_STOP_TRANS_CHAN = 0x75;
        public const int MINOR_REMOTE_GET_PARM = 0x76;
        public const int MINOR_REMOTE_CFG_PARM = 0x77;
        public const int MINOR_REMOTE_GET_STATUS = 0x78; 
        public const int MINOR_REMOTE_ARM = 0x79; 
        public const int MINOR_REMOTE_DISARM = 0x7a; 
        public const int MINOR_REMOTE_REBOOT = 0x7b;  
        public const int MINOR_START_VT = 0x7c;  
        public const int MINOR_STOP_VT = 0x7d;
        public const int MINOR_REMOTE_UPGRADE = 0x7e; 
        public const int MINOR_REMOTE_PLAYBYFILE = 0x7f;
        public const int MINOR_REMOTE_PLAYBYTIME = 0x80;  
        public const int MINOR_REMOTE_PTZCTRL = 0x81; 
        public const int MINOR_REMOTE_FORMAT_HDD = 0x82;  
        public const int MINOR_REMOTE_STOP = 0x83; 
        public const int MINOR_REMOTE_LOCKFILE = 0x84;  
        public const int MINOR_REMOTE_UNLOCKFILE = 0x85;
        public const int MINOR_REMOTE_CFGFILE_OUTPUT = 0x86;  
        public const int MINOR_REMOTE_CFGFILE_INTPUT = 0x87; 
        public const int MINOR_REMOTE_RECFILE_OUTPUT = 0x88; 
        public const int MINOR_REMOTE_DVR_ALARM = 0x89; 
        public const int MINOR_REMOTE_IPC_ADD = 0x8a; 
        public const int MINOR_REMOTE_IPC_DEL = 0x8b;  
        public const int MINOR_REMOTE_IPC_SET = 0x8c; 
        public const int MINOR_REBOOT_VCA_LIB = 0x8d; 
        public const int MINOR_REMOTE_ADD_NAS = 0x8e; 
        public const int MINOR_REMOTE_DEL_NAS = 0x8f; 

        public const int MINOR_REMOTE_SET_NAS = 0x90; 
        public const int MINOR_LOCAL_START_REC_CDRW = 0x91; 
        public const int MINOR_LOCAL_STOP_REC_CDRW = 0x92; 
        public const int MINOR_REMOTE_START_REC_CDRW = 0x93; 
        public const int MINOR_REMOTE_STOP_REC_CDRW = 0x94;  
        public const int MINOR_LOCAL_PIC_OUTPUT = 0x95; 
        public const int MINOR_REMOTE_PIC_OUTPUT = 0x96;  
        public const int MINOR_LOCAL_INQUEST_RESUME = 0x97; 
        public const int MINOR_REMOTE_INQUEST_RESUME = 0x98; 
        public const int MINOR_LOCAL_ADD_FILE = 0x99; 
        public const int MINOR_REMOTE_DELETE_HDISK = 0x9a;  
        public const int MINOR_REMOTE_LOAD_HDISK = 0x9b; 
        public const int MINOR_REMOTE_UNLOAD_HDISK = 0x9c;  
        public const int MINOR_LOCAL_OPERATE_LOCK = 0x9d;  
        public const int MINOR_LOCAL_OPERATE_UNLOCK = 0x9e;  
        public const int MINOR_LOCAL_DEL_FILE = 0x9f;

        public const int MINOR_SUBSYSTEMREBOOT = 0xa0;
        public const int MINOR_MATRIX_STARTTRANSFERVIDEO = 0xa1;
        public const int MINOR_MATRIX_STOPTRANSFERVIDEO = 0xa2;
        public const int MINOR_REMOTE_SET_ALLSUBSYSTEM = 0xa3;
        public const int MINOR_REMOTE_GET_ALLSUBSYSTEM = 0xa4;
        public const int MINOR_REMOTE_SET_PLANARRAY = 0xa5; 
        public const int MINOR_REMOTE_GET_PLANARRAY = 0xa6;
        public const int MINOR_MATRIX_STARTTRANSFERAUDIO = 0xa7;
        public const int MINOR_MATRIX_STOPRANSFERAUDIO = 0xa8;
        public const int MINOR_LOGON_CODESPITTER = 0xa9;
        public const int MINOR_LOGOFF_CODESPITTER = 0xaa; 

        //KY2013 3.0.0
        public const int MINOR_LOCAL_MAIN_AUXILIARY_PORT_SWITCH = 0X302; 
        public const int MINOR_LOCAL_HARD_DISK_CHECK = 0x303;
        
        public const int MINOR_START_DYNAMIC_DECODE = 0xb; 
        public const int MINOR_STOP_DYNAMIC_DECODE = 0xb1;
        public const int MINOR_GET_CYC_CFG = 0xb2;
        public const int MINOR_SET_CYC_CFG = 0xb3; 
        public const int MINOR_START_CYC_DECODE = 0xb4;
        public const int MINOR_STOP_CYC_DECODE = 0xb5;
        public const int MINOR_GET_DECCHAN_STATUS = 0xb6;
        public const int MINOR_GET_DECCHAN_INFO = 0xb7; 
        public const int MINOR_START_PASSIVE_DEC = 0xb8; 
        public const int MINOR_STOP_PASSIVE_DEC = 0xb9;
        public const int MINOR_CTRL_PASSIVE_DEC = 0xba; 
        public const int MINOR_RECON_PASSIVE_DEC = 0xbb;
        public const int MINOR_GET_DEC_CHAN_SW = 0xbc;
        public const int MINOR_SET_DEC_CHAN_SW = 0xbd;
        public const int MINOR_CTRL_DEC_CHAN_SCALE = 0xbe;
        public const int MINOR_SET_REMOTE_REPLAY = 0xbf;
        public const int MINOR_GET_REMOTE_REPLAY = 0xc0;
        public const int MINOR_CTRL_REMOTE_REPLAY = 0xc1;
        public const int MINOR_SET_DISP_CFG = 0xc2;
        public const int MINOR_GET_DISP_CFG = 0xc3;
        public const int MINOR_SET_PLANTABLE = 0xc4;
        public const int MINOR_GET_PLANTABLE = 0xc5;
        public const int MINOR_START_PPPPOE = 0xc6;
        public const int MINOR_STOP_PPPPOE = 0xc7; 
        public const int MINOR_UPLOAD_LOGO = 0xc8;
        
        public const int MINOR_LOCAL_PIN = 0xc9;
        public const int MINOR_LOCAL_DIAL = 0xca; 
        public const int MINOR_SMS_CONTROL = 0xcb;
        public const int MINOR_CALL_ONLINE = 0xc;
        public const int MINOR_REMOTE_PIN = 0xcd;

        public const int MINOR_REMOTE_BYPASS = 0xd0;
        public const int MINOR_REMOTE_UNBYPASS = 0xd1; 
        public const int MINOR_REMOTE_SET_ALARMIN_CFG = 0xd2;
        public const int MINOR_REMOTE_GET_ALARMIN_CFG = 0xd3; 
        public const int MINOR_REMOTE_SET_ALARMOUT_CFG = 0xd4;  
        public const int MINOR_REMOTE_GET_ALARMOUT_CFG = 0xd5;
        public const int MINOR_REMOTE_ALARMOUT_OPEN_MAN = 0xd6; 
        public const int MINOR_REMOTE_ALARMOUT_CLOSE_MAN = 0xd7; 
        public const int MINOR_REMOTE_ALARM_ENABLE_CFG = 0xd8; 
        public const int MINOR_DBDATA_OUTPUT = 0xd9; 
        public const int MINOR_DBDATA_INPUT = 0xda;
        public const int MINOR_MU_SWITCH = 0xdb; 
        public const int MINOR_MU_PTZ = 0xdc; 
        public const int MINOR_DELETE_LOGO = 0xdd;

        public const int MINOR_LOCAL_CONF_REB_RAID = 0x101; 
        public const int MINOR_LOCAL_CONF_SPARE = 0x102;  
        public const int MINOR_LOCAL_ADD_RAID = 0x103;  
        public const int MINOR_LOCAL_DEL_RAID = 0x104; 
        public const int MINOR_LOCAL_MIG_RAID = 0x105;
        public const int MINOR_LOCAL_REB_RAID = 0x106;  
        public const int MINOR_LOCAL_QUICK_CONF_RAID = 0x107;
        public const int MINOR_LOCAL_ADD_VD = 0x108; 
        public const int MINOR_LOCAL_DEL_VD = 0x109; 
        public const int MINOR_LOCAL_RP_VD = 0x10a; 
        public const int MINOR_LOCAL_FORMAT_EXPANDVD = 0x10b;
        public const int MINOR_LOCAL_RAID_UPGRADE = 0x10c; 
        public const int MINOR_LOCAL_STOP_RAID = 0x10d;  
        public const int MINOR_REMOTE_CONF_REB_RAID = 0x111; 
        public const int MINOR_REMOTE_CONF_SPARE = 0x112;  
        public const int MINOR_REMOTE_ADD_RAID = 0x113; 
        public const int MINOR_REMOTE_DEL_RAID = 0x114; 
        public const int MINOR_REMOTE_MIG_RAID = 0x115; 
        public const int MINOR_REMOTE_REB_RAID = 0x116;
        public const int MINOR_REMOTE_QUICK_CONF_RAID = 0x117; 
        public const int MINOR_REMOTE_ADD_VD = 0x118; 
        public const int MINOR_REMOTE_DEL_VD = 0x119;  
        public const int MINOR_REMOTE_RP_VD = 0x11a;
        public const int MINOR_REMOTE_FORMAT_EXPANDVD = 0x11b;
        public const int MINOR_REMOTE_RAID_UPGRADE = 0x11c;
        public const int MINOR_REMOTE_STOP_RAID = 0x11d; 
        public const int MINOR_LOCAL_START_PIC_REC = 0x121; 
        public const int MINOR_LOCAL_STOP_PIC_REC = 0x122; 
        public const int MINOR_LOCAL_SET_SNMP = 0x125; 
        public const int MINOR_LOCAL_TAG_OPT = 0x126; 
        public const int MINOR_REMOTE_START_PIC_REC = 0x131; 
        public const int MINOR_REMOTE_STOP_PIC_REC = 0x132; 
        public const int MINOR_REMOTE_SET_SNMP = 0x135; 
        public const int MINOR_REMOTE_TAG_OPT = 0x136;  

        public const int MINOR_LOCAL_VOUT_SWITCH = 0x140; 
        public const int MINOR_STREAM_CABAC = 0x141;  

        public const int MINOR_LOCAL_SPARE_OPT = 0x142;   
        public const int MINOR_REMOTE_SPARE_OPT = 0x143;  
        public const int MINOR_LOCAL_IPCCFGFILE_OUTPUT = 0x144;
        public const int MINOR_LOCAL_IPCCFGFILE_INPUT = 0x145;  
        public const int MINOR_LOCAL_IPC_UPGRADE = 0x146;   
        public const int MINOR_REMOTE_IPCCFGFILE_OUTPUT = 0x147;
        public const int MINOR_REMOTE_IPCCFGFILE_INPUT = 0x148;  
        public const int MINOR_REMOTE_IPC_UPGRADE = 0x149; 

        public const int MINOR_SET_MULTI_MASTER = 0x201;
        public const int MINOR_SET_MULTI_SLAVE = 0x202; 
        public const int MINOR_CANCEL_MULTI_MASTER = 0x203;  
        public const int MINOR_CANCEL_MULTI_SLAVE = 0x204;

        public const int MINOR_DISPLAY_LOGO = 0x205;
        public const int MINOR_HIDE_LOGO = 0x206;  
        public const int MINOR_SET_DEC_DELAY_LEVEL = 0x207;   
        public const int MINOR_SET_BIGSCREEN_DIPLAY_AREA = 0x208;
        public const int MINOR_CUT_VIDEO_SOURCE = 0x209; 
        public const int MINOR_SET_BASEMAP_AREA = 0x210;  
        public const int MINOR_DOWNLOAD_BASEMAP = 0x211;  
        public const int MINOR_CUT_BASEMAP = 0x212;  
        public const int MINOR_CONTROL_ELEC_ENLARGE = 0x213;   
        public const int MINOR_SET_OUTPUT_RESOLUTION = 0x214; 
        public const int MINOR_SET_TRANCSPARENCY = 0X215; 
        public const int MINOR_SET_OSD = 0x216;
        public const int MINOR_RESTORE_DEC_STATUS = 0x217; 

        public const int MINOR_SCREEN_SET_INPUT = 0x251;
        public const int MINOR_SCREEN_SET_OUTPUT = 0x252; 
        public const int MINOR_SCREEN_SET_OSD = 0x253;
        public const int MINOR_SCREEN_SET_LOGO = 0x254;
        public const int MINOR_SCREEN_SET_LAYOUT = 0x255;  
        public const int MINOR_SCREEN_PICTUREPREVIEW = 0x256; 

        public const int MINOR_SCREEN_GET_OSD = 0x257;  
        public const int MINOR_SCREEN_GET_LAYOUT = 0x258;
        public const int MINOR_SCREEN_LAYOUT_CTRL = 0x259;  
        public const int MINOR_GET_ALL_VALID_WND = 0x260; 
        public const int MINOR_GET_SIGNAL_WND = 0x261;  
        public const int MINOR_WINDOW_CTRL = 0x262;  
        public const int MINOR_GET_LAYOUT_LIST = 0x263; 
        public const int MINOR_LAYOUT_CTRL = 0x264; 
        public const int MINOR_SET_LAYOUT = 0x265;  
        public const int MINOR_GET_SIGNAL_LIST = 0x266; 
        public const int MINOR_GET_PLAN_LIST = 0x267;  
        public const int MINOR_SET_PLAN = 0x268; 
        public const int MINOR_CTRL_PLAN = 0x269;
        public const int MINOR_CTRL_SCREEN = 0x270;
        public const int MINOR_ADD_NETSIG = 0x271;  
        public const int MINOR_SET_NETSIG = 0x272; 
        public const int MINOR_SET_DECBDCFG = 0x273; 
        public const int MINOR_GET_DECBDCFG = 0x274; 
        public const int MINOR_GET_DEVICE_STATUS = 0x275; 
        public const int MINOR_UPLOAD_PICTURE = 0x276; 
        public const int MINOR_SET_USERPWD = 0x277;  
        public const int MINOR_ADD_LAYOUT = 0x278; 
        public const int MINOR_DEL_LAYOUT = 0x279; 
        public const int MINOR_DEL_NETSIG = 0x280; 
        public const int MINOR_ADD_PLAN = 0x281; 
        public const int MINOR_DEL_PLAN = 0x282;  
        public const int MINOR_GET_EXTERNAL_MATRIX_CFG = 0x283; 
        public const int MINOR_SET_EXTERNAL_MATRIX_CFG = 0x284; 
        public const int MINOR_GET_USER_CFG = 0x285;
        public const int MINOR_SET_USER_CFG = 0x286; 
        public const int MINOR_GET_DISPLAY_PANEL_LINK_CFG = 0x287; 
        public const int MINOR_SET_DISPLAY_PANEL_LINK_CFG = 0x288; 

        public const int MINOR_GET_WALLSCENE_PARAM = 0x289; 
        public const int MINOR_SET_WALLSCENE_PARAM = 0x28a; 
        public const int MINOR_GET_CURRENT_WALLSCENE = 0x28b; 
        public const int MINOR_SWITCH_WALLSCENE = 0x28c; 
        public const int MINOR_SIP_LOGIN = 0x28d;
        public const int MINOR_VOIP_START = 0x28e; 
        public const int MINOR_VOIP_STOP = 0x28f; 
        public const int MINOR_WIN_TOP = 0x290;
        public const int MINOR_WIN_BOTTOM = 0x291; 

        // Netra 2.2.2
        public const int MINOR_LOCAL_LOAD_HDISK = 0x300; 
        public const int MINOR_LOCAL_DELETE_HDISK = 0x301; 

        //Netra3.1.0
        public const int MINOR_LOCAL_CFG_DEVICE_TYPE = 0x310; 
        public const int MINOR_REMOTE_CFG_DEVICE_TYPE = 0x311;
        public const int MINOR_LOCAL_CFG_WORK_HOT_SERVER = 0x312;
        public const int MINOR_REMOTE_CFG_WORK_HOT_SERVER = 0x313;
        public const int MINOR_LOCAL_DELETE_WORK = 0x314; 
        public const int MINOR_REMOTE_DELETE_WORK = 0x315; 
        public const int MINOR_LOCAL_ADD_WORK = 0x316; 
        public const int MINOR_REMOTE_ADD_WORK = 0x317; 
        public const int MINOR_LOCAL_IPCHEATMAP_OUTPUT = 0x318; 
        public const int MINOR_LOCAL_IPCHEATFLOW_OUTPUT = 0x319; 
        public const int MINOR_REMOTE_SMS_SEND = 0x350;
        public const int MINOR_LOCAL_SMS_SEND = 0x351;
        public const int MINOR_ALARM_SMS_SEND = 0x352; 
        public const int MINOR_SMS_RECV = 0x353; 
        
        public const int MINOR_LOCAL_SMS_SEARCH = 0x354;
        public const int MINOR_REMOTE_SMS_SEARCH = 0x355;
        public const int MINOR_LOCAL_SMS_READ = 0x356; 
        public const int MINOR_REMOTE_SMS_READ = 0x357;
        public const int MINOR_REMOTE_DIAL_CONNECT = 0x358; 
        public const int MINOR_REMOTE_DIAL_DISCONN = 0x359; 
        public const int MINOR_LOCAL_WHITELIST_SET = 0x35A; 
        public const int MINOR_REMOTE_WHITELIST_SET = 0x35B; 
        public const int MINOR_LOCAL_DIAL_PARA_SET = 0x35C; 
        public const int MINOR_REMOTE_DIAL_PARA_SET = 0x35D; 
        public const int MINOR_LOCAL_DIAL_SCHEDULE_SET = 0x35E;
        public const int MINOR_REMOTE_DIAL_SCHEDULE_SET = 0x35F; 
        public const int MINOR_PLAT_OPER = 0x36; 

        public const int MINOR_REMOTE_OPEN_DOOR = 0x400;  
        public const int MINOR_REMOTE_CLOSE_DOOR = 0x401;  
        public const int MINOR_REMOTE_ALWAYS_OPEN = 0x402; 
        public const int MINOR_REMOTE_ALWAYS_CLOSE = 0x403; 
        public const int MINOR_REMOTE_CHECK_TIME = 0x404;
        public const int MINOR_NTP_CHECK_TIME = 0x405;  
        public const int MINOR_REMOTE_CLEAR_CARD = 0x406; 
        public const int MINOR_REMOTE_RESTORE_CFG = 0x407; 
        public const int MINOR_ALARMIN_ARM = 0x408; 
        public const int MINOR_ALARMIN_DISARM = 0x409; 
        public const int MINOR_LOCAL_RESTORE_CFG = 0x40a; 
        public const int MINOR_REMOTE_CAPTURE_PIC = 0x40b; 
        public const int MINOR_MOD_NET_REPORT_CFG = 0x40c; 
        public const int MINOR_MOD_GPRS_REPORT_PARAM = 0x40d; 
        public const int MINOR_MOD_REPORT_GROUP_PARAM = 0x40e; 
        public const int MINOR_UNLOCK_PASSWORD_OPEN_DOOR = 0x40f; 

        public const int MINOR_SET_TRIGGERMODE_CFG = 0x1001; 
        public const int MINOR_GET_TRIGGERMODE_CFG = 0x1002;  
        public const int MINOR_SET_IOOUT_CFG = 0x1003; 
        public const int MINOR_GET_IOOUT_CFG = 0x1004; 
        public const int MINOR_GET_TRIGGERMODE_DEFAULT = 0x1005;  
        public const int MINOR_GET_ITCSTATUS = 0x1006; 
        public const int MINOR_SET_STATUS_DETECT_CFG = 0x1007;  
        public const int MINOR_GET_STATUS_DETECT_CFG = 0x1008; 
        public const int MINOR_GET_VIDEO_TRIGGERMODE_CFG = 0x1009; 
        public const int MINOR_SET_VIDEO_TRIGGERMODE_CFG = 0x100a; 

        public const int MINOR_LOCAL_ADD_CAR_INFO = 0x2001;
        public const int MINOR_LOCAL_MOD_CAR_INFO = 0x2002;  
        public const int MINOR_LOCAL_DEL_CAR_INFO = 0x2003;  
        public const int MINOR_LOCAL_FIND_CAR_INFO = 0x2004; 
        public const int MINOR_LOCAL_ADD_MONITOR_INFO = 0x2005; 
        public const int MINOR_LOCAL_MOD_MONITOR_INFO = 0x2006; 
        public const int MINOR_LOCAL_DEL_MONITOR_INFO = 0x2007; 
        public const int MINOR_LOCAL_FIND_MONITOR_INFO = 0x2008; 
        public const int MINOR_LOCAL_FIND_NORMAL_PASS_INFO = 0x2009;
        public const int MINOR_LOCAL_FIND_ABNORMAL_PASS_INFO = 0x200a; 
        public const int MINOR_LOCAL_FIND_PEDESTRIAN_PASS_INFO = 0x200b;  
        public const int MINOR_LOCAL_PIC_PREVIEW = 0x200c; 
        public const int MINOR_LOCAL_SET_GATE_PARM_CFG = 0x200d; 
        public const int MINOR_LOCAL_GET_GATE_PARM_CFG = 0x200e; 
        public const int MINOR_LOCAL_SET_DATAUPLOAD_PARM_CFG = 0x200f;
        public const int MINOR_LOCAL_GET_DATAUPLOAD_PARM_CFG = 0x2010;

        public const int MINOR_LOCAL_DEVICE_CONTROL = 0x2011; 
        public const int MINOR_LOCAL_ADD_EXTERNAL_DEVICE_INFO = 0x2012;  
        public const int MINOR_LOCAL_MOD_EXTERNAL_DEVICE_INFO = 0x2013; 
        public const int MINOR_LOCAL_DEL_EXTERNAL_DEVICE_INFO = 0x2014; 
        public const int MINOR_LOCAL_FIND_EXTERNAL_DEVICE_INFO = 0x2015; 
        public const int MINOR_LOCAL_ADD_CHARGE_RULE = 0x2016;  
        public const int MINOR_LOCAL_MOD_CHARGE_RULE = 0x2017; 
        public const int MINOR_LOCAL_DEL_CHARGE_RULE = 0x2018; 
        public const int MINOR_LOCAL_FIND_CHARGE_RULE = 0x2019; 
        public const int MINOR_LOCAL_COUNT_NORMAL_CURRENTINFO = 0x2020; 
        public const int MINOR_LOCAL_EXPORT_NORMAL_CURRENTINFO_REPORT = 0x2021;
        public const int MINOR_LOCAL_COUNT_ABNORMAL_CURRENTINFO = 0x2022; 
        public const int MINOR_LOCAL_EXPORT_ABNORMAL_CURRENTINFO_REPORT = 0x2023;
        public const int MINOR_LOCAL_COUNT_PEDESTRIAN_CURRENTINFO = 0x2024; 
        public const int MINOR_LOCAL_EXPORT_PEDESTRIAN_CURRENTINFO_REPORT = 0x2025; 
        public const int MINOR_LOCAL_FIND_CAR_CHARGEINFO = 0x2026; 
        public const int MINOR_LOCAL_COUNT_CAR_CHARGEINFO = 0x2027;  
        public const int MINOR_LOCAL_EXPORT_CAR_CHARGEINFO_REPORT = 0x2028;
        public const int MINOR_LOCAL_FIND_SHIFTINFO = 0x2029; 
        public const int MINOR_LOCAL_FIND_CARDINFO = 0x2030;  
        public const int MINOR_LOCAL_ADD_RELIEF_RULE = 0x2031;
        public const int MINOR_LOCAL_MOD_RELIEF_RULE = 0x2032;  
        public const int MINOR_LOCAL_DEL_RELIEF_RULE = 0x2033; 
        public const int MINOR_LOCAL_FIND_RELIEF_RULE = 0x2034;  
        public const int MINOR_LOCAL_GET_ENDETCFG = 0x2035;
        public const int MINOR_LOCAL_SET_ENDETCFG = 0x2036; 
        public const int MINOR_LOCAL_SET_ENDEV_ISSUEDDATA = 0x2037; 
        public const int MINOR_LOCAL_DEL_ENDEV_ISSUEDDATA = 0x2038; 
        public const int MINOR_REMOTE_DEVICE_CONTROL = 0x2101;  
        public const int MINOR_REMOTE_SET_GATE_PARM_CFG = 0x2102;
        public const int MINOR_REMOTE_GET_GATE_PARM_CFG = 0x2103; 
        public const int MINOR_REMOTE_SET_DATAUPLOAD_PARM_CFG = 0x2104; 
        public const int MINOR_REMOTE_GET_DATAUPLOAD_PARM_CFG = 0x2105; 
        public const int MINOR_REMOTE_GET_BASE_INFO = 0x2106;
        public const int MINOR_REMOTE_GET_OVERLAP_CFG = 0x2107; 
        public const int MINOR_REMOTE_SET_OVERLAP_CFG = 0x2108; 
        public const int MINOR_REMOTE_GET_ROAD_INFO = 0x2109;
        public const int MINOR_REMOTE_START_TRANSCHAN = 0x210a; 
        public const int MINOR_REMOTE_GET_ECTWORKSTATE = 0x210b;  
        public const int MINOR_REMOTE_GET_ECTCHANINFO = 0x210c; 
        public const int MINOR_REMOTE_ADD_EXTERNAL_DEVICE_INFO = 0x210d;  
        public const int MINOR_REMOTE_MOD_EXTERNAL_DEVICE_INFO = 0x210e; 
        public const int MINOR_REMOTE_GET_ENDETCFG = 0x210f;  
        public const int MINOR_REMOTE_SET_ENDETCFG = 0x2110; 
        public const int MINOR_REMOTE_ENDEV_ISSUEDDATA = 0x2111;
        public const int MINOR_REMOTE_DEL_ENDEV_ISSUEDDATA = 0x2112; 

        public const int MINOR_REMOTE_ON_CTRL_LAMP = 0x2115; 
        public const int MINOR_REMOTE_OFF_CTRL_LAMP = 0x2116; 
        public const int MINOR_SET_VOICE_LEVEL_PARAM = 0x2117;
        public const int MINOR_SET_VOICE_INTERCOM_PARAM = 0x2118; 
        public const int MINOR_SET_INTELLIGENT_PARAM = 0x2119;
        public const int MINOR_LOCAL_SET_RAID_SPEED = 0x211a;  
        public const int MINOR_REMOTE_SET_RAID_SPEED = 0x211b; 
        public const int MINOR_REMOTE_CREATE_STORAGE_POOL = 0x211c;
        public const int MINOR_REMOTE_DEL_STORAGE_POOL = 0x211d; 

        public const int MINOR_REMOTE_DEL_PIC = 0x2120;
        public const int MINOR_REMOTE_DEL_RECORD = 0x2121; 
        public const int MINOR_REMOTE_CLOUD_ENABLE = 0x2123; 
        public const int MINOR_REMOTE_CLOUD_DISABLE = 0x2124; 
        public const int MINOR_REMOTE_CLOUD_MODIFY_PARAM = 0x2125; 
        public const int MINOR_REMOTE_CLOUD_MODIFY_VOLUME = 0x2126; 
        public const int MINOR_REMOTE_GET_GB28181_SERVICE_PARAM = 0x2127; 
        public const int MINOR_REMOTE_SET_GB28181_SERVICE_PARAM = 0x2128;
        public const int MINOR_LOCAL_GET_GB28181_SERVICE_PARAM = 0x2129; 
        public const int MINOR_LOCAL_SET_GB28181_SERVICE_PARAM = 0x212a; 
        public const int MINOR_REMOTE_SET_SIP_SERVER = 0x212b;
        public const int MINOR_LOCAL_SET_SIP_SERVER = 0x212c; 
        public const int MINOR_LOCAL_BLACKWHITEFILE_OUTPUT = 0x212d;
        public const int MINOR_LOCAL_BLACKWHITEFILE_INPUT = 0x212e; 
        public const int MINOR_REMOTE_BALCKWHITECFGFILE_OUTPUT = 0x212f; 
        public const int MINOR_REMOTE_BALCKWHITECFGFILE_INPUT = 0x2130; 

        public const int MINOR_REMOTE_CREATE_MOD_VIEWLIB_SPACE = 0x2200; 
        public const int MINOR_REMOTE_DELETE_VIEWLIB_FILE = 0x2201; 
        public const int MINOR_REMOTE_DOWNLOAD_VIEWLIB_FILE = 0x2202; 
        public const int MINOR_REMOTE_UPLOAD_VIEWLIB_FILE = 0x2203;  
        public const int MINOR_LOCAL_CREATE_MOD_VIEWLIB_SPACE = 0x2204;

        public const int MINOR_LOCAL_SET_DEVICE_ACTIVE = 0x3000;
        public const int MINOR_REMOTE_SET_DEVICE_ACTIVE = 0x3001; 
        public const int MINOR_LOCAL_PARA_FACTORY_DEFAULT = 0x3002; 
        public const int MINOR_REMOTE_PARA_FACTORY_DEFAULT = 0x3003; 

      
        public const int MAJOR_INFORMATION = 4;
        
        public const int MINOR_HDD_INFO = 0xa1;
        public const int MINOR_SMART_INFO = 0xa2; 
        public const int MINOR_REC_START = 0xa3; 
        public const int MINOR_REC_STOP = 0xa4; 
        public const int MINOR_REC_OVERDUE = 0xa5;  
        public const int MINOR_LINK_START = 0xa6; 
        public const int MINOR_LINK_STOP = 0xa7;
        public const int MINOR_NET_DISK_INFO = 0xa8; 
        public const int MINOR_RAID_INFO = 0xa9; 
        public const int MINOR_RUN_STATUS_INFO = 0xaa;
        public const int MINOR_SPARE_START_BACKUP = 0xab;  
        public const int MINOR_SPARE_STOP_BACKUP = 0xac; 
        public const int MINOR_SPARE_CLIENT_INFO = 0xad;  
        public const int MINOR_ANR_RECORD_START = 0xae;  
        public const int MINOR_ANR_RECORD_END = 0xaf; 
        public const int MINOR_ANR_ADD_TIME_QUANTUM = 0xb0; 
        public const int MINOR_ANR_DEL_TIME_QUANTUM = 0xb1; 
        public const int MINOR_PIC_REC_START = 0xb3; 
        public const int MINOR_PIC_REC_STOP = 0xb4;  
        public const int MINOR_PIC_REC_OVERDUE = 0xb5; 
        public const int MINOR_CLIENT_LOGIN = 0xb6; 
        public const int MINOR_CLIENT_RELOGIN = 0xb7; 
        public const int MINOR_CLIENT_LOGOUT = 0xb8;  
        public const int MINOR_CLIENT_SYNC_START = 0xb9; 
        public const int MINOR_CLIENT_SYNC_STOP = 0xba;
        public const int MINOR_CLIENT_SYNC_SUCC = 0xbb;
        public const int MINOR_CLIENT_SYNC_EXCP = 0xbc; 
        public const int MINOR_GLOBAL_RECORD_ERR_INFO = 0xbd;  
        public const int MINOR_BUFFER_STATE = 0xbe; 
        public const int MINOR_DISK_ERRORINFO_V2 = 0xbf; 
        public const int MINOR_UNLOCK_RECORD = 0xc3; 
        public const int MINOR_VIS_ALARM = 0xc4;  
        public const int MINOR_TALK_RECORD = 0xc5; 
           
        public const int MAJOR_EVENT = 0x5;
    
        public const int MINOR_LEGAL_CARD_PASS = 0x01;
        public const int MINOR_CARD_AND_PSW_PASS = 0x02; 
        public const int MINOR_CARD_AND_PSW_FAIL = 0x03;
        public const int MINOR_CARD_AND_PSW_TIMEOUT = 0x04;
        public const int MINOR_CARD_AND_PSW_OVER_TIME = 0x05; 
        public const int MINOR_CARD_NO_RIGHT = 0x06;  
        public const int MINOR_CARD_INVALID_PERIOD = 0x07;
        public const int MINOR_CARD_OUT_OF_DATE = 0x08; 
        public const int MINOR_INVALID_CARD = 0x09;  
        public const int MINOR_ANTI_SNEAK_FAIL = 0x0a;
        public const int MINOR_INTERLOCK_DOOR_NOT_CLOSE = 0x0b; 
        public const int MINOR_NOT_BELONG_MULTI_GROUP = 0x0c; 
        public const int MINOR_INVALID_MULTI_VERIFY_PERIOD = 0x0d; 
        public const int MINOR_MULTI_VERIFY_SUPER_RIGHT_FAIL = 0x0e; 
        public const int MINOR_MULTI_VERIFY_REMOTE_RIGHT_FAIL = 0x0f; 
        public const int MINOR_MULTI_VERIFY_SUCCESS = 0x10;
        public const int MINOR_LEADER_CARD_OPEN_BEGIN = 0x11; 
        public const int MINOR_LEADER_CARD_OPEN_END = 0x12;  
        public const int MINOR_ALWAYS_OPEN_BEGIN = 0x13; 
        public const int MINOR_ALWAYS_OPEN_END = 0x14; 
        public const int MINOR_LOCK_OPEN = 0x15; 
        public const int MINOR_LOCK_CLOSE = 0x16; 
        public const int MINOR_DOOR_BUTTON_PRESS = 0x17; 
        public const int MINOR_DOOR_BUTTON_RELEASE = 0x18;  
        public const int MINOR_DOOR_OPEN_NORMAL = 0x19; 
        public const int MINOR_DOOR_CLOSE_NORMAL = 0x1a; 
        public const int MINOR_DOOR_OPEN_ABNORMAL = 0x1b; 
        public const int MINOR_DOOR_OPEN_TIMEOUT = 0x1c;  
        public const int MINOR_ALARMOUT_ON = 0x1d;  
        public const int MINOR_ALARMOUT_OFF = 0x1e; 
        public const int MINOR_ALWAYS_CLOSE_BEGIN = 0x1f; 
        public const int MINOR_ALWAYS_CLOSE_END = 0x20;
        public const int MINOR_MULTI_VERIFY_NEED_REMOTE_OPEN = 0x21;
        public const int MINOR_MULTI_VERIFY_SUPERPASSWD_VERIFY_SUCCESS = 0x22; 
        public const int MINOR_MULTI_VERIFY_REPEAT_VERIFY = 0x23;
        public const int MINOR_MULTI_VERIFY_TIMEOUT = 0x24; 
        public const int MINOR_DOORBELL_RINGING = 0x25; 
        public const int MINOR_FINGERPRINT_COMPARE_PASS = 0x26; 
        public const int MINOR_FINGERPRINT_COMPARE_FAIL = 0x27;
        public const int MINOR_CARD_FINGERPRINT_VERIFY_PASS = 0x28; 
        public const int MINOR_CARD_FINGERPRINT_VERIFY_FAIL = 0x29; 
        public const int MINOR_CARD_FINGERPRINT_VERIFY_TIMEOUT = 0x2a;
        public const int MINOR_CARD_FINGERPRINT_PASSWD_VERIFY_PASS = 0x2b; 
        public const int MINOR_CARD_FINGERPRINT_PASSWD_VERIFY_FAIL = 0x2c; 
        public const int MINOR_CARD_FINGERPRINT_PASSWD_VERIFY_TIMEOUT = 0x2d; 
        public const int MINOR_FINGERPRINT_PASSWD_VERIFY_PASS = 0x2e; 
        public const int MINOR_FINGERPRINT_PASSWD_VERIFY_FAIL = 0x2f; 
        public const int MINOR_FINGERPRINT_PASSWD_VERIFY_TIMEOUT = 0x30; 
        public const int MINOR_FINGERPRINT_INEXISTENCE = 0x31;

        public const int PARA_VIDEOOUT = 1;
        public const int PARA_IMAGE = 2;
        public const int PARA_ENCODE = 4;
        public const int PARA_NETWORK = 8;
        public const int PARA_ALARM = 16;
        public const int PARA_EXCEPTION = 32;
        public const int PARA_DECODER = 64;
        public const int PARA_RS232 = 128;
        public const int PARA_PREVIEW = 256;
        public const int PARA_SECURITY = 512;
        public const int PARA_DATETIME = 1024;
        public const int PARA_FRAMETYPE = 2048;

       
        public const int PARA_DETECTION = 0x1000;   
        public const int PARA_VCA_RULE = 0x1001;  
        public const int PARA_VCA_CTRL = 0x1002;  
        public const int PARA_VCA_PLATE = 0x1003; 

        public const int PARA_CODESPLITTER = 0x2000; 

        public const int PARA_RS485 = 0x2001; 
        public const int PARA_DEVICE = 0x2002;
        public const int PARA_HARDDISK = 0x2003;
        public const int PARA_AUTOBOOT = 0x2004; 
        public const int PARA_HOLIDAY = 0x2005; 
        public const int PARA_IPC = 0x2006;

        
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_LOG_V30
        {
            public NET_DVR_TIME strLogTime;
            public uint dwMajorType;
            public uint dwMinorType;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_NAMELEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sPanelUser;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_NAMELEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sNetUser;
            public NET_DVR_IPADDR struRemoteHostAddr;
            public uint dwParaType;
            public uint dwChannel;
            public uint dwDiskNumber;
            public uint dwAlarmInPort;
            public uint dwAlarmOutPort;
            public uint dwInfoLen;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = LOG_INFO_LEN)]
            public string sInfo;
        }

        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_LOG
        {
            public NET_DVR_TIME strLogTime;
            public uint dwMajorType;
            public uint dwMinorType;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_NAMELEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sPanelUser;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_NAMELEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sNetUser;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sRemoteHostAddr;
            public uint dwParaType;
            public uint dwChannel;
            public uint dwDiskNumber;
            public uint dwAlarmInPort;
            public uint dwAlarmOutPort;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_ALARMOUTSTATUS_V30
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_ALARMOUT_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] Output;

            public void Init()
            {
                Output = new byte[MAX_ALARMOUT_V30];
            }
        }
        
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_ALARMOUTSTATUS
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.I1)]
            public byte[] Output;
        }
        
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_TRADEINFO
        {
            public ushort m_Year;
            public ushort m_Month;
            public ushort m_Day;
            public ushort m_Hour;
            public ushort m_Minute;
            public ushort m_Second;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 24, ArraySubType = UnmanagedType.I1)]
            public byte[] DeviceName;
            public uint dwChannelNumer;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.I1)]
            public byte[] CardNumber;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 12)]
            public string cTradeType;
            public uint dwCash;
        }
        
        public const int NCR = 0;
        public const int DIEBOLD = 1;
        public const int WINCOR_NIXDORF = 2;
        public const int SIEMENS = 3;
        public const int OLIVETTI = 4;
        public const int FUJITSU = 5;
        public const int HITACHI = 6;
        public const int SMI = 7;
        public const int IBM = 8;
        public const int BULL = 9;
        public const int YiHua = 10;
        public const int LiDe = 11;
        public const int GDYT = 12;
        public const int Mini_Banl = 13;
        public const int GuangLi = 14;
        public const int DongXin = 15;
        public const int ChenTong = 16;
        public const int NanTian = 17;
        public const int XiaoXing = 18;
        public const int GZYY = 19;
        public const int QHTLT = 20;
        public const int DRS918 = 21;
        public const int KALATEL = 22;
        public const int NCR_2 = 23;
        public const int NXS = 24;

        /*Ö¡¸ñÊ½*/
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_FRAMETYPECODE
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 12, ArraySubType = UnmanagedType.I1)]
            public byte[] code;
        }

        
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_FRAMEFORMAT_V30
        {
            public uint dwSize;
            public NET_DVR_IPADDR struATMIP;
            public uint dwATMType;
            public uint dwInputMode;
            public uint dwFrameSignBeginPos;
            public uint dwFrameSignLength;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 12, ArraySubType = UnmanagedType.I1)]
            public byte[] byFrameSignContent;
            public uint dwCardLengthInfoBeginPos;
            public uint dwCardLengthInfoLength;
            public uint dwCardNumberInfoBeginPos;
            public uint dwCardNumberInfoLength;
            public uint dwBusinessTypeBeginPos;
            public uint dwBusinessTypeLength;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 10, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_FRAMETYPECODE[] frameTypeCode;
            public ushort wATMPort;
            public ushort wProtocolType;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 24, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }
       
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_FRAMEFORMAT
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sATMIP;
            public uint dwATMType;
            public uint dwInputMode;
            public uint dwFrameSignBeginPos;
            public uint dwFrameSignLength;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 12, ArraySubType = UnmanagedType.I1)]
            public byte[] byFrameSignContent;
            public uint dwCardLengthInfoBeginPos;
            public uint dwCardLengthInfoLength;
            public uint dwCardNumberInfoBeginPos;
            public uint dwCardNumberInfoLength;
            public uint dwBusinessTypeBeginPos;
            public uint dwBusinessTypeLength;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 10, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_FRAMETYPECODE[] frameTypeCode;
        }

        //SDK_V31 ATM 
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_DVR_FILTER
        {
            public byte byEnable;
            public byte byMode;
            public byte byFrameBeginPos;   
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 1, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.I1)]
            public byte[] byFilterText;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 12, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_DVR_IDENTIFICAT
        {
            public byte byStartMode;
            public byte byEndMode;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
            public NET_DVR_FRAMETYPECODE struStartCode;
            public NET_DVR_FRAMETYPECODE struEndCode;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 12, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_DVR_PACKAGE_LOCATION
        {
            public byte byOffsetMode;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            public uint dwOffsetPos;
            public NET_DVR_FRAMETYPECODE struTokenCode;
            public byte byMultiplierValue;
            public byte byEternOffset;
            public byte byCodeMode;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 9, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
        }

       
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_DVR_PACKAGE_LENGTH
        {
            public byte byLengthMode;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            public uint dwFixLength;
            public uint dwMaxLength;
            public uint dwMinLength;
            public byte byEndMode;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
            public NET_DVR_FRAMETYPECODE struEndCode;
            public uint dwLengthPos;
            public uint dwLengthLen;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes3;
        }
     
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_DVR_OSD_POSITION
        {
            public byte byPositionMode;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            public uint dwPos_x;
            public uint dwPos_y;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
        }

        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct tagNET_DVR_DATE_FORMAT
        {
            public byte byItem1;/*Month,0.mm;1.mmm;2.mmmm*/
            public byte byItem2;/*Day,0.dd;*/
            public byte byItem3;/*Year,0.yy;1.yyyy*/
            public byte byDateForm;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 20, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 4)]
            public string chSeprator;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 4)]
            public string chDisplaySeprator;
            public byte byDisplayForm;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 27, ArraySubType = UnmanagedType.I1)]
            public byte[] res;
        }
       
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct tagNET_DVRT_TIME_FORMAT
        {
            public byte byTimeForm;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 23, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            public byte byHourMode;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 4)]
            public string chSeprator;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 4)]
            public string chDisplaySeprator;
            public byte byDisplayForm;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes3;
            public byte byDisplayHourMode;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 19, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes4;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_DVR_OVERLAY_CHANNEL
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 64, ArraySubType = UnmanagedType.I1)]
            public byte[] byChannel;
            public uint dwDelayTime;
            public byte byEnableDelayTime;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 11, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_DVR_ATM_PACKAGE_ACTION
        {
            public tagNET_DVR_PACKAGE_LOCATION struPackageLocation;
            public tagNET_DVR_OSD_POSITION struOsdPosition;
            public NET_DVR_FRAMETYPECODE struActionCode;
            public NET_DVR_FRAMETYPECODE struPreCode;
            public byte byActionCodeMode;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 7, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_DVR_ATM_PACKAGE_DATE
        {
            public tagNET_DVR_PACKAGE_LOCATION struPackageLocation;
            public tagNET_DVR_DATE_FORMAT struDateForm;
            public tagNET_DVR_OSD_POSITION struOsdPosition;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = UnmanagedType.I1)]
            public byte[] res;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_DVR_ATM_PACKAGE_TIME
        {
            public tagNET_DVR_PACKAGE_LOCATION location;
            public tagNET_DVRT_TIME_FORMAT struTimeForm;
            public tagNET_DVR_OSD_POSITION struOsdPosition;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_DVR_ATM_PACKAGE_OTHERS
        {
            public tagNET_DVR_PACKAGE_LOCATION struPackageLocation;
            public tagNET_DVR_PACKAGE_LENGTH struPackageLength;
            public tagNET_DVR_OSD_POSITION struOsdPosition;
            public NET_DVR_FRAMETYPECODE struPreCode;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = UnmanagedType.I1)]
            public byte[] res;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_DVR_ATM_FRAMETYPE_NEW
        {
            public byte byEnable;
            public byte byInputMode;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.I1)]
            public byte[] byAtmName;
            public NET_DVR_IPADDR struAtmIp;
            public ushort wAtmPort;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
            public uint dwAtmType;
            public tagNET_DVR_IDENTIFICAT struIdentification;
            public tagNET_DVR_FILTER struFilter;
            public tagNET_DVR_ATM_PACKAGE_OTHERS struCardNoPara;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_ACTION_TYPE, ArraySubType = UnmanagedType.Struct)]
            public tagNET_DVR_ATM_PACKAGE_ACTION[] struTradeActionPara;
            public tagNET_DVR_ATM_PACKAGE_OTHERS struAmountPara;
            public tagNET_DVR_ATM_PACKAGE_OTHERS struSerialNoPara;
            public tagNET_DVR_OVERLAY_CHANNEL struOverlayChan;
            public tagNET_DVR_ATM_PACKAGE_DATE byRes4;
            public tagNET_DVR_ATM_PACKAGE_TIME byRes5;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 132, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes3;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_DVR_FRAMEFORMAT_V31
        {
            public uint dwSize;
            public tagNET_DVR_ATM_FRAMETYPE_NEW struAtmFrameTypeNew;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.Struct)]
            public tagNET_DVR_ATM_FRAMETYPE_NEW[] byRes;
        }

        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct tagNET_DVR_ATM_PROTOIDX
        {
            public uint dwAtmType;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = ATM_DESC_LEN)]
            public string chDesc;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_DVR_ATM_PROTOCOL
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_ATM_PROTOCOL_NUM, ArraySubType = UnmanagedType.Struct)]
            public tagNET_DVR_ATM_PROTOIDX[] struAtmProtoidx;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = ATM_PROTOCOL_SORT, ArraySubType = UnmanagedType.U4)]
            public uint[] dwAtmNumPerSort;
        }

        /*****************************DS-6001D/F(begin)***************************/
        //DS-6001D Decoder
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_DECODERINFO
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.I1)]
            public byte[] byEncoderIP;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.I1)]
            public byte[] byEncoderUser;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.I1)]
            public byte[] byEncoderPasswd;
            public byte bySendMode;
            public byte byEncoderChannel;
            public ushort wEncoderPort;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.I1)]
            public byte[] reservedData;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_DECODERSTATE
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.I1)]
            public byte[] byEncoderIP;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.I1)]
            public byte[] byEncoderUser;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.I1)]
            public byte[] byEncoderPasswd;
            public byte byEncoderChannel;
            public byte bySendMode;
            public ushort wEncoderPort;
            public uint dwConnectState;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.I1)]
            public byte[] reservedData;
        }

        public const int NET_DEC_STARTDEC = 1;
        public const int NET_DEC_STOPDEC = 2;
        public const int NET_DEC_STOPCYCLE = 3;
        public const int NET_DEC_CONTINUECYCLE = 4;

        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_DECCHANINFO
        {
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sDVRIP;
            public ushort wDVRPort;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sUserName;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = PASSWD_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sPassword;
            public byte byChannel;
            public byte byLinkMode;
            public byte byLinkType;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_DECINFO
        {
            public byte byPoolChans;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DECPOOLNUM, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_DECCHANINFO[] struchanConInfo;
            public byte byEnablePoll;
            public byte byPoolTime;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_DECCFG
        {
            public uint dwSize;
            public uint dwDecChanNum;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DECNUM, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_DECINFO[] struDecInfo;
        }

        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_PORTINFO
        {
            public uint dwEnableTransPort;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sDecoderIP;
            public ushort wDecoderPort;
            public ushort wDVRTransPort;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 4)]
            public string cReserve;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_PORTCFG
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_TRANSPARENTNUM, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_PORTINFO[] struTransPortInfo;
        }

        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_PLAYREMOTEFILE
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sDecoderIP;
            public ushort wDecoderPort;
            public ushort wLoadMode;

            [StructLayoutAttribute(LayoutKind.Explicit)]
            public struct mode_size
            {
                [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 100, ArraySubType = UnmanagedType.I1)]
                [FieldOffsetAttribute(0)]
                public byte[] byFile;

                [StructLayoutAttribute(LayoutKind.Sequential)]
                public struct bytime
                {
                    public uint dwChannel;
                    [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.I1)]
                    public byte[] sUserName;
                    [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.I1)]
                    public byte[] sPassword;
                    public NET_DVR_TIME struStartTime;
                    public NET_DVR_TIME struStopTime;
                }
            }
        }

        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_DECCHANSTATUS
        {
            public uint dwWorkType;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sDVRIP;
            public ushort wDVRPort;
            public byte byChannel;
            public byte byLinkMode;
            public uint dwLinkType;

            [StructLayoutAttribute(LayoutKind.Explicit)]
            public struct objectInfo
            {
                [StructLayoutAttribute(LayoutKind.Sequential)]
                public struct userInfo
                {
                    [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.I1)]
                    public byte[] sUserName;
                    [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.I1)]
                    public byte[] sPassword;
                    [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 52)]
                    public string cReserve;
                }

                [StructLayoutAttribute(LayoutKind.Sequential)]
                public struct fileInfo
                {
                    [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 100, ArraySubType = UnmanagedType.I1)]
                    public byte[] fileName;
                }
                [StructLayoutAttribute(LayoutKind.Sequential)]
                public struct timeInfo
                {
                    public uint dwChannel;
                    [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.I1)]
                    public byte[] sUserName;
                    [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.I1)]
                    public byte[] sPassword;
                    public NET_DVR_TIME struStartTime;
                    public NET_DVR_TIME struStopTime;
                }
            }
        }


        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_DECSTATUS
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DECNUM, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_DECCHANSTATUS[] struTransPortInfo;
        }

        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_SHOWSTRINGINFO
        {
            public ushort wShowString;
            public ushort wStringSize;
            public ushort wShowStringTopLeftX;
            public ushort wShowStringTopLeftY;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 44)]
            public string sString;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_SHOWSTRING_V30
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_STRINGNUM_V30, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_SHOWSTRINGINFO[] struStringInfo;

            public void Init()
            {
                struStringInfo = new NET_DVR_SHOWSTRINGINFO[MAX_STRINGNUM_V30];
            }
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_SHOWSTRING_EX
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_STRINGNUM_EX, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_SHOWSTRINGINFO[] struStringInfo;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_SHOWSTRING
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_STRINGNUM, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_SHOWSTRINGINFO[] struStringInfo;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct struReceiver
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sName;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_EMAIL_ADDR_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sAddress;
        }
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_EMAILCFG_V30
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sAccount;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_EMAIL_PWD_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sPassword;

            [StructLayoutAttribute(LayoutKind.Sequential)]
            public struct struSender
            {
                [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
                public byte[] sName;
                [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_EMAIL_ADDR_LEN, ArraySubType = UnmanagedType.I1)]
                public byte[] sAddress;
            }

            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_EMAIL_ADDR_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sSmtpServer;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_EMAIL_ADDR_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sPop3Server;

            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.Struct)]
            public struReceiver[] struStringInfo;

            public byte byAttachment;
            public byte bySmtpServerVerify;
            public byte byMailInterval;
            public byte byEnableSSL;
            public ushort wSmtpPort;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 74, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_CRUISE_PARA
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = CRUISE_MAX_PRESET_NUMS, ArraySubType = UnmanagedType.I1)]
            public byte[] byPresetNo;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = CRUISE_MAX_PRESET_NUMS, ArraySubType = UnmanagedType.I1)]
            public byte[] byCruiseSpeed;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = CRUISE_MAX_PRESET_NUMS, ArraySubType = UnmanagedType.U2)]
            public ushort[] wDwellTime;
            public byte byEnableThisCruise;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 15, ArraySubType = UnmanagedType.I1)]
            public byte[] res;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_TIMEPOINT
        {
            public uint dwMonth;
            public uint dwWeekNo;
            public uint dwWeekDate;
            public uint dwHour;
            public uint dwMin;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_ZONEANDDST
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            public uint dwEnableDST;
            public byte byDSTBias;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
            public NET_DVR_TIMEPOINT struBeginPoint;
            public NET_DVR_TIMEPOINT struEndPoint;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_JPEGPARA
        {
            public ushort wPicSize;
            public ushort wPicQuality;
        }


        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_AUXOUTCFG
        {
            public uint dwSize;
            public uint dwAlarmOutChan;
            public uint dwAlarmChanSwitchTime;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_AUXOUT, ArraySubType = UnmanagedType.U4)]
            public uint[] dwAuxSwitchTime;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_AUXOUT * MAX_WINDOW, ArraySubType = UnmanagedType.I1)]
            public byte[] byAuxOrder;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_NTPPARA
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 64, ArraySubType = UnmanagedType.I1)]
            public byte[] sNTPServer;/* Domain Name or IP addr of NTP server */
            public ushort wInterval;/* adjust time interval(hours) */
            public byte byEnableNTP;/* enable NPT client 0-no£¬1-yes*/
            public byte cTimeDifferenceH;
            public byte cTimeDifferenceM;
            public byte res1;
            public ushort wNtpPort; /* ntp server port 9000*/
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = UnmanagedType.I1)]
            public byte[] res2;
        }

        //ddns
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_DDNSPARA
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sUsername;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = PASSWD_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sPassword;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 64, ArraySubType = UnmanagedType.I1)]
            public byte[] sDomainName; 
            public byte byEnableDDNS;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 15, ArraySubType = UnmanagedType.I1)]
            public byte[] res;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_DDNSPARA_EX
        {
            public byte byHostIndex;/* Hikvision DNS ­Dyndns */
            public byte byEnableDDNS;
            public ushort wDDNSPort;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sUsername;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = PASSWD_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sPassword;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DOMAIN_NAME, ArraySubType = UnmanagedType.I1)]
            public byte[] sDomainName;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DOMAIN_NAME, ArraySubType = UnmanagedType.I1)]
            public byte[] sServerName;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct struDDNS
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sUsername;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = PASSWD_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sPassword;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DOMAIN_NAME, ArraySubType = UnmanagedType.I1)]
            public byte[] sDomainName;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DOMAIN_NAME, ArraySubType = UnmanagedType.I1)]
            public byte[] sServerName;
            public ushort wDDNSPort;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 10, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_DDNSPARA_V30
        {
            public byte byEnableDDNS;
            public byte byHostIndex;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DDNS_NUMS, ArraySubType = UnmanagedType.Struct)]
            public struDDNS[] struDDNS;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
        }

        //email
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_EMAILPARA
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 64, ArraySubType = UnmanagedType.I1)]
            public byte[] sUsername;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 64, ArraySubType = UnmanagedType.I1)]
            public byte[] sPassword;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 64, ArraySubType = UnmanagedType.I1)]
            public byte[] sSmtpServer;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 64, ArraySubType = UnmanagedType.I1)]
            public byte[] sPop3Server;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 64, ArraySubType = UnmanagedType.I1)]
            public byte[] sMailAddr;/* email */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 64, ArraySubType = UnmanagedType.I1)]
            public byte[] sEventMailAddr1;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 64, ArraySubType = UnmanagedType.I1)]
            public byte[] sEventMailAddr2;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.I1)]
            public byte[] res;
        }

        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_NETAPPCFG
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sDNSIp;
            public NET_DVR_NTPPARA struNtpClientParam;
            public NET_DVR_DDNSPARA struDDNSClientParam;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 464, ArraySubType = UnmanagedType.I1)]
            public byte[] res;
        }

        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_SINGLE_NFS
        {
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sNfsHostIPAddr;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = PATHNAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sNfsDirectory;

            public void Init()
            {
                this.sNfsDirectory = new byte[PATHNAME_LEN];
            }
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_NFSCFG
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_NFS_DISK, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_SINGLE_NFS[] struNfsDiskParam;

            public void Init()
            {
                this.struNfsDiskParam = new NET_DVR_SINGLE_NFS[MAX_NFS_DISK];

                for (int i = 0; i < MAX_NFS_DISK; i++)
                {
                    struNfsDiskParam[i].Init();
                }
            }
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_CRUISE_POINT
        {
            public byte PresetNum;
            public byte Dwell;
            public byte Speed;
            public byte Reserve;

            public void Init()
            {
                PresetNum = 0;
                Dwell = 0;
                Speed = 0;
                Reserve = 0;
            }
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_CRUISE_RET
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_CRUISE_POINT[] struCruisePoint;

            public void Init()
            {
                struCruisePoint = new NET_DVR_CRUISE_POINT[32];
                for (int i = 0; i < 32; i++)
                {
                    struCruisePoint[i].Init();
                }
            }
        }
        
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_NETCFG_OTHER
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sFirstDNSIP;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sSecondDNSIP;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string sRes;
        }

        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_MATRIX_DECINFO
        {
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sDVRIP;
            public ushort wDVRPort;
            public byte byChannel;
            public byte byTransProtocol;
            public byte byTransMode;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sUserName;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = PASSWD_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sPassword;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_MATRIX_DYNAMIC_DEC
        {
            public uint dwSize;
            public NET_DVR_MATRIX_DECINFO struDecChanInfo;
        }

        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_MATRIX_DEC_CHAN_STATUS
        {
            public uint dwSize;
            public uint dwIsLinked;
            public uint dwStreamCpRate;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string cRes;
        }

        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_MATRIX_DEC_CHAN_INFO
        {
            public uint dwSize;
            public NET_DVR_MATRIX_DECINFO struDecChanInfo;
            public uint dwDecState;
            public NET_DVR_TIME StartTime;
            public NET_DVR_TIME StopTime;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string sFileName;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_MATRIX_DECCHANINFO
        {
            public uint dwEnable;
            public NET_DVR_MATRIX_DECINFO struDecChanInfo;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_MATRIX_LOOP_DECINFO
        {
            public uint dwSize;
            public uint dwPoolTime;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CYCLE_CHAN, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_MATRIX_DECCHANINFO[] struchanConInfo;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct TTY_CONFIG
        {
            public byte baudrate;
            public byte databits;
            public byte stopbits;
            public byte parity;
            public byte flowcontrol;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] res;
        }

        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_MATRIX_TRAN_CHAN_INFO
        {
            public byte byTranChanEnable;
            
            public byte byLocalSerialDevice;/* Local serial device */
           
            public byte byRemoteSerialDevice;/* Remote output serial device */
            public byte res1;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sRemoteDevIP;/* Remote Device IP */
            public ushort wRemoteDevPort;/* Remote Net Communication Port */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            public byte[] res2;
            public TTY_CONFIG RemoteSerialDevCfg;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_MATRIX_TRAN_CHAN_CONFIG
        {
            public uint dwSize;
            public byte by232IsDualChan;
            public byte by485IsDualChan;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            public byte[] res;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_SERIAL_NUM, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_MATRIX_TRAN_CHAN_INFO[] struTranInfo;
        }

        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_MATRIX_DEC_REMOTE_PLAY
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sDVRIP;
            public ushort wDVRPort;
            public byte byChannel;
            public byte byReserve;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sUserName;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = PASSWD_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sPassword;
            public uint dwPlayMode;
            public NET_DVR_TIME StartTime;
            public NET_DVR_TIME StopTime;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string sFileName;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_MATRIX_DEC_REMOTE_PLAY_CONTROL
        {
            public uint dwSize;
            public uint dwPlayCmd;
            public uint dwCmdParam;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_MATRIX_DEC_REMOTE_PLAY_STATUS
        {
            public uint dwSize;
            public uint dwCurMediaFileLen;
            public uint dwCurMediaFilePosition;
            public uint dwCurMediaFileDuration;
            public uint dwCurPlayTime;
            public uint dwCurMediaFIleFrames;
            public uint dwCurDataType;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 72, ArraySubType = UnmanagedType.I1)]
            public byte[] res;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_MATRIX_PASSIVEMODE
        {
            public ushort wTransProtol;
            public ushort wPassivePort;
           
            public NET_DVR_IPADDR struMcastIP;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = UnmanagedType.I1)]
            public byte[] res;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagDEV_CHAN_INFO
        {
            public NET_DVR_IPADDR struIP;
            public ushort wDVRPort;
            public byte byChannel;
            public byte byTransProtocol;
            public byte byTransMode;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 71, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sUserName;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = PASSWD_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sPassword;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagMATRIX_TRAN_CHAN_INFO
        {
            public byte byTranChanEnable;
           
            public byte byLocalSerialDevice;/* Local serial device */
            
            public byte byRemoteSerialDevice;/* Remote output serial device */
            public byte byRes1;
            public NET_DVR_IPADDR struRemoteDevIP;/* Remote Device IP */
            public ushort wRemoteDevPort;/* Remote Net Communication Port */
            public byte byIsEstablished;
            public byte byRes2;
            public TTY_CONFIG RemoteSerialDevCfg;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byUsername;/* 32BYTES */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = PASSWD_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byPassword;/* 16BYTES */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes3;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagMATRIX_TRAN_CHAN_CONFIG
        {
            public uint dwSize;
            public byte by232IsDualChan;
            public byte by485IsDualChan;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            public byte[] vyRes;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_SERIAL_NUM, ArraySubType = UnmanagedType.Struct)]
            public tagMATRIX_TRAN_CHAN_INFO[] struTranInfo;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_STREAM_MEDIA_SERVER_CFG
        {
            public byte byValid;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            public NET_DVR_IPADDR struDevIP;
            public ushort wDevPort;
            public byte byTransmitType;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 69, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_PU_STREAM_CFG
        {
            public uint dwSize;
            public NET_DVR_STREAM_MEDIA_SERVER_CFG struStreamMediaSvrCfg;
            public tagDEV_CHAN_INFO struDevChanInfo;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_MATRIX_CHAN_INFO_V30
        {
            public uint dwEnable;
            public NET_DVR_STREAM_MEDIA_SERVER_CFG streamMediaServerCfg;
            public tagDEV_CHAN_INFO struDevChanInfo;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagMATRIX_LOOP_DECINFO_V30
        {
            public uint dwSize;
            public uint dwPoolTime;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CYCLE_CHAN_V30, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_MATRIX_CHAN_INFO_V30[] struchanConInfo;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct tagDEC_MATRIX_CHAN_INFO
        {
            public uint dwSize;
            public NET_DVR_STREAM_MEDIA_SERVER_CFG streamMediaServerCfg;
            public tagDEV_CHAN_INFO struDevChanInfo;
            public uint dwDecState;
            public NET_DVR_TIME StartTime;
            public NET_DVR_TIME StopTime;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string sFileName;
            public uint dwGetStreamMode;
            public tagNET_MATRIX_PASSIVEMODE struPassiveMode;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_DVR_MATRIX_ABILITY
        {
            public uint dwSize;
            public byte byDecNums;
            public byte byStartChan;
            public byte byVGANums;
            public byte byBNCNums;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 12, ArraySubType = UnmanagedType.I1)]
            public byte[] byVGAWindowMode;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.I1)]
            public byte[] byBNCWindowMode;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.I1)]
            public byte[] res;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_DVR_DISP_LOGOCFG
        {
            public uint dwCorordinateX;
            public uint dwCorordinateY;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            public byte byFlash;
            public byte byTranslucent;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 6, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
            public uint dwLogoSize;
        }

    
        public const int NET_DVR_ENCODER_UNKOWN = 0;
        public const int NET_DVR_ENCODER_H264 = 1;/*HIK 264*/
        public const int NET_DVR_ENCODER_S264 = 2;/*Standard H264*/
        public const int NET_DVR_ENCODER_MPEG4 = 3;/*MPEG4*/
      
        public const int NET_DVR_STREAM_TYPE_UNKOWN = 0;
        public const int NET_DVR_STREAM_TYPE_HIKPRIVT = 1; 
        public const int NET_DVR_STREAM_TYPE_TS = 7;
        public const int NET_DVR_STREAM_TYPE_PS = 8;
        public const int NET_DVR_STREAM_TYPE_RTP = 9;

       
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_MATRIX_CHAN_STATUS
        {
            public byte byDecodeStatus;
            public byte byStreamType;
            public byte byPacketType;
            public byte byRecvBufUsage;
            public byte byDecBufUsage;
            public byte byFpsDecV;
            public byte byFpsDecA;
            public byte byCpuLoad;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            public uint dwDecodedV;
            public uint dwDecodedA;
            public ushort wImgW;
            public ushort wImgH; 
            public byte byVideoFormat;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 27, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
        }
        
        public const int NET_DVR_MAX_DISPREGION = 16;
        
        public enum VGA_MODE
        {
            VGA_NOT_AVALIABLE,
            VGA_THS8200_MODE_SVGA_60HZ,//800*600
            VGA_THS8200_MODE_SVGA_75HZ, //800*600
            VGA_THS8200_MODE_XGA_60HZ,//1024*768
            VGA_THS8200_MODE_XGA_70HZ, //1024*768
            VGA_THS8200_MODE_SXGA_60HZ,//1280*1024
            VGA_THS8200_MODE_720P_60HZ,//1280*720 
            VGA_THS8200_MODE_1080i_60HZ,//1920*1080
            VGA_THS8200_MODE_1080P_30HZ,//1920*1080
            VGA_THS8200_MODE_1080P_25HZ,//1920*1080
            VGA_THS8200_MODE_UXGA_30HZ,//1600*1200
        }

        public enum VIDEO_STANDARD
        {
            VS_NON = 0,
            VS_NTSC = 1,
            VS_PAL = 2,
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_DVR_VGA_DISP_CHAN_CFG
        {
            public uint dwSize;
            public byte byAudio;
            public byte byAudioWindowIdx;
            public byte byVgaResolution;
            public byte byVedioFormat;
            public uint dwWindowMode;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_WINDOWS, ArraySubType = UnmanagedType.I1)]
            public byte[] byJoinDecChan;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 20, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_DISP_CHAN_STATUS
        {
            public byte byDispStatus;
            public byte byBVGA;
            public byte byVideoFormat;
            public byte byWindowMode;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 10, ArraySubType = UnmanagedType.I1)]
            public byte[] byJoinDecChan;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NET_DVR_MAX_DISPREGION, ArraySubType = UnmanagedType.I1)]
            public byte[] byFpsDisp;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
        }

        public const int MAX_DECODECHANNUM = 32;
        public const int MAX_DISPCHANNUM = 24;

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_DVR__DECODER_WORK_STATUS
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DECODECHANNUM, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_MATRIX_CHAN_STATUS[] struDecChanStatus;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DISPCHANNUM, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_DISP_CHAN_STATUS[] struDispChanStatus;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_ANALOG_ALARMIN, ArraySubType = UnmanagedType.I1)]
            public byte[] byAlarmInStatus;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_ANALOG_ALARMOUT, ArraySubType = UnmanagedType.I1)]
            public byte[] byAalarmOutStatus;
            public byte byAudioInChanStatus;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 127, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }
        
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_EMAILCFG
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string sUserName;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string sPassWord;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string sFromName;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 48)]
            public string sFromAddr;/* Sender address */
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string sToName1;/* Receiver1 */
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string sToName2;/* Receiver2 */
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 48)]
            public string sToAddr1;/* Receiver address1 */
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 48)]
            public string sToAddr2;/* Receiver address2 */
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string sEmailServer;/* Email server address */
            public byte byServerType;/* Email server type: 0-SMTP, 1-POP, 2-IMTP¡­*/
            public byte byUseAuthen;/* Email server authentication method: 1-enable, 0-disable */
            public byte byAttachment;/* enable attachment */
            public byte byMailinterval;/* mail interval 0-2s, 1-3s, 2-4s. 3-5s*/
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_COMPRESSIONCFG_NEW
        {
            public uint dwSize;
            public NET_DVR_COMPRESSION_INFO_EX struLowCompression;
            public NET_DVR_COMPRESSION_INFO_EX struEventCompression;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_PTZPOS
        {
            public ushort wAction;
            public ushort wPanPos;
            public ushort wTiltPos;
            public ushort wZoomPos;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_PTZSCOPE
        {
            public ushort wPanPosMin;
            public ushort wPanPosMax;
            public ushort wTiltPosMin;
            public ushort wTiltPosMax;
            public ushort wZoomPosMin;
            public ushort wZoomPosMax;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_RTSPCFG
        {
            public uint dwSize;
            public ushort wPort;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 54, ArraySubType = UnmanagedType.I1)]
            public byte[] byReserve;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_DEVICEINFO
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = SERIALNO_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sSerialNumber;
            public byte byAlarmInPortNum;
            public byte byAlarmOutPortNum;
            public byte byDiskNum;
            public byte byDVRType;
            public byte byChanNum;
            public byte byStartChan;
        }

        //NET_DVR_Login_V30()
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_DEVICEINFO_V30
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = SERIALNO_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sSerialNumber;
            public byte byAlarmInPortNum;
            public byte byAlarmOutPortNum;
            public byte byDiskNum;
            public byte byDVRType;
            public byte byChanNum;
            public byte byStartChan;
            public byte byAudioChanNum;
            public byte byIPChanNum;  
            public byte byZeroChanNum; 
            public byte byMainProto;  	
            public byte bySubProto;    
            public byte bySupport;       

            public byte bySupport1;
            public byte bySupport2;
            public ushort wDevType; 
            public byte bySupport3; 

            public byte byMultiStreamProto;
            public byte byStartDChan;  
            public byte byStartDTalkChan;
            public byte byHighDChanNum;  
            public byte bySupport4;        
            
            public byte byLanguageType;
           
            public byte byVoiceInChanNum;   
            public byte byStartVoiceInChanNo; 
            public byte bySupport5; 
            

            public byte bySupport6;   

            public byte byMirrorChanNum;
            public ushort wStartMirrorChanNo;
            public byte bySupport7;  

            public byte byRes2;
        }

        public enum SDK_NETWORK_ENVIRONMENT
        {
            LOCAL_AREA_NETWORK = 0,
            WIDE_AREA_NETWORK,
        }

        public enum DISPLAY_MODE
        {
            NORMALMODE = 0,
            OVERLAYMODE
        }

        public enum SEND_MODE
        {
            PTOPTCPMODE = 0,
            PTOPUDPMODE,
            MULTIMODE,
            RTPMODE,
            RESERVEDMODE
        }

        public enum CAPTURE_MODE
        {
            BMP_MODE = 0,  
            JPEG_MODE = 1  
        }

        public enum REALSOUND_MODE
        {
            MONOPOLIZE_MODE = 1,
            SHARE_MODE = 2  
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_SDKSTATE
        {
            public uint dwTotalLoginNum;
            public uint dwTotalRealPlayNum;
            public uint dwTotalPlayBackNum;
            public uint dwTotalAlarmChanNum;
            public uint dwTotalFormatNum;
            public uint dwTotalFileSearchNum;
            public uint dwTotalLogSearchNum;
            public uint dwTotalSerialNum;
            public uint dwTotalUpgradeNum;
            public uint dwTotalVoiceComNum;
            public uint dwTotalBroadCastNum;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 10, ArraySubType = UnmanagedType.U4)]
            public uint[] dwRes;
        }

 
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_SDKABL
        {
            public uint dwMaxLoginNum;
            public uint dwMaxRealPlayNum;
            public uint dwMaxPlayBackNum;
            public uint dwMaxAlarmChanNum;
            public uint dwMaxFormatNum;
            public uint dwMaxFileSearchNum;
            public uint dwMaxLogSearchNum;
            public uint dwMaxSerialNum;
            public uint dwMaxUpgradeNum;
            public uint dwMaxVoiceComNum;
            public uint dwMaxBroadCastNum;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 10, ArraySubType = UnmanagedType.U4)]
            public uint[] dwRes;
        }

        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_ALARMER
        {
            public byte byUserIDValid;
            public byte bySerialValid;
            public byte byVersionValid;
            public byte byDeviceNameValid;
            public byte byMacAddrValid; 
            public byte byLinkPortValid;
            public byte byDeviceIPValid;
            public byte bySocketIPValid;
            public int lUserID; /* NET_DVR_Login */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = SERIALNO_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sSerialNumber;
            public uint dwDeviceVersion;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = NAME_LEN)]
            public string sDeviceName;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MACADDR_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byMacAddr;
            public ushort wLinkPort; 
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string sDeviceIP;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string sSocketIP;
            public byte byIpProtocol; 
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 11, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
        }
       
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_DISPLAY_PARA
        {
            public int bToScreen;
            public int bToVideoOut;
            public int nLeft;
            public int nTop;
            public int nWidth;
            public int nHeight;
            public int nReserved;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_CARDINFO
        {
            public int lChannel;
            public int lLinkMode;
            [MarshalAsAttribute(UnmanagedType.LPStr)]
            public string sMultiCastIP;
            public NET_DVR_DISPLAY_PARA struDisplayPara;
        }
      
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_FIND_DATA
        {
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 100)]
            public string sFileName;
            public NET_DVR_TIME struStartTime;
            public NET_DVR_TIME struStopTime;
            public uint dwFileSize;
        }

        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_FINDDATA_V30
        {
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 100)]
            public string sFileName;
            public NET_DVR_TIME struStartTime;
            public NET_DVR_TIME struStopTime;
            public uint dwFileSize;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string sCardNum;
            public byte byLocked;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_FINDDATA_CARD
        {
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 100)]
            public string sFileName;
            public NET_DVR_TIME struStartTime;
            public NET_DVR_TIME struStopTime;
            public uint dwFileSize;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string sCardNum;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_FILECOND
        {
            public int lChannel;
            public uint dwFileType;
            
            public uint dwIsLocked;
            public uint dwUseCardNo;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.I1)]
            public byte[] sCardNumber;
            public NET_DVR_TIME struStartTime;
            public NET_DVR_TIME struStopTime;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_POINT_FRAME
        {
            public int xTop;
            public int yTop;
            public int xBottom;
            public int yBottom;
            public int bCounter;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_DVR_COMPRESSION_AUDIO
        {
            public byte byAudioEncType;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 7, ArraySubType = UnmanagedType.I1)]
            public byte[] byres; 
        }

        public const int MAX_OVERLAP_ITEM_NUM = 50;
        public const int NET_ITS_GET_OVERLAP_CFG = 5072;
        public const int NET_ITS_SET_OVERLAP_CFG = 5073;

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_PLATE_INFO
        {
            public byte byPlateType;
            public byte byColor;
            public byte byBright;
            public byte byLicenseLen;
            public byte byEntireBelieve;
            public byte byRegion; 
            public byte byCountry; 
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 33, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
            public NET_VCA_RECT struPlateRect;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = MAX_LICENSE_LEN)]
            public string sLicense;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_LICENSE_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byBelieve;

            public void Init()
            {
                byRes = new byte[33];
                byBelieve = new byte[MAX_LICENSE_LEN];
            }
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_VEHICLE_INFO
        {
            public uint dwIndex;
            public byte byVehicleType;
            public byte byColorDepth;
            public byte byColor;
            public byte byRadarState;
            public ushort wSpeed;
            public ushort wLength;
            public byte byIllegalType;
            public byte byVehicleLogoRecog; 
            public byte byVehicleSubLogoRecog;
            public byte byVehicleModel; 
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.I1)]
            public byte[] byCustomInfo; 
            public uint wVehicleLogoRecog;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 12, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;

            public void Init()
            {
                byRes = new byte[12];
            }
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_PLATE_RESULT
        {
            public uint dwSize;
            public byte byResultType;
            public byte byChanIndex;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            public uint dwRelativeTime;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.I1)]
            public byte[] byAbsTime;
            public uint dwPicLen;
            public uint dwPicPlateLen;
            public uint dwVideoLen;
            public byte byTrafficLight;
            public byte byPicNum;
            public byte byDriveChan;
            public byte byRes2;
            public uint dwBinPicLen;
            public uint dwCarPicLen;
            public uint dwFarCarPicLen;
            public IntPtr pBuffer3;
            public IntPtr pBuffer4;
            public IntPtr pBuffer5;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes3;
            public NET_DVR_PLATE_INFO struPlateInfo;
            public NET_DVR_VEHICLE_INFO struVehicleInfo;
            public IntPtr pBuffer1;
            public IntPtr pBuffer2;

            public void Init()
            {
                byRes1 = new byte[2];
                byAbsTime = new byte[32];
                byRes3 = new byte[8];
            }
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_VCA_TARGET_INFO
        {
            public uint dwID;
            public NET_VCA_RECT struRect;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_VCA_DEV_INFO
        {
            public NET_DVR_IPADDR struDevIP;
            public ushort wPort;
            public byte byChannel;
            public byte byIvmsChannel;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_VCA_HUMAN_FEATURE
        {
            public byte byAgeGroup;          
            public byte bySex;               
            public byte byEyeGlass;          
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 13, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_VCA_FACESNAP_RESULT
        {
            public uint dwSize; 
            public uint dwRelativeTime; 
            public uint dwAbsTime;  
            public uint dwFacePicID;
            public uint dwFaceScore;   
            public NET_VCA_TARGET_INFO struTargetInfo;
            public NET_VCA_RECT struRect;  
            public NET_VCA_DEV_INFO struDevInfo;
            public uint dwFacePicLen;       
            public uint dwBackgroundPicLen; 
            public byte bySmart;	        
            public byte byAlarmEndMark;
            public byte byRepeatTimes;
            public byte byRes;
            public NET_VCA_HUMAN_FEATURE struFeature;
            public float fStayDuration; 
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sStorageIP; 
            public ushort wStoragePort;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 18, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            public IntPtr pBuffer1;
            public IntPtr pBuffer2; 
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_TIME_V30
        {
            public ushort wYear;
            public byte byMonth;
            public byte byDay;
            public byte byHour;
            public byte byMinute;
            public byte bySecond;
            public byte byRes;
            public ushort wMilliSec;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_ITS_PICTURE_INFO
        {
            public uint dwDataLen;             
            public byte byType;
            public byte byDataType;
            public byte byCloseUpType;  
            public byte byPicRecogMode;
            public uint dwRedLightTime; 
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.I1)]
            public byte[] byAbsTime;
            public NET_VCA_RECT struPlateRect;     
            public NET_VCA_RECT struPlateRecgRect;  
            public IntPtr pBuffer;    
            public uint dwUTCTime;
            public byte byCompatibleAblity;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 7, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_ITS_PLATE_RESULT
        {
            public uint dwSize;
            public uint dwMatchNo;
            public byte byGroupNum;
            public byte byPicNo;
            public byte bySecondCam;    
            public byte byFeaturePicNo; 
            public byte byDriveChan;              
            public byte byVehicleType;   
            public byte byDetSceneID;
            public byte byVehicleAttribute;
            public ushort wIllegalType;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = UnmanagedType.I1)]
            public byte[] byIllegalSubType;   
            public byte byPostPicNo;    
            public byte byChanIndex;   
            public ushort wSpeedLimit;
            public byte byChanIndexEx; 
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 1, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
            public NET_DVR_PLATE_INFO struPlateInfo;  
            public NET_DVR_VEHICLE_INFO struVehicleInfo; 
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 48, ArraySubType = UnmanagedType.I1)]
            public byte[] byMonitoringSiteID;  
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 48, ArraySubType = UnmanagedType.I1)]
            public byte[] byDeviceID;      
            public byte byDir; 
            public byte byDetectType; 
            public byte byRelaLaneDirectionType;
            public byte byCarDirectionType; 
           
            public uint dwCustomIllegalType;

            public IntPtr pIllegalInfoBuf;   
            public byte byIllegalFromatType; 
            public byte byPendant;
            public byte byDataAnalysis;
            public byte byYellowLabelCar;        
            public byte byDangerousVehicles;    

            public byte byPilotSafebelt;
            public byte byCopilotSafebelt;
            public byte byPilotSunVisor;
            public byte byCopilotSunVisor;
            public byte byPilotCall;
           
            public byte byBarrierGateCtrlType;
            public byte byAlarmDataType;
            public NET_DVR_TIME_V30 struSnapFirstPicTime;
            public uint dwIllegalTime;
            public uint dwPicNum;             
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 6, ArraySubType = UnmanagedType.Struct)]
            public NET_ITS_PICTURE_INFO[] struPicInfo;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_ITS_OVERLAPCFG_COND
        {
            public uint dwSize;
            public uint dwChannel;
            public uint dwConfigMode;
            public byte byPicModeType;

            public byte byRelateType;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 14, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        public enum ITS_OVERLAP_ITEM_TYPE
        {
            OVERLAP_ITEM_NULL = 0, 
            OVERLAP_ITEM_SITE,  
            OVERLAP_ITEM_ROADNUM, 
            OVERLAP_ITEM_INSTRUMENTNUM,  
            OVERLAP_ITEM_DIRECTION, 
            OVERLAP_ITEM_DIRECTIONDESC,
            OVERLAP_ITEM_LANENUM,            
            OVERLAP_ITEM_LANEDES,            
            OVERLAP_ITEM_CAPTIME,             
            OVERLAP_ITEM_CAPTIME_MILLSECOND,  
            OVERLAP_ITEM_PLATENUM,           
            OVERLAP_ITEM_CARCOLOR,            
            OVERLAP_ITEM_CARTYPE,             
            OVERLAP_ITEM_CARBRAND,           
            OVERLAP_ITEM_CARSPEED,           
            OVERLAP_ITEM_SPEEDLIMIT,          
            OVERLAP_ITEM_CARLENGTH,           
            OVERLAP_ITEM_ILLEGALNUM,          
            OVERLAP_ITEM_MONITOR_INFO, 
            OVERLAP_ITEM_ILLEGALDES,       
            OVERLAP_ITEM_OVERSPEED_PERCENT,  
            OVERLAP_ITEM_RED_STARTTIME,    
            OVERLAP_ITEM_RED_STOPTIME,  
            OVERLAP_ITEM_RED_DURATION,  
            OVERLAP_ITEM_SECUNITY_CODE,    
            OVERLAP_ITEM_CAP_CODE, 
            OVERLAP_ITEM_SEATBELT,    
            OVERLAP_ITEM_MONITOR_ID, 
            OVERLAP_ITEM_SUN_VISOR,    
            OVERLAP_ITEM_LANE_DIRECTION,  
            OVERLAP_ITEM_LICENSE_PLATE_COLOR, 
            OVERLAP_ITEM_SCENE_NUMBER,  
            OVERLAP_ITEM_SCENE_NAME,  
            OVERLAP_ITEM_YELLOW_SIGN_CAR, 
            OVERLAP_ITEM_DANGEROUS_CAR,  
            OVERLAP_ITEM_CAR_SUBBRAND,  
            OVERLAP_ITEM_CAR_DIRECTION, 
            OVERLAP_ITEM_PENDANT, 
            OVERLAP_ITEM_CALL,  
            OVERLAP_ITEM_CAR_VALIDITY
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_ITS_OVERLAP_SINGLE_ITEM_PARAM
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            public byte byItemType;
            public byte byChangeLineNum;
            public byte bySpaceNum;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 15, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }
   
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_ITS_OVERLAP_ITEM_PARAM
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_OVERLAP_ITEM_NUM, ArraySubType = UnmanagedType.Struct)]
            public NET_ITS_OVERLAP_SINGLE_ITEM_PARAM[] struSingleItem;
            public uint dwLinePercent;
            public uint dwItemsStlye;
            public ushort wStartPosTop;
            public ushort wStartPosLeft;
            public ushort wCharStyle;
            public ushort wCharSize;
            public ushort wCharInterval;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            public uint dwForeClorRGB;
            public uint dwBackClorRGB;
            public byte byColorAdapt;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 31, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_ITS_OVERLAP_INFO_PARAM
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 128, ArraySubType = UnmanagedType.I1)]
            public byte[] bySite;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.I1)]
            public byte[] byRoadNum;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.I1)]
            public byte[] byInstrumentNum;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.I1)]
            public byte[] byDirection;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.I1)]
            public byte[] byDirectionDesc;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.I1)]
            public byte[] byLaneDes;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 44, ArraySubType = UnmanagedType.I1)]
            public byte[] byMonitoringSite1;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.I1)]
            public byte[] byMonitoringSite2;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 64, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_ITS_OVERLAP_CFG
        {
            public uint dwSize;
            public byte byEnable;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            public NET_ITS_OVERLAP_ITEM_PARAM struOverLapItem;
            public NET_ITS_OVERLAP_INFO_PARAM struOverLapInfo;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_SETUPALARM_PARAM
        {
            public uint dwSize;
            public byte byLevel;
            public byte byAlarmInfoType;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            public byte byFaceAlarmDetection;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 10, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_BARRIERGATE_CFG
        {
            public uint dwSize;
            public uint dwChannel;
            public byte byLaneNo;
            public byte byBarrierGateCtrl;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 14, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        public const int MAX_RELAY_NUM = 12;
        public const int MAX_IOIN_NUM = 8;
        public const int MAX_VEHICLE_TYPE_NUM = 8;

        public const int NET_DVR_GET_ENTRANCE_PARAMCFG = 3126; 
        public const int NET_DVR_SET_ENTRANCE_PARAMCFG = 3127; 
               
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_BARRIERGATE_COND
        {
            public byte byLaneNo;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }
        
        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_RELAY_PARAM
        {
            public byte byAccessDevInfo;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_VEHICLE_CONTROL
        {
            public byte byGateOperateType;
            public byte byRes1;
            public ushort wAlarmOperateType; 
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_ENTRANCE_CFG
        {
            public uint dwSize;
            public byte byEnable;
            public byte byBarrierGateCtrlMode;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            public uint dwRelateTriggerMode;
            public uint dwMatchContent;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_RELAY_NUM, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_RELAY_PARAM[] struRelayRelateInfo;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_IOIN_NUM, ArraySubType = UnmanagedType.I1)]
            public byte[] byGateSingleIO;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_VEHICLE_TYPE_NUM, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_VEHICLE_CONTROL[] struVehicleCtrl;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 64, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
        }

        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct NET_DVR_MANUALSNAP
        {
            public byte byOSDEnable;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 23, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_Init();

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_Cleanup();

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetDVRMessage(uint nMessage, IntPtr hWnd);

        public delegate void EXCEPYIONCALLBACK(uint dwType, int lUserID, int lHandle, IntPtr pUser);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetExceptionCallBack_V30(uint nMessage, IntPtr hWnd, EXCEPYIONCALLBACK fExceptionCallBack, IntPtr pUser);
        
        public delegate int MESSCALLBACK(int lCommand, string sDVRIP, string pBuf, uint dwBufLen);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetDVRMessCallBack(MESSCALLBACK fMessCallBack);

        public delegate int MESSCALLBACKEX(int iCommand, int iUserID, string pBuf, uint dwBufLen);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetDVRMessCallBack_EX(MESSCALLBACKEX fMessCallBack_EX);

        public delegate int MESSCALLBACKNEW(int lCommand, string sDVRIP, string pBuf, uint dwBufLen, ushort dwLinkDVRPort);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetDVRMessCallBack_NEW(MESSCALLBACKNEW fMessCallBack_NEW);
        
        public delegate int MESSAGECALLBACK(int lCommand, System.IntPtr sDVRIP, System.IntPtr pBuf, uint dwBufLen, uint dwUser);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetDVRMessageCallBack(MESSAGECALLBACK fMessageCallBack, uint dwUser);

        public delegate void MSGCallBack(int lCommand, ref NET_DVR_ALARMER pAlarmer, IntPtr pAlarmInfo, uint dwBufLen, IntPtr pUser);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetDVRMessageCallBack_V30(MSGCallBack fMessageCallBack, IntPtr pUser);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetDVRMessageCallBack_V31(MSGCallBack fMessageCallBack, IntPtr pUser);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetConnectTime(uint dwWaitTime, uint dwTryTimes);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetReconnect(uint dwInterval, int bEnableRecon);

        [DllImport(@"HCNetSDK.dll")]
        public static extern uint NET_DVR_GetSDKVersion();

        [DllImport(@"HCNetSDK.dll")]
        public static extern uint NET_DVR_GetSDKBuildVersion();

        [DllImport(@"HCNetSDK.dll")]
        public static extern Int32 NET_DVR_IsSupport();

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_StartListen(string sLocalIP, ushort wLocalPort);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_StopListen();

        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_StartListen_V30(String sLocalIP, ushort wLocalPort, MSGCallBack DataCallback, IntPtr pUserData);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_StopListen_V30(Int32 lListenHandle);

        [DllImport(@"HCNetSDK.dll")]
        public static extern Int32 NET_DVR_Login(string sDVRIP, ushort wDVRPort, string sUserName, string sPassword, ref NET_DVR_DEVICEINFO lpDeviceInfo);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_Logout(int iUserID);

        [DllImport(@"HCNetSDK.dll")]
        public static extern uint NET_DVR_GetLastError();

        [DllImport(@"HCNetSDK.dll")]
        public static extern IntPtr NET_DVR_GetErrorMsg(ref int pErrorNo);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetShowMode(uint dwShowType, uint colorKey);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetDVRIPByResolveSvr(string sServerIP, ushort wServerPort, string sDVRName, ushort wDVRNameLen, string sDVRSerialNumber, ushort wDVRSerialLen, IntPtr pGetIP);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetDVRIPByResolveSvr_EX(string sServerIP, ushort wServerPort, ref byte sDVRName, ushort wDVRNameLen, ref byte sDVRSerialNumber, ushort wDVRSerialLen, string sGetIP, ref uint dwPort);

        
        [DllImport(@"HCNetSDK.dll")]
        public static extern Int32 NET_DVR_RealPlay(int iUserID, ref NET_DVR_CLIENTINFO lpClientInfo);

        [DllImport(@"HCNetSDK.dll")]
        public static extern Int32 NET_SDK_RealPlay(int iUserLogID, ref NET_SDK_CLIENTINFO lpDVRClientInfo);
        
        public delegate void REALDATACALLBACK(Int32 lRealHandle, UInt32 dwDataType, ref byte pBuffer, UInt32 dwBufSize, IntPtr pUser);
        [DllImport(@"HCNetSDK.dll")]
      
        public static extern int NET_DVR_RealPlay_V30(int iUserID, ref NET_DVR_CLIENTINFO lpClientInfo, REALDATACALLBACK fRealDataCallBack_V30, IntPtr pUser, UInt32 bBlocked);
              
        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_RealPlay_V40(int iUserID, ref NET_DVR_PREVIEWINFO lpPreviewInfo, REALDATACALLBACK fRealDataCallBack_V30, IntPtr pUser);
          
        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_StopRealPlay(int iRealHandle);

        public delegate void DRAWFUN(int lRealHandle, IntPtr hDc, uint dwUser);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_RigisterDrawFun(int lRealHandle, DRAWFUN fDrawFun, uint dwUser);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetPlayerBufNumber(Int32 lRealHandle, uint dwBufNum);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_ThrowBFrame(Int32 lRealHandle, uint dwNum);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetAudioMode(uint dwMode);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_OpenSound(Int32 lRealHandle);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_CloseSound();

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_OpenSoundShare(Int32 lRealHandle);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_CloseSoundShare(Int32 lRealHandle);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_Volume(Int32 lRealHandle, ushort wVolume);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SaveRealData(Int32 lRealHandle, string sFileName);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_StopSaveRealData(Int32 lRealHandle);

        /*********************************************************
        Function:	REALDATACALLBACK
        Desc:  
        Input:	
        Output:	
        Return:	
        **********************************************************/
        public delegate void SETREALDATACALLBACK(int lRealHandle, uint dwDataType, IntPtr pBuffer, uint dwBufSize, uint dwUser);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetRealDataCallBack(int lRealHandle, SETREALDATACALLBACK fRealDataCallBack, uint dwUser);

        /*********************************************************
        Function:	STDDATACALLBACK
        Desc: 
        Input:	
        Output:	
        Return:	
        **********************************************************/
        public delegate void STDDATACALLBACK(int lRealHandle, uint dwDataType, ref byte pBuffer, uint dwBufSize, uint dwUser);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetStandardDataCallBack(int lRealHandle, STDDATACALLBACK fStdDataCallBack, uint dwUser);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_STDControl(int lUserID, uint dwCommand, ref NET_DVR_STD_CONTROL lpControlParam);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_CapturePicture(Int32 lRealHandle, string sPicFileName);
              
        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_MakeKeyFrame(Int32 lUserID, Int32 lChannel);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_MakeKeyFrameSub(Int32 lUserID, Int32 lChannel);
              
        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetPTZCtrl(Int32 lRealHandle);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetPTZCtrl_Other(Int32 lUserID, int lChannel);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_PTZControl(Int32 lRealHandle, uint dwPTZCommand, uint dwStop);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_PTZControl_Other(Int32 lUserID, Int32 lChannel, uint dwPTZCommand, uint dwStop);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_TransPTZ(Int32 lRealHandle, string pPTZCodeBuf, uint dwBufSize);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_TransPTZ_Other(int lUserID, int lChannel, string pPTZCodeBuf, uint dwBufSize);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_PTZPreset(int lRealHandle, uint dwPTZPresetCmd, uint dwPresetIndex);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_PTZPreset_Other(int lUserID, int lChannel, uint dwPTZPresetCmd, uint dwPresetIndex);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_TransPTZ_EX(int lRealHandle, string pPTZCodeBuf, uint dwBufSize);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_PTZControl_EX(int lRealHandle, uint dwPTZCommand, uint dwStop);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_PTZPreset_EX(int lRealHandle, uint dwPTZPresetCmd, uint dwPresetIndex);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_PTZCruise(int lRealHandle, uint dwPTZCruiseCmd, byte byCruiseRoute, byte byCruisePoint, ushort wInput);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_PTZCruise_Other(int lUserID, int lChannel, uint dwPTZCruiseCmd, byte byCruiseRoute, byte byCruisePoint, ushort wInput);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_PTZCruise_EX(int lRealHandle, uint dwPTZCruiseCmd, byte byCruiseRoute, byte byCruisePoint, ushort wInput);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_PTZTrack(int lRealHandle, uint dwPTZTrackCmd);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_PTZTrack_Other(int lUserID, int lChannel, uint dwPTZTrackCmd);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_PTZTrack_EX(int lRealHandle, uint dwPTZTrackCmd);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_PTZControlWithSpeed(int lRealHandle, uint dwPTZCommand, uint dwStop, uint dwSpeed);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_PTZControlWithSpeed_Other(int lUserID, int lChannel, int dwPTZCommand, int dwStop, int dwSpeed);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_PTZControlWithSpeed_EX(int lRealHandle, uint dwPTZCommand, uint dwStop, uint dwSpeed);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetPTZCruise(int lUserID, int lChannel, int lCruiseRoute, ref NET_DVR_CRUISE_RET lpCruiseRet);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_PTZMltTrack(int lRealHandle, uint dwPTZTrackCmd, uint dwTrackIndex);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_PTZMltTrack_Other(int lUserID, int lChannel, uint dwPTZTrackCmd, uint dwTrackIndex);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_PTZMltTrack_EX(int lRealHandle, uint dwPTZTrackCmd, uint dwTrackIndex);
        
        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_FindFile(int lUserID, int lChannel, uint dwFileType, ref NET_DVR_TIME lpStartTime, ref NET_DVR_TIME lpStopTime);

        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_FindNextFile(int lFindHandle, ref NET_DVR_FIND_DATA lpFindData);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_FindClose(int lFindHandle);

        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_FindNextFile_V30(int lFindHandle, ref NET_DVR_FINDDATA_V30 lpFindData);

        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_FindFile_V30(int lUserID, ref NET_DVR_FILECOND pFindCond);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_FindClose_V30(int lFindHandle);
        
        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_FindNextFile_Card(int lFindHandle, ref NET_DVR_FINDDATA_CARD lpFindData);

        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_FindFile_Card(int lUserID, int lChannel, uint dwFileType, ref NET_DVR_TIME lpStartTime, ref NET_DVR_TIME lpStopTime);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_LockFileByName(int lUserID, string sLockFileName);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_UnlockFileByName(int lUserID, string sUnlockFileName);

        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_PlayBackByName(int lUserID, string sPlayBackFileName, IntPtr hWnd);

        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_PlayBackByTime(int lUserID, int lChannel, ref NET_DVR_TIME lpStartTime, ref NET_DVR_TIME lpStopTime, System.IntPtr hWnd);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_PlayBackControl(int lPlayHandle, uint dwControlCode, uint dwInValue, ref uint LPOutValue);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_StopPlayBack(int lPlayHandle);

        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_PlayBackReverseByName(int lUserID, string sPlayBackFileName, IntPtr hWnd);

        /*********************************************************
        Function:	PLAYDATACALLBACK
        Desc: 
        Input:	
        Output:	
        Return:	
        **********************************************************/
        public delegate void PLAYDATACALLBACK(int lPlayHandle, uint dwDataType, IntPtr pBuffer, uint dwBufSize, uint dwUser);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetPlayDataCallBack(int lPlayHandle, PLAYDATACALLBACK fPlayDataCallBack, uint dwUser);

        public delegate void PLAYDATACALLBACK_V40(int lPlayHandle, uint dwDataType, ref byte pBuffer, uint dwBufSize, IntPtr pUser);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetPlayDataCallBack_V40(int lPlayHandle, PLAYDATACALLBACK_V40 fPlayDataCallBack_V40, IntPtr pUser);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_PlayBackSaveData(int lPlayHandle, string sFileName);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_StopPlayBackSave(int lPlayHandle);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetPlayBackOsdTime(int lPlayHandle, ref NET_DVR_TIME lpOsdTime);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_PlayBackCaptureFile(int lPlayHandle, string sFileName);

        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_GetFileByName(int lUserID, string sDVRFileName, string sSavedFileName);

        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_GetFileByTime(int lUserID, int lChannel, ref NET_DVR_TIME lpStartTime, ref NET_DVR_TIME lpStopTime, string sSavedFileName);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_StopGetFile(int lFileHandle);

        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_GetDownloadPos(int lFileHandle);

        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_GetPlayBackPos(int lPlayHandle);

         [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_Upgrade(int lUserID, string sFileName);

        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_GetUpgradeState(int lUpgradeHandle);

        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_GetUpgradeProgress(int lUpgradeHandle);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_CloseUpgradeHandle(int lUpgradeHandle);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetNetworkEnvironment(uint dwEnvironmentLevel);

        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_AdapterUpgrade(int lUserID, string sFileName);

        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_VcalibUpgrade(int lUserID, int Channel, string sFileName);

        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_Upgrade_V40(int lUserID, uint dwUpgradeType, string sFileName, IntPtr pInbuffer, ushort dwBufferLen);

        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_FormatDisk(int lUserID, int lDiskNumber);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetFormatProgress(int lFormatHandle, ref int pCurrentFormatDisk, ref int pCurrentDiskPos, ref int pFormatStatic);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_CloseFormatHandle(int lFormatHandle);

        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_SetupAlarmChan(int lUserID);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_CloseAlarmChan(int lAlarmHandle);

        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_SetupAlarmChan_V30(int lUserID);

        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_SetupAlarmChan_V41(int lUserID, ref NET_DVR_SETUPALARM_PARAM lpSetupParam);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_CloseAlarmChan_V30(int lAlarmHandle);

        /*********************************************************
        Function:	VOICEDATACALLBACK
        Desc:  
        Input:	
        Output:	
        Return:	
        **********************************************************/
        public delegate void VOICEDATACALLBACK(int lVoiceComHandle, string pRecvDataBuffer, uint dwBufSize, byte byAudioFlag, uint dwUser);

        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_StartVoiceCom(int lUserID, VOICEDATACALLBACK fVoiceDataCallBack, uint dwUser);

        /*********************************************************
        Function:	VOICEDATACALLBACKV30
        Desc:
        Input:	
        Output:	
        Return:	
        **********************************************************/
        public delegate void VOICEDATACALLBACKV30(int lVoiceComHandle, string pRecvDataBuffer, uint dwBufSize, byte byAudioFlag, System.IntPtr pUser);

        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_StartVoiceCom_V30(int lUserID, uint dwVoiceChan, bool bNeedCBNoEncData, VOICEDATACALLBACKV30 fVoiceDataCallBack, IntPtr pUser);


        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetVoiceComClientVolume(int lVoiceComHandle, ushort wVolume);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_StopVoiceCom(int lVoiceComHandle);

        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_StartVoiceCom_MR(int lUserID, VOICEDATACALLBACK fVoiceDataCallBack, uint dwUser);

        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_StartVoiceCom_MR_V30(int lUserID, uint dwVoiceChan, VOICEDATACALLBACKV30 fVoiceDataCallBack, IntPtr pUser);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_VoiceComSendData(int lVoiceComHandle, string pSendBuf, uint dwBufSize);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_ClientAudioStart();

        /*********************************************************
        Function:	VOICEAUDIOSTART
        Desc:
        Input:	
        Output:	
        Return:	
        **********************************************************/
        public delegate void VOICEAUDIOSTART(string pRecvDataBuffer, uint dwBufSize, IntPtr pUser);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_ClientAudioStart_V30(VOICEAUDIOSTART fVoiceAudioStart, IntPtr pUser);


        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_ClientAudioStop();

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_AddDVR(int lUserID);

        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_AddDVR_V30(int lUserID, uint dwVoiceChan);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_DelDVR(int lUserID);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_DelDVR_V30(int lVoiceHandle);

        /*********************************************************
        Function:	SERIALDATACALLBACK
        Desc:
        Input:	
        Output:	
        Return:	
        **********************************************************/
        public delegate void SERIALDATACALLBACK(int lSerialHandle, string pRecvDataBuffer, uint dwBufSize, uint dwUser);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SerialStart(int lUserID, int lSerialPort, SERIALDATACALLBACK fSerialDataCallBack, uint dwUser);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SerialSend(int lSerialHandle, int lChannel, string pSendBuf, uint dwBufSize);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SerialStop(int lSerialHandle);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SendTo232Port(int lUserID, string pSendBuf, uint dwBufSize);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SendToSerialPort(int lUserID, uint dwSerialPort, uint dwSerialIndex, string pSendBuf, uint dwBufSize);

        [DllImport(@"HCNetSDK.dll")]
        public static extern System.IntPtr NET_DVR_InitG722Decoder(int nBitrate);

        [DllImport(@"HCNetSDK.dll")]
        public static extern void NET_DVR_ReleaseG722Decoder(IntPtr pDecHandle);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_DecodeG722Frame(IntPtr pDecHandle, ref byte pInBuffer, ref byte pOutBuffer);

        [DllImport(@"HCNetSDK.dll")]
        public static extern IntPtr NET_DVR_InitG722Encoder();

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_EncodeG722Frame(IntPtr pEncodeHandle, ref byte pInBuffer, ref byte pOutBuffer);

        [DllImport(@"HCNetSDK.dll")]
        public static extern void NET_DVR_ReleaseG722Encoder(IntPtr pEncodeHandle);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_ClickKey(int lUserID, int lKeyIndex);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_StartDVRRecord(int lUserID, int lChannel, int lRecordType);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_StopDVRRecord(int lUserID, int lChannel);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_InitDevice_Card(ref int pDeviceTotalChan);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_ReleaseDevice_Card();

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_InitDDraw_Card(IntPtr hParent, uint colorKey);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_ReleaseDDraw_Card();

        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_RealPlay_Card(int lUserID, ref NET_DVR_CARDINFO lpCardInfo, int lChannelNum);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_ResetPara_Card(int lRealHandle, ref NET_DVR_DISPLAY_PARA lpDisplayPara);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_RefreshSurface_Card();

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_ClearSurface_Card();

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_RestoreSurface_Card();

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_OpenSound_Card(int lRealHandle);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_CloseSound_Card(int lRealHandle);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetVolume_Card(int lRealHandle, ushort wVolume);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_AudioPreview_Card(int lRealHandle, int bEnable);

        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_GetCardLastError_Card();

        [DllImport(@"HCNetSDK.dll")]
        public static extern System.IntPtr NET_DVR_GetChanHandle_Card(int lRealHandle);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_CapturePicture_Card(int lRealHandle, string sPicFileName);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetSerialNum_Card(int lChannelNum, ref uint pDeviceSerialNo);

        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_FindDVRLog(int lUserID, int lSelectMode, uint dwMajorType, uint dwMinorType, ref NET_DVR_TIME lpStartTime, ref NET_DVR_TIME lpStopTime);

        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_FindNextLog(int lLogHandle, ref NET_DVR_LOG lpLogData);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_FindLogClose(int lLogHandle);

        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_FindDVRLog_V30(int lUserID, int lSelectMode, uint dwMajorType, uint dwMinorType, ref NET_DVR_TIME lpStartTime, ref NET_DVR_TIME lpStopTime, bool bOnlySmart);

        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_FindNextLog_V30(int lLogHandle, ref NET_DVR_LOG_V30 lpLogData);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_FindLogClose_V30(int lLogHandle);

        //ATM DVR
        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_FindFileByCard(int lUserID, int lChannel, uint dwFileType, int nFindType, ref byte sCardNumber, ref NET_DVR_TIME lpStartTime, ref NET_DVR_TIME lpStopTime);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_CaptureJPEGPicture(int lUserID, int lChannel, ref NET_DVR_JPEGPARA lpJpegPara, string sPicFileName);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_CaptureJPEGPicture_NEW(int lUserID, int lChannel, ref NET_DVR_JPEGPARA lpJpegPara, string sJpegPicBuffer, uint dwPicSize, ref uint lpSizeReturned);

        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_GetRealPlayerIndex(int lRealHandle);

        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_GetPlayBackPlayerIndex(int lPlayHandle);
        
        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetScaleCFG(int lUserID, uint dwScale);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetScaleCFG(int lUserID, ref uint lpOutScale);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetScaleCFG_V30(int lUserID, ref NET_DVR_SCALECFG pScalecfg);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetScaleCFG_V30(int lUserID, ref NET_DVR_SCALECFG pScalecfg);
        
        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetATMPortCFG(int lUserID, ushort wATMPort);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetATMPortCFG(int lUserID, ref ushort LPOutATMPort);
               
        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_InitDDrawDevice();

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_ReleaseDDrawDevice();

        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_GetDDrawDeviceTotalNums();

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetDDrawDevice(int lPlayPort, uint nDeviceNum);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_PTZSelZoomIn(int lRealHandle, ref NET_DVR_POINT_FRAME pStruPointFrame);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_PTZSelZoomIn_EX(int lUserID, int lChannel, ref NET_DVR_POINT_FRAME pStruPointFrame);
                
        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_StartDecode(int lUserID, int lChannel, ref NET_DVR_DECODERINFO lpDecoderinfo);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_StopDecode(int lUserID, int lChannel);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetDecoderState(int lUserID, int lChannel, ref NET_DVR_DECODERSTATE lpDecoderState);
        
        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetDecInfo(int lUserID, int lChannel, ref NET_DVR_DECCFG lpDecoderinfo);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetDecInfo(int lUserID, int lChannel, ref NET_DVR_DECCFG lpDecoderinfo);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetDecTransPort(int lUserID, ref NET_DVR_PORTCFG lpTransPort);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetDecTransPort(int lUserID, ref NET_DVR_PORTCFG lpTransPort);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_DecPlayBackCtrl(int lUserID, int lChannel, uint dwControlCode, uint dwInValue, ref uint LPOutValue, ref NET_DVR_PLAYREMOTEFILE lpRemoteFileInfo);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_StartDecSpecialCon(int lUserID, int lChannel, ref NET_DVR_DECCHANINFO lpDecChanInfo);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_StopDecSpecialCon(int lUserID, int lChannel, ref NET_DVR_DECCHANINFO lpDecChanInfo);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_DecCtrlDec(int lUserID, int lChannel, uint dwControlCode);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_DecCtrlScreen(int lUserID, int lChannel, uint dwControl);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetDecCurLinkStatus(int lUserID, int lChannel, ref NET_DVR_DECSTATUS lpDecStatus);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_MatrixStartDynamic(int lUserID, uint dwDecChanNum, ref NET_DVR_MATRIX_DYNAMIC_DEC lpDynamicInfo);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_MatrixStopDynamic(int lUserID, uint dwDecChanNum);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_MatrixGetDecChanInfo(int lUserID, uint dwDecChanNum, ref NET_DVR_MATRIX_DEC_CHAN_INFO lpInter);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_MatrixSetLoopDecChanInfo(int lUserID, uint dwDecChanNum, ref NET_DVR_MATRIX_LOOP_DECINFO lpInter);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_MatrixGetLoopDecChanInfo(int lUserID, uint dwDecChanNum, ref NET_DVR_MATRIX_LOOP_DECINFO lpInter);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_MatrixSetLoopDecChanEnable(int lUserID, uint dwDecChanNum, uint dwEnable);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_MatrixGetLoopDecChanEnable(int lUserID, uint dwDecChanNum, ref uint lpdwEnable);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_MatrixGetLoopDecEnable(int lUserID, ref uint lpdwEnable);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_MatrixSetDecChanEnable(int lUserID, uint dwDecChanNum, uint dwEnable);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_MatrixGetDecChanEnable(int lUserID, uint dwDecChanNum, ref uint lpdwEnable);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_MatrixGetDecChanStatus(int lUserID, uint dwDecChanNum, ref NET_DVR_MATRIX_DEC_CHAN_STATUS lpInter);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_MatrixSetTranInfo(int lUserID, ref NET_DVR_MATRIX_TRAN_CHAN_CONFIG lpTranInfo);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_MatrixGetTranInfo(int lUserID, ref NET_DVR_MATRIX_TRAN_CHAN_CONFIG lpTranInfo);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_MatrixSetRemotePlay(int lUserID, uint dwDecChanNum, ref NET_DVR_MATRIX_DEC_REMOTE_PLAY lpInter);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_MatrixSetRemotePlayControl(int lUserID, uint dwDecChanNum, uint dwControlCode, uint dwInValue, ref uint LPOutValue);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_MatrixGetRemotePlayStatus(int lUserID, uint dwDecChanNum, ref NET_DVR_MATRIX_DEC_REMOTE_PLAY_STATUS lpOuter);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_MatrixStartDynamic_V30(int lUserID, uint dwDecChanNum, ref NET_DVR_PU_STREAM_CFG lpDynamicInfo);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_MatrixSetLoopDecChanInfo_V30(int lUserID, uint dwDecChanNum, ref tagMATRIX_LOOP_DECINFO_V30 lpInter);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_MatrixGetLoopDecChanInfo_V30(int lUserID, uint dwDecChanNum, ref tagMATRIX_LOOP_DECINFO_V30 lpInter);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_MatrixGetDecChanInfo_V30(int lUserID, uint dwDecChanNum, ref tagDEC_MATRIX_CHAN_INFO lpInter);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_MatrixSetTranInfo_V30(int lUserID, ref tagMATRIX_TRAN_CHAN_CONFIG lpTranInfo);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_MatrixGetTranInfo_V30(int lUserID, ref tagMATRIX_TRAN_CHAN_CONFIG lpTranInfo);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_MatrixGetDisplayCfg(int lUserID, uint dwDispChanNum, ref tagNET_DVR_VGA_DISP_CHAN_CFG lpDisplayCfg);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_MatrixSetDisplayCfg(int lUserID, uint dwDispChanNum, ref tagNET_DVR_VGA_DISP_CHAN_CFG lpDisplayCfg);


        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_MatrixStartPassiveDecode(int lUserID, uint dwDecChanNum, ref tagNET_MATRIX_PASSIVEMODE lpPassiveMode);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_MatrixSendData(int lPassiveHandle, System.IntPtr pSendBuf, uint dwBufSize);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_MatrixStopPassiveDecode(int lPassiveHandle);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_UploadLogo(int lUserID, uint dwDispChanNum, ref tagNET_DVR_DISP_LOGOCFG lpDispLogoCfg, System.IntPtr sLogoBuffer);

        public const int NET_DVR_SHOWLOGO = 1;
        public const int NET_DVR_HIDELOGO = 2;

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_LogoSwitch(int lUserID, uint dwDecChan, uint dwLogoSwitch);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_MatrixGetDeviceStatus(int lUserID, ref tagNET_DVR__DECODER_WORK_STATUS lpDecoderCfg);


        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_RigisterPlayBackDrawFun(int lRealHandle, DRAWFUN fDrawFun, uint dwUser);


        public const int DISP_CMD_ENLARGE_WINDOW = 1;
        public const int DISP_CMD_RENEW_WINDOW = 2;

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_MatrixDiaplayControl(int lUserID, uint dwDispChanNum, uint dwDispChanCmd, uint dwCmdParam);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_RefreshPlay(int lPlayHandle);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_RestoreConfig(int lUserID);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SaveConfig(int lUserID);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_RebootDVR(int lUserID);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_ShutDownDVR(int lUserID);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetDVRConfig(int lUserID, uint dwCommand, int lChannel, IntPtr lpOutBuffer, uint dwOutBufferSize, ref uint lpBytesReturned);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetDVRConfig(int lUserID, uint dwCommand, int lChannel, System.IntPtr lpInBuffer, uint dwInBufferSize);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetAlarmDeviceUser(int lUserID, int lUserIndex, ref NET_DVR_ALARM_DEVICE_USER lpDeviceUser);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetAlarmDeviceUser(int lUserID, int lUserIndex, ref NET_DVR_ALARM_DEVICE_USER lpDeviceUser);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetDeviceConfig(int lUserID, uint dwCommand, uint dwCount, IntPtr lpInBuffer, uint dwInBufferSize, IntPtr lpStatusList, IntPtr lpOutBuffer, uint dwOutBufferSize);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetDeviceConfig(int lUserID, uint dwCommand, uint dwCount, IntPtr lpInBuffer, uint dwInBufferSize, IntPtr lpStatusList, IntPtr lpInParamBuffer, uint dwInParamBufferSize);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_RemoteControl(int lUserID, uint dwCommand, IntPtr lpInBuffer, uint dwInBufferSize);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetDVRWorkState_V30(int lUserID, IntPtr pWorkState);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetDVRWorkState(int lUserID, ref NET_DVR_WORKSTATE lpWorkState);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetVideoEffect(int lUserID, int lChannel, uint dwBrightValue, uint dwContrastValue, uint dwSaturationValue, uint dwHueValue);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetVideoEffect(int lUserID, int lChannel, ref uint pBrightValue, ref uint pContrastValue, ref uint pSaturationValue, ref uint pHueValue);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_ClientGetframeformat(int lUserID, ref NET_DVR_FRAMEFORMAT lpFrameFormat);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_ClientSetframeformat(int lUserID, ref NET_DVR_FRAMEFORMAT lpFrameFormat);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_ClientGetframeformat_V30(int lUserID, ref NET_DVR_FRAMEFORMAT_V30 lpFrameFormat);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_ClientSetframeformat_V30(int lUserID, ref NET_DVR_FRAMEFORMAT_V30 lpFrameFormat);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_Getframeformat_V31(int lUserID, ref tagNET_DVR_FRAMEFORMAT_V31 lpFrameFormat);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_Setframeformat_V31(int lUserID, ref tagNET_DVR_FRAMEFORMAT_V31 lpFrameFormat);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetAtmProtocol(int lUserID, ref tagNET_DVR_ATM_PROTOCOL lpAtmProtocol);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetAlarmOut_V30(int lUserID, IntPtr lpAlarmOutState);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetAlarmOut(int lUserID, ref NET_DVR_ALARMOUTSTATUS lpAlarmOutState);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetAlarmOut(int lUserID, int lAlarmOutPort, int lAlarmOutStatic);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_ClientSetVideoEffect(int lRealHandle, uint dwBrightValue, uint dwContrastValue, uint dwSaturationValue, uint dwHueValue);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_ClientGetVideoEffect(int lRealHandle, ref uint pBrightValue, ref uint pContrastValue, ref uint pSaturationValue, ref uint pHueValue);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetConfigFile(int lUserID, string sFileName);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetConfigFile(int lUserID, string sFileName);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetConfigFile_V30(int lUserID, string sOutBuffer, uint dwOutSize, ref uint pReturnSize);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetConfigFile_EX(int lUserID, string sOutBuffer, uint dwOutSize);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetConfigFile_EX(int lUserID, string sInBuffer, uint dwInSize);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetLogToFile(int bLogEnable, string strLogDir, bool bAutoDel);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetSDKState(ref NET_DVR_SDKSTATE pSDKState);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetSDKAbility(ref NET_DVR_SDKABL pSDKAbl);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetPTZProtocol(int lUserID, ref NET_DVR_PTZCFG pPtzcfg);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_LockPanel(int lUserID);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_UnLockPanel(int lUserID);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetRtspConfig(int lUserID, uint dwCommand, ref NET_DVR_RTSPCFG lpInBuffer, uint dwInBufferSize);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetRtspConfig(int lUserID, uint dwCommand, ref NET_DVR_RTSPCFG lpOutBuffer, uint dwOutBufferSize);


        //SDK_V222        
        public const int DS6001_HF_B = 60;
        public const int DS6001_HF_P = 61;
        public const int DS6002_HF_B = 62;
        public const int DS6101_HF_B = 63;
        public const int IDS52XX = 64;
        public const int DS9000_IVS = 65;
        public const int DS8004_AHL_A = 66;
        public const int DS6101_HF_P = 67;

        public const int VCA_DEV_ABILITY = 256;
        public const int VCA_CHAN_ABILITY = 272;
        public const int MATRIXDECODER_ABILITY = 512;
        
        //NET_VCA_PLATE_CFG£©
        public const int NET_DVR_SET_PLATECFG = 150;
        public const int NET_DVR_GET_PLATECFG = 151;
        //NET_VCA_RULECFG£©
        public const int NET_DVR_SET_RULECFG = 152;
        public const int NET_DVR_GET_RULECFG = 153;

        //NET_DVR_LF_CFG
        public const int NET_DVR_SET_LF_CFG = 160;
        public const int NET_DVR_GET_LF_CFG = 161;

        public const int NET_DVR_SET_IVMS_STREAMCFG = 162;
        public const int NET_DVR_GET_IVMS_STREAMCFG = 163;

        public const int NET_DVR_SET_VCA_CTRLCFG = 164;
        public const int NET_DVR_GET_VCA_CTRLCFG = 165;

        //NET_VCA_MASK_REGION_LIST
        public const int NET_DVR_SET_VCA_MASK_REGION = 166;
        public const int NET_DVR_GET_VCA_MASK_REGION = 167;

        //ATM NET_VCA_ENTER_REGION
        public const int NET_DVR_SET_VCA_ENTER_REGION = 168;
        public const int NET_DVR_GET_VCA_ENTER_REGION = 169;

        //NET_VCA_LINE_SEGMENT_LIST
        public const int NET_DVR_SET_VCA_LINE_SEGMENT = 170;
        public const int NET_DVR_GET_VCA_LINE_SEGMENT = 171;

        //NET_IVMS_MASK_REGION_LIST
        public const int NET_DVR_SET_IVMS_MASK_REGION = 172;
        public const int NET_DVR_GET_IVMS_MASK_REGION = 173;
        //NET_IVMS_ENTER_REGION
        public const int NET_DVR_SET_IVMS_ENTER_REGION = 174;
        public const int NET_DVR_GET_IVMS_ENTER_REGION = 175;

        public const int NET_DVR_SET_IVMS_BEHAVIORCFG = 176;
        public const int NET_DVR_GET_IVMS_BEHAVIORCFG = 177;

        // IVMS
        public const int NET_DVR_IVMS_SET_SEARCHCFG = 178;
        public const int NET_DVR_IVMS_GET_SEARCHCFG = 179;

        //NET_VCA_PLATE_RESULT
        public const int COMM_ALARM_PLATE = 4353;
        //NET_VCA_RULE_ALARM
        public const int COMM_ALARM_RULE = 4354;

        public const int VCA_MAX_POLYGON_POINT_NUM = 10;
        public const int MAX_RULE_NUM = 8;
        public const int MAX_TARGET_NUM = 30;
        public const int MAX_CALIB_PT = 6;
        public const int MIN_CALIB_PT = 4;
        public const int MAX_TIMESEGMENT_2 = 2;
        public const int MAX_LICENSE_LEN = 16;
        public const int MAX_PLATE_NUM = 3;
        public const int MAX_MASK_REGION_NUM = 4;
        public const int MAX_SEGMENT_NUM = 6;
        public const int MIN_SEGMENT_NUM = 3;

        public const int MAX_VCA_CHAN = 16;
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_CTRLINFO
        {
            public byte byVCAEnable;
            public byte byVCAType;
            public byte byStreamWithVCA;
            public byte byMode;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes; 
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_CTRLCFG
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_VCA_CHAN, ArraySubType = UnmanagedType.Struct)]
            public tagNET_VCA_CTRLINFO[] struCtrlInfo;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_DEV_ABILITY
        {
            public uint dwSize;
            public byte byVCAChanNum;
            public byte byPlateChanNum;
            public byte byBBaseChanNum;
            public byte byBAdvanceChanNum;
            public byte byBFullChanNum;
            public byte byATMChanNum;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 34, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        public enum VCA_ABILITY_TYPE
        {
            TRAVERSE_PLANE_ABILITY = 1,
            ENTER_AREA_ABILITY = 2,
            EXIT_AREA_ABILITY = 4,
            INTRUSION_ABILITY = 8,
            LOITER_ABILITY = 16,
            LEFT_TAKE_ABILITY = 32,
            PARKING_ABILITY = 64,
            RUN_ABILITY = 128,
            HIGH_DENSITY_ABILITY = 256,
            LF_TRACK_ABILITY = 512,
            STICK_UP_ABILITY = 1073741824,
            INSTALL_SCANNER_ABILITY = -2147483648,
        }

        public enum VCA_CHAN_ABILITY_TYPE
        {
            VCA_BEHAVIOR_BASE = 1,
            VCA_BEHAVIOR_ADVANCE = 2,
            VCA_BEHAVIOR_FULL = 3,
            VCA_PLATE = 4,
            VCA_ATM = 5,
        }

        public enum VCA_CHAN_MODE_TYPE
        {
            VCA_ATM_PANEL = 0,
            VCA_ATM_SURROUND = 1,
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_CHAN_IN_PARAM
        {
            public byte byVCAType;
            public byte byMode;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_BEHAVIOR_ABILITY
        {
            public uint dwSize;
            public uint dwAbilityType;
            public byte byMaxRuleNum;
            public byte byMaxTargetNum;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 10, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        /*********************************************************
        Function:	NET_DVR_GetDeviceAbility
        Desc:  
        Input:	
        Output:	
        Return:	TRUE
        **********************************************************/
        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetDeviceAbility(int lUserID, uint dwAbilityType, IntPtr pInBuf, uint dwInLength, IntPtr pOutBuf, uint dwOutLength);

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_POINT
        {
            public float fX;
            public float fY;
        }

        public const int DRAG_PTZ = 51;
        public const int NET_DVR_FISHEYE_CFG = 3244;

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_DRAG_POS_PARAM
        {
            public uint dwChannel; 
            public uint dwPtzChannel;  
            public tagNET_VCA_POINT struToPoint; 
            public tagNET_VCA_POINT struOriPoint;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 56, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }
        public enum FISHEYE_STREAM_OUTPUT_MODE
        {
            FISHEYE_STREAM_MODE_FISHEYE = 1,
            FISHEYE_STREAM_MODE_PTZ = 2,
            FISHEYE_STREAM_MODE_PANORAMA = 3
        }

        public enum CALLBACK_TYPE_DATA_ENUM
        {
            ENUM_FISHEYE_STREAM_STATUS = 1, 
            ENUM_FISHEYE_PTZPOS = 2, 
            ENUM_FISHEYE_REALTIME_OUTPUT = 3
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_FISHEYE_STREAM_STATUS
        {
            public uint dwSize;
            public byte byStreamMode; 
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 63, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_CALLBACK_TYPE_DATA
        {
            public uint dwChannel;      
            public uint dwDataType;    
            public uint dwDataLen;  
            public IntPtr pData; 
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 64, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_VCA_RECT
        {
            public float fX;
            public float fY;
            public float fWidth;
            public float fHeight;
        }

        public enum VCA_EVENT_TYPE
        {
            VCA_TRAVERSE_PLANE = 1,
            VCA_ENTER_AREA = 2,
            VCA_EXIT_AREA = 4,
            VCA_INTRUSION = 8,
            VCA_LOITER = 16,
            VCA_LEFT_TAKE = 32,
            VCA_PARKING = 64,
            VCA_RUN = 128,
            VCA_HIGH_DENSITY = 256,
            VCA_STICK_UP = 1073741824,
            VCA_INSTALL_SCANNER = -2147483648,
        }

        public enum VCA_CROSS_DIRECTION
        {
            VCA_BOTH_DIRECTION,
            VCA_LEFT_GO_RIGHT, 
            VCA_RIGHT_GO_LEFT,
        }
        
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_LINE
        {
            public tagNET_VCA_POINT struStart; 
            public tagNET_VCA_POINT struEnd;

            //             public void init()
            //             {
            //                 struStart = new tagNET_VCA_POINT();
            //                 struEnd = new tagNET_VCA_POINT();
            //             }
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_POLYGON
        {
            /// DWORD->unsigned int
            public uint dwPointNum;

            /// NET_VCA_POINT[10]
            //             [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 10, ArraySubType = System.Runtime.InteropServices.UnmanagedType.Struct)]
            //             public tagNET_VCA_POINT[] struPos;

        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_TRAVERSE_PLANE
        {
            public tagNET_VCA_LINE struPlaneBottom;
            public VCA_CROSS_DIRECTION dwCrossDirection;
            public byte byRes1;
            public byte byPlaneHeight;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 38, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;

            //             public void init()
            //             {
            //                 struPlaneBottom = new tagNET_VCA_LINE();
            //                 struPlaneBottom.init();
            //                 byRes2 = new byte[38];
            //             }
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_AREA
        {
            public tagNET_VCA_POLYGON struRegion;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_INTRUSION
        {
            public tagNET_VCA_POLYGON struRegion;
            public ushort wDuration;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 6, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_PARAM_LOITER
        {
            public tagNET_VCA_POLYGON struRegion;
            public ushort wDuration;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 6, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_TAKE_LEFT
        {
            public tagNET_VCA_POLYGON struRegion;
            public ushort wDuration;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 6, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_PARKING
        {
            public tagNET_VCA_POLYGON struRegion;
            public ushort wDuration;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 6, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_RUN
        {
            public tagNET_VCA_POLYGON struRegion;
            public float fRunDistance;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_HIGH_DENSITY
        {
            public tagNET_VCA_POLYGON struRegion;
            public float fDensity;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_STICK_UP
        {
            public tagNET_VCA_POLYGON struRegion;
            public ushort wDuration;
            public byte bySensitivity;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 5, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_SCANNER
        {
            public tagNET_VCA_POLYGON struRegion;
            public ushort wDuration;
            public byte bySensitivity;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 5, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        [StructLayoutAttribute(LayoutKind.Explicit)]
        public struct tagNET_VCA_EVENT_UNION
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 23, ArraySubType = UnmanagedType.U4)]
            [FieldOffsetAttribute(0)]
            public uint[] uLen;
            [FieldOffsetAttribute(0)]
            public tagNET_VCA_TRAVERSE_PLANE struTraversePlane;
            [FieldOffsetAttribute(0)]
            public tagNET_VCA_AREA struArea;
            [FieldOffsetAttribute(0)]
            public tagNET_VCA_INTRUSION struIntrusion;
            [FieldOffsetAttribute(0)]
            public tagNET_VCA_PARAM_LOITER struLoiter;
            [FieldOffsetAttribute(0)]
            public tagNET_VCA_TAKE_LEFT struTakeTeft;
            [FieldOffsetAttribute(0)]
            public tagNET_VCA_PARKING struParking;
            [FieldOffsetAttribute(0)]
            public tagNET_VCA_RUN struRun;
            [FieldOffsetAttribute(0)]
            public tagNET_VCA_HIGH_DENSITY struHighDensity;  
            [FieldOffsetAttribute(0)]
            public tagNET_VCA_STICK_UP struStickUp;
            [FieldOffsetAttribute(0)]
            public tagNET_VCA_SCANNER struScanner;

            //             public void init()
            //             {
            //                 uLen = new uint[23];
            //                 struTraversePlane = new tagNET_VCA_TRAVERSE_PLANE();
            //                 struTraversePlane.init();
            //                 struArea = new tagNET_VCA_AREA();
            //                 struArea.init();
            //                 struIntrusion = new tagNET_VCA_INTRUSION();
            //                 struIntrusion.init();
            //                 struLoiter = new tagNET_VCA_PARAM_LOITER();
            //                 struLoiter.init();
            //                 struTakeTeft = new tagNET_VCA_TAKE_LEFT();
            //                 struTakeTeft.init();
            //                 struParking = new tagNET_VCA_PARKING();
            //                 struParking.init();
            //                 struRun = new tagNET_VCA_RUN();
            //                 struRun.init();
            //                 struHighDensity = new tagNET_VCA_HIGH_DENSITY();
            //                 struHighDensity.init();
            //                 struStickUp = new tagNET_VCA_STICK_UP();
            //                 struStickUp.init();
            //                 struScanner = new tagNET_VCA_SCANNER();
            //                 struScanner.init();
            //             }
        }

        public enum SIZE_FILTER_MODE
        {
            IMAGE_PIX_MODE,
            REAL_WORLD_MODE,
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_SIZE_FILTER
        {
            public byte byActive;
            public byte byMode;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
            public NET_VCA_RECT struMiniRect;
            public NET_VCA_RECT struMaxRect;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_ONE_RULE
        {
            public byte byActive;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 7, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byRuleName;
            public VCA_EVENT_TYPE dwEventType;
            public tagNET_VCA_EVENT_UNION uEventParam;
            public tagNET_VCA_SIZE_FILTER struSizeFilter;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DAYS * MAX_TIMESEGMENT_2, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_SCHEDTIME[] struAlarmTime;
            public NET_DVR_HANDLEEXCEPTION_V30 struHandleType;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byRelRecordChan;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_RULECFG
        {
            public uint dwSize;
            public byte byPicProType;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
            public NET_DVR_JPEGPARA struPictureParam;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_RULE_NUM, ArraySubType = UnmanagedType.Struct)]
            public tagNET_VCA_ONE_RULE[] struRule;
        }
      
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_TARGET_INFO
        {
            public uint dwID;
            public NET_VCA_RECT struRect;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_RULE_INFO
        {
            public byte byRuleID;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byRuleName;
            public VCA_EVENT_TYPE dwEventType;
            public tagNET_VCA_EVENT_UNION uEventParam;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_DEV_INFO
        {
            public NET_DVR_IPADDR struDevIP;
            public ushort wPort;
            public byte byChannel;
            public byte byRes;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_RULE_ALARM
        {
            public uint dwSize;
            public uint dwRelativeTime;
            public uint dwAbsTime;
            public tagNET_VCA_RULE_INFO struRuleInfo;
            public tagNET_VCA_TARGET_INFO struTargetInfo;
            public tagNET_VCA_DEV_INFO struDevInfo;
            public uint dwPicDataLen;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.U4)]
            public uint[] dwRes;
            public IntPtr pImage;
        }

        public enum IVS_PARAM_KEY
        {
            OBJECT_DETECT_SENSITIVE = 1,
            BACKGROUND_UPDATE_RATE = 2,
            SCENE_CHANGE_RATIO = 3,
            SUPPRESS_LAMP = 4,
            MIN_OBJECT_SIZE = 5,
            OBJECT_GENERATE_RATE = 6,
            MISSING_OBJECT_HOLD = 7,
            MAX_MISSING_DISTANCE = 8,
            OBJECT_MERGE_SPEED = 9,
            REPEATED_MOTION_SUPPRESS = 10,
            ILLUMINATION_CHANGE = 11,
            TRACK_OUTPUT_MODE = 12,
            ENTER_CHANGE_HOLD = 13,
            RESUME_DEFAULT_PARAM = 255,
        }

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetBehaviorParamKey(int lUserID, int lChannel, uint dwParameterKey, int nValue);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetBehaviorParamKey(int lUserID, int lChannel, uint dwParameterKey, ref int pValue);

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_DRAW_MODE
        {
            public uint dwSize;
            public byte byDspAddTarget;
            public byte byDspAddRule;
            public byte byDspPicAddTarget;
            public byte byDspPicAddRule;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetVCADrawMode(int lUserID, int lChannel, ref tagNET_VCA_DRAW_MODE lpDrawMode);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetVCADrawMode(int lUserID, int lChannel, ref tagNET_VCA_DRAW_MODE lpDrawMode);

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_DVR_CB_POINT
        {
            public tagNET_VCA_POINT struPoint;
            public NET_DVR_PTZPOS struPtzPos;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_DVR_LF_CALIBRATION_PARAM
        {
            public byte byPointNum;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CALIB_PT, ArraySubType = UnmanagedType.Struct)]
            public tagNET_DVR_CB_POINT[] struCBPoint;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_DVR_LF_CFG
        {
            public uint dwSize;	
            public byte byEnable;
            public byte byFollowChan;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
            public tagNET_DVR_LF_CALIBRATION_PARAM struCalParam;
        }

        public enum TRACK_MODE
        {
            MANUAL_CTRL = 0,
            ALARM_TRACK,
            TARGET_TRACK,
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_DVR_LF_MANUAL_CTRL_INFO
        {
            public tagNET_VCA_POINT struCtrlPoint;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }
        
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_DVR_LF_TRACK_TARGET_INFO
        {
            public uint dwTargetID;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_DVR_LF_TRACK_MODE
        {
            public uint dwSize;
            public byte byTrackMode;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
            [StructLayoutAttribute(LayoutKind.Explicit)]
            public struct uModeParam
            {
                [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.U4)]
                [FieldOffsetAttribute(0)]
                public uint[] dwULen;
                [FieldOffsetAttribute(0)]
                public tagNET_DVR_LF_MANUAL_CTRL_INFO struManualCtrl;
                [FieldOffsetAttribute(0)]
                public tagNET_DVR_LF_TRACK_TARGET_INFO struTargetTrack;
            }
        }
        
        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetLFTrackMode(int lUserID, int lChannel, ref tagNET_DVR_LF_TRACK_MODE lpTrackMode);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetLFTrackMode(int lUserID, int lChannel, ref tagNET_DVR_LF_TRACK_MODE lpTrackMode);

        public enum VCA_RECOGNIZE_SCENE
        {
            VCA_LOW_SPEED_SCENE = 0,
            VCA_HIGH_SPEED_SCENE = 1,
            VCA_MOBILE_CAMERA_SCENE = 2,
        }

        public enum VCA_RECOGNIZE_RESULT
        {
            VCA_RECOGNIZE_FAILURE = 0,
            VCA_IMAGE_RECOGNIZE_SUCCESS,
            VCA_VIDEO_RECOGNIZE_SUCCESS_OF_BEST_LICENSE,
            VCA_VIDEO_RECOGNIZE_SUCCESS_OF_NEW_LICENSE,
            VCA_VIDEO_RECOGNIZE_FINISH_OF_CUR_LICENSE,
        }

        public enum VCA_PLATE_COLOR
        {
            VCA_BLUE_PLATE = 0,
            VCA_YELLOW_PLATE,
            VCA_WHITE_PLATE,
            VCA_BLACK_PLATE,
            VCA_GREEN_PLATE,
            VCA_BKAIR_PLATE,
            VCA_OTHER = 0xff
        }

        public enum VCA_PLATE_TYPE
        {
            VCA_STANDARD92_PLATE = 0,
            VCA_STANDARD02_PLATE,
            VCA_WJPOLICE_PLATE,
            VCA_JINGCHE_PLATE,
            STANDARD92_BACK_PLATE,
            VCA_SHIGUAN_PLATE,
            VCA_NONGYONG_PLATE,
            VCA_MOTO_PLATE
        }
       
        public enum VCA_TRIGGER_TYPE
        {
            INTER_TRIGGER = 0,
            EXTER_TRIGGER = 1,
        }

        public const int MAX_CHINESE_CHAR_NUM = 64;
        
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_PLATE_PARAM
        {
            public NET_VCA_RECT struSearchRect;
            public NET_VCA_RECT struInvalidateRect;
            public ushort wMinPlateWidth;
            public ushort wTriggerDuration;
            public byte byTriggerType;
            public byte bySensitivity;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 64, ArraySubType = UnmanagedType.I1)]
            public byte[] byCharPriority;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_PLATEINFO
        {
            public VCA_RECOGNIZE_SCENE eRecogniseScene;
            public tagNET_VCA_PLATE_PARAM struModifyParam;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_PLATECFG
        {
            public uint dwSize;
            public byte byPicProType;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
            public NET_DVR_JPEGPARA struPictureParam;
            public tagNET_VCA_PLATEINFO struPlateInfo;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DAYS * MAX_TIMESEGMENT_2, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_SCHEDTIME[] struAlarmTime;
            public NET_DVR_HANDLEEXCEPTION_V30 struHandleType;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byRelRecordChan;
        }

        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct tagNET_VCA_PLATE_INFO
        {
            public VCA_RECOGNIZE_RESULT eResultFlag;
            public VCA_PLATE_TYPE ePlateType;
            public VCA_PLATE_COLOR ePlateColor;
            public NET_VCA_RECT struPlateRect;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.U4)]
            public uint[] dwRes; 
            public uint dwLicenseLen;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = MAX_LICENSE_LEN)]
            public string sLicense; 
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = MAX_LICENSE_LEN)]
            public string sBelieve;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_PLATE_RESULT
        {
            public uint dwSize;
            public uint dwRelativeTime;
            public uint dwAbsTime;
            public byte byPlateNum;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_PLATE_NUM, ArraySubType = UnmanagedType.Struct)]
            public tagNET_VCA_PLATE_INFO[] struPlateInfo;
            public uint dwPicDataLen;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.U4)]
            public uint[] dwRes2;
            public System.IntPtr pImage;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_IVMS_ONE_RULE_
        {
            public byte byActive;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 7, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byRuleName;
            public VCA_EVENT_TYPE dwEventType;
            public tagNET_VCA_EVENT_UNION uEventParam;
            public tagNET_VCA_SIZE_FILTER struSizeFilter;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 68, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_IVMS_RULECFG
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_RULE_NUM, ArraySubType = UnmanagedType.Struct)]
            public tagNET_IVMS_ONE_RULE_[] struRule;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_IVMS_BEHAVIORCFG
        {
            public uint dwSize;
            public byte byPicProType;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
            public NET_DVR_JPEGPARA struPicParam;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DAYS * MAX_TIMESEGMENT, ArraySubType = UnmanagedType.Struct)]
            public tagNET_IVMS_RULECFG[] struRuleCfg;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_IVMS_DEVSCHED
        {
            public NET_DVR_SCHEDTIME struTime;
            public NET_DVR_PU_STREAM_CFG struPUStream;
        }
        
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_IVMS_STREAMCFG
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DAYS * MAX_TIMESEGMENT, ArraySubType = UnmanagedType.Struct)]
            public tagNET_IVMS_DEVSCHED[] struDevSched;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_MASK_REGION
        {
            public byte byEnable;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
            public tagNET_VCA_POLYGON struPolygon;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_MASK_REGION_LIST
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes; 
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_MASK_REGION_NUM, ArraySubType = UnmanagedType.Struct)]
            public tagNET_VCA_MASK_REGION[] struMask;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_ENTER_REGION
        {
            public uint dwSize;
            public byte byEnable;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            public tagNET_VCA_POLYGON struPolygon;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
        }

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_VCA_RestartLib(int lUserID, int lChannel);

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_LINE_SEGMENT
        {
            public tagNET_VCA_POINT struStartPoint;
            public tagNET_VCA_POINT struEndPoint;
            public float fValue;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_VCA_LINE_SEG_LIST
        {
            public uint dwSize;
            public byte bySegNum;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
            public byte[] byRes;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_SEGMENT_NUM, ArraySubType = UnmanagedType.Struct)]
            public tagNET_VCA_LINE_SEGMENT[] struSeg;
        }

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetRealHeight(int lUserID, int lChannel, ref tagNET_VCA_LINE lpLine, ref Single lpHeight);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetRealLength(int lUserID, int lChannel, ref tagNET_VCA_LINE lpLine, ref Single lpLength);

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_IVMS_MASK_REGION_LIST
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DAYS * MAX_TIMESEGMENT, ArraySubType = UnmanagedType.Struct)]
            public tagNET_VCA_MASK_REGION_LIST[] struList;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_IVMS_ENTER_REGION
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DAYS * MAX_TIMESEGMENT, ArraySubType = UnmanagedType.Struct)]
            public tagNET_VCA_ENTER_REGION[] struEnter;
        }
     
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_IVMS_ALARM_JPEG
        {
            public byte byPicProType;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
            public NET_DVR_JPEGPARA struPicParam;
        }

        // IVMS
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_IVMS_SEARCHCFG
        {
            public uint dwSize;
            public NET_DVR_MATRIX_DEC_REMOTE_PLAY struRemotePlay;
            public tagNET_IVMS_ALARM_JPEG struAlarmJpeg;
            public tagNET_IVMS_RULECFG struRuleCfg;
        }
        
        public const int NET_DVR_GET_AP_INFO_LIST = 305;
        public const int NET_DVR_SET_WIFI_CFG = 306;
        public const int NET_DVR_GET_WIFI_CFG = 307;
        public const int NET_DVR_SET_WIFI_WORKMODE = 308;
        public const int NET_DVR_GET_WIFI_WORKMODE = 309;

        //public const int IW_ESSID_MAX_SIZE = 32;
        public const int WIFI_WEP_MAX_KEY_COUNT = 4;
        public const int WIFI_WEP_MAX_KEY_LENGTH = 33;
        public const int WIFI_WPA_PSK_MAX_KEY_LENGTH = 63;
        public const int WIFI_WPA_PSK_MIN_KEY_LENGTH = 8;
        public const int WIFI_MAX_AP_COUNT = 20;

        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct tagNET_DVR_AP_INFO
        {
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = IW_ESSID_MAX_SIZE)]
            public string sSsid;
            public uint dwMode;
            public uint dwSecurity;
            public uint dwChannel;
            public uint dwSignalStrength;
            public uint dwSpeed;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_DVR_AP_INFO_LIST
        {
            public uint dwSize;
            public uint dwCount;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = WIFI_MAX_AP_COUNT, ArraySubType = UnmanagedType.Struct)]
            public tagNET_DVR_AP_INFO[] struApInfo;
        }

        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct tagNET_DVR_WIFIETHERNET
        {
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sIpAddress;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sIpMask;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MACADDR_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byMACAddr;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            public byte[] bRes;
            public uint dwEnableDhcp;
            public uint dwAutoDns;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sFirstDns; 
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sSecondDns;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sGatewayIpAddr;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = UnmanagedType.I1)]
            public byte[] bRes2;
        }

        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct tagNET_DVR__WIFI_CFG_EX
        {
            public tagNET_DVR_WIFIETHERNET struEtherNet;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = IW_ESSID_MAX_SIZE)]
            public string sEssid;
            public uint dwMode;
            public uint dwSecurity;
            [StructLayoutAttribute(LayoutKind.Explicit)]
            public struct key
            {
                [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
                public struct wep
                {
                    public uint dwAuthentication;
                    public uint dwKeyLength;
                    public uint dwKeyType;
                    public uint dwActive;
                    [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = WIFI_WEP_MAX_KEY_COUNT * WIFI_WEP_MAX_KEY_LENGTH)]
                    public string sKeyInfo;
                }

                [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
                public struct wpa_psk
                {
                    public uint dwKeyLength;
                    [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = WIFI_WPA_PSK_MAX_KEY_LENGTH)]
                    public string sKeyInfo;
                    public byte sRes;
                }
            }
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_DVR_WIFI_CFG
        {
            public uint dwSize;
            public tagNET_DVR__WIFI_CFG_EX struWifiCfg;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_DVR_WIFI_WORKMODE
        {
            public uint dwSize;
            public uint dwNetworkInterfaceMode;
        }

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SaveRealData_V30(int lRealHandle, uint dwTransType, string sFileName);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_EncodeG711Frame(uint iType, ref byte pInBuffer, ref byte pOutBuffer);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_DecodeG711Frame(uint iType, ref byte pInBuffer, ref byte pOutBuffer);

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_DVR_SINGLE_NET_DISK_INFO
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            public NET_DVR_IPADDR struNetDiskAddr;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = PATHNAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sDirectory;// PATHNAME_LEN = 128
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 68, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
        }

        public const int MAX_NET_DISK = 16;

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_DVR_NET_DISKCFG
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_NET_DISK, ArraySubType = UnmanagedType.Struct)]
            public tagNET_DVR_SINGLE_NET_DISK_INFO[] struNetDiskParam;
        }
      
        public enum MAIN_EVENT_TYPE
        {
            EVENT_MOT_DET = 0,
            EVENT_ALARM_IN = 1,
            EVENT_VCA_BEHAVIOR = 2,
        }
       
        public enum BEHAVIOR_MINOR_TYPE
        {
            EVENT_TRAVERSE_PLANE = 0,
            EVENT_ENTER_AREA,
            EVENT_EXIT_AREA,
            EVENT_INTRUSION,
            EVENT_LOITER,
            EVENT_LEFT_TAKE,
            EVENT_PARKING,
            EVENT_RUN,
            EVENT_HIGH_DENSITY,
            EVENT_STICK_UP,
            EVENT_INSTALL_SCANNER,
        }
       
        public const int SEARCH_EVENT_INFO_LEN = 300;

        [StructLayoutAttribute(LayoutKind.Sequential)]
     
        public struct struAlarmParam
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_ALARMIN_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byAlarmInNo;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = SEARCH_EVENT_INFO_LEN - MAX_ALARMIN_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;

            public void init()
            {
                byAlarmInNo = new byte[MAX_ALARMIN_V30];
                byRes = new byte[SEARCH_EVENT_INFO_LEN - MAX_CHANNUM_V30];
            }
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct struMotionParam
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byMotDetChanNo;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = SEARCH_EVENT_INFO_LEN - MAX_CHANNUM_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;

            public void init()
            {
                byMotDetChanNo = new byte[MAX_CHANNUM_V30];
                byRes = new byte[SEARCH_EVENT_INFO_LEN - MAX_CHANNUM_V30];
            }
        }
       
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct struVcaParam
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byChanNo;
            public byte byRuleID;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 43, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;

            public void init()
            {
                byChanNo = new byte[MAX_CHANNUM_V30];
                byRes1 = new byte[43];
            }
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct uSeniorParam
        {
            //             [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = SEARCH_EVENT_INFO_LEN, ArraySubType = UnmanagedType.I1)]
            //             public byte[] byLen;
            [FieldOffset(0)]
            public struMotionParam struMotionPara;
            [FieldOffset(0)]
            public struAlarmParam struAlarmPara;

            //             public struVcaParam struVcaPara;

            public void init()
            {
                //                 byLen = new byte[SEARCH_EVENT_INFO_LEN];
                struAlarmPara = new struAlarmParam();
                struAlarmPara.init();
                //                 struMotionPara = new struMotionParam();
                //                 struMotionPara.init();
                //                 struVcaPara = new struVcaParam();
                //                 struVcaPara.init();
            }
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_DVR_SEARCH_EVENT_PARAM
        {
            public ushort wMajorType;
            public ushort wMinorType;
            public NET_DVR_TIME struStartTime;
            public NET_DVR_TIME struEndTime;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 132, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
            public uSeniorParam uSeniorPara;

            public void init()
            {
                byRes = new byte[132];
                uSeniorPara = new uSeniorParam();
                uSeniorPara.init();
            }
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct struAlarmRet
        {
            public uint dwAlarmInNo;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = SEARCH_EVENT_INFO_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;

            public void init()
            {
                byRes = new byte[SEARCH_EVENT_INFO_LEN];
            }
        }
        
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct struMotionRet
        {
            public uint dwMotDetNo;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = SEARCH_EVENT_INFO_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;

            public void init()
            {
                byRes = new byte[SEARCH_EVENT_INFO_LEN];
            }
        }
        
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct struVcaRet
        {
            public uint dwChanNo;
            public byte byRuleID;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byRuleName;
            public tagNET_VCA_EVENT_UNION uEvent;

            public void init()
            {
                byRes1 = new byte[3];
                byRuleName = new byte[NAME_LEN];
            }
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct uSeniorRet
        {
            [FieldOffset(0)]
            public struAlarmRet struAlarmRe;
            [FieldOffset(0)]
            public struMotionRet struMotionRe;
            //             public struVcaRet struVcaRe;

            public void init()
            {
                struAlarmRe = new struAlarmRet();
                struAlarmRe.init();
                //                 struVcaRe = new struVcaRet();
                //                 struVcaRe.init();
            }
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_DVR_SEARCH_EVENT_RET
        {
            public ushort wMajorType;
            public ushort wMinorType;
            public NET_DVR_TIME struStartTime;
            public NET_DVR_TIME struEndTime;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byChan;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 36, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
            public uSeniorRet uSeniorRe;

            public void init()
            {
                byChan = new byte[MAX_CHANNUM_V30];
                byRes = new byte[36];
                uSeniorRe = new uSeniorRet();
                uSeniorRe.init();
            }
        }

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_EmailTest(int lUserID);

        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_FindFileByEvent(int lUserID, ref tagNET_DVR_SEARCH_EVENT_PARAM lpSearchEventParam);

        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_FindNextEvent(int lSearchHandle, ref tagNET_DVR_SEARCH_EVENT_RET lpSearchEventRet);

        public const int PLATE_INFO_LEN = 1024;
        public const int PLATE_NUM_LEN = 16;
        public const int FILE_NAME_LEN = 256;

        public enum Anonymous_26594f67_851c_4f7d_bec4_094765b7ff83
        {
            BLUE_PLATE,
            YELLOW_PLATE, 
            WHITE_PLATE,
            BLACK_PLATE,
        }

        //liscense plate result
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_DVR_PLATE_RET
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = PLATE_NUM_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byPlateNum;
            public byte byVehicleType;
            public byte byTrafficLight;

            public byte byPlateColor;
            public byte byDriveChan;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.I1)]
            public byte[] byTimeInfo;

            public byte byCarSpeed;
            public byte byCarSpeedH;
            public byte byCarSpeedL;
            public byte byRes;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = PLATE_INFO_LEN - 36, ArraySubType = UnmanagedType.I1)]
            public byte[] byInfo;
            public uint dwPicLen;
        }

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_INVOKE_PLATE_RECOGNIZE(int lUserID, int lChannel, string pPicFileName, ref tagNET_DVR_PLATE_RET pPlateRet, string pPicBuf, uint dwPicBufLen);

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagNET_DVR_CCD_CFG
        {
            public uint dwSize;
            public byte byBlc;
            public byte byBlcMode;
            public byte byAwb;
            public byte byAgc;
            public byte byDayNight;
            public byte byMirror;
            public byte byShutter;
            public byte byIrCutTime;
            public byte byLensType;
            public byte byEnVideoTrig;
            public byte byCapShutter;
            public byte byEnRecognise;
        }

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetCCDCfg(int lUserID, int lChannel, ref tagNET_DVR_CCD_CFG lpCCDCfg);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetCCDCfg(int lUserID, int lChannel, ref tagNET_DVR_CCD_CFG lpCCDCfg);

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagCAMERAPARAMCFG
        {
            public uint dwSize;
            public uint dwPowerLineFrequencyMode;
            public uint dwWhiteBalanceMode;
            public uint dwWhiteBalanceModeRGain;
            public uint dwWhiteBalanceModeBGain;
            public uint dwExposureMode;
            public uint dwExposureSet;
            public uint dwExposureUserSet;
            public uint dwExposureTarget;
            public uint dwIrisMode;
            public uint dwGainLevel;
            public uint dwBrightnessLevel;
            public uint dwContrastLevel;
            public uint dwSharpnessLevel;
            public uint dwSaturationLevel;
            public uint dwHueLevel;
            public uint dwGammaCorrectionEnabled;
            public uint dwGammaCorrectionLevel;
            public uint dwWDREnabled;
            public uint dwWDRLevel1;
            public uint dwWDRLevel2;
            public uint dwWDRContrastLevel;
            public uint dwDayNightFilterType;
            public uint dwSwitchScheduleEnabled;
            
            public uint dwBeginTime;
            public uint dwEndTime;

            public uint dwDayToNightFilterLevel;
            public uint dwNightToDayFilterLevel;
            public uint dwDayNightFilterTime;
            public uint dwBacklightMode;
            public uint dwPositionX1;
            public uint dwPositionY1;
            public uint dwPositionX2;
            public uint dwPositionY2;
            public uint dwBacklightLevel;
            public uint dwDigitalNoiseRemoveEnable;
            public uint dwDigitalNoiseRemoveLevel;
            public uint dwMirror; 
            public uint dwDigitalZoom;
            public uint dwDeadPixelDetect;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 20, ArraySubType = UnmanagedType.U4)]
            public uint[] dwRes;
        }

        public const int NET_DVR_GET_CCDPARAMCFG = 1067;
        public const int NET_DVR_SET_CCDPARAMCFG = 1068;

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagIMAGEREGION
        {
            public uint dwSize;
            public ushort wImageRegionTopLeftX;
            public ushort wImageRegionTopLeftY;
            public ushort wImageRegionWidth;
            public ushort wImageRegionHeight;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }
        
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagIMAGESUBPARAM
        {
            public NET_DVR_SCHEDTIME struImageStatusTime;
            public byte byImageEnhancementLevel;
            public byte byImageDenoiseLevel;
            public byte byImageStableEnable;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 9, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        public const int NET_DVR_GET_IMAGEREGION = 1062; 
        public const int NET_DVR_SET_IMAGEREGION = 1063; 
        public const int NET_DVR_GET_IMAGEPARAM = 1064;
        public const int NET_DVR_SET_IMAGEPARAM = 1065;

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagIMAGEPARAM
        {
            public uint dwSize;
            
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DAYS * MAX_TIMESEGMENT, ArraySubType = UnmanagedType.Struct)]
            public tagIMAGESUBPARAM[] struImageParamSched;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetParamSetMode(int lUserID, ref uint dwParamSetMode);

        public struct NET_DVR_CLIENTINFO
        {
            public Int32 lChannel;
            public uint lLinkMode;
            public IntPtr hPlayWnd;
            public string sMultiCastIP;
        }

        public struct NET_SDK_CLIENTINFO
        {
            public Int32 lChannel;
            public Int32 lLinkType; 
            public Int32 lLinkMode;
            public IntPtr hPlayWnd;
            public string sMultiCastIP;
            public Int32 iMediaSrvNum;
            public System.IntPtr pMediaSrvDir;
        }

        public struct NET_DVR_PREVIEWINFO
        {
            public Int32 lChannel;
            public uint dwStreamType;
            public uint dwLinkMode;
            public IntPtr hPlayWnd;
            public bool bBlocked;
            public bool bPassbackRecord;
            public Byte byPreviewMode;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = Hikvision.CHCNetSDK.STREAM_ID_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byStreamID;
            public Byte byProtoType;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            public uint DisplayBufNum;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 216, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        /*********************************************************
        Function:	NET_DVR_Login_V30
        Desc:  
        Input:	sDVRIP [in]
                wServerPort [in]
                sUserName [in]
                sPassword [in]
        Output:	lpDeviceInfo [out] 
        Return:	-1
        **********************************************************/
        [DllImport(@"HCNetSDK.dll")]
        public static extern Int32 NET_DVR_Login_V30(string sDVRIP, Int32 wDVRPort, string sUserName, string sPassword, ref NET_DVR_DEVICEINFO_V30 lpDeviceInfo);
                
        /*********************************************************
        Function:	NET_DVR_Logout_V30
        Desc: 
        Input:	lUserID
        Output:	
        Return:	TRUE
        **********************************************************/
        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_Logout_V30(Int32 lUserID);

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_SNAPCFG
        {
            public uint dwSize;
            public byte byRelatedDriveWay;
            public byte bySnapTimes;
            public ushort wSnapWaitTime;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.U2)]
            public ushort[] wIntervalTime;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 24, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
        }

        /*********************************************************
        Function:	NET_DVR_ContinuousShoot
        Desc:
        Input:	    lUserID
                    lpInter 
        Output:	
        Return:	TRUE
        **********************************************************/
        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_ContinuousShoot(Int32 lUserID, ref NET_DVR_SNAPCFG lpInter);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_ManualSnap(Int32 lUserID, ref NET_DVR_MANUALSNAP lpInter, ref NET_DVR_PLATE_RESULT lpOuter);

        #region
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct PLAY_INFO
        {
            public int iUserID;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 20)]
            public string strDeviceIP;
            public int iDevicePort;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string strDevAdmin;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string strDevPsd;
            public int iChannel;
            public int iLinkMode;
            public bool bUseMedia;    
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 20)]
            public string strMediaIP;
            public int iMediaPort;   
        }

        [DllImport("GetStream.dll")]
        public static extern bool CLIENT_SDK_Init();

        [DllImport("GetStream.dll")]
        public static extern bool CLIENT_SDK_UnInit();

        [DllImport("GetStream.dll")]
        public static extern int CLIENT_SDK_GetStream(PLAY_INFO lpPlayInfo);

        [DllImport("GetStream.dll")]
        public static extern bool CLIENT_SetRealDataCallBack(int iRealHandle, SETREALDATACALLBACK fRealDataCallBack, uint lUser); //

        [DllImport("GetStream.dll")]
        public static extern bool CLIENT_SDK_StopStream(int iRealHandle);

        [DllImport("GetStream.dll")]
        public static extern bool CLIENT_SDK_GetVideoEffect(int iRealHandle, ref int iBrightValue, ref int iContrastValue, ref int iSaturationValue, ref int iHueValue);

        [DllImport("GetStream.dll")]
        public static extern bool CLIENT_SDK_SetVideoEffect(int iRealHandle, int iBrightValue, int iContrastValue, int iSaturationValue, int iHueValue);

        [DllImport("GetStream.dll")]
        public static extern bool CLIENT_SDK_MakeKeyFrame(int iRealHandle);

        #endregion

        #region
        public const int WM_NETERROR = 0x0400 + 102;    
        public const int WM_STREAMEND = 0x0400 + 103;    

        public const int FILE_HEAD = 0;     
        public const int VIDEO_I_FRAME = 1; 
        public const int VIDEO_B_FRAME = 2; 
        public const int VIDEO_P_FRAME = 3; 
        public const int VIDEO_BP_FRAME = 4;
        public const int VIDEO_BBP_FRAME = 5; 
        public const int AUDIO_PACKET = 10;  

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct BLOCKTIME
        {
            public ushort wYear;
            public byte bMonth;
            public byte bDay;
            public byte bHour;
            public byte bMinute;
            public byte bSecond;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct VODSEARCHPARAM
        {
            public IntPtr sessionHandle;                                  
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 50)]
            public string dvrIP;                                            
            public uint dvrPort;                                           
            public uint channelNum;                                        
            public BLOCKTIME startTime;                                     
            public BLOCKTIME stopTime;                                     
            public bool bUseIPServer;                                       
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string SerialNumber;                                    
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct SECTIONLIST
        {
            public BLOCKTIME startTime;
            public BLOCKTIME stopTime;
            public byte byRecType;
            public IntPtr pNext;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct VODOPENPARAM
        {
            public IntPtr sessionHandle; 
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 50)]
            public string dvrIP;                                           
            public uint dvrPort;                                          
            public uint channelNum;                                        
            public BLOCKTIME startTime;                                    
            public BLOCKTIME stopTime;                                      
            public uint uiUser;
            public bool bUseIPServer;                                       
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string SerialNumber;                                     

            public VodStreamFrameData streamFrameData;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct CONNPARAM
        {
            public uint uiUser;
            public ErrorCallback errorCB;
        }

        public delegate void ErrorCallback(System.IntPtr hSession, uint dwUser, int lErrorType);

        public delegate void VodStreamFrameData(System.IntPtr hStream, uint dwUser, int lFrameType, System.IntPtr pBuffer, uint dwSize);

        [DllImport("PdCssVodClient.dll")]
        public static extern bool VODServerConnect(string strServerIp, uint uiServerPort, ref IntPtr hSession, ref CONNPARAM struConn, IntPtr hWnd);

        [DllImport("PdCssVodClient.dll")]
        public static extern bool VODServerDisconnect(IntPtr hSession);

        [DllImport("PdCssVodClient.dll")]
        public static extern bool VODStreamSearch(IntPtr pSearchParam, ref IntPtr pSecList);

        [DllImport("PdCssVodClient.dll")]
        public static extern bool VODDeleteSectionList(IntPtr pSecList);

        [DllImport("PdCssVodClient.dll")]
        public static extern bool VODOpenStream(IntPtr pOpenParam, ref IntPtr phStream);

        [DllImport("PdCssVodClient.dll")]
        public static extern bool VODCloseStream(IntPtr hStream);

        [DllImport("PdCssVodClient.dll")]
        public static extern bool VODOpenDownloadStream(ref VODOPENPARAM struVodParam, ref IntPtr phStream);

        [DllImport("PdCssVodClient.dll")]
        public static extern bool VODCloseDownloadStream(IntPtr hStream);

        [DllImport("PdCssVodClient.dll")]
        public static extern bool VODStartStreamData(IntPtr phStream);
        [DllImport("PdCssVodClient.dll")]
        public static extern bool VODPauseStreamData(IntPtr hStream, bool bPause);
        [DllImport("PdCssVodClient.dll")]
        public static extern bool VODStopStreamData(IntPtr hStream);

        [DllImport("PdCssVodClient.dll")]
        public static extern bool VODSeekStreamData(IntPtr hStream, IntPtr pStartTime);

        [DllImport("PdCssVodClient.dll")]
        public static extern bool VODSetStreamSpeed(IntPtr hStream, int iSpeed);

        [DllImport("PdCssVodClient.dll")]
        public static extern bool VODGetStreamCurrentTime(IntPtr hStream, ref BLOCKTIME pCurrentTime);

        #endregion
        
        #region
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct PACKET_INFO
        {
            public int nPacketType;     // packet type
            // 0:  file head
            // 1:  video I frame
            // 2:  video B frame
            // 3:  video P frame
            // 10: audio frame
            // 11: private frame only for PS


            //      [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.LPStr)]
            public IntPtr pPacketBuffer;
            public uint dwPacketSize;
            public int nYear;
            public int nMonth;
            public int nDay;
            public int nHour;
            public int nMinute;
            public int nSecond;
            public uint dwTimeStamp;
        }

        /******************************************************************************
        * function get a empty port number
        * parameters
        * return 0 - 499 : empty port number
        *          -1      : server is full    	
        * comment
        ******************************************************************************/
        [DllImport(@"C:\DLL\AnalyzeData.dll")]
        public static extern int AnalyzeDataGetSafeHandle();


        /******************************************************************************
        * function open standard stream data for analyzing
        * parameters lHandle - working port number
        *             pHeader - pointer to file header or info header
        * return TRUE or FALSE
        * comment
        ******************************************************************************/
        [DllImport(@"AnalyzeData.dll")]
        public static extern bool AnalyzeDataOpenStreamEx(int iHandle, byte[] pFileHead);


        /******************************************************************************
        * function£ºclose analyzing
        * parameters£ºlHandle - working port number
        * return
        * comment
        ******************************************************************************/
        [DllImport(@"AnalyzeData.dll")]
        public static extern bool AnalyzeDataClose(int iHandle);


        /******************************************************************************
        * function input stream data
        * parameters lHandle  - working port number
        *  	  pBuffer  - data pointer
        *  	  dwBuffersize	- data size
        * return£ºTRUE or FALSE
        * comment
        ******************************************************************************/
        [DllImport(@"AnalyzeData.dll")]
        public static extern bool AnalyzeDataInputData(int iHandle, IntPtr pBuffer, uint uiSize); //byte []


        /******************************************************************************
        * function get analyzed packet
        * parameters£ºlHandle  - working port number
        *  	  pPacketInfo	- returned structure
        * return -1 : error
        *          0 : succeed
        *     1 : failed
        *     2 : file end (only in file mode)    
        * comment
        ******************************************************************************/
        [DllImport(@"AnalyzeData.dll")]
        public static extern int AnalyzeDataGetPacket(int iHandle, ref PACKET_INFO pPacketInfo); 
        
        /*****************************************************************************
        * function get remain data from input buffer
        * parameters£ºlHandle  - working port number
        *  	  pBuf	        - pointer to the mem which stored remain data
        *             dwSize        - size of remain data  
        * return TRUE or FALSE    
        * comment
        ******************************************************************************/
        [DllImport(@"AnalyzeData.dll")]
        public static extern bool AnalyzeDataGetTail(int iHandle, ref IntPtr pBuffer, ref uint uiSize);


        [DllImport(@"AnalyzeData.dll")]
        public static extern uint AnalyzeDataGetLastError(int iHandle);

        #endregion

        #region

        public const int DATASTREAM_HEAD = 0;
        public const int DATASTREAM_BITBLOCK = 1;
        public const int DATASTREAM_KEYFRAME = 2;
        public const int DATASTREAM_NORMALFRAME = 3;


        public const int MESSAGEVALUE_DISKFULL = 0x01;
        public const int MESSAGEVALUE_SWITCHDISK = 0x02;
        public const int MESSAGEVALUE_CREATEFILE = 0x03;
        public const int MESSAGEVALUE_DELETEFILE = 0x04;
        public const int MESSAGEVALUE_SWITCHFILE = 0x05;

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct STOREINFO
        {
            public int iMaxChannels;
            public int iDiskGroup;
            public int iStreamType;
            public bool bAnalyze;
            public bool bCycWrite;
            public uint uiFileSize;

            public CALLBACKFUN_MESSAGE funCallback;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct CREATEFILE_INFO
        {
            public int iHandle;

            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string strCameraid;

            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string strFileName;

            public BLOCKTIME tFileCreateTime;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct CLOSEFILE_INFO
        {
            public int iHandle;

            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string strCameraid;

            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string strFileName;

            public BLOCKTIME tFileSwitchTime;
        }

        public delegate int CALLBACKFUN_MESSAGE(int iMessageType, System.IntPtr pBuf, int iBufLen);

        [DllImport("RecordDLL.dll")]
        public static extern int Initialize(STOREINFO struStoreInfo);

        [DllImport("RecordDLL.dll")]
        public static extern int Release();

        [DllImport("RecordDLL.dll")]
        public static extern int OpenChannelRecord(string strCameraid, IntPtr pHead, uint dwHeadLength);

        [DllImport("RecordDLL.dll")]
        public static extern bool CloseChannelRecord(int iRecordHandle);

        [DllImport("RecordDLL.dll")]
        public static extern int GetData(int iHandle, int iDataType, IntPtr pBuf, uint uiSize);

        #endregion

        public const int REGIONTYPE = 0;
        public const int MATRIXTYPE = 11;
        public const int DEVICETYPE = 2;
        public const int CHANNELTYPE = 3;
        public const int USERTYPE = 5;

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_LOG_MATRIX
        {
            public NET_DVR_TIME strLogTime;
            public uint dwMajorType;
            public uint dwMinorType;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_NAMELEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sPanelUser;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_NAMELEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sNetUser;
            public NET_DVR_IPADDR struRemoteHostAddr;
            public uint dwParaType;
            public uint dwChannel;
            public uint dwDiskNumber;
            public uint dwAlarmInPort;
            public uint dwAlarmOutPort;
            public uint dwInfoLen;
            public byte byDevSequence;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MACADDR_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byMacAddr;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = SERIALNO_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sSerialNumber;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = LOG_INFO_LEN - SERIALNO_LEN - MACADDR_LEN - 1)]
            public string sInfo;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct tagVEDIOPLATLOG
        {
            public byte bySearchCondition;
            public byte byDevSequence;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = SERIALNO_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sSerialNumber;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MACADDR_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byMacAddr;
        }

        [DllImportAttribute(@"HCNetSDK.dll")]
        public static extern int NET_DVR_FindNextLog_MATRIX(int iLogHandle, ref NET_DVR_LOG_MATRIX lpLogData);


        [DllImportAttribute(@"HCNetSDK.dll")]
        public static extern int NET_DVR_FindDVRLog_Matrix(int iUserID, int lSelectMode, uint dwMajorType, uint dwMinorType, ref tagVEDIOPLATLOG lpVedioPlatLog, ref NET_DVR_TIME lpStartTime, ref NET_DVR_TIME lpStopTime);
        
        /*************************************************
        NET_DVR_StartRemoteConfig
        **************************************************/
        public const int MAX_CARDNO_LEN = 48;
        public const int MAX_OPERATE_INDEX_LEN = 32;
        public const int NET_DVR_GET_ALL_VEHICLE_CONTROL_LIST = 3124;
        public const int NET_DVR_VEHICLE_DELINFO_CTRL = 3125; 
        public const int NET_DVR_VEHICLELIST_CTRL_START = 3133;

        /*********************************************************
        Function:	REMOTECONFIGCALLBACK
        Desc: 
        Input:	
        Output:	
        Return:	
        **********************************************************/
        public delegate void REMOTECONFIGCALLBACK(uint dwType, IntPtr lpBuffer, uint dwBufLen, IntPtr pUserData);
        [DllImportAttribute(@"HCNetSDK.dll")]
        public static extern Int32 NET_DVR_StartRemoteConfig(Int32 lUserID, uint dwCommand, IntPtr lpInBuffer, Int32 dwInBufferLen, REMOTECONFIGCALLBACK cbStateCallback, IntPtr pUserData);

 
        [DllImportAttribute(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SendRemoteConfig(Int32 lHandle, uint dwDataType, IntPtr pSendBuf, uint dwBufSize);

        [DllImportAttribute(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_StopRemoteConfig(Int32 lHandle);

        [DllImportAttribute(@"HCNetSDK.dll")]
        public static extern Int32 NET_DVR_GetNextRemoteConfig(Int32 lHandle, IntPtr lpOutBuff, uint dwOutBuffSize);

        public enum VCA_OPERATE_TYPE
        {
            VCA_LICENSE_TYPE = 0x1, 
            VCA_PLATECOLOR_TYPE = 0x2, 
            VCA_CARDNO_TYPE = 0x4,
            VCA_PLATETYPE_TYPE = 0x8, 
            VCA_LISTTYPE_TYPE = 0x10,
            VCA_INDEX_TYPE = 0x20,
            VCA_OPERATE_INDEX_TYPE = 0x40 
        }

        // NET_DVR_StartRemoteConfig CallBack
        public enum NET_SDK_CALLBACK_TYPE
        {
            NET_SDK_CALLBACK_TYPE_STATUS = 0,
            NET_SDK_CALLBACK_TYPE_PROGRESS,
            NET_SDK_CALLBACK_TYPE_DATA
        }

        // NET_DVR_StartRemoteConfig CallBack
        public enum NET_SDK_CALLBACK_STATUS_NORMAL
        {
            NET_SDK_CALLBACK_STATUS_SUCCESS = 1000,
            NET_SDK_CALLBACK_STATUS_PROCESSING,  
            NET_SDK_CALLBACK_STATUS_FAILED, 
            NET_SDK_CALLBACK_STATUS_EXCEPTION,  
            NET_SDK_CALLBACK_STATUS_LANGUAGE_MISMATCH,	
            NET_SDK_CALLBACK_STATUS_DEV_TYPE_MISMATCH,
            NET_DVR_CALLBACK_STATUS_SEND_WAIT   
        }

        public struct NET_DVR_VEHICLE_CONTROL_COND
        {
            public uint dwChannel;
            public uint dwOperateType;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = MAX_LICENSE_LEN)]
            public String sLicense;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = MAX_CARDNO_LEN)]
            public String sCardNo;
            public byte byListType;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            public uint dwDataIndex;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 116, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;

        }

        // NET_DVR_GetNextRemoteConfig
        public enum NET_SDK_GET_NEXT_STATUS
        {
            NET_SDK_GET_NEXT_STATUS_SUCCESS = 1000,
            NET_SDK_GET_NETX_STATUS_NEED_WAIT, 
            NET_SDK_GET_NEXT_STATUS_FINISH,  	
            NET_SDK_GET_NEXT_STATUS_FAILED,
        }

        public struct tagNET_DVR_VEHICLE_CONTROL_LIST_INFO
        {
            public uint dwSize;
            public uint dwChannel;
            public uint dwDataIndex;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = MAX_LICENSE_LEN)]
            public String sLicense;
            public byte byListType;
            public byte byPlateType;
            public byte byPlateColor;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 21, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = MAX_CARDNO_LEN)]
            public String sCardNo;
            public NET_DVR_TIME_V30 struStartTime;
            public NET_DVR_TIME_V30 struStopTime;
            
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = MAX_OPERATE_INDEX_LEN)]
            public String sOperateIndex;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 224, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1; 
        }
        
        public struct tagNET_DVR_VEHICLE_CONTROL_COND
        {
            public uint dwChannel;
            public uint dwOperateType;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = MAX_LICENSE_LEN)]
            public String sLicense; 
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = MAX_CARDNO_LEN)]
            public String sCardNo; 
            public byte byListType;
            
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            public uint dwDataIndex;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 116, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }
        
        public struct NET_DVR_VEHICLE_CONTROL_DELINFO
        {
            public uint dwSize;
            public uint dwDelType;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public String sLicense; 
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 48)]
            public String sCardNo; 
            public byte byPlateType;	
            public byte byPlateColor;	
            public byte byOperateType;

            public byte byListType;
            public uint dwDataIndex;	
            
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = MAX_OPERATE_INDEX_LEN)]
            public String sOperateIndex;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 24, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }
                
        [DllImportAttribute(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetLocalIP(byte[] strIP, ref Int32 pValidNum, ref bool pEnableBind);

        [DllImportAttribute(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetValidIP(uint dwIPIndex, bool bEnableBind);
        
        public const int DOOR_NAME_LEN = 32;
        public const int STRESS_PASSWORD_LEN = 8;
        public const int SUPER_PASSWORD_LEN = 8;
        public const int UNLOCK_PASSWORD_LEN = 8;
        public const int NET_DVR_GET_DOOR_CFG = 2108; 
        public const int NET_DVR_SET_DOOR_CFG = 2109; 
        public const int COMM_ALARM_ACS = 0x5002; 
        public const int ACS_CARD_NO_LEN = 32;
        public const int MAX_DOOR_NUM = 32;
        public const int MAX_GROUP_NUM = 32;
        public const int MAX_CARD_RIGHT_PLAN_NUM = 4;
        public const int CARD_PASSWORD_LEN = 8;

        public struct NET_DVR_DOOR_CFG
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = DOOR_NAME_LEN)]
            public String byDoorName;
            public byte byMagneticType;
            public byte byOpenButtonType;
            public byte byOpenDuration;
            public byte byDisabledOpenDuration;
            public byte byMagneticAlarmTimeout;
            public byte byEnableDoorLock;
            public byte byEnableLeaderCard;
            public byte byRes1;
            public uint dwLeaderCardOpenDuration;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = STRESS_PASSWORD_LEN)]
            public String byStressPassword;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = SUPER_PASSWORD_LEN)]
            public String bySuperPassword;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = UNLOCK_PASSWORD_LEN)]
            public String byUnlockPassword;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 56)]
            public String byRes2;
        }

        public struct NET_DVR_ACS_ALARM_INFO //unsafe
        {
            public uint dwSize;
            public uint dwMajor;
            public uint dwMinor;
            public NET_DVR_TIME struTime;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_NAMELEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sNetUser;
            public NET_DVR_IPADDR struRemoteHostAddr;
            public NET_DVR_ACS_EVENT_INFO struAcsEventInfo;
            public uint dwPicDataLen;
            //public void* pPicData;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 24, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        public struct NET_DVR_ACS_EVENT_INFO
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = ACS_CARD_NO_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byCardNo; 
            public byte byCardType; 
            public byte byWhiteListNo;
            public byte byReportChannel; 
            public byte byCardReaderKind; 
            public uint dwCardReaderNo;
            public uint dwDoorNo;
            public uint dwVerifyNo;
            public uint dwAlarmInNo;
            public uint dwAlarmOutNo; 
            public uint dwCaseSensorNo;
            public uint dwRs485No;
            public uint dwMultiCardGroupNo;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        public const int ZERO_CHAN_INDEX = 500;
        public const int MIRROR_CHAN_INDEX = 400;
        public const int STREAM_ID_LEN = 32;
        public const int MAX_CHANNUM_V40 = 512;

        //compression parameter
        public const int NORM_HIGH_STREAM_COMPRESSION = 0; 
        public const int SUB_STREAM_COMPRESSION = 1;
        public const int EVENT_INVOKE_COMPRESSION = 2;
        public const int THIRD_STREAM_COMPRESSION = 3;
        public const int TRANS_STREAM_COMPRESSION = 4;

        public const int NET_DVR_GET_AUDIO_INPUT = 3201;
        public const int NET_DVR_SET_AUDIO_INPUT = 3202;
        public const int NET_DVR_GET_MULTI_STREAM_COMPRESSIONCFG = 3216;
        public const int NET_DVR_SET_MULTI_STREAM_COMPRESSIONCFG = 3217;

        public struct NET_DVR_VALID_PERIOD_CFG
        {
            public byte byEnable;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            public NET_DVR_TIME_EX struBeginTime; 
            public NET_DVR_TIME_EX struEndTime;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
        }
        public struct NET_DVR_TIME_EX
        {
            public Int16 wYear;
            public byte byMonth;
            public byte byDay;
            public byte byHour;
            public byte byMinute;
            public byte bySecond;
            public byte byRes;
        }        

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_STREAM_INFO
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = STREAM_ID_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byID;
            public uint dwChannel;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }
        
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_MULTI_STREAM_COMPRESSIONCFG_COND
        {
            public uint dwSize;
            public NET_DVR_STREAM_INFO struStreamInfo;
            public uint dwStreamType;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_MULTI_STREAM_COMPRESSIONCFG
        {
            public uint dwSize;
            public uint dwStreamType; 
            public NET_DVR_COMPRESSION_INFO_V30 struStreamPara;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 80, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_AUDIO_INPUT_PARAM
        {
            public byte byAudioInputType;
            public byte byVolume; 
            public byte byEnableNoiseFilter;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 5, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        public struct NET_DVR_RECORDSCHED_V40
        {
            public NET_DVR_SCHEDTIME struRecordTime;

            public byte byRecordType;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 31, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }
       
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_RECORDDAY_V40
        {
            public byte byAllDayRecord;

            public byte byRecordType;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 62, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_RECORD_V40
        {
            public uint dwSize;
            public uint dwRecord;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DAYS, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_RECORDDAY_V40[] struRecAllDay;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DAYS * MAX_TIMESEGMENT_V30, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_RECORDSCHED_V40[] struRecordSched;
            public uint dwRecordTime; 
            public uint dwPreRecordTime;
            public uint dwRecorderDuration;
            public byte byRedundancyRec; 
            public byte byAudioRec; 
            public byte byStreamType;  
            public byte byPassbackRecord; 
            public ushort wLockDuration;
            public byte byRecordBackup;  
            public byte bySVCLevel;
            public byte byRecordManage;  
            public byte byExtraSaveAudio;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 126, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        public const int NET_DVR_GET_CARD_CFG = 2116;
        public const int NET_DVR_SET_CARD_CFG = 2117; 

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_CARD_CFG
        {
            public uint dwSize;
            public uint dwModifyParamType;

            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = ACS_CARD_NO_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byCardNo;
            public byte byCardValid;
            public byte byCardType;
            public byte byLeaderCard;
            public byte byRes1;
            public uint dwDoorRight;
            public NET_DVR_VALID_PERIOD_CFG struValid;
            public uint dwBelongGroup;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = CARD_PASSWORD_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byCardPassword;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DOOR_NUM * MAX_CARD_RIGHT_PLAN_NUM, ArraySubType = UnmanagedType.I1)]
            public byte[] byCardRightPlan; 
            public uint dwMaxSwipeTime; 
            public uint dwSwipeTime; 
            public ushort wRoomNumber; 
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 22, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_CARD_CFG_COND
        {
            public uint dwSize;
            public uint dwCardNum;
            public byte byCheckCardNo;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 31, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_CARD_CFG_SEND_DATA
        {
            public uint dwSize;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.I1)]
            public byte[] byCardNo;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }
        
        public const int NET_DVR_GET_GROUP_CFG = 2112;  
        public const int NET_DVR_SET_GROUP_CFG = 2113;    

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_GROUP_CFG
        {
            public uint dwSize;
            public byte byEnable;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] byRes1;
            public NET_DVR_VALID_PERIOD_CFG struValidPeriodCfg;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            public byte[] byGroupName;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            public byte[] byRes2;

        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_ALARM_DEVICE_USER
        {
            public uint dwSize;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            public byte[] sUserName;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public byte[] sPassword;
            public NET_DVR_IPADDR struUserIP;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public byte[] byAMCAddr;
            public byte byUserType;
            public byte byAlarmOnRight;
            public byte byAlarmOffRight;
            public byte byBypassRight;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            public byte[] byOtherRight;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public byte[] byNetPreviewRight;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public byte[] byNetRecordRight;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public byte[] byNetPlaybackRight;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public byte[] byNetPTZRight;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 128)]
            public byte[] byRes2;

        }

        public const int NET_DVR_GET_FINGERPRINT_CFG = 2150; 
        public const int NET_DVR_SET_FINGERPRINT_CFG = 2151;  
        public const int NET_DVR_DEL_FINGERPRINT_CFG = 2152; 
        public const int MAX_FINGER_PRINT_LEN = 768;            

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_FINGER_PRINT_CFG
        {
            public uint dwSize;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.I1)]
            public byte[] byCardNo;  
            public uint dwFingerPrintLen; 
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 512, ArraySubType = UnmanagedType.I1)]
            public byte[] byEnableCardReader;
            public byte byFingerPrintID; 
            public byte byFingerType; 
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 30, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_FINGER_PRINT_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byFingerData;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        public struct StrucTEST
        {
            public uint dwSize;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_FINGER_PRINT_STATUS
        {
            public uint dwSize;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            public byte[] byCardNo;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 512)]
            public byte[] byCardReaderRecvStatus;
            public byte byFingerPrintID;
            public byte byFingerType;
            public byte byTotalStatus;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 61)]
            public byte[] byRes;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_FINGER_PRINT_INFO_COND
        {
            public uint dwSize;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            public byte[] byCardNo;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 512)]
            public byte[] byEnableCardReader;
            public uint dwFingerPrintNum;
            public byte byFingerPrintID;
            public byte byCallbackMode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
            public byte[] byRes1;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_FINGER_PRINT_BYCARD
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            public byte[] byCardNo;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 512)]
            public byte[] byEnableCardReader; 
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public byte[] byFingerPrintID;  
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 34)]
            public byte[] byRes1;

            //public void init()
            //{
            //    byCardNo = new byte[32];
            //    byEnableCardReader = new byte[512];
            //    byFingerPrintID = new byte[10];
            //    byRes1 = new byte[34];
            //}
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_FINGER_PRINT_BYREADER
        {
            public uint dwCardReaderNo;
            public byte byClearAllCard;  
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] byRes1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            public byte[] byCardNo; 
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 548)]
            public byte[] byRes;

            //public void init()
            //{
            //    byRes1 = new byte[3];
            //    byCardNo = new byte[32];
            //    byRes = new byte[548];
            //}
        }

        //public const int DEL_FINGER_PRINT_MODE_LEN = 588;
        //[StructLayoutAttribute(LayoutKind.Sequential)]
        //public struct NET_DVR_DEL_FINGER_PRINT_MODE
        //{
        //    public NET_DVR_FINGER_PRINT_BYCARD struByCard;   
        //    public NET_DVR_FINGER_PRINT_BYREADER struByReader;

        //    //public void init()
        //    //{
        //    //    struByCard = new NET_DVR_FINGER_PRINT_BYCARD();
        //    //    struByCard.init();
        //    //}
        //}

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_FINGER_PRINT_INFO_CTRL_BYCARD
        {
            public uint dwSize;
            public byte byMode; 
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] byRes1;

            public NET_DVR_FINGER_PRINT_BYCARD struByCard;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
            public byte[] byRes;

        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_FINGER_PRINT_INFO_CTRL_BYREADER
        {
            public uint dwSize;
            public byte byMode; 
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] byRes1;

            public NET_DVR_FINGER_PRINT_BYREADER struByReader;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
            public byte[] byRes;

        }

        public const int NET_DVR_GET_CARD_READER_CFG = 2140;
        public const int NET_DVR_SET_CARD_READER_CFG = 2141;

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_CARD_READER_CFG
        {
            public uint dwSize;
            public byte byEnable;
            public byte byCardReaderType;
            public byte byOkLedPolarity;
            public byte byErrorLedPolarity;
            public byte byBuzzerPolarity;
            public byte bySwipeInterval;
            public byte byPressTimeout;
            public byte byEnableFailAlarm;
            public byte byMaxReadCardFailNum;
            public byte byEnableTamperCheck;
            public byte byOfflineCheckTime;
            public byte byFingerPrintCheckLevel;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 24)]
            public byte[] byRes;
        }

        public const int NET_DVR_GET_WEEK_PLAN_CFG = 2100;
        public const int NET_DVR_SET_WEEK_PLAN_CFG = 2101;
        public const int NET_DVR_GET_DOOR_STATUS_HOLIDAY_PLAN = 2102;
        public const int NET_DVR_SET_DOOR_STATUS_HOLIDAY_PLAN = 2103;
        public const int NET_DVR_GET_DOOR_STATUS_HOLIDAY_GROUP = 2104;
        public const int NET_DVR_SET_DOOR_STATUS_HOLIDAY_GROUP = 2105;
        public const int NET_DVR_GET_DOOR_STATUS_PLAN_TEMPLATE = 2106;
        public const int NET_DVR_SET_DOOR_STATUS_PLAN_TEMPLATE = 2107;
        public const int NET_DVR_GET_DOOR_STATUS_PLAN = 2110;
        public const int NET_DVR_SET_DOOR_STATUS_PLAN = 2111; 
        public const int NET_DVR_CLEAR_ACS_PARAM = 2118;
        public const int NET_DVR_GET_VERIFY_WEEK_PLAN = 2124; 
        public const int NET_DVR_SET_VERIFY_WEEK_PLAN = 2125; 
        public const int NET_DVR_GET_CARD_RIGHT_WEEK_PLAN = 2126; 
        public const int NET_DVR_SET_CARD_RIGHT_WEEK_PLAN = 2127;
        public const int NET_DVR_GET_VERIFY_HOLIDAY_PLAN = 2128;
        public const int NET_DVR_SET_VERIFY_HOLIDAY_PLAN = 2129;
        public const int NET_DVR_GET_CARD_RIGHT_HOLIDAY_PLAN = 2130; 
        public const int NET_DVR_SET_CARD_RIGHT_HOLIDAY_PLAN = 2131;
        public const int NET_DVR_GET_VERIFY_HOLIDAY_GROUP = 2132;
        public const int NET_DVR_SET_VERIFY_HOLIDAY_GROUP = 2133; 
        public const int NET_DVR_GET_CARD_RIGHT_HOLIDAY_GROUP = 2134;
        public const int NET_DVR_SET_CARD_RIGHT_HOLIDAY_GROUP = 2135;
        public const int NET_DVR_GET_VERIFY_PLAN_TEMPLATE = 2136;
        public const int NET_DVR_SET_VERIFY_PLAN_TEMPLATE = 2137;
        public const int NET_DVR_GET_CARD_RIGHT_PLAN_TEMPLATE = 2138;
        public const int NET_DVR_SET_CARD_RIGHT_PLAN_TEMPLATE = 2139;
        public const int NET_DVR_GET_CARD_READER_PLAN = 2142;
        public const int NET_DVR_SET_CARD_READER_PLAN = 2143;
        public const int NET_DVR_GET_CARD_USERINFO_CFG = 2163;
        public const int NET_DVR_SET_CARD_USERINFO_CFG = 2164;

        public const int NET_DVR_CONTROL_PTZ_MANUALTRACE = 3316;
        public const int NET_DVR_GET_SMARTTRACKCFG = 3293;  
        public const int NET_DVR_SET_SMARTTRACKCFG = 3294;

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_DATE
        {
            public ushort wYear; 
            public byte byMonth;
            public byte byDay;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_SIMPLE_DAYTIME
        {
            public byte byHour;
            public byte byMinute;
            public byte bySecond;
            public byte byRes;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_TIME_SEGMENT
        {
            public NET_DVR_SIMPLE_DAYTIME struBeginTime;
            public NET_DVR_SIMPLE_DAYTIME struEndTime;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_SINGLE_PLAN_SEGMENT
        {
            public byte byEnable;
            public byte byDoorStatus;
            public byte byVerifyMode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public byte[] byRes;
            public NET_DVR_TIME_SEGMENT struTimeSegment;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_WEEK_PLAN_CFG
        {
            public uint dwSize;
            public byte byEnable;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] byRes1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = Hikvision.CHCNetSDK.MAX_DAYS * Hikvision.CHCNetSDK.MAX_TIMESEGMENT_V30, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_SINGLE_PLAN_SEGMENT[] struPlanCfg;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public byte[] byRes2;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_HOLIDAY_PLAN_CFG
        {
            public uint dwSize;
            public byte byEnable;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] byRes1;
            public Hikvision.CHCNetSDK.NET_DVR_DATE struBeginDate;
            public Hikvision.CHCNetSDK.NET_DVR_DATE struEndDate;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = Hikvision.CHCNetSDK.MAX_TIMESEGMENT_V30, ArraySubType = UnmanagedType.Struct)]
            public Hikvision.CHCNetSDK.NET_DVR_SINGLE_PLAN_SEGMENT[] struPlanCfg;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public byte[] byRes2;
        }

        public const int TEMPLATE_NAME_LEN = 32;
        public const int MAX_HOLIDAY_GROUP_NUM = 16;
        public const int HOLIDAY_GROUP_NAME_LEN = 32;
        public const int MAX_HOLIDAY_PLAN_NUM = 16;

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_HOLIDAY_GROUP_CFG
        {
            public uint dwSize;
            public byte byEnable;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] byRes1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = Hikvision.CHCNetSDK.HOLIDAY_GROUP_NAME_LEN)]
            public byte[] byGroupName;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = Hikvision.CHCNetSDK.MAX_HOLIDAY_GROUP_NUM, ArraySubType = UnmanagedType.U4)]
            public uint[] dwHolidayPlanNo;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            public byte[] byRes2;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_PLAN_TEMPLATE
        {
            public uint dwSize;
            public byte byEnable;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] byRes1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = Hikvision.CHCNetSDK.TEMPLATE_NAME_LEN)]
            public byte[] byTemplateName;
            public uint dwWeekPlanNo; 
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = Hikvision.CHCNetSDK.MAX_HOLIDAY_GROUP_NUM, ArraySubType = UnmanagedType.U4)]
            public uint[] dwHolidayGroupNo; 
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            public byte[] byRes2;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_DOOR_STATUS_PLAN
        {
            public uint dwSize;
            public uint dwTemplateNo; 
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
            public byte[] byRes;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_CARD_READER_PLAN
        {
            public uint dwSize;
            public uint dwTemplateNo;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
            public byte[] byRes;
        }

        public const int ACS_PARAM_DOOR_STATUS_WEEK_PLAN = 0x00000001; 
        public const int ACS_PARAM_VERIFY_WEEK_PALN = 0x00000002;     
        public const int ACS_PARAM_CARD_RIGHT_WEEK_PLAN = 0x00000004;  
        public const int ACS_PARAM_DOOR_STATUS_HOLIDAY_PLAN = 0x00000008; 
        public const int ACS_PARAM_VERIFY_HOLIDAY_PALN = 0x00000010;  
        public const int ACS_PARAM_CARD_RIGHT_HOLIDAY_PLAN = 0x00000020;  
        public const int ACS_PARAM_DOOR_STATUS_HOLIDAY_GROUP = 0x00000040; 
        public const int ACS_PARAM_VERIFY_HOLIDAY_GROUP = 0x00000080;      
        public const int ACS_PARAM_CARD_RIGHT_HOLIDAY_GROUP = 0x00000100;  
        public const int ACS_PARAM_DOOR_STATUS_PLAN_TEMPLATE = 0x00000200; 
        public const int ACS_PARAM_VERIFY_PALN_TEMPLATE = 0x00000400;      
        public const int ACS_PARAM_CARD_RIGHT_PALN_TEMPLATE = 0x00000800;  
        public const int ACS_PARAM_CARD = 0x00001000;                       
        public const int ACS_PARAM_GROUP = 0x00002000;                    
        public const int ACS_PARAM_ANTI_SNEAK_CFG = 0x00004000;            
        public const int ACS_PAPAM_EVENT_CARD_LINKAGE = 0x00008000;        
        public const int ACS_PAPAM_CARD_PASSWD_CFG = 0x00010000;            

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_ACS_PARAM_TYPE
        {
            public uint dwSize;
            public uint dwParamType;            
     
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            public byte[] byRes;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_CARD_USER_INFO_CFG
        {
            public uint dwSize;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = NAME_LEN)]
            public byte[] byUsername;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
            public byte[] byRes2;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_VIDEOEFFECT
        {
            public byte byBrightnessLevel;
            public byte byContrastLevel;
            public byte bySharpnessLevel;
            public byte bySaturationLevel; 
            public byte byHueLevel;
            public byte byEnableFunc;
            public byte byLightInhibitLevel; 
            public byte byGrayLevel;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_GAIN
        {
            public byte byGainLevel;
            public byte byGainUserSet;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] byRes;
            public uint dwMaxGainValue;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_WHITEBALANCE
        {
            public byte byWhiteBalanceMode;
            public byte byWhiteBalanceModeRGain;
            public byte byWhiteBalanceModeBGain;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public byte[] byRes;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_EXPOSURE
        {
            public byte byExposureMode;
            public byte byAutoApertureLevel;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] byRes;
            public uint dwVideoExposureSet;
            public uint dwExposureUserSet;
            public uint dwRes;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_GAMMACORRECT
        {
            public byte byGammaCorrectionEnabled; /*0 dsibale  1 enable*/
            public byte byGammaCorrectionLevel; /*0-100*/
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public byte[] byRes;
        }
 
        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_WDR
        {
            public byte byWDREnabled;
            public byte byWDRLevel1;
            public byte byWDRLevel2;
            public byte byWDRContrastLevel;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public byte[] byRes;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_DAYNIGHT
        {
            public byte byDayNightFilterType;
            public byte bySwitchScheduleEnabled;
           
            public byte byBeginTime;
            public byte byEndTime;
            
            public byte byDayToNightFilterLevel;
            public byte byNightToDayFilterLevel;
            public byte byDayNightFilterTime;
           
            public byte byBeginTimeMin;
            public byte byBeginTimeSec;
            public byte byEndTimeMin;
            public byte byEndTimeSec;
            
            public byte byAlarmTrigState;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_BACKLIGHT
        {
            public byte byBacklightMode;
            public byte byBacklightLevel;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] byRes1;
            public uint dwPositionX1;
            public uint dwPositionY1;
            public uint dwPositionX2;
            public uint dwPositionY2;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] byRes2;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_NOISEREMOVE
        {
            public byte byDigitalNoiseRemoveEnable;
            public byte byDigitalNoiseRemoveLevel;
            public byte bySpectralLevel;
            public byte byTemporalLevel;
            public byte byDigitalNoiseRemove2DEnable;
            public byte byDigitalNoiseRemove2DLevel;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] byRes;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_CMOSMODECFG
        {
            public byte byCaptureMod;
            public byte byBrightnessGate;
            public byte byCaptureGain1;
            public byte byCaptureGain2;
            public uint dwCaptureShutterSpeed1;
            public uint dwCaptureShutterSpeed2;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] byRes;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_DEFOGCFG
        {
            public byte byMode;
            public byte byLevel;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public byte[] byRes;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_ELECTRONICSTABILIZATION
        {
            public byte byEnable;
            public byte byLevel;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public byte[] byRes;
        }
 
        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_CORRIDOR_MODE_CCD
        {
            public byte byEnableCorridorMode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 11)]
            public byte[] byRes;
        }
        
        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_SMARTIR_PARAM
        {
            public byte byMode;
            public byte byIRDistance;
            public byte byShortIRDistance;
            public byte byInt32IRDistance;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_PIRIS_PARAM
        {
            public byte byMode;
            public byte byPIrisAperture;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public byte[] byRes;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_LASER_PARAM_CFG
        {
            //Length = 16
            public byte byControlMode; 
            public byte bySensitivity;
            public byte byTriggerMode; 
            public byte byBrightness;
            public byte byAngle;
            public byte byLimitBrightness;
            public byte byEnabled;
            public byte byIllumination;
            public byte byLightAngle;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
            public byte[] byRes;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_FFC_PARAM
        {
            public byte byMode;
            
            public byte byRes1;
            public ushort wCompensateTime;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] byRes2;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_DDE_PARAM
        {
            public byte byMode;
            public byte byNormalLevel;
            public byte byExpertLevel;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public byte[] byRes;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_AGC_PARAM
        {
            public byte bySceneType;
            public byte byLightLevel;
            public byte byGainLevel; 
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public byte[] byRes;
        }
        
        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_SNAP_CAMERAPARAMCFG
        {
            public byte byWDRMode;
            public byte byWDRType;
            public byte byWDRLevel;
            public byte byRes1;
            public NET_DVR_TIME_EX struStartTime;
            public NET_DVR_TIME_EX struEndTime;
            public byte byDayNightBrightness;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 43)]
            public byte[] byRes;
        }
       
        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_OPTICAL_DEHAZE
        {
            public byte byEnable;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
            public byte[] byRes;
        }

        public const int NET_DVR_GET_ISP_CAMERAPARAMCFG = 3255;
        public const int NET_DVR_SET_ISP_CAMERAPARAMCFG = 3256;
        public const int NET_DVR_GET_CCDPARAMCFG_EX = 3368;
        public const int NET_DVR_SET_CCDPARAMCFG_EX = 3369; 

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_CAMERAPARAMCFG_EX
        {
            public uint dwsize;
            public NET_DVR_VIDEOEFFECT struVideoEffect;
            public NET_DVR_GAIN struGain;
            public NET_DVR_WHITEBALANCE struWhiteBalance;
            public NET_DVR_EXPOSURE struExposure;
            public NET_DVR_GAMMACORRECT struGammaCorrect;
            public NET_DVR_WDR struWdr;
            public NET_DVR_DAYNIGHT struDayNight;
            public NET_DVR_BACKLIGHT struBackLight;
            public NET_DVR_NOISEREMOVE struNoiseRemove;
            public byte byPowerLineFrequencyMode;
            public byte byIrisMode; 
            public byte byMirror;
            public byte byDigitalZoom;
            public byte byDeadPixelDetect;
            public byte byBlackPwl;
            public byte byEptzGate;
            public byte byLocalOutputGate; 
         
            public byte byCoderOutputMode;
            public byte byLineCoding; 
            public byte byDimmerMode; 
            public byte byPaletteMode; 
            public byte byEnhancedMode; 
            public byte byDynamicContrastEN;
            public byte byDynamicContrast;
            public byte byJPEGQuality;
            public NET_DVR_CMOSMODECFG struCmosModeCfg;
            public byte byFilterSwitch; 
            public byte byFocusSpeed; 
            public byte byAutoCompensationInterval;
            public byte bySceneMode;  
            public NET_DVR_DEFOGCFG struDefogCfg;
            public NET_DVR_ELECTRONICSTABILIZATION struElectronicStabilization;
            public NET_DVR_CORRIDOR_MODE_CCD struCorridorMode;
            public byte byExposureSegmentEnable;
            public byte byBrightCompensate;

            public byte byCaptureModeN;
            public byte byCaptureModeP;
            public NET_DVR_SMARTIR_PARAM struSmartIRParam;
            public NET_DVR_PIRIS_PARAM struPIrisParam;
            
            public NET_DVR_LASER_PARAM_CFG struLaserParam;
            public NET_DVR_FFC_PARAM struFFCParam;
            public NET_DVR_DDE_PARAM struDDEParam;
            public NET_DVR_AGC_PARAM struAGCParam;
            public byte byLensDistortionCorrection;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] byRes1;
            public NET_DVR_SNAP_CAMERAPARAMCFG struSnapCCD;
            public NET_DVR_OPTICAL_DEHAZE struOpticalDehaze;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 180)]
            public byte[] byRes2;
        }

        public const int NET_DVR_GET_FOCUSMODECFG = 3305;
        public const int NET_DVR_SET_FOCUSMODECFG = 3306; 

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_FOCUSMODE_CFG
        {
            public uint dwSize;
            public byte byFocusMode;
            public byte byAutoFocusMode;
            public uint wMinFocusDistance; 
            public byte byZoomSpeedLevel; 
            public byte byFocusSpeedLevel; 
            public byte byOpticalZoom;  
            public byte byDigtitalZoom; 
            public float fOpticalZoomLevel;
            public uint dwFocusPos;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 56)]
            public byte[] byRes;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_ATMFINDINFO
        {
            public byte byTransactionType;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] byRes;
            public uint dwTransationAmount;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_SPECIAL_FINDINFO_UNION
        {
            //[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            //public byte[] byLenth;
            public NET_DVR_ATMFINDINFO struATMFindInfo;
        }

        public const int CARDNUM_LEN_OUT = 32;
        public const int GUID_LEN = 16;

        //[DllImport(@"HCNetSDK.dll")]
        //public static extern bool NET_DVR_GetDiskList(int lUserID, )

        [DllImport(@"HCNetSDK.dll")]
        public static extern Int32 NET_DVR_FindFile_V40(int lUserID, ref NET_DVR_FILECOND_V40 pFindCond);

        [DllImport(@"HCNetSDK.dll")]
        public static extern Int32 NET_DVR_FindNextFile_V40(int lFindHandle, ref NET_DVR_FINDDATA_V40 lpFindData);

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_FILECOND_V40
        {
            public int lChannel;
            public uint dwFileType;
            public uint dwIsLocked;
            public uint dwUseCardNo;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = CARDNUM_LEN_OUT)]
            public byte[] sCardNumber;
            public NET_DVR_TIME struStartTime;
            public NET_DVR_TIME struStopTime;
            public byte byDrawFrame; 
            public byte byFindType; 
            public byte byQuickSearch;
            public byte bySpecialFindInfoType; 
            public uint dwVolumeNum;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = GUID_LEN)]
            public byte[] byWorkingDeviceGUID;
            public NET_DVR_SPECIAL_FINDINFO_UNION uSpecialFindInfo; 
            public byte byStreamType;
            public byte byAudioFile;    
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 30)]
            public byte[] byRes2;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_FINDDATA_V40
        {
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 100)]
            public string sFileName;
            public NET_DVR_TIME struStartTime;
            public NET_DVR_TIME struStopTime;
            public uint dwFileSize;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string sCardNum;
            public byte byLocked;
            public byte byFileType;  
            
            public byte byQuickSearch;
            public byte byRes;
            public uint dwFileIndex;
            public byte byStreamType;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 127)]
            public byte[] byRes1;
        }

        public const int DEV_TYPE_NAME_LEN = 24;

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_DEVICECFG_V40
        {
            public uint dwSize;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = NAME_LEN)]
            public byte[] sDVRName;
            public uint dwDVRID;
            public uint dwRecycleRecord;
           
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = SERIALNO_LEN)]
            public byte[] sSerialNumber;
            public uint dwSoftwareVersion;
            public uint dwSoftwareBuildDate;
            public uint dwDSPSoftwareVersion;
            public uint dwDSPSoftwareBuildDate;
            public uint dwPanelVersion;
            public uint dwHardwareVersion;
            public byte byAlarmInPortNum;
            public byte byAlarmOutPortNum;
            public byte byRS232Num;
            public byte byRS485Num;
            public byte byNetworkPortNum;
            public byte byDiskCtrlNum;
            public byte byDiskNum;
            public byte byDVRType;
            public byte byChanNum;
            public byte byStartChan;
            public byte byDecordChans;
            public byte byVGANum;
            public byte byUSBNum;
            public byte byAuxoutNum;
            public byte byAudioNum;
            public byte byIPChanNum;
            public byte byZeroChanNum;
            public byte bySupport;

            public byte byEsataUseage; 
            public byte byIPCPlug;  	
            public byte byStorageMode;  
            public byte bySupport1; 
            
            public ushort wDevType;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = DEV_TYPE_NAME_LEN)]
            public byte[] byDevTypeName;
            public byte bySupport2; 

            public byte byAnalogAlarmInPortNum; 
            public byte byStartAlarmInNo;   
            public byte byStartAlarmOutNo; 
            public byte byStartIPAlarmInNo;  
            public byte byStartIPAlarmOutNo; 
            public byte byHighIPChanNum; 
            public byte byEnableRemotePowerOn;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public byte[] byRes2;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_DEVICEINFO_V40
        {
            public NET_DVR_DEVICEINFO_V30 struDeviceV30;
            public byte bySupportLock;
            public byte byRetryLoginTime;
            public byte byPasswordLevel; 
            public byte byRes1;
            public uint dwSurplusLockTime;
            public byte byCharEncodeType;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 255)]
            public byte[] byRes2;
        }

        public const int NET_DVR_DEV_ADDRESS_MAX_LEN = 129;
        public const int NET_DVR_LOGIN_USERNAME_MAX_LEN = 64;
        public const int NET_DVR_LOGIN_PASSWD_MAX_LEN = 64;

        public delegate void LOGINRESULTCALLBACK(Int32 lUserID, UInt32 dwResult, IntPtr lpDeviceInfo, IntPtr pUser);

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_USER_LOGIN_INFO
        {
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = NET_DVR_DEV_ADDRESS_MAX_LEN)]
            public string sDeviceAddress;
            public byte byRes1;
            public ushort wPort;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = NET_DVR_LOGIN_USERNAME_MAX_LEN)]
            public string sUserName;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = NET_DVR_LOGIN_PASSWD_MAX_LEN)]
            public string sPassword;
            public LOGINRESULTCALLBACK cbLoginResult;
            public IntPtr pUser;
            public bool bUseAsynLogin;
            public byte byUseUTCTime;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 127)]
            public byte[] byRes2;
        }

        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_Login_V40(ref NET_DVR_USER_LOGIN_INFO pLoginInfo, ref NET_DVR_DEVICEINFO_V40 lpDeviceInfo);

        const int XML_ABILITY_OUT_LEN = 3 * 1024 * 1024;

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_STD_ABILITY
        {
            public IntPtr lpCondBuffer;
            public uint dwCondSize;
            public IntPtr lpOutBuffer;
            public uint dwOutSize;
            public IntPtr lpStatusBuffer;
            public uint dwStatusSize;
            public uint dwRetSize;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            public byte[] byRes;
        }

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetSTDAbility(int lUserID, uint dwAbilityType, IntPtr lpAbilityParam);

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_XML_CONFIG_INPUT
        {
            public uint dwSize;
            public IntPtr lpRequestUrl;
            public uint dwRequestUrlLen;
            public IntPtr lpInBuffer;
            public uint dwInBufferSize;
            public uint dwRecvTimeOut;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            public byte[] byRes;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_XML_CONFIG_OUTPUT
        {
            public uint dwSize;
            public IntPtr lpOutBuffer;
            public uint dwOutBufferSize;
            public uint dwReturnedXMLSize;
            public IntPtr lpStatusBuffer;
            public uint dwStatusSize;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            public byte[] byRes;
        }

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_STDXMLConfig(int lUserID, IntPtr lpInputParam, IntPtr lpOutputParam);
        
        [DllImport("kernel32.dll")]
        public static extern int WinExec(string exeName, int operType);

        public struct NET_DVR_STD_CONFIG
        {
            public IntPtr lpCondBuffer;
            public uint dwCondSize;
            public IntPtr lpInBuffer;
            public uint dwInSize;
            public IntPtr lpOutBuffer;
            public uint dwOutSize;
            public IntPtr lpStatusBuffer;
            public uint dwStatusSize;
            public IntPtr lpXmlBuffer;
            public uint dwXmlSize;
            public byte byDataType;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] byRes1;           
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public byte[] byRes2;
        }

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetSTDConfig(int lUserID, uint dwCommand, IntPtr lpConfigParam);

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetSTDConfig(int lUserID, uint dwCommand, IntPtr lpConfigParam);

        public const int ALARM_INFO_T = 0;
        public const int OPERATION_SUCC_T = 1;
        public const int OPERATION_FAIL_T = 2;
        public const int PLAY_SUCC_T = 3;
        public const int PLAY_FAIL_T = 4;

        public const int MAX_AUDIO_V40 = 8;
        public const int NET_DVR_USER_LOCKED = 153;

        public enum DEMO_CHANNEL_TYPE
        {
            DEMO_CHANNEL_TYPE_INVALID = -1,
            DEMO_CHANNEL_TYPE_ANALOG = 0,
            DEMO_CHANNEL_TYPE_IP = 1,
            DEMO_CHANNEL_TYPE_MIRROR = 2
        }

        //device index info
        [StructLayout(LayoutKind.Sequential)]
        public struct STRU_CHANNEL_INFO
        {
            public int iDeviceIndex;  	//device index
            public int iChanIndex;  	//channel index
            public DEMO_CHANNEL_TYPE iChanType;
            public int iChannelNO;         //channel NO.       
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 100)]
            public string chChanName;         //channel name
            public uint dwProtocol;  	//network protocol
            public uint dwStreamType;
            public uint dwLinkMode;
            public bool bPassbackRecord; 
            public uint dwPreviewMode; 
            public int iPicResolution;    //resolution
            public int iPicQuality;    //image quality
            public Int32 lRealHandle;          //preview handle
            public bool bLocalManualRec;     //manual record
            public bool bAlarm;    //alarm
            public bool bEnable;  	//enable
            public uint dwImageType;  //channel status icon
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string chAccessChanIP;
            public uint nPreviewProtocolType;
            public IntPtr pNext;

            public void init()
            {
                iDeviceIndex = -1;
                iChanIndex = -1;
                iChannelNO = -1;
                iChanType = DEMO_CHANNEL_TYPE.DEMO_CHANNEL_TYPE_INVALID;
                chChanName = null;
                dwProtocol = 0;

                dwStreamType = 0;
                dwLinkMode = 0;

                iPicResolution = 0;
                iPicQuality = 2;

                lRealHandle = -1;
                bLocalManualRec = false;
                bAlarm = false;
                bEnable = false;
                dwImageType = CHAN_ORIGINAL;
                chAccessChanIP = null;
                pNext = IntPtr.Zero;
                dwPreviewMode = 0;
                bPassbackRecord = false;
                nPreviewProtocolType = 0;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_STREAM_MODE
        {
            public byte byGetStreamType;

            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
            public NET_DVR_GET_STREAM_UNION uGetStream;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_IPSERVER_STREAM
        {
            public byte byEnable;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
            public NET_DVR_IPADDR struIPServer;
            public ushort wPort;
            public ushort wDvrNameLen;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byDVRName;
            public ushort wDVRSerialLen;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = SERIALNO_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byDVRSerialNumber;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byUserName;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = PASSWD_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byPassWord; 
            public byte byChannel;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 11, ArraySubType = UnmanagedType.I1)]
            public ushort[] byRes2;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_DDNS_STREAM_CFG
        {
            public byte byEnable;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
            public NET_DVR_IPADDR struStreamServer;
            public ushort wStreamServerPort;
            public byte byStreamServerTransmitType;
            public byte byRes2;
            public NET_DVR_IPADDR struIPServer;
            public ushort wIPServerPort;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes3;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sDVRName;
            public ushort wDVRNameLen;
            public ushort wDVRSerialLen;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = SERIALNO_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sDVRSerialNumber;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sUserName;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = PASSWD_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] sPassWord;
            public ushort wDVRPort;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes4;
            public byte byChannel;
            public byte byTransProtocol;
            public byte byTransMode; 
            public byte byFactoryType; 
        }

        public const int URL_LEN = 240;

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_PU_STREAM_URL
        {
            public byte byEnable;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = URL_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] strURL;
            public byte byTransPortocol; 
            public ushort wIPID;  
            public byte byChannel; 
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
            public byte[] byRes;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_HKDDNS_STREAM
        {
            public byte byEnable;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 64, ArraySubType = UnmanagedType.I1)]
            public byte[] byDDNSDomain;   
            public ushort wPort;     
            public ushort wAliasLen; 
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byAlias;
            public ushort wDVRSerialLen;  
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = SERIALNO_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byDVRSerialNumber;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byUserName;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = PASSWD_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byPassWord;
            public byte byChannel;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 11, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_IPCHANINFO_V40
        {
            public byte byEnable;
            public byte byRes1;
            public ushort wIPID;
            public uint dwChannel;
            public byte byTransProtocol;
            public byte byTransMode;
            public byte byFactoryType;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 241, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_GET_STREAM_UNION
        {
            public NET_DVR_IPCHANINFO struChanInfo;
            public NET_DVR_IPSERVER_STREAM struIPServerStream;
            public NET_DVR_PU_STREAM_CFG struPUStream;
            public NET_DVR_DDNS_STREAM_CFG struDDNSStream;
            public NET_DVR_PU_STREAM_URL struStreamUrl;
            public NET_DVR_HKDDNS_STREAM struHkDDNSStream;
            public NET_DVR_IPCHANINFO_V40 struIPChan;
        }

        public const int MAX_IP_DEVICE_V40 = 64;
        
        public const int NET_DVR_GET_IPPARACFG_V40 = 1062;
        public const int NET_DVR_SET_IPPARACFG_V40 = 1063;

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_IPPARACFG_V40
        {
            public uint dwSize;
            public uint dwGroupNum; 
            public uint dwAChanNum;
            public uint dwDChanNum;
            public uint dwStartDChan; 
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM_V30)]
            public byte[] byAnalogChanEnable;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_IP_DEVICE_V40)]
            public NET_DVR_IPDEVINFO_V31[] struIPDevInfo;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM_V30)]
            public NET_DVR_STREAM_MODE[] struStreamMode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] byRes2;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_IPALARMININFO_V40
        {
            public uint dwIPID;
            public uint dwAlarmIn;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            public byte[] byRes;
        }

        public const int MAX_IP_ALARMIN_V40 = 4096;

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_IPALARMINCFG_V40
        {
            public uint dwSize; 
            public uint dwCurIPAlarmInNum;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_IP_ALARMIN_V40)]
            public NET_DVR_IPALARMININFO_V40[] struIPAlarmInInfo;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
            public byte[] byRes;
        }

        [StructLayout(LayoutKind.Sequential)]

        public struct NET_DVR_IPALARMOUTINFO_V40
        {
            public uint dwIPID;
            public uint dwAlarmOut; 
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            public byte[] byRes;
        }
        public const int NET_DVR_CONTROL_PTZ_PATTERN = 3313;
        public const int DELETE_CRUISE = 45;
        public const int DELETE_ALL_CRUISE = 46;
        public const int STOP_CRUISE = 44;

        public struct NET_DVR_PTZ_PATTERN
        {
            public uint dwSize;
            public uint dwChannel;
            public uint dwPatternCmd;
            public uint dwPatternID;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 64, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }
        
        public struct NET_DVR_AUXILIARY_DEV_UPGRADE_PARAM
        {
            public uint dwSize;
            public uint dwDevNo;
            public byte byDevType;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 131, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        public enum ENUM_UPGRADE_TYPE
        {
            ENUM_UPGRADE_DVR = 0,
            ENUM_UPGRADE_ADAPTER = 1,
            ENUM_UPGRADE_VCALIB = 2,
            ENUM_UPGRADE_OPTICAL = 3,
            ENUM_UPGRADE_ACS = 4,
            ENUM_UPGRADE_AUXILIARY_DEV = 5,
        }
        public const int MAX_IP_ALARMOUT_V40 = 4096;

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_IPALARMOUTCFG_V40
        {
            public uint dwSize;
            public uint dwCurIPAlarmOutNum;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_IP_ALARMOUT_V40)]
            public NET_DVR_IPALARMOUTINFO_V40[] struIPAlarmOutInfo;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
            public byte[] byRes;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct PASSIVEDECODE_CHANINFO
        {
            public Int32 lPassiveHandle;
            public Int32 lRealHandle;
            public Int32 lUserID;
            public Int32 lSel;
            public IntPtr hFileThread;
            public IntPtr hFileHandle;
            public IntPtr hExitThread;
            public IntPtr hThreadExit;
            public string strRecordFilePath;

            public void init()
            {
                lPassiveHandle = -1;
                lRealHandle = -1;
                lUserID = -1;
                lSel = -1;
                hFileThread = IntPtr.Zero;
                hFileHandle = IntPtr.Zero;
                hExitThread = IntPtr.Zero;
                hThreadExit = IntPtr.Zero;
                strRecordFilePath = null;
            }
        }

        public const int NET_DVR_GET_DEVICECFG_V40 = 1100;
        public const int NET_DVR_SET_DEVICECFG_V40 = 1101;

        public const int MAX_DEVICES = 512;//max device number
        //bmp status
        public const int TREE_ALL = 0;//device list	
        public const int DEVICE_LOGOUT = 1;//device not log in
        public const int DEVICE_LOGIN = 2;//device login
        public const int DEVICE_FORTIFY = 3;//on guard
        public const int DEVICE_ALARM = 4;//alarm on device
        public const int DEVICE_FORTIFY_ALARM = 5;//onguard & alarm on device
        public const int CHAN_ORIGINAL = 6;//no preview, no record
        public const int CHAN_PLAY = 7;   //preview
        public const int CHAN_RECORD = 8;   //record
        public const int CHAN_PLAY_RECORD = 9;   //preview and record

        public const int CHAN_ALARM = 10;   //no preview, no record, only alarm
        public const int CHAN_PLAY_ALARM = 11;   //review, no record, with alarm info
        public const int CHAN_PLAY_RECORD_ALARM = 12;   //preview & record & alarm
        public const int CHAN_OFF_LINE = 13;	 //channel off-line

        [StructLayout(LayoutKind.Sequential)]
        public struct PREVIEW_IFNO
        {
            public int iDeviceIndex;  	//device index
            public int iChanIndex;  	//channel index
            public byte PanelNo;
            public int lRealHandle;
            public IntPtr hPlayWnd;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct STRU_DEVICE_INFO
        {
            public int iDeviceIndex;  	//device index
            public Int32 lLoginID;    //ID
            public uint dwDevSoftVer;  	//erserved
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 100)]
            public string chLocalNodeName;      //local device node
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 100)]
            public string chDeviceName;         //device name
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 130)]
            public string chDeviceIP;           //device IP: IP,pppoe address, or network gate address, etc
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 130)]
            public string chDeviceIPInFileName; //if chDeviceIP includes ':'(IPV6),change it to '.', for '.'is not allowed in window's file name
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = NAME_LEN)]
            public string chLoginUserName;      //login user name
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = NAME_LEN)]
            public string chLoginPwd;           //password
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 130)]
            public string chDeviceMultiIP;      //multi-cast group address
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 50)]
            public string chSerialNumber;       //SN
            public int iDeviceChanNum;      //channel numder  (analog + ip)
            public int iStartChan;    //start channel number
            public int iStartDChan;    //start IP channel number
            public int iDeviceType;  	//device type
            public int iDiskNum;    //HD number
            public Int32 lDevicePort;  	//port number
            public int iAlarmInNum;  	//alarm in number
            public int iAlarmOutNum;  	//alarm out number
            public int iAudioNum;    //voice talk number
            public int iAnalogChanNum;  	//analog channel number
            public int iIPChanNum;    //IP channel number
            public int iGroupNO;               //IP Group NO.
            public bool bCycle;    	//if this device is on cycle recording
            public bool bPlayDevice;  	//will be added later
            public bool bVoiceTalk;    //on voice talkor not
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_AUDIO_V40)]
            public Int32[] lAudioHandle;         //voicebroadcast handle
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_AUDIO_V40)]
            public bool[] bCheckBroadcast;      //Add to broad cast group
            public Int32 lFortifyHandle;  	//on guard handle
            public bool bAlarm;    	//whether there is alarm
            public int iDeviceLoginType;  //register mode  0 - fix IP   1- IPSERVER mode    2 -  domain name 
            public uint dwImageType;  	//device status icon
            public bool bIPRet;    	//support IP conection
            public int iEnableChanNum;  	//valid channel number
            public bool bDVRLocalAllRec;  //local recording
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_AUDIO_V40)]
            public Int32[] lVoiceCom;            //voice transmit
            public Int32 lFirstEnableChanIndex;  	//first enabled channel index
            public Int32 lTranHandle;    //232 transparent channel handle
            public byte byZeroChanNum;  //Zero channel number
            public byte byMainProto;  	//main stream protocol type 0-Private, 1-rtp/tcp, 2-rtp/rtsp
            public byte bySubProto;    //sub stream protocol type 0-Private, 1-rtp/tcp, 2-rtp/rtsp
            public byte bySupport;             //ability
            public byte byStartDTalkChan;
            public byte byAudioInputChanNum;
            public byte byStartAudioInputChanNo;
            public byte byLanguageType;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = Hikvision.CHCNetSDK.MAX_CHANNUM_V40)]
            public STRU_CHANNEL_INFO[] pStruChanInfo; //channel structure
            public NET_DVR_IPPARACFG_V40[] pStruIPParaCfgV40;    //IP channel parameters
            public NET_DVR_IPALARMINCFG struAlarmInCfg;
            public NET_DVR_IPALARMINCFG_V40 pStruIPAlarmInCfgV40;  // IP alarm In parameters
            public NET_DVR_IPALARMOUTCFG_V40 pStruIPAlarmOutCfgV40; // IP alarm Out parameters
            public NET_DVR_IPALARMOUTCFG struAlarmOutCfg;
            public IntPtr pNext;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public STRU_CHANNEL_INFO[] struZeroChan;
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string sSecretKey;
            public int iAudioEncType;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
            public PASSIVEDECODE_CHANINFO[] struPassiveDecode;
            public byte bySupport1;
            

            public byte bySupport2;
            

            public byte byStartIPAlarmOutNo;
            public byte byMirrorChanNum;
            public ushort wStartMirrorChanNo;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public STRU_CHANNEL_INFO[] struMirrorChan;
            public byte bySupport5;
            public byte bySupport7;
            public byte byCharaterEncodeType;
            public bool bInit;
            public byte byPanelNo;

            public void Init()
            {
                iGroupNO ++;
                iDeviceIndex ++;
                lLoginID ++;
                dwDevSoftVer = 0;
                chLocalNodeName = null;
                chDeviceName = null;
                chDeviceIP = null;
                chDeviceIPInFileName = null;
                //chDevNetCard1IP[0]	= '\0';
                chLoginUserName = null;
                chLoginPwd = null;
                chDeviceMultiIP = null;
                chSerialNumber = null;
                iDeviceChanNum ++;
                iStartChan = 0;
                iDeviceType = 0;
                iDiskNum = 0;
                lDevicePort = 8000;
                iAlarmInNum = 0;
                iAlarmOutNum = 0;
                iAnalogChanNum = 0;
                iIPChanNum = 0;
                byAudioInputChanNum = 0;
                byStartAudioInputChanNo = 0;
                bCycle = false;
                bPlayDevice = false;
                bVoiceTalk = false;
                lAudioHandle = new Int32[MAX_AUDIO_V40];
                bCheckBroadcast = new bool[MAX_AUDIO_V40];
                lFortifyHandle = -1;
                bAlarm = false;
                iDeviceLoginType = 0;
                dwImageType = DEVICE_LOGOUT;
                bIPRet = false;
                pNext = IntPtr.Zero;
                lVoiceCom = new Int32[MAX_AUDIO_V40];
                for (int i = 0; i < lVoiceCom.Length; i++)
                {
                    lVoiceCom[i] = -1;
                }
                lFirstEnableChanIndex = 0;
                lTranHandle = -1;
                byZeroChanNum = 0;
                lAudioHandle[0] = -1;
                lAudioHandle[1] = -1;
                struAlarmInCfg = new NET_DVR_IPALARMINCFG();
                struAlarmOutCfg = new NET_DVR_IPALARMOUTCFG();
                sSecretKey = "StreamNotEncrypt";
                iAudioEncType = 0;
                bySupport1 = 0;
                bySupport2 = 0;
                bySupport5 = 0;
                bySupport7 = 0;
                byStartDTalkChan = 0;
                byLanguageType = 0;
                byCharaterEncodeType = 0;
                bInit = true;
                byPanelNo = 4;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_SERIALSTART_V40
        {
            public uint dwSize;
            public uint dwSerialType;
            public byte bySerialNum;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 255, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        public delegate void CallBackSerialData(int lSerialHandle, int lCHannel, IntPtr pRecvDataBuffer, uint dwBufSize, IntPtr pUser);
        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_SerialStart_V40(int lUserID, IntPtr lpInBuffer, int dwInBufferSize, CallBackSerialData fSerialDataCallBack, IntPtr pUser);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetForegroundWindow();

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_PlayBackControl_V40(int lPlayHandle, int dwControlCode,
            IntPtr lpInBuffer, uint dwInLen, IntPtr lpOutBuffer, IntPtr lpOutLen);
        
        public delegate bool CHARENCODECONVERT(string strInput, uint dwInputLen, uint dwInEncodeType, string strOutput, uint dwOutputLen, uint dwOutEncodeType);

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_LOCAL_BYTE_ENCODE_CONVERT
        {
            CHARENCODECONVERT fnCharConvertCallBack;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 256, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        public enum CHAR_ENCODE_TYPE
        {
            ENUM_MEM_CHAR_ENCODE_ERR = -1,        //Error   
            ENUM_MEM_CHAR_ENCODE_NO = 0,          //Don't know.
            ENUM_MEM_CHAR_ENCODE_CN = 1,          //EUC-CN, GB2312
            ENUM_MEM_CHAR_ENCODE_GBK = 2,         //GBK
            ENUM_MEM_CHAR_ENCODE_BIG5 = 3,        //BIG5
            ENUM_MEM_CHAR_ENCODE_JP = 4,          //JISX0208-1, EUC-JP
            ENUM_MEM_CHAR_ENCODE_KR = 5,          //EUC-KR
            ENUM_MEM_CHAR_ENCODE_UTF8 = 6,        //UTF-8
            ENUM_MEM_CHAR_ENCODE_ISO8859_1 = 7,   //ISO-8859-n: ENUM_MEM_CHAR_ENCODE_ISO8859_1 + n -1
        }

        [DllImport("kernel32.dll")]
        public static extern int MultiByteToWideChar(int CodePage, int dwFlags, string lpMultiByteStr, int cchMultiByte, [MarshalAs(UnmanagedType.LPWStr)]string lpWideCharStr, int cchWideChar);

        [DllImport("Kernel32.dll")]
        public static extern int WideCharToMultiByte(uint CodePage, uint dwFlags, [In, MarshalAs(UnmanagedType.LPWStr)]string lpWideCharStr, int cchWideChar,
        [Out, MarshalAs(UnmanagedType.LPStr)]StringBuilder lpMultiByteStr, int cbMultiByte, IntPtr lpDefaultChar, // Defined as IntPtr because in most cases is better to pass
        IntPtr lpUsedDefaultChar);

        public enum NET_SDK_LOCAL_CFG_TYPE
        {
            NET_SDK_LOCAL_CFG_TYPE_TCP_PORT_BIND = 0,
            NET_SDK_LOCAL_CFG_TYPE_UDP_PORT_BIND,
            NET_SDK_LOCAL_CFG_TYPE_MEM_POOL,
            NET_SDK_LOCAL_CFG_TYPE_MODULE_RECV_TIMEOUT,
            NET_SDK_LOCAL_CFG_TYPE_ABILITY_PARSE, 
            NET_SDK_LOCAL_CFG_TYPE_TALK_MODE, 
            NET_SDK_LOCAL_CFG_TYPE_PROTECT_KEY, 
            NET_SDK_LOCAL_CFG_TYPE_CFG_VERSION,
            NET_SDK_LOCAL_CFG_TYPE_RTSP_PARAMS,
            NET_SDK_LOCAL_CFG_TYPE_SIMXML_LOGIN,
            NET_SDK_LOCAL_CFG_TYPE_CHECK_DEV,
            NET_SDK_LOCAL_CFG_TYPE_SECURITY,
            NET_SDK_LOCAL_CFG_TYPE_EZVIZLIB_PATH,
            NET_SDK_LOCAL_CFG_TYPE_CHAR_ENCODE, 
            NET_SDK_LOCAL_CFG_TYPE_PROXYS 
        }

        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_SetSDKLocalCfg(NET_SDK_LOCAL_CFG_TYPE enumType, IntPtr lpInBuff);

        public const int CP_UTF8 = 65001;
        public const int CP_ACP = 0;
        
        public const int NET_DVR_GET_MEMU_OUTPUT_MODE = 155649;
        public const int NET_DVR_SET_MEMU_OUTPUT_MODE = 155650;

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_MENU_OUTPUT_MODE
        {
            public uint dwSize;
            public byte byMenuOutputMode;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 63, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        //default image parameter
        public const int DEFAULT_BRIGHTNESS = 6;		//default brightness
        public const int DEFAULT_CONTRAST = 6;			//default contrast
        public const int DEFAULT_SATURATION = 6;	    //default saturation
        public const int DEFAULT_HUE = 6;			    //default hue
        public const int DEFAULT_SHARPNESS = 6;			//default sharpness
        public const int DEFAULT_DENOISING = 6;			//default denoising
        public const int DEFAULT_VOLUME = 50;           //default volume
        public const int MAX_OUTPUTS = 512;

        [StructLayout(LayoutKind.Sequential)]
        public struct VIDEO_INFO
        {
            public uint m_iBrightness;				//1-10
            public uint m_iContrast;				//1-10
            public uint m_iSaturation;				//1-10
            public uint m_iHue;						//1-10
            public uint m_iSharpness;
            public uint m_iDenoising;
            public void Init()
            {
                m_iBrightness = DEFAULT_BRIGHTNESS;
                m_iContrast = DEFAULT_CONTRAST;
                m_iSaturation = DEFAULT_SATURATION;
                m_iHue = DEFAULT_HUE;
                m_iSharpness = DEFAULT_SHARPNESS;
                m_iDenoising = DEFAULT_DENOISING;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct LOCAL_RECORD_TIME
        {
            public ushort iStartHour;
            public ushort iStartMinute;
            public ushort iStopHour;
            public ushort iStopMinute;
            public ushort iStartTime;
            public ushort iStopTime;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct StruDomain
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 64, ArraySubType = UnmanagedType.I1)]
            public byte[] szDomain;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 80, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct StruAddrIP
        {
            public NET_DVR_IPADDR struIp;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct UnionServer
        {
            public StruDomain struDomain;
            public StruAddrIP stryAddrIP;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_SLAVECAMERA_CFG
        {
            public uint dwSize;
            public byte byAddressType;

            public ushort wPort;
            public byte byLoginStatus;
            public UnionServer unionServer;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = NAME_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] szUserName;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = PASSWD_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] szPassWord;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 128, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct STRU_LOCAL_PARAM
        {
            public bool bReconnect;                 //reconnect
            public bool bCyclePlay;				    //cycle play
            public int iCycleTime;				    //cycle time, default 20
            public bool bUseCard;					//hadrware decode
            public bool bNTSC;						//hardware decode mode,FALSE,PAL;TRUE,NTSC;default as pal
            public bool bAutoRecord;				//auto record;
            public bool bCycleRecord;				//cycle record
            public int iStartRecordDriver;		    //client record starting HD dirve
            public int iEndRecordDriver;			//client record stop HD drive
            public int iRecFileSize;				//record file size
            public int iRecordFileInterval;		    //record file interval
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 100)]
            public string chDownLoadPath;           //remote file download directory
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 100)]
            public string chPictureSavePath;		//image capture directory
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 100)]
            public string chRemoteCfgSavePath;	    //remote config file saving directory
            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 100)]
            public string chClientRecordPath;		//client record path
            public bool bAutoCheckDeviceTime;		//check time with device
            public Int32 lCheckDeviceTime;			//check time interval
            public int iAlarmDelayTime;			    //alarm delay time
            public int iAlarmListenPort;
            public bool bAutoSaveLog;				//auto save local log info
            public bool bAlarmInfo;					//display alarm info on log list
            public bool bSuccLog;				    //display log access on log list
            public bool bFailLog;					//display filaure operation on log list
            public bool bAllDiskFull;				//HD full

            //preview
            public bool bPlaying;					//on playing
            public bool bCycling;					//cycle playing
            public bool bPaused;					//cycle pause
            public bool bNextPage;				    //next page
            public bool bFrontPage;				    //previous page
            public bool bEnlarged;				    //enlarge image
            public bool bFullScreen;				//full screen
            public bool bMultiScreen;				//multi-split-window full screen
            public bool bNoDecode;					//soft decode or not
            public bool bPreviewBlock;				//preview block or not

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_OUTPUTS)]
            public VIDEO_INFO[] struVideoInfo;	            //video parameter
            public int iVolume;					    //volume
            public bool bBroadCast;					//voice broadcast
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 128)]
            public string chIPServerIP;
            public bool bOutputDebugString;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 28)]
            public LOCAL_RECORD_TIME[] struLocalRecordTime;
            public uint dwBFrameNum;//throw B frame number
            public uint nLogLevel;
            public bool bCycleWriteLog;
            public uint nTimeout;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public NET_DVR_SLAVECAMERA_CFG[] struSlaveCameraCfg;
            public bool bInit;
            public void Init()
            {
                bReconnect = true;
                bCyclePlay = false;
                iCycleTime = 20;
                bUseCard = false;
                bNTSC = false;
                bAutoRecord = false;
                bCycleRecord = false;
                iStartRecordDriver = 0;
                iEndRecordDriver = 0;
                iRecFileSize = 1;
                iRecordFileInterval = 60;
                chDownLoadPath = "C:\\DownLoad";
                chPictureSavePath = "C:\\Picture";
                chRemoteCfgSavePath = "C:\\SaveRemoteCfgFile";
                chClientRecordPath = "C:\\mpeg4record\\2008-04-30";
                chIPServerIP = "172.7.28.123";

                bAutoCheckDeviceTime = false;
                lCheckDeviceTime = 0;

                iAlarmDelayTime = 10;
                iAlarmListenPort = 7200;

                bAutoSaveLog = true;
                bAlarmInfo = true;
                bSuccLog = true;
                bFailLog = true;

                bAllDiskFull = false;
                bPlaying = false;
                bCycling = false;
                bPaused = false;
                bNextPage = false;
                bFrontPage = false;
                bEnlarged = false;
                bFullScreen = false;
                bMultiScreen = false;
                iVolume = DEFAULT_VOLUME;
                bBroadCast = false;
                bNoDecode = false;
                bPreviewBlock = true;
                bOutputDebugString = false;
                dwBFrameNum = 0;
                nLogLevel = 3;
                bCycleWriteLog = false;
                nTimeout = 5000;
                struVideoInfo = new VIDEO_INFO[MAX_OUTPUTS];
                for (int i = 0; i < MAX_OUTPUTS; i++)
                {
                    struVideoInfo[i].Init();
                }
                struLocalRecordTime = new LOCAL_RECORD_TIME[28];
                struSlaveCameraCfg = new NET_DVR_SLAVECAMERA_CFG[4];
                bInit = true;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_DVR_VOD_PARA
        {
            public uint dwSize;
            public NET_DVR_STREAM_INFO struIDInfo;
            public NET_DVR_TIME struBeginTime;
            public NET_DVR_TIME struEndTime;
            public IntPtr hWnd;
            public byte byDrawFrame;
            public byte byVolumeType;
            public byte byVolumeNum;
            public byte byStreamType;
            public uint dwFileIndex;
            public byte byAudioFile;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 23, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
            public void Init()
            {
                struBeginTime = new NET_DVR_TIME();
                struEndTime = new NET_DVR_TIME();
                struIDInfo = new NET_DVR_STREAM_INFO();
                struIDInfo.byID = new byte[STREAM_ID_LEN];
                hWnd = IntPtr.Zero;
            }
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_PLAYCOND
        {
            public uint dwChannel;
            public NET_DVR_TIME struStartTime;
            public NET_DVR_TIME struStopTime;
            public byte byDrawFrame;
            public byte byStreamType;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = Hikvision.CHCNetSDK.STREAM_ID_LEN, ArraySubType = UnmanagedType.I1)]
            public byte[] byStreamID;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 30, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        [DllImport(@"HCNetSDK.dll")]
        public static extern Int32 NET_DVR_PlayBackByTime_V40(uint lUserID, ref NET_DVR_VOD_PARA pVodPara);

        [DllImport(@"HCNetSDK.dll")]
        public static extern Int32 NET_DVR_PlayBackReverseByTime_V40(uint lUserID, IntPtr hWnd, ref Hikvision.CHCNetSDK.NET_DVR_PLAYCOND pPlayCond);

        public const int NET_DVR_GET_WORK_STATUS = 6189;

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_GETWORKSTATE_COND
        {
            public uint dwSize;
            public byte byFindHardByCond;
            public byte byFindChanByCond;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DISKNUM_V30, ArraySubType = UnmanagedType.U4)]
            public uint[] dwFindHardStatus;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM_V40, ArraySubType = UnmanagedType.U4)]
            public uint[] dwFindChanNo;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 64, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        public const int MAX_ALARMIN_V40 = 4128;
        public const int MAX_ALARMOUT_V40 = 4128;

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_WORKSTATE_V40
        {
            public uint dwChannel;
            public uint dwDeviceStatic;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DISKNUM_V30, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_DISKSTATE[] struHardDiskStatic;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM_V40, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_CHANNELSTATE_V30[] struChanStatic;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_ALARMIN_V40, ArraySubType = UnmanagedType.U4)]
            public uint[] dwHasAlarmInStatic;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_ALARMOUT_V40, ArraySubType = UnmanagedType.U4)]
            public uint[] dwHasAlarmOutStatic;
            public uint dwLocalDisplay;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_AUDIO_V30, ArraySubType = UnmanagedType.I1)]
            public byte[] byAudioInChanStatus;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 126, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        public const int COMM_THERMOMETRY_ALARM = 0x5210;
        public const int COMM_THERMOMETRY_DIFF_ALARM = 0x5211;

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_PTZ_INFO
        {
            public float fPan;
            public float fTilt;
            public float fZoom;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_VCA_POINT
        {
            public float fX;
            public float fY;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_VCA_POLYGON
        {
            public uint dwPointNum;
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = Hikvision.CHCNetSDK.VCA_MAX_POLYGON_POINT_NUM, ArraySubType = System.Runtime.InteropServices.UnmanagedType.Struct)]
            public NET_VCA_POINT[] struPos;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_THERMOMETRY_ALARM //unsafe
        {
            public uint dwSize;
            public uint dwChannel;
            public byte byRuleID;
            public byte byThermometryUnit;
            public ushort wPresetNo; 
            NET_PTZ_INFO struPtzInfo;
            public byte byAlarmLevel;
            public byte byAlarmType;
            public byte byAlarmRule;
            public byte byRuleCalibType;
            NET_VCA_POINT struPoint;
            NET_VCA_POLYGON struRegion;
            public float fRuleTemperature;
            public float fCurrTemperature;
            public uint dwPicLen;
            public uint dwThermalPicLen;
            public uint dwThermalInfoLen;
            /*Char* pPicBuff; 
            Char* pThermalPicBuff;
            Char* pThermalInfoBuff; */
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 68, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_THERMOMETRY_DIFF_ALARM //unsafe
        {
            public uint dwSize;
            public uint dwChannel;
            public byte byAlarmID1;
            public byte byAlarmID2;
            public ushort wPresetNo; 
            public byte byAlarmLevel;
            public byte byAlarmType;
            public byte byAlarmRule;
            public byte byRuleCalibType;
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = System.Runtime.InteropServices.UnmanagedType.Struct)]
            public NET_VCA_POINT[] struPoint;
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = System.Runtime.InteropServices.UnmanagedType.Struct)]
            public NET_VCA_POLYGON[] struRegion;
            float fRuleTemperatureDiff;
            float fCurTemperatureDiff;
            NET_PTZ_INFO struPtzInfo;
            public uint dwPicLen;
            public uint dwThermalPicLen;
            public uint dwThermalInfoLen;
            /*Char* pPicBuff; 
            Char* pThermalPicBuff;
            Char* pThermalInfoBuff; */
            public byte byThermometryUnit;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 63, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }
        
        public const int NET_DVR_GET_REALTIME_THERMOMETRY = 3629;     

        public struct NET_DVR_REALTIME_THERMOMETRY_COND
        {
            public uint dwSize;
            public uint dwChan;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 64, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        public struct NET_DVR_POINT_THERM_CFG
        {
            public float fTemperature;
            public NET_VCA_POINT struPoint;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 120, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        public struct NET_DVR_LINEPOLYGON_THERM_CFG
        {
            public float fMaxTemperature;
            public float fMinTemperature;
            public float fAverageTemperature;
            public float fTemperatureDiff;
            public NET_VCA_POLYGON struRegion;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        public struct NET_DVR_THERMOMETRY_UPLOAD
        {
            public uint dwSize;
            public uint dwRelativeTime;
            public uint dwAbsTime;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.I1)]
            public char[] szRuleName;
            public byte byRuleID;
            public byte byRuleCalibType;
            public ushort wPresetNo;
            public NET_DVR_POINT_THERM_CFG struPointThermCfg;
            public NET_DVR_LINEPOLYGON_THERM_CFG struLinePolygonThermCfg;
            public byte byThermometryUnit;
            public byte byDataType;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 126, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
            public void Init()
            {
                struPointThermCfg = new Hikvision.CHCNetSDK.NET_DVR_POINT_THERM_CFG();
                struLinePolygonThermCfg = new Hikvision.CHCNetSDK.NET_DVR_LINEPOLYGON_THERM_CFG();
            }
        }

        public const int NET_DVR_GET_FIGURE = 6640;

        public struct NET_DVR_GET_FIGURE_COND
        {
            public uint dwLength;
            public uint dwChannel;
            public NET_DVR_TIME_V30 struTimePoint;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 64, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        public struct NET_DVR_FIGURE_INFO
        {
            public uint dwPicLen;
            public IntPtr pPicBuf;
        }

        public struct NET_DVR_PTZ_MANUALTRACE
        {
            public uint dwSize;
            public uint dwChannel;
            public NET_VCA_POINT struPoint;
            public byte byTrackType;   
            public byte byLinkageType;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
            public NET_VCA_POINT struPointEnd;
            public NET_DVR_TIME_V30 struTime;
            public uint dwSerialNo;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 36, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
        }

        public struct NET_DVR_SMARTTRACKCFG
        {
            public uint dwSize;
            public byte byEnable;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
            public uint dwDuration;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 124, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
        }

        public const int NET_DVR_GET_SINGLE_CHANNELINFO = 4360;

        public const int NET_DVR_GET_FACE_DETECT = 3352;
        public const int NET_DVR_SET_FACE_DETECT = 3353;

        public struct NET_DVR_CHANNEL_GROUP
        {
            public uint dwSize;
            public uint dwChannel; 
            public uint dwGroup; 
            public byte byID;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            public uint dwPositionNo;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 56, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        public struct NET_DVR_HANDLEEXCEPTION_V40
        {
            public uint dwHandleType;            

            public uint dwMaxRelAlarmOutChanNum;
            public uint dwRelAlarmOutChanNum;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM_V30, ArraySubType = UnmanagedType.U4)]
            public uint[] dwRelAlarmOut;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 64, ArraySubType = UnmanagedType.U1)]
            public byte[] byRes;
        }

        public struct NET_DVR_DETECT_FACE
        {
            public uint dwSize;
            public byte byEnableDetectFace;
            public byte byDetectSensitive;
            public byte byEnableDisplay;
            public byte byRes;
            public NET_DVR_HANDLEEXCEPTION_V40 struAlarmHandleType;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = Hikvision.CHCNetSDK.MAX_DAYS * Hikvision.CHCNetSDK.MAX_TIMESEGMENT_V30, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_SCHEDTIME[] struAlarmTime;
            public uint dwMaxRelRecordChanNum;
            public uint dwRelRecordChanNum;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM_V30, ArraySubType = UnmanagedType.U4)]
            public uint[] dwRelRecordChan;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = Hikvision.CHCNetSDK.MAX_TIMESEGMENT_V30, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_SCHEDTIME[] struHolidayTime;
            public ushort wDuration;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 30, ArraySubType = UnmanagedType.U1)]
            public byte[] byRes1;
        }

        public const int MAX_FACE_PIC_NUM = 30;
        public const int COMM_ALARM_FACE_DETECTION = 0x4010;

        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_FACE_DETECTION
        {
            public uint dwSize;
            public uint dwRelativeTime; 
            public uint dwAbsTime; 
            public uint dwBackgroundPicLen; 
            public NET_VCA_DEV_INFO struDevInfo;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = Hikvision.CHCNetSDK.MAX_FACE_PIC_NUM, ArraySubType = UnmanagedType.Struct)]
            public NET_VCA_RECT[] struFacePic;
            public byte byFacePicNum;
            public byte byRes1;
            public ushort wDevInfoIvmsChannelEx;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 252, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
            public IntPtr pBackgroundPicpBuffer;
        }

        public const int NET_DVR_GET_PDC_RESULT = 5089;
        public const int NET_DVR_REMOVE_FLASHSTORAGE = 3756;
        public const int NET_DVR_GET_PDC_RULECFG_V42 = 3405;
        public const int NET_DVR_SET_PDC_RULECFG_V42 = 3406;

        [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
        public struct NET_DVR_PDC_QUERY_COND
        {
            public uint dwSize;
            public uint dwChannel; 
            public NET_DVR_TIME_EX struStartTime;
            public NET_DVR_TIME_EX struEndTime; 
            public byte byReportType; 
            public byte byEnableProgramStatistics; 
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
            public byte[] byRes1;
            public uint dwPlayScheduleNo; 
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 120, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
            public byte[] byRes;
        }

        [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
        public struct NET_DVR_PROGRAM_INFO
        {
            public uint dwProgramNo;
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
            public byte[] sProgramName; 
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
            public byte[] byRes;
        }

        [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
        public struct NET_DVR_PDC_RESULT
        {
            public uint dwSize;
            public NET_DVR_TIME_EX struStartTime;
            public NET_DVR_TIME_EX struEndTime;
            public uint dwEnterNum; 
            public uint dwLeaveNum;
            public NET_DVR_PROGRAM_INFO struProgramInfo; 
            public uint dwPeoplePassing; 
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 200, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
            public byte[] byRes; 
        }

        [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
        public struct NET_DVR_PDC_ALRAM_INFO
        {
            public uint dwSize;          
            public byte byMode;         
            public byte byChannel;      

            public byte bySmart;      
            public byte byRes1;       

            public NET_VCA_DEV_INFO struDevInfo;            
            public NET_MODE_PARAM uStatModeParam;
            public uint dwLeaveNum;      
            public uint dwEnterNum;       
            public byte byBrokenNetHttp;   
            public byte byRes3;
            public short wDevInfoIvmsChannelEx;
            public uint dwPassingNum;
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
            public byte[] byRes;
        }

        [StructLayoutAttribute(LayoutKind.Explicit)]
        public struct NET_MODE_PARAM
        {
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 100, ArraySubType = UnmanagedType.U1)]
            [FieldOffsetAttribute(0)]
            public byte[] byLen;
            [FieldOffsetAttribute(0)]
            public NET_MODE_FRAME struStatFrame;
            [FieldOffsetAttribute(0)]
            public NET_MODE_START_TIME struStatTime;
        }

        [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
        public struct NET_MODE_FRAME
        {
            public uint dwRelativeTime;
            public uint dwAbsTime;
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 92, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
            public byte[] byRes;
        }

        [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
        public struct NET_MODE_START_TIME
        {
            public NET_DVR_TIME tmStart;
            public NET_DVR_TIME tmEnd;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 92, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        public struct NET_DVR_FLASHSTORAGE_REMOVE
        {
            public uint dwSize;
            public uint dwChannel;
            public byte byPDCRemoveEnable;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 127, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;   
        }

        public struct NET_DVR_STD_CONTROL
        {
            public IntPtr lpCondBuffer;   
            public uint dwCondSize;      
            public IntPtr lpStatusBuffer;  
            public uint dwStatusSize;   
            public IntPtr lpXmlBuffer;   
            public uint dwXmlSize;      
            public byte byDataType;    
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 55, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        };

        public const int NET_DVR_GET_TRAFFIC_CAP = 6630;
        public const int UPLOAD_VEHICLE_BLACKWHITELST_FILE = 13;

        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_UploadFile_V40(int lUserID, uint dwUploadType, IntPtr lpInBuffer, uint dwInBufferSize, string sFileName, IntPtr lpOutBuffer, uint dwOutBufferSize);
        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_GetUploadState(int lUploadHandle, IntPtr pProgress);
        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_GetUploadResult(int lUploadHandle, IntPtr lpOutBuffer, uint dwOutBufferSize);
        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_UploadClose(int lUploadHandle);

        public const int NET_SDK_DOWNLOAD_VEHICLE_BLACKWHITELST_FILE = 8;

        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_StartDownload(int lUserID, uint dwDownloadType, IntPtr lpInBuffer, uint dwInBufferSize, string sFileName);
        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_GetDownloadState(int lFileHandle, IntPtr pProgress);
        [DllImport(@"HCNetSDK.dll")]
        public static extern int NET_DVR_GetDownloadStateInfo(int lFileHandle, IntPtr pStatusInfo);
        [DllImport(@"HCNetSDK.dll")]
        public static extern bool NET_DVR_StopDownload(int lFileHandle);

        public const int NET_DVR_GET_GUARDCFG = 3134;
        public const int NET_DVR_SET_GUARDCFG = 3135;
                
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_GUARD_COND
        {
            public uint dwSize;
            public uint dwChannel;           
            public byte byRelateType;
            public byte byGroupNo;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 62, ArraySubType = UnmanagedType.U1)]
            public byte[] byRes;
        };

        public struct NET_DVR_TIME_DETECTION
        {
            public NET_DVR_SCHEDTIME struSchedTime;
            public byte byDetSceneID;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 15, ArraySubType = UnmanagedType.U1)]
            public byte[] byRes;
        };
                
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct NET_DVR_GUARD_CFG
        {
            public uint dwSize;
           
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_DAYS * MAX_TIMESEGMENT_V30, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_TIME_DETECTION[] struAlarmSched;
            public NET_DVR_HANDLEEXCEPTION_V40 struHandleException; 
            public uint dwMaxRelRecordChanNum; 
            public uint dwRelRecordChanNum;     
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_CHANNUM_V30, ArraySubType = UnmanagedType.U4)]
            public uint[] dwRelRecordChan;    
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = MAX_TIMESEGMENT_V30, ArraySubType = UnmanagedType.Struct)]
            NET_DVR_TIME_DETECTION[] struHolidayTime; 
            public byte byDirection;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 87, ArraySubType = UnmanagedType.U1)]
            public byte[] byRes;
        };

        public const int NET_DVR_GET_EVENT_TRIGGERS_CAPABILITIES = 3501;

        #region
        [DllImport("KERNEL32.DLL", EntryPoint = "GetPrivateProfileStringW", CharSet = CharSet.Unicode)]
        private static extern uint GetPrivateProfileStringByByteArray(string lpAppName, string lpKeyName, string lpDefault, byte[] lpReturnedString, uint nSize, string lpFileName);
        public static string ReadIniData(string Section, string Key, string NoText, string iniFilePath)
        {
            
            if (File.Exists(iniFilePath))
            {
                //StringBuilder temp = new StringBuilder(1024);                
                //GetPrivateProfileString(Section, Key, NoText, temp, 1024, iniFilePath);
                //return temp.ToString();
                //ReadIniData("DataBase", "DB_SHARE_User", m_DB_SHARE_User, inifile);
                byte[] byteAr = new byte[1024];
                uint resultSize = GetPrivateProfileStringByByteArray(Section, Key, NoText, byteAr, (uint)byteAr.Length, iniFilePath);
                string strall = Encoding.Unicode.GetString(byteAr, 0, (int)resultSize * 2);
                return strall;
            }
            else
            {
                return String.Empty;
            }
        }
        #endregion

        public struct NET_DVR_PDC_ENTER_DIRECTION
        {
            public NET_VCA_POINT struStartPoint;
            public NET_VCA_POINT struEndPoint;
        }

        public struct NET_DVR_PDC_RULE_CFG_V41
        {
            public uint dwSize;
            public byte byEnable;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 23, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes1;
            public NET_VCA_POLYGON struPolygon;
            public NET_DVR_PDC_ENTER_DIRECTION struEnterDirection;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 56, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_SCHEDTIME[] struAlarmTime;
            public NET_DVR_TIME_EX struDayStartTime;
            public NET_DVR_TIME_EX struNightStartTime;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 100, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        public struct NET_DVR_PDC_RULE_COND
        {
            public uint dwSize;
            public uint dwChannel;
            public uint dwID;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 60, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
            public byte[] byRes;
        }

        public struct NET_VCA_LINE
        {
            public NET_VCA_POINT struStart;
            public NET_VCA_POINT struEnd;
        }

        public struct NET_DVR_PDC_RULE_CFG_V42
        {
            public uint dwSize; 
            public byte byEnable; 
            public byte byOSDEnable;
            public byte byCurDetectType;
            public byte byInterferenceSuppression;            
            
            public byte byDataUploadCycle;
            
            public byte bySECUploadEnable;
            public byte byEmailDayReport;
            public byte byEmailWeekReport;
            public byte byEmailMonthReport;
            public byte byEmailYearReport;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 6, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes2;
            public NET_VCA_POLYGON struPolygon; 
            public NET_DVR_PDC_ENTER_DIRECTION struEnterDirection;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 56, ArraySubType = UnmanagedType.Struct)]
            public NET_DVR_SCHEDTIME[] struAlarmTime;
            public NET_DVR_TIME_EX struDayStartTime; 
            public NET_DVR_TIME_EX struNightStartTime; 
            public NET_DVR_HANDLEEXCEPTION_V40 struAlarmHandleType; 
            public byte byDetecteSensitivity;
            public byte byGenerateSpeedSpace;
            public byte byGenerateSpeedTime;
            public byte byCountSpeed;
            public byte byDetecteType;
            public byte byTargetSizeCorrect;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes3;
            public NET_VCA_LINE struLine;
            public byte byHeightFilterEnable;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes4;            
            public byte byCalibrateType;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes6;             
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 44, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;
        }

        public const int NET_DVR_SET_POS_INFO_OVERLAY = 3913;
        public const int NET_DVR_GET_POS_INFO_OVERLAY = 3914;
        [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
        public struct NET_DVR_POS_INFO_OVERLAY
        {
            public uint dwSize;
            public byte byPosInfoOverlayEnable;
            public byte byOverlayType;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 126, ArraySubType = UnmanagedType.I1)]
            public byte[] byRes;  
        }
        //[MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 14, ArraySubType = UnmanagedType.I1)]

        #region 
        public struct LOCAL_LOG_INFO
        {
            public int iLogType;
            public string strTime;
            public string strLogInfo;
            public string strDevInfo;
            public string strErrInfo;
            public string strScenePicfullpath;
            public string strPlatePicfullpath;
            public string strPlateType;
            public string strPlateColor;
            public string strLicense;
            public string strVehicleType;
            public string strVehicleColor;
        }

        public static void AddLog(int iDeviceIndex, int iLogType, string strFormat)
        {
            LOCAL_LOG_INFO m_struLogInfo = new LOCAL_LOG_INFO();
            ArbolP g_deviceTree = ArbolP.Instance();
            DateTime curTime = DateTime.Now;
            string strTime = null;
            string strlogType = "FAIL";
            string strLogInfo = null;
            string strDevInfo = null;
            string strErrInfo = null;
            //string strLog = null;
            int iErrorCode = 0;
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

            switch (iLogType)
            {
                case OPERATION_SUCC_T:
                    strErrInfo = "";
                    strlogType = "SUCC";
                    break;
                case PLAY_SUCC_T:
                    strErrInfo = "";
                    strlogType = "SUCC";
                    break;
                case PLAY_FAIL_T:
                    strErrInfo = "PLAY_M4 Eorror!!!";
                    break;
                case OPERATION_FAIL_T:
                    IntPtr ptrFail = NET_DVR_GetErrorMsg(ref iErrorCode);
                    string strFail = Marshal.PtrToStringAnsi(ptrFail);
                    strErrInfo = string.Format("Err{0}:{1}", NET_DVR_GetLastError(), strFail);
                    break;
                default:
                    IntPtr ptrTemp = NET_DVR_GetErrorMsg(ref iErrorCode);
                    string strTemp = Marshal.PtrToStringAnsi(ptrTemp);
                    strErrInfo = string.Format("Err{0}:{1}", NET_DVR_GetLastError(), strTemp);
                    break;
            }

            m_struLogInfo.iLogType = iLogType;
            m_struLogInfo.strTime = strTime;
            m_struLogInfo.strLogInfo = strLogInfo;
            m_struLogInfo.strDevInfo = strDevInfo;
            m_struLogInfo.strErrInfo = strErrInfo;
            AddList(m_struLogInfo);
        }

        public static void AddList(LOCAL_LOG_INFO struLogInfo)
        {

            string strLogType = "";
            switch (struLogInfo.iLogType)
            {
                case OPERATION_SUCC_T:
                    strLogType = "SUCC";
                    break;
                case OPERATION_FAIL_T:
                    strLogType = "FAIL";
                    break;
                case PLAY_SUCC_T:
                    strLogType = "SUCC";
                    break;
                case PLAY_FAIL_T:
                    strLogType = "FAIL";
                    break;
                default:
                    strLogType = "FAIL";
                    break;

            }
            string loginfo = "[" + strLogType + "]" + struLogInfo.strLogInfo + "\r\n" + struLogInfo.strErrInfo;
            //MessageBox.Show(loginfo, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #endregion
    }

}
