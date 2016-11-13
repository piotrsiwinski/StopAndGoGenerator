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

        private List<long> CountShiftRegisterStates()
        {   
            var outputList = new List<long>();
            foreach (var t in _tapSequence)
            {
               
                outputList.Add(_shiftRegister >> t-1);
            }
            return outputList;
        }
        public int GetElementFromRegister()
        {

            foreach (var t in _tapSequence)
            {
                var result = _shiftRegister >> t-1;
                _shiftRegisterStates.Add(result);
            }
            //_shiftRegisterStates.Add(_shiftRegister);

            var shiftRegisterStatesToXor = CountShiftRegisterStates();

            long xorResult = 0;
            foreach (var t in shiftRegisterStatesToXor)
            {
                xorResult ^= t;
            }
            _shiftRegister = ((xorResult & 0x00000001) << _tapSequence[0]-1) | _shiftRegister>>1;

            var elementsTest = new List<long>();
            elementsTest.Add(_shiftRegister);


            return (int) _shiftRegister;// & 0x00000001;
        }

        public IEnumerable<int> Test()
        {
            var registerLength= _tapSequence[0];
            var result = Math.Pow(2, registerLength);

            for (int i = 0; i < result-1; i++)
            {
                yield return GetElementFromRegister();
            }
        }
    }
}
