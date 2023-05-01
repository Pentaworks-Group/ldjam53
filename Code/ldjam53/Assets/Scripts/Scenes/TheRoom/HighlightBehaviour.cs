using System;
using System.Collections.Generic;

using Assets.Scripts.Models;

using UnityEngine;

namespace Assets.Scripts.Scenes.TheRoom
{
    public class HighlightBehaviour : MonoBehaviour
    {
        private const float INTENSITY = 0.4f;

        private static Vector4 selectedColor = new Vector4(1 * INTENSITY, 0.4f * INTENSITY, 0.7f * INTENSITY, 1);
        private static Vector4 notPlaceableColor = new Vector4(10 * INTENSITY, 0f * INTENSITY, 0f * INTENSITY, 1);

        private readonly List<Material> materials = new List<Material>();
        private Dictionary<Material, Boolean> hadEmission = new Dictionary<Material, bool>();
        private Dictionary<Material, Vector4> hadEmissionColor = new Dictionary<Material, Vector4>();

        private RoomElementBehaviour roomElementBehaviour;

        public void SetElement(RoomElementBehaviour roomElementBehaviour)
        {
            if (roomElementBehaviour == default)
            {
                throw new ArgumentNullException(nameof(roomElementBehaviour));
            }

            this.roomElementBehaviour = roomElementBehaviour;
        }

        public void UpdateHightlight()
        {
            if (!this.roomElementBehaviour.IsPlaceable)
            {
                EnableEmission(notPlaceableColor);
            }
            else if (this.roomElementBehaviour.Element.Selected)
            {
                EnableEmission(selectedColor);
            }
            else
            {
                DisableEmission();
            }
        }

        public void ReloadRenderers()
        {
            foreach (var renderer in GetComponentsInChildren<Renderer>())
            {
                //A single child-object might have mutliple materials on it
                //that is why we need to all materials with "s"
                materials.AddRange(new List<Material>(renderer.materials));
            }

            foreach (var material in materials)
            {
                //Save Emission state
                if (material.IsKeywordEnabled("_EMISSION"))
                {
                    /*                    if(hadEmission.ContainsKey(material))
                                        {
                                            hadEmission.Remove(material);
                                            hadEmissionColor.Remove(material);
                                        }*/

                    hadEmission[material] = true;
                    hadEmissionColor[material] = material.GetVector("_EmissionColor");
                }
                else
                {
                    hadEmission[material] = false;
                }
            }
        }

        private void EnableEmission(Vector4 emissionColor)
        {
            foreach (var material in materials)
            {

                //We need to enable the EMISSION
                material.EnableKeyword("_EMISSION");

                //before we can set the color
                material.SetVector("_EmissionColor", emissionColor);
            }
        }

        private void DisableEmission()
        {
            foreach (var material in materials)
            {
                if(!hadEmission[material])
                {
                    material.DisableKeyword("_EMISSION");
                } else
                {
                    material.SetVector("_EmissionColor", hadEmissionColor[material]);
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