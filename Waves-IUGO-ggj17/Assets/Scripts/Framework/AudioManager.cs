using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

using System.Runtime.CompilerServices;
[assembly: InternalsVisibleToAttribute("ServiceLocator")]

public class AudioManager 
{
  public enum Clips { 
      BUTTONCLICK = 0,
      CORRECTTAP = 1,
      WRONGTAP = 2,
      SONAR = 3,
      EXPLOSION = 4,
  }

  class AudioObject
  {
      public AudioClip clip;
      public float volume;

      internal AudioObject(AudioClip c, float vol)
      {
          clip = c;
          volume = vol;
      }
  }

  Dictionary<Clips, AudioObject> clips;
  Queue<AudioObject> queue;

  GameObject audioGO;

  // Pooling
  AudioSource[] sources;
  AudioSource music;
  const int MAX_AUDIO = 4;
  int nextIdxAvailable;
  bool audioActived;
  bool musicActived;

  internal AudioManager()
  {
    clips = new Dictionary<Clips, AudioObject>();
    // queue of events
    queue = new Queue<AudioObject>();
    // 4 is the maximum sounds played at one frame
    sources = new AudioSource[MAX_AUDIO];
    // Current free space
    nextIdxAvailable = 0;
    // Audio flags
    audioActived = (PlayerPrefs.GetInt("AudioFlag", 1) == 1);
    musicActived = (PlayerPrefs.GetInt("MusicFlag", 1) == 1);

    // game object that holds AudioSource components
    audioGO = GameObject.Find("AudioManageGameObject");
    if (audioGO == null)
    {
      audioGO = new GameObject();
      audioGO.name = "AudioManageGameObject";
      GameObject.DontDestroyOnLoad(audioGO);
    }  
  }

  public void Init()
  {
    Debug.Log("Audio Manager Init");
    for (int i = 0; i < MAX_AUDIO; i++)
    {
      sources[i] = audioGO.AddComponent<AudioSource>();
      if (sources[i] == null)
        Debug.Log(i + " not inialized");    
    }
    // Loading audio clips
    //clips.Add(Clips.BUTTONCLICK, new AudioObject(Resources.Load<AudioClip>("AudioClips/button_click"), 1.0f));
    //clips.Add(Clips.CORRECTTAP, new AudioObject(Resources.Load<AudioClip>("AudioClips/correct_clip"), 1.0f));
    //clips.Add(Clips.WRONGTAP, new AudioObject(Resources.Load<AudioClip>("AudioClips/wrong_clip"), 1.0f));
    clips.Add(Clips.SONAR, new AudioObject(Resources.Load<AudioClip>("AudioClips/sonar_clip"), 1.0f));
    clips.Add(Clips.EXPLOSION, new AudioObject(Resources.Load<AudioClip>("AudioClips/explosion_clip"), 1.0f));


    music = audioGO.AddComponent<AudioSource>();
    music.clip = Resources.Load<AudioClip>("AudioClips/bubbles_clip");
    music.volume = 0.8f;
    music.loop = true;

    StartMusic();
  }

  public bool AudioActived()
  {
    return audioActived;
  }

  public bool MusicActived()
  {
    return musicActived;
  }

  public void SwapAudioActive()
  {
    audioActived = !audioActived;
    PlayerPrefs.SetInt("AudioFlag", audioActived ? 1 : 0);
  }

  public void SwapMusicActive()
  {
    musicActived = !musicActived;
    PlayerPrefs.SetInt("MusicFlag", musicActived ? 1 : 0);
    StartMusic();
  }

  void StartMusic()
  {
    if (!musicActived)
      music.Stop();
    else
      music.Play();
  }

  public void Register(Clips clip, float volume)
  {
    if (!audioActived)
      return;

    AudioObject obj;
    if (clips.TryGetValue(clip, out obj))
    {
      if (!queue.Contains(obj))
      {
        obj.volume = volume;
        queue.Enqueue(obj);
      }
    }
    // if there is space to play a clip, call it.
    if (queue.Count < MAX_AUDIO)
      PlayNext();
  }

  public AudioClip GetClip(Clips clip)
  {
    AudioObject obj;
    if (clips.TryGetValue(clip, out obj))
      return obj.clip;

    return null;
  }

  public float GetClipLength(Clips clip)
  {
    AudioObject obj;
    if (clips.TryGetValue(clip, out obj))
      return obj.clip.length;

    return 0;
  }

  private void PlayNext()
  {
    // if quere is empty, do nothing
    if (queue.Count == 0) return;
    // if next idx is -1, every source is taken
    if (nextIdxAvailable == -1) return;

    AudioObject obj = queue.Dequeue();
    if (obj == null)
        Debug.Log("obj null");
    if (sources[nextIdxAvailable] == null)
        Debug.Log("Source " + nextIdxAvailable + " is null");

    sources[nextIdxAvailable].clip = obj.clip;
    sources[nextIdxAvailable].volume = obj.volume;
    sources[nextIdxAvailable].Play();
    // registering callback for ending play.
    ServiceLocator.Instance.StartCoroutine(DelayedCallback(obj.clip.length, AudioFinished, nextIdxAvailable));

    // find next position available
    for (int i = 0; i < MAX_AUDIO; i++)
    {
      if (!sources[i].isPlaying)
      {
        nextIdxAvailable = i;
        return;
      }
    }
    // flag for no position available 
    nextIdxAvailable = -1;
  }

  private void AudioFinished(int idx)
  {
    // free this audiosource to be played.
    nextIdxAvailable = idx;
    // when an audio has finished, play another.
    PlayNext();
  }

  private IEnumerator DelayedCallback(float time, Action<int> callback, int idx) 
  {
    yield return new WaitForSeconds(time);
    callback(idx);
  }
}
