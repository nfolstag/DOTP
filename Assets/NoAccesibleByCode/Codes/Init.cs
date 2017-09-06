using Assets.Codes.Files;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Init : MonoBehaviour
{	
	public void Awake ()
    {
        Constants.RESOURCES_ROOT_DIR_FULLPATH = FileManager.GetParentOfRootDirectory() + Constants.RESOURCES_ROOT;
        string path1 = Constants.RESOURCES_ROOT_DIR_FULLPATH + Constants.RESOURCES_ROOT_DEFAULT + Constants.RESOURCES_FILE_CONF;
        string path2 = Constants.RESOURCES_ROOT_DIR_FULLPATH + Constants.RESOURCES_FILE_CONF;
        FileManager.CreateAndReplace(path1, path2);
        ConfigurationFileManager.GetInstance().LoadConfigurationValues(path2);        
        SceneManager.LoadScene(1);
	}	

   
}
