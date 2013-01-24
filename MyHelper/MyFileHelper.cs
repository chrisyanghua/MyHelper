using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace MyHelper4Web
{

    /// <summary>
    /// 文件操作类
    /// </summary>
    public class MyFileHelper
    {
        #region Method

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="fileFullPath">文件全路径</param>
        /// <returns>bool 是否删除成功</returns>
        public static bool DeleteFile(string fileFullPath)
        {
            if (File.Exists(fileFullPath))
            {
                if (File.GetAttributes(fileFullPath) == FileAttributes.Normal)
                {
                    File.Delete(fileFullPath);
                }
                else
                {
                    File.SetAttributes(fileFullPath, FileAttributes.Normal);
                    File.Delete(fileFullPath);
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// 根据传来的文件全路径，获取文件名称部分默认包括扩展名
        /// </summary>
        /// <param name="fileFullPath">文件全路径</param>
        /// <returns>string 文件名称</returns>
        public static string GetFileName(string fileFullPath)
        {
            if (File.Exists(fileFullPath))
            {
                var f = new FileInfo(fileFullPath);
                return f.Name;
            }
            return null;
        }

        /// <summary>
        /// 根据传来的文件全路径，获取文件名称部分
        /// </summary>
        /// <param name="fileFullPath">文件全路径</param>
        /// <param name="includeExtension">是否包括文件扩展名</param>
        /// <returns>string 文件名称</returns>
        public static string GetFileName(string fileFullPath, bool includeExtension)
        {
            if (File.Exists(fileFullPath))
            {
                var f = new FileInfo(fileFullPath);
                if (includeExtension)
                {
                    return f.Name;
                }
                return f.Name.Replace(f.Extension, "");
            }
            return null;
        }

        /// <summary>
        /// 根据传来的文件全路径，获取新的文件名称全路径,一般用作临时保存用
        /// </summary>
        /// <param name="fileFullPath">文件全路径</param>
        /// <returns>string 新的文件全路径名称</returns>
        public static string GetNewFileFullName(string fileFullPath)
        {
            if (File.Exists(fileFullPath))
            {
                var f = new FileInfo(fileFullPath);
                string tempFileName = fileFullPath.Replace(f.Extension, "");
                for (int i = 0; i < 1000; i++)
                {
                    fileFullPath = tempFileName + i.ToString() + f.Extension;
                    if (File.Exists(fileFullPath) == false)
                    {
                        break;
                    }
                }
            }
            return fileFullPath;
        }

        /// <summary>
        /// 根据传来的文件全路径，获取文件扩展名不包括“.”，如“doc”
        /// </summary>
        /// <param name="fileFullPath">文件全路径</param>
        /// <returns>string 文件扩展名</returns>
        public static string GetFileExtension(string fileFullPath)
        {
            if (File.Exists(fileFullPath))
            {
                var f = new FileInfo(fileFullPath);
                return f.Extension;
            }
            return null;
        }

        /// <summary>
        /// 根据传来的文件全路径，外部打开文件，默认用系统注册类型关联软件打开
        /// </summary>
        /// <param name="fileFullPath">文件全路径</param>
        /// <returns>bool 文件名称</returns>
        public static bool OpenFile(string fileFullPath)
        {
            if (File.Exists(fileFullPath))
            {
                Process.Start(fileFullPath);
                return true;
            }
            return false;
        }

        /// <summary>
        /// 根据传来的文件全路径，得到文件大小，规范文件大小称呼，如1ＧＢ以上，单位用ＧＢ，１ＭＢ以上，单位用ＭＢ，１ＭＢ以下，单位用ＫＢ
        /// </summary>
        /// <param name="fileFullPath">文件全路径</param>
        /// <returns>bool 文件大小</returns>
        public static string GetFileSize(string fileFullPath)
        {
            if (File.Exists(fileFullPath))
            {
                var f = new FileInfo(fileFullPath);
                long fl = f.Length;
                if (fl > 1024 * 1024 * 1024)
                {
                    return Convert.ToString(Math.Round((fl + 0.00) / (1024 * 1024 * 1024), 2)) + " GB";
                }
                if (fl > 1024 * 1024)
                {
                    return Convert.ToString(Math.Round((fl + 0.00) / (1024 * 1024), 2)) + " MB";
                }
                return Convert.ToString(Math.Round((fl + 0.00) / 1024, 2)) + " KB";
            }
            return null;
        }

        /// <summary>
        /// 文件转换成二进制，返回二进制数组Byte[]
        /// </summary>
        /// <param name="fileFullPath">文件全路径</param>
        /// <returns>byte[] 包含文件流的二进制数组</returns>
        public static byte[] FileToStreamByte(string fileFullPath)
        {
            byte[] fileData = null;
            if (File.Exists(fileFullPath))
            {
                var fs = new FileStream(fileFullPath, FileMode.Open);
                fileData = new byte[fs.Length];
                fs.Read(fileData, 0, fileData.Length);
                fs.Close();
            }
            return fileData;
        }

        /// <summary>
        /// 二进制数组Byte[]生成文件
        /// </summary>
        /// <param name="createFileFullPath">要生成的文件全路径</param>
        /// <param name="streamByte">要生成文件的二进制 Byte 数组</param>
        /// <returns>bool 是否生成成功</returns>
        public static bool ByteStreamToFile(string createFileFullPath, byte[] streamByte)
        {
            if (File.Exists(createFileFullPath) == false)
            {
                FileStream fs = File.Create(createFileFullPath);
                fs.Write(streamByte, 0, streamByte.Length);
                fs.Close();
                return true;
            }
            return false;
        }

        /// <summary>
        /// 二进制数组Byte[]生成文件，并验证文件是否存在，存在则先删除
        /// </summary>
        /// <param name="createFileFullPath">要生成的文件全路径</param>
        /// <param name="streamByte">要生成文件的二进制 Byte 数组</param>
        /// <param name="fileExistsDelete">同路径文件存在是否先删除</param>
        /// <returns>bool 是否生成成功</returns>
        public static bool ByteStreamToFile(string createFileFullPath, byte[] streamByte, bool fileExistsDelete)
        {
            if (File.Exists(createFileFullPath))
            {
                if (fileExistsDelete && DeleteFile(createFileFullPath) == false)
                {
                    return false;
                }
            }
            FileStream fs = File.Create(createFileFullPath);
            fs.Write(streamByte, 0, streamByte.Length);
            fs.Close();
            return true;
        }

        /// <summary>
        /// 读写文件，并进行匹配文字替换
        /// </summary>
        /// <param name="pathRead">读取路径</param>
        /// <param name="pathWrite">写入路径</param>
        /// <param name="replaceStrings">替换字典</param>
        public static void ReadAndWriteFile(string pathRead, string pathWrite, Dictionary<string, string> replaceStrings)
        {
            var objReader = new StreamReader(pathRead);
            if (File.Exists(pathWrite))
            {
                File.Delete(pathWrite);
            }
            var streamw = new StreamWriter(pathWrite, false, Encoding.GetEncoding("utf-8"));
            var readLine = objReader.ReadToEnd();
            if (replaceStrings != null && replaceStrings.Count > 0)
            {
                foreach (var dicPair in replaceStrings)
                {
                    readLine = readLine.Replace(dicPair.Key, dicPair.Value);
                }
            }
            streamw.WriteLine(readLine);
            objReader.Close();
            streamw.Close();
        }

        /// <summary>
        /// 读取文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>返回值</returns>
        public static string ReadFile(string filePath)
        {
            var objReader = new StreamReader(filePath);
            string readLine = null;
            if (File.Exists(filePath))
            {
                readLine = objReader.ReadToEnd();
            }
            objReader.Close();
            return readLine;
        }

        /// <summary>
        /// 写入文件
        /// </summary>
        /// <param name="pathWrite">写入路径</param>
        /// <param name="content">内容</param>
        public static void WriteFile(string pathWrite, string content)
        {
            if (File.Exists(pathWrite))
            {
                File.Delete(pathWrite);
            }
            var streamw = new StreamWriter(pathWrite, false, Encoding.GetEncoding("utf-8"));
            streamw.WriteLine(content);
            streamw.Close();
        }

        /// <summary>
        /// 读取并附加文本
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="content">内容</param>
        public static void ReadAndAppendFile(string filePath, string content)
        {
            File.AppendAllText(filePath, content, Encoding.GetEncoding("utf-8"));
        }

        /// <summary>
        /// 复制文件
        /// </summary>
        /// <param name="sources">源文件</param>
        /// <param name="dest">目标文件</param>
        public static void CopyFile(string sources, string dest)
        {
            var dinfo = new DirectoryInfo(sources);
            foreach (FileSystemInfo f in dinfo.GetFileSystemInfos())
            {
                var destName = Path.Combine(dest, f.Name);
                if (f is FileInfo)
                {
                    File.Copy(f.FullName, destName, true);
                }
                else
                {
                    Directory.CreateDirectory(destName);
                    CopyFile(f.FullName, destName);
                }
            }
        }

        /// <summary>
        /// 复制文件
        /// </summary>
        /// <param name="sources">源文件</param>
        /// <param name="dest">目标文件</param>
        public static void MoveFile(string sources, string dest)
        {
            var dinfo = new DirectoryInfo(sources);
            foreach (FileSystemInfo f in dinfo.GetFileSystemInfos())
            {
                var destName = Path.Combine(dest, f.Name);
                if (f is FileInfo)
                {
                    File.Move(f.FullName, destName);
                }
                else
                {
                    Directory.CreateDirectory(destName);
                    MoveFile(f.FullName, destName);
                }
            }
        }

        /// <summary>
        /// 检测指定文件是否存在,如果存在则返回true。
        /// </summary>
        /// <param name="filePath">文件的绝对路径</param>
        /// <returns>bool 是否存在文件</returns>
        public static bool IsExistFile(string filePath)
        {
            return File.Exists(filePath);
        }

        #endregion

    }
}
