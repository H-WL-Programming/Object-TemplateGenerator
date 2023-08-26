using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TemplateData
{
    public List<Template> templates;
}

[Serializable]
public class Template
{
    public string name;
    public Vector3 position;
    public Vector3 rotation;
    public Vector3 scale;
    public Color color;

    public GameObject prefab;
}
