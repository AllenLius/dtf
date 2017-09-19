using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Resources;

namespace Dta.Core
{
    public class ManagedResourceReader : IResourceReader
    {
        readonly long ResourcesNamePosition;
        readonly long ResourcesDataSectionPosition;
        readonly UInt32 NumberOfResources;
        Dictionary<object, object> m_resources = new Dictionary<object, object>();
        const UInt32 RESOURCES_MAGIC_NUMBER = 0xBEEFCACE;
        const int DOS_HEADER_SIZE = 128;
        const int PE_SIGNATURE_SIZE = 4;
        const int FILE_HEADER_SIZE = 20;
        const int SECTION_COUNT_OFFSET = 2;
        const int DATA_DIRECTORIES_OFFSET = 96;
        const int DATA_DIRECTORIY_SIZE = 8;
        const int DATA_DIRECTORIES_CLI_HEADER_INDEX = 14;
        const int OPTIONAL_HEADER_SIZE = 224;
        const int SECTION_HEADER_SIZE = 40;
        const int CLI_HEADER_RESOURCES_OFFSET = 24;

        public ManagedResourceReader(Stream stream)
        {
            using (BinaryReader binaryReader = new BinaryReader(stream))
            {
                #region Init

                // CLI header->resources
                long fileHeaderPosition = DOS_HEADER_SIZE + PE_SIGNATURE_SIZE;
                long optionHeaderPosition = fileHeaderPosition + FILE_HEADER_SIZE;
                long dataDirectoriesPosition = optionHeaderPosition + DATA_DIRECTORIES_OFFSET;
                long sectionHeaderPosition = optionHeaderPosition + OPTIONAL_HEADER_SIZE;
                // get section count
                binaryReader.BaseStream.Seek(fileHeaderPosition + SECTION_COUNT_OFFSET, SeekOrigin.Begin);
                UInt16 sectionCount = binaryReader.ReadUInt16();
                //reader.BaseStream.Seek(optionHeaderPosition + DATA_DIRECTORIES_COUNT_OFFSET, SeekOrigin.Begin);
                // to CLI header (COR20)
                binaryReader.BaseStream.Seek(dataDirectoriesPosition + DATA_DIRECTORIY_SIZE * DATA_DIRECTORIES_CLI_HEADER_INDEX, SeekOrigin.Begin);
                UInt32 cliRva = binaryReader.ReadUInt32();
                UInt32 cilSize = binaryReader.ReadUInt32();
                binaryReader.BaseStream.Seek(sectionHeaderPosition, SeekOrigin.Begin);
                // get section header list inorder to caculate file offset from rva
                SectionHeader[] sectionHeaders = new SectionHeader[sectionCount];
                for (int i = 0; i < sectionHeaders.Length; i++)
                {
                    sectionHeaders[i] = new SectionHeader(binaryReader);
                    binaryReader.BaseStream.Seek(SectionHeader.SIZE, SeekOrigin.Current);
                }
                long cliPosition = Rva2Offset(cliRva, sectionHeaders);
                //reader.BaseStream.Seek(cliPosition + CLI_HEADER_METADATA_OFFSET, SeekOrigin.Begin);
                //UInt32 metaDataRva = reader.ReadUInt32();
                //UInt32 metaDataSize = reader.ReadUInt32();
                //long metaDataPosition = Rva2Offset(metaDataRva, sectionHeaders);
                binaryReader.BaseStream.Seek(cliPosition + CLI_HEADER_RESOURCES_OFFSET, SeekOrigin.Begin);
                UInt32 resourcesRva = binaryReader.ReadUInt32();
                UInt32 resourceSize = binaryReader.ReadUInt32();
                long resourcesPosition = Rva2Offset(resourcesRva, sectionHeaders);
                binaryReader.BaseStream.Seek(resourcesPosition, SeekOrigin.Begin);
                UInt32 resourcesLen = binaryReader.ReadUInt32();
                long resourcesBasePosition = binaryReader.BaseStream.Position;

                UInt32 resourcesMagic = binaryReader.ReadUInt32();
                if (RESOURCES_MAGIC_NUMBER != resourcesMagic)
                {
                    throw new BadImageFormatException("Resource file is invalid!");
                }
                UInt32 numberOfReaderTypes = binaryReader.ReadUInt32();
                UInt32 sizeOfReaderTypes = binaryReader.ReadUInt32();
                // skip reader types
                binaryReader.BaseStream.Seek(sizeOfReaderTypes, SeekOrigin.Current);
                UInt32 version = binaryReader.ReadUInt32();
                NumberOfResources = binaryReader.ReadUInt32();
                UInt32 numberOfTypes = binaryReader.ReadUInt32();
                // skipe types
                for (int i = 0; i < numberOfTypes; i++)
                {
                    int len;
                    int size;
                    GetInt7Bit(binaryReader, out len, out size);
                    binaryReader.BaseStream.Seek(len + size, SeekOrigin.Current);
                }
                // align pos (8 bytes) base after Resources magic
                long currentOffset = binaryReader.BaseStream.Position - resourcesBasePosition;
                long align = currentOffset & 7;
                if (align != 0)
                {
                    binaryReader.BaseStream.Seek(8 - align, SeekOrigin.Current);
                }
                // skip name hashes (4 bit each)
                UInt32 hashesLen = sizeof(UInt32) * NumberOfResources;
                // offset used to access resources by index
                long resourcesNameOffset = binaryReader.BaseStream.Seek(hashesLen, SeekOrigin.Current);
                // skip name position
                binaryReader.BaseStream.Seek(sizeof(UInt32) * NumberOfResources, SeekOrigin.Current);

                UInt32 resourcesDataSectionOffset = binaryReader.ReadUInt32();
                ResourcesNamePosition = binaryReader.BaseStream.Position;
                // base address of data
                ResourcesDataSectionPosition = resourcesBasePosition + resourcesDataSectionOffset;

                #endregion
                LoadResources(binaryReader);
            }            
        }

        private void LoadResources(BinaryReader m_reader)
        {
            for (int i = 0; i < NumberOfResources; i++)
            {
                int n;
                int size;
                GetInt7Bit(m_reader, out n, out size);
                m_reader.BaseStream.Seek(size, SeekOrigin.Current);
                byte[] buf = m_reader.ReadBytes(n);
                string name = Encoding.Unicode.GetString(buf, 0, buf.Length);
                UInt32 valueOffset = m_reader.ReadUInt32();
                long nextNamePosition = m_reader.BaseStream.Position;
                long valuePosition = ResourcesDataSectionPosition + valueOffset;
                m_reader.BaseStream.Seek(valuePosition, SeekOrigin.Begin);
                byte resType = m_reader.ReadByte();
                GetInt7Bit(m_reader, out n, out size);
                m_reader.BaseStream.Seek(size, SeekOrigin.Current);
                byte[] valueBuf = m_reader.ReadBytes(n);
                if ((ResourceType)resType == ResourceType.StringType)
                {
                    string value = Encoding.UTF8.GetString(valueBuf, 0, valueBuf.Length);
                    m_resources.Add(name, value);
                }
                m_reader.BaseStream.Seek(nextNamePosition, SeekOrigin.Begin);
            }
        }

        public void Close()
        {
        }

        public System.Collections.IDictionaryEnumerator GetEnumerator()
        {
            return m_resources.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return m_resources.GetEnumerator();
        }

        public void Dispose()
        {
        }

        private static long Rva2Offset(UInt32 rva, SectionHeader[] sectionHeaders)
        {
            foreach (SectionHeader sh in sectionHeaders)
            {
                if (sh.VirtualAddress <= rva && sh.VirtualAddress + sh.RawDataSize > rva)
                    return sh.RawDataPointer + (rva - sh.VirtualAddress);
            }
            throw new BadImageFormatException("The resource file is invalid!");
        }

        /// <summary>
        /// get int value of 7bit number array and return len in byte
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="value">int result</param>
        /// <param name="size">byte used to store int value</param>
        private static void GetInt7Bit(BinaryReader reader, out int value, out int size)
        {
            long pos = reader.BaseStream.Position;
            byte num3;
            int num = 0;
            int num2 = 0;
            int s = 0;
            do
            {
                if (num2 == 0x23)
                {
                    throw new FormatException("Format_Bad7BitInt32");
                }
                num3 = reader.ReadByte();
                num |= (num3 & 0x7f) << num2;
                num2 += 7;
                s++;
            }
            while ((num3 & 0x80) != 0);

            value = num;
            size = s;
            reader.BaseStream.Seek(pos, SeekOrigin.Begin);
        }

        private enum ResourceType
        {
            StringType = 0x01,
        }

        private class SectionHeader
        {
            public const int SIZE = 40;
            private string m_name;
            private UInt32 m_virtualAddress;
            private UInt32 m_rawDataSize;
            private UInt32 m_rawDataPointer;

            public SectionHeader(BinaryReader reader)
            {
                long pos = reader.BaseStream.Position;
                for (int i = 0; i < 8; ++i)
                {
                    byte b = reader.ReadByte();
                    if (b != 0)
                        m_name += (char)b;
                }
                reader.BaseStream.Seek(4, SeekOrigin.Current);
                m_virtualAddress = reader.ReadUInt32();
                m_rawDataSize = reader.ReadUInt32();
                m_rawDataPointer = reader.ReadUInt32();
                reader.BaseStream.Position = pos;
            }

            public string Name
            {
                get { return m_name; }
            }

            public UInt32 VirtualAddress
            {
                get { return m_virtualAddress; }
            }

            public UInt32 RawDataSize
            {
                get { return m_rawDataSize; }
            }

            public UInt32 RawDataPointer
            {
                get { return m_rawDataPointer; }
            }
        }
    }
}