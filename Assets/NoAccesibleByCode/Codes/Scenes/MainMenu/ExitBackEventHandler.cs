using Assets.Codes.Animations;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Codes.MainMenu
{
    public class ExitBackEventHandler : EventTrigger
    {
        private static List<string> states;
        private AnimationHandler animHandler;
        private Animation anim;
        public delegate void AddStateDelegate(string state);
        public static AddStateDelegate AddState;

        public void Start()
        {
            AddState = MAddState;
            anim = gameObject.AddComponent<Animation>();
            states = new List<string>();
            states.Add(Constants.TRANSITION_MAINMENU);
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
            if (states.Count > 0)
            {
                Change();
                MainMenuAudioHandler.LoadExternalClipPointer(
                   MainMenuAudioHandler.fx,
                   Constants.RESOURCES_ROOT_DIR_FULLPATH + Constants.RESOURCES_ROOT_MUSIC_FX + Constants.AUDIO_BACK_MENU,
                   false,
                   0
                );
            }
            else
                Application.Quit();
        }

        private void MAddState(string state)
        {
            bool add = true;
            foreach (string s in states)
            {
                if (s == state)
                {
                    add = false;
                    break;
                }
            }
            if (add)
                states.Add(state);
            animHandler.DoChangeExitToBackSpriteAnimation(anim, "");
        }

        private void Change()
        {
            int count = states.Count;
            states.RemoveAt(count - 1);
            
            if(count -2 >= 0)
            {
                switch (states[count - 2])
                {
                    case (Constants.TRANSITION_BATTLEMENU):
                        OptionsHandler.GetInstance().SetBattleMenuState();
                        break;
                    case (Constants.TRANSITION_MAINMENU):
                        OptionsHandler.GetInstance().SetMainMenuState();
                        animHandler.DoChangeBackToExitSpriteAnimation(anim, "");
                        break;
                }
            }            
        }
    }

}
