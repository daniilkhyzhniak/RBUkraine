using RBUkraine.BLL.Enums;

namespace RBUkraine.BLL.Models.CharitableOrganization
{
    public class CharitableOrganizationFilterModel
    {
        public CharitableOrganizationSearchOptions SearchOptions { get; set; } = CharitableOrganizationSearchOptions.ByName;

        public string Search { get; set; }

        public CharitableOrganizationSortOptions SortOptions { get; set; } = CharitableOrganizationSortOptions.ByName;

        public SortDirection SortDirection { get; set; } = SortDirection.Asc;
    }
}
