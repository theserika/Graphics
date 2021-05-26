#if VFX_GRAPH_10_0_0_OR_NEWER
using System;
using UnityEditor.ShaderGraph;
using UnityEngine.Rendering;

namespace UnityEditor.ShaderGraph
{
    static class CreateVFXShaderGraph
    {
        [MenuItem("Assets/Create/Shader Graph/VFX Shader Graph", priority = CoreUtils.Sections.section1 + CoreUtils.Priorities.assetsCreateShaderMenuPriority + 2)]
        public static void CreateVFXGraph()
        {
            var target = (VFXTarget)Activator.CreateInstance(typeof(VFXTarget));

            var blockDescriptors = new[]
            {
                BlockFields.SurfaceDescription.BaseColor,
                BlockFields.SurfaceDescription.Alpha,
            };

            GraphUtil.CreateNewGraphWithOutputs(new[] {target}, blockDescriptors);
        }
    }
}
#endif
