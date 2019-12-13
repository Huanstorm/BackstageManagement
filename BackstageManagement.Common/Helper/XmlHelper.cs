using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BackstageManagement.Common.Helper
{
    public class XmlHelper
    {
        /// <summary>
        /// 将文件转换为byte数组
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static byte[] FileToBytes(string fileName)
        {
            try
            {
                MemoryStream stream = new MemoryStream();
                FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                byte[] fileDatas = new byte[fs.Length];
                fs.Read(fileDatas, 0, Convert.ToInt32(fs.Length));
                fs.Close();
                return fileDatas;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 根据字节流获取XML
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static XElement GetXmlByBytes(byte[] bytes)
        {
            try
            {
                MemoryStream ms = new MemoryStream(bytes);
                XDocument doc = XDocument.Load(ms);
                return doc.Root;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 文件转换成二进制
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static byte[] FileToByteforUNTLog(string path)
        {
            try
            {
                if (!File.Exists(path))
                {
                    return new byte[0];
                }

                FileInfo fi = new FileInfo(path);
                byte[] buff = new byte[fi.Length];

                FileStream fs = fi.OpenRead();
                fs.Read(buff, 0, Convert.ToInt32(fs.Length));
                return buff;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// 二进制转成文件
        /// </summary>
        /// <param name="buff"></param>
        /// <param name="savepath"></param>
        public static void BytetoFileforUNTLog(byte[] buff, string savepath)
        {
            try
            {
                if (File.Exists(savepath))
                {
                    File.Delete(savepath);
                }

                FileStream fs = new FileStream(savepath, FileMode.CreateNew);
                BinaryWriter bw = new BinaryWriter(fs);
                bw.Write(buff, 0, buff.Length);
                bw.Close();
                fs.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
