using System.ComponentModel.DataAnnotations;

namespace minhaApiWeb.Models {
    public class Convidado {
        [Key]
        public int Id { get; set; }

        [Required (ErrorMessage = "Esse campo é obrigatório")]
        [MaxLength (60, ErrorMessage = "Esse campo deve conter entre 3 e 60 caracteres")]
        [MinLength (3, ErrorMessage = "Esse campo deve conter entre 3 e 60 caracteres")]
        public string Nome { get; set; }

        [Required (ErrorMessage = "Este campo é obrigatório")]
        [Range (1, int.MaxValue, ErrorMessage = "Participante inválido")]
        public int ParticipanteId { get; set; }

        public Participante Participante { get; set; }
    }
}