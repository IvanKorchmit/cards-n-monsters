using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerLevel
{
    public static int Level = 1;
    public static int xp;
    public static int maxXP = 60;

    public static void GainXP(int xp)
    {
        PlayerLevel.xp += xp;
        if (PlayerLevel.xp >= maxXP)
        {
            LevelUp();
        }
    }
    public static void LevelUp()
    {
        xp = xp - maxXP;
        maxXP += 120;
        Level++;
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

public static class Trade
{
    public static bool canAfford(Item item, int money, ref int totalcost)
    {
        totalcost = (item.quantity * item.item.cost);
        return totalcost <= money;
    }
}