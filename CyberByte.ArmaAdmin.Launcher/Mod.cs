using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace CyberByte.ArmaAdmin.Launcher
{
    class Mods
    {
        private List<Mod> mods = new List<Mod>();

        public void add(Mod mod)
        {
            mods.Add(mod);
        }

        public List<Mod> get()
        {
            return mods;
        }

        public Mod getFile(int index)
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
        public UInt64 Hash { get;  }
        public String Url { get;  }
        public String Created { get; }

        public Mod(string id, string name, UInt64 size, string rel_path, ulong hash, string url, string timestamp)
        {
            Id = id;
            Name = name;
            Size = size;
            Relative_Path = rel_path;
            Hash = hash;
            Url = url;
            Created = timestamp;
        }

        public void info()
        {
            Debug.WriteLine("MOD {0}, ID {1}, Hash {2}, Size {3}, Relative Path {4}, URL {5}, Created {6}", Name, Id, Hash, Size, Relative_Path, Url, Created);
        }
    }
}
