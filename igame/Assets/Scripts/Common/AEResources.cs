using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System;

public class AEResources
{

	private static Dictionary<string, UnityEngine.Object> allDic = new Dictionary<string, UnityEngine.Object>();
	public static void Load(string resourcePath, Type systemTypeInstance, Action<UnityEngine.Object> action)
	{
		UnityEngine.Object obj = null;
		if (allDic.ContainsKey(resourcePath))
		{
			obj = allDic[resourcePath];
			action(obj);
			return;
		}
			
		obj = Resources.Load(resourcePath, systemTypeInstance);
		if (obj == null)
		{              
			UnityEngine.Debug.LogError("读取路径错误：");
			UnityEngine.Debug.LogError(resourcePath);
			return;
		}
		allDic.Add(resourcePath , obj);
		action(obj);
	}

	public static void LoadSingle(string resourcePath, Action<UnityEngine.Object> action)
	{
		UnityEngine.Object obj = null;
		if (allDic.ContainsKey(resourcePath))
		{
			obj = allDic[resourcePath];
			action(obj);
			return;
		}
			
		obj = Resources.Load(resourcePath);
		if (obj == null)
		{
			UnityEngine.Debug.LogError("读取路径错误：");
			UnityEngine.Debug.LogError(resourcePath);
			return;
		}
		if (!allDic.ContainsKey(resourcePath))
			allDic.Add(resourcePath , obj);
		action(obj);

	}
		
}


/// <summary>
/// 对象池相关
/// </summary>
public class PoolMgr:AESingleton<PoolMgr>
{
	private Dictionary<string , PoolBase> dic = new Dictionary<string, PoolBase> ();

	private  uint defaultNum = 30;

	public void GetNew(string key , Action<GameObject> action )//  , uint num = defaultNum)
	{
		GameObject obj = null;
		if (!dic.ContainsKey (key)) 
		{
			PoolBase pool = new PoolBase (key, defaultNum);
			dic.Add (key, pool);
			obj = pool.Get ();
		} 
		else 
		{
			obj = dic [key].Get ();
		}
		if (obj == null)
			AEResources.LoadSingle (key, (obj1) => {
				obj = obj1 as GameObject;
				GameObject go = GameObject.Instantiate(obj);
				action (go);
			});
		else
			action (obj);
	}

	public void Store(string key , GameObject obj)
	{
		if (dic.ContainsKey (key))
			dic [key].Store (obj);
//		else
//			Debug.Log(key + "Don't have created");
	}

	public void ClearByKey(string key)
	{
		if (dic.ContainsKey (key))
			dic [key].DisPose ();
	}

	public void ClearAll()
	{
		foreach (var item in dic) 
		{
			item.Value.DisPose ();
		}
		dic.Clear ();
	}
}



public class PoolBase
{
	private uint _size;
	private string _key;
	private Stack<GameObject> objectStack =  new Stack<GameObject>(); 

	public PoolBase(string key , uint size)
	{
		_key = key;
		_size = size;
	}

	public GameObject Get()
	{
		if (objectStack.Count > 0) 
		{
			return objectStack.Pop ();
		}
		return null;
	}

	public void Store(GameObject obj)
	{
		if (objectStack.Count >= _size)
			GameObject.DestroyImmediate (obj);
		else 
		{
			obj.SetActive (false);
			objectStack.Push (obj);
		}
	}

	public void DisPose()
	{
		while (objectStack.Count > 0) 
		{
			GameObject obj = objectStack.Pop ();
			GameObject.DestroyImmediate (obj);
		}
	}
}
