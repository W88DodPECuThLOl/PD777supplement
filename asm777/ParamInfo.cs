namespace asm777
{
	internal class ParamInfo
	{
        /// <summary>
        /// マスク
        /// </summary>
        public int mask;
        /// <summary>
        /// シフト
        /// </summary>
        public int shift;
        public ParamInfo(int InParamMask, int InParamShift)
        {
            mask = InParamMask;
            shift = InParamShift;
        }
	}
}
