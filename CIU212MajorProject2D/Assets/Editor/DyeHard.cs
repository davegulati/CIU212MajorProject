using UnityEngine;
using UnityEditor;

public class DyeHard : EditorWindow {

    public float timeScale = 1.0f;
	public float healthToGive = 0;
    public Object bud;

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
        GUILayout.BeginHorizontal();
			timeScale = EditorGUILayout.Slider(timeScale, 0.001f, 1.0f);
			if (GUILayout.Button("Set"))
			{
	            Time.timeScale = timeScale;
			}
        GUILayout.EndHorizontal();
		GUILayout.Label("");

		GUILayout.Label("Sen's Settings", EditorStyles.boldLabel);
        GUILayout.BeginHorizontal();
        healthToGive = EditorGUILayout.FloatField("Set Sen's current health to:", healthToGive);
	        if (GUILayout.Button("Set"))
	        {
	            GameObject.Find("Sen").GetComponent<PlayerHealth>().PlayerSetHealth(healthToGive);
	        }
        GUILayout.EndHorizontal();
		GUILayout.Label("");

		GUILayout.Label("Bud's Settings", EditorStyles.boldLabel);
        GUILayout.BeginHorizontal();
	        GUILayout.Label("Select Bud prefab:", EditorStyles.label);
	        bud = EditorGUILayout.ObjectField(bud, typeof(Object), true);
		if (GUILayout.Button("Spawn Bud"))
		{
			if (bud != null)
			{
				Instantiate(bud);
			}
		}
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
	        if (GUILayout.Button("Kill Bud (Main)"))
			{
				GameObject bud = GameObject.Find("Bud");
	            if (bud != null)
	            {
	                Destroy(bud);
	            }
			}
	        if (GUILayout.Button("Kill Bud (Clones)"))
			{
				bud = GameObject.Find("Bud(Clone)");
	            if (bud != null)
	            {
	                Destroy(bud);
	            }
			}
        GUILayout.EndHorizontal();
		GUILayout.Label("");

		GUILayout.Label("***Enemy Settings***", EditorStyles.boldLabel);
		if (GUILayout.Button("Kill All Enemies"))
		{
			GameObject.Find("Sen").GetComponent<PlayerHealth>().PlayerSetHealth(healthToGive);
		}
    }
}
