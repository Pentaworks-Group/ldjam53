using System;

using GameFrame.Core.UI.List;

using TMPro;

using UnityEngine.UI;

namespace Assets.Scripts.Scenes.TheRoom
{
    public class RoomElementListSlotBehaviour : ListSlotBehaviour<RoomElementListItem>
    {
        private Image iconImage;
        //private AspectRatioFitter ratioFitter;
        private TMP_Text nameText;
        private TMP_Text quantityText;

        private String currentIcon;

        public RoomElementListItem RoomElementItem
        {
            get
            {
                return this.content;
            }
        }

        public override void RudeAwake()
        {
            iconImage = transform.Find("Info/PicContainer/Icon").GetComponent<Image>();
            //ratioFitter = transform.Find("Info/PicContainer/Icon").GetComponent<AspectRatioFitter>();
            nameText = transform.Find("Info/NameText").GetComponent<TMP_Text>();
            quantityText = transform.Find("Info/QuantityText").GetComponent<TMP_Text>();
        }

        public override void UpdateUI()
        {
            if (currentIcon != content.IconReference)
            {
                currentIcon = content.IconReference;

                var icon = GameFrame.Base.Resources.Manager.Sprites.Get(content.IconReference);

                if (icon != null)
                {
                    iconImage.sprite = icon;
                    iconImage.preserveAspect = true;
                }
            }

            nameText.text = content.Name;
            quantityText.text = content.Quantity.ToString();
        }
    }
}
