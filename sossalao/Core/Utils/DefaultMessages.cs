using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sossalao.Core.Utils
{
	public class DefaultMessages
	{
		public const string nonStandardCreate = "Não foi possivel enviar os dados para cadasto, pois os dados não seguem o padrão de cadastro.";
		public const string nonStandardUpdate = "Não foi possivel enviar os dados para atualizar, pois os dados não seguem o padrão de atualização.";
		public const string internalfailureCreate = "Ocorreu uma falha interna e não foi possível cadastrar.";
		public const string internalfailureUpdate = "Ocorreu uma falha interna e não foi possível atualizar.";
		public const string notFound = "Não foi encontrado.";
		public const string DateValidationProblem = "Por favor, valide as datas! Pois a data final tem que ser maior que a data incial.";

	}
}
