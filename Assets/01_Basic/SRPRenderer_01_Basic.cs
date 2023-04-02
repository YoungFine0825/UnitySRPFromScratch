using System;
using UnityEngine;
using UnityEngine.Rendering;

namespace UnitySRPFromScratch._01_Basic
{
    class SRPRenderer_01_Basic : SRPRenderer
    {
     
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
            //
            m_context.Submit();
        }
    }
}
