using System;
using UnityEngine;
using UnityEngine.Rendering;

namespace UnitySRPFromScratch._01_Basic
{
    class SRPRenderer_01_Basic : SRPRenderer
    {

        ShaderTagId m_mainPass = new ShaderTagId("SRP_Pass_Main");
        protected override SRP_ERROR_CODE ValidateRendering()
        {
            return TryCameraCulling();
        }


        protected override void OnRender()
        {
            base.OnRender();
            //
            m_context.SetupCameraProperties(m_camera);
            //
            bool drawSkyBox = m_camera.clearFlags == CameraClearFlags.Skybox ? true : false;
            bool clearDepth = m_camera.clearFlags == CameraClearFlags.Nothing ? false : true;
            bool clearColor = m_camera.clearFlags == CameraClearFlags.Color ? true : false;
            var clearCmd = CommandBufferPool.Get("Clear");
            clearCmd.ClearRenderTarget(clearDepth, clearColor, m_camera.backgroundColor);
            CommandBufferPool.ExecuteAndRelease(m_context,clearCmd);
            if(drawSkyBox) m_context.DrawSkybox(m_camera);
            //Draw Opaques
            SortingSettings sortSetting = new SortingSettings(m_camera);
            sortSetting.criteria = SortingCriteria.CommonOpaque;
            DrawingSettings drawSetting = new DrawingSettings(
                m_mainPass,
                sortSetting
                );
            FilteringSettings filteringSetting = new FilteringSettings(RenderQueueRange.opaque);
            m_context.DrawRenderers(m_cameraCullingResults, ref drawSetting, ref filteringSetting);
            //Draw Transparents
            sortSetting.criteria = SortingCriteria.CommonTransparent;
            drawSetting.sortingSettings = sortSetting;
            filteringSetting.renderQueueRange = RenderQueueRange.transparent;
            m_context.DrawRenderers(m_cameraCullingResults, ref drawSetting, ref filteringSetting);
            //
            m_context.Submit();
        }
    }
}
