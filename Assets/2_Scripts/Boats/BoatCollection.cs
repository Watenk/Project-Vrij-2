using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatCollection : ADictCollection<Boat, BoatSettings>
{
    protected override Boat Construct(BoatSettings data)
    {
        throw new System.NotImplementedException();
    }

    protected override void Deconstruct(Boat instance) {}
}
