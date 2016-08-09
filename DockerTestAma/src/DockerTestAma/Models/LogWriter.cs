using System;
using System.Diagnostics;

namespace DockerTestAma.Models
{
    /// <summary>
    /// LogWriter is a class that can only be made once, it allows logging of an error message.
    /// Singleton design pattern used
    /// Source: https://msdn.microsoft.com/en-us/library/ff650316.aspx
    /// </summary>
    public sealed class LogWriter
    {
        private static volatile LogWriter instance;
        private static object syncRoot = new object();

        /// <summary>
        /// Constructor of the LogWriter, created a single DataBaseTools
        /// </summary>
        private LogWriter()
        {
        }

        /// <summary>
        /// Checks if there is already an instance of this class created, returns this or otherwise returns a new instance.
        /// Using the syncRoot lock creates the object that is threadsafe.
        /// </summary>
        public static LogWriter Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new LogWriter();
                    }
                }

                return instance;
            }
        }

        /// <summary>
        /// Log a message to the filesystem
        /// </summary>
        /// <param name="message">Message that has to be logged</param>
        public void LogMessage(string message)
        {
            Debug.WriteLine(DateTime.Now.ToString("dd/MM/yyyy h:mm:ss tt") + "  :   " + message);
        }
    }
}
