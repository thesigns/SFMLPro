using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMLPro {

    public class Polyline : Drawable {

        private List<Vertex> _vertices = new();
        private List<float> _widths = new();
        private VertexArray _triangles = new(PrimitiveType.Triangles);

        public Polyline() {
        }

        public void AddVertex(Vertex vertex, float width = 1) {

            _vertices.Add(vertex);
            _widths.Add(width);
            _triangles.Resize((uint)_vertices.Count * 12);

            if (_vertices.Count < 2) {
                return;
            }

            for (int i = 0; i < _vertices.Count - 1; i++) {

                var x1a = _vertices[i].Position.X;
                var y1a = _vertices[i].Position.Y;
                var x2a = _vertices[i + 1].Position.X;
                var y2a = _vertices[i + 1].Position.Y;
                var dx = x1a - x2a;
                var dy = y1a - y2a;
                var dist = MathF.Sqrt(dx * dx + dy * dy);
                dx /= dist;
                dy /= dist;
                var halfStroke1 = _widths[i] / 2;
                var halfStroke2 = _widths[i + 1] / 2;
                var x1b = x1a + halfStroke1 * dy;
                var y1b = y1a - halfStroke1 * dx;
                var x1c = x1a - halfStroke1 * dy;
                var y1c = y1a + halfStroke1 * dx;
                var x2b = x2a + halfStroke2 * dy;
                var y2b = y2a - halfStroke2 * dx;
                var x2c = x2a - halfStroke2 * dy;
                var y2c = y2a + halfStroke2 * dx;

                var vert1a = new Vertex(new Vector2f(x1a, y1a), _vertices[i].Color);
                var vert1b = new Vertex(new Vector2f(x1b, y1b), _vertices[i].Color);
                var vert1c = new Vertex(new Vector2f(x1c, y1c), _vertices[i].Color);

                var vert2a = new Vertex(new Vector2f(x2a, y2a), _vertices[i + 1].Color);
                var vert2b = new Vertex(new Vector2f(x2b, y2b), _vertices[i + 1].Color);
                var vert2c = new Vertex(new Vector2f(x2c, y2c), _vertices[i + 1].Color);

                _triangles[12 * (uint)i + 0] = vert1b;
                _triangles[12 * (uint)i + 1] = vert2a;
                _triangles[12 * (uint)i + 2] = vert2b;

                _triangles[12 * (uint)i + 3] = vert1b;
                _triangles[12 * (uint)i + 4] = vert1a;
                _triangles[12 * (uint)i + 5] = vert2a;

                _triangles[12 * (uint)i + 6] = vert1a;
                _triangles[12 * (uint)i + 7] = vert2c;
                _triangles[12 * (uint)i + 8] = vert2a;

                _triangles[12 * (uint)i + 9] = vert1a;
                _triangles[12 * (uint)i + 10] = vert1c;
                _triangles[12 * (uint)i + 11] = vert2c;

            }

            List<Vector2f> fixVertex1 = new List<Vector2f>();
            List<Vector2f> fixVertex2 = new List<Vector2f>();

            for (int i = 0; i < _vertices.Count - 1; i++) {

                var x1 = _triangles[12 * ((uint)i + 0) + 0].Position.X;
                var y1 = _triangles[12 * ((uint)i + 0) + 0].Position.Y;
                var x2 = _triangles[12 * ((uint)i + 0) + 2].Position.X;
                var y2 = _triangles[12 * ((uint)i + 0) + 2].Position.Y;
                var x3 = _triangles[12 * ((uint)i + 1) + 0].Position.X;
                var y3 = _triangles[12 * ((uint)i + 1) + 0].Position.Y;
                var x4 = _triangles[12 * ((uint)i + 1) + 2].Position.X;
                var y4 = _triangles[12 * ((uint)i + 1) + 2].Position.Y;

                var x = ((x1 * y2 - y1 * x2) * (x3 - x4) - (x1 - x2) * (x3 * y4 - y3 * x4)) / ((x1 - x2) * (y3 - y4) - (y1 - y2) * (x3 - x4));
                var y = ((x1 * y2 - y1 * x2) * (y3 - y4) - (y1 - y2) * (x3 * y4 - y3 * x4)) / ((x1 - x2) * (y3 - y4) - (y1 - y2) * (x3 - x4));

                fixVertex1.Add(new Vector2f(x, y));

                x1 = _triangles[12 * ((uint)i + 0) + 10].Position.X;
                y1 = _triangles[12 * ((uint)i + 0) + 10].Position.Y;
                x2 = _triangles[12 * ((uint)i + 0) + 11].Position.X;
                y2 = _triangles[12 * ((uint)i + 0) + 11].Position.Y;
                x3 = _triangles[12 * ((uint)i + 1) + 10].Position.X;
                y3 = _triangles[12 * ((uint)i + 1) + 10].Position.Y;
                x4 = _triangles[12 * ((uint)i + 1) + 11].Position.X;
                y4 = _triangles[12 * ((uint)i + 1) + 11].Position.Y;

                x = ((x1 * y2 - y1 * x2) * (x3 - x4) - (x1 - x2) * (x3 * y4 - y3 * x4)) / ((x1 - x2) * (y3 - y4) - (y1 - y2) * (x3 - x4));
                y = ((x1 * y2 - y1 * x2) * (y3 - y4) - (y1 - y2) * (x3 * y4 - y3 * x4)) / ((x1 - x2) * (y3 - y4) - (y1 - y2) * (x3 - x4));

                fixVertex2.Add(new Vector2f(x, y));

            }

            for (int i = 0; i < _vertices.Count - 2; i++) {
                _triangles[12 * ((uint)i + 0) + 2] = new Vertex(fixVertex1[i], _triangles[12 * ((uint)i + 0) + 2].Color);
                _triangles[12 * ((uint)i + 1) + 0] = new Vertex(fixVertex1[i], _triangles[12 * ((uint)i + 1) + 0].Color);
                _triangles[12 * ((uint)i + 1) + 3] = new Vertex(fixVertex1[i], _triangles[12 * ((uint)i + 1) + 3].Color);
                _triangles[12 * ((uint)i + 0) + 7] = new Vertex(fixVertex2[i], _triangles[12 * ((uint)i + 0) + 7].Color);
                _triangles[12 * ((uint)i + 0) + 11] = new Vertex(fixVertex2[i], _triangles[12 * ((uint)i + 0) + 11].Color);
                _triangles[12 * ((uint)i + 1) + 10] = new Vertex(fixVertex2[i], _triangles[12 * ((uint)i + 1) + 10].Color);
            }

        }

        public void Draw(RenderTarget target, RenderStates states) {
            target.Draw(_triangles, states);
        }



    }

}
