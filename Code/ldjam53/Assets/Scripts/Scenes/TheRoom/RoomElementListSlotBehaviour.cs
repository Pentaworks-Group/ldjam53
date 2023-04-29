using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Assets.Scripts.Models;

using GameFrame.Core.UI.List;

using TMPro;

using UnityEngine.UI;

namespace Assets.Scripts.Scenes.TheRoom
{
    public class RoomElementListSlotBehaviour : ListSlotBehaviour<RoomElement>
    {
        private Image iconImage;
        private TMP_Text nameText;
        private TMP_Text quantityText;

        private String currentIcon;

        public override void RudeAwake()
        {
            iconImage = transform.Find("Info/Icon").GetComponent<Image>();
            nameText = transform.Find("Info/NameText").GetComponent<TMP_Text>();
            quantityText = transform.Find("Info/QuantityText").GetComponent<TMP_Text>();
        }

        public override void UpdateUI()
        {
            if (currentIcon != content.IconReference)
            {
                currentIcon = content.IconReference;
                iconImage.sprite = GameFrame.Base.Resources.Manager.Sprites.Get(content.IconReference);
            }

            nameText.text = content.Texture;
            quantityText.text = UnityEngine.Random.Range(1, 100).ToString();
        }
    }
}
