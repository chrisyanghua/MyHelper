using System.IO;

namespace MyHelper4Web
{
    /// <summary>
    /// 文件夹有关的操作类
    /// </summary>
    public class MyDirHelper
    {
        /// <summary>
        /// 获取指定目录中所有子目录列表,若要搜索嵌套的子目录列表,请使用重载方法.
        /// </summary>
        /// <param name="directoryPath">指定目录的绝对路径</param>
        /// <returns>返回值</returns>
        public static string[] GetDirs(string directoryPath)
        {
            return Directory.GetDirectories(directoryPath);
        }

        /// <summary>
        /// 创建文件夹
        /// </summary>
        /// <param name="destDirectory">指定目录的绝对路径</param>
        public static void CreateDir(string destDirectory)
        {
            if (!string.IsNullOrEmpty(destDirectory) && !Directory.Exists(destDirectory))
            {
                Directory.CreateDirectory(destDirectory);
            }
        }

        /// <summary>
        /// 复制文件夹
        /// </summary>
        /// <param name="strFromDirectory">要复制的文件夹</param>
        /// <param name="strToDirectory">复制到的文件夹</param>
        /// <returns>是否复制成功</returns>
        public static bool CopyDir(string strFromDirectory, string strToDirectory)
        {
            Directory.CreateDirectory(strToDirectory);

            if (!Directory.Exists(strFromDirectory)) return false;

            string[] directories = Directory.GetDirectories(strFromDirectory);

            if (directories.Length > 0)
            {
                foreach (string d in directories)
                {
                    CopyDir(d, strToDirectory + d.Substring(d.LastIndexOf("\\")));
                }
            }
            string[] files = Directory.GetFiles(strFromDirectory);

            if (files.Length > 0)
            {
                foreach (string s in files)
                {
                    File.Copy(s, strToDirectory + s.Substring(s.LastIndexOf("\\")));
                }
            }
            return true;
        }

        /// <summary>
        /// 删除文件夹
        /// </summary>
        /// <param name="dirFullPath">要删除文件夹的全路径</param>
        /// <returns>是否删除成功</returns>
        public static bool DeleteDir(string dirFullPath)
        {
            if (Directory.Exists(dirFullPath))
            {
                Directory.Delete(dirFullPath, true);
            }
            else //文件夹不存在
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 得到当前文件夹中所有文件列表string[]
        /// </summary>
        /// <param name="dirFullPath">要遍历的文件夹全路径</param>
        /// <returns>string[] 文件列表</returns>
        public static string[] GetDirFiles(string dirFullPath)
        {
            string[] fileList;
            if (Directory.Exists(dirFullPath))
            {
                fileList = Directory.GetFiles(dirFullPath, "*.*", SearchOption.TopDirectoryOnly);
            }
            else //文件夹不存在
            {
                return null;
            }
            return fileList;
        }

        /// <summary>
        /// 得到当前文件夹及下级文件夹中所有文件列表string[]
        /// </summary>
        /// <param name="dirFullPath">要遍历的文件夹全路径</param>
        /// <param name="so">查找文件的选项，是否包含子级文件夹</param>
        /// <returns>string[] 文件列表</returns>
        public static string[] GetDirFiles(string dirFullPath, SearchOption so)
        {
            string[] fileList;
            if (Directory.Exists(dirFullPath))
            {
                fileList = Directory.GetFiles(dirFullPath, "*.*", so);
            }
            else //文件夹不存在
            {
                return null;
            }
            return fileList;
        }

        /// <summary>
        /// 得到当前文件夹中指定文件类型［扩展名］文件列表string[]
        /// </summary>
        /// <param name="dirFullPath">要遍历的文件夹全路径</param>
        /// <param name="searchPattern">查找文件的扩展名如“*.*代表所有文件；*.doc代表所有doc文件”</param>
        /// <returns>string[] 文件列表</returns>
        public static string[] GetDirFiles(string dirFullPath, string searchPattern)
        {
            string[] fileList;
            if (Directory.Exists(dirFullPath))
            {
                fileList = Directory.GetFiles(dirFullPath, searchPattern);
            }
            else //文件夹不存在
            {
                return null;
            }
            return fileList;
        }

        /// <summary>
        /// 得到当前文件夹及下级文件夹中指定文件类型［扩展名］文件列表string[]
        /// </summary>
        /// <param name="dirFullPath">要遍历的文件夹全路径</param>
        /// <param name="searchPattern">查找文件的扩展名如“*.*代表所有文件；*.doc代表所有doc文件”</param>
        /// <param name="so">查找文件的选项，是否包含子级文件夹</param>
        /// <returns>string[] 文件列表</returns>
        public static string[] GetDirFiles(string dirFullPath, string searchPattern, SearchOption so)
        {
            string[] fileList;
            if (Directory.Exists(dirFullPath))
            {
                fileList = Directory.GetFiles(dirFullPath, searchPattern, so);
            }
            else //文件夹不存在
            {
                return null;
            }
            return fileList;
        }

        /// <summary>
        /// 确保文件夹被创建
        /// </summary>
        /// <param name="filePath">文件夹全名（含路径）</param>
        public static void AssertDirExist(string filePath)
        {
            var dir = new DirectoryInfo(filePath);
            if (!dir.Exists)
            {
                dir.Create();
            }
        }

        /// <summary>
        /// 检测指定目录是否存在
        /// </summary>
        /// <param name="directoryPath">目录的绝对路径</param>
        /// <returns>bool 是否存在</returns>
        public static bool IsExistDirectory(string directoryPath)
        {
            return Directory.Exists(directoryPath);
        }

        /// <summary>
        /// 检测指定目录是否为空
        /// </summary>
        /// <param name="directoryPath">指定目录的绝对路径</param>
        /// <returns>bool 是否为空</returns>
        public static bool IsEmptyDirectory(string directoryPath)
        {
            //判断是否存在文件
            string[] fileNames = GetFileNames(directoryPath);
            if (fileNames.Length > 0)
            {
                return false;
            }

            //判断是否存在文件夹
            string[] directoryNames = GetDirs(directoryPath);
            if (directoryNames.Length > 0)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 检测指定目录中是否存在指定的文件,若要搜索子目录请使用重载方法.
        /// </summary>
        /// <param name="directoryPath">指定目录的绝对路径</param>
        /// <param name="searchPattern">模式字符串，"*"代表0或N个字符，"?"代表1个字符。
        /// 范例："Log*.xml"表示搜索所有以Log开头的Xml文件。</param>
        /// <returns>bool 是否包含文件</returns>
        public static bool ContainFile(string directoryPath, string searchPattern)
        {
            //获取指定的文件列表
            string[] fileNames = GetFileNames(directoryPath, searchPattern, false);

            //判断指定文件是否存在
            if (fileNames.Length == 0)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 检测指定目录中是否存在指定的文件
        /// </summary>
        /// <param name="directoryPath">指定目录的绝对路径</param>
        /// <param name="searchPattern">模式字符串，"*"代表0或N个字符，"?"代表1个字符。
        /// 范例："Log*.xml"表示搜索所有以Log开头的Xml文件。</param>
        /// <param name="isSearchChild">是否搜索子目录</param>
        /// <returns>bool 是否包含文件</returns>
        public static bool ContainFile(string directoryPath, string searchPattern, bool isSearchChild)
        {
            //获取指定的文件列表
            string[] fileNames = GetFileNames(directoryPath, searchPattern, true);

            //判断指定文件是否存在
            if (fileNames.Length == 0)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 取当前目录
        /// </summary>
        /// <returns>当前目录名</returns>
        public static string GetCurrentDirectory()
        {
            return Directory.GetCurrentDirectory();
        }

        /// <summary>
        /// 设当前目录
        /// </summary>
        /// <param name="path">目录绝对路径</param>
        public static void SetCurrentDirectory(string path)
        {
            Directory.SetCurrentDirectory(path);
        }

        /// <summary>
        /// 取路径中不充许存在的字符
        /// </summary>
        /// <returns>不充许存在的字符</returns>
        public static char[] GetInvalidPathChars()
        {
            return Path.GetInvalidPathChars();
        }

        /// <summary>
        /// 取系统所有的逻辑驱动器
        /// </summary>
        /// <returns>所有的逻辑驱动器</returns>
        public static DriveInfo[] GetAllDrives()
        {
            return DriveInfo.GetDrives();
        }

        /// <summary>
        /// 获取指定目录中所有文件列表
        /// </summary>
        /// <param name="directoryPath">指定目录的绝对路径</param>
        /// <returns>所有文件列表</returns>
        public static string[] GetFileNames(string directoryPath)
        {
            //如果目录不存在，则抛出异常
            if (!IsExistDirectory(directoryPath))
            {
                throw new FileNotFoundException();
            }

            //获取文件列表
            return Directory.GetFiles(directoryPath);
        }

        /// <summary>
        /// 获取指定目录及子目录中所有文件列表
        /// </summary>
        /// <param name="directoryPath">指定目录的绝对路径</param>
        /// <param name="searchPattern">模式字符串，"*"代表0或N个字符，"?"代表1个字符。
        /// 范例："Log*.xml"表示搜索所有以Log开头的Xml文件。</param>
        /// <param name="isSearchChild">是否搜索子目录</param>
        /// <returns>指定目录及子目录中所有文件列表</returns>
        public static string[] GetFileNames(string directoryPath, string searchPattern, bool isSearchChild)
        {
            //如果目录不存在，则抛出异常
            if (!IsExistDirectory(directoryPath))
            {
                throw new FileNotFoundException();
            }

            if (isSearchChild)
            {
                return Directory.GetFiles(directoryPath, searchPattern, SearchOption.AllDirectories);
            }
            return Directory.GetFiles(directoryPath, searchPattern, SearchOption.TopDirectoryOnly);
        }

    }
}
