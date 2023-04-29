using System;
using System.Collections.Generic;

using UnityEngine;

namespace Assets.Scripts.Scenes.TheRoom
{
    public class HighlightBehaviour : MonoBehaviour
    {
        private static Color highlightColor = new Color(1, 0.4f, 0.7f, 1);

        private readonly List<Material> materials = new List<Material>();

        private void Awake()
        {
            foreach (var renderer in GetComponentsInChildren<Renderer>())
            {
                //A single child-object might have mutliple materials on it
                //that is why we need to all materials with "s"
                materials.AddRange(new List<Material>(renderer.materials));
            }
        }

        public void ToggleHighlight(Boolean isHighlighted)
        {
            if (isHighlighted)
            {
                foreach (var material in materials)
                {
                    //We need to enable the EMISSION
                    material.EnableKeyword("_EMISSION");
                    //before we can set the color
                    material.SetColor("_EmissionColor", highlightColor);
                }
            }
            else
            {
                foreach (var material in materials)
                {
                    //we can just disable the EMISSION
                    //if we don't use emission color anywhere else
                    material.DisableKeyword("_EMISSION");
                }
            }
        }
    }
}