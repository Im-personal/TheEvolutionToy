using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Mathematics;
using LearnOpenTK.Common;
using TheEvolutionToy.TheEvolutionSpace;
namespace GW
{

  



    internal class GraphicWindow : GameWindow
    {

        


        string title = "";
       
        public GraphicWindow(string title, int windowWidth, int windowHeight)
            : base(GameWindowSettings.Default, new NativeWindowSettings()
            {
                Size = new Vector2i(windowWidth, windowHeight),
                Title = title,
                // This is needed to run on macos
                Flags = ContextFlags.ForwardCompatible,
            })
        {
            this.title = title;
        
            VSync = VSyncMode.On;
            Run();
        }

        private float[] _vertices;
       

        private int _vertexBufferObject;

        private int _vertexArrayObject;

        private Shader _shader;

        int CircleStart=0;
        int CircleEnd = 0;

        int TriangleStart=0;
        int TriangleEnd = 0;

        int OctogonStart=0;
        int OctogonEnd = 0;

        int SquareStart = 0;
        int SquareEnd = 0;

        int LineStart = 0;
        int LineEnd = 0;

        private float Mult = 0.043056734f;
        private const float MultStart = 0.043056734f;
        private const float MultStartMult = 1/MultStart;
        private float MultSpeed = 1.1f;
        private float EyeX=0;
        private float EyeY=0;
        private float EyeSpeed = 1.0001f;
        private float ShapeMultX = 1;
        private float ShapeMultY = 1;
        private bool ShowBrain = false;

        protected override void OnLoad()
        {
            base.OnLoad();

            GL.ClearColor(0f, 0f, 0f, 1f);

            List<float> verts = new();
            int sides = 360;
            float rad = (float)Math.PI*2 / sides;
            for(int i = 0; i<=sides;i++)
            {
                verts.Add((float)(Math.Sin(rad*i)-Math.Cos(rad*i)));
                verts.Add((float)(Math.Sin(rad*i)+Math.Cos(rad*i)));
            }
            CircleEnd = sides+1;

            OctogonStart = verts.Count/2;
             sides = 8;
             rad = (float)Math.PI * 2 / sides;
            for (int i = 0; i <= sides+1; i++)
            {
                verts.Add((float)(Math.Sin(rad * i) - Math.Cos(rad * i)));
                verts.Add((float)(Math.Sin(rad * i) + Math.Cos(rad * i)));
            }
            OctogonEnd = sides+2;

            TriangleStart = verts.Count/2;
            sides = 3;
            rad = (float)Math.PI * 2 / sides;
            for (int i = 0; i <= sides; i++)
            {
                verts.Add((float)(Math.Sin(rad * i) - Math.Cos(rad * i)));
                verts.Add((float)(Math.Sin(rad * i) + Math.Cos(rad * i)));
            }
            TriangleEnd = sides + 1;
            SquareStart =verts.Count/2;
            SquareEnd = 4;
            verts.Add(0);
            verts.Add(1);
            LineStart = verts.Count / 2;
            verts.Add(0);
            verts.Add(0);
            verts.Add(1);
            verts.Add(1);
            LineEnd = 2;
            verts.Add(1);
            verts.Add(0);

            _vertices = verts.ToArray();
            _vertexBufferObject = GL.GenBuffer();

            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Length * sizeof(float), _vertices, BufferUsageHint.StaticDraw);

            _vertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(_vertexArrayObject);

            GL.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, 2 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            //GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 2 * sizeof(float));
            //GL.EnableVertexAttribArray(1);

            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);

            _shader = new Shader("Shaders/shader.vert", "Shaders/shader.frag");
            _shader.Use();

        }




        private int _FPS = 0;
        public double _fpstime = 0;

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.ClearColor(209/255f, 1, 1, 1);
            _shader.Use();




            draw();

            SwapBuffers();
            GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Length * sizeof(float), _vertices, BufferUsageHint.DynamicDraw);
        }


        public void draw()
        {
            DrawExistanceCreator();
        }


        public void DrawExistanceCreator()
        {


            shapecol = new(0, 0, 0, 0.5f);
            switch (CeilSelected)
            {
                case 2:
                    drawTriangle(mx, my);
                    break;
                case 4:
                    drawOctogon(mx, my);
                    break;
                default:
                    drawCircle(mx, my);
                    break;


            }

            DrawCreature(creature,true);
            if(ShowBrain)
            DrawBrain(creature);
            
            shapecol = new(0, 1, 1, 78f / 255);
            drawEyelessRect(-MultStartMult, -MultStartMult * 1000, MultStartMult / 2, MultStartMult * 2000);


            shapecol = new(0, 0.5f, 0.5f, 1);
            drawEyelessCircle(MultStartMult / 4 - MultStartMult, 19 + CeilSelected * (-7.5f), 3);


            //Ceil
            shapecol = new(0, 1, 0, 1);
            drawEyelessCircle(MultStartMult / 4 - MultStartMult, 19, 2);

            //Bone
            shapecol = new(1, 1, 1, 1);
            drawEyelessCircle(MultStartMult / 4 - MultStartMult, 19+(-7.5f), 2);


            //Fang
            shapecol = new(1, 0, 0, 1);
            drawEyelessTriangle(MultStartMult / 4 - MultStartMult, 19 + 2 * (-7.5f), 2);


            //Eye
            shapecol = new(0, 0, 0, 1);
            drawEyelessCircle(MultStartMult / 4 - MultStartMult, 19 + 3 * (-7.5f), 2);
            shapecol = new(1, 1, 1, 1);
            var a = GetAngle((MultStartMult / 4 - MultStartMult - EyeX), (19 + 3 * (-7.5f) - EyeY), (MouseState.X / Size.X - 0.5f) / (MultStart / 2 * ShapeMultX), (-MouseState.Y / Size.Y + 0.5f) / (MultStart / 2 * ShapeMultY));
            drawEyelessCircle(MultStartMult / 4 - MultStartMult + (float)Math.Cos(a / 180 * Math.PI), 19 + 3 * (-7.5f) - (float)Math.Sin(a / 180 * Math.PI), 2 / 2);


            //thurster
            shapecol = new(0, 0, 1, 1);
            drawEyelessOctogon(MultStartMult / 4 - MultStartMult, 19 + 4 * (-7.5f), 2);

            //stomach
            shapecol = new(1, 0, 220/255f, 1);
            drawEyelessCircle(MultStartMult / 4 - MultStartMult, 19 + 5 * (-7.5f), 2);



        }
        public int CeilSelected = 0;
        Brain creature = new();


        void DrawBrain(Brain b)
        {
            

            var l = b.Columns;
            shapecol = new(0, 0, 1);
            for(int i = 0; i<l.Count;i++)
            {
                var c = l[i].Neyrons;
                for(int j=0; j < c.Count;j++)
                {
                    var dist = -13.75f/(c.Count+1);
                    var nx = -10.4f + 4.5f * i;
                    var ny = 21.3f + dist * (j + 1);
                    drawEyelessCircle(nx, ny,0.2f);
                    
                    if (l.Count-1!=i)
                    for (int k = 0; k < c[j].Weights.Count;k++)
                    {
                        var dist2 = -13.75f / (l[i+1].Neyrons.Count + 1);
                        var nx2 = -10.4f + 4.5f * (i+1)-nx;
                        var ny2 = 21.3f + dist2 * (k + 1)-ny;
                        drawEyelessLine(nx,ny,nx2,ny2);
                    }
                }
            }
        }

        void DrawCreature(Brain b,bool lineItUp=false)
        {
            shapecol = new(1, 168f / 255f, 220f / 255f, 1);
            drawRect(b.X-1, b.Y-1, 2, 2);

            foreach (Ceil c in b.ceils)
            {
                shapecol = c.color;
                switch (c.type)
                {
                    case Ceil.FANG:
                        drawTriangle(b.X+c.attachX, b.Y + c.attachY);
                        break;
                    case Ceil.THURSTER:
                        drawOctogon(b.X + c.attachX, b.Y + c.attachY);
                        shapecol = shapecol/2;
                        drawLine(b.X + c.attachX, b.Y + c.attachY,(float)Math.Cos(c.direction), (float)Math.Sin(c.direction));
                        break;
                    case Ceil.EYE:
                        shapecol = new(0,0,0);
                        drawCircle(b.X + c.attachX, b.Y + c.attachY);
                        shapecol = new(1,1,1);
                        drawCircle(b.X + c.attachX+(float)Math.Cos(c.direction) * 0.5f, b.Y + c.attachY - (float)Math.Sin(c.direction)*0.5f, 0.5f);
                        break;
                    default:
                        drawCircle(b.X + c.attachX, b.Y + c.attachY);
                        break;


                }
                if(lineItUp)
                {
                
                 drawLine(b.X, b.Y, b.X + c.attachX, b.Y + c.attachY);
                }
                
            }

        }

        float GetAngle(float x0, float y0, float x1, float y1)
        {
            if (x0 == x1 && y0 == y1) return 0;
            float cat1 = x0 - x1;
            float cat2 = y0 - y1;
            float tgns = cat1 / cat2;
            if (y0 - y1 >= 0)
                return (float)(180 / Math.PI * Math.Atan(tgns) + 89);
            else
                return (float)(180 / Math.PI * Math.Atan(tgns) + 180 + 89);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
            countFPS(e);


            ProcessKeyboard();
            ProcessMouse();
            
            
        }
        float mx;
        float my;

        int MODE = 0;
        const int CREATURE_MODE = 0;
        const int LEARNING_MODE = 1;
        const int PLAYGROUND_MODE = 2;

        float track = 0;

        void ProcessMouse()
        {
            var m = MouseState;
            var sx = mx;
            var sy = my;

            mx = (m.X/Size.X-0.5f)/(Mult/2*ShapeMultX)-EyeX;
            my = (-m.Y/Size.Y+0.5f)/(Mult/2*ShapeMultY)-EyeY;

            //if creature mode
            switch(MODE)
            {
                case CREATURE_MODE:
                    if (m.IsButtonPressed(OpenTK.Windowing.GraphicsLibraryFramework.MouseButton.Button2))
                    {
                        foreach(Ceil c in creature.ceils)
                        {
                            if (dist(mx,my,c.attachX,c.attachY)<=1)
                            {
                                creature.ceils.Remove(c);
                                break;
                            }    
                        }
                    }
                        if (m.IsButtonPressed(OpenTK.Windowing.GraphicsLibraryFramework.MouseButton.Button1))
                    {
                        track = 0;
                        //MultStartMult / 4 - MultStartMult, 19 + CeilSelected * (-7.5f)
                        var screenx = (m.X / Size.X - 0.5f) / (MultStart / 2 * ShapeMultX);
                        var screeny = (-m.Y / Size.Y + 0.5f) / (MultStart / 2 * ShapeMultY);
                        if (Math.Abs((MultStartMult / 4 - MultStartMult) - screenx) <= 3)
                        {
                            for (int i = 0; i < 6; i++)
                            {
                                if (Math.Abs(19 + i * (-7.5f) - screeny) <= 3)
                                {
                                    CeilSelected = i;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            creature.ceils.Add(new(creature, CeilSelected, Ceil.GetDefault(CeilSelected), mx, my));
                        }

                       

                    }else
                    if (m.IsButtonDown(0))
                    {
                        track += (dist(mx, my, sx, sy));
                        if (track >= 2.75)
                        {
                            track = 0;
                            creature.ceils.Add(new(creature, CeilSelected, Ceil.GetDefault(CeilSelected), mx, my));
                        }
                    }
                    break;
            }
            

            


        }

        private float dist(float x1, float y1, float x2, float y2)
        {
            var x = (x1 - x2);
            var y = (y1 - y2);
            return (float)Math.Sqrt(x * x + y * y);
        }

        void ProcessKeyboard()
        {
            var k = KeyboardState;
            var es = EyeSpeed / (Mult * 20f);
            if (k.IsKeyDown(OpenTK.Windowing.GraphicsLibraryFramework.Keys.Escape))
            {
                this.Close();
            }


            switch (MODE)
            {
                case CREATURE_MODE:
                    if (k.IsKeyPressed(OpenTK.Windowing.GraphicsLibraryFramework.Keys.D1))
                        CeilSelected = 0;
                    if (k.IsKeyPressed(OpenTK.Windowing.GraphicsLibraryFramework.Keys.D2))
                        CeilSelected = 1;
                    if (k.IsKeyPressed(OpenTK.Windowing.GraphicsLibraryFramework.Keys.D3))
                        CeilSelected = 2;
                    if (k.IsKeyPressed(OpenTK.Windowing.GraphicsLibraryFramework.Keys.D4))
                        CeilSelected = 3;
                    if (k.IsKeyPressed(OpenTK.Windowing.GraphicsLibraryFramework.Keys.D5))
                        CeilSelected = 4;
                    if (k.IsKeyPressed(OpenTK.Windowing.GraphicsLibraryFramework.Keys.D6))
                        CeilSelected = 5;
                    if (k.IsKeyPressed(OpenTK.Windowing.GraphicsLibraryFramework.Keys.B))
                        creature.buildBrain();
                    
                    break;
            }
            ShowBrain = k.IsKeyDown(OpenTK.Windowing.GraphicsLibraryFramework.Keys.B);

            if (k.IsKeyDown(OpenTK.Windowing.GraphicsLibraryFramework.Keys.O))
            {
                Mult /= MultSpeed;
            }
            if (k.IsKeyDown(OpenTK.Windowing.GraphicsLibraryFramework.Keys.P))
            {
                Mult *= MultSpeed;
            }
            if (k.IsKeyDown(OpenTK.Windowing.GraphicsLibraryFramework.Keys.W))
            {
                EyeY -= EyeSpeed * es;
            }
            if (k.IsKeyDown(OpenTK.Windowing.GraphicsLibraryFramework.Keys.S))
            {
                EyeY += EyeSpeed * es;
            }
            if (k.IsKeyDown(OpenTK.Windowing.GraphicsLibraryFramework.Keys.D))
            {
                EyeX -= EyeSpeed * es;
            }
            if (k.IsKeyDown(OpenTK.Windowing.GraphicsLibraryFramework.Keys.A))
            {
                EyeX += EyeSpeed * es;
            }
        }
		
		void countFPS(FrameEventArgs e)
		{
			_fpstime += e.Time;
            _FPS++;
            if (_fpstime >= 1)
            {
                Title = title + $" FPS: {_FPS} mx: {mx} my: {my}";
                _fpstime = 0;
                _FPS = 0;

            }
		}

        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(0, 0, Size.X, Size.Y);
            if(Size.X>Size.Y)
            {
                ShapeMultX = (float)Size.Y / Size.X;
            }else
            {
                ShapeMultY = (float)Size.X / Size.Y;
            }

        }

        Color shapecol = new(1,1,1);
        public void drawCircle(float x, float y, float w = 1)
        {

            setUniforms(x, y, w, w);
            GL.DrawArrays(PrimitiveType.TriangleFan, CircleStart, CircleEnd);

        }

        public void drawEyelessCircle(float x, float y, float w = 1)
        {

            setEyelessUniforms(x, y, w, w);
            GL.DrawArrays(PrimitiveType.TriangleFan, CircleStart, CircleEnd);

        }

        public void drawOctogon(float x, float y, float w = 1)
        {

            setUniforms(x, y, w, w);
            GL.DrawArrays(PrimitiveType.TriangleFan, OctogonStart, OctogonEnd);

        }

        public void drawEyelessOctogon(float x, float y, float w = 1)
        {

            setEyelessUniforms(x, y, w, w);
            GL.DrawArrays(PrimitiveType.TriangleFan, OctogonStart, OctogonEnd);

        }

        public void drawHollowOctogon(float x, float y, float w = 1)
        {

            setUniforms(x, y, w, w);
            GL.DrawArrays(PrimitiveType.TriangleStrip, OctogonStart, OctogonEnd);

        }

        public void drawTriangle(float x, float y, float w = 1)
        {
            setUniforms(x,y,w,w);
            
            GL.DrawArrays(PrimitiveType.TriangleFan, TriangleStart, TriangleEnd);

        }

        public void drawEyelessTriangle(float x, float y, float w = 1)
        {
            setEyelessUniforms(x, y, w, w);

            GL.DrawArrays(PrimitiveType.TriangleFan, TriangleStart, TriangleEnd);

        }

        public void drawLine(float x1, float y1, float x2, float y2)
        {
            setUniforms(x1, y1, x2, y2);

            GL.DrawArrays(PrimitiveType.Lines, LineStart, LineEnd);

        }

        public void drawEyelessLine(float x1, float y1, float x2, float y2)
        {
            setEyelessUniforms(x1, y1, x2, y2);

            GL.DrawArrays(PrimitiveType.Lines, LineStart, LineEnd);

        }

        public void drawRect(float x, float y, float w = 1, float h = 1)
        {
            setUniforms(x, y, w, h);

            GL.DrawArrays(PrimitiveType.TriangleStrip, SquareStart, SquareEnd);
        }

        public void drawEyelessRect(float x, float y, float w = 1, float h = 1)
        {
            setEyelessUniforms(x, y, w, h);

            GL.DrawArrays(PrimitiveType.TriangleStrip, SquareStart, SquareEnd);
        }

        public void setUniforms(float x, float y, float w = 1, float h=1)
        {
            int moveLoc = GL.GetUniformLocation(_shader.Handle, "move");
            GL.Uniform4(moveLoc, EyeX+x, EyeY+y, w, h);
            int colorLoc = GL.GetUniformLocation(_shader.Handle, "color");
            GL.Uniform4(colorLoc, shapecol.R, shapecol.G, shapecol.B, shapecol.A);
            int multLoc = GL.GetUniformLocation(_shader.Handle, "mult");
            GL.Uniform2(multLoc, Mult*ShapeMultX,Mult*ShapeMultY);
        }

        public void setEyelessUniforms(float x, float y, float w = 1, float h = 1)
        {
            int moveLoc = GL.GetUniformLocation(_shader.Handle, "move");
            GL.Uniform4(moveLoc, x, y, w, h);
            int colorLoc = GL.GetUniformLocation(_shader.Handle, "color");
            GL.Uniform4(colorLoc, shapecol.R, shapecol.G, shapecol.B, shapecol.A);
            int multLoc = GL.GetUniformLocation(_shader.Handle, "mult");
            GL.Uniform2(multLoc, MultStart * ShapeMultX, MultStart * ShapeMultY);
        }



    }
}
