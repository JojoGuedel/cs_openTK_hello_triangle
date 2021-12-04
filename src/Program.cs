using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

public class Programm {
    public static void Main(string[] args) {
        using(Window window = new Window()) {
            window.Size = new Vector2i(500, 500);
            window.Run();
        }
    }
}

public class Window : GameWindow
{
    float[] Vertices = new float[] {
        -0.5f, -0.5f, 0.0f,
         0.5f, -0.5f, 0.0f,
         0.0f,  0.5f, 0.0f
    };

    public Window() : base(GameWindowSettings.Default, NativeWindowSettings.Default)
    {
    }

    protected override void OnLoad()
    {
        int programID = GL.CreateProgram();

        int vertID = GL.CreateShader(ShaderType.VertexShader);
        GL.ShaderSource(vertID, File.ReadAllText("src/shaders/vert.glsl"));
        GL.CompileShader(vertID);
        GL.AttachShader(programID, vertID);

        int fragID = GL.CreateShader(ShaderType.FragmentShader);
        GL.ShaderSource(fragID, File.ReadAllText("src/shaders/frag.glsl"));
        GL.CompileShader(fragID);
        GL.AttachShader(programID, fragID);

        GL.LinkProgram(programID);
        GL.UseProgram(programID);

        int vertexBufferID = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ArrayBuffer,  vertexBufferID);
        GL.BufferData(BufferTarget.ArrayBuffer, Vertices.Length * sizeof(float), Vertices, BufferUsageHint.StaticDraw);

        int vertexArrayID = GL.GenVertexArray();
        GL.BindVertexArray(vertexArrayID);
        GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
        GL.EnableVertexAttribArray(0);

        GL.ClearColor(0.2f, 0.2f, 0.3f, 1.0f);
        GL.Clear(ClearBufferMask.ColorBufferBit);
        GL.BindVertexArray(vertexArrayID);
        GL.DrawArrays(PrimitiveType.Triangles, 0, Vertices.Length / 3);
        SwapBuffers();
    }

    protected override void OnUpdateFrame(FrameEventArgs args)
    {
    }

    protected override void OnRenderFrame(FrameEventArgs args)
    {
    }

    protected override void OnResize(ResizeEventArgs e)
    {
        GL.Viewport(0, 0, e.Size.X, e.Size.Y);
    }
}