using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

namespace Watenk
{

public static class NavMeshUtil
{
	public static Vector3 GetRandomPositionOnNavMesh(NavMeshSurface surface)
	{
		NavMeshHit hit;
		Vector3 randomPosition = Vector3.zero;
		float range = (surface.size.x + surface.size.y) / 2;
		if (NavMesh.SamplePosition(new Vector3(surface.center.x + UnityEngine.Random.Range(-range, range), 0, UnityEngine.Random.Range(-range, range)), out hit, 10f, NavMesh.AllAreas))
		{
			randomPosition = hit.position;
		}
		return randomPosition;
	}
	
	public static Vector3 GetRandomPositionOnNavMesh(Vector3 center, float range)
	{
		NavMeshHit hit;
		Vector3 randomPosition = Vector3.zero;
		if (NavMesh.SamplePosition(new Vector3(center.x + UnityEngine.Random.Range(-range, range), 0, UnityEngine.Random.Range(-range, range)), out hit, 10f, NavMesh.AllAreas))
		{
			randomPosition = hit.position;
		}
		return randomPosition;
	}
}

public static class ArrayUtil
{
	/// <summary> Inserts a object into an array </summary>
	/// <typeparam name="T"> the type of the array </typeparam>
	/// <param name="oldArray"> The array to insert the object into </param>
	/// <param name="insertObject"> The object to insert </param>
	/// <param name="insertLocation"> The index to insert the object </param>
	/// <returns> A new array with the object inserted into it </returns>
	public static T[] InsertObject<T>(T[] oldArray, T insertObject, int insertLocation)
	{
		T[] newArray = new T[oldArray.Length + 1];
		// Copy objects before insert pos
		for (int i = 0; i < insertLocation; i++)
		{
			newArray[i] = oldArray[i];
		}

		// Insert object
		newArray[insertLocation] = insertObject;

		// Copy objects after insert pos
		for (int i = insertLocation; i < oldArray.Length; i++)
		{
			newArray[i + 1] = oldArray[i];
		}
		
		return newArray;
	}
}

public static class UnityUtil
{
	public static T FindInMonobehaviours<T>()
	{
		MonoBehaviour[] allMonoBehaviours = MonoBehaviour.FindObjectsOfType<MonoBehaviour>();

		foreach (MonoBehaviour monoBehaviour in allMonoBehaviours)
		{
			Type type = monoBehaviour.GetType();
			FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

			foreach (FieldInfo field in fields)
			{
				if (field.FieldType == typeof(T))
				{
					return (T)field.GetValue(monoBehaviour);
				}
			}
		}

		Debug.LogError("No " + typeof(T).Name + " found in any MonoBehaviour in the scene!");
		return default;
	}
}

public static class DebugUtil
{
	/// <summary> Checks if game is running in the editor and throws a Log. </summary>
	/// <param name="message">The message the log will display.</param>
	public static void ThrowLog(string message)
	{
		#if UNITY_EDITOR
			Debug.Log(message);
		#endif
	}
	
	/// <summary> Checks if game is running in the editor and throws a LogWarning. </summary>
	/// <param name="message">The message the warning will display.</param>
	public static void ThrowWarning(string message)
	{
		#if UNITY_EDITOR
			Debug.LogWarning(message);
		#endif
	}
	
	/// <summary> Checks if game is running in the editor and throws a LogError. </summary>
	/// <param name="message">The message the error will display.</param>
	public static void ThrowError(string message)
	{
		#if UNITY_EDITOR
			Debug.LogError(message);
		#endif
	}
	
	public static T TryCast<T>(object objectToCast)
	{
		if (!(objectToCast is T)) ThrowError("Cast to type " + typeof(T).Name + " failed");
		return (T)objectToCast;
	}
}

public static class MathUtil
{
	/// <summary> Map a value from a source range to a target range. </summary>
	public static float Map(float value, float minSource, float maxSource, float minTarget, float maxTarget)
	{
		return (value - minSource) / (maxSource - minSource) * (maxTarget - minTarget) + minTarget;
	}
	
	/// <summary> Checks if a vector is in between 2 bounds </summary>
	public static bool IsVectorInBounds(Vector2Int pos, Vector2Int boundPos1, Vector2Int boundPos2)
	{
		if (pos.x < boundPos1.x || pos.x >= boundPos2.x) { return false; }
		if (pos.y < boundPos1.y || pos.y >= boundPos2.y) { return false; }
		return true;
	}
	
	/// <summary> Checks if a vector is in between 2 bounds </summary>
	public static bool IsVectorInBounds(Vector2 pos, Vector2 boundPos1, Vector2 boundPos2)
	{
		if (pos.x < boundPos1.x || pos.x >= boundPos2.x) { return false; }
		if (pos.y < boundPos1.y || pos.y >= boundPos2.y) { return false; }
		return true;
	}
	
	/// <summary> Checks if a vector is in between 2 bounds </summary>
	public static bool IsVectorInBounds(Vector3 pos, Vector3 boundPos1, Vector3 boundPos2)
	{
		if (pos.x < boundPos1.x || pos.x >= boundPos2.x) { return false; }
		if (pos.y < boundPos1.y || pos.y >= boundPos2.y) { return false; }
		if (pos.z < boundPos1.z || pos.z >= boundPos2.z) { return false; }
		return true;
	}
}

public static class ColorUtil
{
	/// <summary> Converts from the HSL color space to the RGB color space. </summary>
	public static Color HSLToRGB(int hue, int saturation, int lightness){
		// Normalize hue, saturation, and lightness values
		float h = (float)hue / 360f; // Hue is usually defined in the range [0, 360]
		float s = Mathf.Clamp01((float)saturation / 100f); // Saturation is usually defined in the range [0, 100]
		float l = Mathf.Clamp01((float)lightness / 100f); // Lightness is usually defined in the range [0, 100]

		float c = (1 - Mathf.Abs(2 * l - 1)) * s;
		float x = c * (1 - Mathf.Abs((h * 6) % 2 - 1));
		float m = l - c / 2;

		float r, g, b;
		if (h < 1f / 6f)
		{
			r = c;
			g = x;
			b = 0;
		}
		else if (h < 1f / 3f)
		{
			r = x;
			g = c;
			b = 0;
		}
		else if (h < 0.5f)
		{
			r = 0;
			g = c;
			b = x;
		}
		else if (h < 2f / 3f)
		{
			r = 0;
			g = x;
			b = c;
		}
		else if (h < 5f / 6f)
		{
			r = x;
			g = 0;
			b = c;
		}
		else{
			r = c;
			g = 0;
			b = x;
		}

		return new Color(r + m, g + m, b + m);
	}

	/// <summary> Converts from the RGB color space to the HSL color space. </summary>
	public static Vector3 RGBToHSL(Color color)
	{
		float r = color.r;
		float g = color.g;
		float b = color.b;

		float max = Mathf.Max(r, Mathf.Max(g, b));
		float min = Mathf.Min(r, Mathf.Min(g, b));
		float h, s, l;

		// Calculate lightness
		l = (max + min) / 2f;

		if (max == min)
		{
			// Achromatic case (no hue)
			h = 0f;
			s = 0f;
		}
		else
		{
			float d = max - min;

			// Calculate saturation
			s = l > 0.5f ? d / (2f - max - min) : d / (max + min);

			// Calculate hue
			if (max == r)
				h = (g - b) / d + (g < b ? 6f : 0f);
			else if (max == g)
				h = (b - r) / d + 2f;
			else
				h = (r - g) / d + 4f;

			h /= 6f;
		}

		return new Vector3(h * 360f, s * 100f, l * 100f);
	}
}

}