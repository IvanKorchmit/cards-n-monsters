using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
public static class PlayerLevel
{
    public static int Level = 1;
    public static int xp;
    public static int maxXP = 60;
    public static Stats Player;

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
        Player.maxHealth += 10;
        Player.Heal(10);
        xp = xp - maxXP;
        maxXP += 120;
        Level++;
        new PlayerData().Save();
    }
    static PlayerLevel()
    {
        Debug.Log("Invoked static constructor");
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Stats>();
        BaseItem[] items = Resources.LoadAll<BaseItem>("Items");
        PlayerData playerData = new PlayerData();
        playerData = playerData.LoadFromJSON();
        if(playerData != null)
        {
            xp = playerData.xp;
            maxXP = playerData.xpMax;
            Level = playerData.Level;
            Player.maxHealth = playerData.Health;
            Player.Heal(playerData.Health);
            PlayerData.ItemData[] its = playerData.plInventory;
            for (int i = 0; i < its.Length; i++)
            {
                if(its[i].id != -1)
                {
                    foreach (BaseItem item in items)
                    {
                        if(item.id == its[i].id)
                        {
                            Player.inventory[i] = new Item(its[i].quantity,item);
                            break;
                        }
                    }
                }
            }
            // weapon
            foreach (BaseItem item in items)
            {
                if (playerData.weapon != -1)
                {
                    if (item.id == playerData.weapon)
                    {
                        Player.weapon = item as Weapon;
                        break;
                    }
                }
                else
                {
                    Player.weapon = null;
                }
            }
            // helmet
            foreach (BaseItem item in items)
            {
                if (playerData.helmet != -1)
                {
                    if (item.id == playerData.helmet)
                    {
                        Player.helmet = item as Helmet;
                        break;
                    }
                }
                else
                {
                    Player.helmet = null;
                }
            }
            // chestplate
            foreach (BaseItem item in items)
            {
                if (playerData.chestplate != -1)
                {
                    if (item.id == playerData.chestplate)
                    {
                        Player.chestplate = item as Chestplate;
                        break;
                    }
                }
                else
                {
                    Player.chestplate = null;
                }
            }
            // leggings
            foreach (BaseItem item in items)
            {
                if (playerData.leggings != -1)
                {
                    if (item.id == playerData.leggings)
                    {
                        Player.leggings = item as Leggings;
                        break;
                    }
                }
                else
                {
                    Player.leggings = null;
                }
            }
        }
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
[System.Serializable]
public class PlayerData
{
    [System.Serializable]
    public struct ItemData
    {
        public int id;
        public int quantity;
        public ItemData(int id, int quantity)
        {
            this.id = id;
            this.quantity = quantity;
        }
    }

    readonly string SAVE_PATH;
    public PlayerData()
    {
        SAVE_PATH = Application.dataPath + "/save/";
    }
    public ItemData[] plInventory;
    public int xp;
    public int xpMax = 60;
    public int Level = 1;
    public int Health = 100;


    public int weapon;
    public int helmet;
    public int chestplate;
    public int leggings;


    public void Save()
    {
        Debug.Log("Saving");
        Item[] inv = PlayerLevel.Player.inventory;
        xp = PlayerLevel.xp;
        xpMax = PlayerLevel.maxXP;
        Level = PlayerLevel.Level;
        Health = PlayerLevel.Player.MaxHealth;
        plInventory = new ItemData[inv.Length];
        Stats pl = PlayerLevel.Player;
        weapon = pl.weapon != null ? pl.weapon.id : -1;
        helmet = pl.helmet != null ? pl.helmet.id : -1;
        chestplate = pl.chestplate != null ? pl.chestplate.id : -1;
        leggings = pl.leggings != null ? pl.leggings.id : -1;
        for (int i = 0; i < plInventory.Length; i++)
        {
            if (inv[i].item != null)
            {
                plInventory[i] = new ItemData(inv[i].item.id, inv[i].quantity);
            }
            else
            {
                plInventory[i] = new ItemData(-1, 0);
            }
        }
        string json = JsonUtility.ToJson(this,true);
        if (File.Exists(SAVE_PATH+ "SESSION.json"))
        {
            File.WriteAllText(SAVE_PATH + "SESSION.json", json);
        }
        else
        {
            Directory.CreateDirectory(SAVE_PATH);
            FileStream fs = File.Create(SAVE_PATH + "SESSION.json");
            byte[] bytes = Encoding.UTF8.GetBytes(json);
            fs.Write(bytes, 0, bytes.Length);
        }
    }
    public PlayerData LoadFromJSON()
    {
        if (File.Exists(SAVE_PATH + "SESSION.json"))
        {
            Debug.Log("success");
            string json = File.ReadAllText(SAVE_PATH + "SESSION.json", Encoding.UTF8);

            return JsonUtility.FromJson<PlayerData>(json);
        }
        Debug.Log("fail");

        return null;
    }
}