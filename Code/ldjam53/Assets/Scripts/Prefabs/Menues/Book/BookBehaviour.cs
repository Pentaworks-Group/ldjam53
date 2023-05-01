using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Assets.Scripts.Prefabs.Menues.Book
{
    public class BookBehaviour : MonoBehaviour
    {
        private Int32 currentPageIndex = -1;
        private GameObject pageBackButton;
        private GameObject indexPageButton;
        private GameObject pageForwardButton;


        private List<PageBehaviour> pages;
        private PageBehaviour currentPage;


        public void Awake()
        {
            this.pageBackButton = this.transform.Find("PageButtons/PageBackContainer/PageBackButton").gameObject;
            this.indexPageButton = this.transform.Find("PageButtons/IndexPageButton").gameObject;
            this.pageForwardButton = this.transform.Find("PageButtons/PageForwardContainer/PageForwardButton").gameObject;
            LoadPages();
            OpenPage(0);
        }

        private void LoadPages()
        {
            this.pages = new List<PageBehaviour>(); 
            foreach (Transform child in this.transform.Find("PageArea"))
            {
                var pageBehaviour = child.gameObject.GetComponent<PageBehaviour>();
                pageBehaviour.SetPageBehaviour(this);
                pages.Add(pageBehaviour);
            }
        }

        public void OpenPage(PageBehaviour page)
        {
            var index = this.pages.IndexOf(page);

            OpenPage(index);
        }

        public void OpenPage(Int32 pageIndex)
        {
            if (pageIndex >= 0 && pageIndex < this.pages.Count)
            {
                if (currentPage != null)
                {
                    currentPage.gameObject.SetActive(false);
                }

                currentPage = this.pages[pageIndex];
                currentPageIndex = pageIndex;

                currentPage.gameObject.SetActive(true);

                var canOpenPreviousPage = this.currentPageIndex > 0;
                var canOpenNextPage = this.currentPageIndex < (this.pages.Count - 1);

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
