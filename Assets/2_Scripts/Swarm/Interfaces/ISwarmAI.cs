using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISwarmAI
{
    public void UpdateAI(Dictionary<uint, Boid> boids);
}
