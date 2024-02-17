using UnityEngine;
using Newtonsoft.Json;
using System.IO;

public class SaveGameData
{
    public int lastScore = 0;
    public int bestScore = 0;

}

public class GameSaver : MonoBehaviour
{
    public SaveGameData CurrentGameData {get; private set;}

    public bool IsLoaded => CurrentGameData != null;

    private string saveDirectoryPath => Path.Combine(Application.persistentDataPath, "SaveScore.Json");

    public void SaveGame(SaveGameData newSaveGameData)
    {
        using(FileStream streamReader = new FileStream(saveDirectoryPath, FileMode.OpenOrCreate, FileAccess.Write))
        using(StreamWriter streamWriter = new StreamWriter(streamReader))
        using(JsonTextWriter jsonTextWriter = new JsonTextWriter(streamWriter))
        {
            JsonSerializer jsonSerializer = new();
            jsonSerializer.Serialize(jsonTextWriter, newSaveGameData);
        }

        CurrentGameData = newSaveGameData;
    }

    public void LoadGame()
    {
        CurrentGameData = LoadScoreGame() ?? new SaveGameData();

    }

    private SaveGameData LoadScoreGame()
    {
        using(FileStream fileStream = new FileStream(saveDirectoryPath, FileMode.OpenOrCreate, FileAccess.Read))
        using(StreamReader streamReader = new StreamReader(fileStream))
        using(JsonTextReader jsonTextReader = new JsonTextReader(streamReader))
        {
            var jsonSerializer = new JsonSerializer();
            SaveGameData data = jsonSerializer.Deserialize<SaveGameData>(jsonTextReader);
            return data;
        }


    }

}
