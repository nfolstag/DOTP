using System;
using System.IO;
using System.Collections;
using UnityEngine;

namespace Assets.Codes.Files
{
    public class FileManager
    {
        public virtual void TextFileWriteLines(string path, Hashtable table, StreamWriter sw = null)
        {
            if(sw == null)
                sw = new StreamWriter(path);
            foreach (DictionaryEntry de in table)
                sw.WriteLine(de.Value);
            sw.Flush();
            sw.Close();
        }  
        
        public static void CreateAndReplace(string originalPath, string destinyPath)
        {
            if (File.Exists(originalPath))
            {
                if (!File.Exists(destinyPath))                
                    File.Copy(originalPath, destinyPath);               
            }
        }

        public static string GetParentOfRootDirectory()
        {
            string[] folders = Application.dataPath.Split(char.Parse("/"));
            string parentDir = "";
            for (int i = 0; i < folders.Length - 1; i++)
                parentDir += folders[i] + "/";
            return parentDir;
        }       
    }
}
