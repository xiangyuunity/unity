using UnityEngine;
using UnityEditor;
using System.Collections;

public class ResourceOptimizationMenu
{
	[MenuItem ("Tools/Resource Optimization/Optimization Mesh", true)]
	static bool EnableMeshOptimization()
	{
		return MeshOptimization.EnableMeshOptimization();
	}
		
	[MenuItem ("Tools/Resource Optimization/Optimization Mesh")]
	static void OptimizationMesh()
	{
		MeshOptimization.OptimizationMesh();
	}
	
	[MenuItem ("Tools/Resource Optimization/Optimization Texture", true)]
	static bool EnableTextureOptimization()
	{
		return TextureOptimization.EnableTextureOptimization();
	}
		
	[MenuItem ("Tools/Resource Optimization/Optimization Texture")]
	static void OptimizationTexture()
	{
		TextureOptimization.OptimizationTexture();
	}
}
