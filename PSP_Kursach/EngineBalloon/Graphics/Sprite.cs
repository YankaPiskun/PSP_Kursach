using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace EngineBalloon.Graphics
{
    public class Sprite
    {
        private readonly float[] _vertices;

        private readonly uint[] _indices;

        private int _elementBufferObject;

        private int _vertexBufferObject;

        private int _vertexArrayObject;

        private Shader _shader;

        private Texture _texture;

        public Texture Texture => _texture; 

        public Sprite(Shader shader, Texture texture)
        {
            _shader = shader;
            _texture = texture;
            _indices =  new uint[] { 0, 1, 3, 1, 2, 3};
            _vertices = new float[]
            {
                 1.0f,  1.0f, 1.0f, 1.0f, 
                 1.0f, -1.0f, 1.0f, 0.0f, 
                -1.0f, -1.0f, 0.0f, 0.0f, 
                -1.0f,  1.0f, 0.0f, 1.0f
            };

            _vertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(_vertexArrayObject);

            _vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Length * sizeof(float), _vertices, BufferUsageHint.StaticDraw);

            _elementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, _elementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, _indices.Length * sizeof(uint), _indices, BufferUsageHint.StaticDraw);
        }

        public void Draw(Vector2 position, Vector2 scale, float angle, bool flipY)
        {
            var vertexLocation = _shader.GetAttribLocation("aPosition");
            GL.EnableVertexAttribArray(vertexLocation);
            GL.VertexAttribPointer(vertexLocation, 2, VertexAttribPointerType.Float, false, 4 * sizeof(float), 0);

            var texCoordLocation = _shader.GetAttribLocation("aTexCoord");
            GL.EnableVertexAttribArray(texCoordLocation);
            GL.VertexAttribPointer(texCoordLocation, 2, VertexAttribPointerType.Float, false, 4 * sizeof(float), 2 * sizeof(float));

            GL.BindVertexArray(_vertexArrayObject);

            var transform = Matrix4.Identity;

            transform *= Matrix4.CreateRotationZ(angle);

            transform *= Matrix4.CreateScale(scale.X * (flipY ? -1f : 1f), scale.Y, 1);

            transform *= Matrix4.CreateTranslation(position.X, position.Y, 0);

            _texture.Use(TextureUnit.Texture0);

            _shader.SetMatrix4("transform", transform);

            GL.DrawElements(PrimitiveType.Triangles, _indices.Length, DrawElementsType.UnsignedInt, 0);
        }
    }
}
