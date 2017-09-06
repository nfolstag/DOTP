using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine;

namespace Assets.Codes.MainMenu
{
    public class FloatContainerItemsHandler : EventTrigger
    {
        public override void OnPointerEnter(PointerEventData eventData)
        {
            BeginActionButtonsAnimation(gameObject.name, true);            
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            BeginActionButtonsAnimation(gameObject.name, false);
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            switch (gameObject.name)
            {
                case (Constants.ITEM_CONFIGURATION_AUDIO_OK):
                    
                    break;
                case (Constants.ITEM_CONFIGURATION_CONTROLS_OK):
                    
                    break;
                case (Constants.ITEM_CONFIGURATION_GRAPHICS_OK):
                    string[] resolution = ((string)GraphicsConfigurationHandler.GetSomeField(0)).Split(char.Parse("x"));
                    int antialiasing = int.Parse(((string)GraphicsConfigurationHandler.GetSomeField(1)).Split(char.Parse("x"))[1]);
                    Screen.SetResolution(
                        int.Parse(resolution[0]),
                        int.Parse(resolution[1]),
                        (bool)GraphicsConfigurationHandler.GetSomeField(2)
                    );
                    QualitySettings.antiAliasing = antialiasing;
                    FloatContainerHandler.HideFloatContainer();
                    break;
                default:
                    FloatContainerHandler.HideFloatContainer();
                    break;
            }            
        }        

        private Hashtable GetAudioTable()
        {
            Hashtable table = new Hashtable();
           
            return table;
        }

        private Hashtable GetControlsTable()
        {
            Hashtable table = new Hashtable();
           
            return table;
        }        

        private void BeginActionButtonsAnimation(string selectedItem, bool val)
        {
            switch (selectedItem)
            {
                case (Constants.ITEM_CONFIGURATION_AUDIO_OK):

                    break;
                case (Constants.ITEM_CONFIGURATION_AUDIO_CANCEL):

                    break;
                case (Constants.ITEM_CONFIGURATION_CONTROLS_OK):

                    break;
                case (Constants.ITEM_CONFIGURATION_CONTROLS_CANCEL):

                    break;
                case (Constants.ITEM_CONFIGURATION_GRAPHICS_OK):

                    break;
                case (Constants.ITEM_CONFIGURATION_GRAPHICS_CANCEL):

                    break;
            }
        }        
    }
}
