// See https://aka.ms/new-console-template for more information

using SFML.Graphics;
using SFML.System;
using SFML.Window;
using ZX.Console.Code;
using ZX.Console.Code.Commands;

byte[] code = new byte[]
{
    0b000_00_001, 0b00000000,0b0000_1000, // LD BC,8
    0b00000000, // NOP
    0b00_010_000 , 0b1111_1101, // DJNZ -3
    0b01_110_110 // HALT
};


var c = new Z80Compiler();
c.Add(new LD_R_N(),(byte)Reg8Code.A,0b00101100);
for(int i = 0; i < 800;i++) c.Add(new RLRCA(), (byte)RotationType.RLA);
c.Add(new Halt());

Memory memory = new Memory(c.Compile());
memory.LoadFile("D:\\Work\\ZX\\FinalMatrixThe.scr",16384);
ZXSpectrum pc = new ZXSpectrum(memory, 10,true);
pc.Init();

