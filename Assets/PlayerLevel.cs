using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerLevel
{
    public static int Level;
    public static int xp;
    public static int maxXP;

    public static void GainXP(int xp)
    {
        PlayerLevel.xp += xp;
        if (PlayerLevel.xp > maxXP)
        {
            LevelUp();
        }
    }
    public static void LevelUp()
    {

    }

}
public static class MusicZoneTheme
{
    public static AudioClip newMusic;

    public static void ChangeTune()
    {
        Camera.main.GetComponent<AudioSource>().clip = newMusic;
    }


}