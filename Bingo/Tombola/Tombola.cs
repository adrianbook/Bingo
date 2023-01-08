using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TombolaProject;
using TombolaProject.Extensions;

namespace TombolaProject
{
    public class Tombola
    {
        private Stack<int> Numbers;
        
        public Tombola()
        {
            var shuffledNumbers = Enumerable.Range(1, 75).ToList().ShuffleAndReturn();
            Numbers = new Stack<int>(shuffledNumbers);
        }
        public int NextNumber()
        {
            return Numbers.Pop();
        }

        
    }

}
