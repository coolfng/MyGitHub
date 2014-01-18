using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Logging.TraceListeners;
using Microsoft.Practices.EnterpriseLibrary.Logging.Configuration;
using System.Diagnostics;
using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace HKW.Practices.Public.Log
{
    /// <summary>
    /// Trace listener that writes formatted messages to the Visual Studio debugger output.
    /// </summary>
    [ConfigurationElementType(typeof(CustomTraceListenerData))]
    public class DebugTraceListener : CustomTraceListener
    {
        public override void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, object data)
        {
            if (data is LogEntry && Formatter != null)
            {
                WriteLine(Formatter.Format(data as LogEntry));
            }
            else
            {
                WriteLine(data.ToString());
            }
        }

        /// <summary>
        /// Writes a message to the debug window 
        /// </summary>
        /// <param name="message">The string to write to the debug window</param>
        public override void Write(string message)
        {
            Debug.Write(message);
        }

        /// <summary>
        /// Writes a message to the debug window 
        /// </summary>
        /// <param name="message">The string to write to the debug window</param>
        public override void WriteLine(string message)
        {
            Debug.WriteLine(message);
        }
    }
}
