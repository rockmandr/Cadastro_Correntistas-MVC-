using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Exe003MVC.Models
{
    public class Correntista
    {
        public string Nome { get; set; }


        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Data de Nascimento é obrigatório")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Data de Nascimento")]
        public DateTime DataNascimento { get; set; }


        public string Cpf { get; set; }
        public int Rg { get; set; }

        //Dados de endereço
        public int Cep { get; set; }
        public string Rua { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Pais { get; set; }

        //Dados financeiros
        public float Salario { get; set; }
        public float Patrimonio { get; set; }

        //Dados empregador
        public string EmpresaEmQueTrabalha { get; set; }
        public int TempoNoEmprego { get; set; }
        public string Cargo { get; set; }

        //Conta do Correntista
        public string NumeroDaConta { get; set; }
        //public float s { get; set; }

        public bool ValidarIdade()
        {
            TimeSpan tempo = (DateTime.Now - DataNascimento);
            var idade = tempo.TotalDays / 365;

            return idade >= 16;
        }

        public bool ValidaCpf(string cpf)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;

            int soma;
            int resto;

            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");

            if (cpf.Length != 11)
            {
                return false;
            }
            tempCpf = cpf.Substring(0, 9);

            soma = 0;

            for (int i = 0; i < 9; i++)
            {
                soma += int.Parse(tempCpf[i].ToString()) * (multiplicador1[i]);
            }
            resto = soma % 11;

            if (resto < 2)
            {
                resto = 0;
            }
            else
            {
                resto = 11 - resto;
            }

            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            int soma2 = 0;

            for (int i = 0; i < 10; i++)
            {
                soma2 += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            }

            resto = soma2 % 11;

            if (resto < 2)
            {
                resto = 0;
            }
            else
            {
                resto = 11 - resto;
            }

            digito = digito + resto.ToString();
            return cpf.EndsWith(digito);
        }

        public bool ValidaPais()
        {
            if (Pais != "Brasil" && Pais != "Brazil")
            {
                return false;
            }

            return true;
        }
        public string CodigoCorrentista(string codi)
        {
            var hoje = DateTime.Now;
            string hoje_str = hoje.ToString("dd/MM/yyyy");
            string estraindoUltimosNumeros = hoje_str.Substring(6, 2);


            var data = DataNascimento;
            string data_str = data.ToString("dd/MM/yyyy");
            string estraindoUltimosData = data_str.Substring(8, 2);


            var cpf = Cpf;
            string estraindoUltimosCPF = cpf.Substring(8, 3);


            string estraindoDia = hoje_str.Substring(0, 2);

            string codigo = estraindoUltimosNumeros + estraindoUltimosData + estraindoUltimosCPF + '-' + estraindoDia;

            NumeroDaConta = codigo;

            codi = NumeroDaConta;

            return codi;
        }

        /*
        private float SaldoCorrentista(string codigo, float saldo)
        {
            Codigo = codigo;
            Conta = 0;

            return saldo;
        }
        */
    }
}
