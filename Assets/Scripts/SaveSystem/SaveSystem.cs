using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

public static class SaveSystem 
{
    public static void SaveGame(Player player){
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        string path = Path.Combine(Application.persistentDataPath, "player.fun");

        FileStream stream = new FileStream(path, FileMode.Create);
        PlayerData data = new PlayerData(player);

        binaryFormatter.Serialize(stream, data);

        stream.Close(); 
    }

    public static PlayerData LoadGame(){
        string path = Path.Combine(Application.persistentDataPath, "player.fun");
        if(File.Exists(path)){
            BinaryFormatter binaryFormatter=  new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
 
            PlayerData data = binaryFormatter.Deserialize(stream) as PlayerData;
            stream.Close(); 

            return data;

        }else{
            return null;
        }
    }
}

