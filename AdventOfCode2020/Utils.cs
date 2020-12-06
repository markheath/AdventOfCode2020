using System.IO;

namespace AdventOfCode2020
{
    public static class Utils
    {
        public static string FindPath(string fileName, string path = ".")
        {
            var fullPath = Path.Combine(path, fileName);
            if (File.Exists(fullPath))
            {
                return fullPath;
            }

            var parent = Directory.GetParent(path);
            if (parent == null) return null;
            return FindPath(fileName, parent.FullName);
        }
    }
}
