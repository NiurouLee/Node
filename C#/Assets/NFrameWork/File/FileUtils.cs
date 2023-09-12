using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
using Object = UnityEngine.Object;

namespace NFrameWork.File
{
    public static class FileUtils
    {
        public static void DeleteFileAndCreateParentDIrIf(string inPath)
        {
            FileInfo fi = new FileInfo(inPath);
            if (fi.Exists)
            {
                fi.Delete();
            }

            DirectoryInfo dir = fi.Directory;
            if (dir != null && !dir.Exists)
            {
                dir.Create();
            }
        }

        public static void CopyFIleAndCreateDirIf(string inSrcFile, string inTargetFile)
        {
            FileInfo fi = new FileInfo(inTargetFile);
            DirectoryInfo directory = fi.Directory;
            if (!directory.Exists)
            {
                directory.Create();
            }

            System.IO.File.Copy(inSrcFile, inTargetFile, true);
        }

        public static void CopyDirectory(string inSrcDic, string inDestDir, string inExtension)
        {
            DirectoryInfo srcDirInfo = new DirectoryInfo(inSrcDic);
            DirectoryInfo destDirInfo = new DirectoryInfo(inDestDir);
        }

        public static void DeleteDirectory(string inDir)
        {
            if (Directory.Exists(inDir))
            {
                Directory.Delete(inDir);
            }
        }

        public static void EmptyDir(string inDir)
        {
            DeleteDirectory(inDir);
            DirectoryInfo dirInfo = new DirectoryInfo(inDir);
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }

        }


        public static string UpDir(string inPath, int inDepth)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(inPath);
            FileInfo fileInfo = new FileInfo(inPath);
            if (dirInfo.Exists)
            {
                try
                {
                    for (int i = 0; i < inDepth; i++)
                    {
                        dirInfo = dirInfo.Parent;
                    }
                }
                catch (Exception e)
                {
                    return $"ErrorPath:{inPath},UpLevel:{inDepth}";
                }
            }
            else if (fileInfo.Exists)
            {
                return UpDir(dirInfo.Parent.FullName, inDepth);
            }

            return $"ErrorPath:{inPath} is not dir or fullPath";
        }


        public static void SaveBytesToFile(string inFilePath, byte[] inBytes)
        {
            System.IO.File.WriteAllBytes(inFilePath, inBytes);
        }

        public static bool SavaFileByBinary(string inFilePath, Object inObj)
        {
            using (FileStream fs = System.IO.File.Create(inFilePath))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fs, inObj);
            }

            return true;
        }

        public static bool WriteAllBytes(string inFileName, byte[] inBytes)
        {
            if (string.IsNullOrEmpty(inFileName) || inBytes == null)
            {
                return false;
            }

            try
            {
                string directoryPath = Path.GetDirectoryName(inFileName);
                if (!Directory.Exists(directoryPath) && Directory.CreateDirectory(directoryPath) == null)
                {
                    return false;
                }

                System.IO.File.WriteAllBytes(inFileName, inBytes);
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }
    }
}