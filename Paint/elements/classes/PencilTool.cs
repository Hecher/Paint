using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Paint
{
    public class PencilTool : ITool
    {
        private Polyline _currentLine;
        private bool _isDrawing = false;

        public void OnMouseDown(Point position, Canvas canvas)
        {
            _isDrawing = true;

            _currentLine = new Polyline
            {
                Stroke = Brushes.Black,
                StrokeThickness = 3,
                StrokeLineJoin = PenLineJoin.Round,
                StrokeStartLineCap = PenLineCap.Round,
                StrokeEndLineCap = PenLineCap.Round
            };

            _currentLine.Points.Add(position);
            canvas.Children.Add(_currentLine);
        }

        public void OnMouseMove(Point position, Canvas canvas)
        {
            if (_isDrawing && _currentLine != null)
            {
                _currentLine.Points.Add(position);
            }

        }

        public void OnMouseUp(Point position, Canvas canvas)
        {
            _isDrawing = false;
            _currentLine = null;
        }
    }
}
