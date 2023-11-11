using System.IO;
using UnityEngine;
using Vmaya.RW;
using static Vmaya.RW.MyData;

namespace Vmaya.Util
{
    public class CopyDefaultFile : MonoBehaviour
    {
        [System.Serializable]
        public struct pathInfo
        {
            public TargetFolder targetFolder;
            public string relativePathFile;
        }

        public pathInfo[] relativePathFiles;

        private void Awake()
        {
            checkFolders();
            foreach (pathInfo pathFile in relativePathFiles)
            {
                string fullPath = fullFilePath(PathUtils.getFolder(pathFile.targetFolder), pathFile.relativePathFile, "");
                if (File.Exists(pathFile.relativePathFile) && !File.Exists(fullPath))
                    File.Copy(pathFile.relativePathFile, fullPath);
            }
        }

        private void checkFolders()
        {
            foreach (pathInfo pathFile in relativePathFiles)
            {
                FileInfo fi = new FileInfo(pathFile.relativePathFile);
                string path = PathUtils.getFolder(pathFile.targetFolder) + pathFile.relativePathFile.Replace(fi.Name, "");
                if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            }
        }
    }
}
