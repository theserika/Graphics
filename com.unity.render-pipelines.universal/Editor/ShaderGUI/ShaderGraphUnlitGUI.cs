using System;
using UnityEngine;
using UnityEditor.Rendering.Universal;
using static Unity.Rendering.Universal.ShaderUtils;

namespace UnityEditor
{
    // Used for ShaderGraph Unlit shaders
    class ShaderGraphUnlitGUI : BaseShaderGUI
    {
        MaterialProperty[] properties;

        // collect properties from the material properties
        public override void FindProperties(MaterialProperty[] properties)
        {
            // save off the list of all properties for shadergraph
            this.properties = properties;

            base.FindProperties(properties);
        }

        public static void UpdateMaterial(Material material, MaterialUpdateType updateType)
        {
            // Determine whether render queue is user specified, or automatic.
            bool automaticRenderQueue = (material.HasProperty(Property.QueueControl) && material.GetInt(Property.QueueControl) == 0);
            if (automaticRenderQueue)
            {
                // Queue control is set to automatic
                // If the surface type property is not set (i.e., no material override)
                // Set the render queue back to "from shader"
                if (!material.HasProperty(Property.SurfaceType))
                    material.renderQueue = -1;
            }

            BaseShaderGUI.UpdateMaterialSurfaceOptions(material, automaticRenderQueue);
        }

        public override void ValidateMaterial(Material material)
        {
            UpdateMaterial(material, MaterialUpdateType.ModifiedMaterial);
        }

        // material main surface inputs
        public override void DrawSurfaceInputs(Material material)
        {
            DrawShaderGraphProperties(material, properties);
        }

        public override void DrawAdvancedOptions(Material material)
        {
            materialEditor.RenderQueueField();
            DoPopup(Styles.queueControl, queueControlProp, Styles.queueControlNames);
            base.DrawAdvancedOptions(material);
            materialEditor.DoubleSidedGIField();
        }
    }
} // namespace UnityEditor
