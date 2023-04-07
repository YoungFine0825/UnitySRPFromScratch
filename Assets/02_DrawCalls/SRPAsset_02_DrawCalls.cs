using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace UnitySRPFromScratch._02_DrawCalls 
{
    [CreateAssetMenu(menuName = "SRP Assets/02_DrawCalls")]
    public class SRPAsset_02_DrawCalls : RenderPipelineAsset
    {
        protected override RenderPipeline CreatePipeline()
        {
            return new SRP_02_DrawCalls();
        }
    }
}

