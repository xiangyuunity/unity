using UnityEngine;

public class SceneMgr:AESingleton<SceneMgr>
{
    public enum MgrType
    {
        None,
        ResMgr,
        UIMgr,
        LuaMgr
    }

    private static SceneMgr m_instance;

    public GameObject m_objMgr;


    public void Init()
    {
        if (m_objMgr == null)
            m_objMgr = new GameObject("ObjMgr");

//        m_objMgr.AddComponent<UIManager>();
        Object.DontDestroyOnLoad(m_objMgr);
    }

    public Component GetObjMgr(MgrType type)
    {
        Component comp = null;
        if (m_objMgr == null)
            return null;

        switch (type)
        {
//            case MgrType.LuaMgr:
//
//            {
//                comp = m_objMgr.GetComponent<DKLuaMgr>();
//                if (comp == null)
//                    comp = m_objMgr.AddComponent<DKLuaMgr>();
//            }
//
//                break;
//            case MgrType.ResMgr:
//            {
//                comp = m_objMgr.GetComponent<ResourceMgr>();
//                if (comp == null)
//                    comp = m_objMgr.AddComponent<ResourceMgr>();
//            }
//                break;
//            case MgrType.UIMgr:
//            {
//                comp = m_objMgr.GetComponent<UIManager>();
//                if (comp == null)
//                    comp = m_objMgr.AddComponent<UIManager>();
//            }
//                break;
//            default:
//                break;
        }
        return comp;
    }

    public void Update()
    {
    }

    public void Destroy()
    {
        if (m_objMgr != null)
            Object.Destroy(m_objMgr);
    }
}