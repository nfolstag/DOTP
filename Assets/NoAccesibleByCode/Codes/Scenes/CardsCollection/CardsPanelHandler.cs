using Assets.Codes.DAOs;
using Assets.Codes.Databases;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Codes.CardsCollection
{
    public class CardsPanelHandler : MonoBehaviour
    {
        private CardsDatabaseManager cdm;
        private int pageNum;
        public delegate void MoveActionDelegate();
        public static MoveActionDelegate MoveToRight;
        public static MoveActionDelegate MoveToLeft;

        public void Start()
        {
            pageNum = 0;
            MoveToRight = MMoveToRight;
            MoveToLeft = MMoveToLeft;
            GenerateCardsContainer(10);
            cdm = CardsDatabaseManager.GetInstance();
            ShowCards(10, 0);
        }

        private void ShowCards(int limit, int offset)
        {
            ClearPanel();
            List<Card> cards = cdm.GetCards(limit, offset);
            StringBuilder cardsPath =
                new StringBuilder(Constants.RESOURCES_ROOT_DIR_FULLPATH)
                .Append(Constants.RESOURCES_ROOT_PICS_CARDS);
            int containerPos = 0;
            foreach (Card card in cards)
            {
                Texture2D texture = null;
                WWW www = null;
                int i = 1;
                while (texture == null)
                {
                    string editionCodes = GetNthKey(card.Editions, i);
                    if (editionCodes == null) break;
                    string[] split = editionCodes.Split('-');
                    foreach (string s in split)
                    {
                        StringBuilder cardPath = new StringBuilder(cardsPath.ToString());
                        StringBuilder path = cardPath.Append(s).Append("/").Append(card.Name).Append(".full.jpg");
                        if (File.Exists(path.ToString()))
                        {
                            www = new WWW(path.ToString());
                            texture = www.texture;
                            break;
                        }
                    }
                    i++;
                }
                i = 0;
                SetRawImage(transform.GetChild(containerPos).gameObject, texture);
                if (texture != null)
                {
                    DestroyImmediate(www.texture, true);
                    www.Dispose();
                    www = null;
                }
                www = null;
                containerPos++;
            }
        }

        private void GenerateCardsContainer(int numContainers)
        {
            while (numContainers > 0)
            {
                GameObject panel = new GameObject("Panel");
                VerticalLayoutGroup vlg = panel.AddComponent<VerticalLayoutGroup>();                
                vlg.childForceExpandHeight = true;
                vlg.childForceExpandWidth = true;
                panel.SetActive(true);
                panel.transform.SetParent(transform, false);
                numContainers--;

            }
        }

        private void SetRawImage(GameObject container, Texture2D texture)
        {
            RawImage ri = container.GetComponent<RawImage>();
            if (ri == null)
                ri = container.AddComponent<RawImage>();
            ri.texture = texture;
            StartCoroutine(InitGC());
        }

        private void MMoveToRight()
        {
            pageNum++;
            ShowCards(10, pageNum * 10);
        }

        private void MMoveToLeft()
        {
            pageNum--;
            ShowCards(10, pageNum * 10);
        }

        private void ClearPanel()
        {
            int childCount = transform.childCount;
            while (childCount > 0)
            {
                GameObject child = transform.GetChild(childCount - 1).gameObject;
                DestroyImmediate(child.GetComponent<VerticalLayoutGroup>());               
                RawImage ri = child.GetComponent<RawImage>();
                if (ri != null)
                {
                    DestroyImmediate(ri.texture);
                    DestroyImmediate(ri);
                }
                childCount--;
            }
            StartCoroutine(InitGC());
        }

        private string GetNthKey(IList<string[]> list, int nthKey)
        {
            int i = 1;
            string currentKey = null;
            foreach (string[] pair in list)
            {
                if (i == nthKey)
                {
                    currentKey = pair[0];
                    break;
                }
                i++;
            }
            return currentKey;
        }

        private IEnumerator<object> InitGC()
        {
            GC.Collect();
            yield return new object();
        }
    }
}
