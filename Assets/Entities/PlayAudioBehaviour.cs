using Pospec.Helper.Audio;
using UnityEngine;

public class PlayAudioBehaviour : BehaviourState
{
    [SerializeField] AudioSource source;
    [SerializeField] Sound sound;
    [SerializeField] BehaviourState nextState;
    [SerializeField] bool waitForSoundEnd;

    public override BehaviourState NextState()
    {
        return nextState;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        if (waitForSoundEnd)
            source.PlaySound(sound, Finished);
        else
            source.PlaySound(sound);
    }

    private void Update()
    {
        if (!waitForSoundEnd)
            Finished();
    }
}
