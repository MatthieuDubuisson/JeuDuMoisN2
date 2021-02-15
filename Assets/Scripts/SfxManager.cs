using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SfxManager : MonoBehaviour
{
    public AudioSource Audio;
    public AudioSource DoubleJump;
    public AudioSource Transformed;
    public AudioSource DeTransformed;
    public AudioSource Confirm;
    public AudioSource BackButton;
    public AudioSource Alarm;

    public AudioClip Jump;
    public AudioClip DoubleJ;
    public AudioClip Transfo;
    public AudioClip DeTransfo;
    public AudioClip ConfirmSnd;
    public AudioClip BackSnd;
    public AudioClip AlarmSnd;

    public static SfxManager sfxInstance;
    private void Awake()
    {
        if (sfxInstance != null && sfxInstance != this)
        {
            Destroy(this.gameObject);
            return;

        }

        sfxInstance = this;
        DontDestroyOnLoad(this);
    }
}
