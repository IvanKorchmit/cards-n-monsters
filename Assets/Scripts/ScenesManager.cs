using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ScenesManager : MonoBehaviour
{
    public AudioClip testClip;
    public string sceneName;
    public void LoadScene()
    {
        SceneManager.LoadScene(sceneName);
    }
    public void StartTransitioning(string sceneName)
    {
        string SAVE_PATH = Application.dataPath + "/save/SESSION.json";
        if(File.Exists(SAVE_PATH))
        {
            File.Delete(SAVE_PATH);
        }
        this.sceneName = sceneName;
        GetComponent<Animator>().SetTrigger("Left");
    }
    public void ContinueGame(string sceneName)
    {
        this.sceneName = sceneName;
        GetComponent<Animator>().SetTrigger("Left");
    }
    public void ChangeVolumeSFX(System.Single value)
    {
        Settings.SFX = value;
        GetComponent<SoundEvents>().Play(testClip);
    }
    public void ChangeVolumeMusic(System.Single value)
    {
        GetComponent<AudioSource>().volume = value;
        Settings.musicVolume = value;
    }
}
