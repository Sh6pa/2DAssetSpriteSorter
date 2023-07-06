using UnityEngine;
using System.Collections.Generic;
using System;
using System.Runtime.InteropServices.WindowsRuntime;

[System.Serializable]
public struct LayerInfo
{
    [ReadOnly]
    public string Name;
    [ReadOnly]
    public int Index;
    public float Offset;
}

[CreateAssetMenu(menuName = "Layer/LayerData")]
public class LayerData : ScriptableObject
{
    public void UpdateSortingLayersList()
    {
        List<LayerInfo> tmpList = new List<LayerInfo>();
        foreach(SortingLayer layer in SortingLayer.layers)
        {
            if (layer.name.Contains("Default")) // layer.name.Contains("Background") || 
            {
                continue;
            }
            float Offset = 0;
            if (_sortingLayers != null)
            {
                foreach(LayerInfo layerInfo in _sortingLayers)
                {
                    if (layerInfo.Name.Contains(layer.name))
                    {
                        Offset = layerInfo.Offset;
                    }
                    // RECUP L'OFFSET pour l'ajouter à la nouvelle liste pour updater
                }
            }
            LayerInfo layerInfo1 = new LayerInfo();
            layerInfo1.Offset = Offset;
            layerInfo1.Name = layer.name;
            layerInfo1.Index = layer.id;
            tmpList.Add(layerInfo1);
        }
        //if (_InvertZAxis)
        //    tmpList.Reverse();
        tmpList.Reverse();
        _sortingLayers = tmpList.ToArray();
    }

    public void SetBackgroundLayersOnVariables()
    {
        for (int i = 0; i < _sortingLayers.Length; i++)
        {
            if (_sortingLayers[i].Name.Contains("Background"))
            {
                // Debug.Log($"-Abs({BackgroundOffset} + Pow({BackgroundLayerLength} * {(_sortingLayers[i].Name[_sortingLayers[i].Name.Length - 1] - '0')}, {BackgroundLayerLengthMultiplicator})");
                float newOffsetValue = -Mathf.Abs(BackgroundOffset + Mathf.Pow(BackgroundLayerLength * (float)Char.GetNumericValue(_sortingLayers[i].Name[_sortingLayers[i].Name.Length-1]), BackgroundLayerLengthMultiplicator));
                _sortingLayers[i].Offset = newOffsetValue*ZAxisInversion;
            }
        }
    }
    [Header("System Parameters")]
    [SerializeField]
    private Color _foregroundAssetColor;
    public Color ForegroundAssetColor { get { return _foregroundAssetColor; } }
    [Header("Layers")]
    [SerializeField]
    private LayerInfo[] _sortingLayers;
    public LayerInfo[] SortingLayers { get { return _sortingLayers; } }
    [Header("Sorting parameters")]
    [SerializeField]
    private float _spriteSortingOrderPrecision = 100f;
    public float SpriteSortingOrderPrecision { get { return _spriteSortingOrderPrecision; } }
    [SerializeField]
    private string _levelGlobalLightTag = "LevelGlobalLight";
    public string LevelGlobalLightTag { get { return _levelGlobalLightTag; } }
    [Header("Background reset params")]
    [SerializeField]
    private float _backgroundOffset = 3f;
    public float BackgroundOffset { get { return _backgroundOffset; } }
    [SerializeField]
    private float _backgroundLayerLength = 2f;
    public float BackgroundLayerLength { get { return _backgroundLayerLength; } }
    [SerializeField]
    [Range(1f, 2f)]
    private float _backgroundLayerLengthMultiplicator = 1.15f;
    public float BackgroundLayerLengthMultiplicator { get { return _backgroundLayerLengthMultiplicator; } }
    [Header("Debug")]
    [SerializeField]
    private float _layerDebugSize = 1f;
    public float LayerDebugSize { get { return _layerDebugSize; } }
    [SerializeField]
    private GameObject _layerdebugGO;
    public GameObject LayerdebugGO { get { return _layerdebugGO; } }
    
    [Header("Lights")]
    [SerializeField]
    private GameObject _SpotLight;
    public GameObject SpotLight { get { return _SpotLight; } }
    [SerializeField]
    private GameObject _AnimatedSpotLight;
    public GameObject AnimatedSpotLight { get { return _AnimatedSpotLight; } }
    [SerializeField]
    private GameObject _SpriteLight;
    public GameObject SpriteLight { get { return _SpriteLight; } }
    [SerializeField]
    private GameObject _FreeFormLight;
    public GameObject FreeFormLight { get { return _FreeFormLight; } }

    // [Header("Advanced Parameters")]
    [Header("Please Don't touch")]
    [SerializeField]
    private bool _InvertZAxis = false;
    public float ZAxisInversion { get { return (_InvertZAxis) ? 1f : -1f; } }
    [SerializeField]
    private float _LightOffsetInstantiation = 5.0f;
    public float LightOffsetInstantiation { get { return _LightOffsetInstantiation; } }


}


