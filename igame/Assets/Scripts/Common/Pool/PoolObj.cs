
using System.Collections.Generic;
using UnityEngine;

class PoolObj<T> where T : class, new()
{
	private uint _size; 
	private string _key;
	private Stack<T> m_objectStack = new Stack<T>();

	public PoolObj(uint size, string key)
	{
		_size = size;
		_key = key;
	}

	public string assetKey
	{
		get{
			return _key;
		}
	}

	public T New(Object asset)
	{
		GameObject go = null;
		if (m_objectStack.Count > 0)
		{
			go = m_objectStack.Pop() as GameObject;
		}
		if (go != null) return go as T;
		return Object.Instantiate(asset) as T;
	}

	public void Store(T t)
	{
		GameObject go = t as GameObject;
		if (m_objectStack.Count >= _size)
		{
			Object.Destroy(go);
			return;
		}
		if (go != null) go.SetActive(false);
		m_objectStack.Push(t);
	}

	public void Dispose()
	{
		while (m_objectStack.Count > 0) {
			T t = m_objectStack.Pop ();
			if (t is Object) {
				Object.Destroy (t as Object);
			}
		}
	}
}