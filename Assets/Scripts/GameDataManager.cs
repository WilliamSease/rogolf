using System;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class GameDataManager
{
    private const string GAME_DATA_SAVE_NAME = "gameData.sav";

    public static void SaveGameData(GameData data)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + GAME_DATA_SAVE_NAME, FileMode.Create);

        bf.Serialize(stream, data);
        stream.Close();
    }

    public static void SaveGameData(Game game)
    {
        GameData data = new GameData(game);
        SaveGameData(data);
    }

    public static GameData LoadGameData()
    {
        if (File.Exists(Application.persistentDataPath + GAME_DATA_SAVE_NAME))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + GAME_DATA_SAVE_NAME, FileMode.Open);

            GameData data = bf.Deserialize(stream) as GameData;

            stream.Close();

            return data;
        }
        else
        {
            throw new InvalidOperationException("Can't LoadGameData when GameData is null");
        }
    }

    public static void ResetGameData()
    {
        File.Delete(Application.persistentDataPath + GAME_DATA_SAVE_NAME);
    }
}

[Serializable]
public class GameData
{
    public HoleBag holeBag;
    public ItemBag itemBag;
    public PlayerAttributes playerAttributes;
    public TerrainAttributes terrainAttributes;

    public GameData()
    {
        this.holeBag = new HoleBag();
        this.itemBag = new ItemBag();
        this.playerAttributes = new PlayerAttributes();
        this.terrainAttributes = new TerrainAttributes();
    }

    public GameData(Game game)
    {
        this.holeBag = game.GetHoleBag();
        this.itemBag = game.GetItemBag();
        this.playerAttributes = game.GetPlayerAttributes();
        this.terrainAttributes = game.GetTerrainAttributes();
    }
}
