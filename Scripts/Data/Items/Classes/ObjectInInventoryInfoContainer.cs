using System;
using System.Collections.Generic;
using ItemUtils;
using Overworld.Clues;
using UnityEngine;
using Utils;
using Utils.Classes;

namespace Overworld.Items.Containers
{
    [Serializable]
    public class ObjectInInventoryInfoContainer : PlainObjectInInventoryInfoContainer
    {
        [Space]
        public bool hasClues = true;
        public SetOfCluesInfo clues;


        [Space]
        [Header("Great mental health options:")]
        public string pathToiconGreat;
        public Sprite iconGreat;
        public bool isUsableOnGreat = true;
        public List<PhraseToSay> descriptionGreat;
        public int greatDownBorder = Consts.GREAT_MENTAL_HEALTH_DOWN_BORDER_DEFAULT;

        [Space]
        [Header("Normal mental health options:")]
        public string pathToiconNormal;
        public Sprite iconNormal;
        public bool isUsableOnNormal;
        public List<PhraseToSay> descriptionNormal;
        public int normalDownBorder = Consts.NORMAL_MENTAL_HEALTH_DOWN_BORDER_DEFAULT;

        [Space]
        [Header("Bad mental health options:")]
        public string pathToiconBad;
        public Sprite iconBad;
        public bool isUsableOnBad;
        public List<PhraseToSay> descriptionBad;


        public ClueInDiaryInfoContainer GetClue(int health) { return hasClues ? clues.GetInfo(health) : null; }

        public void UpdateInformation(int mcHealth)
        {
            if (mcHealth <= 100 && mcHealth >= greatDownBorder)
            {
                currentSprite = iconGreat;
                currentSpritePath = pathToiconGreat;
                currentDescription = descriptionGreat;
                isUsableCurrently = isUsableOnGreat ? true : false;
                currentState = ItemStates.PERFECT;
            }
            else if (mcHealth < greatDownBorder && mcHealth >= normalDownBorder)
            {
                currentSprite = iconNormal;
                currentSpritePath = pathToiconNormal;
                currentDescription = descriptionNormal;
                isUsableCurrently = isUsableOnNormal ? true : false;
                currentState = ItemStates.GOOD;
            }
            else
            {
                currentSprite = iconBad;
                currentSpritePath = pathToiconBad;
                currentDescription = descriptionBad;
                isUsableCurrently = isUsableOnBad ? true : false;
                currentState = ItemStates.BAD;
            }
        }

    }
}