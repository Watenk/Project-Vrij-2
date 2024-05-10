using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanCollection : ADictCollection<Human, Human>
{
    protected override Human Construct(Human data)
    {
        throw new System.NotImplementedException();
    }

    protected override void Deconstruct(Human instance)
    {
        throw new System.NotImplementedException();
    }
}
