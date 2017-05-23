using Extensions.Data;
using System.IO;
using AltoHttp;


namespace CyberByte.ArmaAdmin.Launcher
{
    class Download
    {
        private dynamic files;
        public HttpDownloadQueue downloadQueue = new HttpDownloadQueue();

        private void GetFiles()
        {
            files = (dynamic)Requests.Get("api/v1/files");
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
