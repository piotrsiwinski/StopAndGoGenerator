using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StopAndGoGenerator;
using XorEncrypt;

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
            var list = new List<long>();
            for (int i = 0; i < 15; i++)
            {
                list.Add(lfsr.GetRandomValueFromRegister());
            }
            var result = list.OrderBy(x => x).ToList();
        }

        [TestMethod]
        public void CanGetAllStates()
        {
            //Lfsr lfsr = new Lfsr(new[] { 4,1 });
            Lfsr lfsr = new Lfsr(new[] { 12, 6, 4, 1});
            var result = lfsr.GetAllPeriodValues().ToList();

        }

        [TestMethod]
        public void TestXor()
        {
            XorEncryption encryption = new XorEncryption();
            encryption.Encrypt(null, null);

        }
    }
}
