using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SNMini.Core
{
    public class AppUrl
    {
        public AppUrl(Uri baseUrl)
            : this (baseUrl, null)
        {
        }

        public AppUrl(Uri baseUrl, string path)
        {
            Scheme = baseUrl.Scheme;
            Host = baseUrl.Host;
            Port = baseUrl.Port;
            Path = path ?? baseUrl.AbsolutePath;
        }

        public string Scheme { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public string Path { get; set; }

        public override string ToString()
        {
            return Host == "localhost" ? String.Format("{0}://{1}:{2}{3}", Scheme, Host, Port, Path)
                                       : String.Format("{0}://{1}{2}", Scheme, Host, Path);
        }
    }
}