using System;
using System.ComponentModel.DataAnnotations;

namespace DevChurras.API.Models
{
    public class Total
    {
        [Required(ErrorMessage = "Este campo é obrigatório")]
        [Range(0, double.MaxValue, ErrorMessage = "Valor inválido")]
        public double ValorGastoComida { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [Range(0, double.MaxValue, ErrorMessage = "Valor inválido")]
        public double ValorGastoBebida { get; set; }

        public double ValorArrecadado { get; private set; }

        public double TotalGasto
            => Math.Round(this.ValorGastoComida + this.ValorGastoBebida, 2);

        public double Saldo
            => Math.Round(this.ValorArrecadado - (this.ValorGastoComida + this.ValorGastoBebida), 2);

        public void IncrementaValorArrecadado(double valor)
            => this.ValorArrecadado += valor;
    }
}