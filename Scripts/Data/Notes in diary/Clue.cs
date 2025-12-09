using System;
using UnityEngine;

namespace Overworld.Diary
{
    public class Clue : Note
    {

        [SerializeField] private bool isComposite = false;

        public bool IsComposite() { return isComposite; }

    }
}