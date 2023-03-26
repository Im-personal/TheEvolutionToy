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
        private float MultSpeed = 1.1f;
        private float EyeX=0;
        private float EyeY=0;
        private float EyeSpeed = 1.0001f;
        private float ShapeMultX = 1;
        private float ShapeMultY = 1;

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
                verts.Add((float)(Math.Sin(rad * i) - Math.Cos(rad * i)) * 0.2f);
                verts.Add((float)(Math.Sin(rad * i) + Math.Cos(rad * i)) * 0.2f);
            }
            OctogonEnd = sides+2;

            TriangleStart = verts.Count/2;
            sides = 3;
            rad = (float)Math.PI * 2 / sides;
            for (int i = 0; i <= sides; i++)
            {
                verts.Add((float)(Math.Sin(rad * i) - Math.Cos(rad * i)) * 0.2f);
                verts.Add((float)(Math.Sin(rad * i) + Math.Cos(rad * i)) * 0.2f);
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



            _shader = new Shader("Shaders/shader.vert", "Shaders/shader.frag");
            _shader.Use();

        }




        private int _FPS = 0;
        public double _fpstime = 0;

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.ClearColor(1, 1, 1, 1);
            _shader.Use();




            draw();

            SwapBuffers();
            GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Length * sizeof(float), _vertices, BufferUsageHint.DynamicDraw);
        }


        public void draw()
        {
            shapecol = new(0, 1, 0);
            drawTriangle(0, 0);
            shapecol = new(1, 0, 0);
            drawLine(0,0, mx, my);
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
        void ProcessMouse()
        {
            var m = MouseState;
            mx = (m.X/Size.X-0.5f)/(Mult/2*ShapeMultX)-EyeX;
            my = (-m.Y/Size.Y+0.5f)/(Mult/2*ShapeMultY)-EyeY;
        }

        void ProcessKeyboard()
        {
            var k = KeyboardState;
            var es = EyeSpeed / (Mult * 20f);
            if (k.IsKeyDown(OpenTK.Windowing.GraphicsLibraryFramework.Keys.Escape))
            {
                this.Close();
            }
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
                Title = title + $" FPS: {_FPS}";
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

        public void drawOctogon(float x, float y, float w = 1)
        {

            setUniforms(x, y, w, w);
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

        public void drawLine(float x1, float y1, float x2, float y2)
        {
            setUniforms(x1, y1, x2, y2);

            GL.DrawArrays(PrimitiveType.Lines, LineStart, LineEnd);

        }

        public void drawRect(float x, float y, float w = 1, float h = 1)
        {
            setUniforms(x, y, w, h);

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

        

    }
}
