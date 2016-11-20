using System.Text;
using DesignPatterns.Chor;

namespace StopAndGoGenerator
{
    public class StopAndGo : HandlerRefType
    {
        public Lfsr FirstLfsr;
        public Lfsr SecondLfsr;
        public Lfsr ThirdLfsr;

        public StopAndGo()
        {
            FirstLfsr = new Lfsr();
            SecondLfsr = new Lfsr();
            ThirdLfsr = new Lfsr();
        }

        public string GenerateRandomValues(int count)
        {
            var stringBuilder = new StringBuilder();
            var firstLfsrResult = FirstLfsr.GetRandomValueFromRegister();
            var secondLfsrResult = SecondLfsr.GetRandomValueFromRegister();
            var thirdLfsrResult = ThirdLfsr.GetRandomValueFromRegister();

            for (int i = 0; i < count; i++)
            {
                if (firstLfsrResult == 1)
                {
                    secondLfsrResult = SecondLfsr.GetRandomValueFromRegister();
                    var result = thirdLfsrResult ^ secondLfsrResult;
                    stringBuilder.Append(result.ToString());
                }
                else
                {
                    thirdLfsrResult = ThirdLfsr.GetRandomValueFromRegister();
                    var result = secondLfsrResult ^ thirdLfsrResult;
                    stringBuilder.Append(result.ToString());
                }
            }


            return stringBuilder.ToString();
        }

        public string GenerateRandomValues(int count, out double progress)
        {
            var stringBuilder = new StringBuilder();
            var firstLfsrResult = FirstLfsr.GetRandomValueFromRegister();
            var secondLfsrResult = SecondLfsr.GetRandomValueFromRegister();
            var thirdLfsrResult = ThirdLfsr.GetRandomValueFromRegister();

            for (int i = 0; i < count; i++)
            {
                if (firstLfsrResult == 1)
                {
                    secondLfsrResult = SecondLfsr.GetRandomValueFromRegister();
                    var result = thirdLfsrResult ^ secondLfsrResult;
                    stringBuilder.Append(result.ToString());
                }
                else
                {
                    thirdLfsrResult = ThirdLfsr.GetRandomValueFromRegister();
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