using System;
using System.IO;
using System.Reflection;

namespace Airports.Dev.Exercise.Services.Shared
{
    public static class Utility
    {
        public static string GetExecutingDirectoryName()
        {
            return AppDomain.CurrentDomain.BaseDirectory;
            //var location = new Uri(Assembly.GetEntryAssembly().GetName().CodeBase);
            //return new FileInfo(location.AbsolutePath).Directory?.FullName;
        }
    }
}
