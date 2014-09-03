using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AtualizadorCartao
{
    class Assinatura
    {
        private DateTime dtAssnatura;

        public DateTime DtAssnatura
        {
            get { return dtAssnatura; }
            set { dtAssnatura = value; }
        }
        private String nrCartao;

        public String NrCartao
        {
            get { return nrCartao; }
            set { nrCartao = value; }
        }
        private byte[] biAssinatura;

        public byte[] BiAssinatura
        {
            get { return biAssinatura; }
            set { biAssinatura = value; }
        }

        private int idAssinatura;

        public int IdAssinatura
        {
            get { return idAssinatura; }
            set { idAssinatura = value; }
        }

        private int nrTamanhoImagem;

        public int NrTamanhoImagem
        {
            get { return nrTamanhoImagem; }
            set { nrTamanhoImagem = value; }
        }

    }
}
