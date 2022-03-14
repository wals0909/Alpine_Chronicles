using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class AudioTests
{
    GameObject gameObject;
    GameObject audioSource;
    AudioPlayer audioPlayer;

    [SetUp]
    public void Setup()
    {
        gameObject = new GameObject();
        audioPlayer = gameObject.AddComponent<AudioPlayer>();
        audioSource = new GameObject();
        audioPlayer.audioSource = audioSource.AddComponent<AudioSource>();
    }

    [TearDown]
    public void TearDown()
    {
        GameObject.DestroyImmediate(audioPlayer);
        GameObject.DestroyImmediate(gameObject);
    }

    // A Test behaves as an ordinary method
    [Test]
    public void VolumeTest()
    {
        audioPlayer.setVolume(1.0f);

        Assert.AreEqual(1.0f, audioPlayer.audioSource.volume);

        audioPlayer.setVolume(0);

        Assert.AreEqual(0, audioPlayer.audioSource.volume);

        audioPlayer.setVolume(0.3f);

        Assert.AreEqual(0.3f, audioPlayer.audioSource.volume);

        audioPlayer.setVolume(2.5f);

        Assert.AreEqual(1.0f, audioPlayer.audioSource.volume);

        audioPlayer.setVolume(-0.4f);

        Assert.AreEqual(0, audioPlayer.audioSource.volume);
    }

}