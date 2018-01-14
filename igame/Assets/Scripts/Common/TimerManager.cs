using System;
using System.Collections.Generic;
using UnityEngine;


public class TimerManager:AESingleton<TimerManager>
{
    private Dictionary<object, TimeVo> _dic = new Dictionary<object, TimeVo>();
    private List<TimeVo> _addList = new List<TimeVo>();
    private List<TimeVo> _removeList = new List<TimeVo>();

    public  void OnceTimer(int delay, Action act, AETimeType type = AETimeType.NORMAL)
    {
        Instance.SelfOnceTimer(delay,act,type);
    }

    public  void OnceTimer(int delay, Action<object[]> cb, AETimeType type = AETimeType.NORMAL, params object[] args)
    {
        Instance.SelfOnceTimer(delay,cb,type,args);
    }

    public  void LoopTimer(int delay, Action cb, int totalCount = 0, AETimeType type = AETimeType.NORMAL)
    {
        Instance.SelfLoopTimer(delay,cb,totalCount,type);
    }

    public  void RemoveTimer(Action cb)
    {
        Instance.SelfRemoveTimer(cb);
    }

    public  void RemoveTimer(Action<object[]> cb)
    {
        Instance.SelfRemoveTimer(cb);
    }

    public  void RemoveTimer(AETimeType type)
    {
        Instance.SelfRemoveTimer(type);
    }

    private void SelfOnceTimer(int delay, Action cb, AETimeType type = AETimeType.NORMAL)
    {
        TimeVo vo = new TimeVo();
        vo.m_delay = delay;
        vo.m_act = cb;
        vo.m_isOnce = true;
        vo.m_type = type;
        _addList.Add(vo);
    }
        
    private void SelfOnceTimer(int delay, Action<object[]> cb, AETimeType type = AETimeType.NORMAL, params object[] args)
    {
        TimeVo vo = new TimeVo();
        vo.m_delay = delay;
        vo.m_act2 = cb;
        vo.m_parms = args;
        vo.m_isOnce = true;
        vo.m_type = type;
        _addList.Add(vo);
    }

    private void SelfLoopTimer(int delay, Action cb, int totalCount = 0, AETimeType type = AETimeType.NORMAL)
    {
        TimeVo vo = new TimeVo();
        vo.m_delay = delay;
        vo.m_act = cb;
        vo.m_isOnce = false;
        vo.m_totalCount = totalCount;
        vo.m_type = type;
        _addList.Add(vo);
    }

    private void SelfRemoveTimer(Action cb)
    {
        if (_dic.ContainsKey(cb))
        {
            _removeList.Add(_dic[cb]);
        }
    }

    private void SelfRemoveTimer(Action<object[]> cb)
    {
        if (_dic.ContainsKey(cb))
        {
            _removeList.Add(_dic[cb]);
        }
    }

    private void SelfRemoveTimer(AETimeType type)
    {
        foreach (TimeVo vo in _dic.Values)
        {
            if (vo.m_type == type)
            {
                _removeList.Add(vo);
            }
        }
    }

    public void FixedUpdate()
    {
        TimeVo vo;
        int len;

        //remove
        len = _removeList.Count;
        for (int i = 0; i < len; ++i)
        {
            vo = _removeList[i];
            if (vo.m_act2 != null)
            {
                _dic.Remove(vo.m_act2);
            }
            else
            {
                _dic.Remove(vo.m_act);
            }
        }
        _removeList.Clear();

        //add
        len = _addList.Count;
        for (int i = 0; i < len; ++i)
        {
            vo = _addList[i];
            if (vo.m_act2 != null)
            {
                _dic[vo.m_act2] = vo;
            }
            else
            {
                _dic[vo.m_act] = vo;
            }
        }
        _addList.Clear();

        //do
        foreach (TimeVo item in _dic.Values)
        {
            item.m_totalDelay += (int)(Time.fixedDeltaTime * 1000);
            if (item.m_totalDelay < item.m_delay)
                continue;

            if (item.m_act2 != null)
            {
                item.m_act2(item.m_parms);
            }
            else
            {
                item.m_act();
            }
            if (item.m_isOnce)
            {
                _removeList.Add(item);
            }
            else
            {
                item.m_totalDelay -= item.m_delay;
                if (item.m_totalCount > 0)
                {
                    item.m_totalCount++;
                    if(item.m_currentCount >= item.m_totalCount)
                    {
                        _removeList.Add(item);
                    }
                }
            }
        }

    }
}

class TimeVo
{
    public int m_delay = 100;
    public Action m_act;
    public Action<object[]> m_act2;
    //带参数的回调
    public object[] m_parms;
    //保存参数
    public bool m_isOnce = false;
    public int m_totalCount = 0;
    public int m_currentCount = 0;
    public int m_totalDelay = 0;
    public AETimeType m_type = AETimeType.NORMAL;
    //类型,可一次性删除所有相同类型定时器
}

public enum AETimeType
{
    NORMAL,
    BATTLE,
}
    



