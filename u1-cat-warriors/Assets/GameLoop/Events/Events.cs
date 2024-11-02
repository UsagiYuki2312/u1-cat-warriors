using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GameLoop
{
    [System.Serializable]
    public class GameObjectEvent : UnityEvent<GameObject> { }

    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }
}