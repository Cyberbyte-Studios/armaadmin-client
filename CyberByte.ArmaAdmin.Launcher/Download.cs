using Extensions.Data;
using System.IO;
using AltoHttp;
using System.Diagnostics;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CyberByte.ArmaAdmin.Launcher
{

    class Download
    {
        public HttpDownloadQueue downloadQueue = new HttpDownloadQueue();
        private static List<FileInfo> files = new List<FileInfo>();
        private static string BaseDir = "";
        private static int BaseDirLength = 0;

        public static Mods GetFiles()
        {
            dynamic files = (dynamic)Requests.Get("api/v1/files");

            Task<Mods> modsTask = Task<Mods>.Factory.StartNew(() =>
            {
                Mods mods = new Mods();
                dynamic jsonMods = files.results;

                //Typecasting is done as each item is just a JValue

                for (int i = 0; i < jsonMods.Count; i++)
                {
                    Mod mod = new Mod(
                        (string)jsonMods[i].id,
                        (string)jsonMods[i].filename,
                        (ulong)jsonMods[i].size,
                        (string)jsonMods[i].realtive_path,
                        (ulong)jsonMods[i].hash,
                        (string)jsonMods[i].download,
                        (string)jsonMods[i].created);

                    mod.info(); //Just prints out all the info

                    mods.add(mod);
                };

                return mods;
            });

            return modsTask.Result;
        }

        /*
         *  TODO:
         *  https://github.com/brandondahler/Data.HashFunction
         *  https://www.nuget.org/packages/System.Data.HashFunction.xxHash
         *  http://datahashfunction.azurewebsites.net/1.8.1/html/787eb446-5a08-d4ea-fde3-955fa4463198.htm
         */
        public static ulong HashFile(string path)
        {
            Stream stream = File.OpenRead(path);
            XXHash.State64 state = XXHash.CreateState64();

            XXHash.UpdateState64(state, stream);

            return XXHash.DigestState64(state);
        }

        private static void WalkDirectoryTree(DirectoryInfo root)
        {
            FileInfo[] dirFiles = null;
            DirectoryInfo[] subDirs = null;

            try
            {
                dirFiles = root.GetFiles("*.*"); //All the files in this Directory
            }
            catch (UnauthorizedAccessException exception)
            {
            }
            catch (DirectoryNotFoundException exception)
            {
            }

            if (dirFiles != null)
            {
                foreach (FileInfo fi in dirFiles)
                {
                    files.Add(fi);
                }

                subDirs = root.GetDirectories();

                foreach (DirectoryInfo dirInfo in subDirs)
                {
                    WalkDirectoryTree(dirInfo);
                }
            }
        }

        private void calcFilesToDownload(List<Mod> mods, List<FileInfo> fileList)
        {
            foreach(FileInfo file in fileList)
            {
                string relPath = file.FullName.Substring(BaseDirLength);

                Mod modFile = mods.FirstOrDefault(mod => mod.Relative_Path == relPath);

                if (modFile == null)
                {
                    //Remove File Cameron
                    continue;
                }

                if (HashFile(file.FullName) != modFile.Hash)
                {
                    downloadQueue.Add(modFile.Url, file.FullName);
                    mods.Remove(modFile);
                }
            }

            foreach(Mod file in mods)
            {
                downloadQueue.Add(file.Url, Path.Combine(BaseDir, file.Relative_Path));
                mods.Remove(file);
            }
        }

        public void start()
        {
            Mods mods = GetFiles();
            BaseDirLength = BaseDir.Length;
            DirectoryInfo dirInf = new DirectoryInfo(BaseDir);

          
            WalkDirectoryTree(dirInf);

            calcFilesToDownload(mods.get(), files);

            

        }
    }

    class DownloadException : Exception
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public DownloadException() : base()

        {

        }

        /// <summary>
        /// Argument constructor
        /// </summary>
        /// <param name="message">This is the description of the exception</param>
        public DownloadException(String message) : base(message)

        {

        }

        /// <summary>
        /// Argument constructor with inner exception
        /// </summary>
        /// <param name="message">This is the description of the exception</param>
        /// <param name="innerException">Inner exception</param>
        public DownloadException(String message, Exception innerException) : base(message, innerException)

        {

        }

        /// <summary>
        /// Argument constructor with serialization support
        /// </summary>
        /// <param name="info">Instance of SerializationInfo</param>
        /// <param name="context">Instance of StreamingContext</param>
        protected DownloadException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }
}
