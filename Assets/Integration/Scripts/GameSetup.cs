using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KnifeHitTest
{
    public class GameSetup : MonoBehaviour
    {
        [SerializeField]
        UIManager uiManager;
        [SerializeField]
        Log log;
        [SerializeField]
        KnifeThrow knifeThrow;

        Persistence persistence;
        ResourceData resourceData;

        const string ResourceSaveKey = "ResourceSave";

        private void OnEnable()
        {
            
        }

        private void OnDisable()
        {
            
        }

        private IEnumerator Start()
        {
            persistence = new Persistence();
            resourceData = persistence.LoadData<ResourceData>(ResourceSaveKey);

            if(resourceData == null)
            {
                resourceData = new ResourceData();
            }

            uiManager = Instantiate(uiManager);
            yield return null;
            uiManager.Initialize();
            yield return null;
            log = Instantiate(log);
            yield return null;
            knifeThrow = Instantiate(knifeThrow);
        }

        void SaveResourceData()
        {
            persistence.SaveData(ResourceSaveKey, resourceData);
        }
    } 
}
