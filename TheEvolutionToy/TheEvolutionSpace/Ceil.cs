﻿
using System.Drawing;
using System.Reflection.Metadata.Ecma335;

namespace TheEvolutionToy.TheEvolutionSpace
{

    internal class Color
    {
        public float R;
        public float G;
        public float B;
        public float A;

        public Color(float r, float g, float b, float a=1)
        {
            R = r;
            G = g;
            B = b;
            A = a;  
        }

        public static Color operator/(Color c, float m)
        {
            var r = c.R / m;
            var g = c.G / m;
            var b = c.B / m;
            return new(r, g, b);
        }

    }
    internal class Ceil
    {



        /*                        ░    ░                 ░░░     ░░ ░░░`                  

                         ░      ,▒╓@@g▓▓▓g╥,      ░                             

                               j▓▓▓▓▓▓▓▓▓▓▓▓╬      ░                            

                             ]╢╢▓▓▓▓▓▓▓▓▓▓▓▓▓▓   ░        ░                     

                           `.╢▓▓╣▒▒▒╙╜╜╜╙╙╙▐▓▓[ ░              .                

                            ╟▓▓▒▒▒▒░░       ╟▓[   ░            ░                

                            ╫▓╣▒▒╢@@╖░ ░    ]╢[                                 

                            ╙╢▒▒▒╬Ñ║▒╣░╙╢╣▒▒▒╜           ,░░                    

                           ░▒`▒▒▒▒▒▒▒▒░     └                                   

                           '░▒▒▒▒▒▒▒╫ÑHH   ░░    .░             ░               

                       .      ╙▒▒▒╢▒▒╖,.,      .▒░                 ░ ░ ░        

                         ░    ,╨▒▒▒▒▒░░ ░░ ░░░░░ ░                              

                              ▒▒▒▒▒▒▒░░░░▒                                      

                       .▒     ║▒▒▒▒╢╜╜░░░ , ░                                   

                      .      ▒▒▒▒▒▒▒▒░░░░ ▓▓▄            ░░  ░                  

                  .  ,╓@@╣░╖ ╙╙║▒▒▒▒▒░░▒ ,▓▓▓█▓▄w,                              

                 ╓g▄▓▓█▓█▓▒╢╢╢╣╢▒▒▒▒▒Ñ`  ▓▓▓███████▓▄▄╖             `─          

            ,@▓▓▓▓████████▒╢╢╢╣"║▒║╜    ,▓▓████▀████████▓▄╖     ░      ░ ░░..   

            ▐▓███▓████████▌╣▓╣╣▒▒▒╖,   ▐▓███▓╬▒▒▒▒▒▀███████▓Φ                   

           ┌▓█████████████▓▓▓▓╢R▄▓▓▓▓║▓████▓▒▒▒▒▒▒░░▓███████▓                   

           ╟▓███████████████▒▒▓▀Ñ&&▄▄▓██████▓╣▒▒░░░▒▓████████[                  

          ░▓▓███████████████░░╢▀▀▀Ñ@▓▓███████▓╣▒▒░▒▒▓████████▌                  

  ░     `░▐▓███████████████▌░░▓▓▀▀▀▓▓█████████▓╣▒▒▒▒▓████████C   ░      ░       

         ░▓█████████████████@▒▓▄▓▓▀▀▓██████████▓╣▒▒╢▓████████▌                  

          ▓█████████████████▓▓▄▄▓▓▓▓█████████████████████████▌     `           ░

          ▓█████████████████▓▓▓▓▓▓▓▓██████████████████▓███████@  ```  ░─`────── 

        ░║▓█████████████████▓▓▓▓▓▓▓████████████████████████████▌  ─▒░░          

   ░. .a▓██████████▀▒╠╠▒▒▒╫█▓▓▀▓▓▓▓██████████████████████████████▓Ç░░░░         

   `░░▒▓▓▓▓▓███▓▓▓▒╜╢╢╢╣╢▓▓█▓▓▀▀▀▓▓█████████████████████████████████▄▒░         

     ▒▐▓▓▓▓▓▓▓▓▓▓▓@▓╣╣╣╣╢▓██▓▓▓▓▓▓███████████████████████████████████▒▒         

   .░░▒▓▓█████▓███▓▓▓▓▓╣╣▓██▓▓▓▓▓▓██████████████████████████████████▒▒▒░        

░.░░░ ▒▓███████████▓▓▓▓█████▓▓▓▓▓██████████████████████████████████▒▒▒▒░. ░     

░░░    ░▀███████████████████▓▓▓▓██████████████████████████████████▌▒▒▒▒▒░░░░▒░░░

▒░     ░░▒▒▒▒▒▒▒▀▀▀█████████▓▓▓▓████████████████████████████████▀░▒▒▒▒▒▒▒░░▒░▒▒▒

▒▒▒▒▒░▒▒▒▒▒▒▒▒▒╣╣▒╫█████████▓▓████████████████████████████▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒

▒▒▒▒▒▒▒▒▒▒▒▒▒▒╢╣╣▒╫█████████▓█████████████████████████████▓▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒*/
        
        public const int USUAL_CEIL = 0;
        public const int BONE = 1;
        public const int FANG = 2;
        public const int EYE = 3;
        public const int THURSTER = 4;
        public const int STOMACH = 5;
        public Color color;

        public bool IsAttachedToBrain;
        public Brain? AttachedToBrain;
        public Ceil? AttachedToCeil;
        public float attachX, attachY;
        public int type;
        public float direction = 0;

        List<Neyron> Input = new();
        List<Neyron> Output = new();

        public Ceil(Brain b, int type ,Color color,float attachX=0, float attachY=0)
        {
            IsAttachedToBrain = true;
            this.type = type;
            this.attachX = attachX;
            this.attachY = attachY;
            this.color = color;
            AttachedToBrain=b;
        }

        public Ceil(Ceil c, int type, Color color, float attachX = 0, float attachY = 0)
        {
            IsAttachedToBrain = false;
            this.type = type;
            this.attachX = attachX;
            this.attachY = attachY;
            this.color = color;
            AttachedToCeil = c;
        }

        public void addInput(Neyron n)
        {
            Input.Add(n);
        }

        public void addOutput(Neyron n)
        {
            Input.Add(n);
        }

        public static Color GetDefault(int type)
        {

            switch(type)
            {
                case USUAL_CEIL:
                    return new(0, 1, 0);
                case BONE:
                    return new(1, 1, 1);
                case FANG:
                    return new(1, 0, 0);
                case EYE:
                    return new(0, 0, 0);
                case THURSTER:
                    return new(0, 0, 1);
                case STOMACH:
                    return new(1, 0, 220 / 255f, 1);
            }
            return new(0, 0, 0);
        }

        

    }
}
