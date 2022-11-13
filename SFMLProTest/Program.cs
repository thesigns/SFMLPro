using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFMLPro;
using SFML.Graphics;
using SFML.System;
using SFML.Window;


namespace SFMLProTest {
    internal class Program {

        private static void Main(string[] args) {

            VideoMode mode = new VideoMode(VideoMode.DesktopMode.Width / 2, VideoMode.DesktopMode.Height / 2);
            ContextSettings contextSettings = new ContextSettings();
            contextSettings.AntialiasingLevel = 8;
            RenderWindow window = new RenderWindow(mode, "SFMLPro Test", Styles.Default, contextSettings);
            window.SetVerticalSyncEnabled(true);

            window.Closed += (sender, args) => window.Close();

            Polyline line = new Polyline();
            line.StrokeWidth = 5.5f;

            line.AddVertex(new Vertex(new Vector2f(100, 44), Color.Cyan), 1);
            line.AddVertex(new Vertex(new Vector2f(33, 100), Color.Red), 2);
            line.AddVertex(new Vertex(new Vector2f(200, 78), Color.Yellow), 3);
            line.AddVertex(new Vertex(new Vector2f(100, 200), Color.Green), 4);
            line.AddVertex(new Vertex(new Vector2f(400, 400), Color.White), 8);

            while (window.IsOpen) {
                window.DispatchEvents();
                window.Clear(Color.Black);

                window.Draw(line);

                window.Display();

                if (Keyboard.IsKeyPressed(Keyboard.Key.Escape)) {
                    window.Close();
                }




            }

        }

    }
}
