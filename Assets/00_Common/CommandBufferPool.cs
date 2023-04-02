using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace UnitySRPFromScratch
{
    public static class CommandBufferPool
    {
        private static Dictionary<string, CommandBuffer> m_cmdDict = new Dictionary<string, CommandBuffer>();
        private static Dictionary<string, bool> m_workingCmd = new Dictionary<string, bool>();

        private static bool isCmdWorking(string name) 
        {
            if (!m_workingCmd.ContainsKey(name)) 
            {
                return false;
            }
            bool isWorking = false;
            m_workingCmd.TryGetValue(name, out isWorking);
            return isWorking;
        }

        public static CommandBuffer Get(string name) 
        {
            if (isCmdWorking(name)) 
            {
                return null;
            }
            CommandBuffer cmd;
            if (!m_cmdDict.TryGetValue(name,out cmd)) 
            {
                cmd = new CommandBuffer();
                cmd.name = name;
                m_cmdDict[name] = cmd;
            }
            m_workingCmd[name] = true;
            return cmd;
        }

        public static void Release(string cmdName)
        {
            if (!m_cmdDict.ContainsKey(cmdName))
            {
                return;
            }
            m_cmdDict[cmdName].Clear();
            m_workingCmd[cmdName] = false;
        }

        public static void Release(CommandBuffer cmd) 
        {
            if (!m_cmdDict.ContainsKey(cmd.name)) 
            {
                cmd.Release();
                return;
            }
            Release(cmd.name);
        }

        public static void ExecuteAndRelease(ScriptableRenderContext context, CommandBuffer cmd) 
        {
            context.ExecuteCommandBuffer(cmd);
            Release(cmd);
        }
    }
}
