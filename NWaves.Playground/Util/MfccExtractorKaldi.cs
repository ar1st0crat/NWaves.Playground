using NWaves.FeatureExtractors;
using NWaves.FeatureExtractors.Options;
using NWaves.Windows;
using System.Linq;

namespace NWaves.Playground.Util
{
    /// <summary>
    /// Extracts MFCC identically to Kaldi with remove_dc_offset=True.
    /// </summary>
    public class MfccExtractorKaldi : MfccExtractor
    {
        private readonly new float[] _windowSamples;
        private readonly float _pre;

        public MfccExtractorKaldi(MfccOptions options, WindowType window) : base(options)
        {
            _windowSamples = Window.OfType(window, FrameSize);
            _preEmphasis = 0;
            _pre = (float)options.PreEmphasis;
        }

        public override void ProcessFrame(float[] block, float[] features)
        {
            var mean = block.Take(FrameSize).Average();

            for (var i = 0; i < FrameSize; i++)
            {
                block[i] -= mean;
            }

            for (var k = FrameSize - 1; k >= 1; k--)
            {
                block[k] -= block[k - 1] * _pre;
            }
            block[0] *= 1 - _pre;

            block.ApplyWindow(_windowSamples);

            base.ProcessFrame(block, features);
        }
    }
}
