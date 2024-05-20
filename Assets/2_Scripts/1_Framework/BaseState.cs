using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState<T>
{
    public abstract void Enter();
    public abstract void Exit();
    public abstract void Update();
}
