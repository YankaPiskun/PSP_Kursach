using OpenTK.Graphics.OpenGL4;

namespace EngineBalloon.Graphics
{
    internal class GraphicController
    {
        public Shader Shader { get; set; }

        public Texture[] Textures { get; set; }

        public GraphicController(int texturesCount, string shaderPathShaders, string shaderPathTextures)
        {
            Textures = new Texture[texturesCount];

            Shader = new Shader(shaderPathShaders + "shader.vert", shaderPathShaders + "shader.frag");
            Shader.Use();


            Textures[0] = Texture.LoadFromFile(shaderPathTextures + "Test.png");
            Textures[0].Use(TextureUnit.Texture0);
        }
    }
}
