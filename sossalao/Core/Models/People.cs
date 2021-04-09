using sossalao.Core.Utils;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sossalao.Core.Models
{
	public class People
	{
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idPeople { get; set; }
        [Required, StringLength(150, ErrorMessage = "Você precisa inserir o nome da pessoa.")]
        public string name { get; set; }
        [Required, Phone, MinLength(10), StringLength(15, ErrorMessage = "Você precisa inserir o numero de telefone da pessoa.")]
        public string phoneNumber { get; set; }
        [EmailAddress, StringLength(150, ErrorMessage = "Você precisa inserir o e-mail da pessoa corretamente.")]
        public string email { get; set; }
        [Required]
        public TypePeople typePeople { get; set; }
	}
}
