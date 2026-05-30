using UnityEngine;
using System.Collections.Generic;
public class SingletonConstructor<TT> : MonoBehaviour
{
    static TT _instance;
    public static TT Instance { get { return _instance; } }
    [SerializeField] string _tag;

    /// Function: ConstructSingleton
    /// <summary>
    /// Purpose: Constructs a singleton based on the script it is attached to
    /// </summary>
    /// <param name="baseScript">takes the child script</param>
    /// <returns> Nothing </returns>
    /// <remarks>Note: use this as the parent class and add the child class in between the <> symbols </remarks>
    [SerializeField] bool dontDestroyOnLoad = false;
    public void ConstructSingleton(TT baseScript)
    {
        gameObject.tag = _tag;
        if (_instance != null && !AreEqual(_instance, baseScript))
            Destroy(gameObject);
        else
            _instance = baseScript;

        if (dontDestroyOnLoad)
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    protected virtual void OnDestroy()
    {
        if (_instance != null && AreEqual(_instance, this.GetComponent<TT>()))
        {
            _instance = default(TT);
        }
    }
    /// Function: AreEqual
    /// <summary>
    /// Purpose: Compares the equality of two generic classes
    /// </summary>
    /// <returns> bool </returns>
    /// <remarks>Note: needed because templates can always change</remarks>
    public bool AreEqual<T>(T left, T right)
    {
        return EqualityComparer<T>.Default.Equals(left, right);
    }
}
