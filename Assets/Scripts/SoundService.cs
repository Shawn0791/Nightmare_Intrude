using UnityEngine;
using System.Collections.Generic;

public class SoundService : MonoBehaviour
{
    public static SoundService instance { get; private set; }

    [System.Serializable]
    public struct SoundInfo
    {
        public string name;
        public AudioClip ac;
    }

    [System.Serializable]
    public struct SoundInfoList
    {
        public string tag;
        public List<SoundInfo> siList;
    }

    private AudioSource _soundSource;
    public List<SoundInfoList> soundList;
    private Dictionary<string, AudioClip> _siDic;

    private string _lastSound = "";

    private int _lastSoundFrame;

    void Start()
    {
        instance = this;
        _soundSource = gameObject.AddComponent<AudioSource>();
        _siDic = new Dictionary<string, AudioClip>();
        _lastSoundFrame = -1;

        foreach (var i in soundList)
        {
            foreach (var info in i.siList)
            {
                _siDic.Add(info.name, info.ac);
                //Debug.Log("dic " + info.name + " " + info.ac);
            }
        }

        DontDestroyOnLoad(this);
    }

    private bool IsEnabled()
    {
        return true;
    }

    public void Play(string soundName, float volume = 1)
    {
        if (string.IsNullOrEmpty(soundName))
        {
            return;
        }
        //Debug.Log("Play " + soundName);
        if (!IsEnabled())
        {
            return;
        }

        if (_lastSoundFrame == Time.frameCount && _lastSound == soundName)
        {
            return;
        }
        if (!_siDic.ContainsKey(soundName))
        {
            Debug.LogWarning("sound key non exist " + soundName);
            return;
        }


        _soundSource.PlayOneShot(_siDic[soundName], volume);
        _lastSound = soundName;
        _lastSoundFrame = Time.frameCount;
    }

    public void Play(string[] soundNames, float volume = 1)
    {
        var soundName = soundNames[Random.Range(0, soundNames.Length)];
        Play(soundName, volume);
    }
}