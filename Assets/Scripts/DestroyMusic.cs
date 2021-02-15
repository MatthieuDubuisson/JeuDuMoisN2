using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DestroyMusic : MonoBehaviour
{
    public static DestroyMusic MusicInstance;

    private void Awake()
    {
        if (MusicInstance != null && MusicInstance != this)
        {
            Destroy(this.gameObject);
            return;

        }
        
        MusicInstance = this;
        DontDestroyOnLoad(this);
    }
}
