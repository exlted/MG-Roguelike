using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL2D.Exceptions
{

    [Serializable]
    public class CameraNotImplementedException : Exception
    {
        public CameraNotImplementedException() { }
        public CameraNotImplementedException(string message) : base(message) { }
        public CameraNotImplementedException(string message, Exception inner) : base(message, inner) { }
        protected CameraNotImplementedException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context)
        { }
    }
}
