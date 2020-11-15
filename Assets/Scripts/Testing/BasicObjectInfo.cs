using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BasicObjectInfo
{
    public string objName;
    public string objDescription;
    public Sprite objIcon;

    public BasicObjectInfo(string name)
    {
        objName = name;
    }

    public BasicObjectInfo(string name, string desc)
    {
        objName = name;
        objDescription = desc;
    }

    public BasicObjectInfo(string name, string desc, Sprite icon)
    {
        objName = name;
        objDescription = desc;
        objIcon = icon;
    }

    public string GetName
    {
        get { return objName; }
    }

    public string GetDescription
    {
        get { return objDescription; }
    }

    public Sprite GetIcon
    {
        get { return objIcon; }
    }
}
