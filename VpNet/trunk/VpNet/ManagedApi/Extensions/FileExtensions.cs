using System.IO;

namespace VpNet.Extensions
{
    public static class FileExtensions
    {
        public static string LoadTextFile(this string path)
        {
            using (var sr = new StreamReader(path)) return sr.ReadToEnd();
        }       

        public static string LoadTextFile(this FileInfo path)
        {
            using (var sr = new StreamReader(path.FullName)) return sr.ReadToEnd();
        }

        public static void SaveTextFile(this FileInfo path, string contents)
        {
            using (var sw = new StreamWriter(path.FullName)) { sw.Write(contents); };
        }

        public static void SaveTextFile(this string contents, string path)
        {
            using (var sw = new StreamWriter(path)) { sw.Write(contents); };
        }
    }
}
