namespace Panda.App.Models.InputModels.Package
{
    using Panda.Mapping;
    using Panda.Models;

    public class PandaUserDropDownViewModel : IMapFrom<PandaUser>
    {
        public string Id { get; set; }

        public string UserName { get; set; }
    }
}
