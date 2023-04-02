using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace UnitySRPFromScratch._01_Basic
{
    [CreateAssetMenu(menuName = "SRP Assets/01_Basic")]
    public class SRPAsset_01_Basic : RenderPipelineAsset
    {
        protected override RenderPipeline CreatePipeline()
        {
            return new SRP_01_Basic();
        }
    }
}

