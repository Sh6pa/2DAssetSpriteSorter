using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.Rendering.Universal;

public class LightTool : MonoBehaviour
{
    [SerializeField]
    private LayerData _layerData;

    [SerializeField]
    private GameObject _levelSection;

    #region LightData
    [SerializeField]
    private int _subdivisions = 0;
    #endregion

    #region Light Settings
    [SerializeField]
    private Color _lightColor = Color.white;

    [SerializeField][Range(0f, 1f)]
    private float _volumeOpacityMultiplier = 0;
    #endregion

    #region Debug
    [SerializeField]
    public bool m_DepthPreview = false;
    
    [SerializeField]
    private int _depthPreviewLayer = 0;
    #endregion

    private SpriteRenderer[] GetAllSpriteofScene()
    {
        return _levelSection.GetComponentsInChildren<SpriteRenderer>();
    }

    #region Layer functions
    public void SortLayers()
    {
        if (m_DepthPreview)
            Debug.Log($"GO:{_levelSection.gameObject}, z:{_levelSection.transform.position.z}");
        SpriteRenderer[] listOfSceneSprites = GetAllSpriteofScene();
        foreach(SpriteRenderer Sprite_ in listOfSceneSprites)
        {
            float Offset_ = Sprite_.transform.position.z - _levelSection.transform.position.z;
            float OrderInLayer_ = 0f;
            if (m_DepthPreview)
                Debug.Log($"GO:{Sprite_.gameObject}, z:{Sprite_.transform.position}, offset:{Offset_}");

            // float previousLayerOffset = 0f;
            // Init
            if (_layerData.SortingLayers.Length >= 1)
            {
                // previousLayerOffset = _layerData.SortingLayers[0].Offset;
                if (Offset_ < _layerData.SortingLayers[0].Offset)
                {
                    Sprite_.sortingLayerName = _layerData.SortingLayers[0].Name;
                    Sprite_.color = _layerData.ForegroundAssetColor;
                    OrderInLayer_ = Mathf.Abs(Offset_ - _layerData.SortingLayers[0].Offset);
                    SetSortingOrder(Sprite_, OrderInLayer_);
                    continue;
                }
            } else
            {
                continue;
            }
            // Cycle
            for (int i = 1; i < _subdivisions; i++)
            {
                if (Offset_ < _layerData.SortingLayers[i].Offset)
                {
                    Sprite_.sortingLayerName = _layerData.SortingLayers[i].Name;
                    OrderInLayer_ = Mathf.Abs(Offset_- _layerData.SortingLayers[i].Offset);
                    SetSortingOrder(Sprite_, OrderInLayer_);
                    if (Sprite_.sortingLayerName.Contains("Background"))
                    {
                        float LastOffset_ = _layerData.SortingLayers[_subdivisions].Offset;
                        float Ratio_ = (LastOffset_ != 0) ? Mathf.Clamp01(Mathf.Abs(Offset_ / LastOffset_)) : 0;
                        Color TempColor_ = Color.Lerp(Color.white, _lightColor, Ratio_);
                        TempColor_.a = 1f;
                        Sprite_.color = TempColor_;
                    } else
                    {
                        Sprite_.color = Color.white;
                    }
                    
                    break;
                }

                // previousLayerOffset = _layerData.SortingLayers[i].Offset;
            }
            if (Offset_ > _layerData.SortingLayers[_subdivisions].Offset)
            {
                Sprite_.sortingLayerName = _layerData.SortingLayers[_subdivisions].Name;
                OrderInLayer_ = Mathf.Abs(Offset_ - _layerData.SortingLayers[_subdivisions].Offset) * _layerData.ZAxisInversion;
                SetSortingOrder(Sprite_, OrderInLayer_);
                Sprite_.color = _lightColor;
            }
            
        }
    }

    // Sets sorting order of Sprite_ on coords
    private void SetSortingOrder(SpriteRenderer SpriteR_, float OrderInLayer_)
    {
        SpriteR_.sortingOrder = Mathf.CeilToInt(OrderInLayer_*_layerData.SpriteSortingOrderPrecision);
    }

    // sets all asset on the edge of every Layer
    public void NormalizeLayers()
    {
        SpriteRenderer[] ListOfSceneSprites_ = GetAllSpriteofScene();
        foreach (SpriteRenderer Sprite_ in ListOfSceneSprites_)
        {
            for (int i = 0; i < _layerData.SortingLayers.Length; i++)
            {
                if (Sprite_.sortingLayerID == _layerData.SortingLayers[i].Index)
                    Sprite_.transform.position = new Vector3(Sprite_.transform.position.x, Sprite_.transform.position.y, _levelSection.transform.position.z + _layerData.SortingLayers[i].Offset);
            }

            // if (Sprite_.sortingLayerID == _layerData.ForegroundLayer.id)
            //{
            //    Sprite_.transform.position = new Vector3(Sprite_.transform.position.x, Sprite_.transform.position.y, _levelSection.transform.position.z + _layerParams.ForegroundOffset);
            //}
            //else if (Sprite_.sortingLayerID == _layerData.Midground0Layer.id)
            //{
            //    Sprite_.transform.position = new Vector3(Sprite_.transform.position.x, Sprite_.transform.position.y, _levelSection.transform.position.z + _layerParams.MidgroundOffset);
            //}
            //else if (Sprite_.sortingLayerID == _layerData.Midground1Layer.id) 
            //{
            //    Sprite_.transform.position = new Vector3(Sprite_.transform.position.x, Sprite_.transform.position.y, _levelSection.transform.position.z + _layerParams.MidgroundOffset/2f);
            //}
            //else if (Sprite_.sortingLayerName == _layerParams.BackgroundLayerName)
            //{
            //    Sprite_.transform.position = new Vector3(Sprite_.transform.position.x, Sprite_.transform.position.y, _levelSection.transform.position.z + _layerParams.BackgroundOffset);
            //}
        }
    }
    #endregion

    #region Light Settings Functions
    public void FadeBackgroundColor()
    {
        Light2D LevelGlobalLight = _levelSection.FindComponentInChildWithTag<Light2D>(_layerData.LevelGlobalLightTag);
        LevelGlobalLight.color = _lightColor;
    }
    public void FadeBackgroundIntensity()
    {
        SortLayers();
        //  Light2D LevelGlobalLight = _levelSection.FindComponentInChildWithTag<Light2D>(_layerData.LevelGlobalLightTag);
        //  LevelGlobalLight.intensity = _volumeOpacityMultiplier;
    }
    public void FadeVolumeOpacity()
    {
        Light2D LevelGlobalLight = _levelSection.FindComponentInChildWithTag<Light2D>(_layerData.LevelGlobalLightTag);
        LevelGlobalLight.intensity = _volumeOpacityMultiplier;
        //var tempcolor = _lightColor;
        //tempcolor.a = _volumeOpacityMultiplier;
        //LevelGlobalLight.color = tempcolor;
    }
    #endregion
    #region LayerDebug
    private SpriteRenderer[] _debugLayerRenderers = null;

    #region Instantiation
    public void CreateDebugLines()
    {
        _debugLayerRenderers = new SpriteRenderer[_layerData.SortingLayers.Length];
        for (int i = 0; i < _subdivisions; i++)
        {
            SetDebugLayerOnIndex(i);
        }
    }

    public void DestroyDebugLines()
    {
        if (_debugLayerRenderers != null)
        {
            foreach(var spriteRendr in _debugLayerRenderers)
            {
                if (spriteRendr != null)
                    DestroyImmediate(spriteRendr.gameObject);
            }
        }
        _debugLayerRenderers = null;
        // If problem appeared
        foreach (Transform child in transform)
        {
            DestroyImmediate(child.gameObject);
        }
    }
    #endregion
    #region Layers Setting
    private Tuple<string, Vector3> GetSortingLayerAndPosOfDebugLayer(int newindex)
    {
        Vector3 spritePos = new Vector3(_levelSection.transform.position.x, _levelSection.transform.position.y, _levelSection.transform.position.z);
        spritePos += _levelSection.transform.forward * _layerData.SortingLayers[newindex].Offset;
        return new Tuple<string, Vector3>(_layerData.SortingLayers[newindex].Name, spritePos);
    }

    private void SetDebugLayerOnIndex(int newindex)
    {
        string sortingLayerName;
        Vector3 spritePos;
        (sortingLayerName, spritePos) = GetSortingLayerAndPosOfDebugLayer(newindex);
        var clone = Instantiate(_layerData.LayerdebugGO, spritePos, Quaternion.identity);
        _debugLayerRenderers[newindex] = clone.GetComponent<SpriteRenderer>();
        _debugLayerRenderers[newindex].sortingLayerName = sortingLayerName;
        _debugLayerRenderers[newindex].gameObject.name = sortingLayerName;
        _debugLayerRenderers[newindex].transform.parent = transform;
    }
    #endregion
    #region Placement
    private void PlaceDebugLines()
    {
        if (_debugLayerRenderers == null)
            return;
        _subdivisions = Mathf.Min(_layerData.SortingLayers.Length-1, _subdivisions) ;
       
        for (int newindex = 0; newindex < _debugLayerRenderers.Length; newindex++)
        {
            if (_debugLayerRenderers[newindex] == null)
                continue;
            var data = GetSortingLayerAndPosOfDebugLayer(newindex);
            _debugLayerRenderers[newindex].transform.position = data.Item2;
            _debugLayerRenderers[newindex].sortingLayerName = data.Item1;
            _debugLayerRenderers[newindex].gameObject.name = data.Item1;
        }
    }

    public void ShowDebugLines()
    {
        if (_debugLayerRenderers == null)
            return;
        for (int index = 0; index < _debugLayerRenderers.Length; index++)
        {
            if (_debugLayerRenderers[index] == null)
                continue;
            
            if (index >= _depthPreviewLayer)
            {
                _debugLayerRenderers[index].enabled = true;
            } else
            {
                _debugLayerRenderers[index].enabled = false;
            }
        }
    }
    #endregion
    private void OnDrawGizmos()
    {
        if (!m_DepthPreview || _debugLayerRenderers == null)
            return;
        PlaceDebugLines();

    }
    #endregion

    #if UNITY_EDITOR
    public void EditorCreateLight(GameObject go)
    {
        if (UnityEditor.SceneView.lastActiveSceneView.camera == null)
            return;
        Vector3 GOPos = UnityEditor.SceneView.lastActiveSceneView.camera.transform.position + (UnityEditor.SceneView.lastActiveSceneView.camera.transform.forward*_layerData.LightOffsetInstantiation);
        Instantiate(go, GOPos, Quaternion.identity);
    }
    #endif
}
