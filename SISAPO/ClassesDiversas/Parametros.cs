using System;
using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential)]
public struct Parametros
{
    public string Nome;
    public TipoCampo Tipo;
    public object Valor;
    public int Tamanho;
    public Parametros(string Nome, TipoCampo Tipo, object Valor, int Tamanho)
    {
        this.Nome = Nome;
        this.Tipo = Tipo;
        this.Valor = Valor;
        this.Tamanho = Tamanho;
    }

    public Parametros(string Nome, TipoCampo Tipo, object Valor)
    {
        this.Nome = Nome;
        this.Tipo = Tipo;
        this.Valor = Valor;
        this.Tamanho = 0;
    }
}

