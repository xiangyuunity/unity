using UnityEngine;
using UnityEditor;
using System.Collections;

public class MeshOptimization
{
	public static bool EnableMeshOptimization()
	{
		Object objSel = Selection.activeObject;
		if (null == objSel)
		{
			return false;
		}
		
		return true;
	}
	
	public static void OptimizationMesh()
	{
		Object[] objSelArray = Selection.GetFiltered(typeof(Object), SelectionMode.Unfiltered | SelectionMode.DeepAssets );
		if (0 == objSelArray.Length)
		{
			EditorUtility.DisplayDialog("OptimizationMesh", 
				"No Mesh Assets Were Selected.", 
				"Ok");
			
			return;
		}
		
		
		foreach (Object objMesh in objSelArray)
		{
			/*
			if (objMesh.GetType() != typeof(MeshFilter))
			{
				continue;
			}
			*/
			
			string strAssetPath = AssetDatabase.GetAssetPath(objMesh);
	
			if (string.IsNullOrEmpty(strAssetPath))
			{
				Debug.LogError("Asset[" + objMesh.name + "] 's AssetPath Is Null Or Empty.");
				continue;
			}
			
			ModelImporter mi = AssetImporter.GetAtPath(strAssetPath) as ModelImporter;
			if (mi == null) 
			{
				//Debug.LogError("Could Not Get Asset[" + objMesh.name + "] 's ModelImporter.");
				continue;
			}
			
			bool bReImportAsset = false;
			
			if (false == mi.optimizeMesh)
			{
				mi.optimizeMesh = true;
				bReImportAsset 	= true;
			}
			
			if (bReImportAsset)
			{
				AssetDatabase.ImportAsset(strAssetPath, ImportAssetOptions.ForceUpdate);
				Debug.Log("ReImport Mesh Asset[" + strAssetPath + "].");
			}
		}
		
		AssetDatabase.Refresh();
	}
}
