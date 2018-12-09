using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace piled.client
{
    public interface IDisplayActivity
    {
        Task ExecuteAsync(IRenderer renderer, CancellationToken cancellationToken);
    }
}
