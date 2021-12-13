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


            Textures[0] = Texture.LoadFromFile(shaderPathTextures + "Player1.png");
            Textures[1] = Texture.LoadFromFile(shaderPathTextures + "Player2.png");
            Textures[2] = Texture.LoadFromFile(shaderPathTextures + "Броня.png");
            Textures[3] = Texture.LoadFromFile(shaderPathTextures + "Ветер.png");
            Textures[4] = Texture.LoadFromFile(shaderPathTextures + "Здоровье.png");
            Textures[5] = Texture.LoadFromFile(shaderPathTextures + "Земля.png");
            Textures[6] = Texture.LoadFromFile(shaderPathTextures + "Пули.png");
            Textures[7] = Texture.LoadFromFile(shaderPathTextures + "РадиусСнарядов.png");
            Textures[8] = Texture.LoadFromFile(shaderPathTextures + "СкоростьСнарядов.png");
            Textures[9] = Texture.LoadFromFile(shaderPathTextures + "Топливо.png");
            Textures[10] = Texture.LoadFromFile(shaderPathTextures + "Урон.png");
            Textures[11] = Texture.LoadFromFile(shaderPathTextures + "Player1win.png");
            Textures[12] = Texture.LoadFromFile(shaderPathTextures + "Player2win.png");
        }

        public Sprite GetSprite(int index)
        {
            return new Sprite(Shader, Textures[index]);
        } 
    }
}
