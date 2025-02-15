using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;

namespace Lab_1_Mobile
{
    public class RhombusView : GraphicsView
    {
        public RhombusView()
        {
            Drawable = new RhombusDrawable();
        }
    }

    public class RhombusDrawable : IDrawable
    {
        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            float width = dirtyRect.Width;
            float height = dirtyRect.Height;
            float centerX = width / 2;
            float centerY = height / 2;

            // Визначення точок ромба
            PathF path = new PathF();
            path.MoveTo(centerX, centerY - height * 0.4f); // Верхня вершина
            path.LineTo(centerX + width * 0.5f, centerY);   // Права вершина
            path.LineTo(centerX, centerY + height * 0.4f); // Нижня вершина
            path.LineTo(centerX - width * 0.5f, centerY);    // Ліва вершина
            path.Close();

            // Створення градієнтної заливки
            var gradientPaint = new LinearGradientPaint
            {
                GradientStops = new[]
                {
                new PaintGradientStop(0, Colors.Purple),
                new PaintGradientStop(0.5f, Colors.MediumPurple),
                new PaintGradientStop(1, Colors.Lavender)
            },
                StartPoint = new Point(0, 0),
                EndPoint = new Point(1, 1)
            };

            // Малювання заливки
            canvas.SetFillPaint(gradientPaint, dirtyRect);
            canvas.FillPath(path);
        }
    }

}

