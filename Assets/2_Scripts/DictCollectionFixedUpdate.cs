using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DictCollectionFixedUpdate<T> : DictCollection<T>, IFixedUpdateable where T : IID, IFixedUpdateable
{
    public void FixedUpdate() {
        foreach (var obj in instances) 
        {
        	obj.Value.FixedUpdate();
        }
    }
}
