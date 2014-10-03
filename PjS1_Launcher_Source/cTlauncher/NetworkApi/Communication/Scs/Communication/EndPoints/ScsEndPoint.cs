// Type: Hik.Communication.Scs.Communication.EndPoints.ScsEndPoint
// Assembly: Scs, Version=1.1.0.1, Culture=neutral, PublicKeyToken=null
// MVID: A2E8A751-E997-4D34-AEF6-43942DCF18A6
// Assembly location: C:\Users\sh4m4_000\Desktop\PjS1Server\build\Scs.dll

using Hik.Communication.Scs.Client;
using Hik.Communication.Scs.Communication.EndPoints.Tcp;
using Hik.Communication.Scs.Server;
using System;

namespace Hik.Communication.Scs.Communication.EndPoints
{
    /// <summary>
    /// Represents a server side end point in SCS.
    /// 
    /// </summary>
    public abstract class ScsEndPoint
    {
        /// <summary>
        /// Create a Scs End Point from a string.
        ///             Address must be formatted as: protocol://address
        ///             For example: tcp://89.43.104.179:10048 for a TCP endpoint with
        ///             IP 89.43.104.179 and port 10048.
        /// 
        /// </summary>
        /// <param name="endPointAddress">Address to create endpoint</param>
        /// <returns>
        /// Created end point
        /// </returns>
        public static ScsEndPoint CreateEndPoint(string endPointAddress)
        {
            if (string.IsNullOrEmpty(endPointAddress))
                throw new ArgumentNullException("endPointAddress");
            string str1 = endPointAddress;
            if (!str1.Contains("://"))
                str1 = "tcp://" + str1;
            string[] strArray = str1.Split(new string[1]
      {
        "://"
      }, StringSplitOptions.RemoveEmptyEntries);
            if (strArray.Length != 2)
                throw new ApplicationException(endPointAddress + " is not a valid endpoint address.");
            string str2 = strArray[0].Trim().ToLower();
            string address = strArray[1].Trim();
            switch (str2)
            {
                case "tcp":
                    return (ScsEndPoint)new ScsTcpEndPoint(address);
                default:
                    throw new ApplicationException("Unsupported protocol " + str2 + " in end point " + endPointAddress);
            }
        }

        /// <summary>
        /// Creates a Scs Server that uses this end point to listen incoming connections.
        /// 
        /// </summary>
        /// 
        /// <returns>
        /// Scs Server
        /// </returns>
        internal abstract IScsServer CreateServer();

        /// <summary>
        /// Creates a Scs Server that uses this end point to connect to server.
        /// 
        /// </summary>
        /// 
        /// <returns>
        /// Scs Client
        /// </returns>
        internal abstract IScsClient CreateClient();
    }
}
