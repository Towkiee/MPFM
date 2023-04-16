using System;
using System.Runtime.InteropServices;
using System.Text;
using System.IO;

namespace IniFile
{
    public class ClassIniFile
    {
        public ClassIniFile()
        {
        }
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern long GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        #region   创建文件
        public static void CreateFile(string path)
        {
            if (!string.IsNullOrEmpty(path))
            {
                try
                {
                    string dr = Path.GetDirectoryName(path);

                    if (!Directory.Exists(dr))
                    {
                        Directory.CreateDirectory(dr);
                    }
                    if (!File.Exists(path))
                    {
                        FileStream fs = File.Create(path);

                        fs.Close();
                    }
                }
                catch (Exception e)
                {
                }
            }
        }
        #endregion
        #region 写ini文件
        ///<param name="Selection">ini文件中的节名</param>
        ///<param name="key">ini 文件中的健</param>
        ///<param name="value">要写入该健所对应的值</param>
        ///<param name="iniFilePath">ini文件路径</param>
        public static bool WriteIniData(string Section, string key, string val, string inifilePath)
        {
            if (File.Exists(inifilePath))
            {
                long opSt = WritePrivateProfileString(Section, key, val, inifilePath);
                if (opSt == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                CreateFile(inifilePath);
                long opSt = WritePrivateProfileString(Section, key, val, inifilePath);
                if (opSt == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        #endregion
        #region  取ini文件
        /// <param name="section">节点名称</param>
        /// <param name="key">对应的key</param>
        /// <param name="noText">读不到值时返回的默认值</param>
        /// <param name="iniFilePath">文件路径</param>
        public static string ReadIniData(string section, string key, string noText, string iniFilePath)
        {
            if (File.Exists(iniFilePath))
            {
                StringBuilder temp = new StringBuilder(1024);
                long k = GetPrivateProfileString(section, key, noText, temp, 1024, iniFilePath);
                if (k != 0)
                {
                    return temp.ToString();
                }
                else
                {
                    return string.Empty;
                }
            }
            else
            {
                return string.Empty;
            }
        }
        #endregion
        #region   把key——value写入ini文件
        public bool savePwdToIni(string pwd)
        {
            string path = System.AppDomain.CurrentDomain.BaseDirectory;
            path += "\\" + "ini" + "\\Password.ini";
            bool b = WriteIniData("Section_1", "pwd", pwd, path);
            return b;
        }
        #endregion

        #region   从路径下的ini文件读取key对应的value
        public string readPwdFromIni()
        {
            string path = System.AppDomain.CurrentDomain.BaseDirectory;
            path += "\\" + "ini" + "\\Password.ini";
            string s = ReadIniData("Section_1", "pwd", "error", path);
            return s;
        }
        //数据读写
        //bool b = WriteIniData("Section_1", "key", value, path);
        //string s = ReadIniData("Section_2", "key", "error", path);
        #endregion
    }
}
