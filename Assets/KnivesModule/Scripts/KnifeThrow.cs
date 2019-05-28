using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KnifeHitTest
{
    public class KnifeThrow : MonoBehaviour
    {
        [SerializeField]
        int stageKnives;
        [SerializeField]
        private PooledAttachableFactory knifeFactory;
        [SerializeField]
        Transform knifePlaceholder;

        IController controller;

        private void Start()
        {
            controller = new MouseController();
            StartCoroutine(StageRoutine());
        }

        IEnumerator StageRoutine()
        {
            while(stageKnives>0)
            {
                Attachable knife = knifeFactory.GetEntity();
                knife.transform.position = knifePlaceholder.position;
                knife.transform.up = knifePlaceholder.up;
                knife.gameObject.SetActive(true);

                while(!controller.HasTapped())
                {
                    yield return null;
                }

                Debug.Log("The Hell");
                IThrowable throwable = (IThrowable)knife;
                throwable.Throw();
                stageKnives--;

                yield return new WaitForSeconds(1);
            }
        }
    } 
}
