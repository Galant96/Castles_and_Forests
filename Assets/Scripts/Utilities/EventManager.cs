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

[System.Serializable]
public class OnMechanismEvent : UnityEvent<bool> { };

[System.Serializable]
public class OnGetReward : UnityEvent<bool, Vector3, int, int> { };

[System.Serializable]
public class OnTeleportationEvent : UnityEvent<GameObject> { };

[System.Serializable]
public class OnCharacterSpawn : UnityEvent<GameObject> { };


