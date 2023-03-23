
using System.Drawing;

namespace TheEvolutionToy.TheEvolutionSpace
{

    internal class Color
    {
        public float R;
        public float G;
        public float B;

        public Color(float r, float g, float b)
        {
            R = r;
            G = g;
            B = b;
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





    }
}
