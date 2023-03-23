using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace LearnOpenTK.Common
{
    // Простой класс, предназначенный для создания шейдеров.
    public class Shader
    {
        public readonly int Handle;

        private readonly Dictionary<string, int> _uniformLocations;

        // Вот как вы создаете простой шейдер.
        // Шейдеры написаны на GLSL, который по своей семантике очень похож на C.
        // Исходный код GLSL компилируется * во время выполнения *, поэтому он может оптимизировать себя для видеокарты, на которой он в данный момент используется.
        // Прокомментированный пример GLSL можно найти в файле shader.vert.
        public Shader(string vertPath, string fragPath)
        {
            // Существует несколько различных типов шейдеров, но единственные два, которые вам нужны для базового рендеринга, - это вершинный и фрагментный шейдеры.
            // Вершинный шейдер отвечает за перемещение по вершинам и загрузку этих данных в фрагментный шейдер.
            // Вершинный шейдер здесь не будет слишком важен, но позже он станет более важным.
            // Шейдер фрагментов отвечает за последующее преобразование вершин в "фрагменты", которые представляют все данные, необходимые OpenGL для рисования пикселя.
            // Фрагментный шейдер - это то, что мы будем использовать здесь чаще всего.

            // Загрузить вершинный шейдер и скомпилировать
            var shaderSource = File.ReadAllText(vertPath);

            // GL.CreateShader создаст пустой шейдер (очевидно). Перечисление ShaderType указывает, какой тип шейдера будет создан.
            var vertexShader = GL.CreateShader(ShaderType.VertexShader);

			// Теперь привяжите исходный код GLSL
            GL.ShaderSource(vertexShader, shaderSource);

            // Затем компиляция
            CompileShader(vertexShader);

            // Мы делаем то же самое для фрагментного шейдера.
            shaderSource = File.ReadAllText(fragPath);
            var fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fragmentShader, shaderSource);
            CompileShader(fragmentShader);

			// Затем эти два шейдера должны быть объединены в шейдерную программу, которая затем может быть использована OpenGL.
            // Для этого создайте программу...
            Handle = GL.CreateProgram();

            // Прикрепите оба шейдера...
            GL.AttachShader(Handle, vertexShader);
            GL.AttachShader(Handle, fragmentShader);

            // А затем свяжите их вместе.
            LinkProgram(Handle);

            // Когда шейдерная программа связана, ей больше не нужны отдельные шейдеры, прикрепленные к ней; скомпилированный код копируется в шейдерную программу.
            // Отсоедините их, а затем удалите.
            GL.DetachShader(Handle, vertexShader);
            GL.DetachShader(Handle, fragmentShader);
            GL.DeleteShader(fragmentShader);
            GL.DeleteShader(vertexShader);

            // Теперь шейдер готов к работе, но сначала мы собираемся кэшировать все однородные местоположения шейдера.
            // Запрос этого из шейдера выполняется очень медленно, поэтому мы делаем это один раз при инициализации и повторно используем эти значения
            // позже.

            // Во-первых, мы должны получить количество активных униформ в шейдере.
            GL.GetProgram(Handle, GetProgramParameterName.ActiveUniforms, out var numberOfUniforms);

            // Далее выделите словарь для хранения местоположений.
            _uniformLocations = new Dictionary<string, int>();

            // Перебирайте всю униформу,
            for (var i = 0; i < numberOfUniforms; i++)
            {
                // получить название этой униформы,
                var key = GL.GetActiveUniform(Handle, i, out _, out _);

                // получить расположение
                var location = GL.GetUniformLocation(Handle, key);

                // а затем добавьте его в словарь.
                _uniformLocations.Add(key, location);
            }
        }

        private static void CompileShader(int shader)
        {
            // Попробуйте скомпилировать шейдер
            GL.CompileShader(shader);

            // Проверка на наличие ошибок компиляции
            GL.GetShader(shader, ShaderParameter.CompileStatus, out var code);
            if (code != (int)All.True)
            {
                // Мы можем использовать `GL.GetShaderInfoLog(шейдер)`, чтобы получить информацию об ошибке.
                var infoLog = GL.GetShaderInfoLog(shader);
                throw new Exception($"Error occurred whilst compiling Shader({shader}).\n\n{infoLog}");
            }
        }

        private static void LinkProgram(int program)
        {
            // Мы связываем программу
            GL.LinkProgram(program);

            // Проверка на наличие ошибок связывания
            GL.GetProgram(program, GetProgramParameterName.LinkStatus, out var code);
            if (code != (int)All.True)
            {
                // Мы можем использовать `GL.GetProgramInfoLog(программа)`, чтобы получить информацию об ошибке.
                throw new Exception($"Error occurred whilst linking Program({program})");
            }
        }

        // Функция-оболочка, которая включает программу шейдеров.
        public void Use()
        {
            GL.UseProgram(Handle);
        }

        // Источники шейдеров, поставляемые с этим проектом, используют жестко закодированные layout(location)-ы. Если вы хотите сделать это динамически,
		// вы можете опустить строки layout(location=X) в вершинном шейдере и использовать это в vertexAttribPointer вместо жестко закодированных значений.
        public int GetAttribLocation(string attribName)
        {
            return GL.GetAttribLocation(Handle, attribName);
        }

        // Установщики униформы
        // Uniforms - это переменные, которые могут быть установлены пользовательским кодом, вместо того, чтобы считывать их из VBO.
        // Вы используете VBO для данных, связанных с вершинами, и униформу почти для всего остального.

        // Настройка униформы почти всегда одинакова, поэтому я объясню это здесь один раз, а не в каждом методе:
        // 1. Привяжите программу, на которую вы хотите установить униформу
        // 2. Получите справку о местонахождении униформы с помощью GL.GetUniformLocation.
        // 3. Используйте соответствующую функцию GL.Uniform* для установки униформы.

        /// <краткое описание>
        /// Установите единообразный int для этого шейдера.
        /// </краткое содержание>
        /// <имя параметра="name">Название униформы</param>
        /// <имя параметра="данные">Данные для установки</параметр>
        public void SetInt(string name, int data)
        {
            GL.UseProgram(Handle);
            GL.Uniform1(_uniformLocations[name], data);
        }

        /// <краткое описание>
        /// Установите равномерное плавающее значение для этого шейдера.
        /// </краткое содержание>
        /// <имя параметра="name">Название униформы</param>
        /// <имя параметра="данные">Данные для установки</параметр>
        public void SetFloat(string name, float data)
        {
            GL.UseProgram(Handle);
            GL.Uniform1(_uniformLocations[name], data);
        }

        /// <краткое описание>
        /// Установите равномерную матрицу для этого шейдера
        /// </краткое содержание>
        /// <имя параметра="name">Название униформы</param>
        /// <имя параметра="данные">Данные для установки</параметр>
        /// <примечания>
        /// <пункт>
        /// Матрица транспонируется перед отправкой в шейдер.
        /// </пункт>
        /// </примечания>
        public void SetMatrix4(string name, Matrix4 data)
        {
            GL.UseProgram(Handle);
            GL.UniformMatrix4(_uniformLocations[name], true, ref data);
        }

        /// <краткое описание>
        /// Установите равномерный Вектор3 для этого шейдера.
        /// </краткое содержание>
        /// <имя параметра="name">Название униформы</param>
        /// <имя параметра="данные">Данные для установки</параметр>
        public void SetVector3(string name, Vector3 data)
        {
            GL.UseProgram(Handle);
            GL.Uniform3(_uniformLocations[name], data);
        }
    }
}
