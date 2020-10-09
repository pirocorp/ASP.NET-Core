using System.Collections.Generic;

namespace Prototype
{
    public class ColorController
    {
        private Dictionary<string, ColorPrototype> colors;

        public ColorController()
        {
            this.colors = new Dictionary<string, ColorPrototype>();
        }

        public ColorPrototype this[string key]
        {
            get => this.colors[key];
            set => this.colors.Add(key, value);
        }
    }
}