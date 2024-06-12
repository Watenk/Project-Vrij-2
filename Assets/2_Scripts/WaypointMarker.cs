using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WaypointMarker : MonoBehaviour
{
    public Image Image;
    private Transform target;
    public TMP_Text meter;
    public Vector3 offset;

    private GameObject boat;

    // Start is called before the first frame update
    void Start()
    {
        boat = GameObject.Find("boat(Clone)");
        target = boat.transform;
    }

    // Update is called once per frame
    void Update()
    {
        float minX = Image.GetPixelAdjustedRect().width / 2;
        float maxX = Screen.width - minX;
        
        float minY = Image.GetPixelAdjustedRect().height / 2;
        float maxY = Screen.height - minY;

        Vector2 pos = Camera.main.WorldToScreenPoint(target.position + offset);

        if(Vector3.Dot((target.position - transform.position), transform.forward) < 0)
        {
            if (pos.x < Screen.width / 2)
            {
                pos.x = maxX;
            }
            else
            {
                pos.x = minX;
            }
        }

        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        Image.transform.position = pos;
        meter.text = "Jump over the boat";
    }
}
