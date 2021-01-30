using UnityEngine;

// Based on http://wiki.unity3d.com/index.php/Singleton

/// <summary>
/// Be aware this will not prevent a non singleton constructor such as `T myT = new T();` To prevent that, add `protected T () {}` to your singleton class.
/// </summary>

public abstract class BASESingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;

    private static object _lock = new object();

    private static bool applicationIsQuitting = false;

    private void OnEnable()
    {
        applicationIsQuitting = false;
    }

    private void OnDestroy()
    {
        applicationIsQuitting = true;
    }

    public static T Instance
    {
        get
        {
            if (applicationIsQuitting)
            {
                Debug.LogWarning("[Singleton] Instance '" + typeof(T) + "' already destroyed on application quit." + " Won't create again - returning null.");
                return null;
            }

            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = (T)FindObjectOfType(typeof(T));

                    if (FindObjectsOfType(typeof(T)).Length > 1)
                    {
                        Debug.LogError("[Singleton] Something went really wrong " + " - there should never be more than 1 singleton!" + " Reopening the scene might fix it.");
                        return _instance;
                    }

                    if (_instance == null)
                    {
                        GameObject singletonPrefab = null;
                        GameObject singleton = null;

                        // Check if exists a singleton prefab on Resources Folder.
                        // -- Prefab must have the same name as the Singleton SubClass
                        singletonPrefab = (GameObject)Resources.Load(typeof(T).ToString(), typeof(GameObject));

                        // Create singleton as new or from prefab
                        if (singletonPrefab != null)
                        {
                            singleton = Instantiate(singletonPrefab);
                            _instance = singleton.GetComponent<T>();
                        }
                        else
                        {
                            singleton = new GameObject();
                            _instance = singleton.AddComponent<T>();
                        }
                        singleton.name = "(singleton) " + typeof(T).ToString();
                        DontDestroyOnLoad(singleton);

                        Debug.Log("[Singleton] An instance of " + typeof(T) + " is needed in the scene, so '" + singleton + "' was created with DontDestroyOnLoad.");
                    }
                    else
                    {
                        Debug.Log("[Singleton] Using instance already created: " + _instance.gameObject.name);
                    }
                }

                return _instance;
            }

        }
    }
}
