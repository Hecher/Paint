using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Paint.elements.classes
{
    class EraserTool : ITool
    {
        private bool _isErasing = false;

        public void OnMouseDown(Point position, Canvas canvas)
        {
            _isErasing = true;
            EraserAt(position, canvas);
        }

        public void OnMouseMove(Point position, Canvas canvas)
        {
            if (_isErasing)
            {
                EraserAt(position, canvas);
            }
        }

        public void OnMouseUp(Point position, Canvas canvas)
        {
            _isErasing = false;
        }

        private void EraserAt(Point position, Canvas canvas)
        {
            HitTestResult result = VisualTreeHelper.HitTest(canvas, position);

            if (result != null && result.VisualHit is Polyline polylineToErase)
            {
                canvas.Children.Remove(polylineToErase);
            }
        }
    }
}
