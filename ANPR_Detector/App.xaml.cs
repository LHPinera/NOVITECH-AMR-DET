using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using System.Diagnostics;
using System.Windows.Controls;
using System.IO;
namespace IntelligentTraffic
{
    /// <summary>
    /// Lógica de interacción para App.xaml
    /// </summary>
    public partial class App : Application
    {
        //Dahua.fDisConnectCallBack disConnect; //call back for device disconnect.
        
        // override OnStartUp function for set NetClient init function and disconnect callback.
        // and exit to call cleanup function.
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);     
        }

        private void DisConnect(IntPtr lLoginID, IntPtr pchDVRIP, int nDVRPort, IntPtr dwUser)
        {
         
        }
        

    }
}
