using UnityEngine;
using UnityEngine.UI;

namespace Assets.Codes.Animations
{
    public class AnimationHandler
    {
        private static AnimationHandler instance = new AnimationHandler();
        public static AnimationHandler GetInstance()
        {
            return instance;
        }

        public void DoMenuItemEnterAnimation(Animation anim)
        {
            if (!anim["ITEM_ENTER"])
            {
                AnimationClip clip = new AnimationClip();
                clip.legacy = true;
                AnimationCurve scaleCurve = AnimationCurve.Linear(0, 1, 0.2f, 1.2f);
                AnimationCurve colorGCurve = AnimationCurve.Linear(0, 0, 0.2f, 0.7f);
                AnimationCurve colorBCurve = AnimationCurve.Linear(0, 0, 0.2f, 0.3f);
                AnimationCurve alphaCurve = AnimationCurve.Linear(0, 0.47f, 0.2f, 1);
                clip.SetCurve("", typeof(Transform), "m_LocalScale.x", scaleCurve);
                clip.SetCurve("", typeof(Transform), "m_LocalScale.y", scaleCurve);
                clip.SetCurve("Border", typeof(RawImage), "m_Color.g", colorGCurve);
                clip.SetCurve("Border", typeof(RawImage), "m_Color.b", colorBCurve);
                clip.SetCurve("Border", typeof(RawImage), "m_Color.a", alphaCurve);
                clip.SetCurve("Border/InternalBorder", typeof(RawImage), "m_Color.g", colorGCurve);
                clip.SetCurve("Border/InternalBorder", typeof(RawImage), "m_Color.b", colorBCurve);
                clip.SetCurve("TextP", typeof(RawImage), "m_Color.g", colorGCurve);
                clip.SetCurve("TextP", typeof(RawImage), "m_Color.b", colorBCurve);
                anim.AddClip(clip, "ITEMENTER");                
            }
            anim.PlayQueued("ITEMENTER", QueueMode.CompleteOthers);
        }

        public void DoMenuItemExitAnimation(Animation anim)
        {
            if (!anim["ITEM_EXIT"])
            {
                AnimationClip clip = new AnimationClip();
                clip.legacy = true;
                AnimationCurve scaleCurve = AnimationCurve.Linear(0, 1.2f, 0.2f, 1);
                AnimationCurve colorGCurve = AnimationCurve.Linear(0, 0.7f, 0.2f, 0);
                AnimationCurve colorBCurve = AnimationCurve.Linear(0, 0.3f, 0.2f, 0);
                clip.SetCurve("", typeof(Transform), "m_LocalScale.x", scaleCurve);
                clip.SetCurve("", typeof(Transform), "m_LocalScale.y", scaleCurve);
                clip.SetCurve("Border", typeof(RawImage), "m_Color.g", colorGCurve);
                clip.SetCurve("Border", typeof(RawImage), "m_Color.b", colorBCurve);
                clip.SetCurve("Border/InternalBorder", typeof(RawImage), "m_Color.g", colorGCurve);
                clip.SetCurve("Border/InternalBorder", typeof(RawImage), "m_Color.b", colorBCurve);
                clip.SetCurve("TextP", typeof(RawImage), "m_Color.g", colorGCurve);
                clip.SetCurve("TextP", typeof(RawImage), "m_Color.b", colorBCurve);
                anim.AddClip(clip, "ITEMEXIT");
            }
            anim.PlayQueued("ITEMEXIT", QueueMode.CompleteOthers);
        }      
        
        public void DoFloatContainerEnterAnimation(Animation anim, string relativePath)
        {
            if(!anim["FLOATCONTAINER_ENTER"])
            {
                AnimationClip clip = new AnimationClip();
                clip.legacy = true;
                AnimationCurve translationCurve = AnimationCurve.Linear(0, -814, 0.5f, 0);
                clip.SetCurve(relativePath, typeof(Transform), "m_LocalPosition.y", translationCurve);
                anim.AddClip(clip, "FLOATCONTAINER_ENTER");
            }
            anim.Play("FLOATCONTAINER_ENTER");
        }

        public void DoFloatContainerExitAnimation(Animation anim, string relativePath)
        {
            if (!anim["FLOATCONTAINER_EXIT"])
            {
                AnimationClip clip = new AnimationClip();
                clip.legacy = true;
                AnimationCurve translationCurve = AnimationCurve.EaseInOut(0, 0, 0.5f, -814);
                clip.SetCurve(relativePath, typeof(Transform), "m_LocalPosition.y", translationCurve);
                anim.AddClip(clip, "FLOATCONTAINER_EXIT");
            }
            anim.Play("FLOATCONTAINER_EXIT");
        }

        public void DoChangeExitToBackSpriteAnimation(Animation anim, string relativePath)
        {
            if (!anim["TOBACK"])
            {
                AnimationClip clip = new AnimationClip();
                clip.legacy = true;
                AnimationCurve exitCurve = AnimationCurve.EaseInOut(0, 1, 0.8f, 0);
                AnimationCurve backCurve = AnimationCurve.EaseInOut(0.8f, 0, 1.6f, 1);
                AnimationCurve alphaExitCurve = AnimationCurve.EaseInOut(0, 1, 0.8f, 0);
                AnimationCurve alphaBackCurve = AnimationCurve.EaseInOut(0.8f, 0, 1.6f, 1);
                clip.SetCurve(relativePath + "", typeof(RawImage), "m_Color.a", alphaExitCurve);
                clip.SetCurve(relativePath + "ExitText", typeof(GameObject), "m_IsActive", exitCurve);
                clip.SetCurve(relativePath + "ExitText", typeof(Text), "m_Color.a", alphaExitCurve);
                clip.SetCurve(relativePath + "InternalBorder", typeof(RawImage), "m_Color.a", alphaExitCurve);
                clip.SetCurve(relativePath+"InternalBorder/Image", typeof(GameObject), "m_IsActive", exitCurve);
                clip.SetCurve(relativePath + "InternalBorder/Image", typeof(RawImage), "m_Color.a", alphaExitCurve);
                clip.SetCurve(relativePath + "", typeof(RawImage), "m_Color.a", alphaBackCurve);
                clip.SetCurve(relativePath + "BackText", typeof(GameObject), "m_IsActive", backCurve);
                clip.SetCurve(relativePath + "BackText", typeof(Text), "m_Color.a", alphaBackCurve);
                clip.SetCurve(relativePath + "InternalBorder", typeof(RawImage), "m_Color.a", alphaBackCurve);
                clip.SetCurve(relativePath + "InternalBorder/Image2", typeof(GameObject), "m_IsActive", backCurve);
                clip.SetCurve(relativePath + "InternalBorder/Image2", typeof(RawImage), "m_Color.a", alphaBackCurve);
                
                anim.AddClip(clip, "TOBACK");
            }
            anim.Play("TOBACK");
        }

        public void DoChangeBackToExitSpriteAnimation(Animation anim, string relativePath)
        {
            if (!anim["TOEXIT"])
            {
                AnimationClip clip = new AnimationClip();
                clip.legacy = true;
                AnimationCurve backCurve = AnimationCurve.EaseInOut(0, 1, 0.8f, 0);
                AnimationCurve exitCurve = AnimationCurve.EaseInOut(0.8f, 0, 1.6f, 1);
                AnimationCurve alphaBackCurve = AnimationCurve.EaseInOut(0, 1, 0.8f, 0);
                AnimationCurve alphaExitCurve = AnimationCurve.EaseInOut(0.8f, 0, 1.6f, 1);
                clip.SetCurve(relativePath + "", typeof(RawImage), "m_Color.a", alphaBackCurve);
                clip.SetCurve(relativePath + "BackText", typeof(GameObject), "m_IsActive", backCurve);
                clip.SetCurve(relativePath + "BackText", typeof(Text), "m_Color.a", alphaBackCurve);
                clip.SetCurve(relativePath + "InternalBorder", typeof(RawImage), "m_Color.a", alphaBackCurve);
                clip.SetCurve(relativePath + "InternalBorder/Image2", typeof(GameObject), "m_IsActive", backCurve);
                clip.SetCurve(relativePath + "InternalBorder/Image2", typeof(RawImage), "m_Color.a", alphaBackCurve);
                clip.SetCurve(relativePath + "", typeof(RawImage), "m_Color.a", alphaExitCurve);
                clip.SetCurve(relativePath + "ExitText", typeof(GameObject), "m_IsActive", exitCurve);
                clip.SetCurve(relativePath + "ExitText", typeof(Text), "m_Color.a", alphaExitCurve);
                clip.SetCurve(relativePath + "InternalBorder", typeof(RawImage), "m_Color.a", alphaExitCurve);
                clip.SetCurve(relativePath + "InternalBorder/Image", typeof(GameObject), "m_IsActive", exitCurve);
                clip.SetCurve(relativePath + "InternalBorder/Image", typeof(RawImage), "m_Color.a", alphaExitCurve);

                anim.AddClip(clip, "TOEXIT");
            }
            anim.Play("TOEXIT");
        }
    }
}
