using System;
using System.Collections.Generic;
//using System.Linq;
//using System.Text;

public class AESingleton<T> where T : AESingleton<T>,new()
{
    protected static T m_instance = null;

    public static T Instance
    {
        get
        {
            if (null == m_instance)
            {
                m_instance = new T();
            }

            return m_instance;
        }
    }
}