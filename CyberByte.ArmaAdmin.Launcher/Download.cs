using Extensions.Data;
using System.IO;
using AltoHttp;
using System.Diagnostics;
using Newtonsoft.Json.Linq;

namespace CyberByte.ArmaAdmin.Launcher
{

    class Download
    {
        private static dynamic files;
        public HttpDownloadQueue downloadQueue = new HttpDownloadQueue();

        public static void GetFiles()
        {
            files = (dynamic)Requests.Get("api/v1/files");

            Debug.WriteLine(files.count);
        }

        private ulong HashFile(string path)
        {
            Stream stream = File.OpenRead(@path);
            XXHash.State64 state = XXHash.CreateState64();

            XXHash.UpdateState64(state, stream);

            return XXHash.DigestState64(state);
        }
    }
}
