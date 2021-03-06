﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using AampLibraryCSharp.IO;

namespace AampLibraryCSharp
{
    internal class ParamObjectV1 
    {
        internal static ParamObject Read(FileReader reader)
        {
            ParamObject entry = new ParamObject();

            long CurrentPosition = reader.Position;

            uint Size = reader.ReadUInt32();
            uint EntryCount = reader.ReadUInt32();
            entry.Hash = reader.ReadUInt32();
            entry.GroupHash = reader.ReadUInt32();

            entry.paramEntries = new ParamEntry[EntryCount];
            for (int i = 0; i < EntryCount; i++)
                entry.paramEntries[i] = ParamEntryV1.Read(reader);

            reader.Seek(CurrentPosition + Size, SeekOrigin.Begin);
            return entry;
        }

        internal static void Write(ParamObject entry, FileWriter writer)
        {
            int EntryCount = entry.paramEntries == null ? 0 : entry.paramEntries.Length;

            long startPosition = writer.Position;

            writer.Write(uint.MaxValue); //Write the size after
            writer.Write(EntryCount);
            writer.Write(entry.Hash);
            writer.Write(entry.GroupHash);

            for (int i = 0; i < EntryCount; i++)
                ParamEntryV1.Write(entry.paramEntries[i], writer);

            writer.WriteSize(writer.Position, startPosition);
        }
    }
}
