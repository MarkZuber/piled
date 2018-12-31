using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace piled.client
{
    public abstract class AbstractDisplayActivity : IDisplayActivity
    {
        protected AbstractDisplayActivity()
        {

        }

        protected RgbCanvas Canvas { get; } = new RgbCanvas();

        protected abstract Task ExecuteAsync(CancellationToken cancellationToken);
        private CancellationToken _cancellationToken;
        private IRenderer _renderer;

        protected void Render()
        {
            _renderer.Render(Canvas);
        }

        public async Task ExecuteAsync(IRenderer renderer, CancellationToken cancellationToken)
        {
            _renderer = renderer;
            _cancellationToken = cancellationToken;

            try
            {
                await ExecuteAsync(_cancellationToken);
            }
            catch (TaskCanceledException)
            {
            }
            finally
            {
                Canvas.Fill(RgbColor.Black);
                Render();
                
                // Give the network a bit of time to flush the packet before the process exits.
                await Task.Delay(300);
            }
        }

        protected void CheckForExit()
        {
            if (_cancellationToken.IsCancellationRequested)
            {
                throw new TaskCanceledException();
            }
        }
    }
}
