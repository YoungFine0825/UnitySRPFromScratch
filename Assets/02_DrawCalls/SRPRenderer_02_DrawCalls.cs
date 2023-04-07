using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace UnitySRPFromScratch._02_DrawCalls
{
    public class SRPRenderer_02_DrawCalls : SRPRenderer
    {
        protected override SRP_ERROR_CODE ValidateRendering()
        {
            return TryCameraCulling();
        }

        protected override void OnRender()
        {
            base.OnRender();
            //
            SetupCamera();
            //
            ClearRenderTarget();
            //Draw Opaques
            SortingSettings sortSetting = new SortingSettings(m_camera);
            sortSetting.criteria = SortingCriteria.CommonOpaque;
            DrawingSettings drawSetting = new DrawingSettings(m_mainPass, sortSetting);
            FilteringSettings filteringSetting = new FilteringSettings(RenderQueueRange.opaque);
            m_context.DrawRenderers(m_cameraCullingResults, ref drawSetting, ref filteringSetting);
            //Draw Transparents
            sortSetting.criteria = SortingCriteria.CommonTransparent;
            drawSetting.sortingSettings = sortSetting;
            filteringSetting.renderQueueRange = RenderQueueRange.transparent;
            m_context.DrawRenderers(m_cameraCullingResults, ref drawSetting, ref filteringSetting);
            //
            DrawUnsupportedShaders();
            //
            DrawGizmos();
            //
            m_context.Submit();
        }
    }
}

