using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace CyberByte.ArmaAdmin.Launcher
{
    class Mods
    {
        private List<Mod> mods = new List<Mod>();

        public void Add(Mod mod)
        {
            mods.Add(mod);
        }

        public List<Mod> Get()
        {
            return mods;
        }

        public Mod GetFile(int index)
        {
            return mods[index];
        }
    }

    class Mod
    {
        public String Id { get; }
        public String Name { get;  }
        public UInt64 Size { get; }
        public String Relative_Path { get; }
        public String Hash { get;  }
        public String Url { get;  }
        public String Created { get; }

        public Mod(string id, string name, UInt64 size, string rel_path, string hash, string url, string timestamp)
        {
            Id = id;
            Name = name;
            Size = size;
            Relative_Path = rel_path;
            Hash = hash;
            Url = url;
            Created = timestamp;
        }

        public void Info()
        {
            Debug.WriteLine("MOD {0}, ID {1}, Hash {2}, Size {3}, Relative Path {4}, URL {5}, Created {6}", Name, Id, Hash, Size, Relative_Path, Url, Created);
        }
    }
}
