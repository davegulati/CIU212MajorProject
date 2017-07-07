using UnityEngine;
using UnityEditor;

public class DyeHard : EditorWindow {

    public float timeScale = 1.0f;
	public float healthToGive = 0;

    [MenuItem("Window/Dye Hard Settings")]
    public static void ShowWindow()
    {
        GetWindow<DyeHard>("Dye Hard Settings");
    }

    private void OnGUI()
    {
        GUILayout.Label("Fields marked with * are not yet functional.", EditorStyles.boldLabel);
        GUILayout.Label("");
		GUILayout.Label("General Settings", EditorStyles.boldLabel);
        GUILayout.Label("Set timescale:", EditorStyles.label);
		timeScale = EditorGUILayout.Slider(timeScale, 0.001f, 1.0f);
		if (GUILayout.Button("Update Game Settings"))
		{
            Time.timeScale = timeScale;
		}

		GUILayout.Label("");

		GUILayout.Label("Sen's Settings", EditorStyles.boldLabel);
        healthToGive = EditorGUILayout.FloatField("Set Sen's health to:", healthToGive);

        if (GUILayout.Button("Update Sen's Settings"))
        {
            GameObject.Find("Sen").GetComponent<PlayerHealth>().PlayerSetHealth(healthToGive);
        }

		GUILayout.Label("");

		GUILayout.Label("Bud's Settings", EditorStyles.boldLabel);
		if (GUILayout.Button("Kill Bud"))
		{
            GameObject bud = GameObject.Find("Bud");
            Destroy(bud);
		}

		GUILayout.Label("");

		GUILayout.Label("***Enemy Settings***", EditorStyles.boldLabel);
		if (GUILayout.Button("Kill All Enemies"))
		{
			GameObject.Find("Sen").GetComponent<PlayerHealth>().PlayerSetHealth(healthToGive);
		}
    }
}
