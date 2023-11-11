using Vmaya.RW;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Vmaya.Collections;
using Vmaya.UI.Components;
using Vmaya.Util;

namespace Vmaya.UI.Collections
{
    public class FileListSource : BaseFSListSource
    {
        [SerializeField]
        private string _filter;
        [SerializeField]
        private TargetFolder _targetFolder;
        [SerializeField]
        private string _relativePath;
        [SerializeField]
        private bool _showDrives;
        [SerializeField]
        private bool _showDirs;
        [SerializeField]
        private bool _showFiles;

        public UnityEvent onChangePath;

        protected string basePath => PathUtils.getFolder(_targetFolder);

        private void Start()
        {
            if (_relativePath == "")
                setAbsolutePath(basePath);
            else setAbsolutePath(MyData.fullDirPath(basePath, _relativePath));
        }

        private bool isTop
        {
            get { return (_relativePath.Length < 4) && (_relativePath[1] == ':'); }
        }

        public void setAbsolutePath(string a_value)
        {
            _relativePath = asPath(a_value);
            Refresh();
            onChangePath.Invoke();
        }

        protected static string asPath(string path)
        {
            if ((path.Length > 1) && (path[path.Length - 1] != Path.DirectorySeparatorChar))
                return path + Path.DirectorySeparatorChar;
            return path;
        }

        public void setRelativePath(ListView drivesList)
        {
            DriveListSource dls = drivesList.Source as DriveListSource;
            if (dls) setAbsolutePath(dls[drivesList.selectedIndex].name);
        }

        public void appendRelative(ListView a_list)
        {
            if (a_list.Source == this as IListSource)
            {
                string sel = this[a_list.selectedIndex].name;

                if (this[a_list.selectedIndex].type == FSType.File)
                    return;

                if (this[a_list.selectedIndex].type == FSType.Dir)
                {
                    if (sel == "..")
                    {
                        if (_relativePath.Length > 2)
                        {
                            int lix = _relativePath.LastIndexOf(Path.DirectorySeparatorChar, _relativePath.Length - 2);
                            if (lix > -1) _relativePath = _relativePath.Substring(0, lix) + Path.DirectorySeparatorChar;
                        }
                    }
                    else _relativePath = asPath(_relativePath) + asPath(this[a_list.selectedIndex].name);
                }
                else if (this[a_list.selectedIndex].type == FSType.Disk)
                {
                    _relativePath = sel;
                }

                Refresh();
                onChangePath.Invoke();
            }
        }

        public string relativePath
        {
            get
            {
                return _relativePath.Trim().Length > 0 ? _relativePath : GetComponentInParent<MyData>().relativePath;
            }
        }

        protected IEnumerator delayRefresh()
        {
            yield return new WaitForSeconds(0.01f);

            List<FileRecord> result = new List<FileRecord>();
            try
            {
                DirectoryInfo dir = new DirectoryInfo(relativePath);
                DirectoryInfo[] dirs = dir.GetDirectories();
                FileInfo[] files = dir.GetFiles();

                if (!isTop) result.Add(new FileRecord(FSType.Dir, "..", relativePath));
                else if (_showDrives) {
                    DriveInfo[] allDrives = DriveInfo.GetDrives();
                    foreach (DriveInfo di in allDrives)
                    {
                        string dirstr = di.RootDirectory.Name.Replace('\\', Path.DirectorySeparatorChar);
                        result.Add(new FileRecord(FSType.Disk, dirstr, dirstr));
                    }
                }

                if (_showDirs)
                        foreach (DirectoryInfo d in dirs)
                        result.Add(new FileRecord(FSType.Dir, d.Name, d.FullName));

                if (_showFiles)
                    foreach (FileInfo f in files) 
                        if (!(((f.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden) ||
                                ((f.Attributes & FileAttributes.System) == FileAttributes.System)) && Filter(f))
                            result.Add(new FileRecord(FSType.File, f.Name, f.FullName));

            } catch (Exception e)
            {
                Debug.Log(relativePath);
                Debug.LogError(e);
            }
            setList(result);
        }

        protected virtual bool Filter(FileInfo f)
        {
            if (!string.IsNullOrEmpty(_filter))
                return _filter.Contains(f.Extension);
            else return true;
        }

        override public void Refresh()
        {
            StartCoroutine(delayRefresh());
        }

        public void outCurrentPath(Text output)
        {
            output.text = _relativePath;
        }
    }
}