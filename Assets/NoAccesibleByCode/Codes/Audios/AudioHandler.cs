using System.Collections;
using System.IO;
using UnityEngine;

namespace Assets.Codes.Audios
{
    public abstract class AudioHandler : MonoBehaviour
    {
        protected virtual void LoadExternalClip(AudioSource source, string path, bool loop = false, float wait = 0f)
        {
            if(File.Exists(path))
            {
                AudioClip clip = new WWW(path).GetAudioClip();
                source.clip = clip;
                source.loop = loop;
                StartCoroutine(StartClip(source, clip, wait));
            }
        }

        protected virtual IEnumerator StartClip(AudioSource source, AudioClip clip, float wait)
        {
            while (clip.loadState != AudioDataLoadState.Loaded) { }
            yield return new WaitForSeconds(wait);
            source.Play();            
        }
    }
}
