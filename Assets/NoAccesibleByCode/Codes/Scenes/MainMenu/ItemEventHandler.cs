using Assets.Codes.Animations;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace Assets.Codes.MainMenu
{   
    public class ItemEventHandler : EventTrigger
    {
        public int asocConst;
        public static string selectedItem;
        private Animation anim;
        private AnimationHandler animHandler;
        private OptionsHandler oh;

        public void Start()
        {
            anim = gameObject.AddComponent<Animation>();
            animHandler = AnimationHandler.GetInstance();            
        }

        public override void OnPointerEnter(PointerEventData eventData)
        {
            animHandler.DoMenuItemEnterAnimation(anim);
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            animHandler.DoMenuItemExitAnimation(anim);
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            switch(gameObject.name)
            {
                case (Constants.ITEM_BATTLE):
                    ExitBackEventHandler.AddState(OptionsHandler.GetInstance().SetBattleMenuState());
                    break;
                case (Constants.ITEM_CARD_COLLECTION):
                    SceneManager.LoadScene(2);
                    break;
                case (Constants.ITEM_CONFIGURATION):
                    ExitBackEventHandler.AddState(OptionsHandler.GetInstance().SetConfigurationMenuState());
                    break;
                case (Constants.ITEM_DECK_ADMINISTRATION):
                    break;
                case (Constants.ITEM_CONFIGURATION_GRAPHICS):
                    FloatContainerHandler.ShowConfigurationByNameInContainer(Constants.FLOATCONTAINER_GRAPHICS);
                    break;
            }
        }

        public static void SetSelectedItem(string val)
        {
            selectedItem = val;
        }
        
    }
}
