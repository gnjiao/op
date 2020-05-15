using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Software.Device.SimensPLC
{
    public class BitUtl
    {
        public void ConvertMAddress(int MAddress, out int word, int bit)
        {
            bit = MAddress % 16;
            word = MAddress - bit;
        }
        public void SetBit(ref int number, int index, bool bitOn)
        {
            int mask = 0;
            if (bitOn)
            {
                mask = 1 << index;
                number = number | mask;
            }
            else
            {
                mask = 0xffff ^ 1 << index;
                number = number & mask;
            }
        }
        public bool IsBitOn(int number, int index)
        {
            int mask = 1 << index;
            return (number & mask) > 0;
        }
    }
}
