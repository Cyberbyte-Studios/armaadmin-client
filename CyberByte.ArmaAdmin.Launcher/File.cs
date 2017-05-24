using System;
using System.Collections.Generic;

namespace CyberByte.ArmaAdmin.Launcher
{
    class Files
    {
        private List<File> files = new List<File>();

        public void add(File file)
        {
            files.Add(file);
        }

        public List<File> get()
        {
            return files;
        }

        public File getFile(int index)
        {
            return files[index];
        }
    }

    class File
    {
        String Id { get; }
        String Name { get;  }
        UInt64 Size { get; }
        String Relative_Path { get; }
        String Hash { get;  }
        String Url { get;  }
        String Created { get; }

        public File (string id, string name, UInt64 size, string rel_path, string hash, string url, string timestamp)
        {
            Id = id;
            Name = name;
            Size = size;
            Relative_Path = rel_path;
            Hash = hash;
            Url = url;
            Created = timestamp;
        }
    }
}
