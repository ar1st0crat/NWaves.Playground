using NWaves.Utils;
using System;
using System.Linq;

namespace NWaves.Playground.Util
{
    /// <summary>
    /// Provides methods for full compliance with python_speech_features.
    /// </summary>
    public static class PythonSpeechFeatures
    {
        /// <summary>
        /// Generates filterbank with weights identical to python_speech_features.
        /// </summary>
        public static float[][] FilterBank(int samplingRate, int filterbankSize, int fftSize, double lowFreq = 0, double highFreq = 0)
        {
            var filterbank = new float[filterbankSize][];

            if (highFreq <= lowFreq)
            {
                highFreq = samplingRate / 2;
            }

            var low = Scale.HerzToMel(lowFreq);
            var high = Scale.HerzToMel(highFreq);

            var res = (fftSize + 1) / (float)samplingRate;

            var bins = Enumerable
                          .Range(0, filterbankSize + 2)
                          .Select(i => (float)Math.Floor(res * Scale.MelToHerz(low + i * (high - low) / (filterbankSize + 1))))
                          .ToArray();

            for (var i = 0; i < filterbankSize; i++)
            {
                filterbank[i] = new float[fftSize / 2 + 1];

                for (var j = (int)bins[i]; j < (int)bins[i + 1]; j++)
                {
                    filterbank[i][j] = (j - bins[i]) / (bins[i + 1] - bins[i]);
                }
                for (var j = (int)bins[i + 1]; j < (int)bins[i + 2]; j++)
                {
                    filterbank[i][j] = (bins[i + 2] - j) / (bins[i + 2] - bins[i + 1]);
                }
            }

            return filterbank;
        }
    }
}
