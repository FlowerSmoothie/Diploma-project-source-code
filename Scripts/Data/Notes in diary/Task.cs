using UnityEngine;

namespace Overworld.Diary
{
    public class Task : Note
    {
        [SerializeField] bool isDone;

        public bool IsDone() { return isDone; }
    }
}