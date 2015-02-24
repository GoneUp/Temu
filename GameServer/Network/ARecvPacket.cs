using System;
using System.IO;
using System.Text;
using Tera.Data.Interfaces;
using Utils.Logger;

namespace Tera.Network
{
    public abstract class ARecvPacket : IRecvPacket
    {
        public BinaryReader Reader;
        public Connection Connection;

        public void Process(Connection connection)
        {
            Connection = connection;

            using (Reader = new BinaryReader(new MemoryStream(connection.Buffer)))
            {
                Read();
            }

            try
            {
                Process();
            }
            catch (Exception ex)
            {
                Logger.WriteLine(LogState.Warn, "ARecvPacket:" + ex);
            }
        }

        public abstract void Read();

        public abstract void Process();

        protected int ReadDword()
        {
            try
            {
                return Reader.ReadInt32();
            }
            catch (Exception)
            {
                Logger.WriteLine(LogState.Warn,"Missing D for: {0}", GetType());
            }
            return 0;
        }

        protected int ReadByte()
        {
            try
            {
                return Reader.ReadByte() & 0xFF;
            }
            catch (Exception)
            {
                Logger.WriteLine(LogState.Warn,"Missing C for: {0}", GetType());
            }
            return 0;
        }

        protected int ReadWord()
        {
            try
            {
                return Reader.ReadInt16() & 0xFFFF;
            }
            catch (Exception)
            {
                Logger.WriteLine(LogState.Warn,"Missing H for: {0}", GetType());
            }
            return 0;
        }

        protected double ReadDouble()
        {
            try
            {
                return Reader.ReadDouble();
            }
            catch (Exception)
            {
                Logger.WriteLine(LogState.Warn,"Missing DF for: {0}", GetType());
            }
            return 0;
        }

        protected float Single()
        {
            try
            {
                return Reader.ReadSingle();
            }
            catch (Exception)
            {
                Logger.WriteLine(LogState.Warn,"Missing F for: {0}", GetType());
            }
            return 0;
        }

        protected long ReadLong()
        {
            try
            {
                return Reader.ReadInt64();
            }
            catch (Exception)
            {
                Logger.WriteLine(LogState.Warn,"Missing Q for: {0}", GetType());
            }
            return 0;
        }

        protected String ReadString()
        {
            Encoding encoding = Encoding.Unicode;
            String result = "";
            try
            {
                short ch;
                while ((ch = Reader.ReadInt16()) != 0)
                    result += encoding.GetString(BitConverter.GetBytes(ch));
            }
            catch (Exception)
            {
                Logger.WriteLine(LogState.Warn,"Missing S for: {0}", GetType());
            }
            return result;
        }

        protected byte[] ReadByte(int length)
        {
            byte[] result = new byte[length];
            try
            {
                Reader.Read(result, 0, length);
            }
            catch (Exception)
            {
                Logger.WriteLine(LogState.Warn,"Missing byte[] for: {0}", GetType());
            }
            return result;
        }
    }
}