using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsAccounting
{
    public class UndoRedoContainer<T>
    {
        private readonly int maxNumber;
        private IList<T> undoContainer;
        private IList<T> redoContainer;

        public UndoRedoContainer(int max)
        {
            maxNumber = max;
            undoContainer = new List<T>();
            redoContainer = new List<T>();
        }

        public int MaxNumber { get { return maxNumber; } }

        public int UndoContainerCount { get { return undoContainer.Count; } }

        public int RedoContainerCount { get { return redoContainer.Count; } }

        public T ItemForward
        {
            get
            {
                var result = redoContainer[redoContainer.Count - 1];

                undoContainer.Add(redoContainer[redoContainer.Count - 1]);
                redoContainer.RemoveAt(redoContainer.Count - 1);
                return result;
            }
        }

        public T ItemBackward
        {
            get
            {
                redoContainer.Add(undoContainer[undoContainer.Count - 1]);

                undoContainer.RemoveAt(undoContainer.Count - 1);
                var result = undoContainer[undoContainer.Count - 1];
                return result;
            }
        }

        public void AddItem(T newItem)
        {
            redoContainer.Clear();
            if (undoContainer.Count == maxNumber)
            {
                undoContainer.RemoveAt(0);
            }
            undoContainer.Add(newItem);
        }

        public void Reset()
        {
            undoContainer.Clear();
            redoContainer.Clear();
        }
    }
}
