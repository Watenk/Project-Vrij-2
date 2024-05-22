using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISingleton<T>
{
    public static T Instance { get; }
}
