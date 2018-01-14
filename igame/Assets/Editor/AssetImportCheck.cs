using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;
using ICSharpCode.SharpZipLib.GZip;


/// <summary>
/// Bart
/// 资源导入检查
/// </summary>
public class AssetImportCheck : AssetPostprocessor
{
    //导入纹理
    void OnPostprocessTexture(Texture2D texture)
    {
        TextureImporter textureImporter = this.assetImporter as TextureImporter;
        if (textureImporter.assetPath.IndexOf("Resources\\UI") > -1) //UI使用mipmap
        {
            textureImporter.mipmapEnabled = false;
            textureImporter.anisoLevel = 0; //各向异性0
        }
        if (textureImporter.maxTextureSize > 2048)
        {
            textureImporter.maxTextureSize = 2048; //纹理最大限制
        }
    }
        

    void OnPostprocessModel(GameObject model)
    {
        bool dirty = false;
        ModelImporter importer = this.assetImporter as ModelImporter;
        importer.isReadable = false;

        if (this.assetPath.IndexOf("effect/mesh") > -1)//特效模型
        {
            if (importer.importMaterials)
            {
                importer.importMaterials = false;
                FileUtil.DeleteFileOrDirectory(Application.dataPath + "/ArtRes/effect/mesh/Materials");//删除文件
                AssetDatabase.SaveAssets();
            }
            return;
        }

        if (assetPath.Contains ("@")) 
        {
            if(importer.animationPositionError >= 0.5f)
                importer.animationPositionError = 0.2f;
            if(importer.animationRotationError >= 0.5f)
                importer.animationRotationError = 0.2f;
        }

        if (!assetPath.Contains("@"))
        {
            if (this.assetPath.IndexOf ("skinned_mesh") > -1 && importer.importAnimation == true)
            {
                importer.importAnimation = false;
            } 
            else
            {
                if(importer.animationPositionError >= 0.5f)
                    importer.animationPositionError = 0.2f;
                if(importer.animationRotationError >= 0.5f)
                    importer.animationRotationError = 0.2f;
            }
            Renderer[] renderers = model.transform.GetComponentsInChildren<Renderer>();
            for (int i = 0; i < renderers.Length; i++)
            {
                Renderer renderer = renderers[i];

                if (this.assetPath.IndexOf("skinned_mesh/role") > -1 || this.assetPath.IndexOf("mesh/weapon") > -1)
                    renderer.sharedMaterial.shader = Shader.Find("Toon/Basic Outline");
                //else
                //renderer.sharedMaterial.shader = Shader.Find("Standard");
                /*
                for (var ii = 0; ii < renderer.sharedMaterials.Length; ++ii)
                {
                    string matName = renderer.name;
                    Material mat = renderer.sharedMaterials[ii];
                    if (renderer.sharedMaterials.Length > 1)//多維材質
                    {
                        matName = renderer.name + "_" + (ii + 1);
                    }

                    if (this.assetPath.IndexOf("skinned_mesh") > -1 && mat.name != matName)
                    {
                        Debug.LogError(this.assetPath + " 材质名" + mat.name + "模型名" + matName + "不匹配！");
                        EditorUtility.DisplayDialog("错误提示", this.assetPath + " 材质名" + mat.name + "模型名" + matName + "不匹配！请修改fbx文件重新导出", "关闭");
                        //FileUtil.DeleteFileOrDirectory(Application.dataPath+assetPath.Replace("Assets",""));//删除文件
                        //AssetDatabase.Refresh();
                    }
                }
                */
            }
        }
        AssetDatabase.SaveAssets();
        if (dirty == true)
            importer.SaveAndReimport();
    }
}