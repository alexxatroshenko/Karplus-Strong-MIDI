using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AudioEditor : MonoBehaviour
{
    [SerializeField] private List<AudioClip> stringClips;
    [SerializeField] private List<AudioClip> noiseClips;
    private AudioBootstrapper bootstrapper;
    private AudioConfig audioConfig;
    [SerializeField] private float[] samplesArr;

    public List<AudioClip> GetStringClips() => stringClips;
    private float[] CreateNoise()
    {
        float[] samples = new float[44100/15];
        for (int i = 0; i < samples.Length; ++i)
        {
            samples[i] = UnityEngine.Random.Range(-1f, 1f);
        }

        return samples;
    }

    private void Awake()
    {
        bootstrapper = GetComponent<AudioBootstrapper>();
        audioConfig = GetComponent<AudioConfig>();
        stringClips = bootstrapper.GetStringClips();
        noiseClips = bootstrapper.GetNoiseClips();
    }

    void Start()
    {
        SetNoiseData(CreateNoise());
        CreateString(noiseClips, stringClips);
    }

    private void CreateString(List<AudioClip> noises, List<AudioClip> strings)
    {
        var fs = audioConfig.GetSampleRate();
        var stringLengthInSeconds = audioConfig.GetStringLengthInSeconds();
        var foreachIndex = 0;
        foreach (var clip in strings)
        {
            List<float> samples = new List<float>();
            int currentSample = 0;
            int index = 0;
            float previousValue = 0;
            float[] noiseData = new float[noises[foreachIndex].samples];
            noises[foreachIndex].GetData(noiseData, 0);
            while (samples.Count < stringLengthInSeconds * fs)
            {
                noiseData[currentSample] = 0.994f * (noiseData[currentSample] + previousValue)/2;
                
                samples.Add(noiseData[currentSample]);
                previousValue = samples[samples.Count - 1];
                index++;
                currentSample++;
                currentSample = currentSample % noiseData.Length;
            }
            samplesArr = samples.ToArray();
            for (int i = 0; i < 2; i++)
            {
                samplesArr[i] = 0; // to fix unity AudioClip bug
            }
            samples.Clear();
            clip.SetData(samplesArr, 0);

            foreachIndex++;
        }
    }

    private void SetNoiseData(float[] noiseData)
    {
        foreach (var clip in noiseClips)
        {
            float[] limitedSamples = new float[clip.samples];
            for (int i = 0; i < limitedSamples.Length; i++)
            {
                limitedSamples[i] = noiseData[i];
            }
            clip.SetData(limitedSamples, 0);
        }
    }
}
