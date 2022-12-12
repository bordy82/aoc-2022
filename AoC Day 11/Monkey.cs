using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AoC_Day_11
{
    internal class Monkey
    {
        public int Id { get; private set; }

        public List<BigInteger> StartingItems { get; private set; }

        public Operation Operation { get; set; }

        public int OperationValue { get; set; }

        public int TestDivisible { get; set; }

        public int TestTrue { get; set; }

        public int TestFalse { get; set; }

        public long InspectCount { get; private set; }

        public Monkey(int id)
        {
            Id = id;
            this.InspectCount = 0;
            this.StartingItems = new List<BigInteger>();
        }

        public BigInteger GetNewWorryLevel(BigInteger worryLevel)
        {
            this.InspectCount++;

            var opsValue = this.OperationValue == -1 ? worryLevel : this.OperationValue;

            if (this.Operation == Operation.Multiply)
            {    
                return worryLevel * opsValue;
            }
            else
            {
                return worryLevel + opsValue;
            }
        }

        public BigInteger Test(BigInteger worryLevel)
        {
            if (worryLevel % this.TestDivisible == 0)
            {
                return this.TestTrue;
            }
            else
            {
                return this.TestFalse;
            }
        }
    }
}

enum Operation
{
    Add,
    Multiply
}
