using System;
using System.Collections.Generic;
using System.Linq;

namespace ScriptureApp
{
    public class SelectionEngine
    {
        private Random _random = new Random();

        public Scripture GetNext(List<Scripture> pool)
        {
            if (pool == null || pool.Count == 0) return null;

            int totalWeight = pool.Sum(s => s.Weight);
            int determinePick = _random.Next(0, totalWeight);
            int currentSum = 0;

            foreach (var scripture in pool)
            {
                currentSum += scripture.Weight;
                if (determinePick < currentSum)
                {
                    UpdateWeights(pool, scripture);
                    return scripture;
                }
            }
            return pool[0]; // Fallback
        }

        private void UpdateWeights(List<Scripture> pool, Scripture selected)
        {
            selected.Weight = 1; 
            
            foreach (var s in pool.Where(x => x != selected))
            {
                s.Weight += 3; 
            }
        }
    }
}