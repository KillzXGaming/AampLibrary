﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace AampV2Library
{
    class Hashes
    {
        //Based from https://github.com/jam1garner/aamp2xml

        public static Dictionary<uint, string> hashName = new Dictionary<uint, string>();

        private static void GenerateHashes()
        {
            foreach (string hashStr in Properties.Resources.aamp_hashed_names.Split('\n'))
            {
                uint hash = Crc32.Compute(hashStr);
                if (!hashName.ContainsKey(hash))
                    hashName.Add(hash, hashStr);
            }
            foreach (string hashStr in Properties.Resources.aamp_hashed_names_numbered.Split('\n'))
            {
                uint hash = Crc32.Compute(hashStr);
                if (!hashName.ContainsKey(hash))
                    hashName.Add(hash, hashStr);
            }
        }

        public static string GetName(uint hash)
        {
            if (hashName.Count == 0)
                GenerateHashes();
            string name = null;
            hashName.TryGetValue(hash, out name);

            if (name == null)
                return hash.ToString();

            return name;
        }
    }
}