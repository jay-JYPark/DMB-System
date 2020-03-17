using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;

namespace DmbDSSystem
{
    public static class TcpProfileMng
    {
        private static TcpProfileDataContainer lstNavTcpProfileData = new TcpProfileDataContainer();
        private static TcpProfileDataContainer lstSpTcpProfileData = new TcpProfileDataContainer();
        private static string dirPath = Directory.GetCurrentDirectory() + "\\Config";
        private static string navFilePath = Directory.GetCurrentDirectory() + "\\Config\\NavProfileConfig.xml";
        private static string spFilePath = Directory.GetCurrentDirectory() + "\\Config\\SpProfileConfig.xml";

        /// <summary>
        /// TCP DMB Profile 프로퍼티
        /// </summary>
        public static List<TcpProfileData> LstNavTcpProfileData
        {
            get { return lstNavTcpProfileData.LstTcpProfileData; }
        }

        /// <summary>
        /// TCP Web Profile 프로퍼티
        /// </summary>
        public static List<TcpProfileData> LstSpTcpProfileData
        {
            get { return lstSpTcpProfileData.LstTcpProfileData; }
        }

        /// <summary>
        /// 일반서비스 Profile 정보 불러오기
        /// </summary>
        public static void LoadNavProfileConfig()
        {
            try
            {
                if (!File.Exists(navFilePath))
                {
                    TcpProfileData profile = new TcpProfileData();
                    profile.IpAddr = "127.0.0.1";
                    profile.Port = 20000;
                    lstNavTcpProfileData.LstTcpProfileData.Add(profile);

                    SaveNavProfileConfig();
                    return;
                }

                using (Stream stream = new FileStream(navFilePath, FileMode.Open))
                {
                    XmlSerializer xmlSerial = new XmlSerializer(typeof(TcpProfileDataContainer));
                    lstNavTcpProfileData = (TcpProfileDataContainer)xmlSerial.Deserialize(stream);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("LoadNavProfileConfig() - " + ex.Message);
            }
        }

        /// <summary>
        /// 일반서비스 Profile 정보 저장
        /// </summary>
        public static void SaveNavProfileConfig()
        {
            try
            {
                if (!Directory.Exists(dirPath))
                {
                    Directory.CreateDirectory(dirPath);
                }

                using (Stream stream = new FileStream(navFilePath, FileMode.Create))
                {
                    XmlSerializer xmlserial = new XmlSerializer(typeof(TcpProfileDataContainer));
                    xmlserial.Serialize(stream, lstNavTcpProfileData);
                    stream.Close();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("SaveNavProfileConfig() - " + ex.Message);
            }
        }

        /// <summary>
        /// 특수서비스 Profile 정보 불러오기
        /// </summary>
        public static void LoadSpProfileConfig()
        {
            try
            {
                if (!File.Exists(spFilePath))
                {
                    TcpProfileData profile = new TcpProfileData();
                    profile.IpAddr = "127.0.0.1";
                    profile.Port = 20000;
                    lstSpTcpProfileData.LstTcpProfileData.Add(profile);

                    SaveSpProfileConfig();
                    return;
                }

                using (Stream stream = new FileStream(spFilePath, FileMode.Open))
                {
                    XmlSerializer xmlSerial = new XmlSerializer(typeof(TcpProfileDataContainer));
                    lstSpTcpProfileData = (TcpProfileDataContainer)xmlSerial.Deserialize(stream);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("LoadSpProfileConfig() - " + ex.Message);
            }
        }

        /// <summary>
        /// 특수서비스 Profile 정보 저장
        /// </summary>
        public static void SaveSpProfileConfig()
        {
            try
            {
                if (!Directory.Exists(dirPath))
                {
                    Directory.CreateDirectory(dirPath);
                }

                using (Stream stream = new FileStream(spFilePath, FileMode.Create))
                {
                    XmlSerializer xmlserial = new XmlSerializer(typeof(TcpProfileDataContainer));
                    xmlserial.Serialize(stream, lstSpTcpProfileData);
                    stream.Close();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("SaveSpProfileConfig() - " + ex.Message);
            }
        }
    }
}