using DesignPatterns.Chor;
using DesignPatterns.Chor.Abstract;

namespace StopAndGoGenerator.UI.LfsrLogic
{
    public class FirstLfsrRegisterHandler : IHandler
    {
        private long _shiftRegisterInitState;
        private int[] _tapSeqence;

        public FirstLfsrRegisterHandler(long shiftRegisterInitState, int[] tapSeqence)
        {
            _shiftRegisterInitState = shiftRegisterInitState;
            _tapSeqence = tapSeqence;
        }

        public void Handle(HandlerRefType refType)
        {
            var stopAndGo = (StopAndGo) refType;
            stopAndGo.FirstLfsr = new Lfsr(_tapSeqence, _shiftRegisterInitState);
        }
    }
}