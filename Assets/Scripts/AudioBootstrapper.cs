using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioBootstrapper : MonoBehaviour
{

    private List<AudioClip> stringClips;
    private List<AudioClip> noiseClips;
    private AudioConfig audioConfig;
    private int sampleRate;
    private int noiseLength;
    private List<int> noiseLengths;

    public List<AudioClip> GetStringClips() => stringClips;
    public List<AudioClip> GetNoiseClips() => noiseClips;
    
    private void Awake()
    {
        stringClips = new List<AudioClip>();
        noiseClips = new List<AudioClip>();
        noiseLengths = new List<int>();
        audioConfig = GetComponent<AudioConfig>();
        sampleRate = audioConfig.GetSampleRate();
        CountNoiseLengths();
        InitializeClips();
    }
    
    private void CountNoiseLengths()
    {
        var frequencies = audioConfig.GetFrequencies();
        var index = 0;
        while (index < frequencies.Count)
        {
            noiseLengths.Add((int) Math.Round(sampleRate / frequencies[index])); 
            index++;
        }
    }

    private void InitializeClips()
    {
        
        int stringLengthInSeconds = audioConfig.GetStringLengthInSeconds();
        var index = 0;
        while (index < audioConfig.GetStringNamesLength())
        {
            stringClips.Add(AudioClip.Create(audioConfig.GetStringName(index), sampleRate*stringLengthInSeconds, 2,
                sampleRate, false));
            noiseClips.Add(AudioClip.Create(audioConfig.GetNoiseName(index), noiseLengths[index], 2,
                sampleRate, false));
            index++;
        }
    }
}