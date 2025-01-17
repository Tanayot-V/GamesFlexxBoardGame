using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace GodWarShip
{
    public enum LevelCard
    {
        Level1,
        Level2,
        Level3,
        Level4
    }

    public enum CardType
    {
        Normal,
        Green,
        Red
    }

    public class LevelDataManager : MonoBehaviour
    {
        public CardDatabaseSO cardDatabaseSO;

        [SerializeField] List<CardSO> poolCardNormalLevel1 = new List<CardSO>();
        [SerializeField] List<CardSO> poolCardNormalLevel2 = new List<CardSO>();
        [SerializeField] CardSO greenCard;
        [SerializeField] CardSO redCardLevel2;

        [SerializeField] List<CardSO> poolCardNormalLevel3 = new List<CardSO>();
        [SerializeField] CardSO redCardLevel3;

        [SerializeField] List<CardSO> poolCardNormalLevel4 = new List<CardSO>();
        [SerializeField] CardSO redCardLevel4;

        [SerializeField] bool isShowGreenCard;
        [SerializeField] List<CardSO> usedCards = new List<CardSO>();
        private LevelCard currentLevelCardl;

        public void InitGame()
        {
            cardDatabaseSO.level1Cards.ToList().ForEach(o => { poolCardNormalLevel1.Add(o);});

            cardDatabaseSO.level2Cards.ToList().ForEach(o => { 
                if(o.type != CardType.Green) poolCardNormalLevel2.Add(o);
                if(o.type == CardType.Red) { redCardLevel2 = o;}
                if(o.type == CardType.Green) greenCard = o;
            });

            cardDatabaseSO.level3Cards.ToList().ForEach(o => { 
                poolCardNormalLevel3.Add(o);
                if(o.type == CardType.Red) { redCardLevel3 = o;}
            });

            cardDatabaseSO.level4Cards.ToList().ForEach(o => { 
                poolCardNormalLevel4.Add(o);
                if(o.type == CardType.Red) { redCardLevel4 = o;}
            });
        }

       public void RandomCard()
       {
            int randomChance = Random.Range(0, 100);

           //Level 1 สุ่มการ์ดในเลเวลแบบไม่ซ้ำ 100%
            for(int i = 0 ;i < 3; i++)
            {
                CardSO selectedCard = GetUniqueRandomCard(poolCardNormalLevel1);
                if (selectedCard != null)
                {
                    Debug.Log($"Round {i + 1}: Selected Card = {selectedCard.name}");
                }
            }

            //หาตำแหน่งแทรกการ์ดสีเขียว
            int indexGreenCard = Random.Range(1, 7);
            Debug.Log("<color=green>Index Green Card:"+ indexGreenCard+"</color>");
            
            // == Lv2 ==
            // Level 2 => Lv 20% Lv2 80%
            int percentageFromLevel1 = 20; // สัดส่วนการ์ดจากเลเวล 1 (20%)
            for(int i = 0 ;i < 3; i++)
            {
                if(indexGreenCard == 1 && i == 0) { usedCards.Add(greenCard); }
                else if(indexGreenCard == 2 && i == 1) { usedCards.Add(greenCard); }
                else if(indexGreenCard == 3 && i == 2) { usedCards.Add(greenCard); }
                else
                {
                    if (randomChance <= percentageFromLevel1 && poolCardNormalLevel1.Count > 0)
                    {
                        // สุ่มจาก Pool 1
                        RandomCard(poolCardNormalLevel1);
                    }
                    else
                    {
                        // สุ่มจาก Pool 2
                        RandomCard(poolCardNormalLevel2);
                    }
                }
            }
            /*
            //เช็ค Level 2 ว่าออก RedCard ไปหรือยัง
            bool isRedLv2 = false;
            usedCards.ForEach(o => { if(o == redCardLevel2) isRedLv2 = true;});
            if(!isRedLv2) 
            {
                poolCardNormalLevel3.Add(redCardLevel3);
                Debug.Log("<color=red> ADD RED CARD TO POOL LEVEL3</color>");
            }
            poolCardNormalLevel2.RemoveAll(o => o != null && o.type == CardType.Red);
            
            // == Lv3 ==
            // Level 3 => Lv2 30% Lv3 70%
            int percentageFromLevel2 = 30; // สัดส่วนการ์ดจากเลเวล 1 (20%)
            for(int i = 0 ;i < 3; i++)
            {
                if(indexGreenCard == 4 && i == 0) { usedCards.Add(greenCard); i++;}
                else if(indexGreenCard == 5 && i == 1) { usedCards.Add(greenCard); i++;}
                else if(indexGreenCard == 6 && i == 2) { usedCards.Add(greenCard); i++;}
                else
                {
                    if (randomChance <= percentageFromLevel2 && poolCardNormalLevel2.Count > 0)
                    {
                        GetUniqueRandomCard(poolCardNormalLevel2);
                    }
                    else
                    {
                    GetUniqueRandomCard(poolCardNormalLevel3);
                    }
                }
            }
            //เช็ค Level 2,Level 3 ว่าออก RedCard ไปหรือยัง
            bool isRedLv3 = false;
            usedCards.ForEach(o => { if(o == redCardLevel3) isRedLv3 = true;});
            if(!isRedLv2) 
            {
                poolCardNormalLevel4.Add(redCardLevel4);
                Debug.Log("<color=red> ADD RED CARD TO POOL LEVEL4-1</color>");
            }
            if(!isRedLv3) 
            {
                poolCardNormalLevel4.Add(redCardLevel4);
                Debug.Log("<color=red> ADD RED CARD TO POOL LEVEL4-2</color>");
            }

            poolCardNormalLevel3.RemoveAll(o => o != null && o.type == CardType.Red);
            /*
            // == Lv4 ==
            // Level 4 => Lv3 40% Lv4 60%
            int percentageFromLevel3 = 40; // สัดส่วนการ์ดจากเลเวล 1 (20%)
            for(int i = 0 ;i < 3; i++)
            {
                // สุ่มค่าเปอร์เซ็นต์
                if (randomChance <= percentageFromLevel3 && poolCardNormalLevel3.Count > 0)
                {
                    GetUniqueRandomCard(poolCardNormalLevel3);
                }
                else
                {
                   GetUniqueRandomCard(poolCardNormalLevel4);
                }
            }*/
            GameManager.Instance.uIGameManager.SetAllIMG(usedCards);
            
            void RandomCard(List<CardSO> _poolCardSOs)
            {
                RemoveCard(_poolCardSOs,GetUniqueRandomCard(_poolCardSOs));
            }

            CardSO GetUniqueRandomCard(List<CardSO> _poolCardSOs)
            {
                if (_poolCardSOs == null || _poolCardSOs.Count == 0)
                {
                    Debug.LogWarning("No cards available in the pool!");
                    return null;
                }

                List<CardSO> tempPool = new List<CardSO>(_poolCardSOs);
                int randomIndex = Random.Range(0, _poolCardSOs.Count);
                CardSO selectedCard = _poolCardSOs[randomIndex];

                usedCards.Add(selectedCard);
                //if(selectedCard.type != CardType.Red)  tempPool.RemoveAt(randomIndex);
                return selectedCard;
            }

            void RemoveCard(List<CardSO> _poolCardSOs,CardSO _cardSO)
            {
                //if(_cardSO.type != CardType.Red) _poolCardSOs.RemoveAll(o => o != null && o == _cardSO);
            }
       }


        public void ClearCards()
        {
            UiController.Instance.DestorySlot(GameManager.Instance.uIGameManager.imgChilds);
            poolCardNormalLevel1.Clear();
            poolCardNormalLevel2.Clear();
            poolCardNormalLevel3.Clear();
            poolCardNormalLevel4.Clear();
            isShowGreenCard = false;
            usedCards.Clear();
        }
    }
}
