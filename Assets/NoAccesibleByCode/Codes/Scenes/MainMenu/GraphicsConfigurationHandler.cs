using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections.Generic;

namespace Assets.Codes.MainMenu
{
    public class GraphicsConfigurationHandler : MonoBehaviour
    {
        public Dropdown aspectDropdown, resolutionDropdown, antialiasingDropdown;
        public Toggle fullScreenToogle;
        public delegate object GetSomeFieldDelegate(int i);
        public static GetSomeFieldDelegate GetSomeField;
        private string resolution, antialiasing;
        private bool fullscreen;
        private List<Dropdown.OptionData> fiveFourList, fourThreeList, threeTwoList, sixtTenList, sixtNineList, aspectList, antialiasingList;        

        public void Start()
        {
            int width = Screen.width; int height = Screen.height; int antialiasing = QualitySettings.antiAliasing;
            bool fullscreen = Screen.fullScreen;
            string aspect = GetAspectFromFloat(((float)width) / height);
            GetSomeField = MGetSomeField;
            FillAspectLists();
            SetEventListeners();
            SetAspectOptions(aspect);
            SetResolutionOptions(aspect, width + "x" + height);
            SetAntialiasingOptions(antialiasing);
            fullScreenToogle.isOn = fullscreen;
            this.fullscreen = fullscreen;
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                aspectDropdown.Hide();
                resolutionDropdown.Hide();
                antialiasingDropdown.Hide();
            }
        }
        
        private object MGetSomeField(int i)
        {
            object field = null;
            switch(i)
            {
                case (0):
                    field = resolution;
                    break;
                case (1):
                    field = antialiasing;
                    break;
                case (2):
                    field = fullscreen;
                    break;
            }
            return field;
        }
        private void OnAspectChange(int i)
        {
            resolutionDropdown.Hide();
            switch (i)
            {
                case (Constants.CONF_GRAPH_ASPECT_54):
                    resolutionDropdown.options = fiveFourList;                    
                    break;
                case (Constants.CONF_GRAPH_ASPECT_43):
                    resolutionDropdown.options = fourThreeList;
                    break;
                case (Constants.CONF_GRAPH_ASPECT_32):
                    resolutionDropdown.options = threeTwoList;
                    break;
                case (Constants.CONF_GRAPH_ASPECT_1610):
                    resolutionDropdown.options = sixtTenList;
                    break;
                case (Constants.CONF_GRAPH_ASPECT_169):
                    resolutionDropdown.options = sixtNineList;
                    break;
            }
            resolution = resolutionDropdown.options[0].text;
        }

        private void OnResolutionChange(int i)
        {
            resolution = resolutionDropdown.options[i].text;
        }

        private void OnantialiasingChange(int i)
        {
            antialiasing = antialiasingDropdown.options[i].text;
        }

        private void OnFullScreenChange(bool b)
        {
            fullscreen = b;
        }

        private void FillAspectLists()
        {
            fiveFourList = new List<Dropdown.OptionData>();
            fourThreeList = new List<Dropdown.OptionData>();
            threeTwoList = new List<Dropdown.OptionData>();
            sixtTenList = new List<Dropdown.OptionData>();
            sixtNineList = new List<Dropdown.OptionData>();
            aspectList = new List<Dropdown.OptionData>();
            antialiasingList = new List<Dropdown.OptionData>();

            fiveFourList.Add(new Dropdown.OptionData("800x400"));
            fiveFourList.Add(new Dropdown.OptionData("1280x768"));
            fourThreeList.Add(new Dropdown.OptionData("800x600"));
            fourThreeList.Add(new Dropdown.OptionData("1024x768"));
            fourThreeList.Add(new Dropdown.OptionData("1152x864"));
            fourThreeList.Add(new Dropdown.OptionData("1280x960"));
            fourThreeList.Add(new Dropdown.OptionData("1400x1050"));
            fourThreeList.Add(new Dropdown.OptionData("1600x1200"));
            threeTwoList.Add(new Dropdown.OptionData("1152x768"));
            threeTwoList.Add(new Dropdown.OptionData("1280x854"));
            threeTwoList.Add(new Dropdown.OptionData("1440x960"));
            sixtTenList.Add(new Dropdown.OptionData("1280x800"));
            sixtTenList.Add(new Dropdown.OptionData("1440x900"));
            sixtTenList.Add(new Dropdown.OptionData("1680x1050"));
            sixtTenList.Add(new Dropdown.OptionData("1920x1200"));
            sixtNineList.Add(new Dropdown.OptionData("1280x720"));
            sixtNineList.Add(new Dropdown.OptionData("1365x768"));
            sixtNineList.Add(new Dropdown.OptionData("1600x900"));
            sixtNineList.Add(new Dropdown.OptionData("1920x1080"));
            aspectList.Add(new Dropdown.OptionData("5:4"));
            aspectList.Add(new Dropdown.OptionData("4:3"));
            aspectList.Add(new Dropdown.OptionData("3:2"));
            aspectList.Add(new Dropdown.OptionData("16:10"));
            aspectList.Add(new Dropdown.OptionData("16:9"));
            antialiasingList.Add(new Dropdown.OptionData("x2"));
            antialiasingList.Add(new Dropdown.OptionData("x4"));
            antialiasingList.Add(new Dropdown.OptionData("x8"));
        }

        private void SetEventListeners()
        {
            Dropdown.DropdownEvent deAspect = new Dropdown.DropdownEvent();
            deAspect.AddListener(new UnityAction<int>(OnAspectChange));
            aspectDropdown.onValueChanged = deAspect;

            Dropdown.DropdownEvent deResolution = new Dropdown.DropdownEvent();
            deResolution.AddListener(new UnityAction<int>(OnResolutionChange));
            resolutionDropdown.onValueChanged = deResolution;

            Dropdown.DropdownEvent deantialiasing = new Dropdown.DropdownEvent();
            deantialiasing.AddListener(new UnityAction<int>(OnantialiasingChange));
            antialiasingDropdown.onValueChanged = deantialiasing;

            Toggle.ToggleEvent teFullscreen = new Toggle.ToggleEvent();
            teFullscreen.AddListener(new UnityAction<bool>(OnFullScreenChange));
            fullScreenToogle.onValueChanged = teFullscreen;
        }

        private void SetAspectOptions(string aspect)
        {
            int i = 0;
            aspectDropdown.options = aspectList;

            foreach (Dropdown.OptionData od in aspectDropdown.options)
            {
                if(od.text == aspect)
                {
                    aspectDropdown.value = i;
                    break;
                }
                i++;
            }
        }

        private void SetResolutionOptions(string aspect, string resolution)
        {
            int i = 0;
            switch(aspect)
            {
                case ("5:4"):
                    resolutionDropdown.options = fiveFourList;
                    break;
                case ("4:3"):
                    resolutionDropdown.options = fourThreeList;
                    break;
                case ("16:10"):
                    resolutionDropdown.options = sixtTenList;
                    break;
                case ("16:9"):
                    resolutionDropdown.options = sixtNineList;
                    break;
            }

            foreach(Dropdown.OptionData od in resolutionDropdown.options)
            {
                if(od.text == resolution)
                {
                    resolutionDropdown.value = i;
                    this.resolution = resolutionDropdown.options[i].text;
                    break;
                }
                i++;
            }
        }

        private void SetAntialiasingOptions(int antialiasing)
        {
            int i = 0;
            string anti = "x" + antialiasing;
            antialiasingDropdown.options = antialiasingList;

            foreach (Dropdown.OptionData od in antialiasingDropdown.options)
            {
                if (od.text == anti)
                {
                    antialiasingDropdown.value = i;
                    this.antialiasing = antialiasingDropdown.options[i].text;
                    break;
                }
                i++;
            }
        }        

        private string GetAspectFromFloat(float fAspect)
        {
            string aspect = "";
            if (fAspect >= 1.7f)
                aspect = "16:9";
            else if (fAspect >= 1.6f)
                aspect = "16:10";
            else if (fAspect >= 1.3f)
                aspect = "4:3";
            else
                aspect = "5:4";           
            return aspect;
        }
    }
}
