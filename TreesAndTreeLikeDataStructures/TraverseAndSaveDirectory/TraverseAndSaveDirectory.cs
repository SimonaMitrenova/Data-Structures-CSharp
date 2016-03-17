namespace TraverseAndSaveDirectory
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    public class TraverseAndSaveDirectory
    {
        private static IDictionary<string, Folder> folders;

        public static void Main(string[] args)
        {
            string path = @"../../";
            folders = new Dictionary<string, Folder>();
            TraverseFolders(path, 0);
            PrintReport();
        }

        public static void PrintReport()
        {
            foreach (var folder in folders)
            {
                int depth = folder.Value.Depth;
                Console.WriteLine("{0}{1}{2,10}", new string(' ', depth), folder.Key, folder.Value.Size);
                foreach (var file in folder.Value.Files)
                {
                    Console.WriteLine("{0}-{1}{2,10}", new string(' ', depth), file.Name, file.Size);
                }
            }
        }

        public static void TraverseFolders(string path, int depth)
        {
            var currentFolder = GetFolderByPath(path, depth);
            
            var subDirectories = Directory.GetDirectories(path);
            foreach (var subDirectory in subDirectories)
            {
                currentFolder.Folders.Add(new Folder(subDirectory, depth));
                TraverseFolders(subDirectory, depth + 1);
            }

            var dirInfo = new DirectoryInfo(path);
            var currentFiles = dirInfo.GetFiles();
            foreach (var currentFile in currentFiles)
            {
                var file = new File(currentFile.Name, currentFile.Length);
                currentFolder.Files.Add(file);
            }
        }

        private static Folder GetFolderByPath(string folderPath, int folderDepth)
        {
            if (!folders.ContainsKey(folderPath))
            {
                folders[folderPath] = new Folder(folderPath, folderDepth);
            }

            return folders[folderPath];
        }
    }
}
