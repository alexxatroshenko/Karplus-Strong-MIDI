using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using Random = System.Random;

public class AudioEditorStable : MonoBehaviour
{
    [SerializeField] private AudioClip clip;
    [SerializeField] private AudioClip clip2;
    [SerializeField] private int delayTime;
    [SerializeField] private int stringLengthInSeconds = 3;
    [SerializeField] private int sampleRate = 8000;
    [SerializeField] private float frequency = 329.63f;
    private int noiseLength;
    private AudioSource audioSource;

    private void Start()
    {
        noiseLength = CountNoiseLength();
        audioSource = GetComponent<AudioSource>();
        clip = AudioClip.Create("Clip",noiseLength , 2, sampleRate, false);
        clip2 = AudioClip.Create("editedClip", sampleRate*stringLengthInSeconds, 2, sampleRate, false);
        CreateNoise();
        CreateString();
    }

    private int CountNoiseLength()
    {
        return (int) Math.Round(sampleRate / frequency);
    }
    public void PlayOriginalAudio()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
        else audioSource.PlayOneShot(clip);
    }

    public void PlayEditedAudio(int param)
    {
        Debug.Log(param);
        audioSource.PlayOneShot(clip2);
    }

    private void CreateString()
    {
        var fs = sampleRate;
        List<float> samples = new List<float>();
        int currentSample = 0;
        int index = 0;
        float previousValue = 0;
        float[] noiseData = new float[noiseLength];
        clip.GetData(noiseData, 0);
        while (samples.Count < stringLengthInSeconds * fs)
        {
            noiseData[currentSample] = 0.995f * (noiseData[currentSample] + previousValue)/2;
            samples.Add(noiseData[currentSample]);
            previousValue = samples[samples.Count - 1];
            index++;
            currentSample++;
            currentSample = currentSample % noiseData.Length;
        }
        //samples.AddRange(new []{0f,0f,0f});
        var samplesArr = samples.ToArray();
        clip2.SetData(samplesArr, 0);
    }

    private void CreateNoise()
    {
        float[] samples = new float[noiseLength];
        for (int i = 0; i < samples.Length; ++i)
        {
            samples[i] = UnityEngine.Random.Range(-1f, 2f);
        }

        clip.SetData(samples, 0);
    }
}