using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Tobii_Eris_Library
{
    public class StatisticsLogger
    {
        private StreamWriter m_streamWriter;
        private string m_path;

        private DateTime m_startLogTime;
        //private DateTime m_earliestLogTime;
        private DateTime m_latestLogTime;

        public StatisticsLogger(string outputFilePath, string headerTitle = null, StringBuilder preLogComments = null)
        {
            m_path = outputFilePath;
            ClearFile();

            m_streamWriter = new StreamWriter(m_path);
            m_streamWriter.AutoFlush = true;

            if (null != headerTitle && "" != headerTitle)
            {
                m_streamWriter.WriteLine(headerTitle);
                m_streamWriter.WriteLine();
            }

            if (null != preLogComments)
            {
                m_streamWriter.WriteLine(preLogComments);
                m_streamWriter.WriteLine();
            }

            // Be sure that the logging start time gets recorded at the very end of this constructor
            //m_earliestLogTime = DateTime.MinValue;
            m_latestLogTime = DateTime.MinValue;
            m_startLogTime = DateTime.Now;
        }

        public void WriteLoggerDetails()
        {
            if (m_streamWriter.BaseStream != null)
            {
                m_streamWriter.WriteLine();
                m_streamWriter.WriteLine("Logging began at: " + m_startLogTime.ToString());
                //m_streamWriter.WriteLine("Earliest Log DateTime: " + m_earliestLogTime.ToString());
                m_streamWriter.WriteLine("Latest Log DateTime: " + m_latestLogTime.ToString());
                m_streamWriter.WriteLine("Logging Start Time and Latest Log Time difference: " + (m_latestLogTime - m_startLogTime).ToString());
                //m_streamWriter.WriteLine("Earliest Log Time and Latest Log Time difference: " + (m_latestLogTime - m_earliestLogTime).ToString());
                m_streamWriter.Close();
            }
        }

        public void ClearFile()
        {
            if (File.Exists(m_path))
            {
                FileStream f = File.Open(m_path, FileMode.Truncate);
                f.Close();
            }
        }

        public void LogData(string toLog)
        {
            DateTime currentTime = DateTime.Now;
            m_latestLogTime = currentTime;
            //if (DateTime.MinValue == m_earliestLogTime)
            //    m_earliestLogTime = currentTime;
            m_streamWriter.WriteLine(currentTime.ToString() + " : " + toLog);
        }

        public void AddComment(string commentToAdd)
        {
            m_streamWriter.WriteLine(commentToAdd);
        }

        public void AddNewLine()
        {
            m_streamWriter.WriteLine("");
        }
    }
}
