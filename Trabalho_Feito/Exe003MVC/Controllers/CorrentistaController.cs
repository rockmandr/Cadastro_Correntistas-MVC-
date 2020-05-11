using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exe003MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace Exe003MVC.Controllers
{
    public class CorrentistaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Cadastrar()
        {
            var model = new Correntista();
            return View(model);
        }


        [HttpPost]
        public IActionResult Cadastrar(Correntista users, string nome, DateTime data, string cpf, int rg,
        int cep, string rua, string cidade, string estado, string pais, float salario, float patrimonio,
        string empresaEmQueTrabalha, int tempoNoEmprego, string cargo, string numeroDaConta)
        {
            //Criamos a variavél idadeValida que armazena o 
            //resultado da validação da regra de negócio da idade 
            //ser maior ou igual a 16
            if (users.ValidarIdade() == false)
            {
                ModelState.AddModelError("DataNascimento", "É necessário a idade maior ou igual a 16 anos!");
            }
            else if (users.ValidaCpf(cpf) == false)
            {
                ModelState.AddModelError("Cpf", "É necessário CPF valído!");
            }
            else if (users.ValidaPais() == false)
            {
                ModelState.AddModelError("Pais", "O(a) Correntista deve morar no Brasil!");
            }

            /*
            else if (users.CodigoCorrentista(numeroDaConta) != "")
            {
                ModelState.AddModelError("NumeroDaConta", "'-'");
            }
            */

            return View(users);
        }


        [HttpPost()]
        public IActionResult Details(string nome, DateTime data, string cpf, int rg,
        int cep, string rua, string cidade, string estado, string pais, float salario, float patrimonio,
        string empresaEmQueTrabalha, int tempoNoEmprego, string cargo, string numeroDaConta)
        {

            Correntista users = new Correntista();

            users.Nome = nome;
            users.DataNascimento = data;
            users.Cpf = cpf;
            users.Rg = rg;

            users.Cep = cep;
            users.Rua = rua;
            users.Cidade = cidade;
            users.Estado = estado;
            users.Pais = pais;

            users.Salario = salario;
            users.Patrimonio = patrimonio;

            users.EmpresaEmQueTrabalha = empresaEmQueTrabalha;
            users.TempoNoEmprego = tempoNoEmprego;
            users.Cargo = cargo;
            users.NumeroDaConta = numeroDaConta;

            users.CodigoCorrentista(numeroDaConta);

            return View(users);

        }
    }
}