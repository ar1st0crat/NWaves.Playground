namespace NWaves.Playground.Util
{
    static class MfccTemplateStringExtensions
    {
        /// <summary>
        /// Replaces suffix 'Size' to 'Duration' if necessary.
        /// </summary>
        public static string PostProcess(this string s, bool inSeconds)
        {
            return inSeconds ? s.Replace("FrameSize", "FrameDuration")
                                .Replace("HopSize", "HopDuration")
                             : s;
        }

        /// <summary>
        /// Adds correction of MFCC #0 for compliance with python_speech_features.
        /// </summary>
        public static string AddPsfCorrection(this string s, bool appendEnergy = false)
        {
            var psfCorrection = appendEnergy ?
                "\nfor (var i = 0; i < mfccs.Count; i++)\n{\n" +
                "    mfccs[i][0] -= (float)(Math.Log(2));\n}\n"
                :
                "\nfor (var i = 0; i < mfccs.Count; i++)\n{\n" +
                "    mfccs[i][0] -= (float)(Math.Log(2) * Math.Sqrt(options.FilterBank.Length));\n}\n";

            return s + psfCorrection;
        }
    }
}
