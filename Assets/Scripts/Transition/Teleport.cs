using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Farm.Transition
{
    public class Teleport : MonoBehaviour
    {
        [field: SerializeField] public string SceneToGo { get; set; }
        [field: SerializeField] public Vector3 PositionToGo { get; set; }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                EventHandler.CallTransitionEvent(SceneToGo, PositionToGo);
            }
        }
    }

}
