using System.ComponentModel.DataAnnotations;

namespace minhaApiWeb.Models
{
    public class Participante
    {
        [Key]
        public int ParticipanteId { get; set; }

        [Required(ErrorMessage = "Esse campo é obrigatório")]
        [MaxLength(60, ErrorMessage = "Esse campo deve conter entre 3 e 60 caracteres")]
        [MinLength(3, ErrorMessage = "Esse campo deve conter entre 3 e 60 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Esse campo é obrigatório")]
        public bool ConsomeBebidaAlcoolica { get; set; }
    }
}