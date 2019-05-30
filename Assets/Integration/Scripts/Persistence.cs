using UnityEngine;

namespace KnifeHitTest
{
    /// <summary>
    /// The persistence class.
    /// Used for saving and loading data to Playerprefs.
    /// Serializes data in JSON format.
    /// </summary>
    public class Persistence
    {
        public void SaveData(string key, object data)
        {
            string json = JsonUtility.ToJson(data);
            PlayerPrefs.SetString(key, json);
        }

        public T LoadData<T>(string key)
        {
            return JsonUtility.FromJson<T>(PlayerPrefs.GetString(key));
        }

        public void ClearAllData()
        {
            PlayerPrefs.DeleteAll();
            Debug.Log("Data Reset");
        }
    }
}
