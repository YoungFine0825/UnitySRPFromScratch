using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

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
        protected CullingResults m_cameraCullingResults;

        public ScriptableRenderContext context { get { return m_context; } }
        public Camera camera { get { return m_camera; } }


        public SRP_ERROR_CODE DoRender(ScriptableRenderContext context, Camera camera) 
        {
            this.m_context = context;
            this.m_camera = camera;
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

        protected void DrawSkyBox() 
        {
            this.m_context.DrawSkybox(this.m_camera);
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

