// Type: Hik.Communication.Scs.Communication.Protocols.BinarySerialization.BinarySerializationProtocol
// Assembly: Scs, Version=1.1.0.1, Culture=neutral, PublicKeyToken=null
// MVID: A2E8A751-E997-4D34-AEF6-43942DCF18A6
// Assembly location: C:\Users\sh4m4_000\Desktop\PjS1Server\build\Scs.dll

using NetworkApi.Communication.Scs.Communication;
using NetworkApi.Communication.Scs.Communication.Messages;
using NetworkApi.Communication.Scs.Communication.Protocols;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;

namespace NetworkApi.Communication.Scs.Communication.Protocols.BinarySerialization
{
    /// <summary>
    /// Default communication protocol between server and clients to send and receive a message.
    ///             It uses .NET binary serialization to write and read messages.
    /// 
    ///             A Message format:
    ///             [Message Length (4 bytes)][Serialized Message Content]
    /// 
    ///             If a message is serialized to byte array as N bytes, this protocol
    ///             adds 4 bytes size information to head of the message bytes, so total length is (4 + N) bytes.
    /// 
    ///             This class can be derived to change serializer (default: BinaryFormatter). To do this,
    ///             SerializeMessage and DeserializeMessage methods must be overrided.
    /// 
    /// </summary>
    public class BinarySerializationProtocol : IScsWireProtocol
    {
        /// <summary>
        /// Maximum length of a message.
        /// 
        /// </summary>
        private const int MaxMessageLength = 134217728;
        /// <summary>
        /// This MemoryStream object is used to collect receiving bytes to build messages.
        /// 
        /// </summary>
        private MemoryStream _receiveMemoryStream;

        /// <summary>
        /// Creates a new instance of BinarySerializationProtocol.
        /// 
        /// </summary>
        public BinarySerializationProtocol()
        {
            this._receiveMemoryStream = new MemoryStream();
        }

        /// <summary>
        /// Serializes a message to a byte array to send to remote application.
        ///             This method is synchronized. So, only one thread can call it concurrently.
        /// 
        /// </summary>
        /// <param name="message">Message to be serialized</param><exception cref="T:Hik.Communication.Scs.Communication.CommunicationException">Throws CommunicationException if message is bigger than maximum allowed message length.</exception>
        public byte[] GetBytes(IScsMessage message)
        {
            byte[] numArray = this.SerializeMessage(message);
            int length = numArray.Length;
            if (length > 134217728)
            {
                throw new CommunicationException("Message is too big (" + (object)length + " bytes). Max allowed length is " + (string)(object)134217728 + " bytes.");
            }
            else
            {
                byte[] buffer = new byte[length + 4];
                BinarySerializationProtocol.WriteInt32(buffer, 0, length);
                Array.Copy((Array)numArray, 0, (Array)buffer, 4, length);
                return buffer;
            }
        }

        /// <summary>
        /// Builds messages from a byte array that is received from remote application.
        ///             The Byte array may contain just a part of a message, the protocol must
        ///             cumulate bytes to build messages.
        ///             This method is synchronized. So, only one thread can call it concurrently.
        /// 
        /// </summary>
        /// <param name="receivedBytes">Received bytes from remote application</param>
        /// <returns>
        /// List of messages.
        ///             Protocol can generate more than one message from a byte array.
        ///             Also, if received bytes are not sufficient to build a message, the protocol
        ///             may return an empty list (and save bytes to combine with next method call).
        /// 
        /// </returns>
        public IEnumerable<IScsMessage> CreateMessages(byte[] receivedBytes)
        {
            this._receiveMemoryStream.Write(receivedBytes, 0, receivedBytes.Length);
            List<IScsMessage> list = new List<IScsMessage>();
            do
                continue;
            while (this.ReadSingleMessage((ICollection<IScsMessage>)list));
            return (IEnumerable<IScsMessage>)list;
        }

        /// <summary>
        /// This method is called when connection with remote application is reset (connection is renewing or first connecting).
        ///             So, wire protocol must reset itself.
        /// 
        /// </summary>
        public void Reset()
        {
            if (this._receiveMemoryStream.Length <= 0L)
                return;
            this._receiveMemoryStream = new MemoryStream();
        }

        /// <summary>
        /// This method is used to serialize a IScsMessage to a byte array.
        ///             This method can be overrided by derived classes to change serialization strategy.
        ///             It is a couple with DeserializeMessage method and must be overrided together.
        /// 
        /// </summary>
        /// <param name="message">Message to be serialized</param>
        /// <returns>
        /// Serialized message bytes.
        ///             Does not include length of the message.
        /// 
        /// </returns>
        protected virtual byte[] SerializeMessage(IScsMessage message)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                new BinaryFormatter().Serialize((Stream)memoryStream, (object)message);
                return memoryStream.ToArray();
            }
        }

        /// <summary>
        /// This method is used to deserialize a IScsMessage from it's bytes.
        ///             This method can be overrided by derived classes to change deserialization strategy.
        ///             It is a couple with SerializeMessage method and must be overrided together.
        /// 
        /// </summary>
        /// <param name="bytes">Bytes of message to be deserialized (does not include message length. It consist
        ///             of a single whole message)
        ///             </param>
        /// <returns>
        /// Deserialized message
        /// </returns>
        protected virtual IScsMessage DeserializeMessage(byte[] bytes)
        {
            using (MemoryStream memoryStream = new MemoryStream(bytes))
            {
                memoryStream.Position = 0L;
                return (IScsMessage)new BinaryFormatter()
                {
                    AssemblyFormat = FormatterAssemblyStyle.Simple,
                    Binder = ((SerializationBinder)new BinarySerializationProtocol.DeserializationAppDomainBinder())
                }.Deserialize((Stream)memoryStream);
            }
        }

        /// <summary>
        /// This method tries to read a single message and add to the messages collection.
        /// 
        /// </summary>
        /// <param name="messages">Messages collection to collect messages</param>
        /// <returns>
        /// Returns a boolean value indicates that if there is a need to re-call this method.
        /// 
        /// </returns>
        /// <exception cref="T:Hik.Communication.Scs.Communication.CommunicationException">Throws CommunicationException if message is bigger than maximum allowed message length.</exception>
        private bool ReadSingleMessage(ICollection<IScsMessage> messages)
        {
            this._receiveMemoryStream.Position = 0L;
            if (this._receiveMemoryStream.Length < 4L)
                return false;
            int length = BinarySerializationProtocol.ReadInt32((Stream)this._receiveMemoryStream);
            if (length > 134217728)
                throw new Exception("Message is too big (" + (object)length + " bytes). Max allowed length is " + (string)(object)134217728 + " bytes.");
            else if (length == 0)
            {
                if (this._receiveMemoryStream.Length == 4L)
                {
                    this._receiveMemoryStream = new MemoryStream();
                    return false;
                }
                else
                {
                    byte[] buffer = this._receiveMemoryStream.ToArray();
                    this._receiveMemoryStream = new MemoryStream();
                    this._receiveMemoryStream.Write(buffer, 4, buffer.Length - 4);
                    return true;
                }
            }
            else if (this._receiveMemoryStream.Length < (long)(4 + length))
            {
                this._receiveMemoryStream.Position = this._receiveMemoryStream.Length;
                return false;
            }
            else
            {
                byte[] bytes = BinarySerializationProtocol.ReadByteArray((Stream)this._receiveMemoryStream, length);
                messages.Add(this.DeserializeMessage(bytes));
                byte[] buffer = BinarySerializationProtocol.ReadByteArray((Stream)this._receiveMemoryStream, (int)(this._receiveMemoryStream.Length - (long)(4 + length)));
                this._receiveMemoryStream = new MemoryStream();
                this._receiveMemoryStream.Write(buffer, 0, buffer.Length);
                return buffer.Length > 4;
            }
        }

        /// <summary>
        /// Writes a int value to a byte array from a starting index.
        /// 
        /// </summary>
        /// <param name="buffer">Byte array to write int value</param><param name="startIndex">Start index of byte array to write</param><param name="number">An integer value to write</param>
        private static void WriteInt32(byte[] buffer, int startIndex, int number)
        {
            buffer[startIndex] = (byte)(number >> 24 & (int)byte.MaxValue);
            buffer[startIndex + 1] = (byte)(number >> 16 & (int)byte.MaxValue);
            buffer[startIndex + 2] = (byte)(number >> 8 & (int)byte.MaxValue);
            buffer[startIndex + 3] = (byte)(number & (int)byte.MaxValue);
        }

        /// <summary>
        /// Deserializes and returns a serialized integer.
        /// 
        /// </summary>
        /// 
        /// <returns>
        /// Deserialized integer
        /// </returns>
        private static int ReadInt32(Stream stream)
        {
            byte[] numArray = BinarySerializationProtocol.ReadByteArray(stream, 4);
            return (int)numArray[0] << 24 | (int)numArray[1] << 16 | (int)numArray[2] << 8 | (int)numArray[3];
        }

        /// <summary>
        /// Reads a byte array with specified length.
        /// 
        /// </summary>
        /// <param name="stream">Stream to read from</param><param name="length">Length of the byte array to read</param>
        /// <returns>
        /// Read byte array
        /// </returns>
        /// <exception cref="T:System.IO.EndOfStreamException">Throws EndOfStreamException if can not read from stream.</exception>
        private static byte[] ReadByteArray(Stream stream, int length)
        {
            byte[] buffer = new byte[length];
            int offset = 0;
            while (offset < length)
            {
                int num = stream.Read(buffer, offset, length - offset);
                if (num <= 0)
                    throw new EndOfStreamException("Can not read from stream! Input stream is closed.");
                offset += num;
            }
            return buffer;
        }

        /// <summary>
        /// This class is used in deserializing to allow deserializing objects that are defined
        ///             in assemlies that are load in runtime (like PlugIns).
        /// 
        /// </summary>
        protected sealed class DeserializationAppDomainBinder : SerializationBinder
        {
            public override Type BindToType(string assemblyName, string typeName)
            {
                string toAssemblyName = assemblyName.Split(new char[1]
        {
          ','
        })[0];
                return Enumerable.FirstOrDefault<Type>(Enumerable.Select<Assembly, Type>(Enumerable.Where<Assembly>((IEnumerable<Assembly>)AppDomain.CurrentDomain.GetAssemblies(), (Func<Assembly, bool>)(assembly => assembly.FullName.Split(new char[1]
        {
          ','
        })[0] == toAssemblyName)), (Func<Assembly, Type>)(assembly => assembly.GetType(typeName))));
            }
        }
    }
}
