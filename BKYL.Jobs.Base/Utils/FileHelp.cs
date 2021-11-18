
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BKYL.Jobs.Base.Utils
{
    public static class FileHelp
    {
        /// <summary>  
        /// 返回指定目录下所有文件信息  
        /// </summary>  
        /// <param name="strDirectory">目录字符串</param>  
        /// <returns></returns>  
        public static List<FileInfo> GetAllFilesInDirectory(string strDirectory)
        {
            List<FileInfo> listFiles = new List<FileInfo>(); //保存所有的文件信息  
            DirectoryInfo directory = new DirectoryInfo(strDirectory);
            DirectoryInfo[] directoryArray = directory.GetDirectories();
            FileInfo[] fileInfoArray = directory.GetFiles();
            if (fileInfoArray.Length > 0) listFiles.AddRange(fileInfoArray);
            foreach (DirectoryInfo _directoryInfo in directoryArray)
            {
                DirectoryInfo directoryA = new DirectoryInfo(_directoryInfo.FullName);
                DirectoryInfo[] directoryArrayA = directoryA.GetDirectories();
                FileInfo[] fileInfoArrayA = directoryA.GetFiles();
                if (fileInfoArrayA.Length > 0) listFiles.AddRange(fileInfoArrayA);
                GetAllFilesInDirectory(_directoryInfo.FullName);//递归遍历  
            }
            return listFiles;
        }
    }
}
