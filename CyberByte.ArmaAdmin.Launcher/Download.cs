﻿using Extensions.Data;
using System.IO;
using AltoHttp;
using System.Diagnostics;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using System;
using System.Runtime.Serialization;

namespace CyberByte.ArmaAdmin.Launcher
{

    class Download
    {
        private static dynamic files;
        public HttpDownloadQueue downloadQueue = new HttpDownloadQueue();

        public static Mods GetFiles()
        {
            files = (dynamic)Requests.Get("api/v1/files");

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
                        (string)jsonMods[i].hash,
                        (string)jsonMods[i].download,
                        (string)jsonMods[i].created);

                    mod.info(); //Just prints out all the info

                    mods.add(mod);
                };

                return mods;
            });

            return modsTask.Result;
        }

        private ulong HashFile(string path)
        {
            Stream stream = File.OpenRead(@path);
            XXHash.State64 state = XXHash.CreateState64();

            XXHash.UpdateState64(state, stream);

            return XXHash.DigestState64(state);
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
