using Assets.Codes.Animations;
using UnityEngine;

namespace Assets.Codes.MainMenu
{
    public class FloatContainerHandler : MonoBehaviour
    {
        private AnimationHandler animHandler;
        private Animation anim;
        public delegate void ShowConfigurationByNameInContainerDelegate<T1>(T1 p1);
        public delegate void HideFloatContainerDelegate();
        public static ShowConfigurationByNameInContainerDelegate<string> ShowConfigurationByNameInContainer;
        public static HideFloatContainerDelegate HideFloatContainer;

        public void Start()
        {
            ShowConfigurationByNameInContainer = ShowConfigurationByNameInContainerM;
            HideFloatContainer = HideFloatContainerM;
            anim = gameObject.AddComponent<Animation>();
            animHandler = AnimationHandler.GetInstance();
        }

        public void ShowConfigurationByNameInContainerM(string name)
        {
            SetFloatContainerContentVisible(name);
            animHandler.DoFloatContainerEnterAnimation(anim, "");
        }

        private void HideFloatContainerM()
        {
            animHandler.DoFloatContainerExitAnimation(anim, "");
        }

        private void SetFloatContainerContentVisible(string name)
        {
            int childCount = transform.childCount;
            for (int i = 0; i < childCount; i++)
            {
                GameObject go = transform.GetChild(i).gameObject;
                if (go.name == name)
                    go.SetActive(true);
                else
                    go.SetActive(false);
            }
        }
    }
}
