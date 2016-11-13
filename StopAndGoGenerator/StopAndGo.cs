using System.Text;

namespace StopAndGoGenerator
{
    public class StopAndGo
    {
        private Lfsr _firstLfsr;
        private Lfsr _secondLfsr;
        private Lfsr _thirdLfsr;

        public StopAndGo()
        {
            _firstLfsr = new Lfsr(new []{32, 7, 5,3,2,1});
            _secondLfsr = new Lfsr(new [] {34,7,6,5,2,1});
            _thirdLfsr = new Lfsr(new [] {36, 6,5,4,2,1});
        }

        public string GenerateRandomValues(int count)
        {
            var stringBuilder = new StringBuilder();
            var firstLfsrResult = _firstLfsr.GetRandomValueFromRegister();
            var secondLfsrResult = _secondLfsr.GetRandomValueFromRegister();
            var thirdLfsrResult = _thirdLfsr.GetRandomValueFromRegister();

            for (int i = 0; i < count; i++)
            {
                if (firstLfsrResult == 1)
                {
                    secondLfsrResult = _secondLfsr.GetRandomValueFromRegister();
                    var result = thirdLfsrResult ^ secondLfsrResult;
                    stringBuilder.Append(result.ToString());
                }
                else
                {
                    thirdLfsrResult = _thirdLfsr.GetRandomValueFromRegister();
                    var result = secondLfsrResult ^ thirdLfsrResult;
                    stringBuilder.Append(result.ToString());
                }
            }
            

            return stringBuilder.ToString();
        }

        public string GenerateRandomValues(int count, out double progress)
        {
            var stringBuilder = new StringBuilder();
            var firstLfsrResult = _firstLfsr.GetRandomValueFromRegister();
            var secondLfsrResult = _secondLfsr.GetRandomValueFromRegister();
            var thirdLfsrResult = _thirdLfsr.GetRandomValueFromRegister();

            for (int i = 0; i < count; i++)
            {
                if (firstLfsrResult == 1)
                {
                    secondLfsrResult = _secondLfsr.GetRandomValueFromRegister();
                    var result = thirdLfsrResult ^ secondLfsrResult;
                    stringBuilder.Append(result.ToString());
                }
                else
                {
                    thirdLfsrResult = _thirdLfsr.GetRandomValueFromRegister();
                    var result = secondLfsrResult ^ thirdLfsrResult;
                    stringBuilder.Append(result.ToString());
                }
                progress = (double) i/count * 100;
            }

            progress = 100;
            return stringBuilder.ToString();
        }

    }
}