using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] public static AudioManager _audioManager;
    [SerializeField] float _bgmVolume;

    [Header("BGM")]
    public AudioClip _bgmClip;
    AudioSource _bgmPlayer;

    private void Awake()
    {
        _audioManager = this;
        Init();
    }

    private void Start()
    {
        BgmPlay(true);
    }

    private void Init()
    {
        GameObject _bgmObject = new GameObject("BgmPlayer");
        _bgmObject.transform.parent = transform;
        _bgmPlayer = _bgmObject.AddComponent<AudioSource>();
        _bgmPlayer.loop = true;
        _bgmPlayer.volume = _bgmVolume;
        _bgmPlayer.clip = _bgmClip;
    }

    public void BgmPlay(bool isPlay)
    {
        if (isPlay)
        {
            _bgmPlayer.Play();
        }
        else
        {
            _bgmPlayer.Stop();
        }
    }
}
