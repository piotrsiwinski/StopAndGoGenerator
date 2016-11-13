using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StopAndGoGenerator;

namespace Generator.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {

            long rejestr = 1;
            string wyniklfsr = "";
            rejestr = ((((rejestr >> 83) ^ (rejestr >> 7) ^ (rejestr >> 6) ^ (rejestr >> 4) ^ (rejestr >> 2) ^ (rejestr)) & 0x00000001) << 83) | (rejestr >> 1);
            wyniklfsr += (rejestr & 0x00000001).ToString();
        }

        [TestMethod]
        public void TestMethod2()
        {
            long rejestr = 1;
            long[] wyniklfsr = new long[16];
            long[] wyniklfsrBits = new long[16];
            for (int i = 0; i < 16; i++)
            {
                rejestr = ((((rejestr >> 3) ^ (rejestr)) & 0x0001) << 3) | (rejestr >> 1);
                wyniklfsr[i] = rejestr;
                var res = wyniklfsr.OrderBy(x => x).ToList();
                wyniklfsrBits[i] += (rejestr & 0x00000001);
            }
            
        }

        [TestMethod]
        public void TestMethod3()
        {
            Lfsr lfsr = new Lfsr(new []{4,1});
            var list = new List<int>();
            for (int i = 0; i < 15; i++)
            {
                list.Add(lfsr.GetElementFromRegister());
            }
            var result = list.OrderBy(x => x).ToList();
        }
    }
}
