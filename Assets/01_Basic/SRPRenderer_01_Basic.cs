﻿using System;
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
            ClearRenderTarget();
            //Draw Opaques
            SortingSettings sortSetting = new SortingSettings(m_camera);
            sortSetting.criteria = SortingCriteria.CommonOpaque;
            DrawingSettings drawSetting = new DrawingSettings( m_mainPass,sortSetting);
            FilteringSettings filteringSetting = new FilteringSettings(RenderQueueRange.opaque);
            m_context.DrawRenderers(m_cameraCullingResults, ref drawSetting, ref filteringSetting);
            //Draw Transparents
            sortSetting.criteria = SortingCriteria.CommonTransparent;
            drawSetting.sortingSettings = sortSetting;
            filteringSetting.renderQueueRange = RenderQueueRange.transparent;
            m_context.DrawRenderers(m_cameraCullingResults, ref drawSetting, ref filteringSetting);
            //
            DrawUnsupportedObjects();
            //
            DrawGizmos();
            //
            m_context.Submit();
        }

        void DrawUnsupportedObjects() 
        {
            DrawingSettings drawSetting = new DrawingSettings(m_legacyShaderTagIds[0], new SortingSettings(camera));
            FilteringSettings filterSetting = FilteringSettings.defaultValue;
            drawSetting.overrideMaterial = m_errorMaterial;
            for (int i = 1; i < m_legacyShaderTagIds.Length; i++)
            {
                drawSetting.SetShaderPassName(i, m_legacyShaderTagIds[i]);
            }
            m_context.DrawRenderers(m_cameraCullingResults, ref drawSetting, ref filterSetting);
        }
    }
}
