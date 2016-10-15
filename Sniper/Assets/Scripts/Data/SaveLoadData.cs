using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveLoadData {

    public static void SaveData() {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + "/gameData.ivr", FileMode.Create);

        GameData data = new GameData();

        bf.Serialize(stream, data);
        stream.Close();
    }

    public static GameData LoadData() {
        if (File.Exists(Application.persistentDataPath + "/gameData.ivr")) {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/gameData.ivr", FileMode.Open);

            GameData data = bf.Deserialize(stream) as GameData;

            stream.Close();
            return data;
        }else {
            Debug.LogError("File doesn't Exist");
            return null;
        }
    }
}

[Serializable]
public class GameData {
    public int totalHits;
    public int totalBullets;
    public int rangeHighscore;
    public string missionWeapon;
    public string missionScope;
    public int[] missionScore;
    public double longestHit;
    public int cash;

    public GameData() {
        totalHits = DataHolder.totalHits;
        totalBullets = DataHolder.totalBullets;
        rangeHighscore = DataHolder.rangeHighScore;

        //Temporarly saved between missions
        missionWeapon = DataHolder.missionWeapon;
        missionScope = DataHolder.missionScope;

        //Stores mission Scores;
        missionScore = DataHolder.missionScore;
        longestHit = DataHolder.longestHit;

        //Cash
        cash = DataHolder.cash;
    }
}
