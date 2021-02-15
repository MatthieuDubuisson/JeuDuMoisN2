using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void PlayGame()
    {
        SfxManager.sfxInstance.Confirm.PlayOneShot(SfxManager.sfxInstance.ConfirmSnd);
        SceneManager.LoadScene("SampleScene");
    }

    public void ReturnMenu()
    {
        SfxManager.sfxInstance.BackButton.PlayOneShot(SfxManager.sfxInstance.BackSnd);
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame()
    {
        SfxManager.sfxInstance.Confirm.PlayOneShot(SfxManager.sfxInstance.ConfirmSnd);
        Application.Quit();
    }

    public void PlayButtonSound()
    {
        SfxManager.sfxInstance.Confirm.PlayOneShot(SfxManager.sfxInstance.ConfirmSnd);
    }

    public void PlayBackMenuSound()
    {
        SfxManager.sfxInstance.BackButton.PlayOneShot(SfxManager.sfxInstance.BackSnd);
    }

}
