using System.Collections.Generic;
using UnityEngine;

public class ComboStateCache 
{
    enum ComboStateType
    {
        START
    }
    public ComboStateCache()
    {

    }
    private Dictionary<ComboStateType, ComboState> _comboStateCache;
}
