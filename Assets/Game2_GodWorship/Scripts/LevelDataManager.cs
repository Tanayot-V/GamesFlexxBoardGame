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

        private List<CardSO> poolCardNormalLevel1 = new List<CardSO>();
        private List<CardSO> poolCardNormalLevel2 = new List<CardSO>();
        private CardSO greenCard;
        private CardSO redCardLevel2;

        private List<CardSO> poolCardNormalLevel3 = new List<CardSO>();
        private CardSO redCardLevel3;

        private List<CardSO> poolCardNormalLevel4 = new List<CardSO>();
        private CardSO redCardLevel4;

        private List<CardSO> usedCards = new List<CardSO>();
        List<CardSO> selectedCards = new List<CardSO>(new CardSO[9]);
        public void InitGameEasyMode()
        {
            Debug.Log("InitGameEasyMode");
            ClearCards();
            cardDatabaseSO.level1Cards.ToList().ForEach(o => { poolCardNormalLevel1.Add(o);});
            cardDatabaseSO.level2Cards.ToList().ForEach(o => { 
                if(o.type != CardType.Green && o.type != CardType.Red) poolCardNormalLevel2.Add(o);
                if(o.type == CardType.Red) { redCardLevel2 = o;}
                if(o.type == CardType.Green) greenCard = o;
            });

            // == Lv1 ==
            for(int i = 0 ;i < 3; i++)
            {
                GetUniqueRandomCard(poolCardNormalLevel1);
            }

            // == Lv2 ==
            List<CardSO> usedLevel2 = new List<CardSO>();
            usedLevel2.Add(greenCard);
            usedLevel2.Add(redCardLevel2);
            GetUniqueRandomCard(poolCardNormalLevel2);
            usedLevel2 = usedLevel2.OrderBy(x => Random.value).ToList();
            usedLevel2.ForEach(o => { usedCards.Add(o); });
            GameManager.Instance.uIGameManager.SetAllIMG(usedCards);
        }

        public void InitGameNormalMode()
        {
            ClearCards();
            Debug.Log("InitGameNormalMode");
            cardDatabaseSO.level1Cards.ToList().ForEach(o => { poolCardNormalLevel1.Add(o);});
            cardDatabaseSO.level2Cards.ToList().ForEach(o => { 
                if(o.type != CardType.Green && o.type != CardType.Red) poolCardNormalLevel2.Add(o);
                if(o.type == CardType.Red) { redCardLevel2 = o;}
                if(o.type == CardType.Green) greenCard = o;
            });

            cardDatabaseSO.level3Cards.ToList().ForEach(o => { 
                if(o.type != CardType.Red) poolCardNormalLevel3.Add(o);
                if(o.type == CardType.Red) { redCardLevel3 = o;}
            });

            cardDatabaseSO.level4Cards.ToList().ForEach(o => { 
                if(o.type != CardType.Red) poolCardNormalLevel4.Add(o);
                if(o.type == CardType.Red) { redCardLevel4 = o;}
            });

            int randomChance = Random.Range(0, 100);
            
            /*----- LEVEL.1 ------*/
            for(int i = 0 ;i < 3; i++)
            {
                GetUniqueRandomCard(poolCardNormalLevel1);
            }

            /*----- CARD Green,Red ------*/
            selectedCards = new List<CardSO>(new CardSO[9]);
            HashSet<int> occupiedPositions = new HashSet<int>();

            // ฟังก์ชันสุ่มตำแหน่งที่ยังว่าง และตรวจสอบไม่ให้การ์ดสีแดงติดกัน
            int GetUniqueRandomPosition(int min, int max, params int[] restrictedAdjacent)
            {
                int pos;
                do
                {
                    pos = Random.Range(min, max);
                } while (occupiedPositions.Contains(pos) || restrictedAdjacent.Contains(pos));
                occupiedPositions.Add(pos);
                return pos;
            }

            // ** สุ่มตำแหน่งการ์ดสีเขียว **
            int greenIndex = Random.Range(0, 6);
            selectedCards[greenIndex] = greenCard;
            occupiedPositions.Add(greenIndex);

            // ** สุ่มตำแหน่งการ์ดสีแดง โดยห้ามอยู่ติดกัน **
            int redIndexLv2 = GetUniqueRandomPosition(0, 2  ,greenIndex);
            int redIndexLv3 = GetUniqueRandomPosition(3, 5, redIndexLv2,greenIndex); // ห้ามติด Lv2
            int redIndexLv4 = GetUniqueRandomPosition(6, 8, redIndexLv3,greenIndex); // ห้ามติด Lv3

            selectedCards[redIndexLv2] = redCardLevel2;
            selectedCards[redIndexLv3] = redCardLevel3;
            selectedCards[redIndexLv4] = redCardLevel4;

            // ** ค้นหาตำแหน่งที่ยังว่างอยู่ **
            List<int> emptySlotsLv2 = Enumerable.Range(0, 3).Where(i => i < selectedCards.Count && selectedCards[i] == null).ToList();
            List<int> emptySlotsLv3 = Enumerable.Range(3, 3).Where(i => i < selectedCards.Count && selectedCards[i] == null).ToList();
            List<int> emptySlotsLv4 = Enumerable.Range(6, 3).Where(i => i < selectedCards.Count && selectedCards[i] == null).ToList();

            // ** สุ่มและเลือกการ์ดธรรมดาที่ไม่ซ้ำกัน **
            List<CardSO> shuffledLv2 = poolCardNormalLevel2.OrderBy(x => Random.value).Distinct().Take(emptySlotsLv2.Count).ToList();
            List<CardSO> shuffledLv3 = poolCardNormalLevel3.OrderBy(x => Random.value).Distinct().Take(emptySlotsLv3.Count).ToList();
            List<CardSO> shuffledLv4 = poolCardNormalLevel4.OrderBy(x => Random.value).Distinct().Take(emptySlotsLv4.Count).ToList();

            // ** ใส่การ์ดธรรมดาที่ไม่ซ้ำกัน ลงในช่องที่เหลือ **
            for (int i = 0; i < emptySlotsLv2.Count; i++)
            {
                selectedCards[emptySlotsLv2[i]] = shuffledLv2[i];
            }
            for (int i = 0; i < emptySlotsLv3.Count; i++)
            {
                selectedCards[emptySlotsLv3[i]] = shuffledLv3[i];
            }
            for (int i = 0; i < emptySlotsLv4.Count; i++)
            {
                selectedCards[emptySlotsLv4[i]] = shuffledLv4[i];
            }
            selectedCards.ForEach(o => { usedCards.Add(o); });
            GameManager.Instance.uIGameManager.SetAllIMG(usedCards);
        }

        public void InitGameHardMode()
        {
            ClearCards();
            cardDatabaseSO.level1Cards.ToList().ForEach(o => { poolCardNormalLevel1.Add(o);});
            cardDatabaseSO.level2Cards.ToList().ForEach(o => { 
                if(o.type != CardType.Green && o.type != CardType.Red) poolCardNormalLevel2.Add(o);
                if(o.type == CardType.Red) { redCardLevel2 = o;}
                if(o.type == CardType.Green) greenCard = o;
            });

            cardDatabaseSO.level3Cards.ToList().ForEach(o => { 
                if(o.type != CardType.Red) poolCardNormalLevel3.Add(o);
                if(o.type == CardType.Red) { redCardLevel3 = o;}
            });

            cardDatabaseSO.level4Cards.ToList().ForEach(o => { 
                if(o.type != CardType.Red) poolCardNormalLevel4.Add(o);
                if(o.type == CardType.Red) { redCardLevel4 = o;}
            });

            /*----- LEVEL.1 ------*/            
            for(int i = 0 ;i < 5; i++) { GetUniqueRandomCard(poolCardNormalLevel1); }
            /*----- END LEVEL.1 ------*/
            
            selectedCards = new List<CardSO>(new CardSO[15]);
            HashSet<int> occupiedPositions = new HashSet<int>();
             // ฟังก์ชันสุ่มตำแหน่งที่ยังว่าง และตรวจสอบไม่ให้การ์ดสีแดงติดกัน
            int GetUniqueRandomPosition(int min, int max, params int[] restrictedAdjacent)
            {
                int pos;
                do
                {
                    pos = Random.Range(min, max);
                } while (occupiedPositions.Contains(pos) || restrictedAdjacent.Contains(pos));
                occupiedPositions.Add(pos);
                return pos;
            }

            // ** สุ่มตำแหน่งการ์ดสีเขียว **
            int greenIndex = Random.Range(0, 10);
            selectedCards[greenIndex] = greenCard;
            occupiedPositions.Add(greenIndex);

            // ** สุ่มตำแหน่งการ์ดสีแดง โดยห้ามอยู่ติดกัน **
            int redIndexLv2 = GetUniqueRandomPosition(0, 4, greenIndex);
            int redIndexLv3 = GetUniqueRandomPosition(5, 9, redIndexLv2,greenIndex); // ห้ามติด Lv2
            int redIndexLv4 = GetUniqueRandomPosition(10, 14, redIndexLv3,greenIndex); // ห้ามติด Lv3

            selectedCards[redIndexLv2] = redCardLevel2;
            selectedCards[redIndexLv3] = redCardLevel3;
            selectedCards[redIndexLv4] = redCardLevel4;
            
            // ** ค้นหาตำแหน่งที่ยังว่างอยู่ **
            List<int> emptySlotsLv2 = Enumerable.Range(0, 5).Where(i => i < selectedCards.Count && selectedCards[i] == null).ToList();
            List<int> emptySlotsLv3 = Enumerable.Range(5, 5).Where(i => i < selectedCards.Count && selectedCards[i] == null).ToList();
            List<int> emptySlotsLv4 = Enumerable.Range(10, 5).Where(i => i < selectedCards.Count && selectedCards[i] == null).ToList();

            // ** สุ่มและเลือกการ์ดธรรมดาที่ไม่ซ้ำกัน **
            List<CardSO> shuffledLv2 = poolCardNormalLevel2.OrderBy(x => Random.value).Distinct().Take(emptySlotsLv2.Count).ToList();
            List<CardSO> shuffledLv3 = poolCardNormalLevel3.OrderBy(x => Random.value).Distinct().Take(emptySlotsLv3.Count).ToList();
            List<CardSO> shuffledLv4 = poolCardNormalLevel4.OrderBy(x => Random.value).Distinct().Take(emptySlotsLv4.Count).ToList();

            // ** ใส่การ์ดธรรมดาที่ไม่ซ้ำกัน ลงในช่องที่เหลือ **
            for (int i = 0; i < emptySlotsLv2.Count; i++)
            {
                selectedCards[emptySlotsLv2[i]] = shuffledLv2[i];
            }
            for (int i = 0; i < emptySlotsLv3.Count; i++)
            {
                selectedCards[emptySlotsLv3[i]] = shuffledLv3[i];
            }

            List<CardSO> level4Card = new List<CardSO>();
            int count = System.Math.Min(emptySlotsLv4.Count, shuffledLv4.Count);
            for (int i = 0; i < count; i++)
            {
                level4Card.Add(shuffledLv4[i]);
                //selectedCards[emptySlotsLv4[i]] = shuffledLv4[i];
            }
            int remainingSlots = emptySlotsLv4.Count - count;
            if (remainingSlots > 0)
            {
                List<CardSO> extraLv3Cards = poolCardNormalLevel3.Except(shuffledLv3) // เอาการ์ดที่ยังไม่ได้ใช้
                                                                .OrderBy(x => Random.value)
                                                                .Take(remainingSlots)
                                                                .ToList();

                for (int i = 0; i < extraLv3Cards.Count; i++)
                {
                    level4Card.Add(extraLv3Cards[i]);
                    //selectedCards[emptySlotsLv4[count + i]] = extraLv3Cards[i];
                }
            }

            level4Card.OrderBy(x => Random.value).ToList();
            for (int i = 0; i < emptySlotsLv4.Count; i++)
            {
                selectedCards[emptySlotsLv4[i]] = level4Card[i];
            }

            selectedCards.ForEach(o => { usedCards.Add(o); });
            GameManager.Instance.uIGameManager.SetAllIMG(usedCards);
        }

        CardSO GetUniqueRandomCard(List<CardSO> _poolCardSOs)
            {
                if (_poolCardSOs == null || _poolCardSOs.Count == 0)
                {
                    Debug.LogWarning("No cards available in the pool!");
                    return null;
                }
                int randomIndex = Random.Range(0, _poolCardSOs.Count);
                CardSO selectedCard = _poolCardSOs[randomIndex];
                usedCards.Add(selectedCard);
                if(selectedCard.type != CardType.Red) _poolCardSOs.RemoveAt(randomIndex);
                _poolCardSOs.RemoveAll(item => item == null);
                return selectedCard;
            }

        public void ClearCards()
        {
            UiController.Instance.DestorySlot(GameManager.Instance.uIGameManager.imgChilds);
            poolCardNormalLevel1.Clear();
            poolCardNormalLevel2.Clear();
            poolCardNormalLevel3.Clear();
            poolCardNormalLevel4.Clear();
            greenCard = null;
            redCardLevel2 = null;
            redCardLevel3 = null;
            redCardLevel4 = null;
            usedCards.Clear();
            GameManager.Instance.uIGameManager.showGO.transform.GetChild(1).GetComponent<CanvasGroup>().alpha = 1;
        }
    }
}
