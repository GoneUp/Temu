// Type: Hik.Communication.ScsServices.Service.ScsServiceAttribute
// Assembly: Scs, Version=1.1.0.1, Culture=neutral, PublicKeyToken=null
// MVID: A2E8A751-E997-4D34-AEF6-43942DCF18A6
// Assembly location: C:\Users\sh4m4_000\Desktop\PjS1Server\build\Scs.dll

using System;

namespace Hik.Communication.ScsServices.Service
{
    /// <summary>
    /// Any SCS Service interface class must has this attribute.
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
    public class ScsServiceAttribute : Attribute
    {
        /// <summary>
        /// Service Version. This property can be used to indicate the code version.
        ///             This value is sent to client application on an exception, so, client application can know that service version is changed.
        ///             Default value: NO_VERSION.
        /// 
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// Creates a new ScsServiceAttribute object.
        /// 
        /// </summary>
        public ScsServiceAttribute()
        {
            this.Version = "NO_VERSION";
        }
    }
}
