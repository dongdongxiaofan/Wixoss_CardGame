using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class LifeCloth : MonoBehaviour
    {
        public List<Card> LifeCloths = new List<Card>();
        public GameObject LifeObj;
        public List<GameObject> LifeObjs = new List<GameObject>();
        //public List<string> LifeClothId = new List<string>();

        public GameObject OtherLifeObj;
        public List<GameObject> OtherLifeObjs = new List<GameObject>();
        public Transform OtherLifeParent;
        public GameManager GameManager;
        public UIGrid OtherGrid;
        public UIGrid Grid;

        public EnerManager EnerManager;
        public Check Check;

        /// <summary>
        /// 生命护甲!!
        /// </summary>
        //        public List<Card> LifeCloths
        //        {
        //            get { return LifeForShow; }
        //            set
        //            {
        //                LifeForShow = value;
        //                if (LifeForShow.Count >= 0)
        //                {
        //                    for (int i = 0; i < LifeObj.Count; i++)
        //                    {
        //                        LifeObj[i].SetActive(i < LifeForShow.Count - 1);
        //                    }
        //                }
        //            }
        //        }

        public void CreateLifeCloth()
        {
            for (int i = 0; i < 7; i++)
            {
                var card = DataSource.MainDeck[DataSource.MainDeck.Count - 1];
                //LifeClothId.Add(card.CardId);
                LifeCloths.Add(card);
                CreateObj(i);
                GameManager.RpcCreateLifeCloth(i);
                DataSource.MainDeck.Remove(card);
            }
        }

        public void CreateObj(int num)
        {
            GameObject Life = Instantiate(LifeObj) as GameObject;
            LifeObjs.Add(Life);
            Transform tran = Life.transform;
            tran.parent = transform;
            tran.localScale = new Vector3(620,620,1);
            tran.localEulerAngles = Vector3.zero;
            tran.localPosition = new Vector3(0, 0, num);
            Grid.Reposition();
        }       

        public void CreateOtherCloth(int num)
        {
            GameObject otherLife = Instantiate(OtherLifeObj) as GameObject;
            OtherLifeObjs.Add(otherLife);
            Transform tran = otherLife.transform;
            tran.parent = OtherLifeParent;
            tran.localScale = new Vector3(620,620,1);
            tran.localEulerAngles = Vector3.zero;
            tran.localPosition = new Vector3(0, 0, num);
            OtherGrid.Reposition();
        }

        public void CrashCloth()
        {
            var card = LifeCloths [LifeCloths.Count - 1];
            var obj = LifeObjs [LifeCloths.Count - 1];
            Destroy(obj);
            if (card.HasBrust)
            {
                if(card.Brust!=null)
                {
                    card.Brust(card);
                }
            }

            EnerManager.CreateEner(card);
            GameManager.RpcEnerCharge(card.CardId);

            StartCoroutine(Check.SetCheck(card,true));
            GameManager.RpcCheck(card.CardId);
            LifeCloths.Remove(LifeCloths[LifeCloths.Count - 1]);
        }

        public void CrashOtherCloth()
        {
            if (OtherLifeObjs.Count == 0)
            {
                Debug.Log("Game Win!!");
                return;
            }
            var obj = OtherLifeObjs [OtherLifeObjs.Count - 1];
            OtherLifeObjs.Remove(obj);
            Destroy(obj);
        }
    }
}
