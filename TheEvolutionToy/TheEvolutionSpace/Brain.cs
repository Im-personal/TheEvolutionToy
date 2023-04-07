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

        public void resetColumn(int n, int nextLen)
        {
            Neyrons.Clear();
            for (int i = 0; i < n; i++)
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
        public List<Column>? Columns;

        public float X, Y;
        public List<Ceil> ceils;
        public Brain(String DNA)
        {
            ceils = new();
        }

        public Brain()
        {
            ceils = new();
            X = 0;
            Y = 0;
        }


        public void buildBrain()
        {

            Columns = new List<Column>();

            int midcols = 5;
            if (ceils.Count>3)
            {
                midcols=(int)(ceils.Count*0.8f);
            }

                Columns.Add(new Column(0, midcols));
            Columns.Add(new Column(midcols, midcols));
            Columns.Add(new Column(midcols, midcols));
            Columns.Add(new Column(midcols, 0));
            Columns.Add(new Column(0));

            var first = Columns[0].Neyrons;
            var last = Columns[Columns.Count-1].Neyrons;
            var almostlast = Columns[Columns.Count-2];

            

            foreach (Ceil c in ceils)
            {
                switch(c.type)
                {
                    
                    case Ceil.FANG:
                    case Ceil.BONE:
                    case Ceil.USUAL_CEIL:
                        Neyron touch = new(midcols);
                        Neyron damage = new(midcols);
                        c.addInput(touch);
                        c.addInput(damage);
                        first.Add(touch);
                        first.Add(damage);
                        break;
                    case Ceil.STOMACH:
                        touch = new(midcols);
                        damage = new(midcols);
                        Neyron consume = new(midcols);
                        c.addInput(consume);
                        c.addInput(touch);
                        c.addInput(damage);
                        first.Add(touch);
                        first.Add(damage);
                        first.Add(consume);
                        break;
                    case Ceil.THURSTER:
                        touch = new(midcols);
                        damage = new(midcols);
                        Neyron speed = new(midcols);
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
                        touch = new(midcols);
                        damage = new(midcols);
                        Neyron R = new(midcols);
                        Neyron G = new(midcols);
                        Neyron B = new(midcols);
                        Neyron dir = new(midcols);
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
            Neyron helper = new(midcols);
            first.Add(helper);
            helper.Value = 1;

            almostlast.resetColumn(midcols,last.Count);

        }

    }
}
