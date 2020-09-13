using System;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class GameDataManager
{
    private const string GAME_DATA_SAVE_NAME = "gameData.sav";

    public static void SaveGameData(Game game)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + GAME_DATA_SAVE_NAME, FileMode.Create);

        GameData data = new GameData(game);

        bf.Serialize(stream, data);
        stream.Close();
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
            return null;
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

    public GameData(Game game)
    {
        this.holeBag = game.holeBag;
        this.itemBag = game.itemBag;
    }
}
