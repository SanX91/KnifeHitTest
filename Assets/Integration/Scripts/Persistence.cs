using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KnifeHitTest
{
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
