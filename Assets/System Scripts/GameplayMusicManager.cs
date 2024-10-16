using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayMusicManager : MonoBehaviour
{
    [SerializeField] private AudioSource musicIntro;
    [SerializeField] private AudioSource musicLoopA;
    [SerializeField] private AudioSource musicLoopB;
    [SerializeField] private float introDuration;
    [SerializeField] private float loopDuration;
    private double nextClipScheduledTime;
    private double DSPTime;
    private double bufferSeconds = 1d;
    private bool introFinish;
    private bool playingA;
    private bool firstUpdate = true;
    private void Start()
    {
        nextClipScheduledTime = AudioSettings.dspTime + bufferSeconds;
    }
    void Update()
    {
        DSPTime = AudioSettings.dspTime;

        if (DSPTime > nextClipScheduledTime - bufferSeconds)
        {
            if (!introFinish)
            {
                if (firstUpdate)
                {
                    firstUpdate = false;
                    musicIntro.PlayScheduled(nextClipScheduledTime);
                    nextClipScheduledTime += introDuration;
                    return;
                }

                introFinish = true;
                playingA = true;
                musicLoopA.PlayScheduled(nextClipScheduledTime);
                nextClipScheduledTime += loopDuration;
                return;
            }

            AudioSource sourceToPlay = playingA ? musicLoopB : musicLoopA;
            sourceToPlay.PlayScheduled(nextClipScheduledTime);
            playingA = !playingA;
            nextClipScheduledTime += loopDuration;
        }
    }

    public void StopAll()
    {
        musicIntro.Stop();
        musicLoopA.Stop();
        musicLoopB.Stop();
    }

    public void SkipToLoop()
    {
        StopAll();
        nextClipScheduledTime = AudioSettings.dspTime + bufferSeconds;
        introFinish = true;
    }
}
