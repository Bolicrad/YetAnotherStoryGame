using System;
using UnityEngine;

namespace Triggers
{
    public class TriggerBase : MonoBehaviour
    {
        protected Collider triggerBox;

        protected void Awake()
        {
            triggerBox = GetComponent<Collider>();
        }
    }
}
