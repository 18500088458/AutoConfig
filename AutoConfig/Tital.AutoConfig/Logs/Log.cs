using System;
using NLog;
using Tital.Logs;

namespace Tital.AutoConfig.Logs
{
    internal class Log : ILog
    {
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public void Debug(string msg, params object[] args)
        {
            _logger.Debug(msg, args);
        }

        public void Debug(Exception err, string msg, params object[] args)
        {
            _logger.Debug(err, msg, args);
        }

        public void Info(string msg, params object[] args)
        {
            _logger.Info(msg, args);
        }

        public void Info(Exception err, string msg, params object[] args)
        {
            _logger.Info(err, msg, args);
        }

        public void Trace(string msg, params object[] args)
        {
            _logger.Trace(msg, args);
        }

        public void Trace(Exception err, string msg, params object[] args)
        {
            _logger.Trace(err, msg, args);
        }

        public void Error(string msg, params object[] args)
        {
            _logger.Error(msg, args);
        }

        public void Error(Exception err, string msg, params object[] args)
        {
            _logger.Error(err, msg, args);
        }

        public void Fatal(string msg, params object[] args)
        {
            _logger.Fatal(msg, args);
        }

        public void Fatal(Exception err, string msg, params object[] args)
        {
            _logger.Fatal(err, msg, args);
        }
    }
}