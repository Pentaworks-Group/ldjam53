using System;

using UnityEngine;

namespace Assets.Scripts.Scenes.Credits
{
    public class CreditsBehaviour : Menues.BaseMenuBehaviour
    {
        public PageBehaviour[] Pages;

        private Int32 currentPageIndex = -1;
        private PageBehaviour currentPage;
        private GameObject pageBackButton;
        private GameObject indexPageButton;
        private GameObject pageForwardButton;

        protected override void OnStart()
        {
            this.pageBackButton = GameObject.Find("UI/PageButtons/PageBackButton");
            this.indexPageButton = GameObject.Find("UI/PageButtons/IndexPageButton");
            this.pageForwardButton = GameObject.Find("UI/PageButtons/PageForwardButton");

            OpenPage(0);
        }

        public void OpenPage(PageBehaviour pageBehaviour)
        {
            var index = Array.IndexOf(this.Pages, pageBehaviour);

            OpenPage(index);
        }

        public void OpenPage(Int32 pageIndex)
        {
            if (pageIndex >= 0 && pageIndex < this.Pages.Length)
            {
                if (currentPage != null)
                {
                    currentPage.gameObject.SetActive(false);
                }

                currentPage = this.Pages[pageIndex];
                currentPageIndex = pageIndex;

                currentPage.gameObject.SetActive(true);

                var canOpenPreviousPage = this.currentPageIndex > 0;
                var canOpenNextPage = this.currentPageIndex < (Pages.Length - 1);

                this.pageBackButton.SetActive(canOpenPreviousPage);
                this.indexPageButton.SetActive(canOpenPreviousPage);
                this.pageForwardButton.SetActive(canOpenNextPage);

                GameFrame.Base.Audio.Effects.Play("PageFlip");
            }
        }

        public void OnNextPageClicked()
        {
            OpenPage(++this.currentPageIndex);
        }

        public void OnPreviousPageClicked()
        {
            OpenPage(--this.currentPageIndex);
        }
    }
}
