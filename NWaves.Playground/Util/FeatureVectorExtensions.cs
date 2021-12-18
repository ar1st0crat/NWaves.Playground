using System.Collections.Generic;
using System.Linq;
using NWaves.FeatureExtractors.Base;
using NWaves.Utils;

namespace NWaves.Playground.Util
{
    public static class FeatureVectorExtensions
    {
        /// <summary>
        /// Creates list of feature vectors from initial vectors
        /// where each vector is prepended with time position (in seconds).
        /// </summary>
        public static List<float[]> WithTimeMarkers(this List<float[]> vectors, FeatureExtractor extractor)
        {
            var timeMarkers = extractor.TimeMarkers(vectors.Count);

            return timeMarkers.Zip(vectors, (t, v) =>
            {
                var f = new float[v.Length + 1];
                f[0] = (float)t;
                v.FastCopyTo(f, v.Length, 0, 1);
                return f;
            })
            .ToList();
        }
    }
}
