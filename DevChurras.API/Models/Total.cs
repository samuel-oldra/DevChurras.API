using System;
using System.ComponentModel.DataAnnotations;

namespace DevChurras.API.Models
{
    public class Total
    {
        public double TotalArrecadado { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [Range(0, int.MaxValue, ErrorMessage = "Valor inválido")]
        public double TotalGastoComida { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [Range(0, int.MaxValue, ErrorMessage = "Valor inválido")]
        public double TotalGastoBebida { get; set; }

        public double TotalGasto
        {
            get
            {
                return Math.Round(this.TotalGastoComida + this.TotalGastoBebida, 2);
            }
        }

        public double Saldo
        {
            get
            {
                return Math.Round(this.TotalArrecadado - (this.TotalGastoComida + this.TotalGastoBebida), 2);
            }
        }
    }
}