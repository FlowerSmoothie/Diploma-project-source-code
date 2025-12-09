using System.Collections.Generic;
using Overworld.Clues;
using Overworld.Items.Containers;
using UnityEngine;

namespace Misc.Saving
{
    public static class DefaultToSaveableConverter
    {
        public static SaveablePlainObjectInInventory DefaultPlainObjectToSaveable(PlainObjectInInventoryInfoContainer original)
        {
            if (original.IsMedicine())
            {
                MedicineItemContainer originalMedicine = (MedicineItemContainer)original;
                SaveableMedicine newMedicine = new SaveableMedicine();
                newMedicine.currentCount = originalMedicine.currentCount;
                newMedicine.healCount = originalMedicine.healCount;

                List<MedicineItemPiece> pieces = originalMedicine.medicinePieces;
                newMedicine.medicinePieces = new List<SaveableMedicinePiece>();
                foreach (MedicineItemPiece piece in pieces)
                {
                    SaveableMedicinePiece saveable = new SaveableMedicinePiece();
                    saveable.description = piece.description;
                    saveable.sprite = piece.spritePath;
                    newMedicine.medicinePieces.Add(saveable);
                }

                newMedicine.ID = original.ID;
                newMedicine.nameInInventory = original.nameInInventory;
                newMedicine.isUsable = original.isUsable;
                newMedicine.isCombinable = original.isCombinable;
                newMedicine.isComposite = original.isComposite;

                newMedicine.currentSprite = original.currentSpritePath;
                newMedicine.currentState = original.currentState;
                newMedicine.currentDescription = original.currentDescription;
                newMedicine.isUsableCurrently = original.isUsableCurrently;

                newMedicine.isVisible = original.isVisible;

                return newMedicine;
            }
            else if (original.IsNote())
            {
                NoteInInventoryInfoContainer originalNote = (NoteInInventoryInfoContainer)original;
                SaveableNoteInInventory newNote = new SaveableNoteInInventory();

                newNote.hasClues = originalNote.hasClues;
                newNote.clues = DefaultSetOfCluesToSaveable(originalNote.clues);

                newNote.iconGreat = originalNote.pathToiconGreat;
                newNote.isUsableOnGreat = originalNote.isUsableOnGreat;
                newNote.descriptionGreat = originalNote.descriptionGreat;
                newNote.greatDownBorder = originalNote.greatDownBorder;

                newNote.iconNormal = originalNote.pathToiconNormal;
                newNote.isUsableOnNormal = originalNote.isUsableOnNormal;
                newNote.descriptionNormal = originalNote.descriptionNormal;
                newNote.normalDownBorder = originalNote.normalDownBorder;

                newNote.iconBad = originalNote.pathToiconBad;
                newNote.isUsableOnBad = originalNote.isUsableOnBad;
                newNote.descriptionBad = originalNote.descriptionBad;


                newNote.notesGreat = originalNote.notesGreat;
                newNote.notesNormal = originalNote.notesNormal;
                newNote.notesBad = originalNote.notesBad;

                newNote.ID = original.ID;
                newNote.nameInInventory = original.nameInInventory;
                newNote.isUsable = original.isUsable;
                newNote.isCombinable = original.isCombinable;
                newNote.isComposite = original.isComposite;

                newNote.currentSprite = original.currentSpritePath;
                newNote.currentState = original.currentState;
                newNote.currentDescription = original.currentDescription;

                newNote.isVisible = original.isVisible;
                
                newNote.isUsableCurrently = original.isUsableCurrently;

                return newNote;
            }
            else
            {
                return DefaultObjectToSaveable((ObjectInInventoryInfoContainer)original);
            }
        }

        public static SaveableSetOfCluesInfo DefaultSetOfCluesToSaveable(SetOfCluesInfo original)
        {
            SaveableSetOfCluesInfo newSet = new SaveableSetOfCluesInfo();

            newSet.greatMental = DefaultClueToSaveable(original.greatMental);
            newSet.greatDownBorder = original.greatDownBorder;
            newSet.normalMental = DefaultClueToSaveable(original.normalMental);
            newSet.normalDownBorder = original.normalDownBorder;
            newSet.badMental = DefaultClueToSaveable(original.badMental);

            return newSet;
        }

        public static SaveableClueInDiaryInfoContainer DefaultClueToSaveable(ClueInDiaryInfoContainer original)
        {
            SaveableClueInDiaryInfoContainer newClue = new SaveableClueInDiaryInfoContainer();

            newClue.comparisonID = original.comparisonID;
            newClue.changesByItself = original.changesByItself;
            newClue.canBeUsedAsDefault = original.canBeUsedAsDefault;

            newClue.state = original.state;
            newClue.nameInDiary = original.nameInDiary;
            newClue.icon = original.iconPath;
            newClue.description = original.description;

            newClue.isComposite = original.isComposite;

            return newClue;
        }

        public static SaveableObjectInInventory DefaultObjectToSaveable(ObjectInInventoryInfoContainer original)
        {
            SaveableObjectInInventory newObject = new SaveableObjectInInventory();
            newObject.hasClues = original.hasClues;
            newObject.clues = DefaultSetOfCluesToSaveable(original.clues);

            newObject.iconGreat = original.pathToiconGreat;
            newObject.isUsableOnGreat = original.isUsableOnGreat;
            newObject.descriptionGreat = original.descriptionGreat;
            newObject.greatDownBorder = original.greatDownBorder;

            newObject.iconNormal = original.pathToiconNormal;
            newObject.isUsableOnNormal = original.isUsableOnNormal;
            newObject.descriptionNormal = original.descriptionNormal;
            newObject.normalDownBorder = original.normalDownBorder;

            newObject.iconBad = original.pathToiconBad;
            newObject.isUsableOnBad = original.isUsableOnBad;
            newObject.descriptionBad = original.descriptionBad;

            newObject.ID = original.ID;
            newObject.nameInInventory = original.nameInInventory;
            newObject.isUsable = original.isUsable;
            newObject.isCombinable = original.isCombinable;
            newObject.isComposite = original.isComposite;

            newObject.currentSprite = original.currentSpritePath;
            newObject.currentState = original.currentState;
            newObject.currentDescription = original.currentDescription;
            newObject.isUsableCurrently = original.isUsableCurrently;

            newObject.isVisible = original.isVisible;

            return newObject;
        }
    }

    public static class SaveableToDefaultConverter
    {
        public static PlainObjectInInventoryInfoContainer SaveablePlainObjectToDefault(SaveablePlainObjectInInventory original)
        {
            if (original.IsMedicine())
            {
                SaveableMedicine originalMedicine = (SaveableMedicine)original;
                MedicineItemContainer newMedicine = new MedicineItemContainer();
                newMedicine.currentCount = originalMedicine.currentCount;
                newMedicine.healCount = originalMedicine.healCount;

                List<SaveableMedicinePiece> pieces = originalMedicine.medicinePieces;
                newMedicine.medicinePieces = new List<MedicineItemPiece>();
                foreach (SaveableMedicinePiece piece in pieces)
                {
                    MedicineItemPiece newPiece = new MedicineItemPiece();
                    newPiece.description = piece.description;
                    newPiece.sprite = Resources.Load<Sprite>(piece.sprite);
                    newPiece.spritePath = piece.sprite;
                    newMedicine.medicinePieces.Add(newPiece);
                }

                newMedicine.ID = original.ID;
                newMedicine.nameInInventory = original.nameInInventory;
                newMedicine.isUsable = original.isUsable;
                newMedicine.isCombinable = original.isCombinable;
                newMedicine.isComposite = original.isComposite;

                newMedicine.currentSprite = Resources.Load<Sprite>(original.currentSprite);
                newMedicine.currentSpritePath = original.currentSprite;
                newMedicine.currentState = original.currentState;
                newMedicine.currentDescription = original.currentDescription;
                newMedicine.isUsableCurrently = original.isUsableCurrently;

                newMedicine.isVisible = original.isVisible;

                return newMedicine;
            }
            else if (original.IsNote())
            {
                SaveableNoteInInventory originalNote = (SaveableNoteInInventory)original;
                NoteInInventoryInfoContainer newNote = new NoteInInventoryInfoContainer();

                newNote.hasClues = originalNote.hasClues;
                newNote.clues = SaveableSetOfCluesToDefault(originalNote.clues);

                newNote.iconGreat = Resources.Load<Sprite>(originalNote.iconGreat);
                newNote.pathToiconGreat = originalNote.iconGreat;
                newNote.isUsableOnGreat = originalNote.isUsableOnGreat;
                newNote.descriptionGreat = originalNote.descriptionGreat;
                newNote.greatDownBorder = originalNote.greatDownBorder;

                newNote.iconNormal = Resources.Load<Sprite>(originalNote.iconNormal);
                newNote.pathToiconNormal = originalNote.iconNormal;
                newNote.isUsableOnNormal = originalNote.isUsableOnNormal;
                newNote.descriptionNormal = originalNote.descriptionNormal;
                newNote.normalDownBorder = originalNote.normalDownBorder;

                newNote.iconBad = Resources.Load<Sprite>(originalNote.iconBad);
                newNote.pathToiconBad = originalNote.iconBad;
                newNote.isUsableOnBad = originalNote.isUsableOnBad;
                newNote.descriptionBad = originalNote.descriptionBad;


                newNote.notesGreat = originalNote.notesGreat;
                newNote.notesNormal = originalNote.notesNormal;
                newNote.notesBad = originalNote.notesBad;

                newNote.ID = original.ID;
                newNote.nameInInventory = original.nameInInventory;
                newNote.isUsable = original.isUsable;
                newNote.isCombinable = original.isCombinable;
                newNote.isComposite = original.isComposite;

                newNote.currentSprite = Resources.Load<Sprite>(original.currentSprite);
                newNote.currentSpritePath = originalNote.currentSprite;
                newNote.currentState = original.currentState;
                newNote.currentDescription = original.currentDescription;
                newNote.isUsableCurrently = original.isUsableCurrently;

                newNote.isVisible = original.isVisible;

                return newNote;
            }
            else
            {
                return SaveableObjectToDefault((SaveableObjectInInventory)original);
            }
        }

        public static SetOfCluesInfo SaveableSetOfCluesToDefault(SaveableSetOfCluesInfo original)
        {
            SetOfCluesInfo newSet = new SetOfCluesInfo();

            newSet.greatMental = SaveableClueToDefault(original.greatMental);
            newSet.greatDownBorder = original.greatDownBorder;
            newSet.normalMental = SaveableClueToDefault(original.normalMental);
            newSet.normalDownBorder = original.normalDownBorder;
            newSet.badMental = SaveableClueToDefault(original.badMental);

            return newSet;
        }

        public static ClueInDiaryInfoContainer SaveableClueToDefault(SaveableClueInDiaryInfoContainer original)
        {
            ClueInDiaryInfoContainer newClue = new ClueInDiaryInfoContainer();

            newClue.comparisonID = original.comparisonID;
            newClue.changesByItself = original.changesByItself;
            newClue.canBeUsedAsDefault = original.canBeUsedAsDefault;

            newClue.state = original.state;
            newClue.nameInDiary = original.nameInDiary;
            newClue.icon = Resources.Load<Sprite>(original.icon);
            newClue.iconPath = original.icon;
            newClue.description = original.description;

            newClue.isComposite = original.isComposite;

            return newClue;
        }

        public static ObjectInInventoryInfoContainer SaveableObjectToDefault(SaveableObjectInInventory original)
        {
            ObjectInInventoryInfoContainer newObject = new ObjectInInventoryInfoContainer();
            newObject.hasClues = original.hasClues;
            newObject.clues = SaveableSetOfCluesToDefault(original.clues);

            newObject.iconGreat = Resources.Load<Sprite>(original.iconGreat);
            newObject.pathToiconGreat = original.iconGreat;
            newObject.isUsableOnGreat = original.isUsableOnGreat;
            newObject.descriptionGreat = original.descriptionGreat;
            newObject.greatDownBorder = original.greatDownBorder;

            newObject.iconNormal = Resources.Load<Sprite>(original.iconNormal);
            newObject.pathToiconNormal = original.iconNormal;
            newObject.isUsableOnNormal = original.isUsableOnNormal;
            newObject.descriptionNormal = original.descriptionNormal;
            newObject.normalDownBorder = original.normalDownBorder;

            newObject.iconBad = Resources.Load<Sprite>(original.iconBad);
            newObject.pathToiconBad = original.iconBad;
            newObject.isUsableOnBad = original.isUsableOnBad;
            newObject.descriptionBad = original.descriptionBad;

            newObject.ID = original.ID;
            newObject.nameInInventory = original.nameInInventory;
            newObject.isUsable = original.isUsable;
            newObject.isCombinable = original.isCombinable;
            newObject.isComposite = original.isComposite;

            newObject.currentSprite = Resources.Load<Sprite>(original.currentSprite);
            newObject.currentSpritePath = original.currentSprite;
            newObject.currentState = original.currentState;
            newObject.currentDescription = original.currentDescription;
            newObject.isUsableCurrently = original.isUsableCurrently;

            newObject.isVisible = original.isVisible;

            return newObject;
        }
    }
}