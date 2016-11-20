using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StopAndGoGenerator
{
    public class Lfsr
    {
        private long _shiftRegisterInitState;
        private readonly int[] _tapSequence;
        private List<long> _shiftRegisterStates;

        public Lfsr()
        {
            _shiftRegisterInitState = 1;
            _shiftRegisterStates = new List<long>();
        }

        public Lfsr(int[] tapSeqence) : this()
        {
            _tapSequence = tapSeqence;
        }

        public Lfsr(int[] tapSeqence, long shiftRegisterInitState) : this()
        {
            _tapSequence = tapSeqence;
            _shiftRegisterInitState = shiftRegisterInitState;
            _shiftRegisterStates = new List<long>(tapSeqence.Length);
        }

        private IEnumerable<long> CountShiftRegisterStates()
        {
            return _tapSequence.Select(t => _shiftRegisterInitState >> t - 1).ToList();
        }

        public long GetRandomValueFromRegister()
        {
            var shiftRegisterStatesToXor = CountShiftRegisterStates();
            long xorResult = shiftRegisterStatesToXor.Aggregate<long, long>(0, (current, t) => current ^ t);
            _shiftRegisterInitState = ((xorResult & 0x00000001) << _tapSequence[0]-1) | _shiftRegisterInitState>>1;
            return _shiftRegisterInitState & 0x00000001;
        }

        public IEnumerable<long> GetAllPeriodValues()
        {
            var registerLength= _tapSequence[0];
            var result = Math.Pow(2, registerLength);

            for (int i = 0; i < 2* result-1; i++)
            {
                yield return GetRandomValueFromRegister();
            }
        }
    }
}
