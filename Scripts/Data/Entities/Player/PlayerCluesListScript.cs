using System;
using System.Collections.Generic;
using Misc;
using Misc.Overworld;
using Overworld.Clues;
using UnityEngine;

namespace EntityUtils.PlayerUtils
{
    public class PlayerCluesListScript : MonoBehaviour
    {

        List<ClueInDiaryInfoContainer> cluesKnowing;
        List<SetOfCluesInfo> cluesToChoose;

        PlayerHealthScript health;

        private void Start()
        {
            cluesKnowing = new List<ClueInDiaryInfoContainer>();
            cluesKnowing = FindAnyObjectByType<DataHolderScript>().GetClues();

            cluesToChoose = new List<SetOfCluesInfo>();
            cluesToChoose = FindAnyObjectByType<DataHolderScript>().GetSets();

            health = GetComponent<PlayerHealthScript>();

            OverworldEventPublisher.i.onSavingData += SaveData;
        }

        public void SaveData(object sender, EventArgs e)
        {
            FindAnyObjectByType<DataHolderScript>().SaveDiary(cluesKnowing, cluesToChoose);
        }

        private void OnDestroy()
        {
            OverworldEventPublisher.i.onSavingData -= SaveData;
        }

        //public List<ClueInDiaryInfoContainer> GetCluesList() { return cluesKnowing; }

        public List<ClueInDiaryInfoContainer> GetClues()
        {
            for (int i = 0; i < cluesKnowing.Count; i++)
            {
                if (cluesKnowing[i].changesByItself)
                {
                    foreach (SetOfCluesInfo set in cluesToChoose)
                    {
                        if (set.GetID() == cluesKnowing[i].comparisonID)
                        {
                            cluesKnowing.Remove(cluesKnowing[i]);
                            cluesKnowing.Insert(i, set.GetInfo(health.GetAmount()));
                        }
                    }
                }
            }
            return cluesKnowing;
        }

        public void AddToClueList(ClueInDiaryInfoContainer newObject)
        {
            for (int i = 0; i < cluesKnowing.Count; i++)
            {
                if (cluesKnowing[i].GetComparisonID() == newObject.GetComparisonID())
                {
                    cluesKnowing.Remove(cluesKnowing[i]);
                    cluesKnowing.Insert(i, newObject);
                    return;
                }
            }
            cluesKnowing.Add(newObject);
        }

        public void AddSet(SetOfCluesInfo set)
        {
            if (!cluesToChoose.Contains(set))
            {
                cluesToChoose.Add(set);
                cluesKnowing.Add(set.GetInfo(health.GetAmount()));
            }
        }

        public void AddList(List<SetOfCluesInfo> sets)
        {
            foreach(SetOfCluesInfo set in sets)
            {
                AddSet(set);
            }
        }

        public bool DoesHaveAClue(ClueInDiaryInfoContainer clue)
        {
            foreach(ClueInDiaryInfoContainer c in cluesKnowing)
            {
                if(c.comparisonID == clue.comparisonID) return true;
            }
            return false;
        }
    }
}