using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

public static class BuildScript
{
	public static void BuildAndroidDemo()
	{
		string outputDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Builds", "Android");
		string outputPath = Path.Combine(outputDirectory, "COM-Dino-Hunter-demo.apk");
		string[] scenes = EditorBuildSettings.scenes.Where(scene => scene.enabled).Select(scene => scene.path).ToArray();
		if (scenes.Length == 0)
		{
			Debug.LogError("No enabled scenes found in EditorBuildSettings.");
			EditorApplication.Exit(1);
			return;
		}
		if (!Directory.Exists(outputDirectory))
		{
			Directory.CreateDirectory(outputDirectory);
		}
		string error = BuildPipeline.BuildPlayer(scenes, outputPath, BuildTarget.Android, BuildOptions.None);
		if (!string.IsNullOrEmpty(error))
		{
			Debug.LogError("Android build failed: " + error);
			EditorApplication.Exit(1);
			return;
		}
		Debug.Log("Android build completed: " + outputPath);
		EditorApplication.Exit(0);
	}
}
