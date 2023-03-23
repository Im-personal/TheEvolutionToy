using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Windowing.Desktop;
using OpenTK.Mathematics;
using LearnOpenTK.Common;
using OpenTK.Compute.OpenCL;

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


        protected override void OnLoad()
        {
            base.OnLoad();

            GL.ClearColor(0f, 0f, 0f, 1f);
            
            

            _vertexBufferObject = GL.GenBuffer();

            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Length * sizeof(float), _vertices, BufferUsageHint.StaticDraw);

            _vertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(_vertexArrayObject);

            GL.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 2 * sizeof(float));
            GL.EnableVertexAttribArray(1);



            _shader = new Shader("Shaders/shader.vert", "Shaders/shader.frag");
            _shader.Use();

        }




        private int _FPS = 0;
        public double _fpstime = 0;

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit);
            _shader.Use();

            






            GL.BindVertexArray(_vertexArrayObject);

            GL.DrawArrays(PrimitiveType.Points, 0, _vertices.Length / 5);

            SwapBuffers();
            GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Length * sizeof(float), _vertices, BufferUsageHint.DynamicDraw);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
            countFPS(e);

            

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
        }
       

    }
}
