using UnityEditor;
using UnityEngine;

public class TemplateEditorWindow : EditorWindow
{
    private TemplateData templateData;
    private int selectedTemplateIndex = -1;
    private Template selectedTemplate;
    private GameObject instantiatedObject;

    [MenuItem("Window/Template Editor")]
    public static void ShowWindow()
    {
        GetWindow<TemplateEditorWindow>("Template Editor");
    }

    private void OnEnable()
    {
        templateData = TemplateManager.LoadTemplateData("UITemplates");
    }

    private void OnGUI()
    {
        GUILayout.Label("UI Object Templates", EditorStyles.boldLabel);

        if (GUILayout.Button("Load Templates"))
        {
            templateData = TemplateManager.LoadTemplateData("UITemplates");
        }

        if (GUILayout.Button("Save Templates"))
        {
            TemplateManager.SaveTemplateData("Assets/Resources/UITemplates.json", templateData);
        }

        EditorGUILayout.Space();

        GUILayout.Label("Create New Template", EditorStyles.boldLabel);

        // Input fields for creating a new template
        string newTemplateName = EditorGUILayout.TextField("Name", "");
        Vector3 newTemplatePosition = EditorGUILayout.Vector3Field("Position", Vector3.zero);
        Vector3 newTemplateRotation = EditorGUILayout.Vector3Field("Rotation", Vector3.zero);
        Vector3 newTemplateScale = EditorGUILayout.Vector3Field("Scale", Vector3.one);
        Color newTemplateColor = EditorGUILayout.ColorField("Color", Color.white);
        GameObject newTemplatePrefab = EditorGUILayout.ObjectField("Prefab", null, typeof(GameObject), false) as GameObject;

        if (GUILayout.Button("Create Template"))
        {
            Template newTemplate = new Template
            {
                name = newTemplateName,
                position = newTemplatePosition,
                rotation = newTemplateRotation,
                scale = newTemplateScale,
                color = newTemplateColor,
                prefab = newTemplatePrefab
            };

            templateData.templates.Add(newTemplate);
        }

        EditorGUILayout.Space();

        GUILayout.Label("Select Template", EditorStyles.boldLabel);

        // Dropdown for selecting a template
        selectedTemplateIndex = EditorGUILayout.Popup("Select Template", selectedTemplateIndex, GetTemplateNames());

        // Display selected template properties
        if (selectedTemplateIndex >= 0)
        {
            selectedTemplate = templateData.templates[selectedTemplateIndex];
            GUILayout.Label("Selected Template Properties", EditorStyles.boldLabel);
            EditorGUILayout.LabelField("Name", selectedTemplate.name);
            EditorGUILayout.Vector3Field("Position", selectedTemplate.position);
            EditorGUILayout.Vector3Field("Rotation", selectedTemplate.rotation);
            EditorGUILayout.Vector3Field("Scale", selectedTemplate.scale);
            EditorGUILayout.ColorField("Color", selectedTemplate.color);

            // Button to instantiate the selected template
            if (GUILayout.Button("Instantiate Template"))
            {
                instantiatedObject = InstantiateTemplate(selectedTemplate);
            }
        }

        EditorGUILayout.Space();

        // Display instantiated object properties
        if (instantiatedObject != null)
        {
            GUILayout.Label("Instantiated Object Properties", EditorStyles.boldLabel);
            EditorGUILayout.Vector3Field("Position", instantiatedObject.transform.position);
            EditorGUILayout.Vector3Field("Rotation", instantiatedObject.transform.rotation.eulerAngles);
            EditorGUILayout.Vector3Field("Scale", instantiatedObject.transform.localScale);

            // Customization of instantiated object properties
            EditorGUILayout.Space();
            GUILayout.Label("Customize Instantiated Object", EditorStyles.boldLabel);
            instantiatedObject.transform.position = EditorGUILayout.Vector3Field("Position", instantiatedObject.transform.position);
            instantiatedObject.transform.rotation = Quaternion.Euler(EditorGUILayout.Vector3Field("Rotation", instantiatedObject.transform.rotation.eulerAngles));
            instantiatedObject.transform.localScale = EditorGUILayout.Vector3Field("Scale", instantiatedObject.transform.localScale);
        }
    }

    private string[] GetTemplateNames()
    {
        string[] templateNames = new string[templateData.templates.Count];
        for (int i = 0; i < templateData.templates.Count; i++)
        {
            templateNames[i] = templateData.templates[i].name;
        }
        return templateNames;
    }

   private GameObject InstantiateTemplate(Template template)
    {
     if (template == null)
     {
         Debug.LogWarning("No template selected for instantiation.");
        return null;
     }

     if (template.prefab == null)
     {
        Debug.LogWarning("Template prefab is missing.");
        return null;
     }

     GameObject instantiatedObject = Instantiate(template.prefab);
     instantiatedObject.transform.position = template.position;
     instantiatedObject.transform.rotation = Quaternion.Euler(template.rotation);
     instantiatedObject.transform.localScale = template.scale;
 
     return instantiatedObject;
    }

}
