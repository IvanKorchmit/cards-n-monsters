using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ScenesManager : MonoBehaviour
{
    public string sceneName;
    public void LoadScene()
    {
        SceneManager.LoadScene(sceneName);
    }
    public void StartTransitioning(string sceneName)
    {
        this.sceneName = sceneName;
        GetComponent<Animator>().SetTrigger("Left");
    }

    public void ChangeVolumeSFX(System.Single value)
    {
        Debug.Log(value);
        Settings.SFX = value;
    }
    public void ChangeVolumeMusic(System.Single value)
    {
        Settings.musicVolume = value;
    }
}
