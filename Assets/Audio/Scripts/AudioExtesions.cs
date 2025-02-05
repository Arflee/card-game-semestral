using DG.Tweening;
using Pospec.Helper.Audio;
using System;
using UnityEngine;

public static class AudioExtensions
{
    public static void PlaySound(this AudioSource source, Sound sound)
    {
        if (source == null || sound.clips == null || sound.clips.Count == 0)
            return;

        AudioClip clip = sound.clips[UnityEngine.Random.Range(0, sound.clips.Count)];
        if (clip == null)
            return;

        source.clip = clip;
        source.volume = sound.volume;
        source.pitch = UnityEngine.Random.Range(sound.minPitch, sound.maxPitch);
        source.loop = sound.looping;
        source.Play();
    }

    public static void PlaySound(this AudioSource source, Sound sound, Action onFinished)
    {
        if (source == null || sound.clips == null || sound.clips.Count == 0)
            return;

        AudioClip clip = sound.clips[UnityEngine.Random.Range(0, sound.clips.Count)];
        if (clip == null)
            return;

        source.clip = clip;
        source.volume = sound.volume;
        source.pitch = UnityEngine.Random.Range(sound.minPitch, sound.maxPitch);
        source.loop = sound.looping;
        source.Play();
        DOTween.Sequence().AppendInterval(clip.length).OnComplete(() => onFinished());
    }
}
