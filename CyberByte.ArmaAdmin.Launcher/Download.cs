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
        public static HttpDownloadQueue downloadQueue = new HttpDownloadQueue();
        private static List<FileInfo> files = new List<FileInfo>();
        private static string BaseDir = "D:\\Test";
        private static int BaseDirLength = 0;

        private static Mods GetFiles()
        {
            Task<Mods> modsTask = Task<Mods>.Factory.StartNew(() =>
            {
                dynamic files = (dynamic)Requests.Get("api/v1/files");

                Mods mods = new Mods();
                dynamic jsonMods = files.results;

                //Typecasting is done as each item is just a JValue

                for (int i = 0; i < jsonMods.Count; i++)
                {
                    Mod mod = new Mod(
                        (string)jsonMods[i].id,
                        (string)jsonMods[i].filename,
                        (ulong)jsonMods[i].size,
                        (string)jsonMods[i].relative_path,
                        (string)jsonMods[i].hash,
                        (string)jsonMods[i].download,
                        (string)jsonMods[i].created);

                    mod.Info(); //Just prints out all the info

                    mods.Add(mod);
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
        private static string HashFile(string path)
        {
            Stream stream = File.OpenRead(path);
            XXHash.State64 state = XXHash.CreateState64();

            XXHash.UpdateState64(state, stream);

            ulong result = XXHash.DigestState64(state);
            return result.ToString("X");
        }

        /// <summary>
        /// Loops through all the files and subfolders and setups a list of files
        /// </summary>
        /// <param name="root">DirectoryInfo path</param>
        private static void WalkDirectoryTree(DirectoryInfo root)
        {
            FileInfo[] dirFiles = null;
            DirectoryInfo[] subDirs = null;

            try
            {
                dirFiles = root.GetFiles("*.*"); //All the files in this Directory
            }
            catch (UnauthorizedAccessException ex)
            {
                Debug.WriteLine(ex.ToString());
            }
            catch (DirectoryNotFoundException ex)
            {
                Debug.WriteLine(ex.ToString());
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

        /// <summary>
        /// Loops through all the files and mods passed to it and compares file hash to mod hash to determin if the file needs to be downloaded
        /// </summary>
        /// <param name="mods">List of mods</param>
        /// <param name="fileList">List of file info</param>
        private static bool CalcFilesToDownload(List<Mod> mods, List<FileInfo> fileList)
        {
            foreach(FileInfo file in fileList)
            {
                string relPath = file.FullName.Substring(BaseDirLength);
                relPath = relPath.Replace(@"\", "/");

                Mod modFile = mods.FirstOrDefault(mod => mod.Relative_Path == relPath);

                if (modFile == null)
                {
                    try
                    {
                        if (File.Exists(file.FullName)) File.Delete(file.FullName);
                    }
                    catch
                    {
                        return false;
                    }
                    continue;
                }

                if (HashFile(file.FullName) != modFile.Hash)
                {
                    downloadQueue.Add(modFile.Url, file.FullName);
                    mods.Remove(modFile);
                }
                else
                {
                    //The File we found on disk is current so remove it from the mods array to prevent it being redownloaded
                    mods.Remove(modFile);
                }
            }

            foreach (Mod file in mods)
            {
                downloadQueue.Add(file.Url, Path.Combine(BaseDir, file.Relative_Path));
            }

            return true;
        }

        public static void Resume()
        {
            downloadQueue.ResumeAsync();
        }

        public static void Pause()
        {
            downloadQueue.Pause();
        }

        public static void Start()
        {
            downloadQueue.StartAsync();
        }
         
        public static void Setup()
        {
            Mods mods = GetFiles();
            BaseDirLength = BaseDir.Length; //Replace with fetch from config
            DirectoryInfo dirInf = new DirectoryInfo(BaseDir);

            WalkDirectoryTree(dirInf);

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Task<bool> calFileTask = Task<bool>.Factory.StartNew(() =>
            {
                return CalcFilesToDownload(mods.Get(), files);
            });

            if (calFileTask.Result)
            {
                stopWatch.Stop();
                TimeSpan ts = stopWatch.Elapsed;
                string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                Console.WriteLine("Time Taken to Build Download Queue" + elapsedTime);
            }
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
