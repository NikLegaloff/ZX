// See https://aka.ms/new-console-template for more information

using ZX.Console.Code;

byte[] code = new byte[]
{
    0b000_00_001, 0b00000000,0b0000_1000, // LD BC,8
    0b00000000, // NOP
    0b00_010_000 , 0b1111_1101, // DJNZ -3
    0b01_110_110 // HALT

};


var memory = new Memory(code);
ZXSpectrum pc = new ZXSpectrum(memory, 10);
pc.Init();
pc.Run(true);
