using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnitySRPFromScratch;

namespace UnitySRPFromScratch._01_Basic
{
    public class SRP_01_Basic : RenderPipeline
    {
        SRPRenderer_01_Basic renderer = new SRPRenderer_01_Basic();
        protected override void Render(ScriptableRenderContext context, Camera[] cameras)
        {
            foreach (var cam in cameras)
            {
#if UNITY_EDITOR
                if (cam.cameraType == CameraType.SceneView)
                {
                    ScriptableRenderContext.EmitWorldGeometryForSceneView(cam);
                }
#endif
                //
                renderer.DoRender(context, cam,this);

            }
        }
    }
}

