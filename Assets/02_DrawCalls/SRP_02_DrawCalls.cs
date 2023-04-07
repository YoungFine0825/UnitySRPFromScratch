using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace UnitySRPFromScratch._02_DrawCalls
{
    public class SRP_02_DrawCalls : RenderPipeline
    {
        SRPRenderer_02_DrawCalls m_renderer = new SRPRenderer_02_DrawCalls();

        public SRP_02_DrawCalls()
        {
            GraphicsSettings.useScriptableRenderPipelineBatching = true;//开启SRP Batcher
        }
        protected override void Render(ScriptableRenderContext context, Camera[] cameras)
        {
            foreach(var camera in cameras) 
            {
                m_renderer.DoRender(context, camera, this);
            }
        }

    }

}
