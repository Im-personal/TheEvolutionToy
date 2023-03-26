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
        public List<Ceil> ceils;
        public Brain(String DNA)
        {
            
        }


        public void buildBrain()
        {

            Columns = new List<Column>();
            Columns.Add(new Column(0, 5));
            Columns.Add(new Column(5, 5));
            Columns.Add(new Column(5, 0));
            Columns.Add(new Column(0));

            var first = Columns[0].Neyrons;
            var last = Columns[Columns.Count-1].Neyrons;

            foreach (Ceil c in ceils)
            {
                switch(c.type)
                {
                    
                    case Ceil.FANG:
                    case Ceil.BONE:
                    case Ceil.USUAL_CEIL:
                        Neyron touch = new(5);
                        Neyron damage = new(5);
                        c.addInput(touch);
                        c.addInput(damage);
                        first.Add(touch);
                        first.Add(damage);
                        break;
                    case Ceil.STOMACH:
                        touch = new(5);
                        damage = new(5);
                        Neyron consume = new(5);
                        c.addInput(consume);
                        c.addInput(touch);
                        c.addInput(damage);
                        first.Add(touch);
                        first.Add(damage);
                        first.Add(consume);
                        break;
                    case Ceil.THURSTER:
                        touch = new(5);
                        damage = new(5);
                        Neyron speed = new(5);
                        c.addInput(speed);
                        c.addInput(touch);
                        c.addInput(damage);
                        first.Add(touch);
                        first.Add(damage);
                        first.Add(speed);

                        Neyron move = new();
                        c.addOutput(move);
                        last.Add(move);
                        break;

                    case Ceil.EYE:
                        touch = new(5);
                        damage = new(5);
                        Neyron R = new(5);
                        Neyron G = new(5);
                        Neyron B = new(5);
                        Neyron dir = new(5);
                        c.addInput(R);
                        c.addInput(G);
                        c.addInput(B);
                        c.addInput(dir);
                        c.addInput(touch);
                        c.addInput(damage);
                        first.Add(touch);
                        first.Add(damage);
                        first.Add(R);
                        first.Add(G);
                        first.Add(B);
                        first.Add(dir);

                        Neyron pupil = new();
                        c.addOutput(pupil);
                        last.Add(pupil);

                        break;



                }
            }

            
        }

    }
}
