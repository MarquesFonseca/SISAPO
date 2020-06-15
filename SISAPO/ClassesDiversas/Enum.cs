using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SISAPO.ClassesDiversas
{
    class Enum
    {
    }

    /// <summary>
    /// Define os estados do formulário para ajustes de acordo com a operação e o estado em que o mesmo se encontra
    /// </summary>
    public enum EstadoForm { Editando, Adicionando, Excluindo, Limpo, Preenchido, Erro }

    /// <summary>
    /// Usado para definir qual o tipo da informação. (Numerico, Decimal, Texto, Data, CPF, CNPJ, IE, CEP, Telefone)
    /// </summary>
    public enum TipoInformacao { Numerico, Decimal, Texto, Data, CPF, CNPJ, IE, CEP, Telefone, Dinheiro }

    /// <summary>
    /// Tipo da chave que será gerada.
    /// </summary>
    public enum TipoChave { Composta, Normal, Concatenada }
}
