using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableFrustumCulling : MonoBehaviour
{
	void Start()
	{
		MeshRenderer renderer = GetComponent<MeshRenderer>();
		
		Bounds newBounds = new Bounds(Vector3.zero, new Vector3(1000, 0, 1000));
		renderer.localBounds = newBounds;
	}
}
