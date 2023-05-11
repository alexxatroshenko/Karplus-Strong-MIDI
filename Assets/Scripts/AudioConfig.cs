using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioConfig : MonoBehaviour
{
    [SerializeField] private int sampleRate = 44100;
    [SerializeField] private int stringLengthInSeconds = 3;
    [SerializeField] private string[] stringNames;
    [SerializeField] private string[] noiseNames;
    
    private List<float> frequencies; 

    public List<float> GetFrequencies() => frequencies;
    public int GetStringNamesLength() => stringNames.Length;
    public string GetStringName(int index) => stringNames[index];
    public string GetNoiseName(int index) => noiseNames[index];
    public int GetSampleRate() => sampleRate;
    public int GetStringLengthInSeconds() => stringLengthInSeconds;
    
    void Awake()
    {
        InitializeFrequencies();
    }
    private void InitializeFrequencies()
    {
        frequencies = new List<float>();
        int index = 0;
        while (index < stringNames.Length)
        {
            frequencies.Add(ConvertNoteToFreq(stringNames[index]));
            index++;
        }
    }

    private float ConvertNoteToFreq(string note)
    {
        switch (note)
        {
            case "A0":
                return 25.5f;
            case "A#0":
                return 29.14f;
            case "B0":
                return 30.87f;
            case "C0":
                return 16.35f;
            case "C#0":
                return 17.32f;
            case "D0":
                return 18.35f;
            case "D#0":
                return 19.45f;
            case "E0":
                return 20.6f;
            case "F0":
                return 21.83f;
            case "F#0":
                return 23.13f;
            case "G0":
                return 24.5f;
            case "G#0":
                return 25.96f;
            case "A1":
                return 55.0f;
            case "A#1":
                return 58.27f;
            case "B1":
                return 61.74f;
            case "C1":
                return 32.7f;
            case "C#1":
                return 34.64f;
            case "D1":
                return 36.71f;
            case "D#1":
                return 38.89f;
            case "E1":
                return 41.20f;
            case "F1":
                return 43.63f;
            case "F#1":
                return 46.25f;
            case "G1":
                return 48f; 
            case "G#1":
                return 51.91f;
            case "A2":
                return 110.0f;
            case "A#2":
                return 116.5f;
            case "B2":
                return 123.5f;
            case "C2":
                return 65.41f;
            case "C#2":
                return 69.30f;
            case "D2":
                return 73.42f;
            case "D#2":
                return 77.79f;
            case "E2":
                return 82.41f;
            case "F2":
                return 87.31f;
            case "F#2":
                return 92.50f;
            case "G2":
                return 98.00f; 
            case "G#2":
                return 103.8f;
            case "C3":
                return 130.8f;
            case "C#3":
                return 138.6f;
            case "D3":
                return 146.8f;
            case "D#3":
                return 155.6f;
            case "E3":
                return 164.8f;
            case "F3":
                return 174.6f;
            case "F#3":
                return 185.0f;
            case "G3":
                return 196.0f;
            case "G#3":
                return 207.7f;
            case "A3":
                return 220.0f;
            case "A#3":
                return 233.1f;
            case "B3":
                return 247.0f;
            case "C4":
                return 261.6f;
            case "C#4":
                return 277.2f;
            case "D4":
                return 293.7f;
            case "D#4":
                return 311.1f;
            case "E4":
                return 329.6f;
            case "F4":
                return 349.2f;
            case "F#4":
                return 370.0f;
            case "G4":
                return 392.0f;
            case "G#4":
                return 415.3f;
            case "A4":
                return 440.0f;
            case "A#4":
                return 466.2f;
            case "B4":
                return 493.9f;
            case "C5":
                return 523.3f;
            case "C#5":
                return 544.4f;
            case "D5":
                return 587.4f;
            case "D#5":
                return 622.3f;
            case "E5":
                return 659.3f;
            case "F5":
                return 698.5f;
            case "F#5":
                return 740.0f;
            case "G5":
                return 784.0f;
            case "G#5":
                return 840.6f;
            case "A5":
                return 880.0f;
            case "A#5":
                return 932.3f;
            case "B5":
                return 987.7f;
        }

        throw new MissingComponentException();
    }
}
