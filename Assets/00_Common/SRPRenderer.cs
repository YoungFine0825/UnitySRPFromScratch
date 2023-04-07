using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEditor;

namespace UnitySRPFromScratch
{
    public enum SRP_ERROR_CODE 
    {
        NONE = 0,
        CAMERA_CULLING_FAILED = 1,
    };

    public class SRPRenderer
    {
        protected ScriptableRenderContext m_context;

        protected Camera m_camera;

        protected RenderPipeline m_renderPipline;

        protected CullingResults m_cameraCullingResults;

        protected ShaderTagId[] m_legacyShaderTagIds = {
            new ShaderTagId("Always"),
            new ShaderTagId("ForwardBase"),
            new ShaderTagId("PrepassBase"),
            new ShaderTagId("Vertex"),
            new ShaderTagId("VertexLMRGBM"),
            new ShaderTagId("VertexLM")
        };

        protected ShaderTagId m_mainPass = new ShaderTagId("SRP_Pass_Main");

        protected Material m_errorMaterial = new Material(Shader.Find("Hidden/InternalErrorShader"));

        public ScriptableRenderContext context { get { return m_context; } }
        public Camera camera { get { return m_camera; } }


        public SRP_ERROR_CODE DoRender(ScriptableRenderContext context, Camera camera,RenderPipeline pipeline) 
        {
            this.m_context = context;
            this.m_camera = camera;
            this.m_renderPipline = pipeline;
            //
            SRP_ERROR_CODE errCode = ValidateRendering();
            if (errCode > 0) 
            {
                return errCode;
            }
            //
            OnRender();
            //
            return SRP_ERROR_CODE.NONE;
        }

        protected SRP_ERROR_CODE TryCameraCulling() 
        {
            if (this.m_camera.TryGetCullingParameters(out ScriptableCullingParameters p)) 
            {
                m_cameraCullingResults = this.m_context.Cull(ref p);
                return SRP_ERROR_CODE.NONE;
            }
            return SRP_ERROR_CODE.CAMERA_CULLING_FAILED;
        }

        protected virtual void SetupCamera() 
        {
            this.m_context.SetupCameraProperties(this.m_camera);
        }

        protected void ClearRenderTarget() 
        {
            bool drawSkyBox = m_camera.clearFlags == CameraClearFlags.Skybox ? true : false;
            bool clearDepth = m_camera.clearFlags == CameraClearFlags.Nothing ? false : true;
            bool clearColor = m_camera.clearFlags == CameraClearFlags.Color ? true : false;
            var clearCmd = CommandBufferPool.Get("Clear");
            clearCmd.ClearRenderTarget(clearDepth, clearColor, m_camera.backgroundColor);
            CommandBufferPool.ExecuteAndRelease(m_context, clearCmd);
            //
            if (drawSkyBox) m_context.DrawSkybox(m_camera);
        }

        protected void DrawSkyBox() 
        {
            this.m_context.DrawSkybox(this.m_camera);
        }

        protected void DrawGizmos() 
        {
#if UNITY_EDITOR
            if (Handles.ShouldRenderGizmos()) 
            {
                m_context.DrawGizmos(m_camera, GizmoSubset.PreImageEffects);
                m_context.DrawGizmos(m_camera, GizmoSubset.PostImageEffects);
            }
#endif
        }

        protected void DrawUnsupportedShaders()
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

        protected void Submit() 
        {
            this.m_context.Submit();
        }

        protected virtual SRP_ERROR_CODE ValidateRendering() 
        {
            return SRP_ERROR_CODE.NONE;
        }

        protected virtual void OnRender() 
        {

        }
    }
}

