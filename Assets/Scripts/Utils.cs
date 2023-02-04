using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
    public class FixedVectorQueue
    {
        private List<Vector3> queue;
        int _queueStartCounter;
        int QueueStartCounter
        {
            get
            {
                while (_queueStartCounter < 0)
                {
                    _queueStartCounter += queue.Count;
                }
                return _queueStartCounter % queue.Count;
            }
            set
            {
                _queueStartCounter = value;
            }
        }

        public Vector3 First
        {
            get
            {
                int index = (QueueStartCounter - 1) % queue.Count;
                while (index < 0)
                {
                    index += queue.Count;
                }
                
                return queue[index];
            }
        }

        public Vector3 Last => queue[QueueStartCounter];

        public FixedVectorQueue(int capacity)
        {
            queue = new(capacity);
            for (int i = 0; i < capacity; i++)
            {
                queue.Add(Vector3.zero);
            }
        }

        public void Enqueue(Vector3 item)
        {
            QueueStartCounter++;
            queue[QueueStartCounter] = item;
        }

    }
}