using UnityEngine;

/// <summary>
/// Be aware that by default, Singletons destroy on scene loads, and are 
/// created programatically when referenced, if none is present in the scene, 
/// when Singleton.Instance is called.
/// 
/// Be aware this will not prevent a non singleton constructor
///   such as `T myT = new T();`
/// To prevent that, add `protected T () {}` to your singleton class.
/// 
/// As a note, this is made as MonoBehaviour because we need Coroutines.
/// </summary>
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;
    private static object _lock = new object();
    
    public static T Instance
    {
        get
        {
			// Read singleton attribute options
			var singletonOptions = System.Attribute.GetCustomAttribute(typeof(T), typeof(SingletonOptions)) as SingletonOptions;
			if (singletonOptions == null)
			{
				singletonOptions = new SingletonOptions(); // Use defaults
			}

			if (applicationIsQuitting && singletonOptions.DontDestroyOnLoad == false)
            {
                Debug.LogWarningFormat("[Singleton] Instance '{0}' already destroyed on application quit. Won't create again - returning null.", typeof(T));
                return null;
            }
            
            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = (T)FindObjectOfType(typeof(T));
                    
                    if (FindObjectsOfType(typeof(T)).Length > 1)
                    {
                        foreach(var obj in FindObjectsOfType(typeof(T)))
                        {
                            Debug.Log(string.Format("Multiple singletons of type {0} detected. Found on {1}", typeof(T).ToString(), obj));
                            if((T)obj != _instance && singletonOptions.DontDestroyOnLoad == true) {
                                Debug.Log("Destroying: " + obj); 
                                Destroy(((T)obj).gameObject);
                            }
                        }
                        return _instance;
                    }

                    if (_instance == null && singletonOptions.CreateInstanceAutomatically == true)
                    {
                        GameObject singleton = new GameObject();
                        _instance = singleton.AddComponent<T>();
                        singleton.name = "(singleton) " + typeof(T).ToString();

                        if (singletonOptions.DontDestroyOnLoad)
                        {
                            DontDestroyOnLoad(singleton);
                        }

                        //Debug.LogFormat("[Singleton] An instance of {0} is needed in the scene, so '{1}' was created with options: {2}", typeof(T), singleton, singletonOptions);
                    }
                    else if(_instance == null && singletonOptions.CreateInstanceAutomatically == false)
                    {
                        Debug.LogErrorFormat("[Singleton] An instance of {0} is needed in the scene, but is not allowed to automatically create one. Please add one to the scene manually.", typeof(T));
                    }
                    else
                    {
                        Debug.LogFormat("[Singleton] Using instance already created: {0}", _instance.gameObject.name);
                    }
                }
                
                return _instance;
            }
        }
    }
    
    private static bool applicationIsQuitting = false;
 
}