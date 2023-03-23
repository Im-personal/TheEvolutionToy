using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheEvolutionToy.TheEvolutionSpace
{


    internal class Neyron
    {
        public float Value;
        public List<float> Weights;

        public Neyron(int lenOfNext=0)
        {
            Value = 0;
            Weights = new List<float>();
            for(int i = 0; i<lenOfNext; i++)
            {
                Weights.Add(GetRandomFloat(-1,1));
            }
        }

        private float GetRandomFloat(float min, float max)
        {
            Random rand = new Random();
            return (float)(rand.NextDouble() * (max - min) + min);
        }

        public int GetRandomNumber(int min, int max)
        {
            Random rand = new Random();
            return rand.Next(min, max);
        }
        public void Mutate(int iter)
        {
            for(int i = 0; i<iter;i++)
            {
                Weights[GetRandomNumber(0, Weights.Count)]+=GetRandomFloat(-1,1);
            }
        }

    }

    internal class Column
    {
        public List<Neyron> Neyrons = new();

        public Column(int n,int nextLen=0)
        {
            for(int i = 0; i<n;i++)
            {
                Neyrons.Add(new Neyron(nextLen));
            }
        }

        public void Clear()
        {
            for (int i = 0; i < Neyrons.Count; i++)
                Neyrons[i].Value = 0;
        }
        public void Mul(Column A)
        {
            Clear();
            var neyA = A.Neyrons;
            var neyB = Neyrons;
            for(int i = 0; i< neyA.Count;i++)
            {
                var weiA = neyA[i].Weights;
                for(int j = 0; j < weiA.Count;j++)
                {
                    neyB[j].Value += neyA[i].Value * weiA[j];
                }
            }
        }

        public void Mutate(int iter)
        {
            for (int i = 0; i < Neyrons.Count; i++)
                Neyrons[i].Mutate(iter);
        }

    }
    internal class Brain
    {
        List<Column> Columns;

        public float X, Y;

        public Brain(String DNA)
        {
            Columns = new List<Column>();
            Columns.Add(new Column(0, 5));
            Columns.Add(new Column(5, 5));
            Columns.Add(new Column(5, 0));
            Columns.Add(new Column(0));
        }

    }
}
