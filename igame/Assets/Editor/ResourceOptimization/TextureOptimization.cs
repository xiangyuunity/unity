using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Reflection;

public class TextureOptimization
{
	static float mTextureStandaloneScale 	= 1.0f;
	static float mTextureWebScale			= 1.0f;
	static float mTextureiPhoneScale		= 1.0f;
	static float mTextureAndroidScale		= 1.0f;
	
	public static bool EnableTextureOptimization()
	{
		Object objSel = Selection.activeObject;
		if (null == objSel)
		{
			return false;
		}
		
		return true;
	}

	public static void OptimizationTexture(Texture objTexture)
	{
		string strAssetPath = AssetDatabase.GetAssetPath(objTexture);
		
		if (string.IsNullOrEmpty(strAssetPath))
		{
			Debug.LogError("Asset[" + objTexture.name + "] 's AssetPath Is Null Or Empty.");
			return;
		}

		object[] args = new object[2] { 0, 0 };

		TextureImporter ti = AssetImporter.GetAtPath(strAssetPath) as TextureImporter;
		if (ti == null) 
		{
			Debug.LogError("Could Not Get Asset[" + objTexture.name + "] 's TextureImporter.");
			return;
		}
		
		bool bReImportAsset = false;
		
		if (ti.textureType == TextureImporterType.Default || ti.textureType == TextureImporterType.Default)
		{
			Texture tex = objTexture as Texture;
			if (null == tex)
			{
				Debug.LogError("Asset[" + objTexture.name + "] Converted Error.");
				return;
			}
			
			if (ti.textureType != TextureImporterType.Default)
			{
				bReImportAsset			= true;
				ti.textureType 			= TextureImporterType.Default;
			}

			if (ti.isReadable)
			{
				ti.isReadable 			= false;
				bReImportAsset			= true;
			}

			if (ti.mipmapEnabled)
			{
				ti.mipmapEnabled		= false;
				bReImportAsset			= true;
			}
			
			if (ti.filterMode != FilterMode.Bilinear)
			{
				ti.filterMode			= FilterMode.Bilinear;
				bReImportAsset			= true;
			}
			
			if (ti.anisoLevel > 0)
			{
				ti.anisoLevel			= 0;
				bReImportAsset			= true;
			}
			
			MethodInfo mi = typeof(TextureImporter).GetMethod("GetWidthAndHeight", BindingFlags.NonPublic | BindingFlags.Instance);
			mi.Invoke(ti, args);
			
			int iSrcWidth 	= (int)args[0];
			int iSrcHeight 	= (int)args[1];
			
			int iMaxSrcSize = Mathf.Max(Mathf.NextPowerOfTwo((int)(Mathf.Max(iSrcWidth, iSrcHeight))), 32);
			if (ti.maxTextureSize != iMaxSrcSize)
			{
				ti.maxTextureSize 		= iMaxSrcSize;
				bReImportAsset			= true;
			}
			
			if (ti.DoesSourceTextureHaveAlpha())
			{
				if (ti.textureFormat != TextureImporterFormat.DXT5)
				{
					ti.textureFormat 	= TextureImporterFormat.DXT5;
					bReImportAsset		= true;
				}
			}
			else
			{
				if (ti.textureFormat != TextureImporterFormat.DXT1)
				{
					ti.textureFormat	= TextureImporterFormat.DXT1;
					bReImportAsset		= true;
				}
			}
			
			//Override 
			int 					maxTextureSize 	= 0;
			TextureImporterFormat	textureFormat	= TextureImporterFormat.AutomaticCompressed;
			//Web
			if (ti.GetPlatformTextureSettings("Web", out maxTextureSize, out textureFormat))
			{
				int iTargetTextureSize 	= Mathf.Max(32, Mathf.NextPowerOfTwo((int)((float)ti.maxTextureSize * mTextureWebScale)));
				if (maxTextureSize != iTargetTextureSize || textureFormat != ti.textureFormat)
				{
					ti.SetPlatformTextureSettings("Web", iTargetTextureSize, ti.textureFormat);
					bReImportAsset		= true;
				}
			}
			else
			{
				int iTargetTextureSize 	= Mathf.Max(32, Mathf.NextPowerOfTwo((int)((float)ti.maxTextureSize * mTextureWebScale)));
				ti.SetPlatformTextureSettings("Web", iTargetTextureSize, ti.textureFormat);
				bReImportAsset			= true;
			}
			
			//Standalone	
			if (ti.GetPlatformTextureSettings("Standalone", out maxTextureSize, out textureFormat))
			{
				int iTargetTextureSize 	= Mathf.Max(32, Mathf.NextPowerOfTwo((int)((float)ti.maxTextureSize * mTextureStandaloneScale)));
				if (maxTextureSize != iTargetTextureSize || textureFormat != ti.textureFormat)
				{
					ti.SetPlatformTextureSettings("Standalone", iTargetTextureSize, ti.textureFormat);
					bReImportAsset		= true;
				}
			}
			else
			{
				int iTargetTextureSize 	= Mathf.Max(32, Mathf.NextPowerOfTwo((int)((float)ti.maxTextureSize * mTextureStandaloneScale)));
				ti.SetPlatformTextureSettings("Standalone", iTargetTextureSize, ti.textureFormat);
				bReImportAsset			= true;
			}
			
			//iPhone
			if (ti.GetPlatformTextureSettings("iPhone", out maxTextureSize, out textureFormat))
			{
				int 					iTargetTextureSize 	= Mathf.Max(32, Mathf.NextPowerOfTwo((int)((float)ti.maxTextureSize * mTextureiPhoneScale)));
				TextureImporterFormat 	fTargetFormat		= TextureImporterFormat.PVRTC_RGBA4;
				
				if (ti.DoesSourceTextureHaveAlpha())
				{
					fTargetFormat = TextureImporterFormat.PVRTC_RGBA4;
				}
				else
				{
					fTargetFormat = TextureImporterFormat.PVRTC_RGB4;
				}
				
				if (maxTextureSize != iTargetTextureSize || textureFormat != fTargetFormat)
				{
					ti.SetPlatformTextureSettings("iPhone", iTargetTextureSize, fTargetFormat);
					bReImportAsset		= true;
				}
			}
			else
			{
				int 					iTargetTextureSize 	= Mathf.Max(32, Mathf.NextPowerOfTwo((int)((float)ti.maxTextureSize * mTextureiPhoneScale)));
				TextureImporterFormat 	fTargetFormat		= TextureImporterFormat.PVRTC_RGBA4;
				
				if (ti.DoesSourceTextureHaveAlpha())
				{
					fTargetFormat = TextureImporterFormat.PVRTC_RGBA4;
				}
				else
				{
					fTargetFormat = TextureImporterFormat.PVRTC_RGB4;
				}
				
				ti.SetPlatformTextureSettings("iPhone", iTargetTextureSize, fTargetFormat);
				bReImportAsset			= true;
			}
			
			//Android
			if (ti.GetPlatformTextureSettings("Android", out maxTextureSize, out textureFormat))
			{
				int 					iTargetTextureSize 	= Mathf.Max(32, Mathf.NextPowerOfTwo((int)((float)ti.maxTextureSize * mTextureAndroidScale)));
				TextureImporterFormat 	fTargetFormat		= TextureImporterFormat.PVRTC_RGBA4;
				
				if (ti.DoesSourceTextureHaveAlpha())
				{
					fTargetFormat = TextureImporterFormat.ATC_RGBA8;
				}
				else
				{
					fTargetFormat = TextureImporterFormat.ATC_RGB4;
				}
				
				if (maxTextureSize != iTargetTextureSize || textureFormat != fTargetFormat)
				{
					ti.SetPlatformTextureSettings("Android", iTargetTextureSize, fTargetFormat);
					bReImportAsset		= true;
				}
			}
			else
			{
				int 					iTargetTextureSize 	= Mathf.Max(32, Mathf.NextPowerOfTwo((int)((float)ti.maxTextureSize * mTextureAndroidScale)));
				TextureImporterFormat 	fTargetFormat		= TextureImporterFormat.PVRTC_RGBA4;
				
				if (ti.DoesSourceTextureHaveAlpha())
				{
					fTargetFormat = TextureImporterFormat.ATC_RGBA8;
				}
				else
				{
					fTargetFormat = TextureImporterFormat.ATC_RGB4;
				}
				
				ti.SetPlatformTextureSettings("Android", iTargetTextureSize, fTargetFormat);
				bReImportAsset			= true;
			}
			
			//FlashPlayer
			if (ti.GetPlatformTextureSettings("FlashPlayer", out maxTextureSize, out textureFormat))
			{
				ti.ClearPlatformTextureSettings("FlashPlayer");
			}
		}
		else if (ti.textureType == TextureImporterType.NormalMap || ti.textureType == TextureImporterType.Lightmap)
		{
			Texture tex = objTexture as Texture;
			if (null == tex)
			{
				Debug.LogError("Asset[" + objTexture.name + "] Converted Error.");
				return;
			}
			
			if (ti.filterMode != FilterMode.Bilinear)
			{
				ti.filterMode			= FilterMode.Bilinear;
				bReImportAsset			= true;
			}
			
			if (ti.anisoLevel > 0)
			{
				ti.anisoLevel			= 0;
				bReImportAsset			= true;
			}
			
			MethodInfo mi = typeof(TextureImporter).GetMethod("GetWidthAndHeight", BindingFlags.NonPublic | BindingFlags.Instance);
			mi.Invoke(ti, args);
			
			int iSrcWidth 	= (int)args[0];
			int iSrcHeight 	= (int)args[1];
			
			int iMaxSrcSize = Mathf.Max(Mathf.NextPowerOfTwo((int)(Mathf.Max(iSrcWidth, iSrcHeight))), 32);
			if (ti.maxTextureSize != iMaxSrcSize)
			{
				ti.maxTextureSize 		= iMaxSrcSize;
				bReImportAsset			= true;
			}
			
			if (ti.textureFormat != TextureImporterFormat.AutomaticCompressed)
			{
				ti.textureFormat 	= TextureImporterFormat.AutomaticCompressed;
				bReImportAsset		= true;
			}
			
			//Override 
			int 					maxTextureSize 	= 0;
			TextureImporterFormat	textureFormat	= TextureImporterFormat.AutomaticCompressed;
			//Web
			if (ti.GetPlatformTextureSettings("Web", out maxTextureSize, out textureFormat))
			{
				int iTargetTextureSize 	= Mathf.Max(32, Mathf.NextPowerOfTwo((int)((float)ti.maxTextureSize * mTextureWebScale)));
				if (maxTextureSize != iTargetTextureSize || textureFormat != TextureImporterFormat.AutomaticCompressed)
				{
					ti.SetPlatformTextureSettings("Web", iTargetTextureSize, TextureImporterFormat.AutomaticCompressed);
					bReImportAsset		= true;
				}
			}
			else
			{
				int iTargetTextureSize 	= Mathf.Max(32, Mathf.NextPowerOfTwo((int)((float)ti.maxTextureSize * mTextureWebScale)));
				ti.SetPlatformTextureSettings("Web", iTargetTextureSize, TextureImporterFormat.AutomaticCompressed);
				bReImportAsset			= true;
			}
			
			//Standalone	
			if (ti.GetPlatformTextureSettings("Standalone", out maxTextureSize, out textureFormat))
			{
				int iTargetTextureSize 	= Mathf.Max(32, Mathf.NextPowerOfTwo((int)((float)ti.maxTextureSize * mTextureStandaloneScale)));
				if (maxTextureSize != iTargetTextureSize || textureFormat != TextureImporterFormat.AutomaticCompressed)
				{
					ti.SetPlatformTextureSettings("Standalone", iTargetTextureSize, TextureImporterFormat.AutomaticCompressed);
					bReImportAsset		= true;
				}
			}
			else
			{
				int iTargetTextureSize 	= Mathf.Max(32, Mathf.NextPowerOfTwo((int)((float)ti.maxTextureSize * mTextureStandaloneScale)));
				ti.SetPlatformTextureSettings("Standalone", iTargetTextureSize, TextureImporterFormat.AutomaticCompressed);
				bReImportAsset			= true;
			}
			
			//iPhone
			if (ti.GetPlatformTextureSettings("iPhone", out maxTextureSize, out textureFormat))
			{
				int iTargetTextureSize 	= Mathf.Max(32, Mathf.NextPowerOfTwo((int)((float)ti.maxTextureSize * mTextureiPhoneScale)));
				
				if (maxTextureSize != iTargetTextureSize || textureFormat != TextureImporterFormat.AutomaticCompressed)
				{
					ti.SetPlatformTextureSettings("iPhone", iTargetTextureSize, TextureImporterFormat.AutomaticCompressed);
					bReImportAsset		= true;
				}
			}
			else
			{
				int iTargetTextureSize 	= Mathf.Max(32, Mathf.NextPowerOfTwo((int)((float)ti.maxTextureSize * mTextureiPhoneScale)));
				
				ti.SetPlatformTextureSettings("iPhone", iTargetTextureSize, TextureImporterFormat.AutomaticCompressed);
				bReImportAsset			= true;
			}
			
			//Android
			if (ti.GetPlatformTextureSettings("Android", out maxTextureSize, out textureFormat))
			{
				int iTargetTextureSize 	= Mathf.Max(32, Mathf.NextPowerOfTwo((int)((float)ti.maxTextureSize * mTextureAndroidScale)));
				
				if (maxTextureSize != iTargetTextureSize || textureFormat != TextureImporterFormat.AutomaticCompressed)
				{
					ti.SetPlatformTextureSettings("Android", iTargetTextureSize, TextureImporterFormat.AutomaticCompressed);
					bReImportAsset		= true;
				}
			}
			else
			{
				int iTargetTextureSize 	= Mathf.Max(32, Mathf.NextPowerOfTwo((int)((float)ti.maxTextureSize * mTextureAndroidScale)));
				
				ti.SetPlatformTextureSettings("Android", iTargetTextureSize, TextureImporterFormat.AutomaticCompressed);
				bReImportAsset			= true;
			}
			
			//FlashPlayer
			if (ti.GetPlatformTextureSettings("FlashPlayer", out maxTextureSize, out textureFormat))
			{
				ti.ClearPlatformTextureSettings("FlashPlayer");
			}
		}
		else
		{
			return;
		}
		
		
		if (bReImportAsset)
		{
			AssetDatabase.ImportAsset(strAssetPath, ImportAssetOptions.ForceUpdate);
			Debug.Log("ReImport Textrue Asset[" + strAssetPath + "].");
		}
	}

	public static void OptimizationTexture()
	{
		Object[] objSelArray = Selection.GetFiltered(typeof(Texture), SelectionMode.DeepAssets );
		if (0 == objSelArray.Length)
		{
			EditorUtility.DisplayDialog("OptimizationTexture", 
				"No Texture Assets Were Selected.", 
				"Ok");
			
			return;
		}

		foreach (Object objTexture in objSelArray)
		{
			if (objTexture.GetType() != typeof(Texture) && objTexture.GetType() != typeof(Texture2D))
			{
				continue;
			}

			Texture texture = objTexture as Texture;
			OptimizationTexture(texture);
		}
		
		AssetDatabase.Refresh();
	}
}
