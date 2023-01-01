using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private static T Instance { get; set; }

    public static T GetInstance => Instance;

    protected virtual void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = (T)this;
    }

    protected virtual void OnDestrory()
    {
        if (Instance == this)
            Instance = null;
    }
}
