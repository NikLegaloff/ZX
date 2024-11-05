// See https://aka.ms/new-console-template for more information

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
c.Add(new Nop());
c.Add(new LD_A_NNm(),null,null,5);
c.Add(new DEC_R(), (byte)Reg8Code.A);
c.Add(new DEC_R(), (byte)Reg8Code.A);
c.Add(new DEC_R(), (byte)Reg8Code.A);
c.Add(new DEC_R(), (byte)Reg8Code.A);
c.Add(new DEC_R(), (byte)Reg8Code.A);
c.Add(new LD_RR_NN(), (byte)Reg16Code.BC,null,0b00001111_00000000);
c.Add(new Nop());
c.Add(new DEC_R(), (byte)Reg8Code.A);
c.Add(new DJNZ(), null,0b1111_1100);
c.Add(new Halt());



Memory memory = new Memory(c.Compile());
ZXSpectrum pc = new ZXSpectrum(memory, 25,true);
pc.Init();
pc.Run();
