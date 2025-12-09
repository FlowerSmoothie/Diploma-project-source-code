using UnityEngine;
using System.IO;
using System;
using Misc.Saving;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    private static string Path => Application.persistentDataPath + "/SaveData.json";

    private static readonly string keyword = "SomeRandomKeyword";

    private static string EncryptDecrypt(string original)
    {
        StringBuilder result = new StringBuilder("");

        for (int i = 0; i < original.Length; i++)
        {
            result.Append((char)(original[i] ^ keyword[i % keyword.Length]));
        }

        return result.ToString();
    }

    public static bool SaveData(DataHolder data)
    {
        try
        {
            string json = JsonUtility.ToJson(data, true);

            using (StreamWriter writer = new StreamWriter(Path))
            {
                writer.Write(EncryptDecrypt(json));
            }

            return true;
        }
        catch
        {
            return false;
        }
    }

    public static bool DeleteData()
    {
        try
        {
            File.Delete(Path);

            return true;
        }
        catch (Exception e)
        {
            Debug.Log(e);
            return false;
        }
    }

    public static DataHolder LoadData()
    {
        try
        {
            if (File.Exists(Path))
            {
                string raw = string.Empty;

                using (StreamReader reader = new StreamReader(Path))
                {
                    raw = reader.ReadToEnd();
                }

                DataHolder data = JsonUtility.FromJson<DataHolder>(EncryptDecrypt(raw));

                return data;
            }
            else
            {
                return null;
            }
        }
        catch (Exception e)
        {
            Debug.Log(e);
            return null;
        }
    }
}