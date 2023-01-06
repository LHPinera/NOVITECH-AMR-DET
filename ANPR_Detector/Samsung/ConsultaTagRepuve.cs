using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using App.Entity;
using Impinj.OctaneSdk;

namespace IntelligentTraffic.Samsung
{
    public class ConsultaTagRepuve
    {
        private static ImpinjReader ReaderImpinj = new ImpinjReader();

        public static void Iniciar(string hostname)
        {
            try
            {
                ReaderImpinj.Connect(hostname);
                Settings settings = ReaderImpinj.QueryDefaultSettings();

                settings.Report.IncludeAntennaPortNumber = true;
                settings.ReaderMode = ReaderMode.AutoSetDenseReader;
                settings.SearchMode = SearchMode.DualTarget;
                settings.Session = 2;
                settings.Antennas.EnableAll();
                settings.Antennas.TxPowerMax = true;
                settings.Antennas.RxSensitivityMax = true;
                ReaderImpinj.ApplySettings(settings);
                ReaderImpinj.TagsReported += OnTagsReported;

                ReaderImpinj.Start();

            }
            catch (OctaneSdkException ex)
            {
                Console.WriteLine("Octane SDK excepción {0} - (Inicio):", ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Excepción {0} - (Inicio):", ex.Message);
            }
        }

        private static void OnTagsReported(ImpinjReader reader, TagReport report)
        {
            Tag tag = report.Tags.Last();

            ceAntena lectura = new ceAntena()
            {
                Fecha  = DateTime.Now,
                Numero = tag.AntennaPortNumber.ToString(),
                Folio  = tag.Epc.ToHexString()
            };

            ShowData(lectura);
        }

        private static void ShowData(ceAntena lectura)
        {
            //Ver información del REPUVE
        }

        private void StopReader(object sender, EventArgs e)
        {
            try
            {
                ReaderImpinj.Stop();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Excepcion {0} - (StopReader): ", ex.Message);
            }
        }

        private void StartReader(object sender, EventArgs e)
        {
            try
            {
                ReaderImpinj.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Excepcion {0} - (StartReader): ", ex.Message);
            }
        }

    }
}
