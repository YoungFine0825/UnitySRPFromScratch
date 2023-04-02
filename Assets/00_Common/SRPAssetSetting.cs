using System;
using UnityEngine;
using UnityEngine.Rendering;

namespace UnitySRPFromScratch
{
    [ExecuteInEditMode]
    class SRPAssetSetting : MonoBehaviour
    {
        public RenderPipelineAsset renderPipelineAsset;
        void OnEnable()
        {
            GraphicsSettings.renderPipelineAsset = renderPipelineAsset;
        }

        void OnValidate()
        {
            GraphicsSettings.renderPipelineAsset = renderPipelineAsset;
        }
    }
}
