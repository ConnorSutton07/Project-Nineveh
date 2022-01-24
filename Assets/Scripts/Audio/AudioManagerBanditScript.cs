using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound2
{
    public string m_name;
    public AudioClip[] m_clips;
    public float volumeNoise;
    public float pitchNoise;

    private AudioSource m_source;

    public void SetSource(AudioSource source)
    {
        m_source = source;
        int randomClip = Random.Range(0, m_clips.Length - 1);
        m_source.clip = m_clips[randomClip];
    }

    public void Play()
    {
        if (m_clips.Length > 1)
        {
            int randomClip = Random.Range(0, m_clips.Length);
            m_source.clip = m_clips[randomClip];
        }
        //add some noise to volume and pitch of sounds
        //m_source.volume = 1.0f + Random.Range(-volumeNoise, volumeNoise); // Random.Range(1 - noiseMagnitude, 1 + noiseMagnitude);
        m_source.pitch = 1.0f + Random.Range(-pitchNoise, pitchNoise); //* Random.Range(1 - noiseMagnitude, 1 + noiseMagnitude);

        //allows for playing of multiple sounds at once from a single source. 
        //passed in ratio (0-1) as multiplier of source's volume
        m_source.PlayOneShot(m_source.clip, 1.0f - Random.Range(0, volumeNoise)); 
    }
}

public class AudioManagerBanditScript : MonoBehaviour
{
    // Make it a singleton class that can be accessible everywhere
    //public static AudioManagerBanditScript instance;

    [SerializeField]
    Sound2[] m_sounds;

    private void Awake()
    {
      //  if (instance != null)
       // {
       //     Debug.LogError("More than one AudioManger in scene");
        //}
       // else
      //  {
      //      instance = this;
      //  }
    }

    private void Start()
    {
        for (int i = 0; i < m_sounds.Length; i++)
        {
            GameObject go = new GameObject("Sound_" + i + "_" + m_sounds[i].m_name);
            go.transform.SetParent(transform);
            m_sounds[i].SetSource(go.AddComponent<AudioSource>());
        }
    }

    public void PlaySound(string name)
    {
        for (int i = 0; i < m_sounds.Length; i++)
        {
            if (m_sounds[i].m_name == name)
            {
                m_sounds[i].Play();
                return;
            }
        }

        Debug.LogWarning("AudioManager: Sound name not found in list: " + name);
    }
}
