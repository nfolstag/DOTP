using Assets.Codes.Audios;
using UnityEngine;

namespace Assets.Codes
{
    [RequireComponent(typeof(AudioSource))]
    public class MainMenuAudioHandler : AudioHandler
    {
        public static AudioSource theme, fx;
        public delegate void Pointer(AudioSource source, string path, bool loop, float wait);
        public static Pointer LoadExternalClipPointer;
        
        public void Start()
        {
            AudioSource[] sources = GetComponents<AudioSource>();
            theme = sources[0];
            fx = sources[1];
            LoadExternalClipPointer = base.LoadExternalClip;
            base.LoadExternalClip(
                theme, 
                Constants.RESOURCES_ROOT_DIR_FULLPATH + Constants.RESOURCES_ROOT_MUSIC + Constants.AUDIO_MAIN, 
                true,
                1.5f
            );
            DontDestroyOnLoad(theme);
        }              
    }
}
