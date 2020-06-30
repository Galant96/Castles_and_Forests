using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[System.Serializable]
public class OnCollectibleHitEvent : UnityEvent <int> { };

[System.Serializable]
public class OnScoreEvent : UnityEvent<int> { };

[System.Serializable]
public class OnThrowEvent : UnityEvent<Vector2> { };
