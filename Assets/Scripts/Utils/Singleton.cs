
using UnityEngine;

public abstract class Singleton<T> where T : Singleton<T>, new()
{
    #region Fields
    
    private static T instance;

    #endregion

    #region Properties
    
    public static T Instance
    {
        get
        {
            if (instance != null)
                return instance;

            instance = new T();
            instance.Init();
            return instance;
        }
    }

    #endregion

    #region Methods

    public static void CreateInstance()
    {
        DestroyInstance();
        instance = Instance;
    }

    public static void DestroyInstance()
    {
        if (instance == null) 
            return;

        instance.Clear();
        instance = default(T);
    }
    
    protected void Init()
    {
        OnInit();
    }

    public void Clear()
    {
        OnClear();
    }
    
    protected virtual void OnInit()
    {
    }

    protected virtual void OnClear()
    {
    }

    #endregion
}