using System.IO;
using UnityEngine;

public class TemplateManager : MonoBehaviour
{
    public static TemplateData LoadTemplateData(string filePath)
    {
        TextAsset json = Resources.Load<TextAsset>(filePath);
        return JsonUtility.FromJson<TemplateData>(json.text);
    }

    public static void SaveTemplateData(string filePath, TemplateData data)
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(filePath, json);
    }
}


// using System;
// using System.IO;
// using UnityEngine;

// public static class TemplateManager
// {
//     public static TemplateData LoadTemplateData(string filePath)
//     {
//         try
//         {
//             TextAsset json = Resources.Load<TextAsset>(filePath);
//             return JsonUtility.FromJson<TemplateData>(json.text);
//         }
//         catch (Exception e)
//         {
//             Debug.LogError("Error loading templates: " + e.Message);
//             return new TemplateData(); // Return an empty template data in case of error
//         }
//     }

//     public static void SaveTemplateData(string filePath, TemplateData data)
//     {
//         try
//         {
//             string json = JsonUtility.ToJson(data, true);
//             File.WriteAllText(filePath, json);
//         }
//         catch (Exception e)
//         {
//             Debug.LogError("Error saving templates: " + e.Message);
//         }
//     }
// }
