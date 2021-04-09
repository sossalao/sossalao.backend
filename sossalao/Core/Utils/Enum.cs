using System.ComponentModel;


namespace sossalao.Core.Utils
{
    public enum Environment
    {
        [Description("dev")]
        DEV = 0,
        [Description("prod")]
        PROD = 1,
        [Description("hml")]
        HML = 2
    }
	public enum TypeProduct
    {
        [Description("Xampu")]
        Xampu,
        [Description("Condicionador")]
        Condicionador,
        [Description("Desmaquilante para remoção de maquiagem.")]
        Desmaquilante,
        [Description("Mascara de hidratar pele.")]
        Mascara
    }
    public enum StatusScheduling
    {
        [Description("Adiado")]
        Adiado,
        [Description("Atendido")]
        Atendido,
        [Description("Cancelado")]
        Cancelado,
        [Description("Marcado")]
        Marcado
    }
    public enum TypePeople
    {
        [Description("Cliente")]
        Client,
        [Description("Funcionario")]
        Employee
    }
    public enum TypeEmployee
    {
        [Description("Efetivo")]
        Efetivo,
        [Description("Freelancer")]
        Freelancer,
        [Description("Parceria")]
        Parceria
    }
    public enum TypeArea
    {
        [Description("Manicure e Pedicure")]
        ManicurePedicure,
        [Description("Maquiagem e Embelezamento")]
        MaquiagemEmbelezamento,
        [Description("Esteticismo")]
        Esteticismo,
        [Description("Cabeleireiro e Hair Stylist")]
        CabeleireiroHairStylist
    }
	public enum AccessLevel
	{
        Master,
        HotScissor,
        BluntScissor
    }
}
