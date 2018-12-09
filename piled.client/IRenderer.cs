using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace piled.client
{
    public interface IRenderer
    {
        void Render(RgbCanvas rgbCanvas);
    }
}
