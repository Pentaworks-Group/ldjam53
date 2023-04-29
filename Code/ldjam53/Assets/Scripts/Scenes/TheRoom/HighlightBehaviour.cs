using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Assets.Scripts.Scenes.TheRoom
{
    public class HighlightBehaviour : MonoBehaviour
    {
        private const float INTENSITY = 0.4f;

        private static Vector4 testHighlightColor = new Vector4(1 * INTENSITY, 0.4f * INTENSITY, 0.7f * INTENSITY, 1);

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

                    //var currentColor = material.color;

                    //before we can set the color
                    material.SetVector("_EmissionColor", testHighlightColor);
                }

                //StartCoroutine(gloom());
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

        //private IEnumerator gloom()
        //{
        //    float i;

        //    while (true)
        //    {
        //        i = 0;
        //        while (i < 1)
        //        {
        //            currentColor = Vector3.Lerp(baseColor, offColor, i);
        //            print(currentColor);
        //            g.SetColor("_Color", currentColor);
        //            yield return new WaitForSeconds(.05f);
        //            i += 0.2f;

        //        }
        //        i = 1;
        //        g.SetColor("_Color", offColor);
        //        yield return new WaitForSeconds(.1f);
        //        while (i > 0)
        //        {

        //            currentColor = Vector3.Lerp(baseColor, offColor, i);
        //            print(currentColor);
        //            g.SetColor("_Color", currentColor);
        //            yield return new WaitForSeconds(.05f);
        //            i -= 0.2f;

        //        }
        //        g.SetColor("_Color", baseColor);
        //        yield return new WaitForSeconds(.5f);
        //    }

        //}
    }
}