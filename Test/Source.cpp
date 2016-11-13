#include <iostream>
using namespace std;

int LFSR()
{
	static unsigned long ShiftRegister = 1;
	auto test = ShiftRegister >> 31;

	ShiftRegister =
		((ShiftRegister >> 31 
			^ ShiftRegister >> 6 
			^ ShiftRegister >> 4 
			^ ShiftRegister >> 2 
			^ ShiftRegister >> 1 
			^ ShiftRegister) & 0x00000001) << 31 | (ShiftRegister >> 1);
	return ShiftRegister & 0x00000001;
}

int test()
{
	static unsigned long Register = 1;
	Register =
		((((Register) >> 3)
			^ (Register >> 1)
			^ Register)) & 0x0001) << 3) | (Register >> 1);
}

int main()
{
	auto result = LFSR();
	cout << result << endl;

	getchar(); getchar();
	return 0;
}