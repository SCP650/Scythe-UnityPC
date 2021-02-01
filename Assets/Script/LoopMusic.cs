/*
Loop Music.cs
Loops music perfectly in Unity. Preload the audio source with the audio file, then put in the start and end loop points in milliseconds in the editor.

Author: Jonathan Fischer
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopMusic : MonoBehaviour
{

    private static long sampleRate = 44100L;

    private AudioSource source;

    public int startLoop;
    public int endLoop;

    private int rewindSamples;

    private int endSamples;

    void Awake()
    {
        long temp;

        source = gameObject.GetComponent<AudioSource>();

        temp = (long)(endLoop - startLoop) * sampleRate / 1000L;
        rewindSamples = (int)temp;

        temp = (long)endLoop * sampleRate / 1000L;
        endSamples = (int)temp;
    }

    // Start is called before the first frame update
    void Start()
    {
        source.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (source.timeSamples > endSamples)
            source.timeSamples -= rewindSamples;
    }
}
