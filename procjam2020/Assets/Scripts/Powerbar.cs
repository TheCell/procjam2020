using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerbar : MonoBehaviour
{
    public float currentPowerNormalized = 1f;

    [SerializeField]
    private Shapes.Rectangle powerBar;
    private Shapes.ShapeGroup shapegroup;
    private float maxSize;
    

    public void Start()
    {
        maxSize = powerBar.Height;
        shapegroup = GetComponent<Shapes.ShapeGroup>();
        if (powerBar == null)
        {
            Debug.LogError("missing powerbar reference");
        }
        if (shapegroup == null)
        {
            Debug.LogError("This Objects expects to hold a ShapeGroup");
        }
    }

    public void Update()
    {
        powerBar.Height = maxSize * currentPowerNormalized;
    }

    public void setVisible(bool visible)
    {
        Color shapegroupColor = shapegroup.Color;
        shapegroupColor.a = visible ? 1f : 0f;
        shapegroup.Color = shapegroupColor;
    }
}
