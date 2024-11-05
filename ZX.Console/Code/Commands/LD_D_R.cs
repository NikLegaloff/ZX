namespace ZX.Console.Code.Commands;

public enum DestRes8Code
{
    B_B, B_C, B_D, B_E, B_H, B_L, B_HLm, B_A, 
    C_B, C_C, C_D, C_E, C_H, C_L, C_HLm, C_A, 
    D_B, D_C, D_D, D_E, D_H, D_L, D_HLm, D_A, 
    E_B, E_C, E_D, E_E, E_H, E_L, E_HLm, E_A, 
    H_B, H_C, H_D, H_E, H_H, H_L, H_HLm, H_A, 
    L_B, L_C, L_D, L_E, L_H, L_L, L_HLm, L_A, 
    HLm_B, HLm_C, HLm_D, HLm_E, HLm_H, HLm_L, /*HLm_HLm,*/ HLm_A, 
    A_B, A_C, A_D, A_E, A_H, A_L, A_HLm, A_A
}


public class LD_D_R : Cmd
{
    public override byte[] Range => [
        0b01000000, 0b01000001, 0b01000010, 0b01000011, 0b01000100, 0b01000101, 0b01000110, 0b01000111,
        0b01001000, 0b01001001, 0b01001010, 0b01001011, 0b01001100, 0b01001101, 0b01001110, 0b01001111,
        0b01010000, 0b01010001, 0b01010010, 0b01010011, 0b01010100, 0b01010101, 0b01010110, 0b01010111,
        0b01011000, 0b01011001, 0b01011010, 0b01011011, 0b01011100, 0b01011101, 0b01011110, 0b01011111,
        0b01100000, 0b01100001, 0b01100010, 0b01100011, 0b01100100, 0b01100101, 0b01100110, 0b01100111,
        0b01101000, 0b01101001, 0b01101010, 0b01101011, 0b01101100, 0b01101101, 0b01101110, 0b01101111,
        0b01110000, 0b01110001, 0b01110010, 0b01110011, 0b01110100, 0b01110101, /*0b01110110,*/ 0b01110111,
        0b01111000, 0b01111001, 0b01111010, 0b01111011, 0b01111100, 0b01111101, 0b01111110, 0b01111111
    ];

    private DestRes8Code _code;

    public override void Execute(Z80 cpu)
    {
        var dest = (Reg8Code)(((ushort)_code & 0b00111000)>>3);
        var src = (Reg8Code)((ushort)_code & 0b00000111);
        Set(cpu, dest, Get(cpu, src));
    }

    public override Cmd Init(byte shift) => new LD_D_R{_code = (DestRes8Code)shift};
}