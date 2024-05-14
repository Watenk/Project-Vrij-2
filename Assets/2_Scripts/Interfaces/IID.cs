using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IID 
{
	public uint ID { get; }
	
	public void ChangeID(uint newID);
}
