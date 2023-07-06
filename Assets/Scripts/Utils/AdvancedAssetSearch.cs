using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting.FullSerializer;
using UnityEditor;
using UnityEngine;
/// <summary>
/// AdvancedAssetSearch
/// </summary>
public static class AAS
{
    /// <summary>
    /// Give you the item you want throught a search in assets
    /// </summary>
    /// <typeparam name="T">needs to derive from UnityEngine.Object</typeparam>
    /// <param name="Name_">The name of the object you are searching for</param>
    /// <param name="Path_">The path you want the search to operate</param>
    /// <returns></returns>
    public static T SearchItemInAssetdatabase<T>(string Name_, string Path_) where T : UnityEngine.Object
    {
        string[] guids2 = AssetDatabase.FindAssets(Name_, new[] { Path_ });
        string GUIDTOPATH = "";
        for (int i = 0; i < guids2.Length; i++)
        {
            GUIDTOPATH = AssetDatabase.GUIDToAssetPath(guids2[i]);
            if (TruncateAssetPathToName(GUIDTOPATH) == Name_)
            {
                break;
            }
        }
        Debug.Log(GUIDTOPATH);
        T ObjectFounded = AssetDatabase.LoadAssetAtPath<T>(GUIDTOPATH);
        return ObjectFounded;
    }

    /// <summary>
    /// Return True 
    /// </summary>
    /// <param name="Name_">Name of the Asset !Without extension!</param>
    /// <param name="Path_">The path where the asset is located (no precise path needed)</param>
    /// <returns></returns>
    public static bool AssetWithSameNameExists(string Name_, string Path_)
    {
        string[] guids2 = AssetDatabase.FindAssets(Name_, new[] { Path_ });
        string GUIDTOPATH = "";
        for (int i = 0; i < guids2.Length; i++)
        {
            GUIDTOPATH = AssetDatabase.GUIDToAssetPath(guids2[i]);
            if (TruncateAssetPathToName(GUIDTOPATH) == Name_)
            {
                return true;
            }
        }
        return false;
    }

    public static string TruncateAssetPathToName(string FullAssetPathName_)
    {
        return FullAssetPathName_.Split("/").Last<string>().Split(".")[0];
    }
}
