﻿using FluentLogger.Interfaces;
using System;
using Jsonite;

namespace FluentLogger
{
    public abstract class BaseLogger : ILog
    {
        protected readonly LogLevel minLevel;
        public Func<string, LogLevel, Exception, object[], string> Format { get; set; } = new Func<string, LogLevel, Exception, object[], string>((mesg, logLevel, ex, objects) =>
         {
             var logLine = DateTime.Now.ToString("hh:mm:ss") + "[" + logLevel.ToString().ToUpper() + "] " + mesg + Environment.NewLine;
             if (ex != null)
             {
                 logLine += "\t\t\t" + ex.Message + Environment.NewLine + ex.StackTrace + Environment.NewLine;
                 if (ex is AggregateException)
                 {
                     var aggEx = ex as AggregateException;
                     logLine += aggEx.Flatten().Message;
                 }
             }
             foreach (var obj in objects)
                 logLine += Json.Serialize(obj) + Environment.NewLine;
             return logLine;
         });
        public BaseLogger(LogLevel minLevel)
        {
            this.minLevel = minLevel;
        }

        public abstract void Record(LogLevel level, string message, Exception ex = null, params object[] objectsToSerialize);

        private void Filter(LogLevel level, string message, Exception ex = null, params object[] objectsToSerialize)
        {
            if (level < minLevel) return;
            Record(level, message, ex, objectsToSerialize);
        }

        public void Error(string message, Exception ex)
        {
            Filter(LogLevel.Error, message, ex);
        }

        public void Error(Exception ex)
        {
            Filter(LogLevel.Error, null, ex);
        }
        public void Fatal(string message, Exception ex)
        {
            Filter(LogLevel.Fatal, message, ex);
        }
        public void Fatal(Exception ex)
        {
            Filter(LogLevel.Fatal, null, ex);
        }

        public void Info(string message)
        {
            Filter(LogLevel.Info, message);
        }

        public void Trace(string message)
        {
            Filter(LogLevel.Trace, message);
        }
        public void Warn(Exception ex)
        {
            Filter(LogLevel.Warn, null, ex);
        }
        public void Warn(string message)
        {
            Filter(LogLevel.Warn, message);
        }

        public void Warn(string message, Exception ex)
        {
            Filter(LogLevel.Warn, message, ex);
        }

        public bool IsEnabled(LogLevel level)
        {
            return (level >= minLevel);
        }

        public virtual void Dispose()
        {
        }

    

        public void Trace(string message, params object[] objects)
        {
            Filter(LogLevel.Trace, message, null, objects);
        }

        public void Info(string message, params object[] objects)
        {
            Filter(LogLevel.Info, message, null, objects);
        }

        public void Warn(string message, params object[] objects)
        {
            Filter(LogLevel.Warn, message, null, objects);
        }

        public void Warn(string message, Exception ex, params object[] objects)
        {
            Filter(LogLevel.Warn, message, ex, objects);
         }

        public void Error(string message)
        {
            Filter(LogLevel.Error, message);
        }

        public void Error(string message, params object[] objects)
        {
            Filter(LogLevel.Error, message, null, objects);
        }

        public void Error(string message, Exception ex, params object[] objects)
        {
            Filter(LogLevel.Error, message, ex, objects);
        }

        public void Critical(string message)
        {
            Filter(LogLevel.Critical, message);
        }

        public void Critical(string message, params object[] objects)
        {
            Filter(LogLevel.Critical, message, null, objects);
        }



        public void Critical(string message, Exception ex)
        {
            Filter(LogLevel.Critical, message, ex);
        }

        public void Critical(string message, Exception ex, params object[] objects)
        {
            Filter(LogLevel.Critical, message, ex, objects);
        }

        public void Critical(Exception ex)
        {
            Filter(LogLevel.Critical, null, ex);
        }

        public void Fatal(string message, params object[] objects)
        {
            Filter(LogLevel.Fatal, message, null, objects);
        }

        public void Fatal(string message, Exception ex, params object[] objects)
        {
            Filter(LogLevel.Fatal, message, ex, objects);
        }
    }
}
