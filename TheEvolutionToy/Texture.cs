using OpenTK.Graphics.OpenGL4;
using System.Drawing;
using System.Drawing.Imaging;
using PixelFormat = OpenTK.Graphics.OpenGL4.PixelFormat;
using StbImageSharp;
using System.IO;

namespace LearnOpenTK.Common
{
    // Вспомогательный класс, очень похожий на Shader, предназначенный для упрощения загрузки текстур.
    public class Texture
    {
        public readonly int Handle;

        public static Texture LoadFromFile(string path)
        {
            // Создать дескриптор
            int handle = GL.GenTexture();

            // Привязать дескриптор
            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, handle);

			// Для этого примера мы собираемся использовать .Встроенная система NET.Библиотека рисования для загрузки текстур.

            // OpenGL имеет источник текстуры в левом нижнем углу вместо верхнего левого угла,
            // // итак, мы говорим Stb Image Sharp переворачивать изображение при загрузке.
            StbImage.stbi_set_flip_vertically_on_load(1);
            
            // // Здесь мы открываем поток в файл и передаем его в Stb Image Sharp для загрузки.
            using (Stream stream = File.OpenRead(path))
            {
                ImageResult image = ImageResult.FromStream(stream, ColorComponents.RedGreenBlueAlpha);

				// Теперь, когда наши пиксели готовы, пришло время создать текстуру. Мы делаем это с помощью GL.texImage2D.
                // Аргументы:
                // Тип текстуры, которую мы создаем. Существуют различные типы текстур, но единственная, которая нам сейчас нужна, - это Texture2D.
                // Уровень детализации. Мы можем использовать это, чтобы начать с меньшего mipmap (если захотим), но нам не нужно этого делать, поэтому оставьте его равным 0.
                // Целевой формат пикселей. Это формат, в котором OpenGL будет хранить наше изображение.
                // Ширина изображения
                // Высота изображения.
                // Граница изображения. Это всегда должно быть равно 0; это устаревший параметр, от которого Хронос так и не избавился.
                // Формат пикселей, описанный выше. Поскольку ранее мы загрузили пиксели как ARGB, нам нужно использовать BGRA.
                // Тип данных пикселей.
                // И, наконец, фактические пиксели.
                GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, image.Width, image.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, image.Data);
            }
            
			// Теперь, когда наша текстура загружена, мы можем установить несколько настроек, влияющих на то, как изображение будет выглядеть при рендеринге.

            // // Сначала мы устанавливаем минимальный и максимальный фильтр. Они используются для уменьшения и увеличения масштаба текстуры соответственно.
            // Здесь мы используем линейный для обоих. Это означает, что OpenGL попытается смешать пиксели, а это означает, что текстуры, масштабированные слишком сильно, будут выглядеть размытыми.
            // Вы также можете использовать (среди других вариантов) Ближайший, который просто захватывает ближайший пиксель, из-за чего текстура выглядит пикселизированной, если масштабировать ее слишком сильно.
            // // ПРИМЕЧАНИЕ: Настройки по умолчанию для обоих из них - Линейная Mipmap. Если вы оставите их по умолчанию, но не создадите mipmaps,
			// ваше изображение вообще не будет отображаться (обычно вместо этого получается чистый черный цвет).
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

            // Теперь установите режим обертывания. S - для оси X, а T - для оси Y.
            // Мы устанавливаем для этого значение Repeat, чтобы текстуры повторялись при обертывании. Здесь не показано, так как координаты текстуры точно совпадают
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);

            // Далее создайте mip-карты.
            // Mipmaps - это уменьшенные копии текстуры в уменьшенном масштабе. Каждый уровень mipmap в два раза меньше предыдущего
			// Сгенерированные mip-карты уменьшаются всего до одного пикселя.
            // OpenGL автоматически переключается между mip-картами, когда объект находится достаточно далеко.
            // Это предотвращает муаровые эффекты, а также экономит пропускную способность текстуры.
            // Здесь вы можете увидеть и прочитать об эффекте Морье https://en.wikipedia.org/wiki/Moir%C3%A9_pattern
            // Вот пример mips в действии https://en.wikipedia.org/wiki/File:Mipmap_Aliasing_Comparison.png
            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);

            return new Texture(handle);
        }

        public Texture(int glHandle)
        {
            Handle = glHandle;
        }

        // Активировать текстуру
        // Можно связать несколько текстур, если вашему шейдеру нужно больше, чем одна.
        // Если вы хотите это сделать, используйте GL.ActiveTexture, чтобы установить, к какому слоту привязывается GL.bindTexture.
        // Стандарт OpenGL требует, чтобы их было не менее 16, но в зависимости от вашей видеокарты их может быть больше.
        public void Use(TextureUnit unit)
        {
            GL.ActiveTexture(unit);
            GL.BindTexture(TextureTarget.Texture2D, Handle);
        }
    }
}
