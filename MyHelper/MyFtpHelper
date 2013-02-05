using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace MyHelper4Web
{
    ///<summary>
    ///</summary>
    public class MyFtpHelper
    {
        /// <summary>
        /// 上传文件到服务器
        /// </summary>
        /// <param name="hostName">服务器地址，如：ftp://192.168.1.101</param>
        /// <param name="fileName">上传的文件本地路径</param>
        /// <param name="uploadDir">上传到服务器的目录，如:ftp://192.168.1.101/Test </param>
        /// <param name="ftpUser">FTP的用户名</param>
        /// <param name="ftpPassWord">FTP的密码</param>
        /// <returns></returns>
        public static void UploadFile(string hostName, string fileName, string uploadDir, string ftpUser, string ftpPassWord)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return;
            }
            if (string.IsNullOrEmpty(uploadDir))
            {
                return;
            }
            FileInfo fileinfo = new FileInfo(fileName);

            if (!FtpIsExistsFile(uploadDir, ftpUser, ftpPassWord))
            {
                if (!CreateDirection(uploadDir, ftpUser, ftpPassWord))
                {
                    return;
                }
            }
            string URI = uploadDir + fileinfo.Name;

            FtpWebRequest ftp = (FtpWebRequest) FtpWebRequest.Create(URI);
            ftp.Credentials = new NetworkCredential(ftpUser, ftpPassWord);
            ftp.KeepAlive = false;
            ftp.Method = WebRequestMethods.Ftp.UploadFile;
            ftp.UseBinary = true;
            ftp.UsePassive = true;
            ftp.ContentLength = fileinfo.Length;

            const int bufferSize = 2048;
            byte[] content = new byte[bufferSize - 1 + 1];
            int dataRead;

            using (FileStream fs = fileinfo.OpenRead())
            {
                try
                {
                    using (Stream rs = ftp.GetRequestStream())
                    {
                        do
                        {
                            dataRead = fs.Read(content, 0, bufferSize);
                            rs.Write(content, 0, dataRead);
                        } while (!(dataRead < bufferSize));
                        rs.Close();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    fs.Close();
                }
            }
            ftp = null;
        }

        /// <summary>
        /// 创建FTP目录，返回值是否创建成功
        /// </summary>
        /// <param name="ftpUri">FTP的目录</param>
        /// <param name="ftpUser">FTP的用户名</param>
        /// <param name="ftpPassWord">FTP的密码</param>
        private static bool CreateDirection(string ftpUri,string ftpUser,string ftpPassWord)
        {
            bool flag = true;
            try
            {
                FtpWebRequest ftp = (FtpWebRequest)FtpWebRequest.Create(ftpUri);
                ftp.Credentials = new NetworkCredential(ftpUser, ftpPassWord);
                ftp.Method = WebRequestMethods.Ftp.MakeDirectory;

                FtpWebResponse response = (FtpWebResponse)ftp.GetResponse();
                response.Close();
            }
            catch (Exception ex)
            {
                flag = false;
            }
            return flag;
        }

        /// <summary>
        /// 判断FTP上目录是否存在
        /// </summary>
        /// <param name="uri">FTP上的目录</param>
        /// <param name="ftpUser">FTP的用户名</param>
        /// <param name="ftpPassWord">FTP的密码</param>
        /// <returns></returns>
        private static bool FtpIsExistsFile(string uri, string ftpUser, string ftpPassWord)
        {
            bool flag = true;
            try
            {
                FtpWebRequest ftp = (FtpWebRequest)FtpWebRequest.Create(uri);
                ftp.Credentials = new NetworkCredential(ftpUser, ftpPassWord);
                ftp.Method = WebRequestMethods.Ftp.ListDirectory;


                FtpWebResponse response = (FtpWebResponse)ftp.GetResponse();
                response.Close();
            }
            catch (Exception)
            {
                flag = false;
            }
            return flag;
        }
    }
}
