using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StopAndGoGenerator
{
    public class Lfsr
    {
        private long _shiftRegister;
        private int[] _tapSequence;
        private List<long> _shiftRegisterStates;

        public Lfsr()
        {
            _shiftRegister = 1;
            _shiftRegisterStates = new List<long>();
        }

        public Lfsr(int[] tapSeqence) : this()
        {
            _tapSequence = tapSeqence;
        }

        public Lfsr(int[] tapSeqence, long shiftRegister) : this()
        {
            _tapSequence = tapSeqence;
            _shiftRegister = shiftRegister;
            _shiftRegisterStates = new List<long>(tapSeqence.Length);
        }

        private IEnumerable<long> CountShiftRegisterStates()
        {
            return _tapSequence.Select(t => _shiftRegister >> t - 1).ToList();
        }

        public long GetRandomValueFromRegister()
        {
            var shiftRegisterStatesToXor = CountShiftRegisterStates();
            long xorResult = shiftRegisterStatesToXor.Aggregate<long, long>(0, (current, t) => current ^ t);
            _shiftRegister = ((xorResult & 0x00000001) << _tapSequence[0]-1) | _shiftRegister>>1;
            return _shiftRegister & 0x00000001;
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
