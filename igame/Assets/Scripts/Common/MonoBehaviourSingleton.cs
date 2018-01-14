using UnityEngine;
using System.Collections;
using System;

abstract public class MonoBehaviourSingleton<T> : MonoBehaviour,IMonoBehaviourSingleton where T : MonoBehaviourSingleton<T> 
{
	protected static T m_instance = null;

	public static T Instance
	{
		get
		{
			if (null == m_instance)
			{
				throw new System.NullReferenceException("Use " + typeof(T).FullName + ".CreateInstance must create.");
			}
			
			return m_instance;
		}
	}


    public static void CreateInstance(bool dontDestroyOnLoad, Transform root = null)
	{
		m_instance = GameObject.FindObjectOfType(typeof(T)) as T;

		if (null == m_instance)
		{ 
			GameObject gobj = new GameObject(typeof(T).FullName);
			m_instance = gobj.AddComponent<T>();
            if(root)
            {
                gobj.transform.parent = root;
            }
		}

		if (dontDestroyOnLoad)
		{
			GameObject.DontDestroyOnLoad(m_instance.gameObject);
		}
	}
	
	public void DestroyInstance()
	{
		if (null != m_instance)
		{
			GameObject.Destroy(m_instance.gameObject);
		}
		
		m_instance = null;
	}


    virtual public void AEUpdate()
    {
        throw new NotImplementedException();
    }
}
