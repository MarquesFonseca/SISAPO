using System;
using System.IO;
using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential)]
public struct AnexoStream
{
    public string NomeAnexo;
    public Stream StreamArquivo;
}

