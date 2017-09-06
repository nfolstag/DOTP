using System.Collections;
using System.IO;

namespace Assets.Codes.Files
{
    public class ConfigurationFileManager : FileManager
    {
        private static ConfigurationFileManager instance;
        public static ConfigurationFileManager GetInstance()
        {
            lock(new object())
            {
                if (instance == null)               
                    instance = new ConfigurationFileManager();         
            }
            return instance;            
        }

        public override void TextFileWriteLines(string path, Hashtable table, StreamWriter sw = null)
        {
            string tempPath = path;
            if (File.Exists(path))            
                tempPath += ".temp";
            if (sw == null)
                sw = File.CreateText(tempPath);
            WriteLinesWithTemporalFile(path, tempPath, table, sw);
            sw.Close();
            File.Delete(path);
            File.Move(tempPath, path);
        } 

        public void LoadConfigurationValues(string path)
        {
            if(File.Exists(path))
            {
                string[] lines = File.ReadAllLines(path);
                char separator = char.Parse("|");
                Hashtable table = null;

                foreach (string line in lines)
                {
                    string l = line.Trim();
                    string[] split = l.Split(separator);
                    
                    if(split[0] == l && l != "")
                    {
                        if(l.Contains("=AUDIO="))
                            table = ConfigurationVariables.audioMap;
                        else
                            table = ConfigurationVariables.controlsMap;
                    }
                    else                   
                       table.Add(split[0], split[1]);                         
                }
            }
        }
        
        private void WriteLinesWithTemporalFile(string originPath, string tempPath, Hashtable table, StreamWriter sw)
        {
            string[] pathLines = File.ReadAllLines(originPath);
            object key = null;
            foreach (string line in pathLines)
            {
                foreach (DictionaryEntry de in table)
                {
                    if (line.Contains(de.Key.ToString()))
                    {
                        sw.WriteLine(de.Value);
                        key = de.Key;
                        break;
                    }                    
                }
                if (key == null)
                    sw.WriteLine(line);
                else
                    table.Remove(key);
                key = null;
            }
            if (table.Count > 0)
                base.TextFileWriteLines(tempPath, table, sw);           
        }       
    }
}
